using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test.Factory.FactoryMethod
{
    class UnDergaduate : LeiFeng
    {
        public override void BuyRice()
        {
            MessageBox.Show("真实输出");
        }
    }
}
