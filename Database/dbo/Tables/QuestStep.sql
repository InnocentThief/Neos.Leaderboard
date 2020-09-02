CREATE TABLE [dbo].[QuestStep] (
    [QuestStepKey] UNIQUEIDENTIFIER NOT NULL,
    [QuestKey]     UNIQUEIDENTIFIER NOT NULL,
    [Name]         NVARCHAR (50)    NOT NULL,
    [Description]  NVARCHAR (4000)  NULL,
    [SortOrder]    INT              NOT NULL,
    CONSTRAINT [PK_QuestStep] PRIMARY KEY CLUSTERED ([QuestStepKey] ASC),
    CONSTRAINT [FK_QuestStep_Quest] FOREIGN KEY ([QuestKey]) REFERENCES [dbo].[Quest] ([QuestKey])
);





