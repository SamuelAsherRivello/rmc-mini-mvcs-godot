using System.Threading.Tasks;
using Godot;
using RMC.Core.Events;
using RMC.Core.Utilities;
using RMC.Mini.Service;

namespace RMC.Mini.Examples.Score.Mini
{
    //  Namespace Properties ------------------------------
    public class IntEvent : RmcEvent<int> {}
    
    /// <summary>
    /// The Service handles external data 
    /// </summary>
    public class ScoreService : BaseService  // Extending 'base' is optional
    {
        //  Events ----------------------------------------
        public readonly IntEvent OnInt = new IntEvent();
        
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

            _ = LoadAsync();
        }
        
        private async Task LoadAsync()
        {
            RequireIsInitialized();

            string fileName = "ScoreMiniText.txt";
            
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
            int scoreInitial = int.Parse(fileContents);
            OnInt.Invoke(scoreInitial);
        }
        
     
        
        //  Event Handlers --------------------------------
    }
}