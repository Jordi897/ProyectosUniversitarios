<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Comentarios.ascx.cs" Inherits="PredictLab.UserControls.Comentario.Comentarios" %>
<%@ Register Src="~/UserControls/Denuncia/Denuncias.ascx" TagPrefix="uc" TagName="Denuncias" %>

<div class="card border-0 bg-white shadow-sm p-3 w-100">

    <h3 class="card-title fw-semibold mb-3 text-center">Comentarios</h3>

    <div class="mb-3" style="max-height: 420px; overflow-y: auto;">

        <asp:Repeater 
            ID="comentariosRepeater" 
            runat="server"
            OnItemCommand="comentariosRepeater_ItemCommand">

            <ItemTemplate>
                <div class="border-bottom pb-3 mb-3">
                    <div class="d-flex justify-content-between align-items-center mb-1">
                        <span class="fw-semibold">
                            <%# Eval("nickname") %>
                        </span>

                        <span class="small text-muted">
                            <%# Convert.ToDateTime(Eval("fecha")).ToString("dd/MM/yyyy HH:mm") %>
                        </span>
                    </div>

                    <p class="mb-2">
                        <%# Eval("mensaje") %>
                    </p>

                    <asp:Button 
                        ID="denunciarButton" 
                        runat="server"
                        Text="Denunciar"
                        CssClass="btn btn-sm btn-outline-danger me-1"
                        CommandName="Denunciar"
                        CommandArgument='<%# Eval("id") %>'
                        CausesValidation="false" />

                    <asp:Button 
                        ID="borrarButton" 
                        runat="server"
                        Text="Borrar"
                        CssClass="btn btn-sm btn-outline-secondary"
                        CommandName="Borrar"
                        CommandArgument='<%# Eval("id") %>'
                        CausesValidation="false"
                        Visible='<%# PuedeModificarComentario(Eval("usuario")) %>' />
                </div>
            </ItemTemplate>

        </asp:Repeater>

    </div>

    <asp:Panel 
        ID="panelDenunciaComentario" 
        runat="server" 
        Visible="false"
        CssClass="mt-3 mb-3">

        <uc:Denuncias ID="DenunciasControl" runat="server" />

    </asp:Panel>

    <hr />

    <div class="mb-3">
        <label for="<%= comentarioText.ClientID %>" class="form-label small text-muted">
            Añadir comentario
        </label>

        <asp:TextBox 
            ID="comentarioText" 
            runat="server" 
            TextMode="MultiLine" 
            Rows="4" 
            MaxLength="500"
            CssClass="form-control">
        </asp:TextBox>

        <asp:RequiredFieldValidator 
            ID="comentarioRequerido" 
            runat="server"
            ControlToValidate="comentarioText"
            ErrorMessage="Debe introducir un comentario."
            ForeColor="#CC0000"
            Display="None"
            ValidationGroup="comentarios">
        </asp:RequiredFieldValidator>
    </div>

    <asp:ValidationSummary 
        ID="ValidationSummary1" 
        runat="server" 
        DisplayMode="List" 
        ForeColor="#CC0000"
        ValidationGroup="comentarios" />

    <asp:Label 
        ID="ErrorText" 
        runat="server" 
        ForeColor="#CC0000" 
        Visible="False">
    </asp:Label>

    <asp:Button 
        ID="publicarButton" 
        runat="server" 
        Text="Publicar comentario" 
        CssClass="btn btn-outline-success mt-2 w-100"
        ValidationGroup="comentarios"
        OnClick="publicarButton_Click" />

</div>