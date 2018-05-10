CREATE TABLE [dbo].[SYSUserRole] (
    [SYSUserRoleID]        INT      IDENTITY (1, 1) NOT NULL,
    [SYSUserID]            INT      NOT NULL,
    [LOOKUPRoleID]         INT      NOT NULL DEFAULT 2,
    [IsActive]             BIT      DEFAULT ((1)) NULL,
    [RowCreatedSYSUserID]  INT      NOT NULL,
    [RowCreatedDateTime]   DATETIME DEFAULT (getdate()) NULL,
    [RowModifiedSYSUserID] INT      NOT NULL,
    [RowModifiedDateTime]  DATETIME DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([SYSUserRoleID] ASC),
    FOREIGN KEY ([LOOKUPRoleID]) REFERENCES [dbo].[LOOKUPRole] ([LOOKUPRoleID]),
    FOREIGN KEY ([SYSUserID]) REFERENCES [dbo].[SYSUser] ([SYSUserID])
);

