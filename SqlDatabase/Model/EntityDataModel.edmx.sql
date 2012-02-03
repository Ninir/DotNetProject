
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 02/03/2012 15:42:14
-- Generated from EDMX file: C:\Users\Augusta\Documents\Visual Studio 2010\Projects\AzureBlog\SqlDatabase\Model\EntityDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [AzureBlog];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Mail] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Slug] nvarchar(max)  NOT NULL,
    [ActivationKey] nvarchar(max)  NOT NULL,
    [CreatedAt] nvarchar(max)  NOT NULL,
    [Group_ID] int  NOT NULL
);
GO

-- Creating table 'Articles'
CREATE TABLE [dbo].[Articles] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [Published] bit  NOT NULL,
    [AllowComments] bit  NOT NULL,
    [Highlight] bit  NOT NULL,
    [CommentsCount] int  NOT NULL,
    [Slug] nvarchar(max)  NOT NULL,
    [User_ID] int  NOT NULL,
    [Report_ID] int  NOT NULL
);
GO

-- Creating table 'Tags'
CREATE TABLE [dbo].[Tags] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Slug] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [Published] bit  NOT NULL,
    [Article_ID] int  NOT NULL,
    [User_ID] int  NOT NULL,
    [Report_ID] int  NOT NULL
);
GO

-- Creating table 'Options'
CREATE TABLE [dbo].[Options] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Fullname] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [String] nvarchar(max)  NOT NULL,
    [Integer] int  NOT NULL,
    [DateTime] datetime  NOT NULL,
    [Boolean] bit  NOT NULL,
    [Decimal] decimal(18,0)  NOT NULL,
    [OptionType_ID] int  NOT NULL
);
GO

-- Creating table 'Credentails'
CREATE TABLE [dbo].[Credentails] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Groups'
CREATE TABLE [dbo].[Groups] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Parent_ID] int  NOT NULL
);
GO

-- Creating table 'Reports'
CREATE TABLE [dbo].[Reports] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [Solved] bit  NOT NULL,
    [User_ID] int  NOT NULL
);
GO

-- Creating table 'OptionTypes'
CREATE TABLE [dbo].[OptionTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CredentailUser'
CREATE TABLE [dbo].[CredentailUser] (
    [Credentails_ID] int  NOT NULL,
    [Users_ID] int  NOT NULL
);
GO

-- Creating table 'CredentailGroup'
CREATE TABLE [dbo].[CredentailGroup] (
    [Credentails_ID] int  NOT NULL,
    [Groups_ID] int  NOT NULL
);
GO

-- Creating table 'ArticleTag'
CREATE TABLE [dbo].[ArticleTag] (
    [Articles_ID] int  NOT NULL,
    [Tags_ID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Articles'
ALTER TABLE [dbo].[Articles]
ADD CONSTRAINT [PK_Articles]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Tags'
ALTER TABLE [dbo].[Tags]
ADD CONSTRAINT [PK_Tags]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Options'
ALTER TABLE [dbo].[Options]
ADD CONSTRAINT [PK_Options]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Credentails'
ALTER TABLE [dbo].[Credentails]
ADD CONSTRAINT [PK_Credentails]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [PK_Groups]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Reports'
ALTER TABLE [dbo].[Reports]
ADD CONSTRAINT [PK_Reports]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'OptionTypes'
ALTER TABLE [dbo].[OptionTypes]
ADD CONSTRAINT [PK_OptionTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Credentails_ID], [Users_ID] in table 'CredentailUser'
ALTER TABLE [dbo].[CredentailUser]
ADD CONSTRAINT [PK_CredentailUser]
    PRIMARY KEY NONCLUSTERED ([Credentails_ID], [Users_ID] ASC);
GO

-- Creating primary key on [Credentails_ID], [Groups_ID] in table 'CredentailGroup'
ALTER TABLE [dbo].[CredentailGroup]
ADD CONSTRAINT [PK_CredentailGroup]
    PRIMARY KEY NONCLUSTERED ([Credentails_ID], [Groups_ID] ASC);
GO

-- Creating primary key on [Articles_ID], [Tags_ID] in table 'ArticleTag'
ALTER TABLE [dbo].[ArticleTag]
ADD CONSTRAINT [PK_ArticleTag]
    PRIMARY KEY NONCLUSTERED ([Articles_ID], [Tags_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Parent_ID] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [FK_GroupGroup]
    FOREIGN KEY ([Parent_ID])
    REFERENCES [dbo].[Groups]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupGroup'
CREATE INDEX [IX_FK_GroupGroup]
ON [dbo].[Groups]
    ([Parent_ID]);
GO

-- Creating foreign key on [Group_ID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_UserGroup]
    FOREIGN KEY ([Group_ID])
    REFERENCES [dbo].[Groups]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserGroup'
CREATE INDEX [IX_FK_UserGroup]
ON [dbo].[Users]
    ([Group_ID]);
GO

-- Creating foreign key on [Credentails_ID] in table 'CredentailUser'
ALTER TABLE [dbo].[CredentailUser]
ADD CONSTRAINT [FK_CredentailUser_Credentail]
    FOREIGN KEY ([Credentails_ID])
    REFERENCES [dbo].[Credentails]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_ID] in table 'CredentailUser'
ALTER TABLE [dbo].[CredentailUser]
ADD CONSTRAINT [FK_CredentailUser_User]
    FOREIGN KEY ([Users_ID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CredentailUser_User'
CREATE INDEX [IX_FK_CredentailUser_User]
ON [dbo].[CredentailUser]
    ([Users_ID]);
GO

-- Creating foreign key on [Credentails_ID] in table 'CredentailGroup'
ALTER TABLE [dbo].[CredentailGroup]
ADD CONSTRAINT [FK_CredentailGroup_Credentail]
    FOREIGN KEY ([Credentails_ID])
    REFERENCES [dbo].[Credentails]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Groups_ID] in table 'CredentailGroup'
ALTER TABLE [dbo].[CredentailGroup]
ADD CONSTRAINT [FK_CredentailGroup_Group]
    FOREIGN KEY ([Groups_ID])
    REFERENCES [dbo].[Groups]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CredentailGroup_Group'
CREATE INDEX [IX_FK_CredentailGroup_Group]
ON [dbo].[CredentailGroup]
    ([Groups_ID]);
GO

-- Creating foreign key on [User_ID] in table 'Articles'
ALTER TABLE [dbo].[Articles]
ADD CONSTRAINT [FK_UserArticle]
    FOREIGN KEY ([User_ID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserArticle'
CREATE INDEX [IX_FK_UserArticle]
ON [dbo].[Articles]
    ([User_ID]);
GO

-- Creating foreign key on [Articles_ID] in table 'ArticleTag'
ALTER TABLE [dbo].[ArticleTag]
ADD CONSTRAINT [FK_ArticleTag_Article]
    FOREIGN KEY ([Articles_ID])
    REFERENCES [dbo].[Articles]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Tags_ID] in table 'ArticleTag'
ALTER TABLE [dbo].[ArticleTag]
ADD CONSTRAINT [FK_ArticleTag_Tag]
    FOREIGN KEY ([Tags_ID])
    REFERENCES [dbo].[Tags]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ArticleTag_Tag'
CREATE INDEX [IX_FK_ArticleTag_Tag]
ON [dbo].[ArticleTag]
    ([Tags_ID]);
GO

-- Creating foreign key on [Article_ID] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_CommentArticle]
    FOREIGN KEY ([Article_ID])
    REFERENCES [dbo].[Articles]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CommentArticle'
CREATE INDEX [IX_FK_CommentArticle]
ON [dbo].[Comments]
    ([Article_ID]);
GO

-- Creating foreign key on [User_ID] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_UserComment]
    FOREIGN KEY ([User_ID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserComment'
CREATE INDEX [IX_FK_UserComment]
ON [dbo].[Comments]
    ([User_ID]);
GO

-- Creating foreign key on [User_ID] in table 'Reports'
ALTER TABLE [dbo].[Reports]
ADD CONSTRAINT [FK_ReportUser]
    FOREIGN KEY ([User_ID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ReportUser'
CREATE INDEX [IX_FK_ReportUser]
ON [dbo].[Reports]
    ([User_ID]);
GO

-- Creating foreign key on [Report_ID] in table 'Articles'
ALTER TABLE [dbo].[Articles]
ADD CONSTRAINT [FK_ReportArticle]
    FOREIGN KEY ([Report_ID])
    REFERENCES [dbo].[Reports]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ReportArticle'
CREATE INDEX [IX_FK_ReportArticle]
ON [dbo].[Articles]
    ([Report_ID]);
GO

-- Creating foreign key on [Report_ID] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_ReportComment]
    FOREIGN KEY ([Report_ID])
    REFERENCES [dbo].[Reports]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ReportComment'
CREATE INDEX [IX_FK_ReportComment]
ON [dbo].[Comments]
    ([Report_ID]);
GO

-- Creating foreign key on [OptionType_ID] in table 'Options'
ALTER TABLE [dbo].[Options]
ADD CONSTRAINT [FK_OptionTypeOption]
    FOREIGN KEY ([OptionType_ID])
    REFERENCES [dbo].[OptionTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OptionTypeOption'
CREATE INDEX [IX_FK_OptionTypeOption]
ON [dbo].[Options]
    ([OptionType_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------