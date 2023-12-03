using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SQLHelper obj = new SQLHelper();
            DataTable dt = new DataTable();
            int variable1 = 0;
            string variable2 = string.Empty;
            int variable3 = 0;
            int variable5 = 0;
            string query = string.Empty;
            try
            {
                Console.WriteLine("featchingData");

                obj.SqlText = @"select  ec.*, eg.col, eg.col, eg.col, eg.col,eg.col, eg.col from table ec
                                JOIN table eg ON
                                eg.col = ec.col 
                                where col not in (select col from table where col = 'val')
                                and ec.col = 'val'";

                dt = obj.getDataTable(false);


                Console.WriteLine("Inserting");
                foreach (DataRow r in dt.Rows)
                {
                    variable1 = Convert.ToInt32(r["col"]);
                    variable2 = r["col"].ToString();;

                    query = @"insert into Direct_Beneficiary_Roster (col, col, col, col, col, col, col, col, 
                                col, col, col, col, col, col) 
                                values
                                (";
                    query += variable1.ToString() + ",";
                    query += "'" + variable2 + "',";
                    query += "'value',";
                    query += r["col"].ToString() + ",";
                    query += r["col"].ToString() + ",";
                    query += r["col"].ToString() + ",";
                    query += r["col"].ToString() + ",";
                    query += r["col"].ToString() + ",";
                    query += r["col"].ToString() + ",";
                    query += r["col"].ToString() + ",";
                    query += (r["col"].ToString() == "True" ? "1" : "0") + ",";
                    query += "'" + r["col"] + "',";
                    query += "'" + r["col"] + "',";
                    
                    
                    variable3 = Convert.ToInt32(r["variable3"]);
                    for (int j = 0; j < variable3; j++)
                    {
                        string toexecute = query + "0)";
                        obj.SqlText = toexecute;
                        obj.ExecuteScalar(false);
                    }

                    variable5 = Convert.ToInt32(r["variable5"]);
                    for (int j = 0; j < variable5; j++)
                    {
                        string toexecute = query + "1)";
                        obj.SqlText = toexecute;
                        obj.ExecuteScalar(false);
                    }
                }
                Console.WriteLine("Completed");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                obj.Close();
                obj.Dispose();
                dt.Dispose();
            }

            Console.ReadLine();
        }
    }
}
