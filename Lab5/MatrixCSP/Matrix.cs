using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class TestTime
{
    public List<string> info { get; set; }
    public TestTime ()
    {
        info = new List<string>();
    }
    public void Add(string record)
    {
        if (record != null)
            info.Add(record);
    }
    /*public void GetObjectData(SerializationInfo inform, StreamingContext c)
    {
        if (k = infstring = inf
        inform.AddValue("time", info, typeof(List<string>));
    }
    public TestTime(SerializationInfo inform, StreamingContext Cont)
    {
        info = inform.GetString("time");
    }*/
    public static bool Save(string filename, TestTime obj)
    {
        bool f = true;
        FileStream fs = null;
        try
        {
            fs = File.Open(filename, FileMode.OpenOrCreate);      
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, obj.info);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            f = false;
        }
        finally
        {
            if (fs != null) fs.Close();
        }
        return f;
    }
    public static bool Load(string filename, ref TestTime obj)
    {
        bool f = true;
        FileStream fs = null;
        try
        {
            fs = File.OpenRead(filename); 
            BinaryFormatter bf = new BinaryFormatter();
            obj = bf.Deserialize(fs) as TestTime;

        }
        catch (FileNotFoundException ex)
        {
            fs = File.Create(filename);
            obj.info = new List<string>();
            //Console.WriteLine(ex.Message);
            f = false;
        }
        finally
        {
            if (fs != null) fs.Close();
        }
        return f;

    }
    public override string ToString()
    {
        string s = "";
        for (int i = 0; i < info.Count; i++)
            s += info[i] + "\n";
        return s;
    }
}

public class Vector
{
    public int LEN;
    public int len;
    public Vector_block[] v;

    public Vector(int n, bool t)
    {
        LEN = n;
        len = 3 * LEN;
        v = new Vector_block[n];
        double[] tmp1 = new double[3] { 20, 28, 39 };
        double[] tmp2 = new double[3] { 36, 49, 41 };
        double[] tmp3 = new double[3] { 15, 35, 26 };
        if (t)
        {
            for (int i = 0; i < n; i++)
            {
                v[i] = new Vector_block((i % 2 == 0 ? tmp1 : tmp2), 3);
            }
        }
        else
        {
            v[0] = new Vector_block(tmp1);
            v[1] = new Vector_block(tmp2);
            v[2] = new Vector_block(tmp3);
        }
    }

    public Vector(int n, double r = 0)
    {
        LEN = n;
        len = 3 * LEN;
        v = new Vector_block[n];
        for (int i = 0; i < n; i++)
        {
            v[i] = new Vector_block(r);
        }
    }

    public Vector(int n, double[] val)
    {
        LEN = n;
        len = 3 * LEN;
        v = new Vector_block[LEN];

        for (int i = 0; i < n; i++) {
            v[i] = new Vector_block();
            for (int j = 0; j < 3; j++)
            {
                v[i].vec[j] = val[3 * i + j];

            }
        }
    }

    public Vector(Vector V)
    {
        LEN = V.len;
        len = 3 * V.LEN;
        Array.Clear(v, 0, len);
        v = new Vector_block[V.LEN];
        for (int i = 0; i < V.LEN; i++)
        {
            v[i] = new Vector_block(V.v[i]);
        }
    }

    public double[] ToDouble()
    {
        double[] tmp = new double[len];
        for (int i = 0; i < LEN; i++)
            for (int j = 0; j < 3; j++)
                tmp[3 * i + j] = v[i].vec[j];
        
        return tmp;
        
    }
       
    //double operator()(const Vector& V, const Vector& M); //скалярное произведение

	public Vector_block this[int i]
    {
        get { if (i < LEN) return this.v[i];
            else throw new System.IndexOutOfRangeException();
            }
        set { this.v[i] = value; }
    }

    public static Vector operator+(Vector v1, Vector v2)
    {
        Vector res = new Vector(v1.len, 0.0);
        for (int i = 0; i < v1.LEN; i++)
        {
            res[i] = v1[i] + v2[i];
        }
        return res;
    }

    public static Vector operator -(Vector v1, Vector v2)
    {
        Vector res = new Vector(v1.len, 0.0);
        for (int i = 0; i < v1.LEN; i++)
        {
            res[i] = v1[i] - v2[i];
        }
        return res;
    }

    public override string ToString()
    {
        string s = "";
        for (int i = 0; i < LEN; i++)
            s += v[i].ToString() + "\n";
        return s;
    }

    public void save(string s, string Name)
    {
        FileStream f = null;
        string docPath = Environment.CurrentDirectory;
        try
        {
            f = new FileStream(Path.Combine(docPath, s), FileMode.Append, FileAccess.Write);
            using (StreamWriter writer = new StreamWriter(f))
            {
                writer.WriteLine(Name + "\n" + this.ToString()+ "\n" );
                
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            if (f != null) f.Close();
        }
    }

};

public class Matrix
{
    public Matrix_block[] A;
    public Matrix_block[] B;
    public Matrix_block[] C;
    public int n;
    public int N;

    public Matrix()
    {
        n = 9;
        N = 3;
        double[] d1 = new double[] { 9, 2, 7, 18, 8, 21, 6, 8, 1 };
        double[] d2 = new double[] { 0, 5, 11, 5, 4, 5, 12, 14, 21 };
        double[] d3 = new double[] { 1, 9, 0, 12, 7, 1, 16, 10, 12 };
        double[] d4 = new double[] { 16, 6, 7, 13, 12, 9, 10, 21, 2 };
        double[] d5 = new double[] { 1, 11, 11, 7, 7, 8, -1, 21, 14 };
        double[] d6 = new double[] { 0, 7, 7, 5, 19, 17, 7, 3, 18 };
        double[] d7 = new double[] { 1, 9, 7, 6, 15, 7, -6, 17, 7 };
        A = new Matrix_block[2] { new Matrix_block(d3, 3), new Matrix_block(d6, 3) };
        B = new Matrix_block[3] { new Matrix_block(d1, 3), new Matrix_block(d4, 3), new Matrix_block(d7, 3) };
        C = new Matrix_block[2] { new Matrix_block(d2, 3), new Matrix_block(d5, 3) };
    }
    public Matrix(int k) 
    {
        n = 3 * k;
        N = k;

        A = new Matrix_block[k - 1];
        B = new Matrix_block[k];
        C = new Matrix_block[k - 1];

        double[, ] tmp1 = new double[3, 3] { { 1.0, 2.0, 0.0 }, { -2.0, 1.0, 2.0 },{ 0.0, -2.0, 1.0 }};

        for (int i = 0; i < k - 1; i++)
            A[i] = new Matrix_block(0.0);
        for (int i = 0; i < k; i++)
            B[i] = new Matrix_block(tmp1, 3);
        for (int i = 0; i < k - 1; i++)
            C[i] = new Matrix_block(0.0);
        //double[,] tmp2 = new double[3, 3] { { 1.0, 2.0, 0.0 }, { -2.0, 1.0, 2.0 }, { 0, -2.0, 1.0 } };
        //B[N - 1] = new Matrix_block(tmp2, 3);
        for (int i = 0; i < N - 1; i++)
        {
            A[i].array[0, 2] = -2;
            C[i].array[2, 0] = -2;
        }
    }
    public Matrix(int k, bool b) // Zeros
    {
        n = 3 * k;
        N = k;

        A = new Matrix_block[k - 1];
        B = new Matrix_block[k];
        C = new Matrix_block[k - 1];

        for (int i = 0; i < k - 1; i++)
            A[i] = new Matrix_block(0.0);
        for (int i = 0; i < k; i++)
            B[i] = new Matrix_block(0.0);
        for (int i = 0; i < k - 1; i++)
            C[i] = new Matrix_block(0.0);

    }
    public Matrix(Matrix Copy)
    {
        N = Copy.N;
        n = Copy.n;
        for (int i = 0; i < N - 1; i++)
            A[i] = Copy.A[i];
        for (int i = 0; i < N; i++)
            B[i] = Copy.B[i];
        for (int i = 0; i < N - 1; i++)
            C[i] = Copy.C[i];
    }
    public static Matrix operator +(Matrix P1, Matrix P2)
    {
        if (P1.N != P2.N)
            throw new System.IndexOutOfRangeException();
        Matrix Res = new Matrix(P1.N, true);
        for (int i = 0; i < P1.N - 2; i++)
            Res.A[i] = P1.A[i] + P2.A[i];
        for (int i = 0; i < P1.N - 1; i++)
            Res.B[i] = P1.B[i] + P2.A[i];
        for (int i = 0; i < P1.N - 2; i++)
            Res.C[i] = P1.C[i] + P2.A[i];
        return Res;
    }
    public static Matrix operator -(Matrix P1, Matrix P2)
    {
        if (P1.N != P2.N)
            throw new System.IndexOutOfRangeException();
        Matrix Res = new Matrix(P1.N, true);
        for (int i = 0; i < P1.N - 2; i++)
            Res.A[i] = P1.A[i] - P2.A[i];
        for (int i = 0; i < P1.N - 1; i++)
            Res.B[i] = P1.B[i] - P2.A[i];
        for (int i = 0; i < P1.N - 2; i++)
            Res.C[i] = P1.C[i] - P2.A[i];
        return Res;
    }
    public static Vector operator *(Matrix m, Vector v)
    {
        if (v.len != m.n)
            throw new System.IndexOutOfRangeException();
        Vector res = new Vector(m.N, 0.0);
        res[0] += m.B[0] * v[0];
        res[0] += m.C[0] * v[1];
        for (int j = 1; j < m.N - 1; j++)
        {
            res[j] += m.A[j - 1] * v[j - 1];
            res[j] += m.B[j] * v[j];
            res[j] += m.C[j] * v[j + 1];
        }
        res[m.N - 1] += m.A[m.N - 2] * v[m.N - 2];
        res[m.N - 1] += m.B[m.N - 1] * v[m.N - 1];
        return res;
    }
    public static Matrix operator *(Matrix m, double d)
    {
        Matrix Res = new Matrix(m);
        for (int i = 0; i < m.N - 2; i++)
            Res.A[i] = m.A[i] * d;
        for (int i = 0; i < m.N - 1; i++)
            Res.B[i] = m.B[i] * d;
        for (int i = 0; i < m.N - 2; i++)
            Res.C[i] = m.C[i] * d;
        return Res;
    }
    public Vector solve(Vector F)
    {
        Matrix_block[] alpha = new Matrix_block[N];
        Vector_block[] beta = new Vector_block[N + 1];
        alpha[1] = (B[0]).inverse() * C[0];
        beta[1] = (B[0]).inverse() * F[0];

        for (int i = 1; i < N - 1; i++)
        {
            alpha[i + 1] = (B[i] - A[i - 1] * alpha[i]).inverse() * C[i];
            beta[i + 1] = (B[i] - A[i - 1] * alpha[i]).inverse() *
                (F[i] - A[i - 1] * beta[i]);
        }

        beta[N] = (B[N - 1] - A[N - 2] * alpha[N - 1]).inverse() *
                (F[N - 1] - A[N - 2] * beta[N - 1]);

        Vector X = new Vector(N);
        X[N - 1] = beta[N];
        for (int i = N - 2; i >= 0; i--)
        {
            X[i] = beta[i + 1] - alpha[i + 1] * X[i + 1];
        }
        return X;

        /*
         * Matrix_block[] alpha = new Matrix_block[N - 1];
        Vector_block[] beta = new Vector_block[N];
        alpha[0] = (B[0]).inverse() * C[0];
        beta[0] = (B[0]).inverse() * F[0];
        for (int i = 1; i < N - 1; i++)
        {
            alpha[i] = (B[i - 1] - A[i - 1] * alpha[i - 1]).inverse() * C[i - 1];
            beta[i] = (B[i - 1] - A[i - 1] * alpha[i - 1]).inverse() *
                (F[i - 1] - A[i - 1] * beta[i - 1]);
        }

        beta[N - 1] = (B[N - 2] - A[N - 2] * alpha[N - 2]).inverse() *
                (F[N - 2] - A[N - 2] * beta[N - 2]);

        Vector X = new Vector(N);
        X[N - 1] = beta[N - 1];
        for (int i = N - 2; i >= 0; i--)
        {
            X[i] = beta[i + 1] - alpha[i] * X[i + 1];
        }*/


        /*for (int i = 1; i < N - 1; i++)
        {
            alpha[i] = (B[i] - A[i - 1] * alpha[i - 1]).inverse() * C[i];
            beta[i] = (B[i] - A[i - 1] * alpha[i - 1]).inverse() *
                (F[i] - A[i - 1] * beta[i - 1]);
        }

        beta[N - 1] = (B[N - 1] - A[N - 2] * alpha[N - 2]).inverse() *
                (F[N - 1] - A[N - 2] * beta[N - 2]);

        Vector X = new Vector(N);
        X[N - 1] = beta[N - 1];
        for (int i = N - 2; i >= 0; i--)
        {
            X[i] = beta[i] - alpha[i] * X[i + 1];
        }

        return X;*/
    }
    public override string ToString()
    {
        string s = "";
        double[,] arr = new double[n, n];
        for (int l = 0; l < 3; l++)
            for (int k = 0; k < 3; k++)
                arr[l, k] = (B[0]).array[l, k];
        for (int l = 0; l < 3; l++)
            for (int k = 0; k < 3; k++)
                arr[l, k + 3] = C[0].array[l, k];
        for (int l = 0; l < 3; l++)
            for (int k = 6; k < n; k++)
                arr[l, k] = 0;

        for (int i = 1; i < N - 1; i++)
        {
            for (int l = 0; l < 3; l++)
                for (int k = 0; k < 3; k++)
                    arr[l + 3 * i, k + 3 * (i - 1)] = A[i - 1].array[l, k];
            for (int l = 0; l < 3; l++)
                for (int k = 0; k < 3; k++)
                    arr[l + 3 * i, k + 3 * i] = B[i].array[l, k];
            for (int l = 0; l < 3; l++)
                for (int k = 0; k < 3; k++)
                    arr[l + 3 * i, k + 3 * (i + 1)] = C[i].array[l, k];
            for (int l = 3 * i; l < 3 * i + 3; l++)
                for (int k = 3 * (i + 2); k < n; k++)
                    arr[l, k] = 0;

        }

        for (int i = 3 * (N - 1); i < n; i++)
            for (int j = 0; j < 3 * (N - 2); j++)
                arr[i, j] = 0;
        for (int l = 0; l < 3; l++)
            for (int k = 0; k < 3; k++)
                arr[l + 3 * (N - 1), k + 3 * (N - 2)] = (A[N - 2]).array[l, k];
        for (int l = 0; l < 3; l++)
            for (int k = 0; k < 3; k++)
                arr[l + 3 * (N - 1), k + 3 * (N - 1)] = B[N - 1].array[l, k];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
                s += (arr[i, j].ToString() + " ");
            s += "\n";
        }
        s += "\n";
        return s;
}
    public double[,] save(string s)
    {
        FileStream f = null;
        double[,] arr = new double[n, n];

        string docPath = Environment.CurrentDirectory;

        try
        {
            f = new FileStream(Path.Combine(docPath, s), FileMode.Append, FileAccess.Write);
            using (StreamWriter writer = new StreamWriter(f))
            {
                writer.WriteLine("Порядок матрицы N = {0}\n" , N);
                for (int l = 0; l < 3; l++)
                    for (int k = 0; k < 3; k++)
                        arr[l, k] = (B[0]).array[l, k];
                for (int l = 0; l < 3; l++)
                    for (int k = 0; k < 3; k++)
                        arr[l, k + 3] = C[0].array[l, k];
                for (int l = 0; l < 3; l++)
                    for (int k = 6; k < n; k++)
                        arr[l, k] = 0;

                for (int i = 1; i < N - 1; i++)
                {
                    for (int l = 0; l < 3; l++)
                        for (int k = 0; k < 3; k++)
                            arr[l + 3 * i, k + 3 * (i - 1)] = A[i - 1].array[l, k];
                    for (int l = 0; l < 3; l++)
                        for (int k = 0; k < 3; k++)
                            arr[l + 3 * i, k + 3 * i] = B[i].array[l, k];
                    for (int l = 0; l < 3; l++)
                        for (int k = 0; k < 3; k++)
                            arr[l + 3 * i, k + 3 * (i + 1)] = C[i].array[l, k];
                    for (int l = 3 * i; l < 3 * i + 3; l++)
                        for (int k = 3 * (i + 2); k < n; k++)
                            arr[l, k] = 0;

                }

                for (int i = 3 * (N - 1); i < n; i++)
                    for (int j = 0; j < 3 * (N - 2); j++)
                        arr[i, j] = 0;
                for (int l = 0; l < 3; l++)
                    for (int k = 0; k < 3; k++)
                        arr[l + 3 * (N - 1), k + 3 * (N - 2)] = (A[N - 2]).array[l, k];
                for (int l = 0; l < 3; l++)
                    for (int k = 0; k < 3; k++)
                        arr[l + 3 * (N - 1), k + 3 * (N - 1)] = B[N - 1].array[l, k];

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                        writer.Write(arr[i, j].ToString() + " ");
                    writer.WriteLine("");
                }
                writer.WriteLine();
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            if (f != null)
                f.Close();

        }
        return arr;
    }
    public double[] ToArray(Matrix T, string s)
    {
        int qa = 0, K = s == "B" ? T.N : T.N - 1;

        double[] tmp = new double[9 * K];
        Matrix_block[] M;
        if (s == "B")
            M = T.B;
        else if (s == "A")
            M = T.A;
        else
            M = T.C;
       
        for (int l = 0; l < K; l++)
        {
            
            for (int i = 0; i < 3; i++)
                for (int k = 0; k < 3; k++)
                {
                    tmp[qa] = (M[l]).array[i, k];
                    qa++;
                }
        }
       /* Console.WriteLine(s);
        for (int i = 0; i < 9 * K; i++)
            Console.WriteLine(tmp[i]);*/
        return tmp;
    }
}

