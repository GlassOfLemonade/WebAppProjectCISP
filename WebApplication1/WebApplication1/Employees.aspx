<%@ Page Language="C#" Inherits="MtSac.LabFinal.EmployeesWP" %>
<!DOCTYPE HTML >
<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head>
  <title>Employees</title>
  <link rel ="stylesheet" type ="text/css" href ="Employees.css" />
  <script type="text/javascript" src="Employees.js"></script>
</head>
<body>
<form method="post" action="Employees.aspx">
<div id="PageLayout">
  <table >
    <tr><td>
      <table id="Header" border="0">
        <colgroup>
          <col style="width:110px;" />
          <col style="width:auto;" />
        </colgroup>
        <tr>
          <td><img src ="images/Logo3.gif"  alt="Logo"  /></td>
          <td>
            <table>
              <colgroup>
                <col style="width:auto;" />
                <col style="width:425px;" />
              </colgroup>
              <tr>
                <td style="vertical-align:top" colspan ="2" >
                  <table border ="0" >
                    <colgroup>
                      <col style="width:auto;" />
                      <col style="width:100px;"/>
                      <col style="width:100px;"/>
                    </colgroup>
                    <tr>
                      <th></th>
                      <th><a href="Employees.csv" tabindex ="-1"  title ="Print this Report" id="lnkExport" >Export CSV</a></th>
                      <th><a href="Employees.pdf" tabindex ="-1"  title ="Print this Report" id="lnkPrint" >Print PDF</a></th>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr><td><br /><br /></td></tr>
              <tr style="vertical-align:bottom">
                <td>
                  <span id="Title" >Employees</span>
                </td>
                <td >
                  <table>
                    <colgroup>
                      <col style="width:auto;" />
                      <col style="width:50px;" />
                    </colgroup>
                    <tr>
                      <td></td>
                      <th><input type="button" id="cmdGo" value="Go" /> </th>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
    </td></tr>
    <tr><td>&nbsp;</td></tr>
    <tr><td>
      <table id="tblReportBody">
      <colgroup>
        <col style="width:75px;"/>
        <col style="width:auto;"/>
        <col style="width:125px;" />
        <col style="width:75px;"/>
        <col style="width:75px;"/>
        <col style="width:75px;"/>
        <col style="width:75px;"/>
        <col style="width:75px;"/>
        <col style="width:75px;"/>
      </colgroup>
        <tr id="Filters"  >
          <td><input type="text" id="txtEmployeeId" runat="server" /></td>
          <td><input type="text" id="txtLastName" runat="server" /></td>
          <td><input type="text" id="txtFirstName" runat="server"/></td>
          <td><input type="text" id="txtHireDate" runat="server"/></td>
          <td><input type="text" id="txtSalary" readonly="readonly"/></td>
          <td></td>
          <td></td>
          <td></td>
          <td></td>          
        </tr>
        <tr style="background-color:#f6f6f6;border:solid 1px #cccccc;" id="ColumnHeader"  >
          <th><a href="#" id="OrderBy.EmployeeId">EmployeeId</a></th>
          <th><a href="#" id="OrderBy.LastName">LastName</a></th>
          <th><a href="#" id="OrderBy.FirstName">FirstName</a></th>
          <th><a href="#" id="OrderBy.HireDate">HireDate</a></th>
          <th><a href="#" id="OrderBy.Salary">Salary</a></th>
          <th><a href="#" id="OrderBy.BasePay">BasePay</a></th>
          <th><a href="#" id="OrderBy.CommPay">CommPay</a></th>
          <th><a href="#" id="OrderBy.BonusPay">BonusPay</a></th>
          <th><a href="#" id="OrderBy.TotalPay">TotalPay</a></th>
        </tr>
        <tbody id="tbReportBody">
          <MtSac:MyLiteral id="MyLit" runat="server" />
        </tbody>
      </table>
      <div id="dvErrorMessage" >
        <input type="hidden" id="hdnOrderBy" runat="server"/>
        <input type="hidden" id="hdnAscDesc" runat="server" />
      </div>
    </td></tr>
    <tr><td>&nbsp;</td></tr>
  </table>    
</div>
</form>
</body>
</html>
