using System;
using System.Collections.Generic;

namespace simple_select_statement
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.Write("Enter SSS:");
            //  string sss = Console.ReadLine();
            string sss = "table1.[field1]|field2.eq('abc')||field2,field3|field3.ne('1')";
            SssTranspiler tp = new SssTranspiler(sss);
            Console.WriteLine("transpiling....");
            Console.Write("SQL:");
            Console.WriteLine(tp.TranspileToSql());
            Console.ReadKey();
        }



    }

}
