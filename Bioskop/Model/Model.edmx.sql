
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/02/2021 08:58:39
-- Generated from EDMX file: C:\Users\Lenovo\Desktop\Template\Bioskop\Model\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Baze2];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_SalaCistacica_Sala]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SalaCistacica] DROP CONSTRAINT [FK_SalaCistacica_Sala];
GO
IF OBJECT_ID(N'[dbo].[FK_SalaCistacica_Cistacica]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SalaCistacica] DROP CONSTRAINT [FK_SalaCistacica_Cistacica];
GO
IF OBJECT_ID(N'[dbo].[FK_FilmGlumac_Film]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FilmGlumac] DROP CONSTRAINT [FK_FilmGlumac_Film];
GO
IF OBJECT_ID(N'[dbo].[FK_FilmGlumac_Glumac]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FilmGlumac] DROP CONSTRAINT [FK_FilmGlumac_Glumac];
GO
IF OBJECT_ID(N'[dbo].[FK_MenadzerRepertoar]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Repertoars] DROP CONSTRAINT [FK_MenadzerRepertoar];
GO
IF OBJECT_ID(N'[dbo].[FK_FilmRepertoar]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Films] DROP CONSTRAINT [FK_FilmRepertoar];
GO
IF OBJECT_ID(N'[dbo].[FK_PosjetilacBlagajnik]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posjetilacs] DROP CONSTRAINT [FK_PosjetilacBlagajnik];
GO
IF OBJECT_ID(N'[dbo].[FK_KartaPosjetilac]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Kartas] DROP CONSTRAINT [FK_KartaPosjetilac];
GO
IF OBJECT_ID(N'[dbo].[FK_SadrziSjediste]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sadrzis] DROP CONSTRAINT [FK_SadrziSjediste];
GO
IF OBJECT_ID(N'[dbo].[FK_SadrziSala]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sadrzis] DROP CONSTRAINT [FK_SadrziSala];
GO
IF OBJECT_ID(N'[dbo].[FK_SadrziPrikazuje]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Prikazujes] DROP CONSTRAINT [FK_SadrziPrikazuje];
GO
IF OBJECT_ID(N'[dbo].[FK_PrikazujeFilm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Prikazujes] DROP CONSTRAINT [FK_PrikazujeFilm];
GO
IF OBJECT_ID(N'[dbo].[FK_PrikazujeKarta]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Kartas] DROP CONSTRAINT [FK_PrikazujeKarta];
GO
IF OBJECT_ID(N'[dbo].[FK_Cistacica_inherits_Radnik]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Radniks_Cistacica] DROP CONSTRAINT [FK_Cistacica_inherits_Radnik];
GO
IF OBJECT_ID(N'[dbo].[FK_Menadzer_inherits_Radnik]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Radniks_Menadzer] DROP CONSTRAINT [FK_Menadzer_inherits_Radnik];
GO
IF OBJECT_ID(N'[dbo].[FK_Blagajnik_inherits_Radnik]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Radniks_Blagajnik] DROP CONSTRAINT [FK_Blagajnik_inherits_Radnik];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Radniks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Radniks];
GO
IF OBJECT_ID(N'[dbo].[Salas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Salas];
GO
IF OBJECT_ID(N'[dbo].[Sjedistes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sjedistes];
GO
IF OBJECT_ID(N'[dbo].[Films]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Films];
GO
IF OBJECT_ID(N'[dbo].[Glumacs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Glumacs];
GO
IF OBJECT_ID(N'[dbo].[Prikazujes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Prikazujes];
GO
IF OBJECT_ID(N'[dbo].[Repertoars]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Repertoars];
GO
IF OBJECT_ID(N'[dbo].[Posjetilacs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Posjetilacs];
GO
IF OBJECT_ID(N'[dbo].[Kartas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Kartas];
GO
IF OBJECT_ID(N'[dbo].[Sadrzis]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sadrzis];
GO
IF OBJECT_ID(N'[dbo].[Radniks_Cistacica]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Radniks_Cistacica];
GO
IF OBJECT_ID(N'[dbo].[Radniks_Menadzer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Radniks_Menadzer];
GO
IF OBJECT_ID(N'[dbo].[Radniks_Blagajnik]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Radniks_Blagajnik];
GO
IF OBJECT_ID(N'[dbo].[SalaCistacica]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SalaCistacica];
GO
IF OBJECT_ID(N'[dbo].[FilmGlumac]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FilmGlumac];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Radniks'
CREATE TABLE [dbo].[Radniks] (
    [IdRadnika] int IDENTITY(1,1) NOT NULL,
    [Ime] nvarchar(max)  NOT NULL,
    [Prezime] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Salas'
CREATE TABLE [dbo].[Salas] (
    [IdSale] int IDENTITY(1,1) NOT NULL,
    [Kapacitet] int  NOT NULL,
    [BrojRedova] int  NOT NULL,
    [Naziv] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Sjedistes'
CREATE TABLE [dbo].[Sjedistes] (
    [IdSjedista] int IDENTITY(1,1) NOT NULL,
    [RedniBroj] int  NOT NULL,
    [Red] int  NOT NULL,
    [Zauzeto] bit  NOT NULL
);
GO

-- Creating table 'Films'
CREATE TABLE [dbo].[Films] (
    [IdFilma] int IDENTITY(1,1) NOT NULL,
    [Trajanje] decimal(18,0)  NOT NULL,
    [Zanr] nvarchar(max)  NOT NULL,
    [Naziv] nvarchar(max)  NOT NULL,
    [RepertoarIdRepertoara] int  NULL
);
GO

-- Creating table 'Glumacs'
CREATE TABLE [dbo].[Glumacs] (
    [IdGlumca] int IDENTITY(1,1) NOT NULL,
    [Ime] nvarchar(max)  NOT NULL,
    [Prezime] nvarchar(max)  NOT NULL,
    [BrojUloga] int  NOT NULL
);
GO

-- Creating table 'Prikazujes'
CREATE TABLE [dbo].[Prikazujes] (
    [Termin] datetime  NOT NULL,
    [IdPrikazivanja] int IDENTITY(1,1) NOT NULL,
    [SadrziSjedisteIdSjedista] int  NULL,
    [SadrziSalaIdSale] int  NOT NULL,
    [FilmIdFilma] int  NOT NULL
);
GO

-- Creating table 'Repertoars'
CREATE TABLE [dbo].[Repertoars] (
    [IdRepertoara] int IDENTITY(1,1) NOT NULL,
    [Naziv] nvarchar(max)  NOT NULL,
    [Trajanje] int  NOT NULL,
    [MenadzerIdRadnika] int  NOT NULL
);
GO

-- Creating table 'Posjetilacs'
CREATE TABLE [dbo].[Posjetilacs] (
    [IdPosjetioca] int IDENTITY(1,1) NOT NULL,
    [Ime] nvarchar(max)  NOT NULL,
    [Prezime] nvarchar(max)  NOT NULL,
    [BlagajnikIdRadnika] int  NOT NULL
);
GO

-- Creating table 'Kartas'
CREATE TABLE [dbo].[Kartas] (
    [IdKarte] int IDENTITY(1,1) NOT NULL,
    [Cijena] int  NOT NULL,
    [PosjetilacIdPosjetioca] int  NULL,
    [PrikazujeIdPrikazivanja] int  NULL
);
GO

-- Creating table 'Sadrzis'
CREATE TABLE [dbo].[Sadrzis] (
    [SjedisteIdSjedista] int  NOT NULL,
    [SalaIdSale] int  NOT NULL
);
GO

-- Creating table 'Radniks_Cistacica'
CREATE TABLE [dbo].[Radniks_Cistacica] (
    [BrojOcistenihSala] int  NOT NULL,
    [IdRadnika] int  NOT NULL
);
GO

-- Creating table 'Radniks_Menadzer'
CREATE TABLE [dbo].[Radniks_Menadzer] (
    [Staz] int  NOT NULL,
    [IdRadnika] int  NOT NULL
);
GO

-- Creating table 'Radniks_Blagajnik'
CREATE TABLE [dbo].[Radniks_Blagajnik] (
    [BrojProdatihKarata] int  NOT NULL,
    [IdRadnika] int  NOT NULL
);
GO

-- Creating table 'SalaCistacica'
CREATE TABLE [dbo].[SalaCistacica] (
    [Salas_IdSale] int  NOT NULL,
    [Cistacicas_IdRadnika] int  NOT NULL
);
GO

-- Creating table 'FilmGlumac'
CREATE TABLE [dbo].[FilmGlumac] (
    [Films_IdFilma] int  NOT NULL,
    [Glumacs_IdGlumca] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdRadnika] in table 'Radniks'
ALTER TABLE [dbo].[Radniks]
ADD CONSTRAINT [PK_Radniks]
    PRIMARY KEY CLUSTERED ([IdRadnika] ASC);
GO

-- Creating primary key on [IdSale] in table 'Salas'
ALTER TABLE [dbo].[Salas]
ADD CONSTRAINT [PK_Salas]
    PRIMARY KEY CLUSTERED ([IdSale] ASC);
GO

-- Creating primary key on [IdSjedista] in table 'Sjedistes'
ALTER TABLE [dbo].[Sjedistes]
ADD CONSTRAINT [PK_Sjedistes]
    PRIMARY KEY CLUSTERED ([IdSjedista] ASC);
GO

-- Creating primary key on [IdFilma] in table 'Films'
ALTER TABLE [dbo].[Films]
ADD CONSTRAINT [PK_Films]
    PRIMARY KEY CLUSTERED ([IdFilma] ASC);
GO

-- Creating primary key on [IdGlumca] in table 'Glumacs'
ALTER TABLE [dbo].[Glumacs]
ADD CONSTRAINT [PK_Glumacs]
    PRIMARY KEY CLUSTERED ([IdGlumca] ASC);
GO

-- Creating primary key on [IdPrikazivanja] in table 'Prikazujes'
ALTER TABLE [dbo].[Prikazujes]
ADD CONSTRAINT [PK_Prikazujes]
    PRIMARY KEY CLUSTERED ([IdPrikazivanja] ASC);
GO

-- Creating primary key on [IdRepertoara] in table 'Repertoars'
ALTER TABLE [dbo].[Repertoars]
ADD CONSTRAINT [PK_Repertoars]
    PRIMARY KEY CLUSTERED ([IdRepertoara] ASC);
GO

-- Creating primary key on [IdPosjetioca] in table 'Posjetilacs'
ALTER TABLE [dbo].[Posjetilacs]
ADD CONSTRAINT [PK_Posjetilacs]
    PRIMARY KEY CLUSTERED ([IdPosjetioca] ASC);
GO

-- Creating primary key on [IdKarte] in table 'Kartas'
ALTER TABLE [dbo].[Kartas]
ADD CONSTRAINT [PK_Kartas]
    PRIMARY KEY CLUSTERED ([IdKarte] ASC);
GO

-- Creating primary key on [SjedisteIdSjedista], [SalaIdSale] in table 'Sadrzis'
ALTER TABLE [dbo].[Sadrzis]
ADD CONSTRAINT [PK_Sadrzis]
    PRIMARY KEY CLUSTERED ([SjedisteIdSjedista], [SalaIdSale] ASC);
GO

-- Creating primary key on [IdRadnika] in table 'Radniks_Cistacica'
ALTER TABLE [dbo].[Radniks_Cistacica]
ADD CONSTRAINT [PK_Radniks_Cistacica]
    PRIMARY KEY CLUSTERED ([IdRadnika] ASC);
GO

-- Creating primary key on [IdRadnika] in table 'Radniks_Menadzer'
ALTER TABLE [dbo].[Radniks_Menadzer]
ADD CONSTRAINT [PK_Radniks_Menadzer]
    PRIMARY KEY CLUSTERED ([IdRadnika] ASC);
GO

-- Creating primary key on [IdRadnika] in table 'Radniks_Blagajnik'
ALTER TABLE [dbo].[Radniks_Blagajnik]
ADD CONSTRAINT [PK_Radniks_Blagajnik]
    PRIMARY KEY CLUSTERED ([IdRadnika] ASC);
GO

-- Creating primary key on [Salas_IdSale], [Cistacicas_IdRadnika] in table 'SalaCistacica'
ALTER TABLE [dbo].[SalaCistacica]
ADD CONSTRAINT [PK_SalaCistacica]
    PRIMARY KEY CLUSTERED ([Salas_IdSale], [Cistacicas_IdRadnika] ASC);
GO

-- Creating primary key on [Films_IdFilma], [Glumacs_IdGlumca] in table 'FilmGlumac'
ALTER TABLE [dbo].[FilmGlumac]
ADD CONSTRAINT [PK_FilmGlumac]
    PRIMARY KEY CLUSTERED ([Films_IdFilma], [Glumacs_IdGlumca] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Salas_IdSale] in table 'SalaCistacica'
ALTER TABLE [dbo].[SalaCistacica]
ADD CONSTRAINT [FK_SalaCistacica_Sala]
    FOREIGN KEY ([Salas_IdSale])
    REFERENCES [dbo].[Salas]
        ([IdSale])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Cistacicas_IdRadnika] in table 'SalaCistacica'
ALTER TABLE [dbo].[SalaCistacica]
ADD CONSTRAINT [FK_SalaCistacica_Cistacica]
    FOREIGN KEY ([Cistacicas_IdRadnika])
    REFERENCES [dbo].[Radniks_Cistacica]
        ([IdRadnika])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SalaCistacica_Cistacica'
CREATE INDEX [IX_FK_SalaCistacica_Cistacica]
ON [dbo].[SalaCistacica]
    ([Cistacicas_IdRadnika]);
GO

-- Creating foreign key on [Films_IdFilma] in table 'FilmGlumac'
ALTER TABLE [dbo].[FilmGlumac]
ADD CONSTRAINT [FK_FilmGlumac_Film]
    FOREIGN KEY ([Films_IdFilma])
    REFERENCES [dbo].[Films]
        ([IdFilma])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Glumacs_IdGlumca] in table 'FilmGlumac'
ALTER TABLE [dbo].[FilmGlumac]
ADD CONSTRAINT [FK_FilmGlumac_Glumac]
    FOREIGN KEY ([Glumacs_IdGlumca])
    REFERENCES [dbo].[Glumacs]
        ([IdGlumca])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FilmGlumac_Glumac'
CREATE INDEX [IX_FK_FilmGlumac_Glumac]
ON [dbo].[FilmGlumac]
    ([Glumacs_IdGlumca]);
GO

-- Creating foreign key on [MenadzerIdRadnika] in table 'Repertoars'
ALTER TABLE [dbo].[Repertoars]
ADD CONSTRAINT [FK_MenadzerRepertoar]
    FOREIGN KEY ([MenadzerIdRadnika])
    REFERENCES [dbo].[Radniks_Menadzer]
        ([IdRadnika])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MenadzerRepertoar'
CREATE INDEX [IX_FK_MenadzerRepertoar]
ON [dbo].[Repertoars]
    ([MenadzerIdRadnika]);
GO

-- Creating foreign key on [RepertoarIdRepertoara] in table 'Films'
ALTER TABLE [dbo].[Films]
ADD CONSTRAINT [FK_FilmRepertoar]
    FOREIGN KEY ([RepertoarIdRepertoara])
    REFERENCES [dbo].[Repertoars]
        ([IdRepertoara])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FilmRepertoar'
CREATE INDEX [IX_FK_FilmRepertoar]
ON [dbo].[Films]
    ([RepertoarIdRepertoara]);
GO

-- Creating foreign key on [BlagajnikIdRadnika] in table 'Posjetilacs'
ALTER TABLE [dbo].[Posjetilacs]
ADD CONSTRAINT [FK_PosjetilacBlagajnik]
    FOREIGN KEY ([BlagajnikIdRadnika])
    REFERENCES [dbo].[Radniks_Blagajnik]
        ([IdRadnika])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PosjetilacBlagajnik'
CREATE INDEX [IX_FK_PosjetilacBlagajnik]
ON [dbo].[Posjetilacs]
    ([BlagajnikIdRadnika]);
GO

-- Creating foreign key on [PosjetilacIdPosjetioca] in table 'Kartas'
ALTER TABLE [dbo].[Kartas]
ADD CONSTRAINT [FK_KartaPosjetilac]
    FOREIGN KEY ([PosjetilacIdPosjetioca])
    REFERENCES [dbo].[Posjetilacs]
        ([IdPosjetioca])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_KartaPosjetilac'
CREATE INDEX [IX_FK_KartaPosjetilac]
ON [dbo].[Kartas]
    ([PosjetilacIdPosjetioca]);
GO

-- Creating foreign key on [SjedisteIdSjedista] in table 'Sadrzis'
ALTER TABLE [dbo].[Sadrzis]
ADD CONSTRAINT [FK_SadrziSjediste]
    FOREIGN KEY ([SjedisteIdSjedista])
    REFERENCES [dbo].[Sjedistes]
        ([IdSjedista])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SalaIdSale] in table 'Sadrzis'
ALTER TABLE [dbo].[Sadrzis]
ADD CONSTRAINT [FK_SadrziSala]
    FOREIGN KEY ([SalaIdSale])
    REFERENCES [dbo].[Salas]
        ([IdSale])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SadrziSala'
CREATE INDEX [IX_FK_SadrziSala]
ON [dbo].[Sadrzis]
    ([SalaIdSale]);
GO

-- Creating foreign key on [SadrziSjedisteIdSjedista], [SadrziSalaIdSale] in table 'Prikazujes'
ALTER TABLE [dbo].[Prikazujes]
ADD CONSTRAINT [FK_SadrziPrikazuje]
    FOREIGN KEY ([SadrziSjedisteIdSjedista], [SadrziSalaIdSale])
    REFERENCES [dbo].[Sadrzis]
        ([SjedisteIdSjedista], [SalaIdSale])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SadrziPrikazuje'
CREATE INDEX [IX_FK_SadrziPrikazuje]
ON [dbo].[Prikazujes]
    ([SadrziSjedisteIdSjedista], [SadrziSalaIdSale]);
GO

-- Creating foreign key on [FilmIdFilma] in table 'Prikazujes'
ALTER TABLE [dbo].[Prikazujes]
ADD CONSTRAINT [FK_PrikazujeFilm]
    FOREIGN KEY ([FilmIdFilma])
    REFERENCES [dbo].[Films]
        ([IdFilma])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PrikazujeFilm'
CREATE INDEX [IX_FK_PrikazujeFilm]
ON [dbo].[Prikazujes]
    ([FilmIdFilma]);
GO

-- Creating foreign key on [PrikazujeIdPrikazivanja] in table 'Kartas'
ALTER TABLE [dbo].[Kartas]
ADD CONSTRAINT [FK_PrikazujeKarta]
    FOREIGN KEY ([PrikazujeIdPrikazivanja])
    REFERENCES [dbo].[Prikazujes]
        ([IdPrikazivanja])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PrikazujeKarta'
CREATE INDEX [IX_FK_PrikazujeKarta]
ON [dbo].[Kartas]
    ([PrikazujeIdPrikazivanja]);
GO

-- Creating foreign key on [IdRadnika] in table 'Radniks_Cistacica'
ALTER TABLE [dbo].[Radniks_Cistacica]
ADD CONSTRAINT [FK_Cistacica_inherits_Radnik]
    FOREIGN KEY ([IdRadnika])
    REFERENCES [dbo].[Radniks]
        ([IdRadnika])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [IdRadnika] in table 'Radniks_Menadzer'
ALTER TABLE [dbo].[Radniks_Menadzer]
ADD CONSTRAINT [FK_Menadzer_inherits_Radnik]
    FOREIGN KEY ([IdRadnika])
    REFERENCES [dbo].[Radniks]
        ([IdRadnika])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [IdRadnika] in table 'Radniks_Blagajnik'
ALTER TABLE [dbo].[Radniks_Blagajnik]
ADD CONSTRAINT [FK_Blagajnik_inherits_Radnik]
    FOREIGN KEY ([IdRadnika])
    REFERENCES [dbo].[Radniks]
        ([IdRadnika])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------