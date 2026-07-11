<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="PredictLab.Pages.Private.Perfil" %>
<%@ Register Src="../../UserControls/Usuario/Usuario.ascx" TagPrefix="uc" TagName="Usuario" %>
<%@ Register Src="../../UserControls/Transaccion/Transaccion.ascx" TagPrefix="tr" TagName="Transaccion" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Style/PerfilStyle.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-sRIl4kxILFvY47J16cr9ZwB07vP4J8+LH7qKQnuqkuIAvNWLzeN8tE5YBujZqJLB" crossorigin="anonymous">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid perfil h-100 ">
        <div class="row usuario h-100">
            <div class="col-12 col-lg-6">
                    <uc:Usuario ID="UsuarioControl" runat="server" />
            </div>
            <div class="col-12 col-lg-6 h-100 transaccion align-items-center  ">
                 <tr:Transaccion ID="TransaccionControl" runat="server" />
            </div>
        </div>
    </div>
    
    
    
</asp:Content>
