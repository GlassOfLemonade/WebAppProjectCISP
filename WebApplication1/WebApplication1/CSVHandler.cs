using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MtSac.LabFinal
{
    public class CSVHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse rs = context.Response;
            EmployeesWP wp = new EmployeesWP();
            var Employees = wp.GetData();
            try
            {
                byte[] Data;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (StreamWriter writer = new StreamWriter(ms))
                    {
                        var stringHeader = "";
                        foreach (var property in Employee._Properties)
                        {
                            stringHeader += property.Name + ",";
                        }
                        writer.WriteLine(stringHeader);
                        foreach (var employee in Employees)
                        {
                            var stringValues = "";
                            foreach (var property in Employee._Properties)
                            {
                                stringValues += property.GetValue(employee, null).ToString() + ",";
                            }
                            writer.WriteLine(stringValues);
                        }
                    }
                    Data = ms.ToArray();
                }
                rs.ContentType = "application/csv";
                rs.AddHeader("Content-Length", Data.Length.ToString());
                rs.AddHeader("Content-Disposition", "inline; filename=MyFile.csv");
                rs.BinaryWrite(Data);
            }
            catch (Exception ex)
            {
                rs.Write(ex.Message);
            }

        }
    }
}