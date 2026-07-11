# hada-p4 - PredictLab

## Indice
- [Integrantes del grupo](#integrantes-del-grupo)
- [Descripción del proyecto](#descripción-del-proyecto)
- [Parte pública](#parte-pública)
- [Listado EN Pública](#listado-en-pública)
- [Parte privada](#parte-privada)
- [Listado EN Privada](#listado-en-privada)
- [Posibles mejoras](#posibles-mejoras)
- [Repartición de Trabajo](#repartición-de-trabajo)
- [Comentarios Entrega 01](#comentarios-entrega-01)
- [Entrega 2 - Esquema de la Base de Datos](#entrega-2---esquema-de-la-base-de-datos)
- [Agregación Nueva Funcionalidad Pago PayPAL](#agregación-nueva-funcionalidad-pago-paypal)
- [Entrega Final](#entrega-final)

## Integrantes del grupo
- Jordi Bodí Soriano (Coordinador) soy yo pero con la cuenta de github de la universidad [@Jordi-897](https://github.com/Jordi-897)
- Lucas López Domene [@lld19](https://github.com/lld19)
- Hugo Nicolás Vila Bernabeu [@hugonvb06-oss](https://github.com/hugonvb06-oss)
- Manuel Calvo Triguero [@Manuel1397](https://github.com/Manuel1397)
- Víctor Ramos Latorre [@Vicpi01](https://github.com/Vicpi01)
- Imran Moucherif Younsi [@ImranMoucherifYounsi](https://github.com/ImranMoucherifYounsi)

[Inicio](#hada-p4-predictlab)
## Descripción del proyecto
La aplicación web planteada consiste en una plataforma de predicciones en la que
los usuarios podrán realizar pronósticos sobre distintos temas, organizados por
categorías como deportes, política o tecnología. La plataforma permitirá consultar
predicciones, participar en ellas utilizando puntos virtuales y comentar el
contenido publicado.

Además, la aplicación incluirá funcionalidades orientadas a la interacción del
sistema hacia los usuarios (notificaciones) y, especialmente, entre los propios usuarios 
mediante mensajes privados, comunidades... También se permitirá consultar estadísticas 
personales relacionadas con la actividad de cada usuario dentro de la plataforma.

En conjunto, se pretende desarrollar una aplicación web centrada en la realización
de predicciones, incorporando funcionalidades complementarias de interacción y
seguimiento de la actividad de los usuarios dentro de la plataforma.

[Inicio](#hada-p4-predictlab)
## Parte pública
La parte pública de la aplicación permitirá a los usuarios no registrados consultar
la información general de la plataforma y acceder a las funcionalidades básicas
disponibles sin necesidad de iniciar sesión.
<ul>
    <li>
        <details>
            <summary><b>Consultar predicciones</b></summary> 
	        Los usuarios no registrados podrán visualizar las
	        predicciones activas disponibles en la plataforma.
        </details>
    </li>
    <li>
        <details>
	        <summary><b>Ver el detalle de una predicción</b></summary>  
	        Se podrá consultar la información básica de
	        cada predicción, como su título, descripción, categoría y estado.
        </details>
    </li>
    <li>
        <details>
	        <summary><b>Navegar por categorías</b></summary> 
	        Las predicciones estarán organizadas por categorías
	        temáticas, como deportes, política o tecnología, para facilitar su consulta.
        </details>
    </li>
    <li>
        <details>
	        <summary><b>Leer comentarios públicos</b></summary>
	        Los usuarios podrán consultar los comentarios
	        asociados a las predicciones publicadas.
        </details>
    </li>
    <li>
        <details>
	        <summary><b>Buscar y filtrar contenido</b></summary> 
	        Se podrán realizar búsquedas sobre las
	        predicciones aplicando uno o varios filtros del sistema.
        </details>
    </li>
    <li>
        <details>
	        <summary><b>Consultar información general de la plataforma</b></summary> 
	        Se mostrará información
	        básica sobre la aplicación y su funcionamiento.
        </details>
    </li>
    <li>
        <details>
	        <summary><b>Registrarse en la aplicación</b></summary> 
	        Desde la parte pública, los usuarios podrán
	        crear una cuenta para acceder posteriormente a la parte privada.
        </details>
    </li>
</ul>

[Inicio](#hada-p4-predictlab)
## Listado EN Pública
- usuario
- Predicción
- Comentario
- Categoría

>[!IMPORTANT]
>La Entidad Categoría no cuenta como entidad de negocio obligatoria para el proyecto por su simplicidad.

[Inicio](#hada-p4-predictlab)
## Parte privada
La parte privada de la aplicación estará dirigida a los usuarios registrados y les
permitirá acceder a las funcionalidades principales de participación, gestión de
su cuenta e interacción con otros usuarios dentro de la plataforma.

<ul>
    <li>
        <details>
            <summary><b>Iniciar sesión</b></summary>
            Los usuarios registrados podrán acceder a su cuenta personal
            mediante sus credenciales.
        </details>
    </li>
    <li>
        <details>
            <summary><b>Gestionar el perfil</b></summary>
            Cada usuario podrá consultar y modificar sus datos
            personales, así como realizar otras acciones relacionadas con su cuenta.
        </details>
    </li>
    <li>
        <details>
            <summary><b>Participar en predicciones</b></summary>
            Los usuarios podrán intervenir en las
            predicciones disponibles utilizando puntos virtuales dentro de la plataforma.
        </details>
    </li>
    <li>
        <details>
            <summary><b>Consultar transacciones</b></summary>
            Cada usuario podrá visualizar el historial de
            movimientos de puntos realizados en su cuenta.
        </details>
    </li>
    <li>
        <details>
            <summary><b>Consultar estadísticas personales</b></summary>
            Se mostrarán datos relacionados con la
            actividad del usuario, como porcentaje de aciertos, puntos ganados, puntos
            gastados o rachas.
        </details>
    </li>
    <li>
        <details>
            <summary><b>Comentar predicciones</b></summary>
            Los usuarios podrán publicar comentarios en las
            predicciones y participar en la interacción asociada a ellas.
        </details>
    </li>
    <li>
        <details>
            <summary><b>Enviar y recibir mensajes privados</b></summary>
            Los usuarios podrán comunicarse de
            forma directa con otros usuarios registrados.
        </details>
    </li>
    <li>
        <details>
            <summary><b>Recibir notificaciones</b></summary>
            La plataforma podrá mostrar avisos automáticos
            relacionados con la actividad del usuario dentro del sistema.
        </details>
    </li>
    <li>
        <details>
            <summary><b>Participar en desafíos/Logros</b></summary>
            Los usuarios podrán completar objetivos
            determinados, como por ejemplo acertar un número concreto de predicciones.
        </details>
    </li>
    <li>
        <details>
            <summary><b>Consultar la conversión de divisa</b></summary>
            Se podrá consultar la equivalencia entre
            distintas monedas y los puntos virtuales utilizados en la plataforma.
        </details>
    </li>
    <li>
        <details>
            <summary><b>Denunciar contenido o usuarios</b></summary>
            Los usuarios podrán reportar comentarios o
            comportamientos inadecuados dentro de la aplicación.
        </details>
    </li>
    <li>
        <details>
            <summary><b>Crear o unirse a comunidades</b></summary>
            Los usuarios podrán formar parte de
            comunidades orientadas a realizar predicciones en grupo y repartir beneficios
            en caso de acierto.
        </details>
    </li>
</ul>

[Inicio](#hada-p4-predictlab)
## Listado EN Privada
- usuario
- Transacción
- Predicción
- Comentario
- Categoría
- EstadísticaUsuario
- Denuncia
- MensajePrivado
- ConversiónDivisa
- Desafío
- Notificación
- Comunidad

>[!IMPORTANT]
>La Entidad Categoría no cuenta como entidad de negocio obligatoria para el proyecto por su simplicidad.

[Inicio](#hada-p4-predictlab)
## Posibles mejoras

<ul>
    <li>
        <details>
            <summary><b>Sistema de recomendación inteligente</b></summary>
            Sugerencias basadas en el historial del usuario.<br>
            Recomendaciones por categorías más visitadas.<br>
            Predicciones destacadas según la actividad de la comunidad.
        </details>
    </li>
    <li>
        <details>
            <summary><b>Panel avanzado de estadísticas</b></summary>
            Gráficas de evolución de puntos.<br>
            Comparativas con otros usuarios.<br>
            Métricas por categoría y rendimiento personal.
        </details>
    </li>
    <li>
        <details>
            <summary><b>Búsqueda predictiva mejorada</b></summary>
            Autocompletado tipo Google.<br>
            Sugerencias dinámicas mientras se escribe.<br>
            Filtros avanzados por estado, fecha o popularidad.
        </details>
    </li>
    <li>
        <details>
            <summary><b>Moderación automática</b></summary>
            Detección de lenguaje inapropiado.<br>
            Sistema de reputación para usuarios.<br>
            Revisión asistida de denuncias.
        </details>
    </li>
    <li>
        <details>
            <summary><b>Versión multidioma</b></summary>
            Español, inglés y valenciano.<br>
            Sistema de traducciones dinámico.
        </details>
    </li>
    <li>
        <details>
            <summary><b>Integración con redes sociales</b></summary>
            Compartir predicciones o resultados.<br>
            Inicio de sesión con Google, GitHub o X.
        </details>
    </li>
    <li>
        <details>
            <summary><b>Historial ampliado</b></summary>
            Registro completo de predicciones realizadas.<br>
            Filtros por fecha, categoría o resultado.
        </details>
    </li>
</ul>

>[!NOTE]
>La lista no es 100%, cualquier funcionalidad descrita no tienen por qué llevarse a cabo en el proyecto.

>[!IMPORTANT]
>No es una lista cerrada, en cualquier momento se puede añadir una funcionalidad que no se haya descrito aquí.

[Inicio](#hada-p4-predictlab)
## Repartición de Trabajo

- **Jordi**: usuario, Funcionalidad nueva.
- **Lucas**: Comentario, Denuncia.
- **Hugo**: Estadísticas, Mensajes Privados.
- **Manuel**: Conversión divisa, Transacción.
- **Víctor**: Predicción, (Categoría), Comunidad.
- **Imran**: Desafío/Logro, Notificación.

> [!IMPORTANT]
> Esto solo es la división de trabajo de las Entidades de negocio que hace cada integrante. 
Las funcionalidades de cada integrante del equipo se deciden en función de las 
entidades de negocio seleccionadas. Con posibilidad de excepción. 
Nada de lo escrito aquí es 100% la última decisión.

(#comentarios-entrega-01)
## Comentarios Entrega 01

Muy buen trabajo, la entrega es correcta y cumple con todo lo que se pide.

Solamente tengo una duda acerca de la ENComunidad que no me queda claro que es, pero vamos, que la entrega es muy buena.

También teded ojo con el nombre de los directorios. Por ejemplo, eliminad en & en la carpeta de libreria ya que esto puede dar problemas de enlaces sobre todo si se coloca despues en un servidor.

En resumen, todo muy bien, seguid así!!!

[Inicio](#hada-p4-predictlab)
## Entrega 2 - Esquema de la Base de Datos

El esquema de la base de datos correspondiente a la segunda entrega se encuentra en el siguiente fichero PDF:

- **Nombre del fichero**: `DATABASE_EERR.pdf`
- **Ruta/ubicación**: `ESQUEMA/DATABASE_EERR.pdf`

**[ABRIR Esquema de la Base de Datos](ESQUEMA/DATABASE_EERR.pdf)**

[Inicio](#hada-p4-predictlab)

## Entrega 3 - EN y CADS junto con login y registro

En esta entrega se han añadido las entidades de negocio y 
los casos de uso correspondientes al login y registro de usuarios. 
Además, se creo la base de datos y se creo el sricpt de la base de datos guardado en esquema.


**[ABRIR Script de la Base de Datos](ESQUEMA/DATABASE_Script.sql)**

>[!NOTE]
>En la pagina principal default hay dos links para el registro y el login, 
como no esta implementada la parte privada,
login si funciona correctamente envia a la pagina default.
Registro si funciona correctamente, envia al login y muetra un mensaje emergente de registro exitoso.

[Inicio](#hada-p4-predictlab)

## Entrega 4 - Demo + Interfaz

En esta entrega se han añadido las correspondientes paginas principales del proyecto,
tanto para la parte pública como para la parte privada. Y añadido cierto diseño para mostrar la idea que va tomando el proyecto
hay que asegurarse de que predictLab sea el proyecto principal para su ejecucion y que la pagina inicia desde Default.aspx que se ejecuta
al iniciar el proyecto.

Tambien se cre la carpeta entregaProyecto con el zip del proeycto entero

[Inicio](#hada-p4-predictlab)

## Comentarios entrega 03

En general, la entrega es correcta y la estructura del proyecto está bien encaminada. Existe separación entre las capas `EN` y `CAD`, el proyecto `PredictLab` ya utiliza la librería `LibraryENCAD`, y la parte de `Usuario` es la más completa y la que mejor se ajusta al funcionamiento esperado para esta entrega.

Como incidencia principal, ha habido un problema con la base de datos. El script SQL original no era reutilizable porque incluía rutas absolutas del equipo en el que se generó. La base de datos debe crearse manualmente y, a continuación, ejecutarse un script de esquema genérico. Si en una futura versión se quiere automatizar también la creación de la base de datos, deberá hacerse sin rutas absolutas y con un script portable que cualquier persona pueda ejecutar en local.

También se ha detectado un problema funcional importante: no ha sido posible completar el registro correctamente, y además no se indica de forma clara cómo debe ser la contraseña. Es necesario que se documenten las restricciones de la contraseña y que tanto el registro como el login queden completamente operativos.

### Revisión general de `EN` y `CAD`

Se ha comprobado que, en términos generales, las clases `EN` sí están planteadas para invocar a sus correspondientes clases `CAD`, lo cual es correcto. Para esta entrega no era obligatorio que todos los `CAD` estuvieran completamente implementados.

En `PredictLab` existen formularios de `login` y `registro`, y ambos utilizan `ENUsuario`, que a su vez llama a `CADUsuario`. Por tanto, el flujo general `interfaz -> EN -> CAD` está bien planteado.

### Resumen de revisión

| Elemento | Estado | Observaciones | Posibles mejoras |
|---|---|---|---|
| `UsuarioEN` / `UsuarioCAD` | Correcto | Es la parte más completa del proyecto y la que mejor encaja con la BBDD. Además, `login` y `registro` dependen de esta entidad. | Corregir detalles de SQL en `Update`, mejorar validaciones y evitar almacenar contraseñas en texto plano. |
| Resto de clases `EN` | Aceptable | En general llaman a su `CAD`, que era el requisito importante. | Ajustar nombres y tipos para que coincidan mejor con la BBDD real. |
| Resto de clases `CAD` | Aceptable | No era imprescindible que estuvieran implementadas por completo en esta entrega. Si están implementadas, no es un problema. | Unificar estilo |
| Correspondencia con la BBDD | Mejorable | Hay varias entidades cuyos nombres de propiedades y columnas no coinciden exactamente con el esquema SQL. | Revisar tabla por tabla y alinear `Id`, tipos `DECIMAL`, nombres de columnas y relaciones. |
| `Login` | Parcialmente correcto | Existe y usa `ENUsuario`, pero debe quedar completamente funcional. | Validar mejor mensajes de error y asegurar autenticación correcta. |
| `Registro` | Mejorable | Existe y usa `ENUsuario`, pero no ha sido posible completar el registro correctamente. | Revisar inserción, validaciones y documentar claramente el formato de contraseña requerido. |
| Script de BBDD | Mejorable | El script original incluía rutas absolutas y no era genérico. | Mantener script portable, sin rutas locales embebidas. |

### Incongruencias detectadas con la BBDD

- En varias entidades se usan propiedades como `Code` cuando en la base de datos la clave real es `Id`.
- Hay tipos definidos en código que no coinciden con la BBDD, especialmente columnas `DECIMAL` modeladas como `int` o `string`.
- Existen nombres de propiedades y columnas que no encajan del todo con el esquema real.
- Algunas consultas SQL de los `CAD` utilizan nombres de columnas que no existen en la base de datos o no respetan exactamente el esquema definido.

### Resumen global

La entrega es correcta y cumple de forma razonable con lo esperado para esta fase, especialmente en la organización general y en la parte de `Usuario`. No obstante, todavía quedan ajustes importantes: hacer el script SQL completamente genérico, corregir las incoherencias entre modelo y base de datos, y dejar totalmente operativo el flujo de `registro` y `login`.

De cara a la entrega 04, todo esto ya debe quedar perfecto: base de datos portable, correspondencia exacta entre `EN`, `CAD` y BBDD, y funcionamiento correcto y completo tanto del registro como del login.

Muy buen trabajo, seguimos así y no s vemos el martes!!

## Agregación Nueva Funcionalidad Pago PayPAL

En este apartado se da email, contraseña, tarjeta de credito y otros datos necesarios para realizar un pago de prueba a través de PayPal. Esta información es completamente ficticia y se utiliza únicamente con fines de demostración en el proyecto.
Los datos se nos dio por paypal, hacemos uso de su API para realizar el pago.

<ul>
    <li>
        <details>
            <summary><b>Pago Mediante PayPal</b></summary>
            email: sb-uqvdb51190337@personal.example.com <br>
            contraseña: GB4a!e;&
        </details>
    </li>
    <li>
        <details>
            <summary><b>Tarjeta de Crédito para Pruebas</b></summary>
            Número de tarjeta: 4020024001802816 <br>
            Fecha de vencimiento: 06/2031 <br>
            Código de seguridad (CVV): Cualquiera de 3 dígitos <br>
            nombre: John <br>
            apellido: DoeskipKYC <br>
            codigo postal: 03430 <br>
            telefono: 630275147 <br>
            email: sb-uqvdb51190337@personal.example.com <br>
            Pais: España <br>
        </details>
    </li>
<ul>

>[!NOTE]
>Saltara para poner un codigo, hay que poner 1234 y se efectuara la compra de prueba en Tarjeta de credito.

[Inicio](#hada-p4-predictlab)
# Entrega Final

## Cambios relevantes respecto a la propuesta inicial
- Se ha implementado la funcionalidad de pago mediante PayPal, utilizando datos de prueba proporcionados por la plataforma para realizar transacciones de demostración.
- Se han corregido las incoherencias entre el modelo de datos en código y la base de datos, garantizando que los nombres de propiedades, tipos de datos y relaciones coincidan con el esquema SQL definido.
- Finalmente no se aplicaron filtros por categoría en las predicciones debido a falta de tiempo.
- Se añadió un panel de Administración con información relevante.
- Las estadísticas de usuario son opcionales; se ofrece al usuario la posibilidad de crearlas.
- La divisa forma parte de la sección pública: puede consultarse sin iniciar sesión, aunque para obtener divisa sí se redirige al login.

## Problemas del grupo
En general, el trabajo en grupo ha sido fluido en todas las entregas, excepto en la Entrega 03, donde algunos compañeros apuraron al máximo el tiempo. Esto obligó al coordinador a realizar un esfuerzo adicional para integrar el trabajo y corregir los errores detectados, aunque finalmente se entregó todo a tiempo y con una calidad aceptable.

En cuanto a la comunicación, hubo ciertos malentendidos sobre la implementación de algunas ideas, lo que generó una ralentización del proyecto.

Respecto a la distribución del trabajo, se intentó repartir las tareas de forma equitativa, aunque algunos compañeros asumieron más carga debido a que sus EN y CAD requerían más lógica de backend.

En algunas páginas donde se mostraba información de varias entidades (como Perfil), surgieron problemas de diseño debido a la libertad creativa otorgada a los integrantes.

También se detectó un problema puntual con un integrante que provocó conflictos en los merge de Git y delegó la resolución al coordinador. Esto ocurrió dos veces durante el desarrollo.

Finalmente, el mayor problema fueron los cuellos de botella: ciertos componentes requerían más tiempo de desarrollo, lo que provocó parones entre compañeros.

## Instrucciones para ejecutar el proyecto
1. Clonar el repositorio desde GitHub.
2. Abrir la solución en Visual Studio.
3. Asegurarse de que el proyecto PredictLab está configurado como proyecto de inicio.
4. Crear la base de datos en App_Data utilizando el script SQL ubicado en la carpeta ESQUEMA.
5. Opcionalmente, establecer Default.aspx como página de inicio.

> [!IMPORTANT]  
> Verificar que la carpeta App_Data existe y que la base de datos se ha creado correctamente antes de ejecutar el proyecto.

## Tareas de cada participante
En cada panel o página se incluye el conjunto de HTML, CSS, JavaScript y la lógica funcional en C#.

| Usuario | Tareas realizadas |
|--------|-------------------|
| Jordi — `Coordinador`, `Jordi-897` | ENUsuario, ENWallet, CADUsuario, CADWallet, funcionalidad de pago PayPal, estructura de directorios, esquema y script de base de datos, handler de búsqueda, handler de pago PayPal, panel de Administración, Registro, Login, Cerrar sesión, páginas informativas (Quiénes somos, Términos y condiciones), gestión de imágenes de perfil, información y configuración del Perfil, página maestra, aportación al README y corrección de errores. |
| Lucas — `lld19` | ENComentario, ENDenuncia, CADComentario, CADDenuncia, panel de Denuncias, comentarios en predicciones y borrado de mensajes, botón del panel de denuncias en el menú, aportación al README. |
| Hugo — `hugonvb06-oss` | ENEstadisticaUsuario, ENMensajePrivado, CADEstadisticaUsuario, CADMensajePrivado, panel de Estadísticas, panel de Mensajes Privados / Chats. |
| Manuel — `Manuel1397` | ENConversionDivisa, ENTransaccion (Apuesta, Gana, Divisa), CADConversionDivisa, CADTransaccion (Apuesta, Gana, Divisa), panel de Conversión de Divisa, panel de Transacciones en Perfil, filtrado de transacciones, creación de notificación de pago realizado. |
| Víctor — `Vicpi01` | ENPrediccion, ENCategoria, ENComunidad, CADPrediccion, CADCategoria, CADComunidad, panel de Predicciones, panel de Comunidades. |
| Imran — `ImranMoucherifYounsi` | ENLogro, ENNotificacion, CADLogro, CADNotificacion, panel de Desafíos/Logros, panel de Notificaciones, integración de notificaciones en el perfil, botón de notificaciones en el menú. |

> [!NOTE]  
> Las tareas asignadas no representan cargas de trabajo equivalentes; algunos puntos resumidos implican mayor complejidad que otros.

## Usuario administrador
El script de la base de datos incluye un usuario administrador para acceder al panel correspondiente.

- Usuario: admin  
- Contraseña: Jj@1

[Inicio](#hada-p4-predictlab)
