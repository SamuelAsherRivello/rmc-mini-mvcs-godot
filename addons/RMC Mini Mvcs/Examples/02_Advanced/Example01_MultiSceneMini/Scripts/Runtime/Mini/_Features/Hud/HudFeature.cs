using RMC.Mini.Features;

namespace RMC.Mini.Examples.MultiScene.Mini.Feature.Hud
{
    /// <summary>
    /// Set up a collection of related <see cref="IConcern"/> instances
    /// </summary>
    public class HudFeature: BaseFeature // Extending 'base' is optional
    {
        //  Initialization --------------------------------

        //  Methods ---------------------------------------
        public override void Build()
        {
            RequireIsInitialized();
            
            // Get from mini
            MultiSceneModel model = MiniMvcs.ModelLocator.GetItem<MultiSceneModel>();
            MultiSceneService service = MiniMvcs.ServiceLocator.GetItem<MultiSceneService>();
            
            // Create new controller
            HudController controller = 
                new HudController(model, View as HudView, service);
            
            // Add to mini
            MiniMvcs.ControllerLocator.AddItem(controller);
            MiniMvcs.ViewLocator.AddItem(View);
      
            // Initialize
            View.Initialize(MiniMvcs.Context);
            controller.Initialize(MiniMvcs.Context);
        }

        public override void Dispose()
        {
            RequireIsInitialized();
            
            if (MiniMvcs.ControllerLocator.HasItem<HudController>())
            {
                MiniMvcs.ControllerLocator.GetItem<HudController>().Dispose();
                MiniMvcs.ControllerLocator.RemoveItem<HudController>();
                MiniMvcs.ViewLocator.RemoveItem<HudView>();
            }
        }
    }
}