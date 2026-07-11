<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Chats.ascx.cs" Inherits="PredictLab.UserControls.Chats.Chats" %>


<link href="../../Style/ChatsStyle.css" rel="stylesheet" />




<div class="card mb-3 h-100 d-flex flex-column contenido">

    <div class="card-header textoPrincipal">Chats privados</div>
        <div class="retenido">
            <asp:Panel runat="server" DefaultButton="botonBusqueda">
            
                <asp:TextBox ID="searchBox" CssClass="buscador textoSecundario w-75" data-tipo="usuario" runat="server" placeholder="Añade un chat" TextMode = "Search"></asp:TextBox>    
                <asp:Button ID="botonBusqueda" CssClass="boton ancho" runat="server" OnClick="botonBusqueda_Click" />
            </asp:Panel>
            <div id="Sugerencias" class="Sugerencias"> </div>
        </div>
        <div class="card-body p-0 flex-grow-1 overflow scroll-propio">

    
        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
            <ItemTemplate>
            
               <div class="p-2">
                <asp:Button 
                runat="server"
                CssClass="btn boton w-100 textoPrincipal"
                Text='<%# Eval("Usuario2.nickname") %>'
                CommandArgument='<%# Eval("Usuario2.nickname") %>'
                OnClick="Boton" />

             </div>
               
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
    <div ID="Modal" class="modal" tabindex="-1">
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js"></script>
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title">Error</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <p><asp:Label runat="server" ID="etiquetaModal"></asp:Label></p>
      </div>
    </div>
  </div>
</div>

<script src="../../scripts/search.js"></script>
<script src="../../scripts/modalChats.js"></script>
