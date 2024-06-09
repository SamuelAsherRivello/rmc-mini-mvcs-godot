using Godot;
using RMC.Core.Architectures.Mini.Controller;
using RMC.Core.Architectures.Mini.Samples.ScoreMini.Mini.View;

namespace RMC.Core.Architectures.Mini.Samples.ScoreMini.Mini
{
    /// <summary>
    /// The Controller coordinates everything between
    /// the <see cref="IConcern"/>s and contains the core app logic 
    /// </summary>
    public class ScoreController: BaseController // Extending 'base' is optional
        <ScoreModel, ScoreView, ScoreService>
    {
        
        //  Initialization  -------------------------------
        public ScoreController(
            ScoreModel model, ScoreView view, ScoreService service)
            : base(model, view, service)
        {
        }
        public override async void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);
                
                // Model
                _model.IsSignedIn.OnValueChanged.AddListener(IsSignedIn_OnValueChanged);
                _model.Score.OnValueChanged.AddListener(ClickCount_OnValueChanged);
                
                // View
                _view.OnAddPoints.AddListener(View_OnUserAction);
                
                // Services - Local
                _service.OnSignInComplete.AddListener(Service_OnSignInComplete);
                
                // Init
                if (!_model.IsSignedIn.Value)
                {
                    await _service.SignInAsync();
                }
                else
                {
                    _model.IsSignedIn.OnValueChangedRefresh();
                }

            }
        }

        public override void Dispose()
        {
            // Model
            _model.IsSignedIn.OnValueChanged.RemoveListener(IsSignedIn_OnValueChanged);
                
            // Service  
            _service.OnSignInComplete.RemoveListener(Service_OnSignInComplete);
        }
        
        //  Methods ---------------------------------------

        
        //  Event Handlers --------------------------------
        private void Service_OnSignInComplete(bool previousValue, bool currentValue)
        {
            RequireIsInitialized();

            GD.Print("Controller's Service_OnSignInComplete");

            _model.IsSignedIn.Value = currentValue;
        }
        
        private void IsSignedIn_OnValueChanged(bool previousValue, bool currentValue)
        {
            RequireIsInitialized();
            
            _view.RefreshUI();
        }
        private void ClickCount_OnValueChanged(int previousValue, int currentValue)
        {
            RequireIsInitialized();
            
            _view.RefreshUI();
        }
        
        private void View_OnUserAction(bool previousValue, bool currentValue)
        {
            RequireIsInitialized();

            _model.Score.Value++;
        }
    }
}