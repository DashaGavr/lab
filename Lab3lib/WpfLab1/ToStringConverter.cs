using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Lab3lib;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfLab1
{
    [ValueConversion(typeof(Activity), typeof(string))]

    public class ToStringConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Activity tmp = value as Activity;
            string s = "";
            try
            {
                if (tmp == null)
                    throw new NullReferenceException();
                s += tmp.Name;
                if (tmp is Project)
                    s += " is Project ";
                else if (tmp is Consulting)
                    s += " is Consulting ";
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return s;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
