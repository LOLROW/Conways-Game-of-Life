using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Conways_Game_Of_Life
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form1 = new Form1();
            form1.Height = 300;
            form1.Width = 300;
            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            form1.Controls.Add(pictureBox);
            form1.StartPosition = FormStartPosition.CenterScreen;
            Thread thread = new Thread(() => Form1.Start(form1, pictureBox));
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            Application.Run(form1);
        }
    }
}