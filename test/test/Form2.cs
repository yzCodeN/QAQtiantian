using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            (Owner as Form1).lbl1.Text = "使用Form2的Owner属性去修改Form1的值";
            (Owner as Form1).txt1.Text = "使用Form2的Owner属性去修改Form1的值";
        }

        private void Tools2_Click(object sender, EventArgs e)
        {
        }
    }
}
