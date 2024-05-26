IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Estados] (
    [idEstado] int NOT NULL IDENTITY,
    [tipo] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Estados] PRIMARY KEY ([idEstado])
);
GO

CREATE TABLE [Permisos] (
    [idPermiso] int NOT NULL IDENTITY,
    [tipo] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Permisos] PRIMARY KEY ([idPermiso])
);
GO

CREATE TABLE [Roles] (
    [idRol] int NOT NULL IDENTITY,
    [nombre] nvarchar(max) NOT NULL,
    [descripcion] nvarchar(max) NOT NULL,
    [idPermiso] int NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([idRol]),
    CONSTRAINT [FK_Roles_Permisos_idPermiso] FOREIGN KEY ([idPermiso]) REFERENCES [Permisos] ([idPermiso]) ON DELETE CASCADE
);
GO

CREATE TABLE [Usuarios] (
    [idUsuario] int NOT NULL IDENTITY,
    [nombreUsuario] nvarchar(max) NOT NULL,
    [password] nvarchar(max) NOT NULL,
    [idRoles] int NOT NULL,
    [idEstado] int NOT NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY ([idUsuario]),
    CONSTRAINT [FK_Usuarios_Estados_idEstado] FOREIGN KEY ([idEstado]) REFERENCES [Estados] ([idEstado]) ON DELETE CASCADE,
    CONSTRAINT [FK_Usuarios_Roles_idRoles] FOREIGN KEY ([idRoles]) REFERENCES [Roles] ([idRol]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Roles_idPermiso] ON [Roles] ([idPermiso]);
GO

CREATE INDEX [IX_Usuarios_idEstado] ON [Usuarios] ([idEstado]);
GO

CREATE INDEX [IX_Usuarios_idRoles] ON [Usuarios] ([idRoles]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240516153211_seCreanLasTablasDeUsuario', N'7.0.12');
GO

COMMIT;
GO

