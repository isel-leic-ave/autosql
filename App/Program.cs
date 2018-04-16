using SqlReflect;
using SqlReflectTest.DataMappers;
using SqlReflectTest.Model;
using System;
using System.Collections;
using System.Diagnostics;

namespace App
{
    class Program
    {
        static readonly string connStr = @"
                    Server=(LocalDB)\MSSQLLocalDB;
                    Integrated Security=true;
                    AttachDbFileName=" +
                        Environment.CurrentDirectory +
                        "\\data\\NORTHWND.MDF";

        static void Main(string[] args)
        {
            CompareMappers(typeof(Employee));
            CompareMappers(typeof(Customer));

        }
        private static void CompareMappers(Type klass)
        {
            Console.WriteLine("############## Reflect WITH Cache");
            IDataMapper emps = new ReflectDataMapper(klass, connStr);
            for (int i = 0; i < 5; i++)
            {
                GetAllItens(emps);
            }

            Console.WriteLine("############## Reflect NO Cache");
            emps = new ReflectDataMapper(klass, connStr, false);
            for (int i = 0; i < 5; i++)
            {
                GetAllItens(emps);
            }
        }

        private static void GetAllItens(IDataMapper data)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            IEnumerable res = data.GetAll();
            stopwatch.Stop();
            Console.WriteLine("Time elapsed (us): {0}", stopwatch.Elapsed.TotalMilliseconds * 1000);
        }
    }
}
