<%@ Page Title="Gestión de denuncias" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="denuncias.aspx.cs" Inherits="PredictLab.Pages.Private.ADMIN.denuncias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-4">

        <h3 class="fw-semibold mb-4">Gestión de denuncias</h3>

        <asp:Label 
            ID="mensajeLabel" 
            runat="server" 
            Visible="false"
            CssClass="d-block mb-3">
        </asp:Label>

        <asp:Repeater 
            ID="denunciasRepeater" 
            runat="server"
            OnItemCommand="denunciasRepeater_ItemCommand">

            <ItemTemplate>
                <div class="card border-0 shadow-sm mb-3">
                    <div class="card-body">

                        <div class="mb-2">
                            <span class="fw-semibold">Comentario denunciado:</span>
                            <p class="mb-1"><%# Eval("mensaje") %></p>
                        </div>

                        <div class="small text-muted mb-2">
                            Usuario del comentario: <%# Eval("usuario") %>
                        </div>

                        <div class="small text-muted mb-2">
                            Denunciante: <%# Eval("emisor") %>
                        </div>

                        <div class="mb-2">
                            <span class="fw-semibold">Causa:</span>
                            <%# Eval("causadenuncia") %>
                        </div>

                        <div class="mb-2">
                            <span class="fw-semibold">Descripción:</span>
                            <%# Eval("descripcion") %>
                        </div>

                        <div class="small text-muted mb-3">
                            Fecha: <%# Convert.ToDateTime(Eval("fecha")).ToString("dd/MM/yyyy") %>
                        </div>

                        <asp:Button 
                            ID="aceptarButton" 
                            runat="server"
                            Text="Aceptar denuncia"
                            CssClass="btn btn-sm btn-danger me-2"
                            CommandName="Aceptar"
                            CommandArgument='<%# Eval("id") %>' />

                        <asp:Button 
                            ID="rechazarButton" 
                            runat="server"
                            Text="Rechazar denuncia"
                            CssClass="btn btn-sm btn-outline-secondary"
                            CommandName="Rechazar"
                            CommandArgument='<%# Eval("id") %>' />

                    </div>
                </div>
            </ItemTemplate>

        </asp:Repeater>

        <asp:Label 
            ID="sinDenunciasLabel" 
            runat="server" 
            Text="No hay denuncias pendientes."
            CssClass="text-muted"
            Visible="false">
        </asp:Label>

    </div>

</asp:Content>