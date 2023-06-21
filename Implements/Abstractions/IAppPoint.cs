using CameraTool.Helpers;

namespace CameraTool.Implements.Abstractions
{
    public abstract class AppBase
    {
        protected ADBHelper ADB = new ADBHelper();

        public abstract void SetDeviceId(string id);
        public abstract Task RunAsync();
    }
}