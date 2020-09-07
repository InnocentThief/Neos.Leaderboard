CREATE TABLE [dbo].[QuestStepProgression] (
    [QuestStepProgressionKey] UNIQUEIDENTIFIER NOT NULL,
    [QuestStepKey]            UNIQUEIDENTIFIER NOT NULL,
    [AccountKey]              UNIQUEIDENTIFIER NOT NULL,
    [ResolvedOn]              DATETIME         NOT NULL,
    CONSTRAINT [PK_QuestStepProgression] PRIMARY KEY CLUSTERED ([QuestStepProgressionKey] ASC),
    CONSTRAINT [FK_QuestStepProgression_Account] FOREIGN KEY ([AccountKey]) REFERENCES [dbo].[Account] ([AccountKey]),
    CONSTRAINT [FK_QuestStepProgression_QuestStep] FOREIGN KEY ([QuestStepKey]) REFERENCES [dbo].[QuestStep] ([QuestStepKey])
);

