using MvvmCross.Core.ViewModels;
using members.Core.ViewModels;

namespace members
{
    /// <summary>
    /// This class is used to customize how the application starts
    /// and which view is loaded on start.
    /// </summary>
    public class AppStart : MvxNavigatingObject, IMvxAppStart
    {
        /// <summary>
        /// Hint can take command-line startup parameters to pass to called ViewModels
        /// </summary>
        /// <param name="hint"></param>
        public void Start(object hint = null)
        {
            // Pass the "MembersViewModel" as the first view to load in the app
            ShowViewModel<MembersViewModel>();
        }
    }
}