<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="quienes somos.aspx.cs" Inherits="PredictLab.Pages.Public.quienes_somos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link href="../../../Style/presentacionStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Hero Section -->
    <section class="hero-section text-center">
        <div class="container">
            <h1><i class="bi bi-people-fill me-3"></i>¿Quiénes Somos?</h1>
            <p class="subtitle mt-3">La plataforma donde tus predicciones importan</p>
        </div>
    </section>
    <!-- Main Content -->
    <main class="container">
        <div class="row justify-content-center">
            <div class="col-lg-10">
                
                <!-- Card 1 -->
                <div class="content-card">
                    <div class="icon-box">
                        <i class="bi bi-lightbulb"></i>
                    </div>
                    <p>
                        Somos una <span class="highlight-text">plataforma social de predicciones</span> diseñada para convertir la opinión colectiva en una herramienta poderosa. Nuestro propósito es ofrecer un espacio donde cualquier persona pueda plantear una predicción y la comunidad pueda participar votando con una moneda virtual que refleja su nivel de convicción.
                    </p>
                </div>
                <!-- Card 2 -->
                <div class="content-card">
                    <div class="icon-box">
                        <i class="bi bi-graph-up-arrow"></i>
                    </div>
                    <p>
                        Creemos en la <span class="highlight-text">inteligencia colectiva</span>, en el valor de las tendencias y en la capacidad de una comunidad activa para anticipar lo que viene. Por eso hemos creado un entorno transparente, seguro y gamificado que convierte cada predicción en una experiencia interactiva.
                    </p>
                </div>
                <!-- Features Row -->
                <div class="row g-4 mt-2">
                    <div class="col-md-4">
                        <div class="feature-card">
                            <div class="icon-box mx-auto">
                                <i class="bi bi-shield-check"></i>
                            </div>
                            <h5>Transparente</h5>
                            <p>Todas las predicciones y resultados son públicos y verificables.</p>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="feature-card">
                            <div class="icon-box mx-auto">
                                <i class="bi bi-lock"></i>
                            </div>
                            <h5>Seguro</h5>
                            <p>Tu información y participación están protegidas en todo momento.</p>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="feature-card">
                            <div class="icon-box mx-auto">
                                <i class="bi bi-trophy"></i>
                            </div>
                            <h5>Gamificado</h5>
                            <p>Gana recompensas y sube de nivel con cada acierto.</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
