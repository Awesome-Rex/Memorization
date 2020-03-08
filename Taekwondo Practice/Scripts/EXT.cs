using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;

namespace Practice_Dojang
{
    public static class EXT
    {
        public static UIElement UIbase(this UIElement origin)
        {
            return origin;
        }

        public static T TClone<T>(this T origin)
        {
            string xaml = XamlWriter.Save(origin);

            StringReader stringReader = new StringReader(xaml);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            T newElement = (T)XamlReader.Load(xmlReader);

            return newElement;
        }
    }
}



