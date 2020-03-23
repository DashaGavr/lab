using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3lib
{
    [Serializable]
    public class Consulting : Activity, IDeepCopy
    {
        public bool is_international { get; set; }
        public double exchange { get; set; }

        public Consulting(Activity A, bool international = false, double change = 0.0) : base(A.Name, A.Start, A.End)
        {
            is_international = international;
            exchange = change;
        }
        public override string ToString()
        {
            return "Consulting: is international " + is_international.ToString() + ",  exchange = " + exchange.ToString();
        }
        public override Object DeepCopy()
        {
            Activity tmp_A = new Activity(this.Name, Start, End);
            Consulting tmp_C = new Consulting(tmp_A, this.is_international, this.exchange);
            return tmp_C;
        }
    }
}
