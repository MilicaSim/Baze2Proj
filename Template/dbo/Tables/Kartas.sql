CREATE TABLE [dbo].[Kartas] (
    [IdKarte]                 INT IDENTITY (1, 1) NOT NULL,
    [Cijena]                  INT NOT NULL,
    [PosjetilacIdPosjetioca]  INT NULL,
    [PrikazujeIdPrikazivanja] INT NULL,
    CONSTRAINT [PK_Kartas] PRIMARY KEY CLUSTERED ([IdKarte] ASC),
    CONSTRAINT [FK_KartaPosjetilac] FOREIGN KEY ([PosjetilacIdPosjetioca]) REFERENCES [dbo].[Posjetilacs] ([IdPosjetioca]),
    CONSTRAINT [FK_PrikazujeKarta] FOREIGN KEY ([PrikazujeIdPrikazivanja]) REFERENCES [dbo].[Prikazujes] ([IdPrikazivanja])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_KartaPosjetilac]
    ON [dbo].[Kartas]([PosjetilacIdPosjetioca] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_PrikazujeKarta]
    ON [dbo].[Kartas]([PrikazujeIdPrikazivanja] ASC);

