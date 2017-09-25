using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace MtSac.LabFinal
{
    /// <summary>
    /// Summary description for ReportErrors.
    /// </summary>
    public partial class ReportErrors : GrapeCity.ActiveReports.SectionReport
    {

        public ReportErrors()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            this.txtError.Value = this.UserData;
        }
    }
}
