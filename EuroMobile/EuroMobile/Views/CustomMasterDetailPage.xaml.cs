using Prism.Navigation;
using Xamarin.Forms;

namespace EuroMobile.Views
{
    public partial class CustomMasterDetailPage : MasterDetailPage, IMasterDetailPageOptions
    {
        public bool IsPresentedAfterNavigation
        {
            get { return false; }
        }

        public CustomMasterDetailPage()
        {
            InitializeComponent();
        }
    }
}