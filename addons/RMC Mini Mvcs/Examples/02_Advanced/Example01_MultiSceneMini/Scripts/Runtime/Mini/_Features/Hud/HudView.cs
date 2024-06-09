using System;
using Godot;
using RMC.Core.Events;
using RMC.Mini.View;

namespace RMC.Mini.Examples.MultiScene.Mini.Feature.Hud
{
    /// <summary>
    /// The View handles user interface and user input
    ///
    /// Relates to the <see cref="HudController"/>
    /// 
    /// </summary>
    public partial class HudView: Control, IView
    {
        //  Events ----------------------------------------
        public readonly RmcEvent OnBack = new RmcEvent();
        
        //  Properties ------------------------------------
        public bool IsInitialized { get { return _isInitialized;} }
        public IContext Context { get { return _context;} }
        public Label TitleLabel { get { return _titleLabel; }}
        public Button BackButton { get { return _backButton; }}
        
        //  Fields ----------------------------------------
        private bool _isInitialized = false;
        private IContext _context;

        [Export] 
        private Label _titleLabel;
        
        [Export] 
        private Button _backButton;
        
        //  Initialization  -------------------------------
        public void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                _isInitialized = true;
                _context = context;
                BackButton.Pressed += BackButton_OnPressed;
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
            // Optional: Handle any cleanup here...
            BackButton.Pressed -= BackButton_OnPressed;
        }
        
        //  Methods ---------------------------------------
        public void RefreshUI()
        {
            RequireIsInitialized();
            MultiSceneModel model = Context.ModelLocator.GetItem<MultiSceneModel>();
            TitleLabel.Text = GetTree().CurrentScene.Name;
            BackButton.Disabled = (!model.HasNavigationBack.Value);
        }
        
        
        //  Event Handlers --------------------------------
        private void BackButton_OnPressed()
        {
            RequireIsInitialized();
            OnBack.Invoke();
        }

        private void ServiceHasLoaded_OnValueChanged(bool previousValue, bool currentValue)
        {
            RequireIsInitialized();
            RefreshUI();
        }
        private void HasNavigationBack_OnValueChanged(bool previousValue, bool currentValue)
        {
            RequireIsInitialized();
            RefreshUI();
        }
        
    }
}