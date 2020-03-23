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
using System.Windows.Shapes;
using Microsoft.Win32;
using Lab3lib;

namespace WpfLab1
{
    /// <summary>
    /// Логика взаимодействия для InputWindow.xaml
    /// </summary>
    public partial class InputWindow : Window
    {
        public InputWindow()
        {
            InitializeComponent();
            
        }


        private void OK_Click(object sender, RoutedEventArgs e) //добавляется
        {
            /*((Project)this.DataContext).theme = Theme.Text;
            ((Project)this.DataContext).count = Convert.ToInt32(Count.Text);;
            ((Project)this.DataContext).Name = (string)OrgList.SelectedItem;
            ((Project)this.DataContext).Start = Start.DisplayDate;
            ((Project)this.DataContext).End = End.DisplayDate;*/
            this.Close();
        }

        private void Cansel_Click(object sender, RoutedEventArgs e) //не добавляется
        {
            this.Close();
        }


    }
    
}
