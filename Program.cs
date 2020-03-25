using ImageOrganizerWinForms.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageOrganizerWinForms
{
    static class Program
    {
        static ViewModelMain VmMain;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandler);

            // start main thread here
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            VmMain = new ViewModelMain();
            Application.Run(VmMain);
        }

        static void ExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            //Console.WriteLine("ExceptionHandler caught : " + e.Message);
            VmMain.ShowMessage("Unexpected Exception: " + e.Message);
            Toolbox.ShowWarning("Unexpected Exception: " + e.Message);
        }
    }
}
