using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MtSac.LabFinal
{
  public class MyLiteral: Literal
  {
    public event Action<HtmlTextWriter> OnRender;
    // Default Constr..
    public MyLiteral()
    {
      
    }

    // We are overriding the Render Method from the Literal WebControl
    protected override void Render(HtmlTextWriter tw) 
    {
      OnRender?.Invoke(tw);
    }

  }
}