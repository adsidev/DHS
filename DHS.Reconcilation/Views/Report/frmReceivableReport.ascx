<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

    <script runat="server">
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewData["FiscalYear"].ToString() != "0")
            {
                ReportViewer1.ServerReport.ReportPath = "/" + ConfigurationManager.AppSettings["SSRSFolder"].ToString() + "/ProjectReceivables";
                Uri uri = new Uri(ConfigurationManager.AppSettings["SSRSURL"].ToString());
                ReportViewer1.ServerReport.ReportServerUrl = uri;
                Microsoft.Reporting.WebForms.ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[4];
                Param[0] = new Microsoft.Reporting.WebForms.ReportParameter("FiscalYearId", ViewData["FiscalYear"].ToString());
                Param[1] = new Microsoft.Reporting.WebForms.ReportParameter("StatusId", ViewData["StatusName"].ToString());
                Param[2] = new Microsoft.Reporting.WebForms.ReportParameter("ProjectId", ViewData["ProjectName"].ToString());
                Param[3] = new Microsoft.Reporting.WebForms.ReportParameter("ProjectGroupId", ViewData["ProjectGroup"].ToString());
                ReportViewer1.ShowParameterPrompts = false;
                ReportViewer1.ServerReport.SetParameters(Param);
                ReportViewer1.ServerReport.Refresh();
                ReportViewer1.Visible = true;
            }
        }
</script>
<form runat="server" id="frmProjectReceivables">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" ProcessingMode="Remote"
    Height="1100px" Width="1000px" AsyncRendering="false" Visible="false">
</rsweb:ReportViewer>
</form>