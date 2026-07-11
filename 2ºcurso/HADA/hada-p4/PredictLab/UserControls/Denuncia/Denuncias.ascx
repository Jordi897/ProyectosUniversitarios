<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Denuncias.ascx.cs" Inherits="PredictLab.UserControls.Denuncia.Denuncias" %>

<div class="border rounded p-3 bg-light">

    <h5 class="fw-semibold mb-3">Denunciar comentario</h5>

    <div class="mb-3">
        <label class="form-label small text-muted">
            Tipo de denuncia
        </label>

        <asp:DropDownList 
            ID="causaDenuncia" 
            runat="server" 
            CssClass="form-select form-select-sm">

            <asp:ListItem Text="Contenido inapropiado" Value="Contenido inapropiado"></asp:ListItem>
            <asp:ListItem Text="Spam" Value="Spam"></asp:ListItem>
            <asp:ListItem Text="Lenguaje ofensivo" Value="Lenguaje ofensivo"></asp:ListItem>

        </asp:DropDownList>
    </div>

    <div class="mb-3">
        <label class="form-label small text-muted">
            Descripción
        </label>

        <asp:TextBox 
            ID="descripcionText" 
            runat="server" 
            TextMode="MultiLine" 
            Rows="3" 
            MaxLength="600"
            CssClass="form-control form-control-sm">
        </asp:TextBox>

        <asp:RequiredFieldValidator 
            ID="descripcionRequerida" 
            runat="server"
            ControlToValidate="descripcionText"
            ErrorMessage="Debe introducir una descripción."
            ForeColor="#CC0000"
            Display="None"
            ValidationGroup="denuncia">
        </asp:RequiredFieldValidator>
    </div>

    <asp:ValidationSummary 
        ID="ValidationSummary1" 
        runat="server" 
        DisplayMode="List" 
        ForeColor="#CC0000" 
        ValidationGroup="denuncia" />

    <asp:Button 
        ID="enviarDenunciaButton" 
        runat="server" 
        Text="Enviar denuncia" 
        CssClass="btn btn-sm btn-danger"
        OnClick="enviarDenunciaButton_Click" 
        ValidationGroup="denuncia" />

    <asp:Label 
        ID="ErrorText" 
        runat="server" 
        ForeColor="#CC0000" 
        Visible="False"
        CssClass="d-block mt-2">
    </asp:Label>

</div>