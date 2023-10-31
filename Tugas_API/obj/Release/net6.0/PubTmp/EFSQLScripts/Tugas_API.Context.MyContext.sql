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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231011035853_first')
BEGIN
    CREATE TABLE [Departments] (
        [Dept_Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_Departments] PRIMARY KEY ([Dept_Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231011035853_first')
BEGIN
    CREATE TABLE [Employees] (
        [NIK] nvarchar(450) NOT NULL,
        [FirstName] nvarchar(max) NULL,
        [LastName] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [Address] nvarchar(max) NULL,
        [Department_Id] nvarchar(450) NULL,
        [status] bit NULL,
        CONSTRAINT [PK_Employees] PRIMARY KEY ([NIK]),
        CONSTRAINT [FK_Employees_Departments_Department_Id] FOREIGN KEY ([Department_Id]) REFERENCES [Departments] ([Dept_Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231011035853_first')
BEGIN
    CREATE INDEX [IX_Employees_Department_Id] ON [Employees] ([Department_Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231011035853_first')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231011035853_first', N'7.0.11');
END;
GO

COMMIT;
GO

