using Godot;
using RMC.Core.Architectures.Mini.Samples.ScoreMini.Mini.View;

namespace RMC.Core.Architectures.Mini.Samples.ScoreMini.Mini
{
    public partial class Main : Node
    {
        [Export] 
        private ScoreView _scoreView;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            ScoreMini scoreMini = new ScoreMini();
            scoreMini.AddView(_scoreView);
            scoreMini.Initialize();
		    
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
        }
    }
}