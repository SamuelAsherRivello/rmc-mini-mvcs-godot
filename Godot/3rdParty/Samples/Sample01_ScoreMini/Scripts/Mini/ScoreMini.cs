using Godot;
using RMC.Core.Architectures.Mini.Controller;
using RMC.Core.Architectures.Mini.Locators;
using RMC.Core.Architectures.Mini.Model;
using RMC.Core.Architectures.Mini.Samples.ScoreMini.Mini.View;
using RMC.Core.Architectures.Mini.Service;
using RMC.Core.Architectures.Mini.View;

namespace RMC.Core.Architectures.Mini.Samples.ScoreMini.Mini
{
    /// <summary>
    /// See <see cref="MiniMvcs{TContext,TModel,TView,TController,TService}"/>
    /// </summary>
    public class ScoreMini: MiniMvcs
            <Context, 
                IModel, 
                IView, 
                IController,
                IService>
    {
        
        //  Properties ------------------------------------
        
        
        //  Fields ----------------------------------------
        private ScoreView _scoreView;

        
        //  Initialization  -------------------------------
        public override void Initialize()
        {
            if (!IsInitialized)
            {
                GD.Print("ScoreMini.Initialized()");
                
                _isInitialized = true;
                
                _context = new Context();
                
                // Locators
                // ...ModelLocator is created in superclass
                _viewLocator = new Locator<IView>();
                _controllerLocator = new Locator<IController>();
                _serviceLocator = new Locator<IService>();
       
                // Model
                ScoreModel scoreModel = new ScoreModel();
                scoreModel.Initialize(_context); //Added to locator inside
                
                // View
                //(Passed in from main scene)
                _scoreView.Initialize(_context);
                
                // Service
                ScoreService scoreService = new ScoreService();
                ServiceLocator.AddItem(scoreService);
                scoreService.Initialize(_context);
                
                // Service
                ScoreController scoreController = new ScoreController
                (
                    scoreModel,
                    _scoreView,
                    scoreService
                );
                
                ControllerLocator.AddItem(scoreController);
                scoreController.Initialize(_context);
                
            }
        }

        
        //  Methods  -------------------------------
        public void AddView(ScoreView scoreView)
        {
            _scoreView = scoreView;
        }
    }
}