using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Numerics;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace Taekwondo_Practice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point initialMouseClick;
        double initialValue;
        Rectangle draggedControl;

        Dictionary<string, float> previousValues;

        public MainWindow()
        {
            InitializeComponent();

            initialMouseClick = new Point(0, 0);
            draggedControl = null;

            previousValues = new Dictionary<string, float>();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            initialMouseClick = Mouse.GetPosition(this);
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (draggedControl != null && e.LeftButton == MouseButtonState.Pressed)
            {
                if (draggedControl == ExposeSidePanel)
                {
                    SidePanel.Width = Math.Clamp(initialValue + (e.GetPosition(this) - initialMouseClick).X, 0, 400);
                    Console.WriteLine((initialMouseClick - e.GetPosition(this)).X.ToString());
                }
            } else if (draggedControl != null && e.LeftButton == MouseButtonState.Released)
            {
                initialMouseClick = new Point();
                draggedControl = null;
                initialValue = 0;
            }
        }

        private void ExposeSidePanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            draggedControl = ExposeSidePanel;
            initialMouseClick = Mouse.GetPosition(this);
            initialValue = SidePanel.Width;

            SidePanel.BeginAnimation(Grid.WidthProperty, null);
        }

        private void ExposeSidePanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Vector2.Distance(new Vector2((float)initialMouseClick.X, (float)initialMouseClick.Y), new Vector2((float)e.GetPosition(this).X, (float)e.GetPosition(this).Y)) < 10)
            {
                if (SidePanel.Width > 0)
                {
                    float x;

                    if (previousValues.TryGetValue(SidePanel.Name + "_w", out x))
                    {
                        previousValues.Remove(SidePanel.Name + "_w");
                    }
                    previousValues.Add(SidePanel.Name + "_w", (float)SidePanel.Width);
                    //SidePanel.Width = 0d;

                    DoubleAnimation anim = new DoubleAnimation();
                    anim.From = SidePanel.Width;
                    anim.To = 0d;
                    anim.Duration = TimeSpan.FromSeconds(0.1);
                    anim.AutoReverse = false;
                    anim.EasingFunction = new QuarticEase();

                    anim.Freeze();
                    SidePanel.BeginAnimation(Grid.WidthProperty, anim);

                }
                else if (SidePanel.Width == 0)
                {
                    float x;
                    previousValues.TryGetValue(SidePanel.Name + "_w", out x);

                    if (x > 0) {
                        DoubleAnimation anim = new DoubleAnimation();
                        anim.From = 0d;
                        anim.To = x;
                        anim.Duration = TimeSpan.FromSeconds(0.1);
                        anim.AutoReverse = false;
                        anim.EasingFunction = new QuarticEase();

                        anim.Freeze();
                        SidePanel.BeginAnimation(Grid.WidthProperty, anim);
                    } else if (x == 0d)
                    {
                        DoubleAnimation anim = new DoubleAnimation();
                        anim.From = 0d;
                        anim.To = 300d;
                        anim.Duration = TimeSpan.FromSeconds(0.1);
                        anim.AutoReverse = false;
                        anim.EasingFunction = new QuarticEase();

                        anim.Freeze();
                        SidePanel.BeginAnimation(Grid.WidthProperty, anim);
                    }
                    
                    previousValues.Remove(SidePanel.Name + "_w");
                }
            }
        }
    }
}
