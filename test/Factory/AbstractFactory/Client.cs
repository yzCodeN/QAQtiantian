using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Factory.AbstractFactory
{
    class Client
    {
        private ShuiGuo shuiGuo;

        public Client(AbstractFactory factory)
        {
            shuiGuo = factory.CreateShuiGuo();
        }

        public void Run()
        {
            shuiGuo.Interact();
        }
    }
}
