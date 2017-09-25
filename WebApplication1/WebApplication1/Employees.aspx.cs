using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace MtSac.LabFinal
{
    public class EmployeesWP : Page
    {
        protected HtmlInputText txtEmployeeId;
        protected HtmlInputText txtLastName;
        protected HtmlInputText txtFirstName;
        protected HtmlInputText txtHireDate;

        protected MyLiteral MyLit;

        public EmployeesWP()
        {
            this.Load += StudentsWP_Load;
        }


        private void StudentsWP_Load(object sender, System.EventArgs e)
        {
            GetUserInput();
            MyLit.OnRender += MyLit_OnRender;
        }


        private Employee _Filter = new Employee();
        private void GetUserInput()
        {
            _Filter.EmployeeId = Request["EmployeeId"] ?? "";
            _Filter.LastName = Request["LastName"] ?? "";
            _Filter.FirstName = Request["FirstName"] ?? "";
            _Filter.HireDate = Request["HireDate"] ?? "";

            if (txtEmployeeId != null) { txtEmployeeId.Value = _Filter.EmployeeId; }
            if (txtLastName != null) { txtLastName.Value = _Filter.LastName; }
            if (txtFirstName != null) { txtFirstName.Value = _Filter.FirstName; }
            if (txtHireDate != null) { txtHireDate.Value = _Filter.HireDate; }
        }

        private void MyLit_OnRender(HtmlTextWriter sw)
        {
            try
            {
                Decimal TotalPay = 0.0M;
                var Employees = GetData();
                foreach (var emp in Employees)
                {
                    sw.WriteLine("<tr>");
                    sw.WriteLine("<td>{0}</td>", emp.EmployeeId);
                    sw.WriteLine("<td>{0}</td>", emp.LastName);
                    sw.WriteLine("<td>{0}</td>", emp.FirstName);
                    sw.WriteLine("<td>{0}</td>", emp.HireDate);
                    sw.WriteLine("<td class='AlignRight'>{0:$#,##0.00}</td>", emp.Salary);

                    sw.WriteLine("<td class='AlignRight' >{0:$#,##0.00}</td>", emp.GetBasePay());
                    if (emp is Manager)
                    {
                        var mngr = emp as Manager;
                        sw.WriteLine("<td class='AlignRight'>{0:$#,##0.00}</td>", mngr.GetCommPay());
                        sw.WriteLine("<td class='AlignRight'>{0:$#,##0.00}</td>", mngr.GetBonusPay());
                    }
                    else if (emp is SalesRep)
                    {
                        var slrp = emp as SalesRep;
                        sw.WriteLine("<td class='AlignRight'>{0:$#,##0.00}</td>", slrp.GetCommPay());
                        sw.WriteLine("<td class='AlignRight'>{0:$#,##0.00}</td>", ""); // Bonus
                    }
                    else
                    {
                        sw.WriteLine("<td class='AlignRight'>{0:$#,##0.00}</td>", "");//Comm Amount
                        sw.WriteLine("<td class='AlignRight'>{0:$#,##0.00}</td>", "");//Bonus Amount
                    }
                    sw.WriteLine("<td class='AlignRight'>{0:$#,##0.00}</td>", emp.GetWeeklyPayAmount());
                    sw.WriteLine("</tr>");
                    TotalPay += emp.GetWeeklyPayAmount();
                }

                sw.WriteLine("<tfoot>");
                sw.WriteLine("<tr style='background-color:#f6f6f6;border:solid 1px #cccccc;'>");
                sw.WriteLine("<th class='AlignLeft' colspan='8' >{0}</th>", "TotalPay");
                sw.WriteLine("<th class='AlignRight'>{0:$#,##0.00}</th>", TotalPay);
                sw.WriteLine("</tr>");
                sw.WriteLine("</tfoot>");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        public List<Employee> GetData()
        {
            List<Employee> Employees = new List<Employee>();
            try
            {
                String sSQL = "dbo.GetEmployees";
                using (SqlConnection cn = new SqlConnection("Server=10.12.69.33;Database=CISP41;UID=sa;Pwd=Password1;"))
                {
                    using (SqlCommand cm = new SqlCommand(sSQL, cn))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandType = CommandType.StoredProcedure;
                        if (_Filter.EmployeeId != "")
                        {
                            cm.Parameters.Add("EmployeeId", SqlDbType.VarChar).Value = _Filter.EmployeeId;
                        }
                        if (_Filter.LastName != "")
                        {
                            cm.Parameters.Add("LastName", SqlDbType.VarChar).Value = _Filter.LastName;
                        }
                        if (_Filter.FirstName != "")
                        {
                            cm.Parameters.Add("FirstName", SqlDbType.VarChar).Value = _Filter.FirstName;
                        }
                        cn.Open();
                        using (SqlDataReader rs = cm.ExecuteReader())
                        {
                            while (rs.Read())
                            {
                                if (rs["IsManager"].ToString() == "True")
                                {
                                    Employees.Add(new Manager(rs));
                                }
                                else if (rs["IsSalesRep"].ToString() == "True")
                                {
                                    Employees.Add(new SalesRep(rs));
                                }
                                else
                                {
                                    Employees.Add(new Employee(rs));
                                }
                            }
                        }
                        cn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                //Response.Write(sSQL.Replace(";",";<Br>"));
            }
            return Employees;
        }
    }
}

