using Godot;
using RMC.Core.Utilities;
using RMC.Mingletons;
using RMC.Mini.Controller;
using RMC.Mini.Service;
using RMC.Mini.View;

namespace RMC.Mini.Features.SceneSystem
{
    /// <summary>
    /// The Controller coordinates everything between
    /// the <see cref="IConcern"/>s and contains the core app logic 
    /// </summary>
    public class SceneSystemController: BaseController // Extending 'base' is optional
        <SceneSystemModel, DummyView, DummyService> 
    {
        public SceneSystemController(
            SceneSystemModel model, DummyView view, DummyService service) 
            : base(model, view, service)
        {
        }

        
        //  Initialization  -------------------------------
        public override void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);
                Context.CommandManager.AddCommandListener<LoadSceneRequestCommand>(OnLoadSceneRequestCommand);
            }
        }

        
        //  Methods ---------------------------------------
        
        
        //  Event Handlers --------------------------------
        
        /// <summary>
        /// Before the change is made
        /// </summary>
        /// <param name="loadSceneRequestCommand"></param>
        private void OnLoadSceneRequestCommand(
            LoadSceneRequestCommand loadSceneRequestCommand)
        {
            RequireIsInitialized();

            // Do not change scene if already in it
            if (loadSceneRequestCommand.SceneName ==
                Mingleton.Instance.SceneTree.CurrentScene.Name)
            {
                return;
            }

            string scenePath = FileAccessUtility.FindFileOnceInResources(loadSceneRequestCommand.SceneName);
            var packedScene = GD.Load<PackedScene>(scenePath);
            
            if (loadSceneRequestCommand.LoadSceneMode == LoadSceneMode.Additive)
            {
                // Add next scene
                var scene = packedScene.Instantiate();
                Mingleton.Instance.SceneTree.Root.AddChild(scene);
            }
            else
            {
                // Replace with next scene
                Mingleton.Instance.SceneTree.ChangeSceneToPacked(packedScene);
            }
        }
    }
}