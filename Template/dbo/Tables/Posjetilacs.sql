CREATE TABLE [dbo].[Posjetilacs] (
    [IdPosjetioca]       INT            IDENTITY (1, 1) NOT NULL,
    [Ime]                NVARCHAR (MAX) NOT NULL,
    [Prezime]            NVARCHAR (MAX) NOT NULL,
    [BlagajnikIdRadnika] INT            NOT NULL,
    CONSTRAINT [PK_Posjetilacs] PRIMARY KEY CLUSTERED ([IdPosjetioca] ASC),
    CONSTRAINT [FK_PosjetilacBlagajnik] FOREIGN KEY ([BlagajnikIdRadnika]) REFERENCES [dbo].[Radniks_Blagajnik] ([IdRadnika])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_PosjetilacBlagajnik]
    ON [dbo].[Posjetilacs]([BlagajnikIdRadnika] ASC);

