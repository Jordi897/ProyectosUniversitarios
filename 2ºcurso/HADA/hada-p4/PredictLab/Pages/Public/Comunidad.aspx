<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="Comunidad.aspx.cs" Inherits="PredictLab.Pages.Public.Comunidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Style/searchStyle.css" rel="stylesheet" />
    <script>
        const ruta = '<%=ResolveUrl("~/")%>';
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid px-4 py-3">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h4 class="fw-semibold mb-0">Comunidades</h4>
            <asp:Button runat="server" ID="btnNuevaComunidad" CssClass="btn btn-success" Text="+ Nueva comunidad" OnClientClick="return false;" data-bs-toggle="modal" data-bs-target="#modalCrearComunidad" />
        </div>
        <div class="row g-4">
            <div class="col-12 col-lg-4">
                <div class="card border-0 shadow-sm">
                    <div class="card-body">
                        <div class="autocomplete">
                            <div class="input-group">
                                <asp:TextBox ID="SearchBox" CssClass="form-control me-2 buscador" data-tipo="predicciones" runat="server" TextMode="Search" placeholder="Nombre de la predicción"></asp:TextBox>
                                <div id="Sugerencias" class="Sugerencias"></div>
                                <asp:Button ID="Buscar" CssClass="btn btn-success" runat="server" Text="Buscar" OnClick="btnBuscar_Click"/>
                            </div>
                        </div>
                        <asp:Repeater ID="repeaterComunidades" runat="server">
                            <ItemTemplate>
                                <div class="comunidad-card border rounded p-3 mb-2">
                                    <div class="d-flex justify-content-between align-items-start">
                                        <div>
                                            <p class="fw-semibold mb-0"><%# Eval("Titulo") %></p>
                                            <p class="text-muted small mb-1"><%# Eval("Descripcion") %></p>
                                            <span class='badge <%# Eval("Voto").ToString() == "SI" ? "badge-si" : "badge-no" %>'>Voto: <%# Eval("Voto").ToString() %></span>
                                        </div>
                                        <asp:Button runat="server" CssClass="btn btn-sm btn-outline-success" Text="Ver" OnClick="verComunidad_Click" CommandArgument='<%# Eval("Id") %>' />
                                    </div>
                                    <div class="d-flex justify-content-between mt-2 small text-muted">
                                        <span>Cantidad: <%# Eval("Saldo") %></span>
                                        <span class="text-muted">Fecha Incursión: <%# Eval("FechaIncursion") %></span>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Label ID="labelError" runat="server" class="d-block text-center text-muted py-5" Visible="false">No hay predicciones en este momento.</asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-12 col-lg-8">
                <asp:HiddenField ID="hfComunidadSeleccionada" runat="server" />
                <asp:Panel runat="server" ID="pnlDetalle" Visible="false">
                    <div class="card border-0 shadow-sm mb-3">
                        <div class="card-body">
                            <div class="d-flex justify-content-between align-items-start mb-3">
                                <div>
                                    <h4 class="fw-semibold mb-1">
                                        <asp:Literal runat="server" ID="litTitulo" />
                                    </h4>
                                    <p class="text-muted mb-2">
                                        <asp:Literal runat="server" ID="litDescripcion" />
                                    </p>
                                    <div class="d-flex gap-2 flex-wrap">
                                        <span class="badge text-bg-secondary">
                                            Predicción: <asp:Literal runat="server" ID="litPrediccion" />
                                        </span>
                                        <asp:Label runat="server" ID="labelVotoComunidad" CssClass="fw-semibold"/>
                                        <span class="text-bg-light text-dark small">
                                            Cantidad: <asp:Literal runat="server" ID="litSaldo" />
                                        </span>
                                    </div>
                                </div>
                                <asp:Button runat="server" ID="btnUnirse" CssClass="btn btn-success" Text="Contribuir" OnClientClick="return false;" data-bs-toggle="modal" data-bs-target="#modalUnirse" />
                            </div>
                        </div>
                    </div>
                    <div class="card border-0 shadow-sm">
                        <div class="card-body">
                            <h6 class="fw-semibold mb-3">Predicción asociada</h6>
                            <p class="fw-medium mb-1">
                                <asp:Literal runat="server" ID="litTituloPrediccion" />
                            </p>
                            <p class="text-muted small mb-2">
                                <asp:Literal runat="server" ID="litDescripcionPrediccion" />
                            </p>
                            <asp:HiddenField ID="hfProbSi" runat="server" Value="50" />
                            <asp:HiddenField ID="hfProbNo" runat="server" Value="50" />
                            <div class="progress mb-2" style="height:10px;">
                                <div class="progress-bar bg-success" role="progressbar" id="barraPrediccionSi"></div>
                                <div class="progress-bar bg-danger" role="progressbar" id="barraPrediccionNo"></div>
                            </div>
                            <div class="d-flex justify-content-between small">
                                <span class="text-success fw-semibold">
                                    Probabilidad: <asp:Literal runat="server" ID="litProbabilidad" />%
                                </span>
                                <span class="text-muted">Fecha Límite: <asp:Literal runat="server" ID="litFechaFin" /></span>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlPlaceholder">
                    <div class="card border-0 shadow-sm text-center py-5 text-muted">
                        <p class="mb-1">Selecciona una comunidad de la lista</p>
                        <p class="mb-1">o crea una nueva para apostar en grupo</p>
                    </div>
                </asp:Panel>
                <asp:Label runat="server" ID="labelErrorDetalle" CssClass="text-danger small mt-2 d-block" Visible="false" />
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalCrearComunidad" tabindex="-1" data-bs-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title fw-semibold">Nueva comunidad</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Título <small class="text-muted">(máx. 100 caracteres)</small></label>
                        <asp:TextBox runat="server" ID="txtTituloNueva" CssClass="form-control" MaxLength="100" placeholder="Nombre de la comunidad" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Descripción <small class="text-muted">(máx. 200 caracteres)</small></label>
                        <asp:TextBox runat="server" ID="txtDescripcionNueva" CssClass="form-control" MaxLength="200" placeholder="Descripción de la comunidad" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Predicción</label>
                        <asp:DropDownList runat="server" ID="ddlPrediccion" CssClass="form-select" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Voto de la comunidad</label>
                        <asp:HiddenField ID="hfLadoResolver" runat="server" />
                        <div class="row g-2 mb-3">
                            <div class="col-6">
                                <button type="button" id="btnModalSi" class="btn btn-outline-success w-100" onclick="seleccionarLado('SI')">SI</button>
                            </div>
                            <div class="col-6">
                                <button type="button" id="btnModalNo" class="btn btn-outline-danger w-100" onclick="seleccionarLado('NO')"> NO</button>
                            </div>
                        </div>
                    </div>
                    <asp:Label runat="server" ID="labelErrorModal" CssClass="text-danger small" Visible="false" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button runat="server" ID="btnCrearComunidad" CssClass="btn btn-success" Text="Crear comunidad" OnClick="btnCrearComunidad_Click" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalUnirse" tabindex="-1" data-bs-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title fw-semibold">Contribuir a la comunidad</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <p class="text-muted small mb-3">La cantidad apostada se sumará al saldo de la comunidad y al ganar conseguirás tu parte proporcional.</p>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Cantidad</label>
                        <asp:TextBox runat="server" ID="txtAportacion" CssClass="form-control" TextMode="Number" />
                    </div>
                    <div class="d-flex gap-2 flex-wrap mb-2">
                        <button type="button" class="btn btn-sm btn-outline-secondary" onclick="setAportacion(5)">5</button>
                        <button type="button" class="btn btn-sm btn-outline-secondary" onclick="setAportacion(10)">10</button>
                        <button type="button" class="btn btn-sm btn-outline-secondary" onclick="setAportacion(25)">25</button>
                        <button type="button" class="btn btn-sm btn-outline-secondary" onclick="setAportacion(50)">50</button>
                        <button type="button" class="btn btn-sm btn-outline-secondary" onclick="setAportacion(100)">100</button>
                    </div>
                    <asp:Label runat="server" ID="labelErrorUnirse" CssClass="text-danger small" Visible="false" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button runat="server" ID="btnConfirmarAportacion" CssClass="btn btn-success" Text="Contribuir" OnClick="btnConfirmarAportacion_Click" />
                </div>
            </div>
        </div>
    </div>
    <script>
        function setAportacion(v)
        {
            document.getElementById('<%= txtAportacion.ClientID %>').value = v;
        }

        function seleccionarLado(lado) {
            document.getElementById('<%= hfLadoResolver.ClientID %>').value = lado;

            if (lado === 'SI') {
                document.getElementById('btnModalSi').className = 'btn btn-success w-100';
                document.getElementById('btnModalNo').className = 'btn btn-outline-danger w-100';
            }
            else {
                document.getElementById('btnModalNo').className = 'btn btn-danger w-100';
                document.getElementById('btnModalSi').className = 'btn btn-outline-success w-100';
            }
        }

        function actualizarBarras() {
            var si = document.getElementById('<%= hfProbSi.ClientID %>').value;
            var no = document.getElementById('<%= hfProbNo.ClientID %>').value;
            document.getElementById('barraPrediccionSi').style.width = si + '%';
            document.getElementById('barraPrediccionNo').style.width = no + '%';
        }
        window.addEventListener('load', actualizarBarras);
    </script>
    <script src="../../scripts/search.js"></script>
</asp:Content>
