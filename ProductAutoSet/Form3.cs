using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProductAutoSet;

namespace ProductAutoSet
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SQLiteExample SQLCall = new SQLiteExample();
        private void button1_Click(object sender, EventArgs e)
        {
            bool res = SQLCall.Match(textBox1.Text);
            if (!res)
            {
                MessageBox.Show("無此帳號", "帳號");
                return;
            }
            else
            {

            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(13))
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(13))
            {
                button1.PerformClick();

            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Form1.Global.OPID = "";
            Form1.Global.OPNAME = "";
        }
    }
}
