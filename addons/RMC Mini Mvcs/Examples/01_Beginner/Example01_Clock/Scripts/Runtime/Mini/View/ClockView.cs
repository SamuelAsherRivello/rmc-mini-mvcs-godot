using System;
using Godot;
using RMC.Mini.Examples.Clock.Mini.Controller.Commands;
using RMC.Mini.View;

namespace RMC.Mini.Examples.Clock.Mini.View
{
    /// <summary>
    /// The View handles user interface and user input
    /// </summary>
    public class ClockView: IView
    {
        //  Properties ------------------------------------
        public bool IsInitialized { get { return _isInitialized;} }
        public IContext Context { get { return _context;} }
        
        //  Fields ----------------------------------------
        private bool _isInitialized = false;
        private IContext _context;
        
        //  Initialization  -------------------------------
        public void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                _isInitialized = true;
                _context = context;
                
                //
                Context.CommandManager.AddCommandListener<TimeChangedCommand>(
                    OnTimeChangedCommand);
            }
        }
        
        public void RequireIsInitialized()
        {
            if (!IsInitialized)
            {
                throw new Exception("MustBeInitialized");
            }
        }
        
        
        //  Methods ---------------------------------------
        
        
        //  Event Handlers --------------------------------
        private void OnTimeChangedCommand(TimeChangedCommand timeChangedCommand)
        {
            RequireIsInitialized();
            
            // For simplicity, the 'rendering' of our view is just a debug log.
            GD.Print($"View's OnTimeChangedCommand({timeChangedCommand.CurrentValue})");
        }

    }
}