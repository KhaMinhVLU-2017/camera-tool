using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CameraTool.Implements.Abstractions;

namespace CameraTool.Implements
{
    public abstract class AppFactory
    {
        public abstract AppBase CreateYoosee();
        public abstract AppBase CreateEye4();
        public abstract AppBase CreateMI();
    }
}