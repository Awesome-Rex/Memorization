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
        //ppearance


        public ImageSource image{get { return (ImageSource)GetValue(imageProperty); }set { SetValue(imageProperty, value); }}
        public static readonly DependencyProperty imageProperty =DependencyProperty.Register("image", typeof(ImageSource), typeof(SplashScreen), new PropertyMetadata(0));


        public Point ImageSize
        {
            get
            {
                return new Point((image.Width * imageMultiply) + imageAdd, (image.Height * imageMultiply) + imageAdd);
            }
        }

        public Point imageOffset { get { return (Point)GetValue(imageOffsetProperty); } set { SetValue(imageOffsetProperty, value); } }
        public static readonly DependencyProperty imageOffsetProperty = DependencyProperty.Register("image", typeof(Point), typeof(SplashScreen), new PropertyMetadata(new Point(0, 0)));
        public double imageMultiply { get { return (double)GetValue(imageMultiplyProperty); } set { SetValue(imageMultiplyProperty, value); } }
        public static readonly DependencyProperty imageMultiplyProperty = DependencyProperty.Register("image", typeof(double), typeof(SplashScreen), new PropertyMetadata(1d));
        public double imageAdd { get { return (double)GetValue(imageAddProperty); } set { SetValue(imageAddProperty, value); } }
        public static readonly DependencyProperty imageAddProperty = DependencyProperty.Register("image", typeof(double), typeof(SplashScreen), new PropertyMetadata(1d));

        public Brush backgroundColour { get { return (Brush)GetValue(backgroundColourProperty); } set { SetValue(backgroundColourProperty, value); } }
        public static readonly DependencyProperty backgroundColourProperty = DependencyProperty.Register("image", typeof(Brush), typeof(SplashScreen), new PropertyMetadata(new SolidColorBrush(System.Windows.Media.Color.FromScRgb(0f, 0f, 0f, 1f))));
        
        //graph
        public double preRelease = 0d;

        public double attack = 2;
        public IEasingFunction attackEase;

        public double sustain = 3;

        public double decay = 2;
        public IEasingFunction decayEase;

        public double release = 1;

        public bool clickSkip = true;
        public double rushDecay = 0d;
        public IEasingFunction rushDecayEase;

        public SplashScreen next;

        //extra
        public bool sleepNext = true;

        private Thread currentThread;

        public SplashScreen()
        {
            InitializeComponent();
        }

        public void StartSplash()
        {
            DoubleAnimation anim;   //declaration


            Thread.Sleep(TimeSpan.FromSeconds(preRelease)); //pre release

            anim = new DoubleAnimation(0d, 1d, new Duration(TimeSpan.FromSeconds(attack))); //attack
            anim.EasingFunction = attackEase;

            Overlay.BeginAnimation(OpacityProperty, anim);

            Thread.Sleep(anim.Duration.TimeSpan);
            Overlay.BeginAnimation(OpacityProperty, null);

            Thread.Sleep(TimeSpan.FromSeconds(sustain));    //sustain

            anim = new DoubleAnimation(1d, 0d, new Duration(TimeSpan.FromSeconds(decay))); //decay
            anim.EasingFunction = decayEase;

            Overlay.BeginAnimation(OpacityProperty, anim);

            Thread.Sleep(anim.Duration.TimeSpan);
            Overlay.BeginAnimation(OpacityProperty, null);

            Overlay.Opacity = 0d;
            Thread.Sleep(TimeSpan.FromSeconds(release));    //release

            //ending
            Visibility = Visibility.Collapsed;

            Thread t = new Thread(next.StartSplash);
            t.Start();
            next.currentThread = t;
            /*if (sleepNext)
            {
                t.Join();
            }*/
        }

        public void skip ()
        {
            currentThread.Abort();

            DoubleAnimation anim;   //declaration

            anim = new DoubleAnimation(1d, 0d, new Duration(TimeSpan.FromSeconds(rushDecay))); //decay
            anim.EasingFunction = rushDecayEase;

            Overlay.BeginAnimation(OpacityProperty, anim);

            Thread.Sleep(anim.Duration.TimeSpan);
            Overlay.BeginAnimation(OpacityProperty, null);


            //ending
            Visibility = Visibility.Collapsed;

            Thread t = new Thread(next.StartSplash);
            t.Start();
            next.currentThread = t;
            /*if (sleepNext)
            {
                t.Join();
            }*/
        }
    }
}
