using GdUnit4;
using Godot;
using static GdUnit4.Assertions;
using RMC.Mini.Features.SceneSystem;
using RMC.Mini.Service;
using RMC.Mini.View;

namespace RMC.Mini.Features
{
    /// <summary>
    /// Test suite for the SceneSystemMvcs class
    /// </summary>
    [TestSuite]
    public partial class SceneSystemMvcsTest
    {
        /// <summary>
        /// The MiniMvcs subclass using SceneSystemModel,
        /// DummyView, SceneSystemController, and DummyService
        /// </summary>
        public class SampleSceneSystemMvcs : MiniMvcs<IContext, SceneSystemModel, DummyView, SceneSystemController, DummyService>
        {
            public override void Initialize()
            {
                if (!_isInitialized)
                {
                    // Context
                    _context = new Context();
                
                    // Model
                    SceneSystemModel model = new SceneSystemModel();
         
                    // Service
                
                    // Controller
                    SceneSystemController controller = new SceneSystemController(model, new DummyView(), new DummyService());
                
                    // Initialize
                    model.Initialize(_context); //Added to locator inside
                    controller.Initialize(_context);
                    _isInitialized = true;
                }
            }
        }

        private SampleSceneSystemMvcs _mvcs;
        
        [Before]
        public void BeforeTestSuite()
        {
            // GD.Print("BeforeTestSuite");
        }

        [BeforeTest]
        public void BeforeTest()
        {
            _mvcs = new SampleSceneSystemMvcs();
        }

        [AfterTest]
        public void AfterTest()
        {
            // GD.Print("AfterTest");
        }

        [After]
        public void AfterTestSuite()
        {
            // GD.Print("AfterTestSuite");
        }

        [TestCase]
        public void Initialize_IsInitialized_True()
        {
            // Arrange & Act
            _mvcs.Initialize();

            // Assert
            AssertBool(_mvcs.IsInitialized).IsTrue();
        }

        [TestCase]
        public void ModelLocator_HasItem_ReturnsTrue()
        {
            // Arrange
            _mvcs.Initialize();

            // Act
            GD.Print(("what1: " + _mvcs));
            GD.Print(("what2: " + _mvcs.ModelLocator));
            bool hasItem = _mvcs.ModelLocator.HasItem<SceneSystemModel>();

            // Assert
            AssertBool(hasItem).IsTrue();
        }

        [TestCase]
        public void ControllerLocator_HasItem_ReturnsTrue()
        {
            // Arrange
            _mvcs.Initialize();

            // Act
            bool hasItem = _mvcs.ControllerLocator.HasItem<SceneSystemController>();

            // Assert
            AssertBool(hasItem).IsTrue();
        }
    }
}
