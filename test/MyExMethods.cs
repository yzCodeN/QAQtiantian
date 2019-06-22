using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    static class MyExMethods  //我的扩展方法
    {
        //（1）扩展方法不能和调用的方法放到同一个类中
        //（2）第一个参数必须要，并且必须是this，这是扩展方法的标识。如果方法里面还要传入其他参数，可以在后面追加参数
        //（3）扩展方法所在的类必须是静态类
        //（4）最好保证扩展方法和调用方法在同一个命名空间下


        public static int MyListCount<T>(this IEnumerable<T> list)
        {
            int sum = 0;
            var e = list.GetEnumerator();
            while (e.MoveNext())
            {
                sum++;
            }
            return sum;
        }

        public static int MyListMax(this IEnumerable<int> list)
        {
            int Max = int.MinValue;
            var e = list.GetEnumerator();
            while (e.MoveNext())
            {
                if (Max < e.Current)
                {
                    Max = e.Current;

                }

            }
            return Max;
        }

        public static int ToInt(this string str)
        {
            return int.Parse(str);
        }

        public static void ForEach<T>(this IEnumerable<T> source,Action<T> func)   
        {
            //效果：可以按顺序输出每一个元素
            //使用方法
            //第一个参数指定只有实现了该接口的类才可以调用
            //委托用来指定输出什么
            
            //输出案例
            //var list = new MyListDuo();
            // list.ForEach(i => txt1.Text += i);

            foreach (var item in source)
            {
                func(item);
            }
        }

        internal static readonly object syn = new object();
        public static List<string> ToList(this Array a)
        {
            var list = new List<string>();
            foreach (var item in a)
            {
                list.Add(item.ToString());
            }
            return list;
        }

    }
}
