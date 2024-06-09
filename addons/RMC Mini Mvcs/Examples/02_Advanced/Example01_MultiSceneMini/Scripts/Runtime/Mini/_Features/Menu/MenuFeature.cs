using RMC.Mini.Examples.MultiScene.Mini.Feature.Hud;
using RMC.Mini.Features;
using RMC.Mini.Service;

namespace RMC.Mini.Examples.MultiScene.Mini.Feature.Menu
{
    /// <summary>
    /// Set up a collection of related <see cref="IConcern"/> instances
    /// </summary>
    public class MenuFeature: BaseFeature // Extending 'base' is optional
    {
        //  Initialization --------------------------------

        //  Methods ---------------------------------------
        public override void Build()
        {
            RequireIsInitialized();
            
            // Get from mini
            MultiSceneModel multiSceneModel = MiniMvcs.ModelLocator.GetItem<MultiSceneModel>();
            
            // Create new controller
            MenuController controller = 
                new MenuController(multiSceneModel, View as MenuView, new DummyService());
            
            // Add to mini
            MiniMvcs.ControllerLocator.AddItem(controller);
            MiniMvcs.ViewLocator.AddItem(View);
            
            // Initialize
            View.Initialize(MiniMvcs.Context);
            controller.Initialize(MiniMvcs.Context);
            
            // Set Mode
            multiSceneModel.HasNavigationBack.Value = false;
        }

        
        public override void Dispose()
        {
            RequireIsInitialized();
            
            if (MiniMvcs.ControllerLocator.HasItem<MenuController>())
            {
                //TODO: Maybe make RemoveItem(willDispose==true) for all locators?
                MiniMvcs.ControllerLocator.GetItem<MenuController>().Dispose();
                MiniMvcs.ControllerLocator.RemoveItem<MenuController>();
                MiniMvcs.ViewLocator.RemoveItem<MenuView>();
            }
        }
    }
}