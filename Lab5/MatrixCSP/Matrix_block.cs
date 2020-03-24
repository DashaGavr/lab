using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Vector_block
{
    public int len = 3;
    public double[] vec;
    public Vector_block(double[] vals, int n = 3)
    {
        len = n;
        vec = new double[3];
        for (int i = 0; i < n; i++)
            vec[i] = vals[i];
    }
    public Vector_block(double r = 0)
    {
        len = 3;
        vec = new double[3] { r, r, r };
        
    }
    public Vector_block()
    {
        len = 3;
        vec = new double[3];
    }
    public Vector_block(Vector_block previous)
    {
        for (int i = 0; i < len; i++)
            vec[i] = previous.vec[i];
    }
    public static Vector_block operator +(Vector_block v1, Vector_block v2) // ЧЕЕЕЕЕЕЕЕЕЕЕГОООООО
    {
        int i;
        Vector_block res = new Vector_block();
        for (i = 0; i < 3; i++)
            res.vec[i] = v1.vec[i] + v2.vec[i];
        return res;
    }
    public static Vector_block operator -(Vector_block v1, Vector_block v2) // ЧЕЕЕЕЕЕЕЕЕЕЕГОООООО
    {
        int i;
        Vector_block res = new Vector_block();
        for (i = 0; i < 3; i++)
            res.vec[i] = v1.vec[i] - v2.vec[i];
        return res;
    }
    public double this[int i]
    {
        get
        {
            return this.vec[i];
        }
        set
        {
            this.vec[i] = value;
        }
    }
    public override string ToString()
    {
        string s = "{";
        for (int i = 0; i < len; i++)
            if (i != len - 1)
                s += vec[i].ToString() + " ";
            else
                s += vec[i].ToString();
        s += "}";
        return s;
    }

};

public class   Matrix_block
{
    public int n = 3;
    public double[,] array;
   
    public  Matrix_block()
    {
        n = 3;
        array = new double[3, 3];
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                array[i,j] = 0.0;
    }
    public  Matrix_block(double r)
    {
        n = 3;
        array = new double[3, 3];
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                array[i, j] = r;
    }
    public  Matrix_block(double[] r, bool flag)
    {
        n = 3;
        array = new double[3, 3];
        for (int i = 0; i < 3; i++)
            array[i, i] = r[i];
    }
    public  Matrix_block(double[] arr, int num)
    {
	    n = num;
	    array = new double[n, n];
	    for (int i = 0; i < n; i++) 
		    for (int j = 0; j < n; j++) 
                array[i, j] = arr[3 * i + j];
	}
    public  Matrix_block(double[,] arr, int num)
    {
        n = num;
        array = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                array[i, j] = arr[i, j];
    }
    public  Matrix_block(Matrix_block M): this(0.0)
    {
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                array[i, j] = M.array[i, j];
    }
    public  Matrix_block identity()
    {
        int i, j;
        Matrix_block NEW = new Matrix_block(0.0);
        for (i = 0; i < n; i++)
        {
            for (j = 0; j < n; j++)
            {
                NEW.array[i, i] = 1;
            }
        }
        return NEW;
    }
    public double this[int i, int j]
    {
        get { return this.array[i, j];  }
        set { this.array[i, j] = value; }
    }
    public void     transpose()
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                double tmp = array[i, j];
                array[i, j] = array[j, i];
                array[j, i] = tmp;
            }
        }
    }
    public          Matrix_block set(int i, int j, double val)
    {
        if (i > n || j > n)
            throw new System.IndexOutOfRangeException();
        array[i, j] = val;
        return (this);
    }
    public static   Matrix_block operator +(  Matrix_block m,   Matrix_block n)
    {
          Matrix_block RES = new  Matrix_block(0.0);
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                RES.array[i, j] = n.array[i, j] + m.array[i, j];
        return RES;
    }
    public static   Matrix_block operator -(  Matrix_block m,   Matrix_block n)
    {
          Matrix_block RES = new  Matrix_block(0.0);
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                RES.array[i, j] = m.array[i, j] - n.array[i, j];
        return RES;
    }
    public static   Vector_block operator *(  Matrix_block m, Vector_block v)
    {
        int i, j;
        Vector_block res = new Vector_block();
        if (m.n != v.len || m.n != v.len)
            throw new System.IndexOutOfRangeException();
        for (i = 0; i < m.n; i++)
        {
            for (j = 0; j < m.n; j++)
            {
                res[i] += m.array[i, j] * v[j];
            }
        }
        return res;
    }
    public static   Matrix_block operator *(  Matrix_block m1,   Matrix_block m2)
    {
        if (m1.n != m2.n)
            throw new System.IndexOutOfRangeException();
          Matrix_block res = new  Matrix_block();
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                for (int k = 0; k < 3; k++)
                    res.array[i, j] += m1.array[i, k] * m2.array[k, j];
        return res;
    }
    public static   Matrix_block operator *(  Matrix_block M,  double d)
    {
          Matrix_block Res = new Matrix_block(0.0);
        for (int i = 0; i < M.n; i++)
            for (int j = 0; j < M.n; j++)
                Res.array[i, j] = M.array[i, j] * d;
        return Res;
    }
    public double   det()
    {
        double x = array[0, 0] * array[1, 1] * array[2, 2] - array[0, 0] * array[1, 2] * array[2, 1]
      - array[0, 1] * array[1, 0] * array[2, 2] + array[0, 1] * array[1, 2] * array[2, 0]
      + array[0, 2] * array[1, 0] * array[2, 1] - array[0, 2] * array[1, 1] * array[2, 0];

        return x;
    }
    public double   det2()
    {
        return array[0, 0] * array[1, 1] - array[0, 1] * array[1, 0];
    }
    public          Matrix_block inverse()
    {
        double MD = this.det();
        Matrix_block inverse = new  Matrix_block(0.0);
        if (MD != 0.0)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int m = n - 1;
                    double[,] temp_matr = new double[m, m];
                    get_minor(this.array, temp_matr, i, j);
                    Matrix_block TMP = new Matrix_block(temp_matr, 2);
                    double D = TMP.det2();
                    inverse.array[j, i] = Math.Pow(-1.0, i + j + 2) * D / MD;
                }
            }
        }
        return (inverse);
    }
    void            get_minor(double[,] from, double[,] to, int indRow, int indCol)
    {
        int ki = 0;
        for (int i = 0; i < 3; i++)
        {
            if (i != indRow)
            {
                for (int j = 0, kj = 0; j < 3; j++)
                {
                    if (j != indCol)
                    {
                        to[ki,kj] = from[i,j];
                        kj++;
                    }
                }
                ki++;
            }
        }
    }
    public override string ToString()
    {
        string s = "";
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
                s += array[i, j] + " ";
            s += "\n";
        }
        return s;
    }

};

