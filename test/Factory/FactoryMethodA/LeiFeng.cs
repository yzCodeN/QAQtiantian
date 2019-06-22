using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test.Factory.FactoryMethod
{
    public abstract class LeiFeng
    {
        public virtual void Sweep()
        {
            MessageBox.Show("我帮老人扫地");
        }
        public virtual void Wash()
        {
            MessageBox.Show("我帮老人洗衣");
        }
        public virtual void BuyRice()
        {
            MessageBox.Show("我帮老人买米");
        }
    }
}
