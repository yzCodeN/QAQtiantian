using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallBackExampleTwo
{
    public interface ICallBacks
    {
        void Run();
    }
    public class Controller
    {
        List<ICallBacks> CallBackObjects = new List<ICallBacks>();
        public void AddCallBack(ICallBacks callBacks)
        {
            CallBackObjects.Add(callBacks);
        }

        public void Begin()
        {
            foreach (var item in CallBackObjects)
            {
                item.Run();
            }
        }
    }
}
