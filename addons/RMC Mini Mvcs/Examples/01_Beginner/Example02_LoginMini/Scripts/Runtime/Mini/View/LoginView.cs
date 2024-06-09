using System;
using Godot;
using RMC.Core.Events;
using RMC.Mini.Examples.Login.Mini.Controller.Commands;
using RMC.Mini.Examples.Login.Mini.Model;
using RMC.Mini.View;

namespace RMC.Mini.Examples.Login.Mini.View
{

    //  Class Attributes ----------------------------------
    public class UserDataEvent : RmcEvent<UserData> {}
    public class BoolEvent : RmcEvent<bool> {}
    
    /// <summary>
    /// The View handles user interface and user input
    /// </summary>
    public partial class LoginView: Control, IView
    {
        //  Events ----------------------------------------
        public readonly UserDataEvent OnLogin = new UserDataEvent();
        public readonly BoolEvent OnCanLoginChanged = new BoolEvent();
        public readonly RmcEvent OnLogout = new RmcEvent();
        
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
        private LineEdit _usernameLineEdit;

        [Export]
        private LineEdit _passwordLineEdit;

        [Export]
        private Button _loginButton;
        
        [Export]
        private Button _logoutButton;

        //  Initialization  -------------------------------
        public void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                _isInitialized = true;
                _context = context;
                
                //
                _loginButton.Pressed += LoginButton_OnPressed;
                _logoutButton.Pressed += LogoutButton_OnPressed;
                _usernameLineEdit.TextChanged += AnyInputField_OnTextChanged;
                _passwordLineEdit.TextChanged += AnyInputField_OnTextChanged;
                AnyInputField_OnTextChanged("");
                    
                //
                Context.CommandManager.AddCommandListener<LogoutCommand>(
                    OnLogoutCommand);
                Context.CommandManager.AddCommandListener<LoginSubmittedCommand>(
                    OnLoginSubmittedCommand);
                Context.CommandManager.AddCommandListener<LoginCompletedCommand>(
                    OnLoginCompletedCommand);

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
            Context?.CommandManager?.RemoveCommandListener<LogoutCommand>(
                OnLogoutCommand);
            Context?.CommandManager?.RemoveCommandListener<LoginSubmittedCommand>(
                OnLoginSubmittedCommand);
            Context?.CommandManager?.RemoveCommandListener<LoginCompletedCommand>(
                OnLoginCompletedCommand);
        }

        
        //  Methods ---------------------------------------
        private void AnyInputField_OnTextChanged(string _)
        {
            bool isValidInput = _usernameLineEdit.Text.Length > 0 && 
                                _passwordLineEdit.Text.Length > 0;

            _loginButton.Disabled = !isValidInput;
            OnCanLoginChanged.Invoke(!_loginButton.Disabled);
        }
        
        
        //  Event Handlers --------------------------------
        
        private void LoginButton_OnPressed()
        {
            OnLogin.Invoke(new UserData(_usernameLineEdit.Text, _passwordLineEdit.Text));
        }
        
        
        private void LogoutButton_OnPressed()
        {
            OnLogout.Invoke();
        }

        private void OnLogoutCommand(LogoutCommand logoutCommand)
        {
            RequireIsInitialized();

            if (_loginButton != null)
            {
                _loginButton.Disabled = true;
                _loginButton.Text = "Login";
            }

            if (_logoutButton != null)
            {
                _logoutButton.Disabled = true;
                _logoutButton.Text = "Logout";
            }
            
            if (_usernameLineEdit != null) _usernameLineEdit.Editable = true;
            if (_passwordLineEdit != null) _passwordLineEdit.Editable = true;
            if (_usernameLineEdit != null) _usernameLineEdit.Text = "";
            if (_passwordLineEdit != null) _passwordLineEdit.Text = "";
            if (_titleLabel != null) _titleLabel.Text = "Login, Mini Example";
            if (_statusLabel != null) _statusLabel.Text = "Enter Username & Password (Hint: 'pass1234') ...";

        }
        
        private void OnLoginSubmittedCommand(LoginSubmittedCommand loginSubmittedCommand)
        {
            RequireIsInitialized();

            _loginButton.Disabled = true;
            _logoutButton.Disabled = true;
            _usernameLineEdit.Editable = false;
            _passwordLineEdit.Editable = false;
            
            _statusLabel.Text = $"Submitting password of {loginSubmittedCommand.UserData.Password} ...";
        }
        
        private void OnLoginCompletedCommand(LoginCompletedCommand loginCompletedCommand)
        {
            RequireIsInitialized();

            string result = "";
            if (loginCompletedCommand.WasSuccess)
            {
                result = "Success";
            }
            else
            {
                result = "Failed";
                _logoutButton.Text = "Clear";
            }
            
            _loginButton.Disabled = true;
            _logoutButton.Disabled = false;
            _usernameLineEdit.Editable = false;
            _passwordLineEdit.Editable = false;
            _statusLabel.Text = $"{result} for password of {loginCompletedCommand.UserData.Password}!";
            
            //NOTE...
            if (loginCompletedCommand.WasSuccess)
            {
                //The view used only Commands to observe state above (good).
                //  Another option is to reference the model per below (bad),
                //  or observe the model events (good)
                LoginModel loginModel = Context.ModelLocator.GetItem<LoginModel>();
                //GD.Print($"View Sees Model with {loginModel.LoggedInUserData.Value}");
            }
        }
    }
}