using Godot;
using RMC.Mini.Controller;
using RMC.Mini.Examples.MultiScene.Standard;
using RMC.Mini.Features.SceneSystem;

namespace RMC.Mini.Examples.MultiScene.Mini.Feature.Hud
{
    /// <summary>
    /// The Controller coordinates everything between
    /// the <see cref="IConcern"/>s and contains the core app logic 
    /// </summary>
    public class HudController: BaseController // Extending 'base' is optional
        <MultiSceneModel, HudView, MultiSceneService> 
    {
        public HudController(
            MultiSceneModel model, HudView view, MultiSceneService service) 
            : base(model, view, service)
        {
            
        }

        
        //  Initialization  -------------------------------
        public override void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);
                
                // Model
                _model.HasLoadedService.OnValueChanged.AddListener(HasLoadedService_OnValueChanged);
                _model.HasNavigationBack.OnValueChanged.AddListener(HasNavigationBack_OnValueChanged);
                
                // View
                _view.OnBack.AddListener(View_OnBack);
                
                // Service
                _service.OnLoaded.AddListener(Service_OnLoaded);
                if (!_model.HasLoadedService.Value)
                {
                    _service.Load();
                }
            }
        }



        public override void Dispose()
        {
            base.Dispose();
            
            _model.HasLoadedService.OnValueChanged.RemoveListener(HasLoadedService_OnValueChanged);
            _model.HasNavigationBack.OnValueChanged.RemoveListener(HasNavigationBack_OnValueChanged);
            _view.OnBack.RemoveListener(View_OnBack);
            
        }


        //  Godot Methods ---------------------------------------
        
        
        //  Methods ---------------------------------------
        
        
        //  Event Handlers --------------------------------

        private void HasNavigationBack_OnValueChanged(bool oldvalue, bool newvalue)
        {
            RequireIsInitialized();
            _view.RefreshUI();
        }

        private void HasLoadedService_OnValueChanged(bool oldvalue, bool newvalue)
        {
            RequireIsInitialized();
            _view.RefreshUI();
        }
        

        
        private void View_OnBack()
        {
            RequireIsInitialized();
            
            // Demonstrates proper Controller-to-Controller communication with a Command
            Context.CommandManager.InvokeCommand(
                new LoadSceneRequestCommand(MultiSceneMiniConstants.Scene01_Menu, LoadSceneMode.Single));
        }

        
        private void Service_OnLoaded(string funMessage)
        {
            RequireIsInitialized();
            GD.Print($"HudController.Service_OnLoaded({funMessage})");
            _model.HasLoadedService.Value = true;
            _model.FunMessage.Value = funMessage;
        }
    }
}