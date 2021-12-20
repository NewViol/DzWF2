using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace DzProgressBar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            VScrollBar dop = new VScrollBar();


           // textBox.Controls.Add(dop);
            textBox.ScrollBars = ScrollBars.Vertical;
            dop.Scroll += progressScroll;

            dop.MouseWheel += progressScrollForMouse;
        }

        private void progressScroll(object sender, ScrollEventArgs e)
        {
            var scroll = (VScrollBar)sender;
            progressBar.Minimum = 0;
            progressBar.Maximum = scroll.Maximum + 1;
            progressBar.Value = scroll.Value;
            progressBar.Step = scroll.Value / 10;
            
            if(scroll.Value != scroll.Maximum)
            {
                progressBar.PerformStep();
            }
        }

        private void progressScrollForMouse(object sender, MouseEventArgs e)
        {
            var scroll = (VScrollBar)sender;
            progressBar.Minimum = 0;
            progressBar.Maximum = scroll.Maximum + 1;
            progressBar.Value = scroll.Value;
            progressBar.Step = scroll.Value / 10;

            if (scroll.Value != scroll.Maximum)
            {
                progressBar.PerformStep();
            }
        }
        private void buttonShow_Click(object sender, EventArgs e)
        {
            FileStream file = new FileStream("DownloadText.txt", FileMode.OpenOrCreate);
            string dopstr = "The agency said its verification of the Verkhoyansk record highlighted how temperatures were increasing in a climatically important region of the world.";            
            
            StreamWriter sw = new StreamWriter(file);

            sw.Write(dopstr, dopstr.Length);
            StreamReader sr = new StreamReader(file);

            int sr_lenght = (int)file.Length;


            string dopStr = string.Empty;
            while (!sr.EndOfStream){
                dopStr += sr.ReadLine();
                progressBar.PerformStep();
            }
            sr.Close();
            textBox.Text = dopStr;
        }


    }
}
