
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GETInvetoryCatogriesList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GETInvetoryCatogriesList]
GO
CREATE PROC GETInvetoryCatogriesList
as
Begin
	SELECT TPM.ProductId,TPM.ProductName FROM tblProductMaster TPM
LEFT OUTER JOIN tblProductVendorCat TPVC
ON TPM.ProductId=TPVC.ProductCategoryId
where LEN(TPM.ProductName)>0
GROUP BY TPM.ProductId,TPM.ProductName
ORDER BY TPM.ProductName

SELECT TPVC.ProductCategoryId, TVC.VendorCategpryId,TVC.VendorCategoryNm from tblVendorCategory TVC
LEFT OUTER JOIN tbl_VendorCat_VendorSubCat TVCVS
ON TVC.VendorCategpryId=TVCVS.VendorCategoryId
INNER JOIN tblProductVendorCat  TPVC
ON TVC.VendorCategpryId=TPVC.VendorCategoryId
Group By TVC.VendorCategpryId,TVC.VendorCategoryNm,TPVC.ProductCategoryId
ORDER BY TVC.VendorCategoryNm

SELECT TVCVS.VendorCategoryId,TVSC.VendorSubCategoryId,TVSC.VendorSubCategoryName FROM tbl_VendorCat_VendorSubCat TVCVS
LEFT OUTER JOIN tblVendorSubCategory TVSC
ON TVSC.VendorSubCategoryId=TVCVS.VendorSubCategoryId
GROUP BY TVCVS.VendorCategoryId,TVSC.VendorSubCategoryId,TVSC.VendorSubCategoryName
End