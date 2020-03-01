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
using Practice_Dojang.Resources;
using System.Threading;

namespace Practice_Dojang.Resources.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Subject.Navigate(new Practice_Dojang.Resources.Views.Pages.Training());
            MenuBar.Navigate(new Practice_Dojang.Resources.Views.Pages.MenuBar());
        }
    }
}
