using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hardware_MS
{
    static class Message
    {
        public static void ShowSuccess(string message, string title)
        {
            MessageBox.Show(message,title,MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
        }
        public static void ShowError(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
