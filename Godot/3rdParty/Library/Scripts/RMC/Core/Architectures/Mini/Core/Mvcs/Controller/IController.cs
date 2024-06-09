
using System;

namespace RMC.Core.Architectures.Mini.Controller
{
    /// <summary>
    /// The Controller coordinates everything between
    /// the <see cref="IConcern"/>s and contains the core app logic 
    /// </summary>
    public interface IController : IConcern, IDisposable
    {
        //  Properties ------------------------------------

        //  Methods ---------------------------------------
    }
}