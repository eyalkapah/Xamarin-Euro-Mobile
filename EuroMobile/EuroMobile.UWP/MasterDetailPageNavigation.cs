using EuroMobile.UWP;
using EuroMobile.Views;
using System;

using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(MasterPage), typeof(MasterPageRenderer))]

namespace EuroMobile.UWP
{
    internal class MasterPageRenderer : PageRenderer
    {
        protected override Windows.Foundation.Size ArrangeOverride(Windows.Foundation.Size finalSize)
        {
            return base.ArrangeOverride(finalSize);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
            {
                return;
            }

            try
            {
                if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.XamlCompositionBrushBase"))
                {
                    AcrylicBrush myBrush = new AcrylicBrush();
                    myBrush.BackgroundSource = AcrylicBackgroundSource.HostBackdrop;
                    myBrush.TintColor = Windows.UI.Color.FromArgb(255, 200, 200, 200);
                    myBrush.TintOpacity = 0.2;

                    Background = myBrush;
                }
                else
                {
                    SolidColorBrush myBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 240, 240, 240));

                    Background = myBrush;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}