using RMC.Mini.Controller.Commands;
using RMC.Mini.Examples.Login.Mini.Model;

namespace RMC.Mini.Examples.Login.Mini.Controller.Commands
{
    /// <summary>
    /// This Command is a stand-alone object containing
    /// the before and after value during a data change
    /// </summary>
    public class LoggedInUserDataChangedCommand : ValueChangedCommand<UserData>
    {
        //  Initialization  -------------------------------
        public LoggedInUserDataChangedCommand(UserData previousValue, UserData currentValue) : 
            base(previousValue, currentValue)
        {
        }
    }
}