using RMC.Core.Architectures.Mini.Model;
using RMC.Core.Data.Types;

namespace RMC.Core.Architectures.Mini.Samples.UGS.Mini.Model
{
    /// <summary>
    /// The Model stores runtime data 
    /// </summary>
    public partial class TestModel: BaseModel // Extending 'base' is optional
    {
        //  Properties -----------------------------------------
        public Observable<bool> IsSignedIn { get { return _isSignedIn;} }

        //  Fields ----------------------------------------
        private readonly Observable<bool> _isSignedIn = new Observable<bool>();
        
        //  Initialization  -------------------------------
        public override void Initialize(IContext context) 
        {
            if (!IsInitialized)
            {
                base.Initialize(context);

                // Set Defaults
                _isSignedIn.Value = false;
            }
        }

        //  Methods ---------------------------------------

        //  Event Handlers --------------------------------
    }
}