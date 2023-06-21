using CameraTool.Implements.Abstractions;

namespace CameraTool.Implements.Internals
{
    public class YooseePoint : AppBase
    {
        private string _id;

        public override async Task RunAsync()
        {
            ADB.TapByPercent(_id, 63.0, 31.0);
            await Task.Delay(20000);
            ADB.TapByPercent(_id, 76.3, 9.4);
        }

        public override void SetDeviceId(string id)
        {
            _id = id;
        }
    }
}