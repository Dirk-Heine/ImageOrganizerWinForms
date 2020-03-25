using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageOrganizerWinForms.Common
{
    ///// <summary>
    ///// Used to store wether a function was executed without error. Stores Error message in case it did not.
    ///// </summary>
    //class Result
    //{
    //    public bool Successful { get; set; }
    //    public Exception Error { get; set; }

    //    public Result()
    //    {
    //        Successful = false;
    //        Error = null;
    //    }

    //}

    public enum Status
    {
        Ok,
        Cancelled,
        //Failed,
        //Aborted,
        Warning,
        Error,
        //Success,
        //CheckState
    }
    public interface IResult
    {
        Status Status { get; }

        object State { get; }

        string Message { get; }
    }
    public class Result : IResult
    {
        public Status Status { get; }
        public object State { get; }
        public string Message { get; }

        public Result(Status status, object state = null, string message = "")
        {
            Status = status;
            State = state;
            Message = message;
        }
    }
}
