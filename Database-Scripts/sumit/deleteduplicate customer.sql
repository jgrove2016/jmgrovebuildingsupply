use jgrove_JGP




SELECT * FROM new_customer WHERE id NOT IN (SELECT MIN(id) _
	FROM new_customer GROUP BY CustomerName,CellPh, Email)  


	SELECT * FROM new_customer where CustomerName='justin'






select CustomerName, Email, CellPh, COUNT(*) as repeatedCount
from dbo.new_customer
group by CustomerName, Email, CellPh HAVING COUNT(*)>1




WITH CTE AS
(
SELECT *, ROW_NUMBER() OVER (partition BY CustomerName, Email, CellPh ORDER BY CustomerName, Email, CellPh)
AS RowNumber FROM dbo.new_customer  
)
select * from CTE







WITH CTE AS
(
SELECT *, ROW_NUMBER() OVER (partition BY CellPh ORDER BY CellPh)
AS RowNumber FROM dbo.new_customer  
)
delete from CTE where RowNumber <>1





select * from dbo.new_customer where CustomerName='justin'















