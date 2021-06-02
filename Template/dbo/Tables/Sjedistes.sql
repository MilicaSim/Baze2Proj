CREATE TABLE [dbo].[Sjedistes] (
    [IdSjedista] INT IDENTITY (1, 1) NOT NULL,
    [RedniBroj]  INT NOT NULL,
    [Red]        INT NOT NULL,
    [Zauzeto]    BIT NOT NULL,
    CONSTRAINT [PK_Sjedistes] PRIMARY KEY CLUSTERED ([IdSjedista] ASC)
);

