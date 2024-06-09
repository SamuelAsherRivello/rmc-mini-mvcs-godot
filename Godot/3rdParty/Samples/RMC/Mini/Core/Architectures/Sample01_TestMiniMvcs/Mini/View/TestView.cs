using System;
using RMC.Core.Architectures.Mini.Samples.UGS.Mini.Controller;
using RMC.Core.Architectures.Mini.View;
using RMC.Core.Data.Types;

namespace RMC.Core.Architectures.Mini.Samples.UGS.Mini.View
{
    /// <summary>
    /// The View handles user interface and user input
    ///
    /// Relates to the <see cref="TestController"/>
    /// 
    /// </summary>
    public class TestView: IView
    {
        //  Events ----------------------------------------
        public SuperEvent<bool,bool> OnUserAction = new SuperEvent<bool,bool>(new Observable<bool>());

        
        //  Properties ------------------------------------
        public bool IsInitialized { get { return _isInitialized;} }
        public IContext Context { get { return _context;} }
        
        //  Fields ----------------------------------------
        private bool _isInitialized = false;
        private IContext _context;

        
        //  Initialization  -------------------------------
        public void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                _isInitialized = true;
                _context = context;

                RefreshUI();
                
            }
        }

        public void RequireIsInitialized()
        {
            if (!IsInitialized)
            {
                throw new Exception("MustBeInitialized");
            }
        }
        
        
        //  Unity Methods ---------------------------------
        protected void OnDestroy()
        {
            // Optional: Handle any cleanup here...
        }

        //  Methods ---------------------------------------
        public void RefreshUI()
        {

        }
        
        
        //  Event Handlers --------------------------------
    }
}