using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Factory.StaticFactory
{
    class Cat : Animal
    {
        public override string GetAnimalType()
        {
            return "小猫喵喵";
        }
    }
}
