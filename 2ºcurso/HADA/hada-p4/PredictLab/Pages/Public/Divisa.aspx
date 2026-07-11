<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="Divisa.aspx.cs" Inherits="PredictLab.Pages.Public.Divisa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <div class="container mt-4 w-50">
         <h3 class="mb-4 text-primary">Conversor de Divisas</h3>

         <div class="alert alert-secondary mb-4 shadow-sm">
             <p class="mb-0">
                 Usa el botón "Convertir" para comprobar la cantidad de monedas virtuales equivalentes a una cantidad de la
                 divisa elegida. Para hacer la recarga real en tu perfil, pulsa en el botón "Conversión real". Si la divisa no se encuentra disponible, contacta con soporte para
                 más información.
             </p>
         </div>
        <asp:Panel ID="PanelDelFormulario" runat="server">
         <div class="card shadow-sm">
             <div class="card-body">
                 <div class="d-flex flex-column">
                    <div class="d-flex align-items-start">

                     <div class="flex-grow-1">
                         <div class="mb-5">
                             <label for="Moneda" class="form-label fw-bold">Moneda a Convertir:</label>
                             <p class="text-muted mb-2"> - Introduce el símbolo correspondiente a la divisa a convertir</p>                        
                             <div class="input-group">
                                 <asp:TextBox ID="Moneda" runat="server" CssClass="form-control" placeholder="ej:$,€,etc" AutoPostBack="true">   </asp:TextBox>
                                    <asp:DropDownList ID="SeleccionDivisa" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="SeleccionarDivisa">
                                        <asp:ListItem Value="">Seleccionar </asp:ListItem>
                                        <asp:ListItem Value="GBP">GBP (Libra) </asp:ListItem>
                                        <asp:ListItem Value="USD">USD (Dolar) </asp:ListItem>   
                                        <asp:ListItem Value="EUR">EUR (Euro) </asp:ListItem>
                                     </asp:DropDownList>
                             </div>
                        </div>   
                             <div class="mb-5">
                                 <label for="Cantidad" class="form-label fw-bold">Cantidad:</label>
                                 <p class="text-muted mb-2"> - Introduce la cantidad de la moneda a convertir</p>
                                  <asp:TextBox ID="Cantidad" runat="server" CssClass="form-control" placeholder="(Introduzca la cantidad)" AutoPostBack="true">   </asp:TextBox>
                                <div class="mt-3 d-flex flex-wrap gap-2" role="group" aria-label="EleccionCantidades">
                                    <asp:Button ID="amt5" runat="server" CssClass="btn btn-light border btn-sm" Text="5"
                                        OnClick="CantidadRapidaClick" CommandArgument="5" />
                                    <asp:Button ID="amt10" runat="server" CssClass="btn btn-light border btn-sm" Text="10"
                                        OnClick="CantidadRapidaClick" CommandArgument="10" />
                                    <asp:Button ID="amt25" runat="server" CssClass="btn btn-light border btn-sm" Text="25"
                                        OnClick="CantidadRapidaClick" CommandArgument="25" />
                                    <asp:Button ID="amt50" runat="server" CssClass="btn btn-light border btn-sm" Text="50"
                                        OnClick="CantidadRapidaClick" CommandArgument="50" />
                                    <asp:Button ID="amt100" runat="server" CssClass="btn btn-light border btn-sm" Text="100"
                                        OnClick="CantidadRapidaClick" CommandArgument="100" />                     
                                </div>                     
                             </div>
                             <div class="mt-4 d-flex gap-2">
                                 <asp:Button ID="busqueda" runat="server" CssClass="btn btn-primary w-50" Text="Convertir" OnClick="Click_Convertir" ></asp:Button>
                                 <asp:Button ID="ConversionReal" runat="server" CssClass="btn btn-primary w-50" Text="Hacer conversión real" OnClick="ClickConversionReal" />
                             </div>
                         </div>

                        <asp:Panel ID="PanelDeResultado" runat="server" CssClass="ms-4" Visible="false">
                            <div class="border rounded p-3" style="min-width:220px">
                                <asp:Label ID="ResultadoLabel" runat="server" CssClass="h6 d-block mb-2"></asp:Label>
                     </div>
                    </asp:Panel>
                 </div>
                </div>
                </div>
             </div>
            </asp:Panel>
            <asp:Panel ID="PanelConfirmacion" runat="server" Visible="false">
                <div class="card shadow-sm border-primary">
                    <div class="card page-header bg-primary text-white">
                        <h5 class="mb-0"> Confirmar conversión real</h5>
                    </div>
                    <div class="card-body">
                        <p class="text-muted-muted"> Estas seguro que quieres hacer la recarga?</p>
                        <ul class="list-group mb-4">
                            <li class="list-group-item d-flex justify-content-between align-items-center">
                                <span class="fw-bold">Divisa</span>
                                <asp:Label ID="ConfirmacionDivisa" runat="server" CssClass="page-badge bg-secondary fs-6"></asp:Label>
                            </li>
                            <li class="list-group-item d-flex justify-content-between align-items-center">
                                <span class="fw-bold">Cantidad del cambio</span>
                                <asp:Label ID="ConfirmacionCantidad" runat="server" CssClass="page-badge bg-secondary fs-6"></asp:Label>
                            </li>
                            <li class="list-group-item d-flex justify-content-between align-items-center list-group-item-success">
                                <span class="fw-bold">Monedas virtuales correspondientes</span>
                                <asp:Label ID="ConfirmacionMonedasVirtuales" runat="server" CssClass="page-badge bg-success fs-6"></asp:Label>
                            </li>
                        </ul>
                        <div class="d-flex gap-2">
                            <asp:Button ID="BotonCancelar" runat="server" CssClass="btn btn-outline-secondary w-50" Text="Cancelar" OnClick="CancelarClick" />
                            <asp:Button ID="BotonConfirmar" runat="server" CssClass="btn btn-success w-50" Text="Confirmar" OnClick="ConfirmarClick" />
                            
                            <div id="paypal-button-container"><p>Solo permite pago en €</p></div>

                            <script src="https://www.paypal.com/sdk/js?client-id=sb"></script>

                            <script src="../../scripts/PayPalAPI.js"></script>
                        </div>
                    </div>
                </div>
            </asp:Panel>       
     
            <asp:Panel ID="PanelFinal" runat="server" Visible="false" CssClass="mt-3">
                <asp:Label ID="ResultadoFinal" runat="server"></asp:Label>
            </asp:Panel>
     </div>
            
        </asp:Content>








