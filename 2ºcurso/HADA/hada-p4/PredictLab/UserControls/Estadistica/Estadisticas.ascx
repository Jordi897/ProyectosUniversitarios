<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Estadisticas.ascx.cs" Inherits="PredictLab.UserControls.Estadisticas.WebUserControl1" %>
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-sRIl4kxILFvY47J16cr9ZwB07vP4J8+LH7qKQnuqkuIAvNWLzeN8tE5YBujZqJLB" crossorigin="anonymous">
<ul ID="estadisticasUsuario" class="list-group" runat="server">

  <li class="list-group-item list-group-item-dark">Mayor racha: <asp:Label ID="MRacha" runat="server" Text="" ></asp:Label></li>
  <li class="list-group-item list-group-item-dark">Racha actual: <asp:Label ID="RActual" runat="server" Text="" ></asp:Label></li>
  <li class="list-group-item list-group-item-dark">Maximo de puntos: <asp:Label ID="MPuntos" runat="server" Text="" ></asp:Label></li>
  <li class="list-group-item list-group-item-dark">Predicciones ganadas: <asp:Label ID="PGanadas" runat="server" Text="" ></asp:Label></li>
  <li class="list-group-item list-group-item-dark">Predicciones perdidas: <asp:Label ID="PPerdidas" runat="server" Text="" ></asp:Label></li>
  <li class="list-group-item list-group-item-dark">Predicciones totales: <asp:Label ID="PTotales" runat="server" Text="" ></asp:Label></li>
</ul>
<asp:Button ID="btnCrearEstadisticas" runat="server" CssClass="btn btn-primary" Text="Crear estadisticas" OnClick="btnCrear_Click" />