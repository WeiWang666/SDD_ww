using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace test3
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename1 = "C:\\Users\\wwalts\\Desktop\\轨道文件.txt";
            StreamReader reader = new StreamReader(filename1);
            string filename2 = "C:\\Users\\wwalts\\Desktop\\result.txt";
            StreamWriter writer = new StreamWriter(filename2);

            int i = 0;
            double[] t = new double[13];
            double[] x = new double[13];
            double[] y = new double[13];
            double[] z = new double[13];

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                string[] a = new string[4];
                a = line.Split(',');
                t[i] = double.Parse(a[0]);
                x[i] = double.Parse(a[1]);
                y[i] = double.Parse(a[2]);
                z[i] = double.Parse(a[3]);
                i++;
            }


            double xp = 0;
            double yp = 0;
            double tp = 0;
            double zp = 0;
            for (int j = 1; j <= i; j++)
            {
                xp = xp + x[j - 1] ;
                yp = yp + y[j - 1] ;
                tp = tp + t[j - 1] ;
                zp = zp + z[j - 1] ;
            }
            xp = xp / i;
            yp = yp / i;
            tp = tp / i;
            zp = zp / i;
            Console.WriteLine(tp);

            double fenzi = 0;
            double fenmu = 0;
            double fenzix = 0;
            double fenmux = 0;
            double fenziy = 0;
            double fenmuy = 0;
            double fenziz = 0;
            double fenmuz = 0;
            for (int j = 1; j <= i; j++)
            {
                fenzi = fenzi + (x[j - 1] - xp) * (y[j - 1] - yp);
                fenmu = fenmu + (x[j - 1] - xp) * (x[j - 1] - xp);

                fenzix = fenzix + (t[j - 1] - tp) * (x[j - 1] - xp);
                fenmux = fenmux + (t[j - 1] - tp) * (t[j - 1] - tp);

                fenziy = fenziy + (t[j - 1] - tp) * (y[j - 1] - yp);
                fenmuy = fenmuy + (t[j - 1] - tp) * (t[j - 1] - tp);

                fenziz = fenziz + (t[j - 1] - tp) * (z[j - 1] - zp);
                fenmuz = fenmuz + (t[j - 1] - tp) * (t[j - 1] - tp);
            }

            double b1 = fenzi / fenmu;
            double b0 = yp - b1 * xp;

            double b1x = fenzix / fenmux;
            double b0x = xp - b1x * tp;

            double b1y = fenziy / fenmuy;
            double b0y = yp - b1y * tp;

            double b1z = fenziz / fenmuz;
            double b0z = zp - b1z * tp;


            writer.WriteLine("回归系数：b0,b1");
            writer.WriteLine("X：{0}，{1}", b0x, b1x);
            writer.WriteLine("Y：{0}，{1}", b0y, b1y);
            writer.WriteLine("Z：{0}，{1}", b0z, b1z);
            writer.WriteLine("预报结果");
            double[] ti = new double[] { 4200, 4500, 4800 };
            for (i = 0; i <= 2; i++)
            {
                writer.WriteLine("ti={0},X={1},Y={2},Z={3}", ti[0], b0x + b1x * ti[i], b0y + b1y * ti[i], b0z + b1z * ti[i]);
            }

            reader.Close();
            writer.Close();
            Console.ReadKey();


        }
    }
}
