using RMC.Mini.Service;
using RMC.Mini.View;

namespace RMC.Mini.Features.SceneSystem
{
    /// <summary>
    /// Set up a collection of related <see cref="IConcern"/> instances
    /// </summary>
    public class SceneSystemFeature: BaseFeature // Extending 'base' is optional
    {
        //  Properties ------------------------------------
        
        //  Fields ----------------------------------------

        //  Methods ---------------------------------------
        public override void Build()
        {
            RequireIsInitialized();
            
            //Model
            SceneSystemModel sceneSystemModel = new SceneSystemModel();
            
            // Create new controller
            SceneSystemController controller = new SceneSystemController
            (
                sceneSystemModel, 
                new DummyView(), 
                new DummyService()
            );
            
            // Add to mini
            sceneSystemModel.Initialize(MiniMvcs.Context);
            MiniMvcs.ControllerLocator.AddItem(controller);
                  
            // Initialize
            controller.Initialize(MiniMvcs.Context);
        }

        
        public override void Dispose()
        {
            RequireIsInitialized();
            
            if (MiniMvcs.ControllerLocator.HasItem<SceneSystemController>())
            {
                MiniMvcs.ControllerLocator.RemoveItem<SceneSystemController>();
            }
            
            if (MiniMvcs.ModelLocator.HasItem<SceneSystemModel>())
            {
                MiniMvcs.ModelLocator.RemoveItem<SceneSystemModel>();
            }
        }
    }
}
