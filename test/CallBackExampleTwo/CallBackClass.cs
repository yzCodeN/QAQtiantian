using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CallBackExampleTwo
{
    public class CallBackClass : ICallBacks
    {
        public void Run()
        {
            MessageBox.Show(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
        }
    }
}
