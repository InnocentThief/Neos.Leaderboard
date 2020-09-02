CREATE TABLE [dbo].[Quest] (
    [QuestKey]    UNIQUEIDENTIFIER NOT NULL,
    [AccountKey]  UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (50)    NOT NULL,
    [Description] NVARCHAR (4000)  NOT NULL,
    CONSTRAINT [PK_Quest] PRIMARY KEY CLUSTERED ([QuestKey] ASC),
    CONSTRAINT [FK_Quest_Account] FOREIGN KEY ([AccountKey]) REFERENCES [dbo].[Account] ([AccountKey])
);

