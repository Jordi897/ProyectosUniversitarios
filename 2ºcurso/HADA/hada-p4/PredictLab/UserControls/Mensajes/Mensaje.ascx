<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Mensaje.ascx.cs" Inherits="PredictLab.UserControls.Mensaje.Mensaje" %>
<link href="../../Style/mensajeStyle.css" rel="stylesheet" />
<div class="card contenido mb-3 h-100 d-flex flex-column overflow-auto">
    <div class="card-header cabeza textoPrincipal">Mensajes con <asp:Label ID="Chat" runat="server" Text=""></asp:Label></div>
    
    <div class="card-body container textoPrincipal scroll-propio" runat="server">
        
            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                <ItemTemplate>
                    <div ID="Mensaje" class="card mensaje" data-origen="" runat="server">
                        <div ID="cabeza-mensaje" class="card-header cabeza" >
                            <h5 class="modal-title"><asp:Label ID="UsuarioNick" runat="server" Text="" ></asp:Label></h5>
                        </div>
                        <div class="card-body">
                            <pre class="card-text text-wrap"><asp:Label ID="Texto" runat="server" Text=""></asp:Label></pre>
                        </div>
                        <div class="card-footer text-body-secondary">
                            <p><asp:Label ID="Fecha" runat="server" Text=""></asp:Label></p>
                        </div>
                    </div>
    
                </ItemTemplate>
            </asp:Repeater>
         </div>
        <div class="input-group mt-2">
            <asp:TextBox ID="txtNuevoMensaje" runat="server" CssClass="form-control" placeholder="Escribe un mensaje..." TextMode = "MultiLine"></asp:TextBox>
            <div class="input-group-append">
                <asp:Button ID="btnEnviar" runat="server" CssClass="btn boton" Text="Enviar" OnClick="btnEnviar_Click" />
            </div>
        </div>
    </div>
