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

CREATE TABLE [Paciente] (
    [Id] uniqueidentifier NOT NULL,
    [Matricula] varchar(100) NULL,
    [DataCadastro] datetime2 NOT NULL,
    [Nome] varchar(200) NOT NULL,
    [Telefone] varchar(20) NOT NULL,
    [RG] varchar(20) NOT NULL,
    [Genero] int NOT NULL,
    [CPF] varchar(14) NOT NULL,
    CONSTRAINT [PK_Paciente] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Endereco] (
    [Id] uniqueidentifier NOT NULL,
    [PacienteId] uniqueidentifier NOT NULL,
    [Logradouro] varchar(200) NOT NULL,
    [Numero] varchar(50) NOT NULL,
    [Complemento] varchar(250) NULL,
    [Cep] varchar(8) NOT NULL,
    [Bairro] varchar(100) NOT NULL,
    [Cidade] varchar(100) NOT NULL,
    [Estado] varchar(50) NOT NULL,
    CONSTRAINT [PK_Endereco] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Endereco_Paciente_PacienteId] FOREIGN KEY ([PacienteId]) REFERENCES [Paciente] ([Id]) ON DELETE NO ACTION
);
GO

CREATE UNIQUE INDEX [IX_Endereco_PacienteId] ON [Endereco] ([PacienteId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210430190733_Initial', N'5.0.5');
GO

COMMIT;
GO

