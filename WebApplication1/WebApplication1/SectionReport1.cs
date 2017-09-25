using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace MtSac.LabFinal
{
    /// <summary>
    /// Summary description for SectionReport1.
    /// </summary>
    public partial class SectionReport1 : GrapeCity.ActiveReports.SectionReport
    {
        private List<Employee> _Employees;
        private int index = 0;

        public SectionReport1(List<Employee> Employees)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            _Employees = Employees;
        }

        // get properties from employee class
        private static PropertyInfo[] _Properties = typeof(Employee).GetProperties();
        // initialize data fields
        private void SectionReport1_DataInitialize(object sender, EventArgs e)
        {
            foreach (var property in _Properties)
            {
                this.Fields.Add(property.Name);
            }
        }
        // fetch data for each field from employee list
        private void SectionReport1_FetchData(object sender, FetchEventArgs eArgs)
        {
            if (index < _Employees.Count)
            {
                var employee = _Employees[index++];
                foreach (var property in _Properties)
                {
                    Fields[property.Name].Value = property.GetValue(employee, null);
                }

                #region non property field helpers
                
                #endregion
                // non-property fields
                txtBasePay.Text = employee.GetBasePay().ToString("#,##0.00");
                if (employee is Manager)
                {
                    var manager = employee as Manager;
                    txtBonusPay.Text = manager.GetBonusPay().ToString("#,##0.00");
                    txtCommPay.Text = manager.GetCommPay().ToString("#,##0.00");
                }
                else if (employee is SalesRep)
                {
                    var salesRep = employee as SalesRep;
                    txtBonusPay.Text = "";
                    txtCommPay.Text = salesRep.GetCommPay().ToString("#,##0.00");
                }
                else
                {
                    txtBonusPay.Text = "";
                    txtCommPay.Text = "";
                }
                eArgs.EOF = false;
            }
        }
        
        private void Fetch_Total(object sender, EventArgs e)
        {

        }
    }
}
