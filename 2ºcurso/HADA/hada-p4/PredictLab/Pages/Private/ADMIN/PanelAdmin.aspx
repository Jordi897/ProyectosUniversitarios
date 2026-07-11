<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="PanelAdmin.aspx.cs" Inherits="PredictLab.Pages.Private.ADMIN.PanelAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../Style/AdminStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="admin-dashboard">
        <section class="stats-grid">
            <div class="card stat-card">
                <h3>Usuarios Totales</h3>
                <asp:Label ID="usuariosTotales" CssClass="num" runat="server" Text="0" />
            </div>
            <div class="card stat-card">
                <h3>Usuarios Conectados</h3>
                <asp:Label ID="usuariosConectados" CssClass="num" runat="server" Text="0" />
            </div>
        </section>
        <section class="panel-section">
            <h2>Predicciones con más puntos en juego</h2>
            <div class="card">
                <table class="table table-hover mb-0">
                    <thead>
                        <tr>
                            <th scope="col">Predicción</th>
                            <th scope="col">Descripción</th>
                            <th scope="col">Puntos en juego</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="PrediccionesDestacadas" runat="server" OnItemCommand="PrediccionesDestacadas_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("titulo") %></td>
                                    <td><%# Eval("prediccion") %></td>
                                    <td><%# Eval("cantidadrecaudada") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </section>
        <section class="panel-section">
            <h2>Predicciones Finalizadas (Pendientes de Resolver)</h2>
            <div class="card">
                <table class="table table-hover mb-0">
                    <thead>
                        <tr>
                            <th scope="col">Predicción</th>
                            <th scope="col">Fecha Finalización</th>
                            <th scope="col">Acción</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="PrediccionesFinalizadas" runat="server" OnItemCommand="PrediccionesFinalizadas_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("titulo") %></td>
                                    <td><%# Eval("fechafin") %></td>
                                    <td>
                                        <asp:Button ID="ResolverButton" runat="server"
                                            Text="Resolver"
                                            CommandName="Resolver"
                                            CommandArgument='<%# Eval("id") %>'
                                            CssClass="btn btn-primary btn-sm" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </section>

        <section class="panel-section">
            <h2>Usuarios que más dinero han gastado</h2>
            <div class="card">
                <table class="table table-hover mb-0">
                    <thead>
                        <tr>
                            <th scope="col">Usuario</th>
                            <th scope="col">Email</th>
                            <th scope="col">Gastado</th>
                            <th scope="col">Moneda</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="UsuariosTopGasto" runat="server" OnItemCommand="UsuariosTopGasto_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("nickname") %></td>
                                    <td><%# Eval("email") %></td>
                                    <td><%# Eval("totalCantidad") %></td>
                                    <td><%# Eval("divisa") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </section>

        <section class="panel-section">
            <h2>Usuarios que más puntos han ganado</h2>
            <div class="card">
                <table class="table table-hover mb-0">
                    <thead>
                        <tr>
                            <th scope="col">Usuario</th>
                            <th scope="col">Email</th>
                            <th scope="col">Ganado</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="UsuariosTop" runat="server" OnItemCommand="UsuariosTop_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("nickname") %></td>
                                    <td><%# Eval("email") %></td>
                                    <td><%# Eval("saldo") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </section>

    </div>

</asp:Content>
