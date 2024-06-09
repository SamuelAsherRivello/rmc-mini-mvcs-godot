using Godot;
using RMC.Core.Architectures.Mini.Samples.UGS.Mini;

public partial class TestMiniMvcsScene : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TestMiniMvcs testMiniMvcs = new TestMiniMvcs();
		testMiniMvcs.Initialize();
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
