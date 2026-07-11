<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="PredictLab.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../Style/AccesoStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid d-flex justify-content-center align-items-center min-vh-100 Acceso">
        <div class="card p-4 shadow w-100">
            <h3>
                Registrarte
            </h3>
            <div class="mb-5">
                Correo Electronico*:
                <asp:TextBox ID="emailText" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                <asp:RequiredFieldValidator ID="correoRequerido" runat="server" ControlToValidate="emailText" ErrorMessage="Error debes introducir un correo electronico" ForeColor="#CC0000" Display="None">Error deves introducir un correo electronico</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="EmailValidator" runat="server" ControlToValidate="emailText" ErrorMessage="Error no se introdujo un correo electronico valido" ForeColor="#CC0000" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="None">Error no se introdujo un correo electronico valido</asp:RegularExpressionValidator>
            </div>
            <div class="mb-5">
                Nombre de Usuario*:
                <asp:TextBox ID="nicknameText" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="nicknameRequerido" runat="server" ControlToValidate="nicknameText" ErrorMessage="Error se debe introucir un nombre de usuario" ForeColor="#CC0000" Display="None">Error se debe introucir un nombre de usuario</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="nicknameValidator" runat="server" ControlToValidate="nicknameText" ErrorMessage="Error el usuario tiene que tener entre 3 y 10 caracteres y no usar simbolos especiales" ForeColor="#CC0000" ValidationExpression="^[A-Za-z0-9]{3,10}$" Display="None">Error el usuario tiene que tener entre 3 y 10 caracteres y no usar simbolos especiales</asp:RegularExpressionValidator>
            </div>
            <div class="mb-5">
                Nombre Real*:
                <asp:TextBox ID="nombreText" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="realNameRequerido" runat="server" ControlToValidate="nombreText" ErrorMessage="Error es necasario que introuzca su nombre" ForeColor="#CC0000" Display="None">Error es necasario que introuzca su nombre</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="realnameValidator" runat="server" ControlToValidate="nombreText" ErrorMessage="Error introduzca su nombre real porfavor" ForeColor="#CC0000" ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ]+$" Display="None">Error introduzca su nombre real porfavor</asp:RegularExpressionValidator>
            </div>
            <div class="mb-5">
                Apellidos*:
                <asp:TextBox ID="ApellidosText" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ApellidosRequerido" runat="server" ControlToValidate="ApellidosText" ErrorMessage="Error intoduzca un apellido" ForeColor="#CC0000" Display="None">Error intoduzca un apellido</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="ApellidosValidator" runat="server" ControlToValidate="ApellidosText" ErrorMessage="Error porfavor introduzca su apellido o apellidos correctamente" ForeColor="#CC0000" ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ ]+$" Display="None">Error porfavor introduzca su apellido o apellidos correctamente</asp:RegularExpressionValidator>
            </div>
            <div class="mb-5">
                Telefono:
                <asp:TextBox ID="TelefonoText" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RegularExpressionValidator ID="telefonoValidator" runat="server" ControlToValidate="TelefonoText" ErrorMessage="Error el formato del numero no es correcto, solo se permiten numeros de españa" ForeColor="#CC0000" ValidationExpression="^(\+34)?[6789]\d{8}$" Display="None">Error el formato del numero no es correcto, solo se permiten numeros de españa</asp:RegularExpressionValidator>
            </div>
            <div class="mb-5">
                Contraseña*:<asp:TextBox ID="passwordText" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="passwordRequerida" runat="server" ControlToValidate="passwordText" ErrorMessage="Error tienes que introducir una contraseña" ForeColor="#CC0000" Display="None">Error tienes que introducir una contraseña</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="passwordValidator" runat="server" ControlToValidate="passwordText" ErrorMessage="Error contraseña debil o caracter no reconocido. La contraseña requiere de una letra mayuscula y minuscula, un caracter especial reconocible (@$!%*?&amp;) y un numero" ForeColor="#CC0000" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&amp;])[A-Za-z\d@$!%*?&amp;]{4,}$" Display="None">Error contraseña debil o caracter no reconocido</asp:RegularExpressionValidator>
            </div>
            <div class="mb-5">
                Repetir Contraseña*:<asp:TextBox ID="passwordTextRepiter" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                <asp:CompareValidator ID="passwordComparado" runat="server" ControlToCompare="passwordText" ControlToValidate="passwordTextRepiter" ErrorMessage="Error no son iguales las contraseñas" ForeColor="#CC0000" Display="None">Error no son iguales las contraseñas</asp:CompareValidator>
                <asp:RequiredFieldValidator ID="passwordRepitRequerido" runat="server" ControlToValidate="passwordTextRepiter" ErrorMessage="Error es necesario que repita la contraseña" ForeColor="#CC0000" Display="None">Error es necesario que repita la contraseña</asp:RequiredFieldValidator>
            </div>
            <asp:Label ID="ErrorText" runat="server" ForeColor="#CC0000"></asp:Label>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ForeColor="#CC0000" />
            <br />
            <asp:Button CssClass="btn btn-outline-success me-5 mb-2" ID="Button_Registrarse" runat="server" Text="Registrarse" OnClick="Button_Registrarse_Click" CausesValidation="False" />

            <asp:LinkButton ID="loginButton" runat="server" CausesValidation="False" PostBackUrl="Login.aspx">Ya Tengo una Cuenta</asp:LinkButton>
        </div>
    </div>
</asp:Content>
