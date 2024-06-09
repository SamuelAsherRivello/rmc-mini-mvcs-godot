using Godot;
using RMC.Mini.Controller;
using RMC.Mini.Examples.Score.Mini.View;

namespace RMC.Mini.Examples.Score.Mini
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
        public override void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);
                
                // Model
                _model.IsSignedIn.OnValueChanged.AddListener(IsSignedIn_OnValueChanged);
                _model.Score.OnValueChanged.AddListener(Points_OnValueChanged);
                
                // View
                _view.OnAddPoints.AddListener(View_OnAddPoints);
                _view.OnResetPoints.AddListener(View_OnResetPoints);
                
                // Services
                _service.OnInt.AddListener(Service_OnLoaded);
                
                // Init
                if (!_model.IsSignedIn.Value)
                {
                    _service.Load();
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
            _service.OnInt.RemoveListener(Service_OnLoaded);
        }
        
        //  Methods ---------------------------------------

        
        //  Event Handlers --------------------------------
        private void Service_OnLoaded(int scoreInitial)
        {
            RequireIsInitialized();

            GD.Print("Controller's Service_OnSignInComplete");

            _model.Score.Value = scoreInitial;
            _model.IsSignedIn.Value = true;
        }
        
        private void IsSignedIn_OnValueChanged(bool previousValue, bool currentValue)
        {
            RequireIsInitialized();
            
            _view.RefreshUI();
        }
        private void Points_OnValueChanged(int previousValue, int currentValue)
        {
            RequireIsInitialized();
            
            GD.Print($"Controller's Points_OnValueChanged({previousValue}, {currentValue})");
         
            _view.RefreshUI();
        }
        
        private void View_OnAddPoints()
        {
            RequireIsInitialized();

            _model.Score.Value++;
        }
        
        private void View_OnResetPoints()
        {
            RequireIsInitialized();
        
            _model.Score.Value = ScoreModel.ScoreDefaultValue;
        }
        
    }
}