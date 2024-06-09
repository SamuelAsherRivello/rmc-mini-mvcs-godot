
using RMC.Mini.Controller.Commands;

namespace RMC.Mini.Features.SceneSystem
{
    public enum LoadSceneMode
    {
        Single,
        Additive
    }
    
    /// <summary>
    /// The Command is a stand-alone object containing
    /// all arguments needed to perform a request
    /// </summary>
    public class LoadSceneRequestCommand : ICommand
    {
        //  Properties ------------------------------------
        public string SceneName { get; private set; }
        public LoadSceneMode LoadSceneMode { get; set; }


        //  Fields ----------------------------------------

        
        //  Initialization  -------------------------------
        public LoadSceneRequestCommand(string sceneName, LoadSceneMode loadSceneMode)
        {
            SceneName = sceneName;
            LoadSceneMode = loadSceneMode;
        }
    }
}