using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KHopeClient
{
    public class Tools
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="title">Error title (default: "Error")</param>
        internal static void WarningMessage(string message, string title = "提示")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
