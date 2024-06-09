using System;
using System.Threading.Tasks;
using GdUnit4;
using static GdUnit4.Assertions;
using RMC.Mini.View;

namespace RMC.Mini.Tests
{

    [TestSuite]
    public partial class BaseViewTest
    {
        
        public class SampleView : BaseView
        {
            public string State { get; private set; }

            public void ChangeState(string newState)
            {
                RequireIsInitialized();
                State = newState;
            }
        }
        
        
        private SampleView _sampleView;

        [Before]
        public void BeforeTestSuite()
        {
            //GD.Print("Before Test Suite");
            _sampleView = new SampleView();
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
            _sampleView = null;
        }

        [TestCase]
        public void IsInitialized_False_ByDefault()
        {
            // Arrange

            // Act
            var result = _sampleView.IsInitialized;

            // Assert
            AssertThat(result).IsFalse();
        }

        [TestCase]
        public void Initialize_SetsIsInitializedToTrue()
        {
            // Arrange
            var context = new Context();

            // Act
            _sampleView.Initialize(context);
            var result = _sampleView.IsInitialized;

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
                _sampleView.RequireIsInitialized();
                return null;
            }));
        }

        [TestCase]
        public void RequireIsInitialized_DoesNotThrowException_WhenInitialized()
        {
            // Arrange
            var context = new Context();
            _sampleView.Initialize(context);

            // Act & Assert
            _sampleView.RequireIsInitialized();
        }

        [TestCase]
        public void ChangeState_UpdatesState_WhenInitialized()
        {
            // Arrange
            var context = new Context();
            _sampleView.Initialize(context);
            var newState = "NewState";

            // Act
            _sampleView.ChangeState(newState);
            var result = _sampleView.State;

            // Assert
            AssertThat(result == newState).IsTrue();
        }
    }
}
