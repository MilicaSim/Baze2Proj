CREATE TABLE [dbo].[Prikazujes] (
    [Termin]                   DATETIME NOT NULL,
    [IdPrikazivanja]           INT      IDENTITY (1, 1) NOT NULL,
    [SadrziSjedisteIdSjedista] INT      NULL,
    [SadrziSalaIdSale]         INT      NOT NULL,
    [FilmIdFilma]              INT      NOT NULL,
    CONSTRAINT [PK_Prikazujes] PRIMARY KEY CLUSTERED ([IdPrikazivanja] ASC),
    CONSTRAINT [FK_PrikazujeFilm] FOREIGN KEY ([FilmIdFilma]) REFERENCES [dbo].[Films] ([IdFilma]),
    CONSTRAINT [FK_SadrziPrikazuje] FOREIGN KEY ([SadrziSjedisteIdSjedista], [SadrziSalaIdSale]) REFERENCES [dbo].[Sadrzis] ([SjedisteIdSjedista], [SalaIdSale])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_SadrziPrikazuje]
    ON [dbo].[Prikazujes]([SadrziSjedisteIdSjedista] ASC, [SadrziSalaIdSale] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_PrikazujeFilm]
    ON [dbo].[Prikazujes]([FilmIdFilma] ASC);

