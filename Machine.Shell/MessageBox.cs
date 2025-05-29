using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine.Shell
{
    public class MessageBox
    {
        public static DialogResult ShowDialog(string title, string message)
        {
            var form = new FormMessageBox
            {
                Title = title,
                Message = message
            };

            return form.ShowDialog();
        }
    }
}
