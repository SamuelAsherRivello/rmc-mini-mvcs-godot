using System;
using Godot;
using RMC.Core.Events;
using RMC.Mini.View;


namespace RMC.Mini.Examples.Score.Mini.View
{
	/// <summary>
	/// The View handles user interface and user input
	///
	/// Relates to the <see cref="ScoreController"/>
	/// 
	/// </summary>
	public partial class ScoreView : Control, IView
	{
		//  Events ----------------------------------------
		public RmcEvent OnAddPoints = new RmcEvent();
		public RmcEvent OnResetPoints = new RmcEvent();
        
		//  Properties ------------------------------------
		public bool IsInitialized { get { return _isInitialized;} }
		public IContext Context { get { return _context;} }
        
		//  Fields ----------------------------------------
		private bool _isInitialized = false;
		private IContext _context;
		
		[Export] 
		private Label _titleLabel;
		
		[Export] 
		private Label _statusLabel;
		
		[Export] 
		private Button _addScoreButton;
		
		[Export] 
		private Button _resetScoreButton;
        
		//  Initialization  -------------------------------
		public void Initialize(IContext context)
		{
			if (!IsInitialized)
			{
				_isInitialized = true;
				_context = context;

				_addScoreButton.Pressed += AddScoreButtonOnPressed;
				_resetScoreButton.Pressed += ResetScoreButtonOnPressed;
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
		}

		
		//  Methods ---------------------------------------
		public void RefreshUI()
		{
			ScoreModel scoreModel = Context.ModelLocator.GetItem<ScoreModel>();
			_titleLabel.Text = $"{scoreModel.Title.Value}";
			_addScoreButton.Disabled = !scoreModel.IsSignedIn.Value;
			_resetScoreButton.Disabled = !scoreModel.IsSignedIn.Value || !scoreModel.CanResetScore;
			
			if (!scoreModel.IsSignedIn.Value)
			{
				_statusLabel.Text = $"Loading...";
			}
			else
			{
				_statusLabel.Text = $"Score = {scoreModel.Score.Value}";
			}
		}
        
        
		//  Event Handlers --------------------------------
		private void AddScoreButtonOnPressed()
		{
			OnAddPoints.Invoke();
		}
		
		private void ResetScoreButtonOnPressed()
		{
			OnResetPoints.Invoke();
		}
	}
}