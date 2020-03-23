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
using Microsoft.Win32;
using Lab3lib;
using System.Collections.Specialized;

namespace WpfLab1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    //[ValueConversion(typeof(Activity), typeof(string))]
    public partial class MainWindow : Window
    {
        public ResearcherObservable RS = new ResearcherObservable();

        public Project P = new Project(new Activity("", new DateTime(2020, 2, 1), new DateTime(2020, 3, 1)));

        public MainWindow()
        {
            InitializeComponent();
             
            RS.Name = "John";
            RS.Surname = "Smith";
            
            this.DataContext = RS;

            this.Closing += Window_Closing;
        }

        public void FilterByProjects(object source, FilterEventArgs args) 
        {
            Activity tmp = args.Item as Activity;
            if (tmp != null && tmp is Project)
                args.Accepted = true;
            else
                args.Accepted = false;
        }

        /*private void Projects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Projects.ItemsSource = RS.Projects;
            //Projects.ItemsSource = RS;
            AllCollection.ItemsSource = RS;
        }

        private void AllCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Projects.ItemsSource = RS.Projects;
            AllCollection.ItemsSource = RS;
            //Projects.ItemsSource = RS;
        }*/
      
        private void AddDefProj(object sender, RoutedEventArgs e)
        {
            RS.AddDefaultProject();
        }

        private void AddCustom(object sender, RoutedEventArgs e)
        {
            InputWindow IN = new InputWindow();
            IN.Owner = this;
            IN.DataContext = P;
            IN.Closed += Child_Window_Closed;
            IN.OrgList.ItemsSource = RS.Worklist;
            IN.Show();

            P.theme = IN.Theme.Text;
            P.Name = (string)IN.OrgList.SelectedItem;
            P.count = Convert.ToInt32(IN.Count.Text);
            P.Start = IN.Start.DisplayDate;
            P.End = IN.End.DisplayDate;
   
        }

        public void Child_Window_Closed(object sender, EventArgs e)
        {
            if (P.theme != "" && P.count != 0)
                RS.Add((Project)P.DeepCopy());
        }

        private void AddDefCons(object sender, RoutedEventArgs e)
        {
            RS.AddDefaultConsulting();
            AllCollection.ItemsSource = RS;
        }

        private void AddDefaults(object sender, RoutedEventArgs e)
        {
            RS.AddDefault();
            AllCollection.ItemsSource = RS;
        }

        private void Remove(object sender, RoutedEventArgs e)
        {
           if (AllCollection.SelectedIndex >= 0 && AllCollection.SelectedIndex < RS.Count)
            {
                RS.RemoveActivityAt(AllCollection.SelectedIndex);
                
            }
        }

        private void New_click(object sender, RoutedEventArgs e)
        {
            if (RS.IsChanged == true)
            {
                const string message = "Выйти без сохранения?";
                if (MessageBox.Show(message, "Exit", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    SaveFileDialog dialogS = new SaveFileDialog();
                    if (dialogS.ShowDialog() == true)
                        ResearcherObservable.Save(dialogS.FileName, RS);
                }
            }
            AllCollection.ItemsSource = null;
            Projects.ItemsSource = null;
            RS = new ResearcherObservable();
            //RS.CollectionChanged += Handler_CollectionChanged;
        }

        /*private void Handler_CollectionChanged(object sender, NotifyCollectionChangedEventArgs arg)
        {
            //Projects.ItemsSource = RS.Projects;
            //Projects.ItemsSource = RS;
            //AllCollection.ItemsSource = RS;
        }*/

        private void Openfile_Click(object sender, RoutedEventArgs e) //меняться AmountOfWork
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)

            {
                if (RS.IsChanged == true)
                {
                    const string message = "Выйти без сохранения?";
                    MessageBoxResult res = MessageBox.Show(message, "Exit", MessageBoxButton.YesNo); //обрабатывать YesNO
                    if (res == MessageBoxResult.No)
                    {
                        SaveFileDialog dialogS= new SaveFileDialog();
                        if (dialogS.ShowDialog() == true)
                            ResearcherObservable.Save(dialogS.FileName, RS);
                    }
                }
                ResearcherObservable.Load(dialog.FileName, ref RS);
                this.DataContext = RS;
                RS.IsChanged = false;//десериализация
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == true)
            {
                ResearcherObservable.Save(dialog.FileName, RS);
                RS.IsChanged = false;
            }
        }

        private void Window_Closing(object sender, EventArgs e)
        {
            if (RS.IsChanged == true)
            {
                const string message = "Выйти без сохранения?";
                if (MessageBox.Show(message, "Exit", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    SaveFileDialog dialogS = new SaveFileDialog();
                    if (dialogS.ShowDialog() == true)
                        ResearcherObservable.Save(dialogS.FileName, RS);
                }
                RS.IsChanged = false;

            }
        }

        private void WOT_Checked(object sender, RoutedEventArgs e)
        {
            AllCollection.ItemTemplate = null;
        }

        private void WT_Checked(object sender, RoutedEventArgs e)
        {
            DataTemplate dataTemplate = this.TryFindResource("listTemplate") as DataTemplate;
            if (dataTemplate != null)
                AllCollection.ItemTemplate = dataTemplate;            
        }
    }




}
