var PageMngr = null;

window.onload = window_onLoad;
function window_onLoad()
{
  try
  {
    PageMngr = new PageManager();
  }
  catch (ex)
  {
    alert(ex.description);
    window.status = ex.description;
  }
}

function PageManager()
{
  var frm = document.forms[0];

  var page = document.documentElement;
  if (page) { page.onkeydown = page_onkeydown; }
  function page_onkeydown(e)
  {
    var evt = e || window.event;
    var kc = evt.keyCode || evt.which;
    if (kc === 8)
    {
      var elm = evt.srcElement || evt.target;
      if (elm.readOnly) { return false; }
      if (elm.tagName === "INPUT" && elm.type === "checkbox") { return false; }
      if (elm.tagName !== "INPUT" && elm.tagName !== "TEXTAREA") { return false; }
    }
    return true;
  }

  var cmdGo = document.getElementById("cmdGo");
  if (cmdGo) { cmdGo.onclick = cmdGo_onclick; }
  function cmdGo_onclick()
  {
    SubmitForm();    
  }

  var txtFilters = document.getElementById("Filters");
  if (txtFilters) { txtFilters.onkeypress = txtFilters_onkeypress; }
  function txtFilters_onkeypress(e)
  {
    var evt = e || window.event;
    var kc = evt.keyCode;
    if (kc == 13)
    {
      SubmitForm();
    }
  }

  var rwColumnHeader = document.getElementById("ColumnHeader");
  if (rwColumnHeader) { rwColumnHeader.onclick = rwColumnHeader_onclick; }
  function rwColumnHeader_onclick(e)
  {
    var evt = e || window.event;
    var elm = evt.srcElement || evt.target;
    if (elm.tagName === "A")
    {
      var OrderBy = elm.id.replace("OrderBy.", "");
      if (hdnOrderBy.value === OrderBy)
      {
        hdnAscDesc.value = hdnAscDesc.value === "1" ? "0" : "1";
      }
      else
      {
        hdnOrderBy.value = OrderBy;
      }
      SubmitForm();
    }
    return false;
  }


  function SubmitForm()
  {
    var url = frm.action + "?";
    var elms = document.getElementsByTagName("INPUT");
    for (var ix = 0; ix < elms.length; ix++)
    {
      var elm = elms[ix];
      if (elm.type === "text" || elm.type === "hidden")
      {
        if (elm.id !== "" && elm.value !== "")
        {
          url += elm.id.replace("txt", "").replace("hdn", "") + "=" + elm.value + "&";
        }
      }
    }
    document.location.replace(url);
  }

  var sURL = (document.location.href).toLowerCase();
  

  if (sURL.indexOf("studentid") > 0)
  {
    var txtStudentId = document.getElementById("txtStudentId");
    if (txtStudentId) { txtStudentId.focus(); }
  }

  if (sURL.indexOf("lastname") > 0)
  {
    var txtLastName = document.getElementById("txtLastName");
    if (txtLastName) { txtLastName.focus(); }
  }

  if (sURL.indexOf("firstname") > 0)
  {
    var txtFirstName = document.getElementById("txtFirstName");
    if (txtFirstName) { txtFirstName.focus(); }
  }

  
}
