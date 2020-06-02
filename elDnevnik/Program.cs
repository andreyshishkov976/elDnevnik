using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace elDnevnik
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Login login = new Login();
            Application.Run(login);
            if (login.DialogResult == DialogResult.Yes) 
            {
                PrepodWin prepodWin = new PrepodWin(login.ID);
                Application.Run(prepodWin);
            }
            if(login.DialogResult == DialogResult.OK)
            {
                AdminWin adminWin = new AdminWin();
                Application.Run(adminWin);
            }
        }
    }
}
