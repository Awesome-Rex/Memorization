using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Taekwondo_Practice.Resources.Views.Pages
{
    /// <summary>
    /// Interaction logic for MenuBar.xaml
    /// </summary>
    public partial class MenuBar : Page
    {
        public MenuBar()
        {
            InitializeComponent();

            MainMenuBar.Height = 0d;
        }

        private void MainMenuBar_MouseEnter(object sender, MouseEventArgs e)
        {
            DoubleAnimation anim = new DoubleAnimation(35d, new Duration(TimeSpan.FromSeconds(0.2d)));
            anim.EasingFunction = new ExponentialEase();
            MainMenuBar.BeginAnimation(HeightProperty, anim);
        }

        private void MainMenuBar_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation anim = new DoubleAnimation(0d, new Duration(TimeSpan.FromSeconds(0.2d)));
            anim.EasingFunction = new ExponentialEase();

            if (MainMenuBar.Height == 35d) {
                Thread.Sleep(TimeSpan.FromSeconds(0.5d));
            }
            MainMenuBar.BeginAnimation(HeightProperty, anim);
        }
    }
}
