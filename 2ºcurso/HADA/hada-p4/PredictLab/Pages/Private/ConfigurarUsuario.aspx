<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="configurarUsuario.aspx.cs" Inherits="PredictLab.Pages.Private.configurarUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Style/ConfiguracionStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="containerUser d-flex">
        <section class="carta">
            <asp:Image ID="ImageFondo" runat="server" alt="Fondo de la carta" class="fondoCarta" ImageUrl="~/imagenes/fondoUser.png" />
            <asp:Image ID="ImagePerfil" runat="server" alt="Foto de perfil" class="fotoPerfil" ImageUrl="~/imagenes/icon-User.png"/>
            <div class="container justify-content-center align-items-center pb-2">
                <br />
                <h5>Configuracion de Cuenta</h5>
            <div class="mb-5">
                Correo Electronico:
                <asp:Label ID="emailText" CssClass="form-control" runat="server" Text=""></asp:Label>
            </div>
            <div class="mb-5">
                Nombre de Usuario:
                <asp:TextBox ID="nicknameText" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="nicknameRequerido" runat="server" ControlToValidate="nicknameText" ErrorMessage="Error se debe introucir un nombre de usuario" ForeColor="#CC0000" Display="None">Error se debe introucir un nombre de usuario</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="nicknameValidator" runat="server" ControlToValidate="nicknameText" ErrorMessage="Error el usuario tiene que tener entre 3 y 10 caracteres y no usar simbolos especiales" ForeColor="#CC0000" ValidationExpression="^[A-Za-z0-9]{3,10}$" Display="None">Error el usuario tiene que tener entre 3 y 10 caracteres y no usar simbolos especiales</asp:RegularExpressionValidator>
            </div>
            <div class="mb-5">
                Nombre Real:
                <asp:TextBox ID="nombreText" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="realNameRequerido" runat="server" ControlToValidate="nombreText" ErrorMessage="Error es necasario que introuzca su nombre" ForeColor="#CC0000" Display="None">Error es necasario que introuzca su nombre</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="realnameValidator" runat="server" ControlToValidate="nombreText" ErrorMessage="Error introduzca su nombre real porfavor" ForeColor="#CC0000" ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ]+$" Display="None">Error introduzca su nombre real porfavor</asp:RegularExpressionValidator>
            </div>
            <div class="mb-5">
            Apellidos:
                <asp:TextBox ID="ApellidosText" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ApellidosRequerido" runat="server" ControlToValidate="ApellidosText" ErrorMessage="Error intoduzca un apellido" ForeColor="#CC0000" Display="None">Error intoduzca un apellido</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="ApellidosValidator" runat="server" ControlToValidate="ApellidosText" ErrorMessage="Error porfavor introduzca su apellido o apellidos correctamente" ForeColor="#CC0000" ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ ]+$" Display="None">Error porfavor introduzca su apellido o apellidos correctamente</asp:RegularExpressionValidator>
            </div>
            <div class="mb-5">
            Telefono:
            <asp:TextBox ID="TelefonoText" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RegularExpressionValidator ID="telefonoValidator" runat="server" ControlToValidate="TelefonoText" ErrorMessage="Error el formato del numero no es correcto, solo se permiten numeros de españa" ForeColor="#CC0000" ValidationExpression="^(\+34)?[6789]\d{8}$" Display="None">Error el formato del numero no es correcto, solo se permiten numeros de españa</asp:RegularExpressionValidator>
            </div>
                <div>
                    Foto de Perfil:
                    <asp:FileUpload ID="FileFoto" runat="server" accept="image/*" />
                </div>
            <asp:Label ID="ErrorText" runat="server" ForeColor="#CC0000"></asp:Label>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ForeColor="#CC0000" />
            <br />
            <div>
                <!-- Button trigger modal -->
<button type="button" class="btn btn-outline-success" data-bs-toggle="modal" data-bs-target="#save">
  Guardar Cambios
</button>


<!-- Modal -->
<div class="modal fade" id="save" tabindex="-1" aria-labelledby="saveLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="saveLabel">Seguro que Quiere Guardar los cambios?</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        Los cambios se guardaran y se aplicaran a su cuenta, si desea continuar pulse el boton de guardar cambios, de lo contrario pulse cerrar para cancelar los cambios realizados.
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <asp:Button ID="GuardarCambios" runat="server" CssClass="btn btn-primary" Text="Guardar Cambios" OnClick="GuardarCambios_Click"/>
      </div>
    </div>
  </div>
</div>
            </div>
            </div>
        </section>
    </div>
</asp:Content>
