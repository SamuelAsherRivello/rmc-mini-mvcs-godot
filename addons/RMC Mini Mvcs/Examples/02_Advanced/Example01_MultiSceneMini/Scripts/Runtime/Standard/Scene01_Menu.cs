using Godot;
using RMC.Mingletons;
using RMC.Mini.Examples.MultiScene.Mini;
using RMC.Mini.Examples.MultiScene.Mini.Feature.Hud;
using RMC.Mini.Examples.MultiScene.Mini.Feature.Menu;
using RMC.Mini.Features.SceneSystem;

namespace RMC.Mini.Examples.MultiScene.Standard
{
    /// <summary>
    /// This is the main entry point to one of the scenes
    /// </summary>
    public partial class Scene01_Menu : Node
    {
        //  Fields ----------------------------------------
        [Export]
        private MenuView _menuView;
        
        [Export]
        private HudView _hudView;
        
        //  Godot Methods ---------------------------------
		
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            GD.Print($"Scene01_Menu._Ready()");
            AddFeature();
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
            RemoveFeature();
            // Optional: Handle any cleanup here...
        }

        //  Methods ---------------------------------------
        private void AddFeature()
        {

            MultiSceneMini mini = Mingleton.Instance.GetOrCreateAsClass<MultiSceneMini>();
            
            //  Scene-Specific ----------------------------
            MenuFeature menuFeature = new MenuFeature();
            menuFeature.AddView(_menuView);
            mini.AddFeature<MenuFeature>(menuFeature);
            
            //  Scene-Agnostic (Temporary) -----------------
            HudFeature hudFeature = new HudFeature();
            hudFeature.AddView(_hudView);
            mini.AddFeature<HudFeature>(hudFeature);

            //  Scene-Agnostic (Permanent) -----------------
            if (!mini.HasFeature<SceneSystemFeature>())
            {
                SceneSystemFeature sceneSystemFeature = new SceneSystemFeature();
                mini.AddFeature<SceneSystemFeature>(sceneSystemFeature);
            }
        }
        
        private void RemoveFeature()
        {
            MultiSceneMini mini = Mingleton.Instance.GetOrCreateAsClass<MultiSceneMini>();
            
            //  Scene-Specific ----------------------------
            mini.RemoveFeature<MenuFeature>();
            
            //  Scene-Agnostic ----------------------------
            mini.RemoveFeature<HudFeature>();
        }
        
        //  Event Handlers --------------------------------
    }
}