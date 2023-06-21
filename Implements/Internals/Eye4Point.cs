using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CameraTool.Implements.Abstractions;

namespace CameraTool.Implements.Internals
{
    public class Eye4Point : AppBase
    {
        private string _id;

        public override async Task RunAsync()
        {
            ADB.TapByPercent(_id, 76.7, 29.9);
            await Task.Delay(10000);
            ADB.TapByPercent(_id, 49.1, 42.6);
        }

        public override void SetDeviceId(string id)
        {
            _id = id;
        }
    }
}