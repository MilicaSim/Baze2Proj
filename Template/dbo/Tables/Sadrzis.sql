CREATE TABLE [dbo].[Sadrzis] (
    [SjedisteIdSjedista] INT NOT NULL,
    [SalaIdSale]         INT NOT NULL,
    CONSTRAINT [PK_Sadrzis] PRIMARY KEY CLUSTERED ([SjedisteIdSjedista] ASC, [SalaIdSale] ASC),
    CONSTRAINT [FK_SadrziSala] FOREIGN KEY ([SalaIdSale]) REFERENCES [dbo].[Salas] ([IdSale]),
    CONSTRAINT [FK_SadrziSjediste] FOREIGN KEY ([SjedisteIdSjedista]) REFERENCES [dbo].[Sjedistes] ([IdSjedista])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_SadrziSala]
    ON [dbo].[Sadrzis]([SalaIdSale] ASC);

