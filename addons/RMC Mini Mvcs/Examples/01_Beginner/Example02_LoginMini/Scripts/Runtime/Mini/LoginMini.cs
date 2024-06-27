using RMC.Mini.Examples.Login.Mini.Controller;
using RMC.Mini.Examples.Login.Mini.Model;
using RMC.Mini.Examples.Login.Mini.Service;
using RMC.Mini.Examples.Login.Mini.View;

namespace RMC.Mini.Examples.Login.Mini
{
    /// <summary>
    /// See <see cref="SimpleMiniMvcs{TContext,TModel,TView,TController,TService}"/>"/>
    /// </summary>
    public class LoginMini: SimpleMiniMvcs
            <Context, 
            LoginModel, 
            LoginView, 
            LoginController,
            LoginService>
    {

        //  Initialization  -------------------------------
        public LoginMini(LoginView view)
        {
            _view = view;
        }
        
        public override void Initialize()
        {
            if (!IsInitialized)
            {
                _isInitialized = true;
                
                //
                _context = new Context();
                _model = new LoginModel();
                _service = new LoginService();
                _controller = new LoginController(_model, _view, _service);
                
                //
                _model.Initialize(_context);
                _view.Initialize(_context);
                _service.Initialize(_context);
                _controller.Initialize(_context);
            }
        }
    }
}