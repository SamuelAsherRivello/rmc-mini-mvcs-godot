using System;
using Godot;
using RMC.Core.Architectures.Mini.View;
using RMC.Core.Data.Types;

namespace RMC.Core.Architectures.Mini.Samples.ScoreMini.Mini.View
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
		public SuperEvent<bool,bool> OnAddPoints = new SuperEvent<bool,bool>(new Observable<bool>());

        
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
        
		//  Initialization  -------------------------------
		public void Initialize(IContext context)
		{
			if (!IsInitialized)
			{
				_isInitialized = true;
				_context = context;

				_addScoreButton.Pressed += AddScoreButtonOnPressed;

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
		
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			
		}

		//  Methods ---------------------------------------
		public void RefreshUI()
		{
			ScoreModel scoreModel = Context.ModelLocator.GetItem<ScoreModel>();
			_titleLabel.Text = $"{scoreModel.Title.Value}";
			_addScoreButton.Disabled = !scoreModel.IsSignedIn.Value;

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
			OnAddPoints.Invoke(false, true);
		}
	}
}