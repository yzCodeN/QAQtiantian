using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Factory.StaticFactory
{
    public class AnimalFactory
    {
        //静态工厂
        public static Animal GetAnimal(string strType)
        {
            if (strType == "喵喵")
            {
                return new Cat();
            }
            else if (strType == "汪汪")
            {
                return new Dog();
            }
            else if (strType == "哼哼")
            {
                return new Pig();
            }
            return null;
        }
    }
}
