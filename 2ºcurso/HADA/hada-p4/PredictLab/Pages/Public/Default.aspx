<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PredictLab.Default" StyleSheetTheme="" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Style/searchStyle.css" rel="stylesheet" />
    <script>
        const ruta = '<%=ResolveUrl("~/")%>';
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid px-4 py-3">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h4 class="fw-semibold mb-0">Predicciones activas:</h4>
            <div class="autocomplete">
                <div class="input-group">
                    <asp:TextBox ID="SearchBox" CssClass="form-control me-2 buscador" data-tipo="predicciones" runat="server" TextMode="Search" placeholder="Nombre de la predicción"></asp:TextBox>
                    <div id="Sugerencias" class="Sugerencias"></div>
                    <asp:Button ID="Buscar" CssClass="btn btn-success" runat="server" Text="Buscar" OnClick="btnBuscar_Click"/>
                </div>
            </div>
            <span class="badge text-bg-secondary fw-normal">
                <asp:Literal ID="labelTotal" runat="server">Predicciones: 0</asp:Literal>
            </span>
        </div>
        <div class="row g-3">
            <asp:Repeater ID="repeaterPredicciones" runat="server">
                <ItemTemplate>
                    <div class="col-12 col-md-6">
                        <a href='<%# ResolveUrl("~/Pages/Private/Prediccion.aspx?id=" + Eval("id")) %>' class="card h-100 shadow border-0 text-decoration-none text-dark card-prediccion">
                            <div class="card-body">
                                <div class="d-flex justify-content-between align-items-center mb-2">
                                    <span class="badge text-bg-light border text-secondary fw-normal">
                                        <%# Eval("categoria") %>
                                    </span>
                                    <span class="text-muted">Cantidad: <%# Eval("cantidadrecaudada") %></span>
                                </div>
                                <h3><%# Eval("titulo") %></h3>
                                <div class="progress mb-2" style="height: 10px">
                                    <div class="progress-bar bg-success" role="progressbar" style="width: <%# Math.Round((Convert.ToDecimal(Eval("votossi")) + Convert.ToDecimal(Eval("votosno"))) == 0 ? 50 : (Convert.ToDecimal(Eval("votossi")) * 100 / (Convert.ToDecimal(Eval("votossi")) + Convert.ToDecimal(Eval("votosno")))), 0) %>%" aria-valuenow="<%# Math.Round((Convert.ToDecimal(Eval("votossi")) + Convert.ToDecimal(Eval("votosno"))) == 0 ? 50 : (Convert.ToDecimal(Eval("votossi")) * 100 / (Convert.ToDecimal(Eval("votossi")) + Convert.ToDecimal(Eval("votosno")))), 0) %>" aria-valuemin="0" aria-valuemax="100"></div>
                                    <div class="progress-bar bg-danger" role="progressbar" style="width: <%# Math.Round((Convert.ToDecimal(Eval("votossi")) + Convert.ToDecimal(Eval("votosno"))) == 0 ? 50 : (Convert.ToDecimal(Eval("votosno")) * 100 / (Convert.ToDecimal(Eval("votossi")) + Convert.ToDecimal(Eval("votosno")))), 0) %>%" aria-valuenow="<%# Math.Round((Convert.ToDecimal(Eval("votossi")) + Convert.ToDecimal(Eval("votosno"))) == 0 ? 50 : (Convert.ToDecimal(Eval("votosno")) * 100 / (Convert.ToDecimal(Eval("votossi")) + Convert.ToDecimal(Eval("votosno")))), 0) %>" aria-valuemin="0" aria-valuemax="100"></div>
                                </div>
                                <div class="d-flex justify-content-between align-items-center">
                                    <span class="fw-semibold text-success">
                                        Probabilidad: <%# Math.Round((Convert.ToDecimal(Eval("votossi")) + Convert.ToDecimal(Eval("votosno"))) == 0 ? 50 : (Convert.ToDecimal(Eval("votossi")) * 100 / (Convert.ToDecimal(Eval("votossi")) + Convert.ToDecimal(Eval("votosno")))), 2) %>%
                                    </span>
                                    <span class="text-muted">Fecha Límite: <%# Eval("fechafin") %></span>
                                </div>
                            </div>
                        </a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <asp:Label ID="labelError" runat="server" class="d-block text-center text-muted py-5" Visible="false">No hay predicciones en este momento.</asp:Label>
        <div class="mt-4 d-flex justify-content-end">
            <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#modalCrear">+ Crear Predicción</button>
        </div>
        <div class="modal fade" id="modalCrear" tabindex="-1" data-bs-backdrop="static">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title fw-semibold">Nueva Predicción</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                    </div>
                    <div class="modal-body">
                        <div class="mb-3">
                            <asp:Label runat="server" class="form-label fw-semibold">Título <small class="text-muted">(máx. 100 caracteres)</small></asp:Label>
                            <asp:TextBox ID="txtTitulo" runat="server" class="form-control" placeholder="Título de la predicción"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <asp:Label runat="server" class="form-label fw-semibold">Descripción <small class="text-muted">(máx. 200 caracteres)</small></asp:Label>
                            <asp:TextBox ID="txtPrediccion" runat="server" class="form-control" placeholder="Descripcion de la predicción" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <asp:Label runat="server" class="form-label fw-semibold">Categoría:</asp:Label>
                            <asp:DropDownList runat="server" ID="txtCategoria" class="form-select"></asp:DropDownList>
                        </div>
                        <div class="mb-3">
                            <asp:Label runat="server" class="form-label fw-semibold">Fecha Límite:</asp:Label>
                            <asp:TextBox ID="txtFecha" runat="server" class="form-control" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <asp:Label runat="server" class="form-label fw-semibold">Hora Límite:</asp:Label>
                            <asp:TextBox ID="txtHora" runat="server" class="form-control" TextMode="Time"></asp:TextBox>
                        </div>
                        <asp:Label ID="labelModal" runat="server" class="text-danger" Visible="false" Text=""></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Cancelar</button>
                        <asp:Button ID="btnCrearPrediccion" runat="server" class="btn btn-success" Text="Crear predicción" OnClick="btnCrearPrediccion_Click"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <style>
        .card-prediccion {
            transition: transform 0.15s, box-shadow 0.15s;
        }
        .card-prediccion:hover {
            transform: translateY(-4px);
            box-shadow: 0 8px 24px rgba(0,0,0,0.12) !important;
        }
    </style>
    <script src="../../scripts/search.js"></script>
</asp:Content>
