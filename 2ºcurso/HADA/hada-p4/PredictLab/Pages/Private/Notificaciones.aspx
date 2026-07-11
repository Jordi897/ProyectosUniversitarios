<%@ Page Title="Notificaciones" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="Notificaciones.aspx.cs" Inherits="PredictLab.Pages.Private.Notificaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Notificaciones</h2>

        <div class="card border-0 shadow-sm mt-3">
            <div class="card-header bg-white d-flex justify-content-between align-items-center">
                <div>
                    <div class="fw-bold">Bandeja de notificaciones</div>
                    <div class="text-muted small">Nuevas y anteriores</div>
                </div>

                <asp:Button
                    ID="BtnMarcarTodasLeidas"
                    runat="server"
                    Text="Marcar todas como leídas"
                    CssClass="btn btn-outline-success btn-sm"
                    OnClick="BtnMarcarTodasLeidas_Click" />
            </div>

            <div class="list-group list-group-flush">
                <asp:Repeater ID="RptNotificaciones" runat="server" OnItemCommand="RptNotificaciones_ItemCommand">
                    <ItemTemplate>
                        <div class='list-group-item p-3 d-flex justify-content-between align-items-start gap-3
                                    <%# Convert.ToBoolean(Eval("Leida")) ? "" : "bg-light" %>'>

                            <div class="d-flex align-items-start gap-3">
                                <!-- Indicador -->
                                <div class='rounded-circle mt-1 <%# Convert.ToBoolean(Eval("Leida")) ? "bg-secondary" : "bg-primary" %>'
                                     style="width:10px;height:10px;"></div>

                                <div>
                                    <div class="d-flex align-items-center gap-2">
                                        <div class="fw-semibold"><%# Eval("Titulo") %></div>

                                        <span class='badge <%# Convert.ToBoolean(Eval("Leida")) ? "text-bg-secondary" : "text-bg-primary" %>'>
                                            <%# Convert.ToBoolean(Eval("Leida")) ? "Leída" : "Nueva" %>
                                        </span>
                                    </div>

                                    <div class="text-muted small"><%# Eval("Fecha", "{0:yyyy-MM-dd HH:mm}") %></div>
                                    <div class="mt-1"><%# Eval("Mensaje") %></div>
                                </div>
                            </div>

                            <!-- lado derecho -->
                            <div class="text-end d-flex flex-column gap-2">
                                <asp:LinkButton
                                    ID="BtnMarcarLeida"
                                    runat="server"
                                    CommandName="MarcarLeida"
                                    CommandArgument='<%# Eval("Id") %>'
                                    CssClass="btn btn-outline-primary btn-sm"
                                    Visible='<%# !Convert.ToBoolean(Eval("Leida")) %>'>
                                    Marcar leída
                                </asp:LinkButton>

                                <span class="text-muted small">
                                    <%# Convert.ToBoolean(Eval("Leida")) ? "Visto" : "Pendiente" %>
                                </span>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>