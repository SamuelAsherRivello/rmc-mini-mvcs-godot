using System.Threading.Tasks;
using Godot;
using RMC.Core.Architectures.Mini.Service;
using RMC.Core.Data.Types;

namespace RMC.Core.Architectures.Mini.Samples.UGS.Mini.Service
{
    //  Namespace Properties ------------------------------
    
    /// <summary>
    /// The Service handles external data 
    /// </summary>
    public class TestService : BaseService  // Extending 'base' is optional
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
            
            //TODO: load something
            
            GD.Print("loading");
            OnSignInComplete.Invoke(false, true);
        }
        //  Methods ---------------------------------------
        
        //  Event Handlers --------------------------------
    }
}