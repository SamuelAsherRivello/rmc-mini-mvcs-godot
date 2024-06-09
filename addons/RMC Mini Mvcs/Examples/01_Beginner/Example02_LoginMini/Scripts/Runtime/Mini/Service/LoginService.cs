using System.Threading.Tasks;
using Godot;
using RMC.Core.Events;
using RMC.Core.Utilities;
using RMC.Mini.Examples.Login.Mini.Model;
using RMC.Mini.Service;

namespace RMC.Mini.Examples.Login.Mini.Service
{
    //  Namespace Properties ------------------------------
    public class OnLoginCompletedEvent : RmcEvent<UserData, bool> {}

    //  Class Attributes ----------------------------------

    /// <summary>
    /// The Service handles external data 
    /// </summary>
    public class LoginService : BaseService  // Extending 'base' is optional
    {
        //  Events ----------------------------------------
        public readonly OnLoginCompletedEvent OnLoginCompletedCompleted = new OnLoginCompletedEvent();
        
        //  Properties ------------------------------------
        
        //  Fields ----------------------------------------

        //  Initialization  -------------------------------
        public override void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);
            }
        }

        //  Methods ---------------------------------------
        public void Login (UserData userData)
        {
            RequireIsInitialized();
            
            _ = LoginAsync(userData);
        }
        
        private async Task LoginAsync(UserData userData)
        {
            RequireIsInitialized();

            string fileName = "LoginMiniText.txt";
            
            // Find file contents
            string filePath = FileAccessUtility.FindFileOnceInResources(fileName);
            if (string.IsNullOrEmpty(filePath))
            {
                GD.PrintErr($"LoadAsync() failed. Cannot find '{fileName}'.");
            }
            
            var file = FileAccess.Open(filePath, FileAccess.ModeFlags.Read);
            string fileContents = file.GetAsText();
            file.Close();

            // Simulate network latency
            await Task.Delay(500);

            string correctPassword = fileContents.Trim();
            bool wasSuccess = string.Equals(userData.Password, correctPassword);
            
            // Return strongly typed data
            OnLoginCompletedCompleted.Invoke(userData, wasSuccess);
        }
        
        //  Event Handlers --------------------------------
    }
}