namespace CameraTool.Implements
{
    public class AppFactoryMaker
    {
        private static AppFactory _app;

        public static AppFactory GetAppFactory(string request)
        {
            if (request.Equals("point"))
                _app = new PointAppFactory();
            if (request.Equals("visual"))
                _app = new VisualAppFactory();
            return _app;
        }
    }
}