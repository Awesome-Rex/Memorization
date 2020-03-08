using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace Practice_Dojang.Resources.Views.UserControls
{
    /// <summary>
    /// Interaction logic for TrainingCommandView.xaml
    /// </summary>
    public partial class TrainingCommandView : UserControl
    {
        public ITF_Res.Command source;
        public int index
        {
            get
            {
                return ((Panel)Parent).Children.IndexOf(this);
            }
            set
            {
                TrainingCommandView clone = EXT.TClone<TrainingCommandView>(this);

                ((Panel)Parent).Children.Insert(value, clone);

                ((Panel)Parent).Children.Remove(this);
            }
        }

        public TrainingCommandView()
        {
            InitializeComponent();
        }
    }
}
