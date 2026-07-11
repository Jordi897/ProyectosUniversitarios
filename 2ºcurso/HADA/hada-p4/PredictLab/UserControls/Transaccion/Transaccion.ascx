<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Transaccion.ascx.cs" Inherits="PredictLab.UserControls.Transaccion.Transaccion" %>

    
<div>
  <div class="card p-0 shadow-sm w-100">
      <div class="card-header d-flex justify-content-between align-items-center">
      <h5 class="mb-0 text-primary">Transacción:</h5>
                    <div class="dropdown">
                          <button class="btn btn-sm btn-outline-primary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                                Filtrar por:
                             </button>
                          <ul class="dropdown-menu">
                            <li><asp:LinkButton runat="server" CssClass="dropdown-item" OnClick="Todas_Filtrar">Todas</asp:LinkButton></li>
                            <li><asp:LinkButton runat="server" CssClass="dropdown-item" OnClick="Gana_Filtrar">Finalización de Predicción</asp:LinkButton></li>
                            <li><asp:LinkButton runat="server" CssClass="dropdown-item" OnClick="Divisa_Filtrar">Cambio de Divisas</asp:LinkButton></li>
                            <li><asp:LinkButton runat="server" CssClass="dropdown-item" OnClick="Apuesta_Filtrar">Apuesta</asp:LinkButton></li>
                          </ul>
                   </div>
       </div>
      <div class="card-body d-flex flex-column">

          

         <asp:Panel ID="PanelVacio" runat="server" CssClass="text-center text-muted mb-3" Visible="false" aria-live="polite">
             <div class="py-3">No hay transacciones para mostrar</div>
         </asp:Panel>

          <div id="ContenidoTransaccion" runat="server">
              <div class="row mb-2">
              <div class="col-4 fw-bold">Id:</div>
              <div class="col-8">
              <asp:Label ID="Id" runat="server" CssClass="form-control-plaintext" Text="-" />
              <p class="text-muted small mb-0"> - Identificador único de la transacción por usuario</p>
          </div>
      </div>
 
      <div class="row mb-2">
         <div class="col-4 fw-bold">Fecha:</div>
           <div class="col-8">
     <asp:Label ID="Fecha" runat="server" CssClass="form-control-plaintext" Text="-" />
            <p class="text-muted small mb-0"> - Fecha de la operación</p>
         </div>
      </div>

      <div class="row mb-2">
        <div class="col-4 fw-bold">Cantidad:</div>
                <div class="col-8">
        <asp:Label ID="CantidadGastada" runat="server" CssClass="form-control-plaintext" Text="-" />
            <p class="text-muted small mb-0"> - Importe gastado/obtenido (en el caso de una conversión o una resolución de predicción) en la operación</p>
          </div>
     </div>

       <div id="FilaVoto" runat="server" class="row mb-2">
        <div class="col-4 fw-bold">Voto:</div>
              <div class="col-8">
         <asp:Label ID="Voto" runat="server" CssClass="form-control-plaintext" Text="-" />
          <p class="text-muted small mb-0"> - Opción elegida en la predicción</p>
            </div>
     </div>


       <div id="FilaPrediccion" runat="server" class="row mb-2">
   <div class="col-4 fw-bold">Predicción:</div>
           <div class="col-8">
   <asp:Label ID="Prediccion" runat="server" CssClass="form-control-plaintext" Text="-" />
          <p class="text-muted small mb-0"> - Referencia asociada a la predicción (ordenadas por antiguedad)</p> 
      </div>
</div>

       <div id="FilaDivisa" runat="server" class="row mb-2">
   <div class="col-4 fw-bold">Divisa:</div>
           <div class="col-8">
   <asp:Label ID="Divisa" runat="server" CssClass="form-control-plaintext" Text="-" />
         <p class="text-muted small mb-0"> - Divisa implicada en la conversión</p>
     </div>
</div>

     
   </div>

      <div class="row mt-3">

      <div class="col-6 d-flex justify-content-start">
          <div class="btn-group" role="group" aria-label="Navegacion transacciones">
              <asp:Button ID="Anterior" runat="server" CssClass="btn btn-outline-primary" Text="Anterior" OnClick="Click_Anterior"/>
              <asp:Button ID="Posterior" runat="server" CssClass="btn btn-outline-primary" Text="Posterior" OnClick="Click_Posterior"/>
          </div>
      </div>

       <div class="col-6 d-flex justify-content-end">
        <div class="btn-group-vertical">
            <asp:Button ID="Primera" runat="server" CssClass="btn btn-outline-primary mb-1" Text="Primera" OnClick="Click_Primera" />
            <asp:Button ID="Ultima" runat="server" CssClass="btn btn-outline-primary" Text="Ultima" OnClick="Click_Ultima" />
        </div>       

     
         </div>
      </div>

      </div>
    </div>

   </div>
      
