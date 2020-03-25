using System.Threading;
using System.ComponentModel;

namespace ImageOrganizerWinForms.Common
{
    /// <summary>
    /// BackgroundWorker which can be killed.
    /// 
    /// Usage:
    /// backgroundWorker1 = new AbortableBackgroundWorker();
    ///    //...
    ///    backgroundWorker1.RunWorkerAsync();
    /// if (backgroundWorker1.IsBusy == true)
    /// {
    ///    backgroundWorker1.Abort();
    ///    backgroundWorker1.Dispose();
    /// }
    /// </summary>
    public class AbortableBackgroundWorker : BackgroundWorker
    {
        private Thread workerThread;

        protected override void OnDoWork(DoWorkEventArgs e)
        {
            workerThread = Thread.CurrentThread;
            //try
            //{
                try
                {
                    base.OnDoWork(e);
                }
                catch (ThreadAbortException)
                {
                    e.Cancel = true; //We must set Cancel property to true!
                    Thread.ResetAbort(); //Prevents ThreadAbortException propagation
                }
            //}
            //catch
            //{
            //}
        }

        public void Abort()
        {
            if (workerThread != null)
            {
                workerThread.Abort();
                workerThread = null;
            }
        }
    }
}