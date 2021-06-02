CREATE TABLE [dbo].[Repertoars] (
    [IdRepertoara]      INT            IDENTITY (1, 1) NOT NULL,
    [Naziv]             NVARCHAR (MAX) NOT NULL,
    [Trajanje]          INT            NOT NULL,
    [MenadzerIdRadnika] INT            NOT NULL,
    CONSTRAINT [PK_Repertoars] PRIMARY KEY CLUSTERED ([IdRepertoara] ASC),
    CONSTRAINT [FK_MenadzerRepertoar] FOREIGN KEY ([MenadzerIdRadnika]) REFERENCES [dbo].[Radniks_Menadzer] ([IdRadnika])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_MenadzerRepertoar]
    ON [dbo].[Repertoars]([MenadzerIdRadnika] ASC);

