using RMC.Core.Architectures.Mini.Model;
using RMC.Core.Data.Types;

namespace RMC.Core.Architectures.Mini.Samples.ScoreMini.Mini
{
    /// <summary>
    /// The Model stores runtime data 
    /// </summary>
    public partial class ScoreModel: BaseModel // Extending 'base' is optional
    {
        //  Properties -----------------------------------------
        public Observable<bool> IsSignedIn { get { return _isSignedIn;} }
        public Observable<string> Title { get { return _title;} }
        public Observable<int> Score { get { return _score;} }
        
        //  Fields ----------------------------------------
        private readonly Observable<bool> _isSignedIn = new Observable<bool>();
        private readonly Observable<int> _score = new Observable<int>();
        private readonly Observable<string> _title = new Observable<string>();
        
        //  Initialization  -------------------------------
        public override void Initialize(IContext context) 
        {
            if (!IsInitialized)
            {
                base.Initialize(context);

                // Set Defaults
                _isSignedIn.Value = false;
                _score.Value = 0;
                _title.Value = "RMC Mini Mvcs - Godot Demo";
            }
        }

        //  Methods ---------------------------------------

        //  Event Handlers --------------------------------
    }
}