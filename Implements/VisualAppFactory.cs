using CameraTool.Implements.Internals;
using CameraTool.Implements.Abstractions;

namespace CameraTool.Implements
{
    public class VisualAppFactory : AppFactory
    {
        public override AppBase CreateEye4()
        {
            return new Eye4Visual();
        }

        public override AppBase CreateMI()
        {
            throw new NotImplementedException();
        }

        public override AppBase CreateYoosee()
        {
            return new YooseeVisual();
        }
    }
}