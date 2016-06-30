USE [jgrove_JGP]
GO
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Yogesh Keraliya
-- Create date: 06/29/2016
-- Description:	This will search task based on search parameters.
-- =============================================
CREATE PROCEDURE usp_SearchUserTasks
(
@UserID INT = NULL,
@Title  VARCHAR(250) = NULL,
@Designation VARCHAR(50) = NULL,
@Status TINYINT = NULL,
@CreatedOn DATETIME =  NULL,
@Start INT, -- pagenumber
@PageLimit INT -- pagesize
)	
AS
BEGIN
	
SELECT        Tasks.TaskId, Tasks.Title, UsersMaster.Designation, UsersMaster.InstallId, UsersMaster.FristName, Tasks.[Status], Tasks.DueDate
FROM          tblTask AS Tasks INNER JOIN
              tblTaskUser AS TaskUsers ON Tasks.TaskId = TaskUsers.TaskId INNER JOIN
              tblInstallUsers AS UsersMaster ON TaskUsers.UserId = UsersMaster.Id

WHERE

TaskUsers.[UserId] = ISNULL(@UserID , TaskUsers.[UserId]) AND
Tasks.[Title] LIKE '%' + ISNULL(@Title,Tasks.[Title]) + '%' AND
UsersMaster.[Designation] = ISNULL(@Designation , UsersMaster.[Designation]) AND
Tasks.[Status] = ISNULL(@Status,Tasks.[Status]) AND
CONVERT(varchar,Tasks.[CreatedOn],101) =  ISNULL(CONVERT(varchar,@CreatedOn,101), CONVERT(varchar,Tasks.[CreatedOn],101)) 
ORDER BY CONVERT(varchar,Tasks.[DueDate],101)
OFFSET @Start ROW
FETCH NEXT @PageLimit ROWS ONLY

-- get total number of records for virtual count

SELECT        COUNT(Tasks.TaskId) AS VirtualCount
FROM          tblTask AS Tasks INNER JOIN
              tblTaskUser AS TaskUsers ON Tasks.TaskId = TaskUsers.TaskId INNER JOIN
              tblInstallUsers AS UsersMaster ON TaskUsers.UserId = UsersMaster.Id

WHERE

TaskUsers.[UserId] = ISNULL(@UserID , TaskUsers.[UserId]) AND
Tasks.[Title] LIKE '%' + ISNULL(@Title,Tasks.[Title]) + '%' AND
UsersMaster.[Designation] = ISNULL(@Designation , UsersMaster.[Designation]) AND
Tasks.[Status] = ISNULL(@Status,Tasks.[Status]) AND
CONVERT(varchar,Tasks.[CreatedOn],101) =  ISNULL(CONVERT(varchar,@CreatedOn,101), CONVERT(varchar,Tasks.[CreatedOn],101))


END
GO


------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Yogesh Keraliya
-- Create date: 096/30/2016
-- Description:	Fetch all sales and install users for whom tasks are available in system
-- =============================================
CREATE PROCEDURE usp_GetUsersNDesignationForTaskFilter 
AS
BEGIN

SET NOCOUNT ON;

--- get all username for whom tasks are assgined
	SELECT        Users.Id, Users.FristName AS FirstName
	FROM            tblInstallUsers AS Users INNER JOIN
                         tblTaskUser AS Tasks ON Users.Id = Tasks.UserId
WHERE        (Users.UserType = 'SalesUser') OR
                         (Users.UserType = 'sales') OR
                         (Users.UserType = 'installer')
GROUP BY Users.Id, Users.FristName
ORDER BY Users.FristName

-- get all user's designations for whom tasks are assigned.
SELECT        Users.Id, Users.Designation
FROM            tblInstallUsers AS Users INNER JOIN
                         tblTaskUser AS Tasks ON Users.Id = Tasks.UserId
WHERE        (Users.UserType = 'SalesUser') OR
                         (Users.UserType = 'sales') OR
                         (Users.UserType = 'installer')
GROUP BY Users.Id, Users.Designation
ORDER BY Users.Designation


END
GO
