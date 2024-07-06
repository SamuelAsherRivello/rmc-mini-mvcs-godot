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
        public class SampleSceneSystemMvcs : MiniMvcs<
            IContext, 
            SceneSystemModel, 
            
            //No view needed
            DummyView, 
            
            SceneSystemController, 
            
            //No service needed
            DummyService>
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
                    ControllerLocator.AddItem(controller);
                    
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
        public void ModelLocator_IsNull_BeforeInitialize()
        {
            // Arrange

            // Act

            // Assert
            AssertObject(_mvcs).IsNotNull();
            AssertObject(_mvcs.ModelLocator).IsNull();
        }

        [TestCase]
        public void ContextModelLocator_IsNotNull_AfterInitialize()
        {
            // Arrange
            _mvcs.Initialize();

            // Act

            // Assert
            AssertObject(_mvcs.Context.ModelLocator).IsNotNull();
        }
        
        
        [TestCase]
        public void ModelLocator_IsNotNull_AfterInitialize()
        {
            // Arrange
            _mvcs.Initialize();

            // Act

            // Assert
            AssertObject(_mvcs.ModelLocator).IsNotNull();
        }

        [TestCase]
        public void ModelLocator_HasItem_ReturnsTrue()
        {
            // Arrange
            _mvcs.Initialize();

            // Act
            AssertObject(_mvcs.ModelLocator).IsNotNull();
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
            AssertObject(_mvcs.ControllerLocator).IsNotNull();
            bool hasItem = _mvcs.ControllerLocator.HasItem<SceneSystemController>();

            // Assert
            AssertBool(hasItem).IsTrue();
        }
    }
}
