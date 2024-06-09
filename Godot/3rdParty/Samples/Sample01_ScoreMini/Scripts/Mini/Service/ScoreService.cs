using System.Threading.Tasks;
using Godot;
using RMC.Core.Architectures.Mini.Service;
using RMC.Core.Data.Types;

namespace RMC.Core.Architectures.Mini.Samples.ScoreMini.Mini
{
    //  Namespace Properties ------------------------------
    
    /// <summary>
    /// The Service handles external data 
    /// </summary>
    public class ScoreService : BaseService  // Extending 'base' is optional
    {
        //  Events ----------------------------------------
        public SuperEvent<bool,bool> OnSignInComplete = new SuperEvent<bool,bool>(new Observable<bool>());
        
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

        public async Task SignInAsync ()
        {
            RequireIsInitialized();

            //Simulate calling the server
            await Task.Delay(1000);
            
            OnSignInComplete.Invoke(false, true);
        }
        
        //  Methods ---------------------------------------
        
        //  Event Handlers --------------------------------
    }
}