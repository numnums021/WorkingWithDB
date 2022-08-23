CREATE TABLE [dbo].[Users] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [Login]       NVARCHAR(50) NULL,
    [Password]    NVARCHAR(50) NULL,
    [Accesslevel] NVARCHAR(50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

