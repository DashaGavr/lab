using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3lib
{
    [Serializable]
    public class Activity : IDeepCopy
    {
        public string Name { get; set; }
        private DateTime[] time { get; set; } = new DateTime[2];
        public DateTime Start
        {
            get
            {
                return time[0];
            }
            set
            {
                time[0] = value;
            }
        }
        public DateTime End
        {
            get
            {
                return time[1];
            }
            set
            {
                time[1] = value;
            }
        }
        public String shortStart
        {
            get
            {
                return Start.ToShortDateString();
            }
        }
        public String shortEnd
        {
            get
            {
                return End.ToShortDateString();
            }
        }

        public Activity(string name = "", DateTime begin = new DateTime(), DateTime end = new DateTime())
        {
            Name = name;
            time[0] = begin;
            time[1] = end;
        }

        public override string ToString()
        {
            return Name + "  " + "begin: " + time[0].ToShortDateString() + ",  " + "end: " + time[1].ToShortDateString();
        }
        /*public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
                return false;
            else
            {
                Activity A = (Activity)obj;
                return ((A.Name == this.Name) && (A.time[0] == time[0]) && (A.time[1] == time[1]));
            }
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode() + time[0].GetHashCode() + time[1].GetHashCode();
        }
        public static bool operator ==(Activity A, Activity B)
        {
            if (ReferenceEquals(A, null) || ReferenceEquals(B, null))
                return ReferenceEquals(A, B);
            return A.Equals(B);
        }
        public static bool operator !=(Activity A, Activity B)
        {
            if (ReferenceEquals(A, null) || ReferenceEquals(B, null))
                return !ReferenceEquals(A, B);
            return !(A.Equals(B));
        }*/

        public virtual Object DeepCopy()
        {
            Activity tmp = new Activity(this.Name, this.time[0], this.time[1]);
            return tmp;
        }
    }
}
