using System;
using System.Collections.Generic;
using System.Text;
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

using System.Drawing;
using Color = System.Drawing.Color;
using Point = System.Windows.Point;
using System.Threading;

namespace Practice_Dojang.Resources.Views.UserControls
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : UserControl
    {
        public ImageSource image;
        public Point imageOffset = new Point(0d, 0d);

        public Color backgroundColour = Color.Black;

        public double preRelease = 0d;

        public double attack = 2;
        public IEasingFunction attackEase;

        public double sustain = 3;

        public double decay = 2;
        public IEasingFunction decayEase;

        public double release = 1;

        public bool clickSkip = true;
        public double rushDecay = 0d;

        public SplashScreen next;

        public SplashScreen()
        {
            InitializeComponent();
        }

        public void StartSplash()
        {
            DoubleAnimation anim;

            Thread.Sleep(TimeSpan.FromSeconds(preRelease));

            anim = new DoubleAnimation(0d, 1d, new Duration(TimeSpan.FromSeconds(attack)));
            anim.EasingFunction = attackEase;

            //anim.Completed

            Overlay.BeginAnimation(OpacityProperty, anim);

            //Overlay.

            //((Frame)ContentOperations.GetParent(this)).Navigate(next);
        }
    }
}
