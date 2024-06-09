using System;
using Godot;
using RMC.Core.Events;
using RMC.Mini.Examples.MultiScene.Mini.Feature.Hud;
using RMC.Mini.View;

namespace RMC.Mini.Examples.MultiScene.Mini.Feature.Menu
{
    /// <summary>
    /// The View handles user interface and user input
    ///
    /// Relates to the <see cref="MenuController"/>
    /// 
    /// </summary>
    public partial class MenuView: Control, IView
    {
        //  Events ----------------------------------------
        public readonly RmcEvent OnPlayGame = new RmcEvent();
        
        //  Properties ------------------------------------
        public bool IsInitialized { get { return _isInitialized;} }
        public IContext Context { get { return _context;} }
        public Button PlayGameButton { get { return _playGameButton; }}
        
        //  Fields ----------------------------------------
        private bool _isInitialized = false;
        private IContext _context;

        [Export] 
        private Button _playGameButton;

        //  Initialization  -------------------------------
        public void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                _isInitialized = true;
                _context = context;
                
                PlayGameButton.Pressed += PlayGameButton_OnPressed;
                
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
        
        
        
        //  Godot Methods ---------------------------------
		
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
        }

        
        /// <summary>
        /// Called every frame. 'delta' is the elapsed time since the previous frame.
        /// </summary>
        public override void _Process(double delta)
        {
        }
		
		
        /// <summary>
        /// Called when the node is about to leave the SceneTree
        /// </summary>
        public override void _ExitTree()  
        {
            RequireIsInitialized();
            
            // Optional: Handle any cleanup here...
        }


        //  Methods ---------------------------------------
        public void RefreshUI()
        {
            RequireIsInitialized();
            
            MultiSceneModel multiSceneModel = Context.ModelLocator.GetItem<MultiSceneModel>();
            _playGameButton.Disabled = !multiSceneModel.HasLoadedService.Value;
        }
        
        
        //  Event Handlers --------------------------------
        private void PlayGameButton_OnPressed()
        {
            OnPlayGame.Invoke();
        }
    }
}