using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Taekwondo_Practice.Resources.Scripts.Views.UserControls.SettingFields
{

    public partial class TextCombo : UserControl
    {
        public Label Info
        {
            get { return (Label)GetValue(InfoProperty); }
            set { SetValue(InfoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for label.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InfoProperty =
            DependencyProperty.Register("Text", typeof(Label), typeof(TextCombo));



        public ComboBox Box
        {
            get { return (ComboBox)GetValue(BoxProperty); }
            set { SetValue(BoxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for combo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BoxProperty =
            DependencyProperty.Register("Box", typeof(ComboBox), typeof(TextCombo));

        public TextCombo()
        {
            InitializeComponent();

            this.DataContext = this;
        }
    }
}
