<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="Mensajes.aspx.cs" Inherits="PredictLab.Pages.Private.Mensajes" %>
<%@ Register Src="~/UserControls/Chat/Chats.ascx" TagName="ChatsControl" TagPrefix="ucch" %>
<%@ Register Src="~/UserControls/Mensajes/Mensaje.ascx" TagName="MensajeControl" TagPrefix="ucmsg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css" />
    <link href="../../Style/mensajesStyle.css" rel="stylesheet" />
    <title>Mensajes</title>
    <script>
        const ruta = '<%=ResolveUrl("~/") %>';
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

        <div class="container-fluid fondo">
            <div class="row" style="height: 100vh;">
                <div class="col-md-2 border-end">
                    <ucch:ChatsControl ID="ChatsControl1" runat="server" />
                </div>
                <div ID="MensajesChat" class="col-md-10" runat="server">
                    <ucmsg:MensajeControl ID="MensajeControl1" runat="server" />
                </div>
            </div>
        </div>
     
</asp:Content>
