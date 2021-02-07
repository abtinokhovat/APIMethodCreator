using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIMethodCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection(@"Data Source=server-paliz\sql2017;Initial Catalog=avamarket;User ID=sa;Password=Pa_12345");
            var cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "dbo.SP_API_DynamicCreate";
            cmd.Connection = con;
            var dt = new DataTable();
            var da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string modelName = dt.Rows[i].ItemArray[1].ToString();
                string fileName = @"C:\Users\Paliz Co\Desktop\New Folder\" + modelName + ".cs";
                // Create a new file     
                using (FileStream fs = File.Create(fileName))
                {
                    // Add some text to file    
                    Byte[] title = new UTF8Encoding(true).GetBytes(dt.Rows[i].ItemArray[2].ToString());
                    fs.Write(title, 0, title.Length);
                }

                string methodFile = @"C:\Users\Paliz Co\Desktop\New Folder\method.cs";
                // Create Method File
                File.AppendAllLines(methodFile, new string[] {

                    dt.Rows[i].ItemArray[3].ToString()
                });


            }
        }

    }
}
