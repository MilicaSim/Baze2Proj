CREATE TABLE [dbo].[Salas] (
    [IdSale]     INT            IDENTITY (1, 1) NOT NULL,
    [Kapacitet]  INT            NOT NULL,
    [BrojRedova] INT            NOT NULL,
    [Naziv]      NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Salas] PRIMARY KEY CLUSTERED ([IdSale] ASC)
);

