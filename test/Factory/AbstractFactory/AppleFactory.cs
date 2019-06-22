using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Factory.AbstractFactory
{
    class AppleFactory : AbstractFactory
    {
        public override ShuiGuo CreateShuiGuo()
        {
            return new Apple();
        }
    }
}
