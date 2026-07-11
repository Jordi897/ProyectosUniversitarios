# 🤖 Bot de Discord — Proyecto Personal

Este proyecto es un **bot de Discord** que desarrollé antes de comenzar la carrera.  
El objetivo era aprender Node.js, manejar APIs externas y entender cómo funcionan los bots dentro del ecosistema de Discord.

El bot está construido con **Node.js** y utiliza la librería oficial **discord.js** para interactuar con la API de Discord.

---

## 🚀 Funcionalidades principales
- Conexión con la API de Discord mediante token.
- Registro del bot en un servidor (guild).
- Comandos tipo *slash*.
- Estructura simple.
- Script externo para desplegar comandos en un servidor concreto.

---


>[!IMPORTANT]
> Antes de subir el proyecto a GitHub eliminé: - La carpeta `node_modules/` - El archivo `config.json` que contenía mis tokens

---

## ⚙️ Archivo `config.json` (debes crearlo tú)

El bot necesita un archivo `config.json` en la raíz del proyecto con esta estructura:

```json
{
    "token": "token de la api de bot de discord",
    "clientId": "idcliente",
    "guildId": "idserver"
}
```
## ⚙️ Importar el proyecto

Para instalar las dependecias en la raiz ejecute:
```bash
npm import
```
Este leera el archivo packaje-lock con las dependencias.

## 🛠 Script `deploy_commands.js`

El proyecto incluye un script llamado **`deploy_commands.js`**, cuya función es **registrar los comandos del bot en un servidor específico de Discord**.  
Discord no carga automáticamente los comandos tipo *slash*, por lo que este script es obligatorio para que el bot pueda utilizarlos.

### 📌 ¿Qué hace exactamente este script?

- Lee la configuración del archivo `config.json`.
- Usa `clientId` y `guildId` para identificar:
  - La aplicación del bot.
  - El servidor donde se registrarán los comandos.
- Envía los comandos definidos en el proyecto a la API de Discord.
- Permite que esos comandos aparezcan en el servidor y puedan ser usados por los usuarios.

### 🔧 Requisitos previos

Antes de ejecutar el script, debes asegurarte de que:

1. El archivo `config.json` existe.
2. El campo `guildId` contiene el **ID del servidor donde quieres registrar los comandos**.
3. El bot tiene permisos para registrar comandos en ese servidor.

### 📌 Orden correcto de ejecución

Para que los comandos funcionen correctamente, debes seguir este orden:

1. **Configurar `guildId`** en `config.json`  
2. Ejecutar el script de despliegue:

```bash
node deploy_commands.js
```
