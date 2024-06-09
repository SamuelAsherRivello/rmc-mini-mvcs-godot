using GdUnit4;
using RMC.Mini.Service;
using static GdUnit4.Assertions;

namespace RMC.Mini.Mvcs.Tests
{
    [TestSuite]
    public partial class BaseServiceTest
    {
        public class SampleService : BaseService
        {
        }
        
        private SampleService _service;

        [Before]
        public void BeforeTestSuite()
        {
            //GD.Print("Before Test Suite");
            _service = new SampleService();
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
            _service = null;
        }

        [TestCase]
        public void IsInitialized_False_ByDefault()
        {
            // Arrange

            // Act
            var result = _service.IsInitialized;
            
            // Assert
            AssertThat(result).IsFalse();
        }

        [TestCase]
        public void Initialize_SetsIsInitializedToTrue()
        {
            // Arrange
            Context context = new Context();

            // Act
            _service.Initialize(context);
            var result = _service.IsInitialized;

            // Assert
            AssertThat(result).IsTrue();
        }
    }
}