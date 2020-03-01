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
using Taekwondo_Practice.Resources;
using System.Threading;

namespace Taekwondo_Practice.Resources.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Subject.Navigate(new Taekwondo_Practice.Resources.Views.Pages.Training());
            MenuBar.Navigate(new Taekwondo_Practice.Resources.Views.Pages.MenuBar());
        }
    }
}
