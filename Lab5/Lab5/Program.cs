using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

class Program

{
    static void Main(string[] args)
    {
        Test();
        TestTime time_test = new TestTime();
        Console.Write("Пользовательский ввод: \nВведите имя файла для записи решения на C#:  ");
        string tmp = Console.ReadLine();
        string docPath = Environment.CurrentDirectory;
        string filename = Path.Combine(docPath, tmp);
        Console.WriteLine("Нажатие enter - продолжить ввод(или любая другая клавиша)\nНажмите Escape - завершить работу");
        int Counter = 0;
        if (Console.ReadKey().Key == ConsoleKey.Escape)
            return;
        try
        {
            while (true) //main loop
            {
                Counter++;
                Stopwatch watch_CSP = new Stopwatch();

                Console.WriteLine("Введите порядок блочной матрицы N > 1 ");
                int N = Int32.Parse(Console.ReadLine()); ;
                Matrix TMP = new Matrix(N);
                Vector F = new Vector(N, 1);
                
                string s = $"test №" + Counter + " : N = " + TMP.N + "\nC# = ";
                watch_CSP.Start();
                Vector X = TMP.solve(F);
                double[,] arr = TMP.save(filename);
                watch_CSP.Stop();
                s += (watch_CSP.ElapsedMilliseconds).ToString() + "ms\n";
                long l1 = watch_CSP.ElapsedMilliseconds;

                X.save(filename, "X");
                F.save(filename, "F");
                double[] f = F.ToDouble();
                double[] x = X.ToDouble();
                double[] T_A = TMP.ToArray(TMP, "A");
                double[] T_B = TMP.ToArray(TMP, "B");
                double[] T_C = TMP.ToArray(TMP, "C");
                Stopwatch watch_CPP = new Stopwatch();
                watch_CPP.Start();
                global(T_A, T_B, T_C, f, x, F.len);
                watch_CPP.Stop();
                Vector X_C = new Vector(TMP.N, x);
                X_C.save(filename, "X from CPP");
                //s += (watch_CSP.ElapsedMilliseconds).ToString() + "ms\n";

                s += "C++ = " + (watch_CPP.ElapsedMilliseconds).ToString() + "ms\n";
                long l2 = watch_CPP.ElapsedMilliseconds;
                s += "C# / C++ = " + (((double)l1 / (double)l2));
                time_test.Add(s);
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                    break;
            }
        }
        catch(System.FormatException ex)
        {
            Console.WriteLine("Ожидалось число. Ввод завершен.");
        }
        catch (System.IndexOutOfRangeException ex)
        {
            Console.WriteLine("Ожидалось N > 1");
        }
        finally
        {
            if (time_test != null && time_test.info.Count() != 0)
                Console.WriteLine(time_test);
            TestTime.Save("time_p.txt", time_test);
        }
    }

    

    public static void Test()
    { 
        Matrix TMP = new Matrix();
        Vector F = new Vector(3, false);
        TestTime time_test = new TestTime();
        
        string s = "test1: N = " + TMP.N + "\nC# = ";
        Stopwatch watch_CSP = new Stopwatch();
        watch_CSP.Start();
        Vector X = TMP.solve(F);
        watch_CSP.Stop();
        double[,] arr = TMP.save("testCSP.txt");
        X.save("testCSP.txt", "X");
        F.save("testCSP.txt", "F");
        
        Vector F2 = TMP * X;
        Console.WriteLine($"Проверка правильности решения F1\n{F}\nF2\n{F2}");
        F2.save("testCSP.txt", "Check F");
        double[] f = F.ToDouble();
        double[] x = new double[F.len];
        double[] T_A = TMP.ToArray(TMP, "A");
        double[] T_B = TMP.ToArray(TMP, "B");
        double[] T_C = TMP.ToArray(TMP , "C");
        Stopwatch watch_CPP = new Stopwatch();
        watch_CPP.Start();
        global(T_A, T_B, T_C, f, x, F.len);
        watch_CPP.Stop();
        Vector X_C = new Vector(TMP.N, x);
        Console.WriteLine($"Решение С++ X_CPP\n{X_C}");
        s += (watch_CSP.ElapsedMilliseconds).ToString() + "ms\n";
        s += "C++ = " + (watch_CPP.ElapsedMilliseconds).ToString() + "ms\n";
        long l1 = watch_CSP.ElapsedMilliseconds;
        long l2 = watch_CPP.ElapsedMilliseconds; 
        s += "koeff = " + ((l2 / l1));
        time_test.Add(s);
        TestTime.Save("time.txt", time_test);
        Console.WriteLine(time_test);
    }

    [DllImport("C:\\Users\\Dasha\\source\\repos\\Lab5\\Debug\\MatrixCPP1.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void global(double[] a, double[] b, double[] c, double[] f, double[] x, int n);
}

