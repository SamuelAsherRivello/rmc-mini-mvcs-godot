#if TOOLS
using Godot;
using RMC.Core.Debug;

namespace RMC.Mini
{
    [Tool]
    public partial class Plugin : EditorPlugin
    {
        private const string PluginName = "RMC Mini Mvcs";
        private const bool IsLoggerEnabled = true;
        private static readonly ILogger _logger = _logger = new Logger(IsLoggerEnabled) { Prefix = $"[{PluginName}]" };
        
        public override void _EnterTree()
        {
            _logger.PrintEmptyLine();
            _logger.PrintHeader(PluginName);
            _logger.Print($"Plugin enabled.");
            _logger.PrintDivider();
        }

        public override void _ExitTree()
        {
            _logger.PrintEmptyLine();
            _logger.PrintHeader(PluginName);
            _logger.Print($"Plugin disabled.");
            _logger.PrintDivider();
        }

    }
}

#endif