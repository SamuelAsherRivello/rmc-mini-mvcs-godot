using RMC.Mini.Model;

namespace RMC.Mini.Features.SceneSystem
{
    //  Class Attributes ----------------------------------

    /// <summary>
    /// The Model stores runtime data 
    /// </summary>
    public class SceneSystemModel: BaseModel // Extending 'base' is optional
    {
        //  Properties ------------------------------------
        // public Observable<string> SceneName { get { return _sceneName;} }         // No data needed yet....
        
        //  Fields ----------------------------------------
        // private readonly Observable<string> _sceneName = new Observable<string>();
        
        //  Initialization  -------------------------------
        public override void Initialize(IContext context) 
        {
            if (!IsInitialized)
            {
                base.Initialize(context);

                // Set Defaults
                // _sceneName.Value = "";
            }
        }

        
        //  Methods ---------------------------------------
 

        //  Event Handlers --------------------------------
    }
}