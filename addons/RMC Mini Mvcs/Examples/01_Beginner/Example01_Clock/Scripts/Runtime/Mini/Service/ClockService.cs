using Godot;
using System.Threading.Tasks;
using RMC.Core.Events;
using RMC.Core.Utilities;
using RMC.Mini.Service;

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
namespace RMC.Mini.Examples.Clock.Mini.Service
{
    //  Namespace Properties ------------------------------
    public class IntEvent : RmcEvent<int> {}

    /// <summary>
    /// The Service handles external data 
    /// </summary>
    public class ClockService : BaseService  // Extending 'base' is optional
    {
        //  Events ----------------------------------------
        public readonly IntEvent OnLoaded = new IntEvent();

        
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
        public void Load ()
        {
            RequireIsInitialized();

            LoadAsync();
        }

        
        private async Task LoadAsync()
        {
            RequireIsInitialized();

            string fileName = "ClockMiniText.txt";
            
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

            // Return strongly typed data
            int timeDelayInSeconds = int.Parse(fileContents);
            OnLoaded.Invoke(timeDelayInSeconds);
        }


        
        //  Event Handlers --------------------------------
    }
}