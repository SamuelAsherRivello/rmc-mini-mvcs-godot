using Godot;

namespace RMC.Mini.Examples.Clock.Mini
{
    public partial class ClockMiniExample : Node
    {
        //  Properties ------------------------------------

        
        //  Fields ----------------------------------------
        
        
        //  Godot Methods   -------------------------------
        
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            GD.Print($"ClockMiniExample._Ready()");
            
            ClockMini clockMini = new ClockMini();
            clockMini.Initialize();
        }

        /// <summary>
        /// Called every frame. 'delta' is the elapsed time since the previous frame.
        /// </summary>
        public override void _Process(double delta)
        {
        }
        
        //  Other Methods ---------------------------------
        
        
        //  Event Handlers --------------------------------
    }
}
