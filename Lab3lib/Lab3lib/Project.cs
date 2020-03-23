using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3lib
{
    [Serializable]
    public class Project : Activity
    {
        public Project(Activity A, string topic = "", int count_part = 0) : base(A.Name, A.Start, A.End)
        {
            theme = topic;
            count = count_part;
        }

        public int count { get; set; }

        public string theme { get; set; }

        public override Object DeepCopy()
        {
            Activity tmp_A = new Activity(this.Name, this.Start, this.End);
            Project tmp_P = new Project(tmp_A, this.theme, this.count);
            return tmp_P;
        }

        public override string ToString()
        {
            return "Project: theme = " + theme + ",  participants = " + count.ToString() + " " + base.ToString();
        }
    }
}
