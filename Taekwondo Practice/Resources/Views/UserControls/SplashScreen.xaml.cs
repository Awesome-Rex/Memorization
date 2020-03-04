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
        //appearance

        /*public ImageSource image{get { return (ImageSource)GetValue(imageProperty); }set { SetValue(imageProperty, value); }}
        public static readonly DependencyProperty imageProperty =DependencyProperty.Register("image", typeof(ImageSource), typeof(SplashScreen), new PropertyMetadata(0));


        public Point ImageSize
        {
            get
            {
                return new Point((image.Width * imageMultiply) + imageAdd.X, (image.Height * imageMultiply) + imageAdd.Y);
            }
        }

        public Point imageOffset { get { return (Point)GetValue(imageOffsetProperty); } set { SetValue(imageOffsetProperty, value); } }
        public static readonly DependencyProperty imageOffsetProperty = DependencyProperty.Register("image", typeof(Point), typeof(SplashScreen), new PropertyMetadata(new Point(0, 0)));
        public double imageMultiply { get { return (double)GetValue(imageMultiplyProperty); } set { SetValue(imageMultiplyProperty, value); } }
        public static readonly DependencyProperty imageMultiplyProperty = DependencyProperty.Register("image", typeof(double), typeof(SplashScreen), new PropertyMetadata(1d));
        public Point imageAdd { get { return (Point)GetValue(imageAddProperty); } set { SetValue(imageAddProperty, value); } }
        public static readonly DependencyProperty imageAddProperty = DependencyProperty.Register("image", typeof(Point), typeof(SplashScreen), new PropertyMetadata(1d));
        */
        /*public Brush backgroundColour { get { return (Brush)GetValue(backgroundColourProperty); } set { SetValue(backgroundColourProperty, value); } }
        public static readonly DependencyProperty backgroundColourProperty = DependencyProperty.Register("image", typeof(Brush), typeof(SplashScreen), new PropertyMetadata(new SolidColorBrush(System.Windows.Media.Color.FromScRgb(0f, 0f, 0f, 1f))));
        */
        //graph
        /*public Object Contents { get { return (Object)GetValue(ContentsProperty); } set { SetValue(ContentsProperty, value); } }
        public static readonly DependencyProperty ContentsProperty = DependencyProperty.Register("Content", typeof(Object), typeof(SplashScreen), new PropertyMetadata(new Grid()));
        */

        public double PreRelease { get { return (double)GetValue(preReleaseProperty); } set { SetValue(preReleaseProperty, value); } }
        public static readonly DependencyProperty preReleaseProperty = DependencyProperty.Register("PreRelease", typeof(double), typeof(SplashScreen), new PropertyMetadata(0d));

        public double Attack { get { return (double)GetValue(attackProperty); } set { SetValue(attackProperty, value); } }
        public static readonly DependencyProperty attackProperty = DependencyProperty.Register("Attack", typeof(double), typeof(SplashScreen), new PropertyMetadata(2d));
        public IEasingFunction AttackEase { get { return (IEasingFunction)GetValue(attackEaseProperty); } set { SetValue(attackEaseProperty, value); } }
        public static readonly DependencyProperty attackEaseProperty = DependencyProperty.Register("AttackEase", typeof(IEasingFunction), typeof(SplashScreen), new PropertyMetadata(new QuarticEase()));

        public double Sustain { get { return (double)GetValue(sustainProperty); } set { SetValue(sustainProperty, value); } }
        public static readonly DependencyProperty sustainProperty = DependencyProperty.Register("Sustain", typeof(double), typeof(SplashScreen), new PropertyMetadata(3d));

        public double Decay { get { return (double)GetValue(decayProperty); } set { SetValue(decayProperty, value); } }
        public static readonly DependencyProperty decayProperty = DependencyProperty.Register("Decay", typeof(double), typeof(SplashScreen), new PropertyMetadata(2d));
        public IEasingFunction DecayEase { get { return (IEasingFunction)GetValue(decayEaseProperty); } set { SetValue(decayEaseProperty, value); } }
        public static readonly DependencyProperty decayEaseProperty = DependencyProperty.Register("DecayEase", typeof(IEasingFunction), typeof(SplashScreen), new PropertyMetadata(new QuarticEase()));

        public double Release { get { return (double)GetValue(releaseProperty); } set { SetValue(releaseProperty, value); } }
        public static readonly DependencyProperty releaseProperty = DependencyProperty.Register("Release", typeof(double), typeof(SplashScreen), new PropertyMetadata(1d));

        public bool ClickSkip { get { return (bool)GetValue(clickSkipProperty); } set { SetValue(clickSkipProperty, value); } }
        public static readonly DependencyProperty clickSkipProperty = DependencyProperty.Register("ClickSkip", typeof(bool), typeof(SplashScreen), new PropertyMetadata(true));
        public double RushDecay { get { return (double)GetValue(rushDecayProperty); } set { SetValue(rushDecayProperty, value); } }
        public static readonly DependencyProperty rushDecayProperty = DependencyProperty.Register("RushDecay", typeof(double), typeof(SplashScreen), new PropertyMetadata(0.4d));
        public IEasingFunction RushDecayEase { get { return (IEasingFunction)GetValue(rushDecayEaseProperty); } set { SetValue(rushDecayEaseProperty, value); } }
        public static readonly DependencyProperty rushDecayEaseProperty = DependencyProperty.Register("RushDecayEase", typeof(IEasingFunction), typeof(SplashScreen), new PropertyMetadata(new QuarticEase()));

        public SplashScreen Next { get { return (SplashScreen)GetValue(nextProperty); } set { SetValue(nextProperty, value); } }
        public static readonly DependencyProperty nextProperty = DependencyProperty.Register("Next", typeof(SplashScreen), typeof(SplashScreen), new PropertyMetadata(null));

        //extra
        //public bool sleepNext = true;

        private Thread currentThread;

        public SplashScreen()
        {
            InitializeComponent();
        }

        public void StartSplash()
        {
            DoubleAnimation anim;   //declaration


            Thread.Sleep(TimeSpan.FromSeconds(PreRelease)); //pre release

            anim = new DoubleAnimation(0d, 1d, new Duration(TimeSpan.FromSeconds(Attack))); //attack
            anim.EasingFunction = AttackEase;

            Overlay.BeginAnimation(OpacityProperty, anim);

            Thread.Sleep(anim.Duration.TimeSpan);
            Overlay.BeginAnimation(OpacityProperty, null);

            Thread.Sleep(TimeSpan.FromSeconds(Sustain));    //sustain

            anim = new DoubleAnimation(1d, 0d, new Duration(TimeSpan.FromSeconds(Decay))); //decay
            anim.EasingFunction = DecayEase;

            Overlay.BeginAnimation(OpacityProperty, anim);

            Thread.Sleep(anim.Duration.TimeSpan);
            Overlay.BeginAnimation(OpacityProperty, null);

            Overlay.Opacity = 0d;
            Thread.Sleep(TimeSpan.FromSeconds(Release));    //release

            //ending
            Visibility = Visibility.Collapsed;

            Thread t = new Thread(Next.StartSplash);
            t.Start();
            Next.currentThread = t;
            /*if (sleepNext)
            {
                t.Join();
            }*/
        }

        public void skip ()
        {
            currentThread.Abort();

            DoubleAnimation anim;   //declaration

            anim = new DoubleAnimation(1d, 0d, new Duration(TimeSpan.FromSeconds(RushDecay))); //decay
            anim.EasingFunction = RushDecayEase;

            Overlay.BeginAnimation(OpacityProperty, anim);

            Thread.Sleep(anim.Duration.TimeSpan);
            Overlay.BeginAnimation(OpacityProperty, null);


            //ending
            Visibility = Visibility.Collapsed;

            Thread t = new Thread(Next.StartSplash);
            t.Start();
            Next.currentThread = t;
            /*if (sleepNext)
            {
                t.Join();
            }*/
        }
    }
}
