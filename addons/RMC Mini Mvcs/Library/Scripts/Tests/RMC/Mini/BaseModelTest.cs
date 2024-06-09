using GdUnit4;
using RMC.Core.Observables;
using RMC.Mini.Model;
using static GdUnit4.Assertions;

namespace RMC.Mini.Mvcs.Tests
{

    [TestSuite]
    public partial class BaseModelTest
    {
        public class SampleModel : BaseModel
        {
            public Observable<int> SampleObservable { get; private set; }

            public SampleModel()
            {
                SampleObservable = new Observable<int>(0);
            }
        }
        
        private SampleModel _sampleModel;

        [Before]
        public void BeforeTestSuite()
        {
            //GD.Print("Before Test Suite");
            _sampleModel = new SampleModel();
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
            _sampleModel = null;
        }

        [TestCase]
        public void SampleObservable_DefaultValue_IsZero()
        {
            // Arrange

            // Act
            var result = _sampleModel.SampleObservable.Value;

            // Assert
            AssertThat(result == 0).IsTrue();
        }

        [TestCase]
        public void SampleObservable_SetValue_IsUpdated()
        {
            // Arrange
            int newValue = 10;

            // Act
            _sampleModel.SampleObservable.Value = newValue;
            var result = _sampleModel.SampleObservable.Value;

            // Assert
            AssertThat(result == newValue).IsTrue();
        }

        [TestCase]
        public void SampleObservable_OnValueChanged_IsTriggered()
        {
            // Arrange
            int expectedValue = -1;
            _sampleModel.SampleObservable.OnValueChanged.AddListener((oldValue, newValue) => expectedValue = newValue);

            // Act
            int newValue = 20;
            _sampleModel.SampleObservable.Value = newValue;

            // Assert
            AssertThat(expectedValue == newValue).IsTrue();
        }
    }
}
