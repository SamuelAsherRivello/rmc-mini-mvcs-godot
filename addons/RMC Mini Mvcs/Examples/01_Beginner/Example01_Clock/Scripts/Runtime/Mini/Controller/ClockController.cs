using System.Diagnostics;
using System.Threading.Tasks;
using Godot;
using RMC.Mini.Controller;
using RMC.Mini.Examples.Clock.Mini.Controller.Commands;
using RMC.Mini.Examples.Clock.Mini.Model;
using RMC.Mini.Examples.Clock.Mini.Service;
using RMC.Mini.Examples.Clock.Mini.View;

namespace RMC.Mini.Examples.Clock.Mini.Controller
{
    /// <summary>
    /// The Controller coordinates everything between
    /// the <see cref="IConcern"/>s and contains the core app logic 
    /// </summary>
    public class ClockController: BaseController // Extending 'base' is optional
        <ClockModel, ClockView, ClockService> 
    {

        //  Fields ----------------------------------------
        // Initialize and start the stopwatch
        private Stopwatch _stopwatch = new Stopwatch();


        //  Initialization  -------------------------------
        public ClockController(ClockModel model, ClockView view, ClockService service)
            : base(model, view, service)
        {
            
        }
        
        
        public override void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);
                
                _model.Time.OnValueChanged.AddListener(Model_OnTimerChanged);
                _service.OnLoaded.AddListener(Service_OnLoaded);
                _service.Load();
            }
        }
        
        public override void Dispose()
        {
            // Model
            _model.Time.OnValueChanged.RemoveListener(Model_OnTimerChanged);
                
            // Service  
            _service.OnLoaded.RemoveListener(Service_OnLoaded);
        }
        
        //  Methods ---------------------------------------
        private void StartTicking()
        {
            RequireIsInitialized();

            _stopwatch.Start();
            
            ClockHelper.StartTicking(async() => 
            {
                // Round the number for readability
                float time = (float)System.Math.Round(_stopwatch.Elapsed.TotalSeconds, 0);
                _model.Time.Value = time;
                
                // Wait 
                int timeDelayInMilliSeconds = _model.TimeDelayInSeconds.Value * 1000;
                await Task.Delay(timeDelayInMilliSeconds);
            });
        }

        
        //  Event Handlers --------------------------------
        private void Service_OnLoaded(int timeDelayInSeconds)
        {
            RequireIsInitialized();
            
            GD.Print($"Controller's Service_OnLoaded({timeDelayInSeconds})");
            
            _model.TimeDelayInSeconds.Value = timeDelayInSeconds;
            
            StartTicking();
        }
        
        
        private void Model_OnTimerChanged(float previousValue, float currentValue)
        {
            RequireIsInitialized();
            
            // Demo - Controller may update view INDIRECTLY...
            Context.CommandManager.InvokeCommand(
                new TimeChangedCommand(previousValue, currentValue));

        }
    }
}