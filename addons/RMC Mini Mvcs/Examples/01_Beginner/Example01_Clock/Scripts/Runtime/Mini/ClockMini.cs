using RMC.Mini.Examples.Clock.Mini.Controller;
using RMC.Mini.Examples.Clock.Mini.Model;
using RMC.Mini.Examples.Clock.Mini.Service;
using RMC.Mini.Examples.Clock.Mini.View;

namespace RMC.Mini.Examples.Clock.Mini
{
    /// <summary>
    /// See <see cref="SimpleMiniMvcs{TContext,TModel,TView,TController,TService}"/>"/>
    /// </summary>
    public class ClockMini: SimpleMiniMvcs
        <Context, 
        ClockModel, 
        ClockView, 
        ClockController,
        ClockService>
    {
        //  Initialization  -------------------------------
        public override void Initialize()
        {
            if (!IsInitialized)
            {
                _isInitialized = true;
                
                //
                _context = new Context();
                _model = new ClockModel();
                _view = new ClockView();
                _service = new ClockService();
                _controller = new ClockController(_model, _view, _service);
                
                //
                _model.Initialize(_context);
                _view.Initialize(_context);
                _service.Initialize(_context);
                _controller.Initialize(_context);
            }
        }
    }
}