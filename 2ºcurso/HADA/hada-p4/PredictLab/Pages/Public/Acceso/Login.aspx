<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PredictLab.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../Style/AccesoStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid d-flex justify-content-center align-items-center min-vh-100 Acceso">
        <div class="card p-5 shadow w-100">
            <h3>
                Login
            </h3>
            <div class="mb-3">
            Email o username:
            <asp:TextBox ID="emailUserText" CssClass="form-control" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="loginRequerido" runat="server" ControlToValidate="emailUserText" Display="None" ErrorMessage="Error Introuzca un usuario o email" ForeColor="#CC0000"></asp:RequiredFieldValidator>
            </div>
            <div class="mb-3">
                Contraseña:
                <asp:TextBox ID="passwordText" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="passwordRequerido" runat="server" ControlToValidate="passwordText" Display="None" ErrorMessage="Error Introuzca una contraseña" ForeColor="#CC0000"></asp:RequiredFieldValidator>
            </div>
            <asp:Label ID="ErrorText" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ForeColor="#CC0000" /><br />
            <asp:Button CssClass="btn btn-outline-success me-5 mb-2" ID="LoginButton" runat="server" OnClick="LoginButton_Click" Text="Iniciar Sesion" CausesValidation="False" />
            <asp:LinkButton ID="registreButton" runat="server" CausesValidation="False" PostBackUrl="Registro.aspx">registrarse</asp:LinkButton>
    </div>
    </div>
    <div class="modal fade" id="registroModal" tabindex="-1">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title">Registro exitoso</h5>
      </div>
      <div class="modal-body">
        Tu cuenta ha sido creada correctamente. Ya puedes iniciar sesión.
      </div>
    </div>
  </div>
</div>

<script>
    window.onload = function () {
        const params = new URLSearchParams(window.location.search);
        if (params.get("registro") === "ok") {
            var modal = new bootstrap.Modal(document.getElementById('registroModal'));
            modal.show();
        }
        // Eliminar el parámetro de la URL sin recargar
        params.delete("registro");
        const newUrl = params.toString()
            ? window.location.pathname + "?" + params.toString()
            : window.location.pathname;

        window.history.replaceState({}, "", newUrl);
    }
</script>
</asp:Content>
