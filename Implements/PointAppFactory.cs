using CameraTool.Implements.Internals;
using CameraTool.Implements.Abstractions;

namespace CameraTool.Implements
{
    public class PointAppFactory : AppFactory
    {
        public override AppBase CreateEye4()
        {
            return new Eye4Point();
        }

        public override AppBase CreateMI()
        {
            return new MiPoint();
        }

        public override AppBase CreateYoosee()
        {
            return new YooseePoint();
        }
    }
}