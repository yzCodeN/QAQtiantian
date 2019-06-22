using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System;
using System.IO;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Net.Http;
using CallBackExampleTwo;

namespace test
{
    #region 访问修饰符
    //Public：同一程序集中的任何其他代码或引用该程序集的其他程序集都可以访问该类型的成员；

    //Private：只有同一类或结构中的代码可以访问该类型或成员；

    //Protected：只有同一类或结构，或者此类的派生类中的代码才可以访问的类型或成员；

    //Internal：同一程序集中的任何代码都可以访问该类型或成员，但其他程序集中的代码不可以；

    //Protected internal：由其声明的程序集或另一个程序集派生的类中任何代码都可访问的类型或成员，从另一个程序集进行访问，必须在类声明中发生，该类声明派生自其中声明受保护的内部元素的类，并且必须通过派生的类类型的实例发生；

    #endregion

    #region 小玩意
    ////Clipboard类：表示访问剪切板，其中有很多方法，例如写一个 Clipboard.SetText("NMSL");  然后放在单击按钮中，当单击该按钮后，粘贴的数据就是NMSL

    ////全局唯一标识符 GUID
    ////广泛用于注册表、类标志，是一串基本不可能重复的字符串
    ////使用方式：
    //public class testGuid
    //{
    //    public static object StrGuid = Guid.NewGuid();
    //}
    #endregion

    #region 反射案例类
    ////放在最外面是因为用反射来对类进行实例化时，获取值必须是命名空间.类名，所以，只能放在命名空间往下一层开头的地方
    ////使用接口封装方法
    ////不直接实例化类，通过获取程序集中的类来进行实例化应用
    //interface INInstance
    //{
    //    void A1();
    //    void B1();
    //}

    //public class A : INInstance
    //{
    //    public string MyProperty { get; set; }
    //    public void Function(string a) { MessageBox.Show(a); }
    //    public void A1() { MessageBox.Show("A.A"); }
    //    public void B1() { MessageBox.Show("A.B"); }
    //}

    //public class B : INInstance
    //{
    //    public string MyProperty { get; set; }

    //    public void Function(string a) { MessageBox.Show(a); }

    //    public void A1() { MessageBox.Show("B.A"); }
    //    public void B1() { MessageBox.Show("B.B"); }
    //}
    #endregion
    public partial class Form1 : Form
    {
        #region 关于C#      
        #region 各种方法案例
        #region 多态与IEnumerable接口结合使用        
        public interface IAnimal
        {
            void eat();
        }
        class Monkey : IAnimal
        {
            public void eat()
            {
                MessageBox.Show("吃香蕉");
            }
        }
        class Pigeon : IAnimal
        {
            public void eat()
            {
                MessageBox.Show("吃咕咕");
            }
        }
        class Loin : IAnimal
        {
            public void eat()
            {
                MessageBox.Show("吃肉肉");
            }
        }
        class Feeder
        {
            public string Name;
            public Feeder(string _Name)
            {
                Name = _Name;
            }

            public void FeedAnimals(IEnumerable<IAnimal> ans)
            {
                MessageBox.Show("饲养员是" + Name);
                ans.ForEach(animal => animal.eat());
            }
        }
        #endregion

        #region 二叉树遍历
        //节点类
        public class ForEachNode<T>
        {
            //创建的节点类，其中每个节点有节点数据、左节点、右节点和父节点

            //节点数据
            public T Data { get; private set; }
            //左子节点
            public ForEachNode<T> LNode { get; set; }
            //右子节点
            public ForEachNode<T> RNode { get; set; }
            //父节点
            public ForEachNode<T> PNode { get; set; }
            public ForEachNode() { } //公有构造函数,用来作为默认调用

            public ForEachNode(T data)  //使用构造函数来获取初始数据
            {
                Data = data;
            }
        }
        //设置节点信息
        protected static void SetNodeInfo<T>(ref ForEachNode<T> node, ForEachNode<T> L, ForEachNode<T> R, ForEachNode<T> P)
        {
            node.LNode = L;
            node.RNode = R;
            node.PNode = P;
        }
        //先序遍历
        protected static void RootFirst<T>(ForEachNode<T> root)
        {
            //判断该对象是否为空
            if (root != null)
            {
                //不为空输出数据
                Console.Write(root.Data + "  ");
                //父节点输出完后再调用方法(递归)再次进入该方法,调用该对象的子节点输出子节点数据
                RootFirst(root.LNode);
                RootFirst(root.RNode);
            }
        }
        //中序遍历
        protected static void RootSecond<T>(ForEachNode<T> root)
        {
            //中序先输出该父节点的左子节点的信息
            //再输出父节点的信息
            //最后输出父节点的右子节点的信息
            if (root != null)
            {
                RootSecond(root.LNode);
                Console.Write(root.Data + "  ");
                RootSecond(root.RNode);
            }
        }
        //后序遍历
        protected static void RootLast<T>(ForEachNode<T> root)
        {
            //中序先输出该父节点的左子节点的信息
            //再输出父节点的右子节点的信息
            //最后输出父节点的信息
            if (root != null)
            {
                RootLast(root.LNode);
                RootLast(root.RNode);
                Console.Write(root.Data + "  ");
            }
        }
        public void testForEach(string str)
        {
            //初始化创建节点
            ForEachNode<string> nodeA = new ForEachNode<string>("A");
            ForEachNode<string> nodeB = new ForEachNode<string>("B");
            ForEachNode<string> nodeC = new ForEachNode<string>("C");
            ForEachNode<string> nodeD = new ForEachNode<string>("D");
            ForEachNode<string> nodeE = new ForEachNode<string>("E");
            //节点之间建立关系
            //D和E的父节点是C，且他们没有子节点,诸如此类的写法
            SetNodeInfo(ref nodeD, null, null, nodeC);
            SetNodeInfo(ref nodeE, null, null, nodeC);
            SetNodeInfo(ref nodeC, nodeD, nodeE, nodeA);
            SetNodeInfo(ref nodeB, null, null, nodeA);
            SetNodeInfo(ref nodeA, nodeB, nodeC, null);
            int i = 0;
            while (i < 2)
            {
                Console.WriteLine("\r\n1、先序遍历");
                Console.WriteLine("2、中序遍历");
                Console.WriteLine("3、后序遍历");
                Console.WriteLine("选择遍历方法：");

                string num = str;
                switch (num)
                {
                    case "1": RootFirst(nodeA); break;
                    case "2": RootSecond(nodeA); break;
                    case "3": RootLast(nodeA); break;
                    default: Console.WriteLine("请输入正确的数字"); break;
                }
                i++;
            }
        }
        #endregion
        public void InitData()  //数组
        {


            #region 我的弱智写法
            //int[] c = new int[] { 1, 2, 3 }; 
            //int[,] b = new int[3, 3];
            //int d = 0;
            //int i;
            //int j=0;
            //try
            //{
            //    for (i = 0; i < c.Length; i++)          
            //    {
            //        b[d, j] = c[j];
            //        j++;
            //        if (j==c.Length)
            //        {
            //            i = 0;
            //            j = 0;
            //            d++;
            //        }                  
            //        if (d==3)
            //        {
            //            i = c.Length + 1;
            //        }

            //    }

            //    txt1.Text = b[1, 1].ToString();                                               
            //}                                                                                 
            //catch (System.Exception ex)                                                              
            //{                                                                                        
            //                                                                                         
            //    MessageBox.Show(ex.Message);                                                      
            //}          
            #endregion
            #region 多维数组
            //                                                                                      { 0, 0 }
            //                                                                                      { 1, 2 }
            //                                                                                      { 2, 4 }
            //                                                                                      { 3, 6 }
            //                                                                                      { 4, 8 }
            /* 一个带有 5 行 2 列的数组 */
            int[,] a = new int[5, 2] { { 0, 0 }, { 1, 2 }, { 2, 4 }, { 3, 6 }, { 4, 8 } };  //5行2列，一个纵向排列的数组     

            int[,] b = new int[5, 4] { { 0, 0, 0, 0 }, { 1, 1, 1, 1 }, { 2, 2, 2, 2 }, { 3, 3, 3, 3 }, { 4, 4, 4, 4 } };
            int i, j;

            ///* 输出数组中每个元素的值 */      
            for (i = 0; i < 5; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    MessageBox.Show(string.Format("a[{0},{1}] = {2}", i, j, b[i, j]));
                }
            }
            #endregion

            #region 交错数组，基本与多维数组相同，不同的就是在每个数组中可以放入长度不一样的值
            int[][] c = new int[][] { new int[] { 0, 0 }, new int[] { 1, 2 }, new int[] { 2, 4 }, new int[] { 3, 6, 5, 4, 2 }, new int[] { 4, 8, 9 } };
            //{0,0}
            //{1,2}
            //{2,4}
            //{3,6,5,4,2}
            //{4,8,9}
            #endregion


        }
        #region 传递数组值给函数（方法）
        public double GetAvg(int[] z, int size)
        {
            int f;
            double Avg;
            int sum = 0; ;
            for (f = 0; f < size; ++f)
            {
                sum += z[f];
            }
            Avg = sum / size;
            return Avg;
        }
        #endregion

        #region 查询出数组中最大的数
        public int Array()
        {
            const int N = 50;
            int[] array = new int[N];
            for (int i = 0; i < N; i++)
            {
                array[i] = i;
            }

            int max = array[0];  //定义一个初始值
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > max)  //遍历数组，如果max小于当前数组中的这个值，就用这个值替换掉max的值
                {
                    max = array[i];
                }
            }

            return max; //长度是50，但是是从0开始计算，所以最后一个值是49
        }
        #endregion

        #region 使用for循环的简单排序
        public void paixu()
        {
            const int N = 50;  //定义的数组初始大小
            int[] array = new int[N];

            var r = new Random();  //定义的随机数

            for (int i = 0; i < N; i++)
            {
                array[i] = r.Next(1, 20);  //将随机数中的非负整数（范围在1~20之间的数给到array数组中）  
                txt1.Text += array[i].ToString() + "\r\n";//输出数组中的值
            }


            //排序  这处排序的思想，每次循环遍历一遍数组，找到其中最小的值，与第一个数交换，然后开始遍历的点+1，也就是从第二位开始遍历，循环完成排序
            int temp;
            for (int i = 0; i < N - 1; i++)  //外层循环，最后一个数为数组中倒数第二个数
            {
                for (int j = i + 1; j < N; j++)  //内层循环，最后一个数为数组中最后一个数
                {
                    if (array[i] > array[j])   //从数组中第一个数和第二个数开始比较，如果第一个数大于第二个数，则交换
                    {
                        temp = array[j];
                        array[j] = array[i];
                        array[i] = temp;
                    }
                }
            }
            txt1.Text += "------排序后------\r\n";
            foreach (var x in array)
            {
                txt1.Text += x.ToString() + "\r\n";
            }
        }


        #endregion

        #region 集合Dictionary（键值对集合）的应用
        void Dic()
        {
            var r = new Random();
            var dics = new Dictionary<int, string>();
            dics.Add(1, "A");
            dics.Add(2, "B");
            dics.Add(3, "C");
            dics.Add(4, "D");
            dics.Add(5, "E");
            dics.Add(6, "F");

            for (int i = 0; i < dics.Count; i++)
            {
                var Z = dics.ElementAt(r.Next(1, 6)).Value;
                txt1.Text += Z + "\r\n";
            }
            //foreach (var dic in dics)  //因为foreach遍历的值是只读的，无法修改，所以要使用for
            //{
            //    txt1.Text += dic.Value;
            //}
        }
        #endregion

        #region 特性案例类

        [Serializable]
        class A
        {
            [Required]
            public string Myproperty { get; set; }

            public void Fcuntion(int a) { MessageBox.Show(a.ToString()); }
        }
        class B
        {
            [Required]
            public string Myproperty { get; set; }

            public void Fcuntion(int a) { MessageBox.Show(a.ToString()); }
        }

        [AttributeUsage(AttributeTargets.Property)]    //表示特性可以修饰的对象，这处为只能修饰属性
        public class RequiredAttribute : Attribute      //自定义的特性，以Attribute结尾，特性的使用名就是去掉Attribute的值
        {
            public static bool IsPropertyRequired(object obj)
            {
                var type = obj.GetType();
                var properties = type.GetProperties();  //返回当前class的所有公共属性

                foreach (var property in properties)
                {
                    var attributes = property.GetCustomAttributes(typeof(RequiredAttribute), false);
                    if (attributes.Length > 0)
                    {
                        if (property.GetValue(obj) == null)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }
        #endregion

        #endregion

        #region 委托、Lambda和订阅发布模式
        #region 委托，匿名函数
        //委托是用来解决无法封装重复性高的方法中的变化点，引入委托也叫匿名函数
        //第一步 声明委托类型
        //第二步 修改形参列表
        //第三步 传入委托
        //理解：委托用来封闭函数，使用delegate关键字其后为数字签名，数字签名为 方法的类型和形参；
        //      声明完后，在需要封闭的函数内将该委托定义为一个形参，同时在需要使用函数才能封闭的点使用；
        //      然后在声明委托之后的位置对委托进行赋值，然后使用定义的名字去调用该委托；

        //对一个数进行比较，也就是传一个参数就行了
        delegate bool Function(int num);  //第一步

        static Function bijiao10 = delegate (int n) { return n > 10; };
        static Function isEven = delegate (int n) { return n % 2 == 0; };

        static List<int> Traverse(List<int> nums, Function function)  //第二步
        {
            var list = new List<int>();
            foreach (var num in nums)
            {
                if (function(num))
                {
                    list.Add(num);
                }
            }
            return list;
        }

        //两个数进行比较，=-=两个参数  
        //Lambda：一种简化委托的写法
        //分为语句Lambda和表达式Lambda
        //语句Lambda：
        //(type var,...) => {...}  第一步：将delegate转换为 "=>" 箭头符号表示
        //(var,...) => {...}       第二步：因为在最开始的委托声明中已经声明了类型，所以这处可以省略类型
        //var => {...}             在只有一个参数的情况下，可以省略不写括号
        //() => {...}              在没有参数的情况下，用一个括号表示
        //表达式Lambda：也就是只有一条语句的情况下，箭头函数后的操作无需加大括号区分区域
        //  n => n % 2 == 0

        //delegate bool FunctionMax(int maxValue, int max);  自定义委托的声明

        //static FunctionMax MaxValue = delegate (int maxValue, int max) { return maxValue > max; };
        //static FunctionMax MinValue = delegate (int maxValue, int max) { return maxValue < max; };


        static int MaxMinValue(List<int> nums, Func<int, int, bool> functionMax)   //这处可以直接使用系统自带的泛型委托Func<>
        {
            int max = nums[0];
            var list = new List<int>();
            foreach (var num in nums)
            {
                if (functionMax(num, max))
                {
                    max = num;
                }
            }
            return max;
        }

        static void GetFullInfo(string yourname, Action<string> action)    //系统自带的泛型委托Action<>
        {
            string firstStr = "Welcome to cnblogs ";
            action(firstStr + yourname);
        }
        #endregion

        #region 订阅发布模式没看懂版
        //完成了多发布多订阅的情况，而且可以设置退订，但是目前没看懂
        #region 订阅者接口
        //定义订阅事件
        public delegate void SubscribeHandle(string str);
        //定义订阅接口
        public interface ISubscribe
        {
            event SubscribeHandle SubscribeEvent;
        }
        #endregion
        #region 发布者接口
        //定义发布事件
        public delegate void PublishHandle(string str);
        //定义发布接口
        public interface IPublish
        {
            event PublishHandle PublishEvent;

            void Notify(string str);
        }
        #endregion

        #region 订阅器
        public class SubPubComponet : ISubscribe, IPublish  //订阅器
        {
            private string SubName;
            public SubPubComponet(string subName)
            {
                SubName = subName;
                PublishEvent += new PublishHandle(Notify);
            }

            #region ISubscribe Members  
            //订阅成员  I Subscribe Members 
            event SubscribeHandle subscribeEvent;
            event SubscribeHandle ISubscribe.SubscribeEvent
            {
                add { subscribeEvent += value; }  //value等同于
                remove { subscribeEvent -= value; }
            }
            #endregion

            #region IPublish Members
            //发布成员
            public PublishHandle PublishEvent;

            event PublishHandle IPublish.PublishEvent
            {
                add { PublishEvent += value; }
                remove { PublishEvent -= value; }
            }
            #endregion

            public void Notify(string str)
            {
                if (subscribeEvent != null)
                    subscribeEvent(string.Format("\r\n消息来源{0}:消息内容:{1}", SubName, str));
            }
        }
        #endregion

        #region 订阅者S
        public class Subscriber
        {
            private string _subscriberName;

            public Subscriber(string subscriberName)
            {
                this._subscriberName = subscriberName;
            }

            public ISubscribe AddSubscribe { set { value.SubscribeEvent += Show; } }  //此处value代表接口本身
            public ISubscribe RemoveSubscribe { set { value.SubscribeEvent -= Show; } }

            private void Show(string str)
            {
                MessageBox.Show(string.Format("我是{0}，\r\n我收到订阅的消息是:{1}\r\n", _subscriberName, str));
            }
        }
        #endregion

        #region 发布者P
        public class Publisher : IPublish
        {
            private string PublisherName;

            public Publisher(string publisherName)
            {
                this.PublisherName = publisherName;
            }

            private event PublishHandle PublishEvent;
            event PublishHandle IPublish.PublishEvent
            {
                add { PublishEvent += value; }
                remove { PublishEvent -= value; }
            }

            public void Notify(string str)
            {
                if (PublishEvent != null)
                    PublishEvent.Invoke(string.Format("\r\n我是{0},我发布{1}消息\r\n", PublisherName, str));
            }
        }
        #endregion

        void zz()
        {
            #region TJVictor.DesignPattern.SubscribePublish
            //新建两个订阅器
            SubPubComponet subPubComponet1 = new SubPubComponet("订阅器1");
            SubPubComponet subPubComponet2 = new SubPubComponet("订阅器2");
            //新建两个发布者
            IPublish publisher1 = new Publisher("发布者1");
            IPublish publisher2 = new Publisher("发布者2");
            //与订阅器关联
            publisher1.PublishEvent += subPubComponet1.PublishEvent;
            publisher1.PublishEvent += subPubComponet2.PublishEvent;
            publisher2.PublishEvent += subPubComponet2.PublishEvent;
            //新建两个订阅者
            Subscriber s1 = new Subscriber("订阅人1");
            Subscriber s2 = new Subscriber("订阅人2");
            //进行订阅
            s1.AddSubscribe = subPubComponet1;
            s1.AddSubscribe = subPubComponet2;
            s2.AddSubscribe = subPubComponet2;
            //发布者发布消息
            publisher1.Notify("博客1");
            publisher2.Notify("博客2");
            ////发送结束符号
            //MessageBox.Show("".PadRight(50, '-'));
            ////s1取消对订阅器2的订阅
            //s1.RemoveSubscribe = subPubComponet2;
            ////发布者发布消息
            //publisher1.Notify("发布者消息博客1\r\n");
            //publisher2.Notify("发布者消息博客2\r\n");
            ////发送结束符号
            //MessageBox.Show("".PadRight(50, '-'));
            #endregion
        }

        #endregion

        ////多播委托：只要委托的签名（类型和形参）相同，在此基础上那么各个委托之间可以像一个list一样去进行 "+" "-" "+=" "-="；
        ////all = AddThenPrint + print;  all(2);  输出  3  2
        //static Action<int> all;

        //static Action<int> print = i => MessageBox.Show(i.ToString());

        //static Action<int> AddThenPrint = i => { i++; MessageBox.Show(i.ToString()); };

        //#region 报社-订阅发布设计模式
        ////interface INewspaper
        ////{
        ////     void SetNewspaper(Newspaper newspaper);

        ////     void readNewspaper();
        ////}

        //class Company//: INewspaper
        //{
        //    public string Name { get; set; }
        //    public Company(string name)
        //    {
        //        Name = name;
        //    }

        //    public Newspaper Newspaper { get; set; }  //实例化报纸类用来点出报纸类中的值

        //    public void SetNewspaper(Newspaper newspaper)   //获取到报纸类中的实例，然后可以用这个实例去点出值
        //    {
        //        Newspaper = newspaper;
        //    }

        //    public void readNewspaper()          //定义一个显示出的数据，将值放入进去
        //    {
        //        MessageBox.Show("公司" + Name + "正在读报纸，出版社：" + Newspaper.PublisherName + ",标题是" + Newspaper.Title + ",内容是" + Newspaper.Content);
        //    }
        //}

        //class Person//: INewspaper
        //{
        //    public string Name { get; set; }  //人的名字
        //    public Person(string name)        //用Person类的构造函数来对名字进行初始化
        //    {
        //        Name = name;
        //    }

        //    public Newspaper Newspaper { get; set; }  //实例化报纸类用来点出报纸类中的值

        //    public void SetNewspaper(Newspaper newspaper)   //获取到报纸类中的实例，然后可以用这个实例去点出值
        //    {
        //        Newspaper = newspaper;
        //    }

        //    public void readNewspaper()          //定义一个显示出的数据，将值放入进去
        //    {
        //        MessageBox.Show("个人" + Name + "正在读报纸，出版社：" + Newspaper.PublisherName + ",标题是" + Newspaper.Title + ",内容是" + Newspaper.Content);
        //    }
        //}

        //class Publisher
        //{
        //    public string Name { get; set; }      //定义报社类的名字
        //    public Publisher(string name)
        //    {
        //        Name = name;
        //    }

        //    //public List<INewspaper> Subscribers = new List<INewspaper>();   //接口方法实现观察者模式

        //    //public delegate void _Subscribers(Newspaper newspaper);   //多播委托实现观察者模式
        //    //public _Subscribers Subscribers;
        //    public event Action<Newspaper> Subscribers = null;   //以上两句为无返回值的带一个参数的委托，完全可以使用Action代替

        //    public void SendNewspaper(Newspaper newspaper) //该函数也叫做发布、通知、广播，也就是触发该函数时就向所有订阅该委托的用户们发送数据
        //    {
        //        newspaper.PublisherName = Name;
        //        //Subscribers?.Invoke(newspaper);   //?.Invoke等价于使用if判空并且执行该语句，不过该语句是一次执行完委托链，无法侦测异常
        //        if (Subscribers != null)
        //        {
        //            foreach (Action<Newspaper> handler in Subscribers.GetInvocationList())  //取出每一个对象完成委托，然后加上try进行异常判断
        //            {
        //                try
        //                {
        //                    handler(newspaper);
        //                }
        //                catch (Exception ex)
        //                {
        //                    MessageBox.Show(ex.Message);
        //                }
        //            }
        //        }
        //        //var p1 = new Person("A");
        //        //var p2 = new Person("B");
        //        //var p3 = new Person("C");

        //        //p1.SetNewspaper(newspaper);   //使用List中的ForEach循环取得每一个实现了接口的class，并给其实例化报纸
        //        //p2.SetNewspaper(newspaper);
        //        //p3.SetNewspaper(newspaper);
        //    }
        //}

        //class Newspaper
        //{
        //    public string PublisherName { get; set; }   //出版社名
        //    public string Title { get; set; }  //标题
        //    public string Content { get; set; }   //内容
        //}
        #endregion

        #region 接口、泛型、递归和异常
        //泛型类型（包括类、接口、委托和结构——没有泛型枚举）和泛型方法
        #region 接口

        public interface IWorker { void work(string s); }
        class James1 : IWorker
        {
            public void work(string s)
            {
                MessageBox.Show("我的名字是James1，我的工作是" + s);
            }
        }
        class James2 : IWorker
        {
            public void work(string s)
            {
                MessageBox.Show("我的名字是James2，我的工作是" + s);
            }
        }

        public void InterFaceTest()
        {
            IWorker worker = new James1();
            worker.work("找工作");
        }
        #endregion

        #region 接口的方便实现 
        public interface IFly { void fly(); }  //定义接口
        abstract class Vehicle       //定义基类
        {

        }

        class Plane : Vehicle, IFly, IWorker    //子类继承基类和接口，中间使用逗号分隔,接口可以同时实现多个
        {
            public void fly()   //重载方法
            {
                //throw new System.NotImplementedException();
                MessageBox.Show("灰机在飞");
            }

            public void work(string s)
            {
                //throw new System.NotImplementedException();
                MessageBox.Show("我是交通工具类下的飞机类，我可以飞，我的名字叫：" + s);
            }
        }

        void FaceTest(IFly fly, IWorker worker)   //定义一个方法，然后在要输出的地方实例化类带入到方法实现功能
        {
            fly.fly();
            worker.work("灰机");
        }
        #endregion

        #region 泛型方法
        static void Swap<T>(ref T a, ref T b) //相当于声明了一个类型，是什么类型，由带入的实参决定，编译器自行判断
        {
            //ref可以理解为实参和形参之间如果不加ref，传递的是副本，也就是复制一份传递到形参，如果加了ref，就是直接把实参的地址传递过去
            //可以理解为不加ref，实参的值不会受方法内的操作影响，因为传递的是复制体；
            //如果加了ref，也就是传递本体，在方法内的操作也会影响到实参
            T t;
            t = a;
            a = b;
            b = t;
            MessageBox.Show("a：" + a.ToString() + "\r\nb：" + b.ToString());
        }
        #endregion

        #region 递归和异常的应用
        static long FileOrDirCount(string path, bool onlyFile, bool onlyDir, string extension = ".cs")
        {
            long Count = 0;  //需要注意，读取的文件夹和文件包括隐藏和只读的文件和文件夹，但是直接在D盘里面查看不会汇总出隐藏和只读的文件和文件夹，除非你选择显示隐藏和只读，所以有差异
            try
            {
                //统计file的个数  文件
                if (onlyFile)
                {
                    var files = Directory.GetFiles(path); //获取文件路径
                    /**************************************/
                    //此处代码为取得后缀名为固定值的文件个数
                    foreach (var file in files)
                    {
                        if (file.ToLower().EndsWith(extension) || !string.IsNullOrEmpty(extension))
                        {
                            Count++;
                        }
                    }
                    /**************************************/
                    Count += files.Length;   //取得当前路径有多少文件
                }

                //统计directory的个数   目录（也就是文件夹）

                var dirs = Directory.GetDirectories(path);
                if (onlyDir) { Count += dirs.Length; }
                foreach (var dir in dirs)
                {
                    Count += FileOrDirCount(dir, onlyFile, onlyDir);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;     //该语句表示将异常返回到IDE中 该关键字有多种用法：https://blog.csdn.net/pan_junbiao/article/details/79947563
            }
            return Count;


        }

        #endregion
        #endregion

        #region 枚举、结构体和构造函数

        enum GetTest
        {
            oneDay = 1,
            twoDay = 3,
            thereDay = 5,
            fourDay = 7,
            fiveDay = 9,
            sixDay = 11,
            seven = 13,
            eight = 15,
            nine = 17,
            ten = 99
        }

        enum len { Length, width, height };

        #region 结构体
        struct Books  //结构体，相当于定义了一个新的类型，里面可以放不同的基础数据类型    --1
        {
            public string title;
            public string author;
            public string subject;
            public int book_id;

            public void GetValue(string t, string a, string s, int i) //在结构体中定义的方法
            {
                title = t;
                author = a;
                subject = s;
                book_id = i;
            }

            public void ShowValue()
            {
                MessageBox.Show("标题为：" + title + "\r\n作者为：" + author + "\r\n主题为：" + subject + "\r\n书籍编号为：" + book_id);
            }
        }
        #endregion

        #region 构造函数类
        ////构造函数
        public class ProgramTest
        {
            int j;
            public ProgramTest()
            {
                j = 3;
                MessageBox.Show("I am ProgramTest 默认构造函数,j=" + j);
            }
            public ProgramTest(int i)
            {
                j = 2;
                MessageBox.Show("I am ProgramTest 有参构造函数,i=" + i + ",j=" + j);
            }
        }
        //私有构造函数
        public class Test
        {
            private Test()
            {
                int j = 3;
                MessageBox.Show("I am ProgramTest 默认构造函数,j=" + j);
            }
            public Test(int i)
            {
                Test test = new Test();
            }
        }

        //静态构造函数
        public class StaticTest
        {
            static int i;
            static StaticTest()
            {
                i = 1998;
            }

            public void zz()
            {
                MessageBox.Show(i.ToString());
            }
        }

        //同时存在静态构造函数和默认构造函数
        public class FullTest
        {
            private static int i;
            static FullTest()    //静态构造函数始终会第一个被.NET自动调用
            {
                i = 88;
                MessageBox.Show("静态构造函数" + i.ToString());
            }

            public FullTest()
            {
                MessageBox.Show("默认构造函数" + i.ToString());
            }

            ~FullTest()     //析构函数，会在关闭窗口时输出
            {
                MessageBox.Show("输出完毕，自动销毁变量");
            }
        }
        #endregion
        #endregion

        #region 继承与多态

        #region 继承类
        //public class testJC  //基类
        //{
        //    protected int width, height;   //protectend：用这个修饰符的只有此类及其子类可以访问
        //    public void setWidth(int z)
        //    {
        //        width = z;
        //    }
        //    public void setHeight(int y)
        //    {
        //        height = y;
        //    }
        //}

        //class testJCdd : testJC  //子类    继承了testJC类
        //{
        //    public int getWH()      //可以使用基类中的变量和方法
        //    {
        //        return (width * height);
        //    }
        //}

        //public void test()  //定义的方法
        //{
        //    testJCdd testJCdd = new testJCdd();
        //    testJCdd.setWidth(10);
        //    testJCdd.setHeight(3);
        //    MessageBox.Show(testJCdd.getWH().ToString());
        //}
        #endregion

        #region 基类的初始化  
        //class Rectangle  //这个继承中讲的一些东西：子类调用了父类构造函数的形参，对父类中的变量实现了初始化
        //{
        //    // 成员变量
        //    protected double length;
        //    protected double width;
        //    public Rectangle() { } //父类无参构造函数，在子类使用无参构造函数时调用
        //    public Rectangle(double l, double w)
        //    {
        //        length = l;
        //        width = w;
        //    }
        //    public double GetArea()
        //    {
        //        return length * width;
        //    }
        //    public void Display()
        //    {
        //        MessageBox.Show("长度：" + length);
        //        MessageBox.Show("宽度：" + width);
        //        MessageBox.Show("面积：" + GetArea());
        //    }
        //}//end class Rectangle  
        //class Tabletop : Rectangle
        //{
        //    public Tabletop(double l, double w) : base(l, w)  //子类构造函数调用父类构造函数的参数,也就是子类继承父类的构造函数参数
        //    {

        //    }
        //    public double GetCost()
        //    {
        //        double cost;
        //        cost = GetArea() * 70;
        //        return cost;
        //    }
        //    public void Display()
        //    {
        //        base.Display();
        //        MessageBox.Show("成本：" + GetCost());
        //    }
        //}
        #endregion


        #region 多态类
        //public class Animal
        //{
        //    public int Age;
        //    public string Name;
        //    public virtual void Cry() { }
        //}

        //public class Cat : Animal
        //{
        //    public override void Cry()
        //    {
        //        MessageBox.Show("喵喵,名字：" + Name + ",年龄：" + Age);
        //    }
        //}

        //public class Dog : Animal
        //{
        //    public override void Cry()
        //    {
        //        MessageBox.Show("汪汪,名字：" + Name + ",年龄：" + Age);
        //    }
        //}

        //public void test(Animal animal)
        //{
        //    animal.Cry();
        //}
        #endregion

        #region 多态练习 一
        //已有人物，，两个
        //血条，，，两个
        public abstract class Man
        {
            public abstract void Render();  //共有的方法
        }

        public class Pgist : Man //主角
        {
            public int HP = 100;              //血量和消息
            public override void Render()
            {
                MessageBox.Show("主角的HP剩余：" + HP);
            }
        }

        public class Mer : Man //雇佣兵
        {
            public int HP = 200;
            public override void Render()
            {
                MessageBox.Show("雇佣兵的HP剩余：" + HP);
            }
        }

        public class ManTest                //用一个类实例化两个class，可以将所有放实例值的class放一起实例化
        {
            public Pgist HP = new Pgist();
            public Mer MerHP = new Mer();
        }

        public void redraw(List<Man> mans)  //子类和基类属于同类型，所以在方法中调用子类也可以
        {
            foreach (var man in mans)
            {
                man.Render();
            }
        }
        #endregion

        #region 多态练习二
        public abstract class Document
        {
            public abstract void Init();
            public abstract void SetParameters();
            public abstract void Print();
        }

        public class PDF : Document
        {
            public override void Init()
            {
                MessageBox.Show("PDF打印初始化中.....");
            }
            public override void SetParameters()
            {
                MessageBox.Show("PDF参数设定中......");
            }
            public override void Print()
            {
                MessageBox.Show("PDF文件打印中......");
            }
        }

        public class BMP : Document
        {
            public override void Init()
            {
                MessageBox.Show("BMP打印初始化中.....");
            }
            public override void SetParameters()
            {
                MessageBox.Show("BMP参数设定中......");
            }
            public override void Print()
            {
                MessageBox.Show("BMP文件打印中......");
            }
        }

        void Print(Document doc)
        {
            doc.Init();
            doc.SetParameters();
            doc.Print();
        }
        #endregion

        #region 运算符重载
        class Box
        {
            private double length;      // 长度
            private double breadth;     // 宽度
            private double height;      // 高度

            public double getVolume()
            {
                return length * breadth * height;
            }
            public void setLength(double len)
            {
                length = len;
            }

            public void setBreadth(double bre)
            {
                breadth = bre;
            }

            public void setHeight(double hei)
            {
                height = hei;
            }
            // 重载 + 运算符来把两个 Box 对象相加
            public static Box operator +(Box b, Box c)
            {
                Box box = new Box();
                box.length = b.length + c.length;
                box.breadth = b.breadth + c.breadth;
                box.height = b.height + c.height;
                return box;
            }
        }
        #endregion
        #endregion
        #region 集合List  迭代器和foreach的关系
        class Enumerator   //定义的迭代器，所谓的迭代器就是一层一层的往下，不过C#已经完全封装了迭代器，无需再自定义，可以直接调用接口
        {
            public Enumerator(int[] nums)  //初始化值
            {
                Nums = nums;
            }

            private int[] Nums = null;  //内部用来接收初始化值

            private int Index = -1;  //定义的迭代

            public bool MoveNext()   //迭代所用的用来一层层走的方法
            {
                Index++;
                return Index < Nums.Length;
            }

            public int Current { get { return Nums[Index]; } }   //获取到每一层迭代的值
        }

        class MyList : IEnumerable<int>
        {
            private int[] Nums { get; set; }

            public MyList(int n)
            {
                Nums = new int[n];

                var ran = new Random();

                for (int i = 0; i < n; i++)
                {
                    Nums[i] = ran.Next(1, 10);
                }

            }

            public IEnumerator<int> GetEnumerator()   //两个方法用来实现接口
            {
                foreach (var num in Nums)
                {
                    yield return num;       //yield return 表示按顺序返回值
                }


            }

            IEnumerator IEnumerable.GetEnumerator()  //因为历史原因而产生的方法，无需多的操作直接方法上一个方法
            {
                return GetEnumerator();
            }

            //public Enumerator GetEnumerator()   //自定义的迭代器所使用的方法，用来返回值
            //{
            //    return new Enumerator(Nums);
            //}

            //public void GetEnumerator(List<int> ListNum)
            //{
            //    foreach (var num in Nums)
            //    {
            //        ListNum.Add(num);
            //    }
            //}


        }
        #endregion

        #region Linq
        #region 案例
        //两个类，只需要继承接口，然后封装住，需要使用的方法直接写在静态类中，见MyExmethods类，非常方便
        class MyListDuo : IEnumerable<int>
        {
            public IEnumerator<int> GetEnumerator()
            {
                yield return 1;
                yield return 1;
                yield return 2;
                yield return 1;
                yield return 5;
                yield return 5;
                yield return 4;
                yield return 3;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

        }

        class MyListThere : IEnumerable<int>
        {
            public IEnumerator<int> GetEnumerator()
            {
                yield return 1;
                yield return 3;
                yield return 2;
                yield return 7;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
        #endregion

        #region 扩展方法理解
        //往封装好的代码（类）中如何添加新的方法来使用？
        //这时就需要用到了扩展方法
        //在外部定义一个类，改为静态类
        //在其中根据情况给访问修饰符，定义方法，参数使用(this [类或接口] [名字])来进行指向；
        //只要是实例的类或实现了接口的实例类就可以调用扩展方法
        //static int MyListCount(IEnumerable<int> list)
        //{
        //    int sum = 0;
        //    var e = list.GetEnumerator();
        //    while (e.MoveNext())
        //    {
        //        sum++;
        //    }
        //    return sum;
        //}

        //static int MyListMax(IEnumerable<int> list)
        //{
        //    int sum = int.MinValue;
        //    var e = list.GetEnumerator();
        //    while (e.MoveNext())
        //    {
        //        if (sum < e.Current)
        //        {
        //            sum = e.Current;

        //        }

        //    }
        //    return sum;
        //}
        #endregion

        #region 用来做案例的类

        public class Patent //专利
        {
            public string Title { get; set; }  //专利名
            public string YearOfpublication { get; set; } //公布\申请年份
            public string ApplicationNumber { get; set; } //注册申请号
            public long[] Inventorids { get; set; }  //专利的所属人，可以是多个
            public override string ToString()
            {
                return string.Format("{0}{1}", Title, YearOfpublication);
            }
        }

        public class Inventor  //专利人信息
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public string City { get; set; } //城市
            public string State { get; set; }  //州
            public string Country { get; set; }  //国家
            public override string ToString()
            {
                return string.Format("{0}({3}{1},{2})", Name, City, State, Country);
            }
        }

        public static class PatentData
        {
            public static readonly Inventor[] inventors = new Inventor[]   //创建了一个该类的只读数组，往数组的每一个值中添加一个该类的实例对象
            {
                new Inventor()
                {
                     Name = "Benjamin Fran",
                     City = "Philadelphia",
                     State = "PA",
                     Country = "USA",
                     Id = 1
                },
                new Inventor()
                {
                     Name = "Orville Wright",
                     City = "Kitty Hawk",
                     State = "NC",
                     Country = "USA",
                     Id = 2
                },
                  new Inventor()
                {
                     Name = "George Stephenson",
                     City = "Wylam",
                     State = "Northumberland",
                     Country = "UK",
                     Id = 3
                },
                new Inventor()
                {
                     Name = "John Michaelis",
                     City = "Chicago",
                     State = "LI",
                     Country = "USA",
                     Id = 4
                },
                new Inventor()
                {
                     Name = "Mary Jacob",
                     City = "New York",
                     State = "NY",
                     Country = "USA",
                     Id = 5
                }
            };

            public static readonly Patent[] patents = new Patent[]
            {
                new Patent()
                {
                    Title = "Bifocals",
                    YearOfpublication = "1784",
                    Inventorids = new long[]{1}
                },
                new Patent()
                {
                    Title = "Phonograph",
                    YearOfpublication = "1877",
                    Inventorids = new long[]{1}
                },
                new Patent()
                {
                    Title = "Kinetoscope",
                    YearOfpublication = "1888",
                    Inventorids = new long[]{4}
                },
                new Patent()
                {
                    Title = "Flying machine",
                    YearOfpublication = "1903",
                    Inventorids = new long[]{2,3}
                },
                new Patent()
                {
                    Title = "Steam Locomotive",
                    YearOfpublication = "1815",
                    Inventorids = new long[]{5}
                }
            };
        }

        public void Print<T>(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                txt1.Text += item + "---\r\n";
            }
        }
        #endregion

        #endregion
        #endregion

        public Form1()  //打开就响应的就叫做主线程
        {
            InitializeComponent();
        }
        #region 组合模式
        interface INode      //定义的公用接口
        {
            void Draw();
        }

        class Node : INode   //主方法
        {
            //因为每个类的实现功能不同，使用接口统一方法
            public List<INode> Nodes = new List<INode>();    //定义的节点集合
            public void Draw()
            {
                Nodes.ForEach(node => node.Draw());          //实现每一个实现了INode接口的类的Draw()方法
            }

        }
        //不同的方法，如果再需要添加类似的方法，只需要继续添加类，实现接口，实例化就可以用而不是修改
        class NodeType1 : INode
        {
            public void Draw()
            {
                MessageBox.Show("写生");
            }
        }
        class NodeType2 : INode
        {
            public void Draw()
            {
                MessageBox.Show("速写");
            }
        }
        class NodeType3 : INode
        {
            public void Draw()
            {
                MessageBox.Show("素描");
            }
        }

        #region 组合模式应用
        //和上面的基本相同，使用组合模式可能会导致类都依赖于一个接口，就算该类无需接口中的方法，但是也要实现
        //公用交易接口
        interface ITrading
        {
            void Trading();
        }

        //银行
        class Bank : ITrading
        {
            public List<ITrading> Tradings = new List<ITrading>();

            public void Trading()
            {
                Tradings.ForEach(Trading => Trading.Trading());
            }
        }
        //存款
        class Deposit : ITrading
        {
            public Deposit(int _Balance)
            {
                Balance = _Balance;
            }

            //private int Balance;

            public int Balance;

            public void Trading()
            {
                MessageBox.Show("余额为：" + Balance);
            }
        }
        //取款
        class Withdrawals : ITrading
        {
            public Withdrawals(int _withdrawal)
            {
                withdrawal = _withdrawal;
            }

            public int withdrawal;

            public void Trading()
            {
                MessageBox.Show("以取款：" + withdrawal);
            }
        }
        //转账
        class Transfer : ITrading
        {
            public Transfer(int _transfer)
            {
                transfer = _transfer;
            }

            public int transfer;

            public void Trading()
            {
                MessageBox.Show("以转账：" + transfer);
            }
        }
        #endregion
        #endregion

        #region 模板模式
        //当创建多个类，每个类都有很多的重复代码
        #region 使用接口实现
        ////多个重复方法，使用接口抽象
        ////再定义一个操作类，写个方法表名需要对方法进行的操作
        ////然后实现了该接口的类就可以使用操作类中的方法
        //interface IXmlAble
        //{
        //    string GetHeader();
        //    string GetBody();
        //}

        //class GetXml
        //{
        //    public IXmlAble IGen;
        //    public void Get()
        //    {
        //        MessageBox.Show(IGen.GetHeader() + IGen.GetBody());
        //    }
        //}

        //class XmlFormat1 : IXmlAble
        //{
        //    public string GetHeader() { return "Xml1"; }

        //    public string GetBody() { return "Xml2"; }
        //}

        //class XmlFormat2 : IXmlAble
        //{
        //    public string GetHeader() { return "1"; }

        //    public string GetBody() { return "1"; }
        //}

        //class XmlFormat3 : IXmlAble
        //{
        //    public string GetHeader() { return "1"; }

        //    public string GetBody() { return "1"; }
        //}
        #endregion
        #region 使用父类继承实现
        ////abstract 表示该类为抽象类，不可实例化，不可赋值，只能用来继承
        ////在方法中不使用abstract关键字，而是使用virtual的话就表示该方法可以被复写
        //abstract class GetXml
        //{
        //    public virtual string GetHeader() { return "a"; }
        //    public abstract string GetBody();
        //    public void Getxml()
        //    {
        //        MessageBox.Show(GetHeader() + GetBody());
        //    }
        //}

        //class XmlType1 : GetXml
        //{
        //    public override string GetHeader()
        //    {
        //        return "Xml1Head";
        //    }

        //    public override string GetBody()
        //    {
        //        return "Xml1Body";
        //    }
        //}

        //class XmlType2 : GetXml
        //{
        //    public override string GetHeader()
        //    {
        //        return "Xml2Head";
        //    }

        //    public override string GetBody()
        //    {
        //        return "Xml2Body";
        //    }
        //}

        #endregion
        #endregion

        #region 桥接模式
        //例如：查看降雨的数据，将其显示在地图上        
        //如果不定义接口直接将数据类放入Map类显示？
        //第一：Map类做为显示数据的类可能会有多种数据，直接将其他数据类放入Map类就会产生Map类不封闭的情况
        //第二：Map类会依赖于其他的数据类，产生强耦合关系
        //定义接口
        //第一：Map和其他数据类没有了直接的关系，解耦合
        //第二：定义了接口，Map类只需要实现该接口中的方法即可，则代码封闭，Map类无需再修改
        //第三：增加数据类很方便，只需要继承接口，即可

        public interface IGetData { void GetData(); }
        class RainData : IGetData { public void GetData() { MessageBox.Show("我是降雨数据"); } }
        class SeedData : IGetData { public void GetData() { MessageBox.Show("我是种子数据"); } }

        interface ICanShow { void Show(); }
        //将数据显示到地图上
        class Map : ICanShow
        {
            public IGetData DataSet;   //从接口中获取数据
            public void Show() { DataSet.GetData(); }  //输出方法，输出实现了接口的类的方法
        }

        class Bar : ICanShow  //将数据显示到柱状图上
        {
            public IGetData DataSet;
            public void Show() { DataSet.GetData(); }
        }

        class Report : ICanShow    //将数据显示到报表上
        {
            public IGetData DataSet;
            public void Show() { DataSet.GetData(); }
        }

        //将不同的展示方法使用接口统一定义
        //该接口使用一个类实现，无需去查看是什么类，只要显示接口就调用
        //完成解耦并封闭该类
        //上面的Map类和Bar类也是如此
        class ShowStyle
        {
            public ICanShow canShow;
            public void ShowType() { canShow.Show(); }
        }

        //第一层的封装实例化方法
        public void ShowData(IGetData getData)
        {
            var Show = new Map() { DataSet = getData };
            Show.Show();
        }
        #endregion

        #region 状态模式
        #endregion

        #region 单例模式
        //该模式用于使类只能实例化一次，只允许创建一个实例
        //初始写法
        //class OnlyYou
        //{
        //    private static OnlyYou obj = null; //定义初始为null
        //    private OnlyYou() { }              //定义构造函数为私有
        //    public static OnlyYou GetOnlyYou()  //提供一个公有静态的方法
        //    {
        //        if(obj == null)                //如果这个对象为null，说明还没有创建
        //        {
        //            obj = new OnlyYou();      //直接创建一个该对象，然后return返回，获取了一个实例，且仅能有这一个
        //        }
        //        return obj;
        //    }
        // }
        //引出一个问题，在多线程的情况下，可能会同时调用该方法，就创建了多个实例，违背了初始的意愿
        //最粗暴的解决方法，加一把锁，lock；或者使用同步方法特性 [MethodImpl(MethodImplOptions.Synchronized)]
        //最简单的方法   
        public class OnlyYou
        {
            private OnlyYou() { }
            private static OnlyYou obj = null;
            static OnlyYou()  //创建的静态构造方法，该构造方法只会被创建一次，所以可以用来做单例模式
            {
                obj = new OnlyYou();
            }
            public static OnlyYou GetOnlyYou()
            {
                return obj;
            }
        }
        #endregion

        private void btn1_Click(object sender, EventArgs e)
        {
            int _a = 1;
            int _b = 2;
            int abc(int a,int b)
            {
                return a + b;
            }
            abc(_a, _b);
            #region 设计模式
            #region 状态模式实现


            #endregion

            #region 桥接模式实现
            ////查看方式类，使用不同的查看方式只需要创建一个实例，当然实例类的接口方法也得实现
            //var showType = new ShowStyle() { canShow = new Bar() { DataSet = new RainData() } };

            //////查看数据类，创建实例的写法
            ////var showRain = new Map() { DataSet = new RainData() };
            ////showRain.Show();

            ////var showSeed = new Map() { DataSet = new SeedData() };
            ////showSeed.Show();

            ////如果需要查看多个数据，则创建不同的实例,进一步简化代码，抽象为一个方法
            //ShowData(new RainData());
            //ShowData(new SeedData());
            #endregion

            #region 组合模式实现
            //var node = new Node();         //实例化

            //node.Nodes.Add(new NodeType1());
            //node.Nodes.Add(new NodeType2());

            //var node1 = new Node();
            //node1.Nodes.Add(new NodeType1());
            //node1.Nodes.Add(new NodeType2());
            //node.Nodes.Add(node1);

            //node.Nodes.Add(new NodeType3());

            //node.Draw();
            ////node1.Draw();  //也可以从某个节点开始运行
            ///
            //组合模式应用
            //var user = new Bank();
            //user.Tradings.Add(new Deposit(500));

            //user.Trading();
            #endregion

            #region 模板模式实现
            ////模板模式使用继承父类继承实现
            //GetXml getXml = new XmlType2();
            //getXml.Getxml();

            ////模板模式使用接口实现
            //var getXml = new GetXml() { IGen = new XmlFormat1()};
            //getXml.Get();
            #endregion

            #endregion

            //frm2.ShowDialog();  //和Show()方法不同的时使用该方法启动后，除非关闭Form2，否则无法将焦点给到Form1中

            #region 关于C#

            #region 多态和接口的结合使用
            //var controller = new Controller();
            //controller.AddCallBack(new CallBackClass());
            //controller.Begin();
            //var ans = new List<IAnimal> { new Monkey(), new Pigeon(), new Loin() };
            //var f = new Feeder("阿天");
            //f.FeedAnimals(ans);
            #endregion

            #region 多线程与同步
            ////创建线程关键字 Task
            //var task = new Task(() => txt1.Text+="创建了一个线程");     //这就是一个线程
            //task.Start();   //线程要使用Start()方法来启动
            //task.Wait();    //wait方法表示线程运行结束后等待一会

            ////另一种创建方式
            //var task2 = Task.Factory.StartNew(() => "1");   
            #region 多线程并行运算处理
            //txt1.Text += "-----------\r\n";
            //void Calc(Action action)     //封装的方法，用来统计处理所用的时间
            //{
            //    Stopwatch stopwatch = new Stopwatch();
            //    stopwatch.Start();
            //    //list.ForEach(i => i++);
            //    action();
            //    stopwatch.Stop();
            //    txt1.Text += stopwatch.ElapsedMilliseconds + "\r\n";
            //}
            //void Do(ref int i)
            //{
            //    i = (int)Math.Abs(Math.Sin(i));
            //}
            //var list = new List<int>();
            //int num = 5000000;
            //for (int i = 1; i <= num; i++)
            //{
            //    list.Add(i);
            //}

            //Calc(() => list.ForEach(i => Do(ref i)));

            //Calc(() => list.AsParallel().ForAll(i => Do(ref i)));

            //Calc(() => Parallel.ForEach(list, i => Do(ref i)));

            ////for的写法，目测是最快的
            ////Stopwatch sw2 = new Stopwatch();
            ////sw2.Start();
            ////Parallel.For(0, 5000000, i => i++);
            ////sw2.Stop();
            ////txt1.Text += sw2.ElapsedMilliseconds + "\r\n";
            #endregion

            #region 多线程异常处理
            //var task = Task.Factory.StartNew(() => { throw new ApplicationException("here is an error"); });
            //task.ContinueWith((t) => { MessageBox.Show(t.Exception.ToString()); }, TaskContinuationOptions.OnlyOnFaulted);
            ////try
            ////{
            ////    task.Wait();
            ////}
            ////catch (AggregateException exs)
            ////{

            ////    foreach (var ex in exs.InnerExceptions)
            ////    {
            ////        MessageBox.Show(ex.Message);
            ////    }
            ////}
            #endregion

            #region 同步
            ////对可变的状态进行同步
            ////不更改的东西不需要同步
            ////多个线程访问同一个对象时，必须同步

            ////个人理解：同步使多个线程使用同一对象时，不会产生冲突，而是按顺序执行

            //var syn = MyExMethods.syn;       //相同命名空间下，静态类中的静态属性无需实例化
            //int Count = 0;
            //void Increment()
            //{
            //    for (int i = 0; i < 50000000; i++)
            //    {
            //        //两种写法，这种为C#封装的写法，该类中还有其他原子操作方法，原子操作见同步最下方
            //        //第一种写法速度快于第二种
            //        Interlocked.Increment(ref Count); 
            //        //lock (MyExMethods.syn)
            //        //{
            //        //    Count++;
            //        //}

            //    }
            //}
            //void Decrement()
            //{
            //    for (int i = 0; i < 50000000; i++)
            //    {
            //        Interlocked.Decrement(ref Count);
            //        //lock (MyExMethods.syn)
            //        //{
            //        //    Count--;
            //        //}
            //    }
            //}

            //var task1 = new Task(() => Increment());  //创建一个线程，带入方法到委托
            //var task2 = new Task(Decrement);          //创建另一个线程，直接给方法名，系统自动识别

            //task1.Start();
            //task2.Start();
            //Task.WaitAll(task1, task2);

            //txt1.Text += Count+"\r\n";
            ////如果不加Lock关键词和定义syn的情况
            ////执行完后会发现进行了五百万次加减操作后的Count并不为0，因为线程是交错进行的
            ////每一次++操作，分为四个步骤
            ////步骤一.从Count获取到值 假设为 0     内部执行的操作： 将Count复制到了某个内存单元
            ////步骤二.将复制的值递增 0 ，结果为1                    在内存单元中+1
            ////步骤三.将结果值 1 ，复制到Count                      
            ////步骤四.从_Count中复制值 1                             再从内存单元中复制出来

            ////上面那种情况会导致线程不安全，数据处理产生错误，此时需要加上锁
            ////加锁关键词   lock
            ////定义一个私有静态只读对象   private static readonly object syn = new object()
            ////在产生变化的地方加上锁，参数为创建的这个对象，在内部写的操作为安全线程的操作

            ////上锁后就具有了原子性（无法分割）也就是lock关键字使操作具有原子性，完成了线程安全
            ////在此基础上，C#提供了一个原子操作的简单封装   关键词  Interlocked：为多个线程共享的变量提供原子操作
            #endregion
            #endregion

            #region 反射、特性、序列化、流的应用和动态编程
            #region 反射
            ////var a = new A();            //反射应用,反射中有许多直接操作元数据的方法
            ////var b = new B();
            ////var type = a.GetType();     //第一步：使用GetType或者typeof获取到Class A的类型

            ////var property = type.GetProperty("MyProperty");    //获取属性，参数为属性名并创建一个值来保存获取到的属性
            ////property.SetValue(a, "调用A类的属性");            //调用该属性的反射中才有的方法来对其操作，第一个参数为实例对象，第二个参数为要赋的值
            ////txt1.Text = a.MyProperty;     //此时再调用该属性查看      输出值为：调用A类的属性

            ////var function = type.GetMethod("Function");        //获取方法
            ////function.Invoke(a, new object[] { "调用function方法"});   //Invoke表示调用该方法，第一个参数为需要调用方法的类，第二个参数为方法中的形参

            ////定义一个值来接收输入框要调用的方法的名字

            //////如果要根据输入框的值来进行调用方法？
            ////var function2 = type.GetMethod(str.ToUpper());     //用反射轻松实现，直接根据输入值调用方法
            ////function2.Invoke(a, null);        //直接调用，因为要调用的方法没有参数，所以直接给null

            ////利用上面根据字符串实现方法，另一种使用，不直接实例化类，根据程序集名（也就是项目）去进行实例化类
            //var strA = txt1.Text;
            //var strA1 = txt2.Text;
            //var a = new A();
            ////var type = a.GetType();
            //try
            //{
            //    //组合写法
            //    var Instance = Assembly.Load("test").CreateInstance("test." + strA);    //获取指定路径程序集中的类的实例
            //    var function = Instance.GetType().GetMethod(strA1);
            //    function.Invoke(Instance, null);
            //    //分离写法
            //    //Assembly assembly = Assembly.Load("test");        //获取程序集
            //    //Type type = assembly.GetType("test." + strA);     //指明路径获取class A的type
            //    //object functionDou = Activator.CreateInstance(type); //获取该class的实例
            //    //var function = type.GetMethod(strA1);             //反射获取class中方法
            //    //function.Invoke(functionDou, null);               //Invoke调用方法，第一个参数为实例，第二个参数为方法中的形参赋值
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message);
            //}
            #endregion
            #region 特性
            //使用自定义特性
            //var A1 = new A() { Myproperty = "123"};
            //string str = RequiredAttribute.IsPropertyRequired(A1) ? "已经赋值" : "未赋值"; 
            //MessageBox.Show(str);

            //if (RequiredAttribute.IsPropertyRequired(A1)) //if写法
            //{
            //    MessageBox.Show("已经赋值"); 
            //}
            //else
            //{
            //    MessageBox.Show("未赋值");
            //}
            #endregion
            #region 序列化和反序列化
            ////第一步在需要序列化的实例给一个特性[Serializable]，表示可以序列化
            ////什么是序列化和反序列化？
            ////序列化：将一个实例保存成一种格式
            ////反序列化：将序列化的格式反序列化为实例
            ////在这个过程中可以实现很多能力，例如：传输到数据库，发送给别人
            //var a = new A();
            ////序列化
            //using (var steam = File.Open(typeof(A).Name + ".bin", FileMode.Create))  //表示只在该范围中实现实例化
            //{
            //    var bf = new BinaryFormatter();  
            //    bf.Serialize(steam, a);        //该方法有两个参数，第一个为流，带入之前定义的文件流，可以带入多种流，第二个参数为要序列化的对象
            //}

            ////反序列化
            //A after = null;
            //using (var steam = File.Open(typeof(A).Name + ".bin", FileMode.Open))
            //{
            //    var bf = new BinaryFormatter();
            //    after = (A)bf.Deserialize(steam);     //返回的是object，需要转换为原本的类型
            //}
            #endregion
            ////动态编程
            //dynamic a = 1;
            //a = "string";
            #endregion

            #region Linq

            #region 标准查询运算符
            //IEnumerable<Patent> patents = PatentData.patents;
            //IEnumerable<Inventor> inventors = PatentData.inventors;
            #region 案例
            //patents = patents.Where(p => p.YearOfpublication.ToInt() < 1850);  //判断申请日期是否大于1850

            //var filesPath = Directory.GetFiles("D:/");       //获取每一个文件，自定义返回类型
            //var x = filesPath.Select(filePath =>
            //  {
            //      var fileInfo = new FileInfo(filePath);
            //      return new { fileInfo.Name, fileInfo.Length };
            //  }
            //);

            //txt1.Text += patents.Count(c => c.YearOfpublication.StartsWith("18"));     //StartsWith：判断是否以指定的字符串开头

            //var OrderInventors = inventors.OrderBy(i => i.Country).ThenBy(n => n.Name);  //排序

            //var groups = inventors.GroupBy(i => i.Country);     //根据国家做分组
            //foreach (var group in groups)      //遍历分的组，分组的group如果存在下级还可以继续分组
            //{
            //    txt1.Text += "Count = "+ group.Count()+"\t\r\n";
            //    foreach (var g in group)
            //    {
            //        txt1.Text += "\r\n\t" + g.Name;
            //    }
            //    txt1.Text += "\r\n";
            //}
            #endregion

            #region Join(连接)
            ////第一个参数表示连接的集合
            ////第二第三参数表示连接条件,对应表的顺序
            ////第四参数集合连接后的输出
            //var x = patents.Join(inventors, p => p.Inventorids[0], i => i.Id, (p, i) => new { p.Title, i.Name });
            #endregion
            #endregion

            #region 标准查询表达式
            //标准查询表达式可以完全使用标准查询运算符的格式写出来，只是没有这么方便，简洁
            #region 简单Linq查询
            //var query =
            //       from patent in patents     //from给定一个名字，指定集合源
            //       where patent.YearOfpublication.StartsWith("18")   //where进行条件的判断
            //       select new { patent.YearOfpublication };       //select指定要查询的集合

            //var queryTwo =
            //       from fileName in Directory.GetFiles(" E:\\编程\\18.7.18邓楠天C#与前端\\点NET与SQL_2019-03-28更新\\18.7.18邓楠天C#\\C#")
            //       let fileInfo = new FileInfo(fileName)
            //       select new { fileInfo.Name, fileInfo.LastWriteTime };
            #endregion
            #region 较全Linq查询
            //还有select嵌套
            //join连接以及其他Linq用法
            //var quertThere =
            //        from inventor in inventors  //from可以理解为将一个集合分解成一个个元素再对其进行其他操作
            //        group inventor by inventor.Name into groups    //group用来分组，分组后应该是为一个表，into给一个表名
            //        from item in groups    //groups由于内部还有数据还可以分，所以继续分解
            //        orderby item.Name, item.Name           //排序
            //        select new { groups.Key, item.Name };    //Key值为用来group分组所用的by后面的元素
            #endregion
            #endregion
            //用来输出
            //txt1.Text += z;
            //Print(quertThere);
            #endregion

            #region 集合实现，迭代器和foreach的关系
            //var List = new MyList(5);

            ////得出foreach的使用条件为里面的数据必须实现了迭代器接口
            ////因为foreach的遍历实现就是内部调用的该接口的两个方法
            ////MoveNext() = 向下迭代    Current() = 返回迭代的值
            //foreach (var num in List)
            //{
            //    txt1.Text += num + "\r\n";

            //    foreach (var num2 in List)
            //    {
            //        txt1.Text += "\t" + num2 + "\r\n";
            //    }
            //}
            #region 使用自定义迭代器或使用while来实现输出
            //var e1 = nums.GetEnumerator();         
            //while (e1.MoveNext())
            //{
            //    txt1.Text += e1.Current + "\r\n";
            //    var e2 = nums.GetEnumerator();
            //    while (e2.MoveNext())
            //    {
            //        txt1.Text += "\t" + e2.Current + "\r\n";
            //    }
            //}
            #endregion
            #endregion

            #region 订阅发布设计模式响应
            //var A = new Person("A");  //实例化类，因为继承了接口INewspaper且重载了接口内的方法，所以可以用来实现接口
            //var B = new Person("B"); 
            //var C = new Person("C");

            //var D = new Company("D");
            //var publisher = new Publisher("101号出版社");  //实例化报社类

            ////+：多播委托的+就是将函数和委托或者委托和委托连接起来，也叫订阅或注册
            ////-：减就是取消订阅或叫做取消注册，也就是不加入这个函数或委托了
            //publisher.Subscribers += A.SetNewspaper;   //因为该委托的签名（返回类型和形参）与类中函数完全相同，所以可以使用多播委托
            //publisher.Subscribers += B.SetNewspaper;

            //publisher.Subscribers += (n) => { throw new ApplicationException("here are a error!"); };  //假定的出现的异常

            //publisher.Subscribers += C.SetNewspaper;
            //publisher.Subscribers += D.SetNewspaper;

            ////publisher.Subscribers.Add(A);   //将实现接口的类加入到集合中
            ////publisher.Subscribers.Add(B);
            ////publisher.Subscribers.Add(C);
            ////publisher.Subscribers.Add(D);

            ////使用报社类的方法对报纸类中的属性进行初始化
            //publisher.SendNewspaper(new Newspaper() { Title = "今日头条", Content = "NBA代言大使最强篮球王CXK" });

            //A.readNewspaper();  //调用Person类中的读报纸方法
            #endregion

            #region 委托
            //var nums = Traverse(new List<int>() { 1, 5, 9, 11, 15, 18, 19, 21 }, isEven);    //声明委托来完成操作
            //foreach (var num in nums)
            //{
            //txt1.Text += num + "\r\n";
            //}

            //var nums = MaxMinValue(new List<int>() { 7, 9, 5, 2, 4, 8, 9, 10, 15, 20, 99, 101, 1, 6, 8, 98, 0 }, (maxValue, max) => maxValue > max);  // Lambda表达式写法
            //MessageBox.Show(nums.ToString());
            #endregion

            #region 递归和异常的应用
            //string path = "D:\\";
            //FileOrDirCount(path, true, true);
            //MessageBox.Show("文件数为：" + FileOrDirCount(path, true, false).ToString() + "\r\n文件夹数为：" + FileOrDirCount(path, false, true).ToString() + "\r\n总数为：" + FileOrDirCount(path, true, true).ToString());
            ////MessageBox.Show(FileOrDirCount("E:\\18.7.18邓楠天C#与前端\\ASP.NET与SQL_2019-02-25更新\\18.7.18邓楠天C#\\C#", false).ToString());
            #endregion

            #region 计时器，用来测试代码的运行时间
            ////计时器用来计算在Start和Stop中间运行的代码所用的时间，以毫秒为单位
            //Stopwatch stopwatch = new Stopwatch();  //计时器
            //stopwatch.Start();
            //Plane plane = new Plane();
            //FaceTest(plane, plane);

            //stopwatch.Stop();
            //MessageBox.Show(stopwatch.ElapsedMilliseconds.ToString());
            #endregion

            #region 结构体
            //Books book1 = new Books();  // --1   调用结构体，需要实例化
            //Books book2 = new Books();
            //
            //book1.GetValue("不服就干", "阿天天", "继续工作", 10086);   //实例化结构体，输入不同实参，返回不同值
            //book2.GetValue("电信之子", "天天", "中国电信", 10001);
            //book1.ShowValue();
            //book2.ShowValue();
            #endregion

            #region 数组取值练习
            //int[] shuZu = new int[] { 100, 50, 1000, 1000000, 234 };  //定义的数组
            //MessageBox.Show(GetAvg(shuZu, shuZu.Length).ToString());
            //InitData();
            #endregion

            #region 枚举
            //第一种写法                       //枚举值默认类型为枚举名
            //GetTest test = GetTest.twoDay;   //先取出值，再定义一个值用来保存转换类型后的枚举值
            //int testInt = (int)test;
            //MessageBox.Show(testInt.ToString());
            //第二种写法
            //输出值为整数
            //int testInt = (int)GetTest.twoDay;     //直接定义一个数据类型的值，然后取得枚举的值，直接转换
            //MessageBox.Show(testInt.ToString());    //然后再转换为字符串输出
            //输出值为原枚举值
            //string testStr = GetTest.twoDay.ToString();
            //MessageBox.Show(testStr);

            //enum（枚举）的用法，使数据看上去更加直观
            //int[] parameter = new int[3] { 1, 5, 8 };
            //MessageBox.Show("Length: " + parameter[(int)len.Length].ToString());
            //MessageBox.Show("width: " + parameter[(int)len.width].ToString());
            //MessageBox.Show("height: " + parameter[(int)len.height].ToString());
            #endregion

            #region 构造函数
            //start
            //ProgramTest programTest = new ProgramTest();    //当实例化类时，会自动调用构造函数，多个构造方法按重载算
            //ProgramTest programTest1 = new ProgramTest(1);

            #region 私有构造函数
            //Test test = new Test();  //因为构造函数定义的是私有的，所有无法调用
            //Test test1 = new Test(1);  //但是重载的构造函数定义的是公有的，所以可以调用,因为构造函数在同一类中，私有的也可以调用，通过内部调用私有函数就可以通过该有参构造函数调用私有构造函数
            #endregion

            //静态构造函数
            //StaticTest test = new StaticTest();
            //test.zz();

            //同时存在静态构造函数和默认构造函数
            //FullTest fulltest = new FullTest();
            #endregion

            #region 多重继承和多态
            //// 多重继承
            //Rectangle Rect = new Rectangle();
            //int area;
            //Rect.setWidth(5);
            //Rect.setHeight(7);
            //area = Rect.getArea();
            //// 打印对象的面积
            //MessageBox.Show("总面积： " + Rect.getArea());
            //MessageBox.Show("油漆总成本： $" + Rect.getCost(area));


            //多态
            //Animal animal = new Cat() { Name = "小黑", Age = 1 };
            //test(animal);
            #region 多态练习一单击按钮实现
            //List<Man> mans = new List<Man>();
            //ManTest manTest = new ManTest();
            //manTest.HP.HP -= 20;
            //manTest.MerHP.HP -= 30;
            //mans.Add(manTest.HP);
            //mans.Add(manTest.MerHP);
            //
            //redraw(mans);
            #endregion

            #region 多态练习二单击按钮实现
            //string path = "../../abc.pdf";
            //string extension = "BMP";
            //Document doc = null;
            //if (extension.ToLower().Equals("pdf"))
            //{
            //    doc = new PDF();
            //    Print(doc);
            //}
            //else if (extension.ToLower().Equals("bmp"))
            //{
            //    doc = new BMP();
            //    Print(doc);
            //}

            #endregion
            #endregion

            #region 匿名对象
            //假如需要定义的字段、属性、方法并不常用，无需定义一个类来进行调用，直接var new一个对象，往里面放值即可使用，无需类名
            //var user = new { Name = "zzz", Age = 11 };
            //MessageBox.Show(user.Name);

            #endregion

            #endregion
        }

        //鼠标在窗体上发生移动
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripLabel1.Text = "X:" + e.X.ToString() + "Y:" + e.Y.ToString();
        }

        //计时器
        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripLabel2.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }

    }

}
