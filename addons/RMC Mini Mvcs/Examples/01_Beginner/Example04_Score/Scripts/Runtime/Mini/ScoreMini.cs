using Godot;
using RMC.Mini.Controller;
using RMC.Mini.Examples.Score.Mini.View;
using RMC.Mini.Model;
using RMC.Mini.Service;
using RMC.Mini.View;

namespace RMC.Mini.Examples.Score.Mini
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
        public ScoreMini (ScoreView scoreView)
        {
            _scoreView = scoreView;
        }
        
        public override void Initialize()
        {
            if (!IsInitialized)
            {
                GD.Print("ScoreMini.Initialized()");
                
                _isInitialized = true;
                
                // Context
                _context = new Context();
                
                // Model
                ScoreModel scoreModel = new ScoreModel();

                // Service
                ScoreService scoreService = new ScoreService();
                ServiceLocator.AddItem(scoreService);
    
                // Service
                ScoreController scoreController = new ScoreController
                (
                    scoreModel,
                    _scoreView,
                    scoreService
                );
                
                // Controller
                ControllerLocator.AddItem(scoreController);
                
                // Initialize
                scoreModel.Initialize(_context); //Added to locator inside
                _scoreView.Initialize(_context);
                scoreService.Initialize(_context);
                scoreController.Initialize(_context);
                
            }
        }

        
        //  Methods  -------------------------------

    }
}