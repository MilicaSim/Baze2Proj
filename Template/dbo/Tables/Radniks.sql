CREATE TABLE [dbo].[Radniks] (
    [IdRadnika] INT            IDENTITY (1, 1) NOT NULL,
    [Ime]       NVARCHAR (MAX) NOT NULL,
    [Prezime]   NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Radniks] PRIMARY KEY CLUSTERED ([IdRadnika] ASC)
);

