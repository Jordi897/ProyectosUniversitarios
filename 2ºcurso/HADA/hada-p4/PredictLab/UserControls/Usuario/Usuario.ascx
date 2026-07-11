<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Usuario.ascx.cs" Inherits="PredictLab.UserControls.Usuario.Usuario" %>
<%@ Register Src="../Estadistica/Estadisticas.ascx" TagPrefix="es" TagName="Estadisticas" %>
<div class="container">
        <section class="carta">
            <asp:Image ID="ImageFondo" runat="server" alt="Fondo de la carta" class="fondoCarta" ImageUrl="~/imagenes/fondoUser.png" />

            <asp:Image ID="ImagePerfil" runat="server" alt="Foto de perfil" class="fotoPerfil" ImageUrl="~/imagenes/icon-User.png" />

            <p><asp:Label ID="NickName" runat="server" Text="Nickname"></asp:Label></p>
            
            <p><asp:Label ID="NombreApellidos" runat="server" Text="Nombre y apellidos"></asp:Label></p>

            <p>Wallet:<asp:Label ID="Wallet" runat="server" Text="100"></asp:Label></p>

            <div class="container-fluid">
                <asp:LinkButton ID="ConfigurarUser" runat="server" PostBackUrl="~/Pages/Private/ConfigurarUsuario.aspx">Configurar Usuario</asp:LinkButton>
                <button type="button" class="btn btn-primary float-end" data-bs-toggle="offcanvas" data-bs-target="#offcanvasNotification" aria-controls="offcanvasNotification">
                    Notificaciones <span class="badge text-bg-secondary"><asp:Label ID="LblNumNotis" runat="server" Text="0" /></span>
                </button>
                <es:Estadisticas ID="EstUsuarios" runat="server" />
            </div>
        </section>
        <style>
            .container{
                width: 100%;
                height: 100%;
                display: flex;
                justify-content: center;
                align-items: center;
                border: 1px solid black;
            }
            .carta{
                width: 100%;
                border: 1px solid black;
                border-radius: 10px;
                overflow: hidden;
                display: flex;
                flex-direction: column;
                align-items: center;
                background: rgb(26,26,26);
                color: rgb(240, 240, 240);
            }
            .fondoCarta{
                margin-top: -7rem;
                width: 100%;
                height: 300px;
                z-index: 1;
            }
            .fotoPerfil{
                position: relative;
                object-fit: cover;
                margin-top: -4.5rem;
                border-radius: 50%;
                width: 150px;
                height: 150px;
                border: 2px solid black;
                z-index: 2;
            }
            .carta p:nth-child(3){
                margin-top: 0.5rem;
                font-size: 25px;
                font-weight: bold;
            }
            .carta p:nth-child(4){
                font-size: 20px;
                margin-top: -24px;
            }
            
        </style>
    <div class="offcanvas offcanvas-end text-bg-dark" tabindex="-1" id="offcanvasNotification" aria-labelledby="offcanvasExampleLabel">
  <div class="offcanvas-header">
    <h5 class="offcanvas-title" id="offcanvasNotificationLabel">Notificaciones</h5>
    <button type="button" class="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
  </div>
  <div class="offcanvas-body">
      <asp:Repeater ID="RepeaterNotificaciones" runat="server" OnItemCommand="RepeaterNotis_ItemCommand">
    <ItemTemplate>
        <div class="card mb-3">
            <div class="card-body">
                <div class="d-flex justify-content-between align-items-start">
                    <h5 class="card-title mb-1"><%# Eval("Titulo") %></h5>

                    <%-- Botón para marcar como leída (demo) --%>
                    <asp:LinkButton
                        ID="BtnMarcarLeida"
                        runat="server"
                        CommandName="MarcarLeida"
                        CommandArgument='<%# Eval("Id") %>'
                        CssClass="btn btn-sm btn-primary">
                        Marcar como leída
                    </asp:LinkButton>
                </div>

                <p class="card-text mb-1"><%# Eval("Mensaje") %></p>
                <small class="text-secondary"><%# Eval("Fecha", "{0:yyyy-MM-dd HH:mm}") %></small>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
<asp:HyperLink
    ID="LnkVerTodasNotificaciones"
    runat="server"
    NavigateUrl="~/Pages/Private/Notificaciones.aspx"
    CssClass="btn btn-outline-light w-100 mt-2">
    Ver todas las notificaciones
</asp:HyperLink>
  </div>
</div>
    </div>