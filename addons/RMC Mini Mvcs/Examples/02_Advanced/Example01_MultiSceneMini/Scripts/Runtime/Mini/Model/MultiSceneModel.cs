using RMC.Core.Observables;
using RMC.Mini.Model;

namespace RMC.Mini.Examples.MultiScene.Mini.Feature.Hud
{
    /// <summary>
    /// The Model stores runtime data 
    /// </summary>
    public class MultiSceneModel: BaseModel // Extending 'base' is optional
    {
        //  Properties ------------------------------------
        public Observable<bool> HasNavigationBack { get { return _hasNavigationBack;} }
        public Observable<bool> HasLoadedService { get { return _hasLoadedService;} }
        public Observable<string> FunMessage { get { return _funMessage;} }

        //  Fields ----------------------------------------
        private readonly Observable<bool> _hasNavigationBack = new Observable<bool>();
        private readonly Observable<bool> _hasLoadedService = new Observable<bool>();
        private readonly Observable<string> _funMessage = new Observable<string>();
        
        //  Initialization  -------------------------------
        public override void Initialize(IContext context) 
        {
            if (!IsInitialized)
            {
                base.Initialize(context);

                // Set Defaults
                _hasNavigationBack.Value = false;
                _hasLoadedService.Value = false;
                _funMessage.Value = "";
            }
        }

        
        //  Methods ---------------------------------------

        //  Event Handlers --------------------------------
 
    }
}