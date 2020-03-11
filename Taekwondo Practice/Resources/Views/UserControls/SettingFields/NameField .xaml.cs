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

namespace Practice_Dojang.Resources.Scripts.Views.UserControls.SettingFields
{

    public partial class NameField : UserControl
    {
        public String FieldName
        {
            get { return (String)GetValue(FieldNameProperty); }
            set { SetValue(FieldNameProperty, value); }
        }
        public static readonly DependencyProperty FieldNameProperty = DependencyProperty.Register("FieldName", typeof(String), typeof(NameField), new PropertyMetadata("New LineField"));



        public UIElement Field
        {
            get { return (UIElement)GetValue(FieldProperty); }
            set { SetValue(FieldProperty, value); }
        }
        public static readonly DependencyProperty FieldProperty = DependencyProperty.Register("Field", typeof(UIElement), typeof(NameField), new PropertyMetadata(null));


        public GridLength NameWidth { get { return (GridLength)GetValue(NameWidthProperty); } set { SetValue(NameWidthProperty, value); } }
        public static readonly DependencyProperty NameWidthProperty = DependencyProperty.Register("NameWidth", typeof(GridLength), typeof(NameField), new PropertyMetadata(new GridLength(1, GridUnitType.Star)));

        public GridLength FieldWidth { get { return (GridLength)GetValue(FieldWidthProperty); } set { SetValue(FieldWidthProperty, value); } }
        public static readonly DependencyProperty FieldWidthProperty = DependencyProperty.Register("FieldWidth", typeof(GridLength), typeof(NameField), new PropertyMetadata(new GridLength(2, GridUnitType.Star)));


        public bool FieldOverflow { get { return (bool)GetValue(FieldOverflowProperty); } set { SetValue(FieldOverflowProperty, value); } }
        public static readonly DependencyProperty FieldOverflowProperty = DependencyProperty.Register("FieldOverflow", typeof(bool), typeof(NameField), new PropertyMetadata(false));

        public NameField()
        {
            InitializeComponent();

        }
    }
}
