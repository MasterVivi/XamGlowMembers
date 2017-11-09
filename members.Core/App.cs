using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace members
{
    public class App : MvxApplication
    {
        /// <summary>
        /// Setup IoC (Inversion of control) registrations.
        /// </summary>
        public App()
        {
            // Register the AppStart functionality as a sinleton to the
            // given interface
            var appStart = new AppStart();
            Mvx.RegisterSingleton<IMvxAppStart>(appStart);
        }
    }
}