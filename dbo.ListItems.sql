CREATE TABLE [dbo].[ListItems] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [AddDateTime] DATETIME       NOT NULL,
    [Title]       NVARCHAR (200) NOT NULL,
    [IsDone]      BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

