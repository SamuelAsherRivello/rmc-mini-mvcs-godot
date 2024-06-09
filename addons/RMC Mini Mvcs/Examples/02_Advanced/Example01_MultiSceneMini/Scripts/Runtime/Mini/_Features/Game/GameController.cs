using RMC.Core.Events;
using RMC.Mini.Controller;
using RMC.Mini.Examples.MultiScene.Mini.Feature.Hud;
using RMC.Mini.Service;

namespace RMC.Mini.Examples.MultiScene.Mini.Feature.Game
{
    /// <summary>
    /// The Controller coordinates everything between
    /// the <see cref="IConcern"/>s and contains the core app logic 
    /// </summary>
    public class GameController: BaseController // Extending 'base' is optional
        <MultiSceneModel, GameView, DummyService> 
    {
        public GameController(
            MultiSceneModel model, GameView view, DummyService service) 
            : base(model, view, service)
        {
        }
        
        //  Events ---------------------------------------
        public readonly RmcEvent OnFun = new RmcEvent();
        
        //  Initialization  -------------------------------
        public override void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);
                
                // Model
                _model.HasLoadedService.OnValueChanged.AddListener(HasLoadedService_OnValueChanged);
                    
                // View
                _view.OnFun.AddListener(View_OnFun);
                
            }
        }

        //  Methods ---------------------------------------

        
        //  Event Handlers --------------------------------
        private void HasLoadedService_OnValueChanged(bool oldvalue, bool newvalue)
        {
            _view.RefreshUI();
        }
        
        private void View_OnFun()
        {
            MultiSceneModel multiSceneModel = Context.ModelLocator.GetItem<MultiSceneModel>();
            _view.ShowMessage(multiSceneModel.FunMessage.Value);
        }
    }
}