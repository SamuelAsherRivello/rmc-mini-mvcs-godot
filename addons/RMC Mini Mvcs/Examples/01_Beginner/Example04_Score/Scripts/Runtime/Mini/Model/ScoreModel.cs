using RMC.Core.Observables;
using RMC.Mini.Model;

namespace RMC.Mini.Examples.Score.Mini
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

        public bool CanResetScore
        {
            get
            {
                return _score.Value != ScoreDefaultValue;
            }
        }

        //  Fields ----------------------------------------
        private readonly Observable<bool> _isSignedIn = new Observable<bool>();
        private readonly Observable<int> _score = new Observable<int>();
        private readonly Observable<string> _title = new Observable<string>();
        public static readonly int ScoreDefaultValue = 0;
        
        //  Initialization  -------------------------------
        public override void Initialize(IContext context) 
        {
            if (!IsInitialized)
            {
                base.Initialize(context);

                // Set Defaults
                _isSignedIn.Value = false;
                _score.Value = ScoreDefaultValue;
                _title.Value = "RMC Mini Mvcs - Godot Demo";
            }
        }

        //  Methods ---------------------------------------

        //  Event Handlers --------------------------------
    }
}