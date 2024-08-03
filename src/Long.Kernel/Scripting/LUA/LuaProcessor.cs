#if DEBUG
// This lock must be used when supporting LUA Scripts that call another LUA Script! That happens in some parts of the game and the plan is to make sure
// that this won't break the execution. Because when a LUA Script is running the older data will be overriden, so we need to save states to make sure
// that when the child script finishes it's execution, the parent context must be back. This must be heavily tested!
#define TEST_REENTRANT_LOCK
#endif

using NLua;
using Long.Kernel.States.User;
using Long.Kernel.States;
using Canyon.Game.Scripting.Attributes;
using Long.Kernel.States.Items;
using Long.Shared.Helpers;

namespace Long.Kernel.Scripting.LUA
{
    public sealed partial class LuaProcessor
    {
        private static readonly ILogger logger = Logger.CreateSysConsoleLogger("lua");

        private readonly Lua lua;
        private object syncObject = new object();
        private List<LuaExecutionInfo> executionInfoList = new List<LuaExecutionInfo>();

        private string currentScript;
        private Character user;
        private Role role;
        private Item item;
        private string[] input;

        public LuaProcessor()
        {
            lua = new Lua();
            Initialize();
        }

        private void Initialize()
        {
            foreach (var item in new LuaScriptsSettings().Scripts)
            {
                string[] splitPath = item.Value.Split(new char[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries);
                string realPath = Path.Combine(splitPath);
                realPath = Path.Combine(Environment.CurrentDirectory, "lua", realPath);
                if (!File.Exists(realPath))
                {
                    logger.Warning("Script file \"{0}\" not found!", item.Value);
                    continue;
                }

#if DEBUG
                logger.Debug("Loading LUA Script {0}={1}", item.Key, Path.Combine(splitPath));
#endif
                lua.DoFile(realPath);
            }
            RegisterLocalFunctions();
            Execute(null, null, null, Array.Empty<string>(), "Event_Server_Start()");
        }

        private void RegisterLocalFunctions()
        {
            foreach (var method in GetType().GetMethods().Where(x => x.IsPublic))
            {
                var customAttributes = method.GetCustomAttributes(false);
                if (customAttributes.All(x => x is not LuaFunctionAttribute))
                {
                    // skip not mapped
                    continue;
                }

#if DEBUG
                logger.Debug("Lua function {0} registered!", method.Name);
#endif

                lua.RegisterFunction(method.Name, this, method);
            }
        }

        public void ReloadScript(int idScript)
        {
            lock (syncObject)
            {
                if (new LuaScriptsSettings().Scripts.TryGetValue(idScript, out var file))
                {
                    string[] splitPath = file.Split('\\');
                    string realPath = Path.Combine(splitPath);
                    realPath = Path.Combine(Environment.CurrentDirectory, "lua", realPath);
                    if (!File.Exists(realPath))
                    {
                        logger.Warning("Script file \"{0}\" not found!", realPath);
                        return;
                    }
                    lua.DoFile(realPath);
                }
            }
        }

        public bool Execute(Character user, Role role, Item item, string[] input, string script)
        {
            lock (syncObject)
            {
                try
                {
#if TEST_REENTRANT_LOCK
                    executionInfoList.Add(new LuaExecutionInfo
                    {
                        Input = input,
                        Script = script,
                        User = user,
                        Role = role,
                        Item = item,
                    });
#endif

                    currentScript = script;
                    this.user = user;
                    this.role = role;
                    this.item = item;
                    this.input = input;

                    lua.DoString(script);
                    return true;

                }
                catch (Exception ex)
                {
                    logger.Error("An error occurred when executing LUA Scripts!!! [{0}]: {1}", script, ex.Message);
                    return false;
                }
                finally
                {
#if TEST_REENTRANT_LOCK
                    if (executionInfoList.Count > 1)
                    {
                        executionInfoList.RemoveAt(executionInfoList.Count - 1);
                        int currIndex = executionInfoList.Count - 1;
                        this.user = executionInfoList[currIndex].User;
                        this.role = executionInfoList[currIndex].Role;
                        this.item = executionInfoList[currIndex].Item;
                        this.input = executionInfoList[currIndex].Input;
                        currentScript = executionInfoList[currIndex].Script;
                    }
                    else
                    {
#endif
                        currentScript = string.Empty;
                        this.user = null;
                        this.role = null;
                        this.item = null;
                        this.input = Array.Empty<string>();
#if TEST_REENTRANT_LOCK
                        executionInfoList.Clear();
                    }
#endif
                }
            }
        }

#if TEST_REENTRANT_LOCK
        /// <summary>
        /// This execution info makes it possible to make multiple calls to the LUA Script from inside of another script. Since we lock the instance for execution,
        /// we need to make sure that we are executing the data in the same context with the same data.
        /// Must not forget to queue map actions inside of LUA.
        /// Remind that locks are reentrant, so same thread may enter the same lock and must exit all.
        /// </summary>
        private struct LuaExecutionInfo
        {
            public string Script { get; set; }
            public Character User { get; set; }
            public Role Role { get; set; }
            public Item Item { get; set; }
            public string[] Input { get; set; }
        }
#endif
    }
}