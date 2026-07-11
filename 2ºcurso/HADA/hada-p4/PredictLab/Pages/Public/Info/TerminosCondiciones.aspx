<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="TerminosCondiciones.aspx.cs" Inherits="PredictLab.Pages.Public.Info.TerminosCondiciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../Style/TerminosStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-xl py-5">
    <div class="row g-5">
 
      <!-- ── Sidebar / TOC ── -->
      <aside class="col-lg-3 d-none d-lg-block">
        <div class="sidebar">
          <p class="sidebar-label">Índice</p>
          <ul class="toc-list" id="tocList">
            <li><a href="#s1"><span class="toc-num">01</span> Objeto del servicio</a></li>
            <li><a href="#s2"><span class="toc-num">02</span> Registro de usuario</a></li>
            <li><a href="#s3"><span class="toc-num">03</span> Puntos virtuales</a></li>
            <li><a href="#s4"><span class="toc-num">04</span> Predicciones y apuestas</a></li>
            <li><a href="#s5"><span class="toc-num">05</span> Contenido de usuarios</a></li>
            <li><a href="#s6"><span class="toc-num">06</span> Chats privados</a></li>
            <li><a href="#s7"><span class="toc-num">07</span> Propiedad intelectual</a></li>
            <li><a href="#s8"><span class="toc-num">08</span> Pagos y facturación</a></li>
            <li><a href="#s9"><span class="toc-num">09</span> Limitación de responsabilidad</a></li>
            <li><a href="#s10"><span class="toc-num">10</span> Modificaciones</a></li>
            <li><a href="#s11"><span class="toc-num">11</span> Protección de datos</a></li>
            <li><a href="#s12"><span class="toc-num">12</span> Jurisdicción</a></li>
          </ul>
        </div>
      </aside>
 
      <!-- ── Main content ── -->
      <main class="col-lg-9">
 
        <!-- Page header -->
        <header class="page-header">
          <span class="page-badge">Documento legal</span>
          <h1 class="page-title">Términos y Condiciones<br>de Uso</h1>
          <p class="page-subtitle">Al acceder o utilizar el sitio, el usuario acepta íntegramente estos términos.</p>
          <div class="page-meta">
            <span><i class="bi bi-file-text"></i> 13 secciones</span>
            <span><i class="bi bi-calendar3"></i> Última actualización: 2025</span>
            <span><i class="bi bi-shield-check"></i> Solo con fines de entretenimiento</span>
          </div>
        </header>
 
        <!-- Section 1 -->
        <section class="tc-section" id="s1">
          <div class="section-header">
            <span class="section-num">01</span>
            <h2 class="section-title">Objeto del servicio</h2>
          </div>
          <div class="section-body">
            <p>La plataforma permite a los usuarios participar en distintas actividades de comunidad:</p>
            <ul class="tc-list">
              <li>Crear y participar en predicciones.</li>
              <li>Apostar utilizando puntos virtuales.</li>
              <li>Adquirir puntos virtuales mediante pagos reales.</li>
              <li>Comentar predicciones públicas.</li>
              <li>Comunicarse mediante chats privados.</li>
            </ul>
            <div class="tc-highlight">
              El servicio se ofrece únicamente con fines de entretenimiento. No constituye juego de azar regulado ni apuestas con dinero real.
            </div>
          </div>
        </section>
 
        <!-- Section 2 -->
        <section class="tc-section" id="s2">
          <div class="section-header">
            <span class="section-num">02</span>
            <h2 class="section-title">Registro de usuario</h2>
          </div>
          <div class="section-body">
            <p>Para utilizar las funciones principales, el usuario debe:</p>
            <ul class="tc-list">
              <li>Crear una cuenta con datos veraces.</li>
              <li>Ser mayor de edad según la legislación aplicable.</li>
              <li>No estar sujeto a restricciones legales relacionadas con juegos o apuestas virtuales.</li>
            </ul>
            <p>La plataforma puede suspender o eliminar cuentas que incumplan estos requisitos.</p>
          </div>
        </section>
 
        <!-- Section 3 -->
        <section class="tc-section" id="s3">
          <div class="section-header">
            <span class="section-num">03</span>
            <h2 class="section-title">Puntos virtuales</h2>
          </div>
          <div class="section-body">
            <div class="tc-highlight">Los puntos virtuales no tienen valor monetario fuera de la plataforma.</div>
            <p>El usuario puede obtener puntos:</p>
            <ul class="tc-list">
              <li>Ganándolos mediante predicciones acertadas.</li>
              <li>Comprándolos mediante los métodos de pago habilitados.</li>
            </ul>
            <p>Las compras de puntos son definitivas y no reembolsables, salvo obligación legal. La plataforma puede modificar el valor, coste o funcionamiento de los puntos en cualquier momento.</p>
          </div>
        </section>
 
        <!-- Section 4 -->
        <section class="tc-section" id="s4">
          <div class="section-header">
            <span class="section-num">04</span>
            <h2 class="section-title">Predicciones y apuestas</h2>
          </div>
          <div class="section-body">
            <p>Las predicciones son creadas por la comunidad y pueden contener errores o información no verificada. El usuario participa bajo su propia responsabilidad.</p>
            <ul class="tc-list">
              <li>La plataforma no garantiza la veracidad, exactitud o resultado de ninguna predicción.</li>
              <li>La manipulación de resultados, creación de predicciones fraudulentas o cualquier intento de alterar el sistema está prohibido.</li>
            </ul>
          </div>
        </section>
 
        <!-- Section 5 -->
        <section class="tc-section" id="s5">
          <div class="section-header">
            <span class="section-num">05</span>
            <h2 class="section-title">Contenido generado por usuarios</h2>
          </div>
          <div class="section-body">
            <p>Incluye comentarios, predicciones, mensajes privados y cualquier aporte dentro del sitio. El usuario se compromete a no publicar:</p>
            <ul class="tc-list">
              <li>Contenido ilegal, ofensivo, difamatorio o discriminatorio.</li>
              <li>Spam, publicidad no autorizada o enlaces maliciosos.</li>
              <li>Información personal de terceros sin consentimiento.</li>
            </ul>
            <p>La plataforma puede eliminar contenido o suspender usuarios que incumplan estas normas.</p>
          </div>
        </section>
 
        <!-- Section 6 -->
        <section class="tc-section" id="s6">
          <div class="section-header">
            <span class="section-num">06</span>
            <h2 class="section-title">Chats privados</h2>
          </div>
          <div class="section-body">
            <p>Los chats están destinados a la comunicación entre usuarios dentro del marco del servicio. La plataforma puede aplicar sistemas automáticos de moderación para prevenir abusos.</p>
            <ul class="tc-list">
              <li>No se permite el acoso, amenazas, extorsión ni intercambio de contenido ilegal.</li>
            </ul>
          </div>
        </section>
 
        <!-- Section 7 -->
        <section class="tc-section" id="s7">
          <div class="section-header">
            <span class="section-num">07</span>
            <h2 class="section-title">Propiedad intelectual</h2>
          </div>
          <div class="section-body">
            <p>El software, diseño, marca y elementos propios del sitio son propiedad de la plataforma. El contenido generado por usuarios sigue siendo de su autoría, pero el usuario concede una licencia no exclusiva para mostrarlo dentro del servicio.</p>
          </div>
        </section>
 
        <!-- Section 8 -->
        <section class="tc-section" id="s8">
          <div class="section-header">
            <span class="section-num">08</span>
            <h2 class="section-title">Pagos y facturación</h2>
          </div>
          <div class="section-body">
            <ul class="tc-list">
              <li>Los pagos se procesan mediante proveedores externos seguros.</li>
              <li>El usuario garantiza que es titular legítimo del método de pago utilizado.</li>
              <li>La plataforma no almacena datos bancarios sensibles.</li>
            </ul>
          </div>
        </section>
 
        <!-- Section 9 -->
        <section class="tc-section" id="s9">
          <div class="section-header">
            <span class="section-num">09</span>
            <h2 class="section-title">Limitación de responsabilidad</h2>
          </div>
          <div class="section-body">
            <p>La plataforma no se hace responsable de:</p>
            <ul class="tc-list">
              <li>Pérdidas de puntos virtuales.</li>
              <li>Fallos técnicos, interrupciones o errores del servicio.</li>
              <li>Contenido publicado por usuarios.</li>
              <li>Resultados de predicciones o decisiones tomadas por los usuarios basadas en ellas.</li>
            </ul>
            <div class="tc-highlight">El uso del sitio es bajo responsabilidad exclusiva del usuario.</div>
          </div>
        </section>
 
        <!-- Section 10 -->
        <section class="tc-section" id="s10">
          <div class="section-header">
            <span class="section-num">10</span>
            <h2 class="section-title">Modificaciones del servicio</h2>
          </div>
          <div class="section-body">
            <p>La plataforma puede modificar, suspender o eliminar funciones, reglas o precios sin previo aviso. El uso continuado implica aceptación de los cambios.</p>
          </div>
        </section>
 
        <!-- Section 11 -->
        <section class="tc-section" id="s11">
          <div class="section-header">
            <span class="section-num">11</span>
            <h2 class="section-title">Protección de datos</h2>
          </div>
          <div class="section-body">
            <p>El tratamiento de datos personales se detalla en la Política de Privacidad. El usuario acepta dicho tratamiento al utilizar el servicio.</p>
          </div>
        </section>
 
        <!-- Section 12 -->
        <section class="tc-section" id="s12">
          <div class="section-header">
            <span class="section-num">12</span>
            <h2 class="section-title">Jurisdicción y legislación aplicable</h2>
          </div>
          <div class="section-body">
            <p>Estos términos se rigen por la legislación del país donde opere la plataforma. Cualquier disputa será resuelta ante los tribunales competentes.</p>
          </div>
        </section>
      </main>
    </div>
  </div>
 
  <!-- Back to top -->
  <a href="#" class="back-top" id="backTop" aria-label="Volver arriba">
    <i class="bi bi-arrow-up"></i>
  </a>

    <script>
    // Back to top button
    const backTop = document.getElementById('backTop');
    window.addEventListener('scroll', () => {
      backTop.classList.toggle('visible', window.scrollY > 300);
    });
 
    // Active TOC link on scroll
    const sections = document.querySelectorAll('.tc-section');
    const tocLinks = document.querySelectorAll('.toc-list a');
 
    const observer = new IntersectionObserver((entries) => {
      entries.forEach(entry => {
        if (entry.isIntersecting) {
          tocLinks.forEach(link => link.classList.remove('active'));
          const active = document.querySelector(`.toc-list a[href="#${entry.target.id}"]`);
          if (active) active.classList.add('active');
        }
      });
    }, { rootMargin: '-20% 0px -70% 0px' });
 
    sections.forEach(s => observer.observe(s));
 
    // Smooth scroll for TOC links
    tocLinks.forEach(link => {
      link.addEventListener('click', e => {
        e.preventDefault();
        const target = document.querySelector(link.getAttribute('href'));
        if (target) target.scrollIntoView({ behavior: 'smooth' });
      });
    });
    </script>
</asp:Content>
