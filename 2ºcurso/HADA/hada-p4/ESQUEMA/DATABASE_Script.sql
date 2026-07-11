/*
Script para crear únicamente el contenido (tablas, constraints, triggers, etc.)
de la base de datos llamada "Database". La base debe existir previamente.
Todas las columnas y nombres de constraints se usan en minúsculas.
*/
GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;
SET NUMERIC_ROUNDABORT OFF;
GO

-- Comprobar que la base de datos existe
IF DB_ID(N'Database') IS NULL
BEGIN
    PRINT N'La base de datos "Database" no existe. Créala antes de ejecutar este script.';
    RETURN;
END
GO

USE [Database];
GO

-- A partir de aquí se crean las tablas, constraints y triggers con columnas en minúsculas.
PRINT N'Creando Tabla [dbo].[catPrediccion]...';
GO
CREATE TABLE [dbo].[catPrediccion] (
    [categoria] NVARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([categoria] ASC)
);
GO

PRINT N'Creando Tabla [dbo].[chat]...';
GO
CREATE TABLE [dbo].[chat] (
    [usuario1]      NVARCHAR (254) NOT NULL,
    [usuario2]      NVARCHAR (254) NOT NULL,
    [fechacreacion] NCHAR (10)     NULL,
    CONSTRAINT [pk_chat] PRIMARY KEY CLUSTERED ([usuario1] ASC, [usuario2] ASC)
);
GO

PRINT N'Creando Tabla [dbo].[comentario]...';
GO
CREATE TABLE [dbo].[comentario] (
    [id]         INT            IDENTITY (1, 1) NOT NULL,
    [mensaje]    NVARCHAR (500) NULL,
    [fecha]      DATETIME       NULL,
    [prediccion] INT            NOT NULL,
    [usuario]    NVARCHAR (254) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

PRINT N'Creando Tabla [dbo].[comunidad]...';
GO
CREATE TABLE [dbo].[comunidad] (
    [id]             INT           IDENTITY (1, 1) NOT NULL,
    [wallet]         INT           NULL,
    [titulo]         NVARCHAR (100) NULL,
    [descripcion]    NVARCHAR (200) NULL,
    [fechaincursion] DATETIME      NULL,
    [prediccion]     INT           NOT NULL,
    [voto]           NCHAR (2)     NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    UNIQUE NONCLUSTERED ([wallet] ASC)
);
GO

PRINT N'Creando Tabla [dbo].[conversiondivisa]...';
GO
CREATE TABLE [dbo].[conversiondivisa] (
    [moneda]       NCHAR (10)      NOT NULL,
    [valorvirtual] DECIMAL (10, 2) NULL,
    PRIMARY KEY CLUSTERED ([moneda] ASC)
);
GO

PRINT N'Creando Tabla [dbo].[denuncia]...';
GO
CREATE TABLE [dbo].[denuncia] (
    [id]            INT            IDENTITY (1, 1) NOT NULL,
    [causadenuncia] NVARCHAR (30)  NOT NULL,
    [descripcion]   NVARCHAR (600) NULL,
    [fecha]         DATE           NULL,
    [estado]        NCHAR (10)     NOT NULL,
    [emisor]        NVARCHAR (254) NOT NULL,
    [comentario]    INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

PRINT N'Creando Tabla [dbo].[estadisticasusuario]...';
GO
CREATE TABLE [dbo].[estadisticasusuario] (
    [usuario]              NVARCHAR (254)  NOT NULL,
    [mayorracha]           INT             NULL,
    [maxpuntos]            DECIMAL (10, 2) NULL,
    [prediccionesganadas]  INT             NULL,
    [prediccionesperdidas] INT             NULL,
    [rachaactual]           INT             NULL,
    PRIMARY KEY CLUSTERED ([usuario] ASC)
);
GO

PRINT N'Creando Tabla [dbo].[logro]...';
GO
CREATE TABLE [dbo].[logro] (
    [titulo]      NVARCHAR (10)   NOT NULL,
    [descripcion] NVARCHAR (50)   NULL,
    [recompensa]  DECIMAL (10, 2) NULL,
    PRIMARY KEY CLUSTERED ([titulo] ASC)
);
GO

PRINT N'Creando Tabla [dbo].[logrousuario]...';
GO
CREATE TABLE [dbo].[logrousuario] (
    [logro]   NVARCHAR (10)  NOT NULL,
    [usuario] NVARCHAR (254) NOT NULL,
    CONSTRAINT [pk_logrousuario] PRIMARY KEY CLUSTERED ([logro] ASC, [usuario] ASC)
);
GO

PRINT N'Creando Tabla [dbo].[mensaje]...';
GO
CREATE TABLE [dbo].[mensaje] (
    [id]        INT            IDENTITY (1, 1) NOT NULL,
    [fecha]     DATETIME       NULL,
    [contenido] NVARCHAR (300) NULL,
    [emisor]    NVARCHAR (254) NOT NULL,
    [chat1]     NVARCHAR (254) NOT NULL,
    [chat2]     NVARCHAR (254) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

PRINT N'Creando Tabla [dbo].[notificacion]...';
GO
CREATE TABLE [dbo].[notificacion] (
    [id]      INT           IDENTITY (1, 1) NOT NULL,
    [titulo]  NVARCHAR (50) NULL,
    [mensaje] NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

PRINT N'Creando Tabla [dbo].[notificacionusuario]...';
GO
CREATE TABLE [dbo].[notificacionusuario] (
    [usuario]      NVARCHAR (254) NOT NULL,
    [notificacion] INT            NOT NULL,
    [leido]         BIT            NULL,
    [fecha] 	   DATETIME       NULL,
    CONSTRAINT [pk_notificacionusuario] PRIMARY KEY CLUSTERED ([usuario] ASC, [notificacion] ASC)
);
GO

PRINT N'Creando Tabla [dbo].[notiprediccion]...';
GO
CREATE TABLE [dbo].[notiprediccion] (
    [notificacion] INT NOT NULL,
    [prediccion]   INT NOT NULL,
    PRIMARY KEY CLUSTERED ([notificacion] ASC)
);
GO

PRINT N'Creando Tabla [dbo].[prediccion]...';
GO
CREATE TABLE [dbo].[prediccion] (
    [id]                INT             IDENTITY (1, 1) NOT NULL,
    [titulo]            NVARCHAR (100)   NULL,
    [prediccion]        NVARCHAR (222)   NOT NULL,
    [cantidadrecaudada] DECIMAL (10, 2) NOT NULL,
    [estado]            NVARCHAR (10)      NOT NULL,
    [categoria]         NVARCHAR (20)   NULL,
    [creador]           NVARCHAR (254)  NULL,
    [votossi]           DECIMAL (10, 2) NOT NULL,
    [votosno]           DECIMAL (10, 2) NOT NULL,
    [fechafin]          DATETIME        NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

PRINT N'Creando Tabla [dbo].[tag]...';
GO
CREATE TABLE [dbo].[tag] (
    [prediccion] INT        NOT NULL,
    [tag]        NCHAR (10) NOT NULL,
    CONSTRAINT [pk_tag] PRIMARY KEY CLUSTERED ([prediccion] ASC, [tag] ASC)
);
GO

PRINT N'Creando Tabla [dbo].[tranaapuesta]...';
GO
CREATE TABLE [dbo].[tranaapuesta] (
    [id]         INT       NOT NULL,
    [voto]       NCHAR (2) NULL,
    [wallet]     INT       NOT NULL,
    [prediccion] INT       NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

PRINT N'Creando Tabla [dbo].[trandivisa]...';
GO
CREATE TABLE [dbo].[trandivisa] (
    [id]     INT        NOT NULL,
    [wallet] INT        NOT NULL,
    [divisa] NCHAR (10) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

PRINT N'Creando Tabla [dbo].[trangana]...';
GO
CREATE TABLE [dbo].[trangana] (
    [id]         INT NOT NULL,
    [wallet]     INT NOT NULL,
    [prediccion] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

PRINT N'Creando Tabla [dbo].[transaccion]...';
GO
CREATE TABLE [dbo].[transaccion] (
    [id]       INT             IDENTITY (1, 1) NOT NULL,
    [cantidad] DECIMAL (10, 2) NOT NULL,
    [fecha]    DATETIME        NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

PRINT N'Creando Tabla [dbo].[usuario]...';
GO
CREATE TABLE [dbo].[usuario] (
    [email]     NVARCHAR (254) NOT NULL,
    [nombre]    NVARCHAR (20)  NOT NULL,
    [apellidos] NVARCHAR (50)  NOT NULL,
    [telefono]  NVARCHAR (20)  NULL,
    [nickname]  NVARCHAR (10)  NOT NULL,
    [wallet]    INT            NULL,
    [password]  NVARCHAR (200) NOT NULL,
    [salt]      NVARCHAR (200) NOT NULL,
    [admin]     BIT        NOT NULL DEFAULT 0,
    PRIMARY KEY CLUSTERED ([email] ASC),
    UNIQUE NONCLUSTERED ([wallet] ASC),
    CONSTRAINT [nombreusuario] UNIQUE NONCLUSTERED ([nickname] ASC)
);
GO

PRINT N'Creando Tabla [dbo].[wallet]...';
GO
CREATE TABLE [dbo].[wallet] (
    [id]            INT             IDENTITY (1, 1) NOT NULL,
    [saldo]         DECIMAL (10, 2) NOT NULL,
    [saldoretenido] DECIMAL (10, 2) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

PRINT N'Creando Restricción DEFAULT en [dbo].[estadisticasusuario]...';
GO
ALTER TABLE [dbo].[estadisticasusuario]
    ADD DEFAULT ((0)) FOR [mayorracha];
GO
ALTER TABLE [dbo].[estadisticasusuario]
    ADD DEFAULT ((0)) FOR [maxpuntos];
GO
ALTER TABLE [dbo].[estadisticasusuario]
    ADD DEFAULT ((0)) FOR [prediccionesganadas];
GO
ALTER TABLE [dbo].[estadisticasusuario]
    ADD DEFAULT ((0)) FOR [prediccionesperdidas];
GO

PRINT N'Creando Restricción DEFAULT en [dbo].[prediccion]...';
GO
ALTER TABLE [dbo].[prediccion]
    ADD DEFAULT ((0)) FOR [cantidadrecaudada];
GO
ALTER TABLE [dbo].[prediccion]
    ADD DEFAULT ((0)) FOR [votossi];
GO
ALTER TABLE [dbo].[prediccion]
    ADD DEFAULT ((0)) FOR [votosno];
GO

PRINT N'Creando Restricción DEFAULT en [dbo].[wallet]...';
GO
ALTER TABLE [dbo].[wallet]
    ADD DEFAULT ((0)) FOR [saldo];
GO

PRINT N'Creando claves externas...';
GO
ALTER TABLE [dbo].[chat]
    ADD CONSTRAINT [fk_chat_tousuario1] FOREIGN KEY ([usuario1]) REFERENCES [dbo].[usuario] ([email]);
GO
ALTER TABLE [dbo].[chat]
    ADD CONSTRAINT [fk_chat_tousuario2] FOREIGN KEY ([usuario2]) REFERENCES [dbo].[usuario] ([email]);
GO
ALTER TABLE [dbo].[comentario]
    ADD CONSTRAINT [fk_comentario_toprediccion] FOREIGN KEY ([prediccion]) REFERENCES [dbo].[prediccion] ([id]);
GO
ALTER TABLE [dbo].[comentario]
    ADD CONSTRAINT [fk_comentario_tousuario] FOREIGN KEY ([usuario]) REFERENCES [dbo].[usuario] ([email]);
GO
ALTER TABLE [dbo].[comunidad]
    ADD CONSTRAINT [fk_comunidad_toprediccion] FOREIGN KEY ([prediccion]) REFERENCES [dbo].[prediccion] ([id]);
GO
ALTER TABLE [dbo].[comunidad]
    ADD CONSTRAINT [fk_comunidad_towallet] FOREIGN KEY ([wallet]) REFERENCES [dbo].[wallet] ([id]);
GO
ALTER TABLE [dbo].[denuncia]
    ADD CONSTRAINT [fk_denuncia_tocomentario] FOREIGN KEY ([comentario]) REFERENCES [dbo].[comentario] ([id]);
GO
ALTER TABLE [dbo].[denuncia]
    ADD CONSTRAINT [fk_denuncia_tousuario] FOREIGN KEY ([emisor]) REFERENCES [dbo].[usuario] ([email]);
GO
ALTER TABLE [dbo].[estadisticasusuario]
    ADD CONSTRAINT [fk_estadisticasusuario_tousuario] FOREIGN KEY ([usuario]) REFERENCES [dbo].[usuario] ([email]);
GO
ALTER TABLE [dbo].[logrousuario]
    ADD CONSTRAINT [fk_logrousuario_tologro] FOREIGN KEY ([logro]) REFERENCES [dbo].[logro] ([titulo]);
GO
ALTER TABLE [dbo].[logrousuario]
    ADD CONSTRAINT [fk_logrousuario_tousuario] FOREIGN KEY ([usuario]) REFERENCES [dbo].[usuario] ([email]);
GO
ALTER TABLE [dbo].[mensaje]
    ADD CONSTRAINT [fk_mensaje_tochat] FOREIGN KEY ([chat1], [chat2]) REFERENCES [dbo].[chat] ([usuario1], [usuario2]);
GO
ALTER TABLE [dbo].[mensaje]
    ADD CONSTRAINT [fk_mensaje_tousuario] FOREIGN KEY ([emisor]) REFERENCES [dbo].[usuario] ([email]);
GO
ALTER TABLE [dbo].[notificacionusuario]
    ADD CONSTRAINT [fk_notificacionusuario_tonotificacion] FOREIGN KEY ([notificacion]) REFERENCES [dbo].[notificacion] ([id]);
GO
ALTER TABLE [dbo].[notificacionusuario]
    ADD CONSTRAINT [fk_notificacionusuario_tousuario] FOREIGN KEY ([usuario]) REFERENCES [dbo].[usuario] ([email]);
GO
ALTER TABLE [dbo].[notiprediccion]
    ADD CONSTRAINT [fk_notiprediccion_tonotificacion] FOREIGN KEY ([notificacion]) REFERENCES [dbo].[notificacion] ([id]);
GO
ALTER TABLE [dbo].[notiprediccion]
    ADD CONSTRAINT [fk_notiprediccion_toprediccion] FOREIGN KEY ([prediccion]) REFERENCES [dbo].[prediccion] ([id]);
GO
ALTER TABLE [dbo].[prediccion]
    ADD CONSTRAINT [fk_prediccion_tocatprediccion] FOREIGN KEY ([categoria]) REFERENCES [dbo].[catPrediccion] ([categoria]);
GO
ALTER TABLE [dbo].[prediccion]
    ADD CONSTRAINT [fk_prediccion_tousuario] FOREIGN KEY ([creador]) REFERENCES [dbo].[usuario] ([email]);
GO
ALTER TABLE [dbo].[tag]
    ADD CONSTRAINT [fk_tag_toprediccion] FOREIGN KEY ([prediccion]) REFERENCES [dbo].[prediccion] ([id]);
GO
ALTER TABLE [dbo].[tranaapuesta]
    ADD CONSTRAINT [fk_tranaapuesta_toprediccion] FOREIGN KEY ([prediccion]) REFERENCES [dbo].[prediccion] ([id]);
GO
ALTER TABLE [dbo].[tranaapuesta]
    ADD CONSTRAINT [fk_tranaapuesta_totransaccion] FOREIGN KEY ([id]) REFERENCES [dbo].[transaccion] ([id]);
GO
ALTER TABLE [dbo].[tranaapuesta]
    ADD CONSTRAINT [fk_tranaapuesta_towallet] FOREIGN KEY ([wallet]) REFERENCES [dbo].[wallet] ([id]);
GO
ALTER TABLE [dbo].[trandivisa]
    ADD CONSTRAINT [fk_trandivisa_todivisa] FOREIGN KEY ([divisa]) REFERENCES [dbo].[conversiondivisa] ([moneda]);
GO
ALTER TABLE [dbo].[trandivisa]
    ADD CONSTRAINT [fk_trandivisa_towallet] FOREIGN KEY ([wallet]) REFERENCES [dbo].[wallet] ([id]);
GO
ALTER TABLE [dbo].[trandivisa]
    ADD CONSTRAINT [fk_trandivisa_totransaccion] FOREIGN KEY ([id]) REFERENCES [dbo].[transaccion] ([id]);
GO
ALTER TABLE [dbo].[trangana]
    ADD CONSTRAINT [fk_trangana_totransaccion] FOREIGN KEY ([id]) REFERENCES [dbo].[transaccion] ([id]);
GO
ALTER TABLE [dbo].[trangana]
    ADD CONSTRAINT [fk_trangana_towallet] FOREIGN KEY ([wallet]) REFERENCES [dbo].[wallet] ([id]);
GO
ALTER TABLE [dbo].[usuario]
    ADD CONSTRAINT [fk_usuario_towallet] FOREIGN KEY ([wallet]) REFERENCES [dbo].[wallet] ([id]) ON DELETE CASCADE;
GO

PRINT N'Creando CHECKs...';
GO
ALTER TABLE [dbo].[chat]
    ADD CONSTRAINT [ck_chat_orden] CHECK ([usuario1]<[usuario2]);
GO
ALTER TABLE [dbo].[comunidad]
    ADD CONSTRAINT [ck_comunidad_voto] CHECK ([voto]='NO' OR [voto]='SI');
GO
ALTER TABLE [dbo].[conversiondivisa]
    ADD CONSTRAINT [ck_conversiondivisa_column] CHECK ([valorvirtual]>(0));
GO
ALTER TABLE [dbo].[denuncia]
    ADD CONSTRAINT [ck_denuncia_estado] CHECK ([estado]='RESUELTA' OR [estado]='ENPROCESO' OR [estado]='PENDIENTE');
GO
ALTER TABLE [dbo].[mensaje]
    ADD CONSTRAINT [ck_mensaje_emisorexiste] CHECK ([emisor]=[chat1] OR [emisor]=[chat2]);
GO
ALTER TABLE [dbo].[prediccion]
    ADD CONSTRAINT [ck_prediccion_estado] CHECK ([estado]='FINALIZADO' OR [estado]='CERRADO' OR [estado]='ACTIVO');
GO
ALTER TABLE [dbo].[tranaapuesta]
    ADD CONSTRAINT [ck_tranaapuesta_voto] CHECK ([voto]='NO' OR [voto]='SI');
GO
ALTER TABLE [dbo].[transaccion]
    ADD CONSTRAINT [ck_transaccion_cantidad] CHECK ([cantidad]>(0));
GO
ALTER TABLE [dbo].[wallet]
    ADD CONSTRAINT [ck_wallet_column] CHECK ([saldo]>=(0) AND [saldoretenido]>=(0));
GO

PRINT N'Creando Desencadenador [dbo].[trigger_createwalletcomunidad]...';
GO

CREATE TRIGGER [dbo].[trigger_createwalletcomunidad]
    ON [dbo].[comunidad]
    FOR INSERT
    AS
    BEGIN
        SET NoCount ON

        DECLARE @walletId INT;

        INSERT INTO wallet (saldo, saldoretenido) VALUES (0, 0);
        SET @walletId = SCOPE_IDENTITY();

        UPDATE comunidad set wallet = @walletId where id = (SELECT id FROM inserted);
    END
GO

PRINT N'Creando Desencadenador [dbo].[trigger_createwallet]...';
GO

CREATE TRIGGER [dbo].[trigger_createwallet]
    ON [dbo].[usuario]
    FOR INSERT
    AS
    BEGIN
        SET NoCount ON;

        DECLARE @walletId INT;

        INSERT INTO wallet (saldo, saldoretenido) VALUES (0, 0);
        SET @walletId = SCOPE_IDENTITY();

        UPDATE usuario set wallet = @walletId Where email = (SELECT email FROM inserted);
    END
GO

DECLARE @VarDecimalSupported AS BIT;
SELECT @VarDecimalSupported = 0;
IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;
IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'Database', 'ON';
    END
GO

PRINT N'Actualización completada.';
GO

INSERT INTO [dbo].[usuario] ([email], [nombre], [apellidos], [telefono], [nickname], [password], [salt], [admin]) VALUES (N'administrador@hada.com', N'Administrador', N'Hada', N'123456789', N'admin', N'oNv9DhDw8nEpylnuoII/QkIlocfCXG1JrLP7FcXSH70=', N'cXN7MjuBjDO2qeUGukcuxA==', 1);

INSERT INTO [dbo].[catPrediccion] ([categoria]) VALUES (N'Política');
INSERT INTO [dbo].[catPrediccion] ([categoria]) VALUES (N'Tecnología');
INSERT INTO [dbo].[catPrediccion] ([categoria]) VALUES (N'Entretenimiento');
INSERT INTO [dbo].[catPrediccion] ([categoria]) VALUES (N'Deportes');
INSERT INTO [dbo].[catPrediccion] ([categoria]) VALUES (N'Cryptos');
INSERT INTO [dbo].[catPrediccion] ([categoria]) VALUES (N'Otras');

INSERT INTO [dbo].[conversiondivisa] ([moneda], [valorvirtual]) VALUES (N'USD', 2);
INSERT INTO [dbo].[conversiondivisa] ([moneda], [valorvirtual]) VALUES (N'EUR', 2.2);
INSERT INTO [dbo].[conversiondivisa] ([moneda], [valorvirtual]) VALUES (N'GBP', 1.3);

INSERT INTO [dbo].[notificacion] ([titulo], [mensaje]) VALUES (N'Bienvenido', N'Gracias por registrate en PredictLAB');
INSERT INTO [dbo].[logro] ([titulo], [descripcion], [recompensa]) VALUES (N'BIENVENIDA', N'Has creado tu cuenta en PredictLAB', 10);