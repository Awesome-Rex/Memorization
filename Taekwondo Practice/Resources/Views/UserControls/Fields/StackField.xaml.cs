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

namespace Practice_Dojang.Resources.Views.UserControls.Fields
{
    /// <summary>
    /// Interaction logic for StackField.xaml
    /// </summary>
    public partial class StackField : UserControl
    {
        public double StackSpace { get { return (double)GetValue(StackHeightProperty); } set { SetValue(StackHeightProperty, value); } }
        public static readonly DependencyProperty StackHeightProperty = DependencyProperty.Register("StackHeight", typeof(double), typeof(StackField), new PropertyMetadata(3));


        public bool FieldOverflow { get { return (bool)GetValue(FieldOverflowProperty); } set { SetValue(FieldOverflowProperty, value); } }
        public static readonly DependencyProperty FieldOverflowProperty = DependencyProperty.Register("FieldOverflow", typeof(bool), typeof(StackField), new PropertyMetadata(false));


        public StackField()
        {
            InitializeComponent();
        }
    }
}
