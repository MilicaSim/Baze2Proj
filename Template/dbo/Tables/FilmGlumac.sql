CREATE TABLE [dbo].[FilmGlumac] (
    [Films_IdFilma]    INT NOT NULL,
    [Glumacs_IdGlumca] INT NOT NULL,
    CONSTRAINT [PK_FilmGlumac] PRIMARY KEY CLUSTERED ([Films_IdFilma] ASC, [Glumacs_IdGlumca] ASC),
    CONSTRAINT [FK_FilmGlumac_Film] FOREIGN KEY ([Films_IdFilma]) REFERENCES [dbo].[Films] ([IdFilma]),
    CONSTRAINT [FK_FilmGlumac_Glumac] FOREIGN KEY ([Glumacs_IdGlumca]) REFERENCES [dbo].[Glumacs] ([IdGlumca])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_FilmGlumac_Glumac]
    ON [dbo].[FilmGlumac]([Glumacs_IdGlumca] ASC);

