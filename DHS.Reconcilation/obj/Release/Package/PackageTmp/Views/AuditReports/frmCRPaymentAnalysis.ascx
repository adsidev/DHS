﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

    <script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ViewData["Facility"].ToString() != "0")
        {
            ReportViewer1.ServerReport.ReportPath = "/" + ConfigurationManager.AppSettings["SSRSFolder"].ToString() + "/CRPaymentAnalysis";
            Uri uri = new Uri(ConfigurationManager.AppSettings["SSRSURL"].ToString());
            ReportViewer1.ServerReport.ReportServerUrl = uri;
            Microsoft.Reporting.WebForms.ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[1];
            Param[0] = new Microsoft.Reporting.WebForms.ReportParameter("FacilityID", ViewData["Facility"].ToString());
            ReportViewer1.ShowParameterPrompts = false;
            ReportViewer1.ServerReport.SetParameters(Param);           
            ReportViewer1.ServerReport.Refresh();
            ReportViewer1.Visible = true;
        }
    }
</script>
<form runat="server" id="form1">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" ProcessingMode="Remote"
    Width="100%" AsyncRendering="false" Visible="false">
</rsweb:ReportViewer>
</form>