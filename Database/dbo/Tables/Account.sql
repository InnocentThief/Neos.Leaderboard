CREATE TABLE [dbo].[Account] (
    [AccountKey]   UNIQUEIDENTIFIER NOT NULL,
    [Username]     NVARCHAR (100)   NOT NULL,
    [PasswordHash] BINARY (32)      NOT NULL,
    [Salt]         BINARY (32)      NOT NULL,
    CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED ([AccountKey] ASC)
);



