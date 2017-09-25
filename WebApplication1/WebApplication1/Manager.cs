using System;
using System.Data;
using System.Data.SqlClient;

namespace MtSac.LabFinal
{
  public class Manager : Employee, IPayable
  {

    private Decimal _BonusRate;
    public String BonusRate
    {
      get { return _BonusRate.ToString(); }
      set { Decimal.TryParse(value, out _BonusRate); }
    }

    private Decimal _CommissionRate;
    private String CommissionRate
    {
      get { return _CommissionRate.ToString(); }
      set { decimal.TryParse(value, out _CommissionRate); }
    }


    #region Constructors     

    public Manager() 
    {
    }

    public Manager(Manager emp) : base(emp)
    {
      this.BonusRate = emp.BonusRate;
    }

    public Manager(SqlDataReader rs) : base(rs)
    {
      this.BonusRate = rs["BonusRate"].ToString();
      this.CommissionRate = rs["CommRate"].ToString();
    }

    #endregion

    public Decimal GetBonusPay()
    { 
      return Utility.TotalSales * (_BonusRate / 10000M); 
    }

    public Decimal GetCommPay()
    {
      return Utility.TotalSales * (_CommissionRate / 10000M);
    }


    //Interface IPayable
    public override decimal GetWeeklyPayAmount()
    {
      Decimal PayAmount = base.GetWeeklyPayAmount();
      Decimal BonusAmount = GetBonusPay();
      Decimal CommAmount = GetCommPay();
      return PayAmount + BonusAmount + CommAmount;
    }

  }

}