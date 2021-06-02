CREATE TABLE [dbo].[Films] (
    [IdFilma]               INT            IDENTITY (1, 1) NOT NULL,
    [Trajanje]              DECIMAL (18)   NOT NULL,
    [Zanr]                  NVARCHAR (MAX) NOT NULL,
    [Naziv]                 NVARCHAR (MAX) NOT NULL,
    [RepertoarIdRepertoara] INT            NULL,
    CONSTRAINT [PK_Films] PRIMARY KEY CLUSTERED ([IdFilma] ASC),
    CONSTRAINT [FK_FilmRepertoar] FOREIGN KEY ([RepertoarIdRepertoara]) REFERENCES [dbo].[Repertoars] ([IdRepertoara])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_FilmRepertoar]
    ON [dbo].[Films]([RepertoarIdRepertoara] ASC);

