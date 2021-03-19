using EasyFileTransfer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample.Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void SnedButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //EasyFileTransfer.Model.Response rsp = EftClient.Send(openFileDialog1.FileName, textBox2.Text, Convert.ToInt32(textBox1.Text));
                backgroundWorker1.RunWorkerAsync(openFileDialog1.FileName);
                timer1.Start();
                //if (rsp.status == 1)
                //{
                //    MessageBox.Show("send successfully.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string fn = (string)e.Argument;
            EasyFileTransfer.Model.Response rsp = EftClient.Send(fn, textBox2.Text, Convert.ToInt32(textBox1.Text));
            timer1.Stop();
            progressBar1.BeginInvoke((MethodInvoker)(() => progressBar1.Value = 100));
            MessageBox.Show(rsp.description, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            progressBar1.BeginInvoke((MethodInvoker)(() => progressBar1.Value = 0));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = EftClient.ProgressValue;
        }
    }
}
