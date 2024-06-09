using Godot;
using RMC.Core.Architectures.Mini.Controller;
using RMC.Core.Architectures.Mini.Samples.UGS.Mini.Model;
using RMC.Core.Architectures.Mini.Samples.UGS.Mini.Service;
using RMC.Core.Architectures.Mini.Samples.UGS.Mini.View;

namespace RMC.Core.Architectures.Mini.Samples.UGS.Mini.Controller
{
    /// <summary>
    /// The Controller coordinates everything between
    /// the <see cref="IConcern"/>s and contains the core app logic 
    /// </summary>
    public class TestController: BaseController // Extending 'base' is optional
        <TestModel, TestView, TestService>
    {

        
        //  Initialization  -------------------------------
        public TestController(
            TestModel model, TestView view, TestService service)
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
                
                // View
                _view.OnUserAction.AddListener(View_OnUserAction);
                
                // Services - Local
                GD.Print("listening");
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
        private void IsSignedIn_OnValueChanged(bool previousValue, bool currentValue)
        {
            RequireIsInitialized();
            
            _view.RefreshUI();
        }
        
        private void View_OnUserAction(bool oldvalue, bool newvalue)
        {
            RequireIsInitialized();
            
            GD.Print("controller hears views user action");
        }

        
        private void Service_OnSignInComplete(bool previousValue, bool currentValue)
        {
            RequireIsInitialized();

            GD.Print("Service_OnSignInComplete");
            _model.IsSignedIn.Value = true;
        }
    }
}