IF EXISTS(SELECT * FROM sys.columns 
            WHERE Name = N'userid' AND Object_ID = Object_ID(N'tbl_VendorNotes'))
BEGIN
   Alter table tbl_VendorNotes drop column [State]
END
alter table tbl_VendorNotes add userid nvarchar(250) null
go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vendorNotes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vendorNotes]
GO
create proc [dbo].[sp_vendorNotes]
@action int=null,
@NotesId int= null,
@VendorId nvarchar(50)=null,
@userid nvarchar(250)=null,
@Notes nvarchar(max)=null,
@TempId nvarchar(250)=null
as
begin
if(@action=1)
begin
declare @CreatedOn datetime=null;
set @CreatedOn=GETDATE();
insert into tbl_VendorNotes(VendorId,userid,Notes,CreatedOn,TempId)
values (@VendorId,@userid,@Notes,@CreatedOn,@TempId)
end
if(@action=2)
begin
if(@TempId<>'')
 begin
	select NotesId,userid,Notes,CreatedOn from tbl_VendorNotes where VendorId=@VendorId and TempId=@TempId
	end
	else
	begin
	select NotesId,userid,Notes,CreatedOn from tbl_VendorNotes where VendorId=@VendorId and VendorId>0
	end

end
end