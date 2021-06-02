CREATE TABLE [dbo].[Glumacs] (
    [IdGlumca]  INT            IDENTITY (1, 1) NOT NULL,
    [Ime]       NVARCHAR (MAX) NOT NULL,
    [Prezime]   NVARCHAR (MAX) NOT NULL,
    [BrojUloga] INT            NOT NULL,
    CONSTRAINT [PK_Glumacs] PRIMARY KEY CLUSTERED ([IdGlumca] ASC)
);

