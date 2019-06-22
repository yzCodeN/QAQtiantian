using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test.Factory.AbstractFactory
{
    class Orange : ShuiGuo
    {
        public override void Interact()
        {
            MessageBox.Show("购买了一个橘子");
        }
    }
}
