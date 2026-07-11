<%@ Page Title="Logros" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="Logros.aspx.cs" Inherits="PredictLab.Pages.Private.Logros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <div class="d-flex justify-content-between align-items-start gap-3">
            <div>
                <h2 class="mb-0">Logros</h2>
                <div class="text-muted">Descubre y completa logros para ganar recompensas</div>
            </div>

            <asp:HyperLink ID="LinkVolver" runat="server"
                NavigateUrl="~/Pages/Private/Perfil.aspx"
                CssClass="btn btn-outline-secondary btn-sm">
                Volver a perfil
            </asp:HyperLink>
        </div>

        <div class="row g-3 mt-3">
            <asp:Repeater ID="RptLogros" runat="server">
                <ItemTemplate>
                    <div class="col-12 col-md-6 col-lg-4">
                        <div class='card border-0 shadow-sm h-100 <%# Convert.ToInt32(Eval("Conseguido")) == 1 ? "" : "opacity-50" %>'>
                            <div class="card-body">
                                <div class="d-flex justify-content-between align-items-start gap-2">
                                    <div>
                                        <div class="fw-bold"><%# Eval("Titulo") %></div>
                                        <div class="text-muted small mt-1"><%# Eval("Descripcion") %></div>

                                        <div class="mt-2">
                                            <span class='badge <%# Convert.ToInt32(Eval("Conseguido")) == 1 ? "text-bg-success" : "text-bg-secondary" %>'>
                                                <%# Convert.ToInt32(Eval("Conseguido")) == 1 ? "Conseguido" : "Bloqueado" %>
                                            </span>
                                        </div>
                                    </div>

                                    <span class="badge text-bg-warning text-dark">
                                        +<%# Eval("Recompensa") %> c
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>