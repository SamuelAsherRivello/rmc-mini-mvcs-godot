
using RMC.Mini.Controller.Commands;

namespace RMC.Mini.Examples.Clock.Mini.Controller.Commands
{
    /// <summary>
    /// This Command is a stand-alone object containing
    /// the before and after value during a data change
    /// </summary>
    public class TimeChangedCommand: ValueChangedCommand<float>
    {
        //  Initialization  -------------------------------
        public TimeChangedCommand(float previousValue, float currentValue) : 
            base(previousValue, currentValue)
        {
        }
    }
}