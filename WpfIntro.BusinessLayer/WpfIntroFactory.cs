using System.Windows.Input;

namespace WpfIntro.BusinessLayer
{
    public static class WpfIntroFactory
    {
        private static IWpfIntroFactory _instance;

        public static IWpfIntroFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new WpfIntroFactoryImpl();
            }
            return _instance;
        }
    }
}