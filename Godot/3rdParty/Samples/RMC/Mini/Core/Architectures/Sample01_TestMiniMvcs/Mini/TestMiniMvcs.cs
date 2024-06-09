using RMC.Core.Architectures.Mini.Controller;
using RMC.Core.Architectures.Mini.Locators;
using RMC.Core.Architectures.Mini.Model;
using RMC.Core.Architectures.Mini.Samples.UGS.Mini.Controller;
using RMC.Core.Architectures.Mini.Samples.UGS.Mini.Model;
using RMC.Core.Architectures.Mini.Samples.UGS.Mini.Service;
using RMC.Core.Architectures.Mini.Samples.UGS.Mini.View;
using RMC.Core.Architectures.Mini.Service;
using RMC.Core.Architectures.Mini.View;

namespace RMC.Core.Architectures.Mini.Samples.UGS.Mini
{
    /// <summary>
    /// See <see cref="MiniMvcs{TContext,TModel,TView,TController,TService}"/>
    /// </summary>
    public class TestMiniMvcs: MiniMvcs
            <Context, 
                IModel, 
                IView, 
                IController,
                IService>
    {
        
        //  Fields ----------------------------------------

        
        //  Properties ------------------------------------
        
        
        //  Initialization  -------------------------------
        public override async void Initialize()
        {
            if (!IsInitialized)
            {
                _isInitialized = true;
                
                _context = new Context();
                
                // Locators
                // ...ModelLocator is created in superclass
                _viewLocator = new Locator<IView>();
                _controllerLocator = new Locator<IController>();
                _serviceLocator = new Locator<IService>();
       
                // Model
                TestModel testModel = new TestModel();
                testModel.Initialize(_context); //Added to locator inside
                
                // View
                TestView testView = new TestView();
                ViewLocator.AddItem(testView);
                testView.Initialize(_context);
                
                // Service
                TestService testService = new TestService();
                ServiceLocator.AddItem(testService);
                testService.Initialize(_context);
                
                // Service
                TestController testController = new TestController
                (
                    testModel,
                    testView,
                    testService
                );
                
                ControllerLocator.AddItem(testController);
                testController.Initialize(_context);

                // Do stuff....
                testView.OnUserAction.Invoke(false, true);
                
            }
        }

        
        //  Methods  -------------------------------
    }
}