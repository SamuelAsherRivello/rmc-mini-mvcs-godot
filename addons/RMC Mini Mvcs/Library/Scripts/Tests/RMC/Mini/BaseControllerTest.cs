using System;
using System.Threading.Tasks;
using GdUnit4;
using static GdUnit4.Assertions;
using RMC.Mini.Controller;

namespace RMC.Mini.Tests
{
    [TestSuite]
    public partial class BaseControllerTest
    {
        
        public class SampleModel { }
        public class SampleView { }
        public class SampleService { }
    
        public class SampleController : BaseController<SampleModel, SampleView, SampleService>
        {
            public SampleController(SampleModel model, SampleView view, SampleService service) : base(model, view, service)
            {
            }

            public void PerformAction()
            {
                RequireIsInitialized();
                // Perform some action
            }
        }
        
        
        private SampleController _sampleController;
        private SampleModel _sampleModel;
        private SampleView _sampleView;
        private SampleService _sampleService;

        [Before]
        public void BeforeTestSuite()
        {
            //GD.Print("Before Test Suite");
            _sampleModel = new SampleModel();
            _sampleView = new SampleView();
            _sampleService = new SampleService();
            _sampleController = new SampleController(_sampleModel, _sampleView, _sampleService);
        }

        [BeforeTest]
        public void BeforeTest()
        {
            //GD.Print("Before Each Test");
        }

        [AfterTest]
        public void AfterTest()
        {
            //GD.Print("After Each Test");
        }

        [After]
        public void AfterTestSuite()
        {
            //GD.Print("After Test Suite");
            _sampleController.Dispose();
            _sampleController = null;
            _sampleModel = null;
            _sampleView = null;
            _sampleService = null;
        }

        [TestCase]
        public void IsInitialized_False_ByDefault()
        {
            // Arrange

            // Act
            var result = _sampleController.IsInitialized;

            // Assert
            AssertThat(result).IsFalse();
        }

        [TestCase]
        public void Initialize_SetsIsInitializedToTrue()
        {
            // Arrange
            var context = new Context();

            // Act
            _sampleController.Initialize(context);
            var result = _sampleController.IsInitialized;

            // Assert
            AssertThat(result).IsTrue();
        }

        [TestCase]
        public void RequireIsInitialized_ThrowsException_WhenNotInitialized()
        {
            // Arrange

            // Act & Assert
            AssertThrown<Exception>(new Task<Exception>(() =>
            {
                _sampleController.RequireIsInitialized();
                return null;
            }));
        }

        [TestCase]
        public void RequireIsInitialized_DoesNotThrowException_WhenInitialized()
        {
            // Arrange
            var context = new Context();
            _sampleController.Initialize(context);

            // Act & Assert - No exceptions
            _sampleController.RequireIsInitialized();
        }
    }
}
