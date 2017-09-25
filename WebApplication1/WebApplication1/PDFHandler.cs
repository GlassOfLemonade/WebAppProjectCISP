using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Export.Pdf.Section;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MtSac.LabFinal
{
    public class PDFHandler : IHttpHandler
    {
        private string _FileName = string.Empty;
        private string FileName
        {
            get { return _FileName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _FileName = value.Substring(value.LastIndexOf("/") + 1);
                }
            }
        }

        private HttpRequest request;
        private HttpResponse response;
    
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            response = context.Response;
            request = context.Request;


            try
            {
                EmployeesWP wp = new EmployeesWP();
                var Employees = wp.GetData();
                using (SectionReport1 report = new SectionReport1(Employees))
                {
                    report.Run();
                    ToStream(report);
                }
            }
            catch (Exception ex)
            {
                // throw error to report
                using (ReportErrors report = new ReportErrors())
                {
                    report.UserData = ex.Message;
                    report.Run();
                    ToStream(report);
                }
            }
        }

        private void ToStream(SectionReport report)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (PdfExport pdf = new PdfExport())
                {
                    pdf.Version = PdfVersion.Pdf17;
                    pdf.Options.FitWindow = true;
                    pdf.Options.DisplayTitle = true;
                    pdf.Export(report.Document, ms);
                }

                response.ContentType = "application/pdf";
                response.AddHeader("Content-Length", ms.Length.ToString());
                response.AddHeader("Content-Disposition", "inline; filename=" + FileName);
                response.BinaryWrite(ms.ToArray());
            }
        }
    }
}