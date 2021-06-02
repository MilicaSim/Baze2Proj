CREATE TABLE [dbo].[Radniks_Menadzer] (
    [Staz]      INT NOT NULL,
    [IdRadnika] INT NOT NULL,
    CONSTRAINT [PK_Radniks_Menadzer] PRIMARY KEY CLUSTERED ([IdRadnika] ASC),
    CONSTRAINT [FK_Menadzer_inherits_Radnik] FOREIGN KEY ([IdRadnika]) REFERENCES [dbo].[Radniks] ([IdRadnika]) ON DELETE CASCADE
);

