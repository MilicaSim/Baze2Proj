CREATE TABLE [dbo].[Radniks_Cistacica] (
    [BrojOcistenihSala] INT NOT NULL,
    [IdRadnika]         INT NOT NULL,
    CONSTRAINT [PK_Radniks_Cistacica] PRIMARY KEY CLUSTERED ([IdRadnika] ASC),
    CONSTRAINT [FK_Cistacica_inherits_Radnik] FOREIGN KEY ([IdRadnika]) REFERENCES [dbo].[Radniks] ([IdRadnika]) ON DELETE CASCADE
);

