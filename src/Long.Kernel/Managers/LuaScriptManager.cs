using Long.Kernel.Scripting.LUA;
using Long.Kernel.States.User;
using Long.Kernel.States;
using Long.Kernel.States.Items;

namespace Long.Kernel.Managers
{
    public class LuaScriptManager
    {
        private static readonly ILogger logger = Log.ForContext<LuaScriptManager>();
        private static LuaProcessor processor;
        private static bool initialized = false;

        public static Task InitializeAsync()
        {
            if (processor != null)
            {
                logger.Error("Attempt of reloading lua processor!");
                return Task.CompletedTask;
            }

            logger.Information("Loading LUA scripts");
            processor = new LuaProcessor();
            initialized = true;
            return Task.CompletedTask;
        }

        public static void Reload(int idScript)
        {
            if (initialized)
            {
                processor.ReloadScript(idScript);
            }
        }

        public static void Run(Character user, Role role, Item item, string[] input, string script)
        {
            if (initialized)
            {
                processor.Execute(user, role, item, input, script);
            }
        }

        public static void Run(string script)
        {
            if (initialized)
            {
                processor.Execute(null, null, null, null, script);
            }
        }

        public static string ParseTaskDialogAnswerToScript(string task)
        {
            string result = string.Empty;
            bool typeFetched = false;
            string tempType = string.Empty;
            string tempMessage = string.Empty;
            for (int i = 0; i < task.Length; i++)
            {
                if (task[i].Equals('<'))
                {
                    if (typeFetched)
                    {
                        if (tempType.Contains('F'))
                        {
                            result = tempMessage + "(";
                        }
                        else if (tempType.Contains('S'))
                        {
                            result += $"'{tempMessage}',";
                        }
                        else if (tempType.Contains('N'))
                        {
                            result += $"{tempMessage},";
                        }

                        typeFetched = false;
                    }

                    tempType = string.Empty;
                    tempMessage = string.Empty;
                    tempType += task[i];
                }
                else if (task[i].Equals('>'))
                {
                    typeFetched = true;
                    tempType += task[i];
                }
                else if (typeFetched)
                {
                    tempMessage += task[i];
                }
                else
                {
                    tempType += task[i];
                }
            }

            if (typeFetched && !string.IsNullOrEmpty(tempMessage))
            {
                if (tempType.Contains('F'))
                {
                    result += tempMessage + "(";
                }
                else if (tempType.Contains('S'))
                {
                    result += $"'{tempMessage}',";
                }
                else if (tempType.Contains('N'))
                {
                    result += $"{tempMessage},";
                }
            }

            result = result.Trim(',');
            result += ")";
            return result;
        }
    }
}
