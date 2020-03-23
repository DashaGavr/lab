using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;
using System.ComponentModel;
using System.Linq;
using System.Windows;


namespace Lab3lib
{
    [Serializable]
    
    public class ResearcherObservable : System.Collections.ObjectModel.ObservableCollection<Activity> , INotifyPropertyChanged
    {
        public ResearcherObservable(string name = "")
        {
            Name = name;
            list = new List<string>(3);
            list.Add("New_Work_1");
            list.Add("New_Work_2");
            list.Add("New_Work_3");
            IsChanged = false;
            this.CollectionChanged += CollectionChangedEventHandler;
        }

        private int amountOfWork;

        private List<string> list;

        private bool isChanged;

        [field: NonSerialized] override protected event PropertyChangedEventHandler PropertyChanged;
        
        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void CollectionChangedEventHandler(object sender, NotifyCollectionChangedEventArgs ar)
        {
            IsChanged = true;
        }

        public string Name { get; set; }

        public string Surname { get; set; }

        public bool IsChanged { 
            get 
            {
                return isChanged;
            }
            set 
            {
                isChanged = value;
                OnPropertyChanged("IsChanged");
                OnPropertyChanged("AmountOfWork");
            }
        }

        public IEnumerable<Project> Projects
        {
            get
            {
                return from x in base.Items
                       where x is Project
                       select x as Project;
            }
        }

        public List<string> Worklist
        {
            get
            {
                return list;
            }
        }

        public int AmountOfWork
        {
            get
            {
                int i = 0;
                foreach (Activity tmp in this)
                    if (tmp is Project)
                        i++;
                return i;
            }
            set
            {
                amountOfWork = value;               
                //OnPropertyChanged("AmountOfWork");
            }
        }

        public void AddActivity(params Activity[] items)
        {
            int i;
            foreach (Activity A in items)
            {
                for (i = 0; i < this.Count; i++) //сравнивать данные , ввести IComparer
                {
                    if (!this[i].Equals(A))
                        this.Add(A);
                }
            }
        }

        public void RemoveActivityAt(int index)
        {
            //if (index < this.Count)
            //    this.RemoveAt(index);
            try
            {
                this.RemoveAt(index);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AddDefault()
        {
            Activity[] A = new Activity[2] { new Activity("Work_1", new DateTime(2000, 1, 1), new DateTime(2019, 10, 22)), new Activity("Work_2", new DateTime(2000, 1, 1), new DateTime(2019, 10, 22)) };
            Consulting[] S = new Consulting[2] { new Consulting(A[0], true), new Consulting(A[1], false, 944.78) };
            Project[] P = new Project[2] { new Project(A[0], "Project_1", 3), new Project(A[1], "Project_2", 1) };
            foreach (Activity tmp in A)
                this.Add(tmp);
            foreach (Activity tmp in S)
                this.Add(tmp);
            foreach (Activity tmp in P)
                this.Add(tmp);
            amountOfWork += 2;
            
        }

        public void AddDefaultConsulting()
        {
            Activity A = new Activity("Consalting_TMP", new DateTime(2000, 1, 1), new DateTime(2019, 10, 22));
            Consulting S = new Consulting(A);
            this.Add(S);
        }

        public void AddDefaultProject()
        {
            Activity A = new Activity("Project_TMP", new DateTime(2000, 1, 1), new DateTime(2019, 10, 22));
            Project P = new Project(A, "My first WPF", 1);
            this.Add(P);
            amountOfWork += 2;
        }

        public override string ToString()
        {
            string s = this.Name + this.Surname;
            int i = 1;
            foreach (Activity A in this)
            {
                s = s + "№ " + i.ToString() + " " + A.ToString() + "\n";        //итерационный номер i
                i++;
            }
            return s;
        }

        public new Activity this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                base[index] = value;
            }
        }

        public static bool Save(string filename, ResearcherObservable obj)
        {
            bool f = true;
            FileStream fs = null;
            try
            {
                fs = File.Open(filename, FileMode.OpenOrCreate);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, obj);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //Console.WriteLine(ex.Message);
                f = false;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
            return f;
        }

        public static bool Load(string filename, ref ResearcherObservable obj)
        {
            bool f = true;
            FileStream fs = null;
            try
            {
                fs = File.OpenRead(filename);
                BinaryFormatter bf = new BinaryFormatter();
                obj = bf.Deserialize(fs) as ResearcherObservable;
                obj.CollectionChanged += obj.CollectionChangedEventHandler;
                
            }
            catch (Exception ex) //  мб другая обработка
            {
                MessageBox.Show(ex.Message);
                //Console.WriteLine(ex.Message);
                //obj = new ResearcherObservable();
                f = false;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
            return f;

        }

        /*public Object DeepCopy()
            {
                ResearcherObservable tmp_R = new ResearcherObservable(this.Name);
                tmp_R.lst = new List<Activity> (lst.Count);
                foreach (Activity L in lst)
                    tmp_R.lst.Add((Activity)L.DeepCopy()); 
                return tmp_R;
            }*/
    }
}
    