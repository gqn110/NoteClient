using KHopeClient.IService;
using KHopeClient.Model;
using KHopeClient.Service;
using System;
using System.Windows.Forms;

namespace KHopeClient
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //生成表
            new CreateDefData().Create();

            Application.Run(new frmHome());
            //处理未捕获的异常
            //  Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //处理非UI线程异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        /// <summary>
        /// UI线程异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            CommonMethod.WriteLogErr(e.ExceptionObject.ToString());
        }

        /// <summary>
        /// 非UI线程异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            CommonMethod.WriteLogErr(e.Exception.Message + "\r\n" + e.Exception.StackTrace);
        }
    }
}
