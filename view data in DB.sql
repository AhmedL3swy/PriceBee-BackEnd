-- Get all data in the data base
-- our tables 
--1. [dbo].[Category]
select * from [dbo].[Category]
--2. [dbo].[SubCategory]
select * from [dbo].[SubCategory]
--3. [dbo].[Product]
select * from [dbo].[Product]
--4. 


-- Category and SubCategory
SELECT * FROM [dbo].[Category] c
RIGHT JOIN [dbo].[SubCategory] s
ON c.Id = s.CategoryId;

-- Category and SubCategory and Product
--SELECT * FROM [dbo].[Category] c
--RIGHT JOIN [dbo].[SubCategory] s
--ON c.Id = s.CategoryId
--RIGHT JOIN [dbo].[Product] p
--ON s.Id = p.SubCategoryId;

SELECT * FROM [dbo].[Product] p
RIGHT JOIN [dbo].[SubCategory] s
ON p.SubCategoryId = s.Id
RIGHT JOIN [dbo].[Category] c
ON s.CategoryId = c.Id;


