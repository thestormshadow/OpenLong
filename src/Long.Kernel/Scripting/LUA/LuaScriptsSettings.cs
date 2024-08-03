using Microsoft.Extensions.Configuration;

namespace Long.Kernel.Scripting.LUA
{
    public class LuaScriptsSettings
    {
        public LuaScriptsSettings()
        {
            new ConfigurationBuilder()
                .AddIniFile(Path.Combine(Environment.CurrentDirectory, "lua", "lua.ini"))
                .Build()
                .Bind(this);
        }

        public Dictionary<int, string> Scripts { get; set; }
    }
}