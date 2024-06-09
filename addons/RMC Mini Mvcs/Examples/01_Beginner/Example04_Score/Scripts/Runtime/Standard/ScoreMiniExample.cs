using Godot;
using RMC.Mini.Examples.Score.Mini.View;

namespace RMC.Mini.Examples.Score.Mini
{
    public partial class ScoreMiniExample : Node
    {
        //  Properties ------------------------------------

        
        //  Fields ----------------------------------------
        [Export] 
        private ScoreView _scoreView;
        
        
        //  Godot Methods   -------------------------------
        
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            GD.Print($"ScoreMiniExample._Ready()");
            
            ScoreMini scoreMini = new ScoreMini(_scoreView);
            scoreMini.Initialize();
		    
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


