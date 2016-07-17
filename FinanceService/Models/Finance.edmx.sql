
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/16/2016 20:21:41
-- Generated from EDMX file: C:\Repos\Finance\Finance\FinanceService\Models\Finance.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Finance];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_TransactionPlan_Transaction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FactPlan] DROP CONSTRAINT [FK_TransactionPlan_Transaction];
GO
IF OBJECT_ID(N'[dbo].[FK_TransactionPlan_Plan]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FactPlan] DROP CONSTRAINT [FK_TransactionPlan_Plan];
GO
IF OBJECT_ID(N'[dbo].[FK_PlanTargetCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TargetCategorySet] DROP CONSTRAINT [FK_PlanTargetCategory];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PlanSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PlanSet];
GO
IF OBJECT_ID(N'[dbo].[TargetCategorySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TargetCategorySet];
GO
IF OBJECT_ID(N'[dbo].[TransactionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransactionSet];
GO
IF OBJECT_ID(N'[dbo].[FactPlan]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FactPlan];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PlanSet'
CREATE TABLE [dbo].[PlanSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Amount] bigint  NOT NULL,
    [DateMin] datetime  NOT NULL,
    [DateMax] datetime  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'TargetCategorySet'
CREATE TABLE [dbo].[TargetCategorySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [PlanId] int  NOT NULL,
    [TransactionId] int  NOT NULL
);
GO

-- Creating table 'TransactionSet'
CREATE TABLE [dbo].[TransactionSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Amount] bigint  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'FactPlan'
CREATE TABLE [dbo].[FactPlan] (
    [Transaction_Id] int  NOT NULL,
    [Plan_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'PlanSet'
ALTER TABLE [dbo].[PlanSet]
ADD CONSTRAINT [PK_PlanSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TargetCategorySet'
ALTER TABLE [dbo].[TargetCategorySet]
ADD CONSTRAINT [PK_TargetCategorySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TransactionSet'
ALTER TABLE [dbo].[TransactionSet]
ADD CONSTRAINT [PK_TransactionSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Transaction_Id], [Plan_Id] in table 'FactPlan'
ALTER TABLE [dbo].[FactPlan]
ADD CONSTRAINT [PK_FactPlan]
    PRIMARY KEY CLUSTERED ([Transaction_Id], [Plan_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Transaction_Id] in table 'FactPlan'
ALTER TABLE [dbo].[FactPlan]
ADD CONSTRAINT [FK_TransactionPlan_Transaction]
    FOREIGN KEY ([Transaction_Id])
    REFERENCES [dbo].[TransactionSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Plan_Id] in table 'FactPlan'
ALTER TABLE [dbo].[FactPlan]
ADD CONSTRAINT [FK_TransactionPlan_Plan]
    FOREIGN KEY ([Plan_Id])
    REFERENCES [dbo].[PlanSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransactionPlan_Plan'
CREATE INDEX [IX_FK_TransactionPlan_Plan]
ON [dbo].[FactPlan]
    ([Plan_Id]);
GO

-- Creating foreign key on [PlanId] in table 'TargetCategorySet'
ALTER TABLE [dbo].[TargetCategorySet]
ADD CONSTRAINT [FK_PlanTargetCategory]
    FOREIGN KEY ([PlanId])
    REFERENCES [dbo].[PlanSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlanTargetCategory'
CREATE INDEX [IX_FK_PlanTargetCategory]
ON [dbo].[TargetCategorySet]
    ([PlanId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------