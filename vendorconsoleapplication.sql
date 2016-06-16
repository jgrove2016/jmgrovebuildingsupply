CREATE PROC [dbo].[SaveVendorsMappingByExcel]
@VendorID int=null,
@VendorName nvarchar(500)=null,
@ProductCatName nvarchar(500)=null,
@VendorCatName nvarchar(500)=null,
@VendorSubCatName nvarchar(500)=null,
@IsManufacturerType bit=0
AS
BEGIN
--Declare Variables
DECLARE @ProductTable TABLE (ProductCatID int)
DECLARE @VendorTable TABLE (VendorCatID int)
DECLARE @VendorSubTable TABLE (VendorSubCatID int)
Declare @ProductCatID int=null;
Declare @VendorCatID int=null;
Declare @VendorSubCatID int=null;
Declare @ManufacturerType nvarchar(500)='Retail/Wholesale';

--Set Value of ManufacturerType
IF(@IsManufacturerType=1)
BEGIN
SET @ManufacturerType='Manufacturer';
END

--Set Value Into ProductCatID, VendorCatID and VendorSubCatID
SELECT top 1 @ProductCatID=ProductId FROM tblProductMaster where LOWER(ProductName)=LOWER(@ProductCatName)
SELECT top 1 @VendorCatID=VendorCategpryId FROM tblVendorCategory where LOWER(VendorCategoryNm)=LOWER(@VendorCatName)
SELECT top 1 @VendorSubCatID=VendorSubCategoryId FROM tblVendorSubCategory where LOWER(VendorSubCategoryName)=LOWER(@VendorSubCatName)

--If ProductCatID Doesn't Exists then Create a New One
IF ((@ProductCatID IS NULL) OR (LEN(@ProductCatID) = 0))
BEGIN
INSERT INTO tblProductMaster(ProductName)
OUTPUT Inserted.ProductId INTO @ProductTable
VALUES(@ProductCatName)
SELECT @ProductCatID=ProductCatID FROM @ProductTable
END
PRINT @ProductCatID

--If VendorCatID Doesn't Exists then Create a New One
IF ((@VendorCatID IS NULL) OR (LEN(@VendorCatID) = 0))
BEGIN
INSERT INTO tblVendorCategory(VendorCategoryNm,IsRetail_Wholesale,IsManufacturer)
OUTPUT Inserted.VendorCategpryId INTO @VendorTable
VALUES(@VendorCatName,~@IsManufacturerType,@IsManufacturerType)
SELECT @VendorCatID=VendorCatID FROM @VendorTable
END
PRINT @VendorCatID

--If VendorSubCatID Doesn't Exists then Create a New One IF @VendorSubCatName Exists
IF (((@VendorSubCatID IS NULL) OR (LEN(@VendorSubCatID) = 0)) AND (@VendorSubCatName IS NOT NULL AND (LEN(@VendorSubCatName) >0)))
BEGIN
INSERT INTO tblVendorSubCategory(VendorSubCategoryName,IsRetail_Wholesale,IsManufacturer)
OUTPUT Inserted.VendorSubCategoryId INTO @VendorSubTable
VALUES(@VendorSubCatName,~@IsManufacturerType,@IsManufacturerType)
SELECT @VendorSubCatID=VendorSubCatID FROM @VendorSubTable
END
PRINT @VendorSubCatID

--Check If Value Exist or Need to Map
SELECT * FROM tblProductVendorCat WHERE ProductCategoryId =@ProductCatID AND VendorCategoryId=@VendorCatID
IF (@@ROWCOUNT = 0 )
BEGIN
INSERT INTO tblProductVendorCat(ProductCategoryId,VendorCategoryId) 
VALUES(@ProductCatID,@VendorCatID)
END

--Check If Value Exist or Need to Map
SELECT * FROM tbl_Vendor_VendorCat WHERE VendorId =@VendorID AND VendorCatId=@VendorCatID
IF (@@ROWCOUNT = 0 )
BEGIN
INSERT INTO tbl_Vendor_VendorCat(VendorId,VendorCatId) 
VALUES(@VendorID,@VendorCatID)
END

--IF @VendorSubCatName Exists
IF(@VendorSubCatName IS NOT NULL AND (LEN(@VendorSubCatName) >0))
BEGIN
--Check If Value Exist or Need to Map
SELECT * FROM tbl_Vendor_VendorSubCat WHERE VendorId =@VendorID AND VendorSubCatId=@VendorSubCatID
IF (@@ROWCOUNT = 0 )
BEGIN
INSERT INTO tbl_Vendor_VendorSubCat(VendorId,VendorSubCatId) 
VALUES(@VendorID,@VendorSubCatID)
END
END

--IF @VendorSubCatName Exists
IF(@VendorSubCatName IS NOT NULL AND (LEN(@VendorSubCatName) >0))
BEGIN
--Check If Value Exist or Need to Map
SELECT * FROM tbl_VendorCat_VendorSubCat WHERE VendorCategoryId =@VendorCatID AND VendorSubCategoryId=@VendorSubCatID
IF (@@ROWCOUNT = 0 )
BEGIN
INSERT INTO tbl_VendorCat_VendorSubCat(VendorCategoryId,VendorSubCategoryId) 
VALUES(@VendorCatID,@VendorSubCatID)
END
END

--IF @VendorSubCatName Exists
IF(@VendorSubCatName IS NOT NULL AND (LEN(@VendorSubCatName) >0))
BEGIN
--Update On Vendores Table
UPDATE tblVendors SET VendorCategoryId=@VendorCatID, VendorSubCategoryId=@VendorSubCatID,ManufacturerType=@ManufacturerType
WHERE VendorId=@VendorID
END
ELSE
BEGIN
UPDATE tblVendors SET VendorCategoryId=@VendorCatID,ManufacturerType=@ManufacturerType
WHERE VendorId=@VendorID
END


END