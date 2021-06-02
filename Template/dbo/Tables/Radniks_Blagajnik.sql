CREATE TABLE [dbo].[Radniks_Blagajnik] (
    [BrojProdatihKarata] INT NOT NULL,
    [IdRadnika]          INT NOT NULL,
    CONSTRAINT [PK_Radniks_Blagajnik] PRIMARY KEY CLUSTERED ([IdRadnika] ASC),
    CONSTRAINT [FK_Blagajnik_inherits_Radnik] FOREIGN KEY ([IdRadnika]) REFERENCES [dbo].[Radniks] ([IdRadnika]) ON DELETE CASCADE
);

