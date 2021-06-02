CREATE TABLE [dbo].[SalaCistacica] (
    [Salas_IdSale]         INT NOT NULL,
    [Cistacicas_IdRadnika] INT NOT NULL,
    CONSTRAINT [PK_SalaCistacica] PRIMARY KEY CLUSTERED ([Salas_IdSale] ASC, [Cistacicas_IdRadnika] ASC),
    CONSTRAINT [FK_SalaCistacica_Cistacica] FOREIGN KEY ([Cistacicas_IdRadnika]) REFERENCES [dbo].[Radniks_Cistacica] ([IdRadnika]),
    CONSTRAINT [FK_SalaCistacica_Sala] FOREIGN KEY ([Salas_IdSale]) REFERENCES [dbo].[Salas] ([IdSale])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_SalaCistacica_Cistacica]
    ON [dbo].[SalaCistacica]([Cistacicas_IdRadnika] ASC);

