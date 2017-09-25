using System;
using System.Data.SqlClient;

namespace MtSac.LabFinal
{
  public class SalesRep : Employee, IPayable
  {
    #region Properties  
    private Decimal _CommissionRate;
    private String CommissionRate
    {
      get { return _CommissionRate.ToString(); }
      set { decimal.TryParse(value, out _CommissionRate); }
    }
    #endregion

    #region Constructors     
    public SalesRep() { }

    public SalesRep(SqlDataReader rs) : base(rs)
    {
      CommissionRate = rs["CommRate"].ToString();
    }

    public SalesRep(SalesRep emp) : base(emp)
    {
      CommissionRate = emp.CommissionRate;
    }
    #endregion

    public Decimal GetCommPay()
    {
      return Utility.TotalSales * (_CommissionRate / 10000M);
    }
    
    //Interface IPayable
    public override decimal GetWeeklyPayAmount()
    {
      Decimal PayAmount = base.GetWeeklyPayAmount();
      Decimal CommAmount = GetCommPay();
      return PayAmount + CommAmount;
    }
  }
}