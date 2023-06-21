using CameraTool.Implements.Abstractions;

namespace CameraTool.Implements.Internals
{
    public class MiPoint : AppBase
    {
        private string _id;

        public override async Task RunAsync()
        {
            ADB.TapByPercent(_id, 49.4, 30.6);
            await Task.Delay(20000);
            ADB.TapByPercent(_id, 24.3, 36.2);
            await Task.Delay(10000);
            ADB.TapByPercent(_id, 84.0, 41.9);
            await Task.Delay(5000);
            ADB.TapByPercent(_id, 84.0, 41.9);

        }

        public override void SetDeviceId(string id)
        {
            _id = id;
        }
    }
}