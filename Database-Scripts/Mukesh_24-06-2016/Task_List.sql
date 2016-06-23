USE [jgrove_JGP];
GO
/****** Object:  UserDefinedFunction [dbo].[fnGetStringListToID]    Script Date: 06/24/2016 01:38:43 ******/


SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
CREATE FUNCTION [dbo].[fnGetStringListToID]
    (
      @sInputList VARCHAR(8000) = '' ,
      @Delimiter CHAR(1) = ','  
    )
RETURNS @result TABLE ( id INT )
AS
    BEGIN  
  
        DECLARE @Item VARCHAR(8000);   
  
        IF CHARINDEX(@Delimiter, @sInputList, 0) <> 0
            BEGIN  
  
                WHILE CHARINDEX(@Delimiter, @sInputList, 0) <> 0
                    BEGIN  
                        SELECT  @Item = RTRIM(LTRIM(SUBSTRING(@sInputList, 1,
                                                              CHARINDEX(@Delimiter,
                                                              @sInputList, 0)
                                                              - 1))) ,
                                @sInputList = RTRIM(LTRIM(SUBSTRING(@sInputList,
                                                              CHARINDEX(@Delimiter,
                                                              @sInputList, 0)
                                                              + 1,
                                                              LEN(@sInputList))));  
  
                        IF LEN(@Item) > 0
                            INSERT  @result
                                    SELECT  CONVERT(INT, @Item);  
                    END;  
            END;  
  
        IF LEN(@sInputList) > 0
            INSERT  @result
                    SELECT  CONVERT(INT, @sInputList); -- Put the last item in   
  
        RETURN;  
    END;
GO
/****** Object:  Table [dbo].[tblTask]    Script Date: 06/24/2016 01:38:34 ******/
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
CREATE TABLE [dbo].[tblTask]
    (
      [ID] [INT] IDENTITY(1, 1)
                 NOT NULL ,
      [Title] [NVARCHAR](250) NOT NULL ,
      [Description] [NVARCHAR](MAX) NOT NULL ,
      [Designation] [NVARCHAR](50) NOT NULL ,
      [Status] [INT] NOT NULL ,
      [UserAcceptanceID] [INT] NULL ,
      [DueDate] [DATETIME] NOT NULL ,
      [Hours] [NVARCHAR](10) NOT NULL ,
      [Notes] [NVARCHAR](500) NULL ,
      [Attachment] [NVARCHAR](200) NULL ,
      [CreatedBy] [INT] NULL ,
      [CreatedDate] [DATETIME] NULL ,
      [ModifiedBy] [INT] NULL ,
      [ModifiedDate] [INT] NULL ,
      CONSTRAINT [PK_tblTask] PRIMARY KEY CLUSTERED ( [ID] ASC )
        WITH ( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
               IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
               ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
    )
ON  [PRIMARY];
GO

IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[spGetUsersByDesignation]')
                    AND OBJECTPROPERTY(id, N'IsProcedure') = 1 )
    BEGIN
        DROP PROCEDURE [dbo].[spGetUsersByDesignation];
    END;
GO

/****** Object:  StoredProcedure [dbo].[spGetUsersByDesignation]    Script Date: 06/24/2016 01:38:40 ******/
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO


-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE   PROCEDURE [dbo].[spGetUsersByDesignation]
    @Designation NVARCHAR(50) = NULL
AS
    BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
        SET NOCOUNT ON;  
      
        SELECT  Id ,
                FristName + ' ' + LastName AS UserName
        FROM    dbo.tblInstallUsers
        WHERE   Designation IS NOT NULL
                AND ( @Designation IS NULL
                      OR Designation = @Designation
                    )
                AND UserType IN ( 'Sales', 'SalesUser', 'installer' )
        ORDER BY UserName;
    END;
GO
/****** Object:  StoredProcedure [dbo].[spGetAllDesignation]    Script Date: 06/24/2016 01:38:40 ******/
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO


IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[spGetAllDesignation]')
                    AND OBJECTPROPERTY(id, N'IsProcedure') = 1 )
    BEGIN
        DROP PROCEDURE [dbo].[spGetAllDesignation];
    END;
GO

/****** Object:  StoredProcedure [dbo].[spGetUsersByDesignation]    Script Date: 06/24/2016 01:38:40 ******/
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE  PROCEDURE [dbo].[spGetAllDesignation]
AS
    BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
        SET NOCOUNT ON;  
       
      
        SELECT DISTINCT
                Designation
        FROM    dbo.tblInstallUsers
        WHERE   Designation IS NOT NULL
                AND UserType IN ( 'Sales', 'SalesUser', 'installer' );
        
                
       
    END;
GO


/****** Object:  Table [dbo].[tblTaskAssignUsers]    Script Date: 06/24/2016 01:38:34 ******/


SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
CREATE TABLE [dbo].[tblTaskAssignUsers]
    (
      [ID] [INT] IDENTITY(1, 1)
                 NOT NULL ,
      [TaskID] [INT] NOT NULL ,
      [UserID] [INT] NOT NULL ,
      CONSTRAINT [PK_TaskAssignUsers] PRIMARY KEY CLUSTERED ( [ID] ASC )
        WITH ( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
               IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
               ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
    )
ON  [PRIMARY];
GO
/****** Object:  StoredProcedure [dbo].[spGetTaskList]    Script Date: 06/24/2016 01:38:40 ******/

IF EXISTS ( SELECT  *
            FROM    sysobjects
            WHERE   id = OBJECT_ID(N'[dbo].[spGetTaskList]')
                    AND OBJECTPROPERTY(id, N'IsProcedure') = 1 )
    BEGIN
        DROP PROCEDURE [dbo].[spGetTaskList];
    END;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
CREATE    PROCEDURE [dbo].[spGetTaskList]
    @Title NVARCHAR(250) = NULL ,
    @Designation NVARCHAR(50) = NULL ,
    @AssignedUser NVARCHAR(50) = NULL ,
    @Status INT = NULL ,
    @CreatedDate DATE = NULL
AS
    BEGIN      
 -- SET NOCOUNT ON added to prevent extra result sets from      
        SET NOCOUNT ON;      
           
          
        SELECT   DISTINCT TOP 5
                T.ID ,
                T.Title ,
                T.Designation ,
                T.[Status] ,
                T.UserAcceptanceID ,
                T.DueDate ,
                T.[Hours] ,
                T.Notes ,
                T.CreatedBy ,
                T.CreatedDate ,
                T.ModifiedBy ,
                T.ModifiedDate
        INTO    #TMPTask
        FROM    dbo.tblTask T
                LEFT  JOIN dbo.tblTaskAssignUsers tu ON T.ID = tu.TaskID
        WHERE   ( @Title IS NULL
                  OR T.Title LIKE '%' + @Title + '%'
                )
                AND ( @Designation IS NULL
                      OR T.Designation = @Designation
                    )
                AND ( @AssignedUser IS NULL
                      OR tu.UserID IN (
                      SELECT    *
                      FROM      dbo.fnGetStringListToID(@AssignedUser, ',') )
                    )
                AND ( @Status IS NULL
                      OR T.[Status] = @Status
                    )
                AND ( @CreatedDate IS NULL
                      OR CAST(T.CreatedDate AS DATE) = @CreatedDate
                    )
        ORDER BY T.CreatedDate;
                      
           
        --Get Task Data     
        SELECT  *
        FROM    #TMPTask;    
            
        --Get Task assigned users    
        SELECT  *
        FROM    dbo.tblTaskAssignUsers
        WHERE   TaskID IN ( SELECT DISTINCT
                                    TaskID
                            FROM    #TMPTask );    
                    
          --Drop temp table                
        DROP TABLE #TMPTask;    
           
    END;
GO
/****** Object:  ForeignKey [FK_tblTask_tblUsers]    Script Date: 06/24/2016 01:38:34 ******/
ALTER TABLE [dbo].[tblTask]  WITH CHECK ADD  CONSTRAINT [FK_tblTask_tblUsers] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUsers] ([Id]);
GO
ALTER TABLE [dbo].[tblTask] CHECK CONSTRAINT [FK_tblTask_tblUsers];
GO
/****** Object:  ForeignKey [FK_Task_TaskAssignUsers]    Script Date: 06/24/2016 01:38:34 ******/
ALTER TABLE [dbo].[tblTaskAssignUsers]  WITH CHECK ADD  CONSTRAINT [FK_Task_TaskAssignUsers] FOREIGN KEY([TaskID])
REFERENCES [dbo].[tblTask] ([ID]);
GO
ALTER TABLE [dbo].[tblTaskAssignUsers] CHECK CONSTRAINT [FK_Task_TaskAssignUsers];
GO
/****** Object:  ForeignKey [FK_TaskAssignUsers_tblInstallUsers]    Script Date: 06/24/2016 01:38:34 ******/
ALTER TABLE [dbo].[tblTaskAssignUsers]  WITH CHECK ADD  CONSTRAINT [FK_TaskAssignUsers_tblInstallUsers] FOREIGN KEY([UserID])
REFERENCES [dbo].[tblInstallUsers] ([Id]);
GO
ALTER TABLE [dbo].[tblTaskAssignUsers] CHECK CONSTRAINT [FK_TaskAssignUsers_tblInstallUsers];
GO


------------Insert Queries-testing Purpose--

INSERT  INTO dbo.tblTask
        ( Title ,
          Description ,
          Designation ,
          Status ,
          UserAcceptanceID ,
          DueDate ,
          Hours ,
          Notes ,
          Attachment ,
          CreatedBy ,
          CreatedDate ,
          ModifiedBy ,
          ModifiedDate
        )
VALUES  ( 'Test Title' , -- Title - nvarchar(250)
          'test Desc' , -- Description - nvarchar(max)
          'SSE' , -- Designation - nvarchar(50)
          1 , -- Status - int
          1 , -- UserAcceptanceID - int
          GETDATE() , -- DueDate - datetime
          '10' , -- Hours - nvarchar(10)
          '' , -- Notes - nvarchar(500)
          NULL , -- Attachment - nvarchar(200)
          2 , -- CreatedBy - int
          GETDATE() , -- CreatedDate - datetime
          NULL , -- ModifiedBy - int
          NULL  -- ModifiedDate - int
        );
        
INSERT  INTO dbo.tblTask
        ( Title ,
          Description ,
          Designation ,
          Status ,
          UserAcceptanceID ,
          DueDate ,
          Hours ,
          Notes ,
          Attachment ,
          CreatedBy ,
          CreatedDate ,
          ModifiedBy ,
          ModifiedDate
        )
VALUES  ( 'Test Title 123' , -- Title - nvarchar(250)
          'test Desc' , -- Description - nvarchar(max)
          'SSE' , -- Designation - nvarchar(50)
          1 , -- Status - int
          1 , -- UserAcceptanceID - int
          GETDATE() , -- DueDate - datetime
          '10' , -- Hours - nvarchar(10)
          '' , -- Notes - nvarchar(500)
          NULL , -- Attachment - nvarchar(200)
          2 , -- CreatedBy - int
          GETDATE() , -- CreatedDate - datetime
          NULL , -- ModifiedBy - int
          NULL  -- ModifiedDate - int
        );
        
        
INSERT  INTO dbo.tblTask
        ( Title ,
          Description ,
          Designation ,
          Status ,
          UserAcceptanceID ,
          DueDate ,
          Hours ,
          Notes ,
          Attachment ,
          CreatedBy ,
          CreatedDate ,
          ModifiedBy ,
          ModifiedDate
        )
VALUES  ( 'Test Title Admin' , -- Title - nvarchar(250)
          'test Desc' , -- Description - nvarchar(max)
          'Admin' , -- Designation - nvarchar(50)
          1 , -- Status - int
          1 , -- UserAcceptanceID - int
          GETDATE() , -- DueDate - datetime
          '10' , -- Hours - nvarchar(10)
          '' , -- Notes - nvarchar(500)
          NULL , -- Attachment - nvarchar(200)
          2 , -- CreatedBy - int
          GETDATE() , -- CreatedDate - datetime
          NULL , -- ModifiedBy - int
          NULL  -- ModifiedDate - int
        );
        
INSERT  INTO dbo.tblTaskAssignUsers
        ( TaskID, UserID )
VALUES  ( 1, -- TaskID - int
          115  -- UserID - int
          );
              
INSERT  INTO dbo.tblTaskAssignUsers
        ( TaskID, UserID )
VALUES  ( 1, -- TaskID - int
          152  -- UserID - int
          );
      
INSERT  INTO dbo.tblTaskAssignUsers
        ( TaskID, UserID )
VALUES  ( 2, -- TaskID - int
          121  -- UserID - int
          );
          
          Go