using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Hardware_MS
{
    class Functions
    {
        public void OpenForm(Form form, Panel pnlContainer)
        {
            //clear pnl content
            pnlContainer.Controls.Clear();

            //form wait
            FrmWait frmWait = new FrmWait();
            frmWait.TopLevel = false;

            form.TopLevel = false;
            form.AutoScroll = true;
            pnlContainer.Controls.Add(form);
            pnlContainer.Controls.Add(frmWait);
            frmWait.Show();
            frmWait.BringToFront();
            Application.DoEvents();
            Thread.Sleep(300);
            form.Show();
            form.BringToFront();
        }
        public void AllowNumbersOnly(object sender,KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
