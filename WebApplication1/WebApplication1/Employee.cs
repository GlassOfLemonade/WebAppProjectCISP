using System;
using System.Data.SqlClient;
using System.Reflection;

namespace MtSac.LabFinal
{
    public class Employee : IIdentity, IPayable
    {
        //Interface IIdentity
        public String Id
        {
            get { return _EmployeeId.ToString(); }
        }

        #region Properties

        private int _EmployeeId;
        public String EmployeeId
        {
            get { return _EmployeeId.ToString("#"); }
            set { Int32.TryParse(value, out _EmployeeId); }
        }

        private String _LastName;
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        private String _FirstName;
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        private String _Title;
        public String Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        private DateTime _HireDate;
        public String HireDate
        {
            get { return _HireDate.Ticks > 0 ? _HireDate.ToString("MM/dd/yyyy") : ""; }
            set { DateTime.TryParse(value, out _HireDate); }
        }

        private Decimal _Salary;
        public String Salary
        {
            get { return _Salary.ToString("$##,##0.00"); }
            set { Decimal.TryParse(value, out _Salary); }
        }



        private Decimal _ManagerId;
        public String ManagerId
        {
            get { return _ManagerId.ToString(); }
            set { Decimal.TryParse(value, out _ManagerId); }
        }

        private Boolean _IsManager;
        public String IsManager
        {
            get { return _IsManager.ToString(); }
            set { Boolean.TryParse(value, out _IsManager); }
        }

        private Boolean _IsSalesRep;
        public String IsSalesRep
        {
            get { return _IsSalesRep.ToString(); }
            set { Boolean.TryParse(value, out _IsSalesRep); }
        }
        #endregion

        public static readonly PropertyInfo[] _Properties = typeof(Employee).GetProperties();

        #region Constructors      
        public Employee()
        {
        }

        public Employee(SqlDataReader rs)
        {
            for (var ix = 0; ix < rs.FieldCount; ix++)
            {
                foreach (PropertyInfo prty in _Properties)
                {
                    if (prty.Name.ToLower() == rs.GetName(ix).ToLower())
                    {
                        prty.SetValue(this, rs[ix].ToString(), null);
                        break;
                    }
                }
            }
        }

        public Employee(Employee emp)
        {
            foreach (PropertyInfo prty in _Properties)
            {
                Object value = prty.GetValue(emp, null);
                prty.SetValue(this, value, null);
            }
        }

        #endregion

        public Decimal GetBasePay()
        {
            return (_Salary / 12.0M) / 2.0m;
        }

        //Interface IPayable
        public virtual Decimal GetWeeklyPayAmount()
        {
            return (_Salary / 12.0M) / 2.0m;
        }
    }
}
