using RMC.Mini.Examples.MultiScene.Mini.Feature.Hud;
using RMC.Mini.Features;
using RMC.Mini.Service;

namespace RMC.Mini.Examples.MultiScene.Mini.Feature.Game
{
    /// <summary>
    /// Set up a collection of related <see cref="IConcern"/> instances
    /// </summary>
    public class GameFeature: BaseFeature // Extending 'base' is optional
    {
        //  Initialization --------------------------------

        //  Methods ---------------------------------------
        
        public override void Build()
        {
            RequireIsInitialized();
            
            // Get from mini
            MultiSceneModel multiSceneModel = MiniMvcs.ModelLocator.GetItem<MultiSceneModel>();
            
            // Create new controller
            GameController controller = 
                new GameController(multiSceneModel, View as GameView, new DummyService());
            
            // Add to mini
            MiniMvcs.ControllerLocator.AddItem(controller);
            MiniMvcs.ViewLocator.AddItem(View);
            
            // Initialize
            View.Initialize(MiniMvcs.Context);
            controller.Initialize(MiniMvcs.Context);
     
            // Set Mode
            multiSceneModel.HasNavigationBack.Value = true;
        }

        public override void Dispose()
        {
            RequireIsInitialized();
            
            if (MiniMvcs.ControllerLocator.HasItem<GameController>())
            {
                MiniMvcs.ControllerLocator.GetItem<GameController>().Dispose();
                MiniMvcs.ControllerLocator.RemoveItem<GameController>();
                MiniMvcs.ViewLocator.RemoveItem<GameView>();
            }
        }

        //  Event Handlers --------------------------------

    }
}