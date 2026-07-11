<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="Prediccion.aspx.cs" Inherits="PredictLab.Pages.Public.Prediccion" %>
<%@ Register Src="~/UserControls/Comentario/Comentarios.ascx" TagPrefix="uc" TagName="Comentarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid py-4 px-4">
        <div class="row g-4 align-items-start">
            <div class="col-12 col-lg-7">
                <div class="d-flex justify-content-between align-items-center mb-3">
                    <a href="../Public/Default.aspx" class="btn btn-sm btn-outline-secondary">← Volver a predicciones</a>
                    <asp:Button runat="server" ID="btnResolverPrediccion" CssClass="btn btn-sm btn-outline-success" Text="Resolver predicción" Visible="true" OnClientClick="return false;" data-bs-toggle="modal" data-bs-target="#modalResolver" />
                </div>
                <div class="mb-4">
                    <span class="badge text-bg-secondary mb-2">
                        <asp:Literal runat="server" ID="labelCategoria" Text="Categoría"></asp:Literal>
                    </span>
                    <br />
                    <h3 class="fw-semibold mb-2">
                        <asp:Literal runat="server" ID="labelTitulo" Text="Título"></asp:Literal>
                    </h3>
                    <span class="text-muted">
                        <asp:Literal runat="server" ID="labelPregunta" Text="Pregunta"></asp:Literal>
                    </span>
                </div>
                <div class="row g-3 mb-4">
                    <div class="col-4">
                        <div class="card text-center border-0 bg-white shadow-sm h-100">
                            <span class="text-muted">Probabilidad</span>
                            <h4 class="mb-0 text-success fw-semibold">
                                <span id="spanProb"><asp:Literal runat="server" ID="labelProb" Text="50"></asp:Literal></span>%
                            </h4>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="card text-center border-0 bg-white shadow-sm h-100">
                            <span class="text-muted">Cantidad Total</span>
                            <h4 class="mb-0 fw-semibold">
                                <asp:Literal runat="server" ID="labelCantidadApostada" Text="Yeah"></asp:Literal>
                            </h4>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="card text-center border-0 bg-white shadow-sm h-100">
                            <span class="text-muted">Fecha Límite</span>
                            <h4 class="mb-0 fw-semibold">
                                <asp:Literal runat="server" ID="labelFechaLim" Text="Yeah"></asp:Literal>
                            </h4>
                        </div>
                    </div>
                </div>
                <div class="card border-0 bg-white shadow-sm">
                    <asp:HiddenField ID="hfTotalSi" runat="server" />
                    <asp:HiddenField ID="hfTotalNo" runat="server" />
                    <h5 class="card-title fw-semibold mb-3">Hacer predicción</h5>
                    <div class="row g-2 mb-3">
                        <div class="col-6">
                            <asp:Button ID="btnSi" runat="server" CssClass="btn btn-outline-success w-100 side-btn" Text="SI" OnClick="btnSi_Click"></asp:Button>
                        </div>
                        <div class="col-6">
                            <asp:Button ID="btnNo" runat="server" CssClass="btn btn-outline-danger w-100 side-btn" Text="NO" OnClick="btnNo_Click"></asp:Button>
                        </div>
                    </div>
                    <span class="form-label small text-muted">Cantidad</span>
                    <br />
                    <asp:TextBox runat="server" ID="txtCantidad" TextMode="Number" class="form-control amount-input mb-2"></asp:TextBox>
                    <div class="d-flex gap-2 flex-wrap mb-3">
                        <button type="button" class="btn btn-sm btn-outline-secondary" onclick="setAportacion(5)">5</button>
                        <button type="button" class="btn btn-sm btn-outline-secondary" onclick="setAportacion(10)">10</button>
                        <button type="button" class="btn btn-sm btn-outline-secondary" onclick="setAportacion(25)">25</button>
                        <button type="button" class="btn btn-sm btn-outline-secondary" onclick="setAportacion(50)">50</button>
                        <button type="button" class="btn btn-sm btn-outline-secondary" onclick="setAportacion(100)">100</button>
                    </div>
                    <br />
                    <div class="rounded p-3 mb-3">
                        <div class="d-flex justify-content-between border-bottom pb-2 mb-2 small">
                            <span class="text-muted">Apuesta</span>
                            <asp:Label ID="labelApuesta" runat="server" class="fw-semibold text-success" Text=""></asp:Label>
                        </div>
                        <div class="d-flex justify-content-between border-bottom pb-2 mb-2 small">
                            <span class="text-muted">Acciones</span>
                            <asp:Label ID="labelAcciones" runat="server" class="text-muted" Text="0"></asp:Label>
                        </div>
                        <div class="d-flex justify-content-between small">
                            <span class="text-muted">Ganancia</span>
                            <asp:Label ID="labelGanancia" runat="server" class="text-muted" Text="0"></asp:Label>
                        </div>
                    </div>
                    <asp:Label ID="labelErrorApuesta" runat="server" CssClass="text-danger" Text="Ha ocurrido un error" Visible="false"></asp:Label>
                    <asp:Button runat="server" ID="btnApostar" Text="Predecir" OnClick="btnApostar_Click"  CssClass="btn btn-success w-100 fw-semibold" disabled="disabled" />
                </div>
            </div>
            <div class="col-12 col-lg-5 align-self-start">
                <uc:Comentarios ID="ComentariosControl" runat="server" />
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalResolver" tabindex="-1" data-bs-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title fw-semibold">Resolver predicción</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <div class="bg-light rounded p-3 mb-4">
                        <p class="fw-semibold mb-1">
                            <asp:Literal runat="server" ID="modalTitulo" />
                        </p>
                        <p class="text-muted small mb-1">
                            <asp:Literal runat="server" ID="modalPregunta" />
                        </p>
                        <div class="d-flex gap-3 mt-2 small">
                            <span>Cantidad Total: <asp:Literal runat="server" ID="modalCantidad"></asp:Literal></span>
                            <span class="text-success">SÍ: <asp:Literal runat="server" ID="modalSi" />€</span>
                            <span class="text-danger">NO: <asp:Literal runat="server" ID="modalNo" />€</span>
                        </div>
                    </div>
                    <p class="fw-semibold mb-2">¿Cuál es el resultado?</p>
                    <asp:HiddenField ID="hfLadoResolver" runat="server" />
                    <div class="row g-2 mb-3">
                        <div class="col-6">
                            <button type="button" id="btnModalSi" class="btn btn-outline-success w-100" onclick="seleccionarLado('SI')">SI</button>
                        </div>
                        <div class="col-6">
                            <button type="button" id="btnModalNo" class="btn btn-outline-danger w-100" onclick="seleccionarLado('NO')"> NO</button>
                        </div>
                    </div>
                    <asp:Label runat="server" ID="labelErrorResolver" CssClass="text-danger" Visible="false" />
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button runat="server" ID="btnConfirmarResolucion" CssClass="btn btn-success" Text="Confirmar resolución" OnClick="btnConfirmarResolucion_Click" />
                </div>
            </div>
        </div>
    </div>
        </div>
<script>
    function seleccionarLado(lado)
    {
        document.getElementById('<%= hfLadoResolver.ClientID %>').value = lado;

        if (lado === 'SI')
        {
            document.getElementById('btnModalSi').className = 'btn btn-success w-100';
            document.getElementById('btnModalNo').className = 'btn btn-outline-danger w-100';
            document.getElementById('spanLadoResolver').textContent = 'SÍ ha ocurrido';
            document.getElementById('spanLadoResolver').className = 'fw-semibold text-success';
        }
        else
        {
            document.getElementById('btnModalNo').className = 'btn btn-danger w-100';
            document.getElementById('btnModalSi').className = 'btn btn-outline-success w-100';
            document.getElementById('spanLadoResolver').textContent = 'NO ha ocurrido';
            document.getElementById('spanLadoResolver').className = 'fw-semibold text-danger';
        }
        document.getElementById('pnlLadoResolver').style.display = 'block';
    }

    document.addEventListener('DOMContentLoaded', function () {
        document.getElementById('<%= txtCantidad.ClientID %>').addEventListener('input', calcularResumen);
    });

    function setAportacion(v) {
        document.getElementById('<%= txtCantidad.ClientID %>').value = v;
        calcularResumen();
    }

    function calcularResumen() {
        var cantidad = parseFloat(document.getElementById('<%= txtCantidad.ClientID %>').value) || 0;
        var lado = document.getElementById('<%= labelApuesta.ClientID %>').innerText.trim();
        var btnApostar = document.getElementById('<%= btnApostar.ClientID %>');

        if (cantidad <= 0 || (lado !== 'SI' && lado !== 'NO')) {
            document.getElementById('<%= labelAcciones.ClientID %>').innerText = '0';
            document.getElementById('<%= labelGanancia.ClientID %>').innerText = '0';
            btnApostar.disabled = true;
            return;
        }

        var totalSi = parseFloat(document.getElementById('<%= hfTotalSi.ClientID %>').value) || 0;
        var totalNo = parseFloat(document.getElementById('<%= hfTotalNo.ClientID %>').value) || 0;

        var nuevoTotalSi = totalSi + (lado === 'SI' ? cantidad : 0);
        var nuevoTotalNo = totalNo + (lado === 'NO' ? cantidad : 0);

        var beneficio, retorno;
        if (lado === 'SI') {
            beneficio = (cantidad / nuevoTotalSi) * nuevoTotalNo;
        } else {
            beneficio = (cantidad / nuevoTotalNo) * nuevoTotalSi;
        }
        retorno = cantidad + beneficio;

        document.getElementById('<%= labelAcciones.ClientID %>').innerText = cantidad.toFixed(2) + '€';
        document.getElementById('<%= labelGanancia.ClientID %>').innerText = '+' + beneficio.toFixed(2) + '€ (ganancia real: ' + retorno.toFixed(2) + '€)';
        btnApostar.disabled = false;
    }

    window.onload = function () {
        calcularResumen();
    };
</script>
</asp:Content>