using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace faithTest
{
    internal class Program
    {
        
            public static void Main(string[] args)
            {
            #region
            /* string sp = "aaa' or 1=1 -- ";
             StringBuilder sql=new StringBuilder().Append(" select * from UserInfo where 1=1");
             sql.AppendFormatWithSafe(" and RealName like '%{0}%'", sp, sql);
             Console.WriteLine(sql.ToString());*/
            /* SqlParameter[] sp = new SqlParameter[] {
                 new SqlParameter("@PdtCode", "aaa' or 1=1 -- "),

             };
             StringBuilder sql = new StringBuilder();
             sql.Append(@"select * from user where 1=1 and pid=@PdtCode");
             Console.WriteLine(sql.ToString());*/
            #endregion
            /*int m, n;
            Console.WriteLine("几个头？");
            n = int.Parse(Console.ReadLine());
            Console.WriteLine("几只脚？");
            m = int.Parse(Console.ReadLine());
            Console.WriteLine("鸡：" + (4 * n - m) / 2 + "兔子:" + (m - 2 * n) / 2);*/

            /*string a = "hahahahhahahah";
            if (a.IndexOf(" ")>=0)
            {
                Console.WriteLine("有空格");
            }
            else
            {
                Console.WriteLine("没有空格");
            }*/
            string value = faith.a.ToString();
            Console.WriteLine(value);
            //获取本机域名
            decimal a = 1;
            decimal b = 1;
            Console.WriteLine(a * (1 + b * (decimal)0.01));
        }
        public enum faith { 
            a,
            b
        }


    }
}
