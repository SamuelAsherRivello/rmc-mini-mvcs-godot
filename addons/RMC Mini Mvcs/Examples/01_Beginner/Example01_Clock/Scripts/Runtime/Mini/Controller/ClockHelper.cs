using System;
using System.Threading.Tasks;

namespace RMC.Mini.Examples.Clock.Mini
{
    /// <summary>
    /// This utility class hides the complexity of running
    /// repeated code without using engine-specific tick/update methods.
    /// </summary>
    public static class ClockHelper 
    {
        //  Methods ---------------------------------------
        public static async void StartTicking(Func<Task> fun)
        {
            var tcs = new TaskCompletionSource<bool>();
            await Task.Run(() => { tcs.SetResult(true);});
            tcs.Task.ConfigureAwait(true).GetAwaiter().OnCompleted(async() =>
            {
                //Call
                await fun.Invoke();
                    
                //Repeat
                StartTicking(fun);
            });
        }
    }
}