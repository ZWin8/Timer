using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;



namespace Timer
{

    public sealed partial class MainPage : Page
    {
        double txtBlkBaseSize;
        public MainPage()
        {
            this.InitializeComponent();
            Loaded += MainPage_Loaded;   
         }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            txtBlkBaseSize = TxtRender2.ActualHeight;
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        void CompositionTarget_Rendering(object sender, object e)
        {
            
            // The timer rendering
            RenderingEventArgs ranArgs = e as RenderingEventArgs;
            double t = (0.25 * ranArgs.RenderingTime.TotalSeconds) % 1;
            t = t < 0.5 ? 2 * t : 2 - 2 * t; 
            TxtRender.FontSize = 1 + 143 * t;
            byte gray = (byte)(255 * t);
            TxtRender.Foreground = new SolidColorBrush(Color.FromArgb(255, gray, gray, gray));
            // Another way to render
            byte gray_r = (byte) (255 - gray);
            GridBrush.Color = Color.FromArgb(255, gray_r, gray_r, gray_r);
            //
            TxtRender2.FontSize = this.ActualHeight / txtBlkBaseSize;
            TxtRender2.Opacity = 0.3;
            foreach (GradientStop gs in LGBrush.GradientStops)
            {
                gs.Offset = LGBrush.GradientStops.IndexOf(gs) / 4.0 - t;
            }
            
        }

        private void onTick(object sender, object e)
        {
            TxtTimer.Text = "Time is : " + DateTime.Now.ToString("h:mm:ss tt");
            // TxtTimer.FontSize += 0.2;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DispatcherTimer m_timer = new DispatcherTimer();
            m_timer.Start();
            m_timer.Tick += onTick;
        }
    }
}
