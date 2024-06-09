using RMC.Mini.Controller;
using RMC.Mini.Examples.MultiScene.Mini.Feature.Hud;
using RMC.Mini.Features;
using RMC.Mini.Model;
using RMC.Mini.Service;
using RMC.Mini.View;

namespace RMC.Mini.Examples.MultiScene.Mini
{
    /// <summary>
    /// See <see cref="MiniMvcs{TContext,TModel,TView,TController,TService}"/>
    /// </summary>
    public class MultiSceneMini: MiniMvcs
            <Context, 
                IModel, 
                IView, 
                IController,
                IService>
    {
        
        //  Fields ----------------------------------------

        
        //  Properties ------------------------------------
        /// <summary>
        /// This is an optional addon that gives this <see cref="ConfiguratorMini"/>
        /// the support of a <see cref="IFeatureBuilder{F}"/> to easily 
        /// add and remove <see cref="IFeature"/>. 
        /// </summary>
        private FeatureBuilder FeatureBuilder { get; set; }
        
        //  Initialization  -------------------------------
        
        /// <summary>
        /// Since this constructor is called by <see cref="Mingleton"/>,
        /// we also want to initialize within
        /// </summary>
        public MultiSceneMini()
        {
            Initialize();
        }
        
        public override void Initialize()
        {
            if (!IsInitialized)
            {
                _isInitialized = true;
                
                // Context
                _context = new Context();
                
                // Model
                MultiSceneModel model = new MultiSceneModel();
         
                // Service
                MultiSceneService service = new MultiSceneService();
                ServiceLocator.AddItem(service);
                
                // Feature
                FeatureBuilder = new FeatureBuilder();
                
                // Initialize
                model.Initialize(_context); //Added to locator inside
                service.Initialize(_context);
                FeatureBuilder.Initialize(this);
                
            }
        }

        
        //  Methods  -------------------------------
        public bool HasFeature<TFeature>(string key = "") where TFeature : IFeature
        {
            RequireIsInitialized();
            
            return FeatureBuilder.HasFeature<TFeature>(key);
        }
        
        public void AddFeature<TFeature>(TFeature feature, string key = "") where TFeature : IFeature
        {
            RequireIsInitialized();

            FeatureBuilder.AddFeature<TFeature>(feature, key);
        }
        
        public void RemoveFeature<TFeature>(string key = "") where TFeature : IFeature
        {
            RequireIsInitialized();

            FeatureBuilder.RemoveFeature<TFeature>(key);
        }
        
    }
}