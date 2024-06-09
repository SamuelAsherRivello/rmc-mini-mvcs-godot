using Godot;
using RMC.Mini.Examples.Login.Mini.View;

namespace RMC.Mini.Examples.Login.Mini
{
    public partial class LoginMiniExample : Node
    {
        //  Properties ------------------------------------

        
        //  Fields ----------------------------------------
        [Export] 
        private LoginView _loginView;
        
        
        //  Godot Methods   -------------------------------
        
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            GD.Print($"LoginMiniExample._Ready()");
            
            LoginMini loginMini = new LoginMini(_loginView);
            loginMini.Initialize();
		    
        }

        
        /// <summary>
        /// Called every frame. 'delta' is the elapsed time since the previous frame.
        /// </summary>
        public override void _Process(double delta)
        {
        }
        
        
        //  Other Methods ---------------------------------
        
        
        //  Event Handlers --------------------------------
    }
}


