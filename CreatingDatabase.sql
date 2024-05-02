DROP DATABASE ProductComparingDB;
Go


-- Create the database
CREATE DATABASE ProductComparingDB;
GO

-- Switch to the newly created database
USE ProductComparingDB;
GO

-- Creating Category table
-- CategoryID: Primary Key, Identity
CREATE TABLE Category (
    Id INT PRIMARY KEY IDENTITY,
    Name_Ar NVARCHAR(50) NOT NULL,
    Name_En VARCHAR(50) NOT NULL
);
GO

-- Creating SubCategory table
-- SubCategoryID: Primary Key, Identity
-- CategoryID: Foreign Key
CREATE TABLE SubCategory (
    Id INT PRIMARY KEY IDENTITY,
    Name_Ar NVARCHAR(50) NOT NULL,
    Name_Eng VARCHAR(50) NOT NULL,
    CategoryId INT NOT NULL,
    -- Foreign Key
    FOREIGN KEY (CategoryId) REFERENCES Category(Id)
);
GO

-- Creating Product table
-- ProductID: Primary Key, Identity
-- SubCategoryID: Foreign Key
CREATE TABLE Product (
    Id INT PRIMARY KEY IDENTITY,
    Name_Ar NVARCHAR(50) NOT NULL,
    Name_Eng VARCHAR(50) NOT NULL,
    Description_Ar NVARCHAR(255) NULL,
    Description_Eng VARCHAR(255) NULL,
    SubCategoryId INT NOT NULL,
    -- Foreign Key
    FOREIGN KEY (SubCategoryId) REFERENCES SubCategory(Id)
);
GO

-- Creating PriceHistory table
-- Prod_Id: Primary Key, Identity, Foreign Key
CREATE TABLE PriceHistory (
    Prod_Id INT,
    Price DECIMAL(18, 2) NOT NULL,
    Date DATETIME NOT NULL,
    -- Foreign Key
    FOREIGN KEY (Prod_Id) REFERENCES Product(Id)
);
GO

-- Creating Domain table
-- DomainID: Primary Key, Identity
CREATE TABLE Domain (
    Id INT PRIMARY KEY IDENTITY,
    Name_Ar NVARCHAR(50) NOT NULL,
    Name_Eng VARCHAR(50) NOT NULL,
    Description_Ar NVARCHAR(255) NULL,
    Description_Eng VARCHAR(255) NULL,
    Url NVARCHAR(255) NOT NULL,
    Logo NVARCHAR(255) NOT NULL
);
GO

-- Creating ProductLinks table
-- Id: Primary Key, Identity
-- Prod_Id: Foreign Key
-- DomainID: Foreign Key
CREATE TABLE ProductLinks (
    Id INT PRIMARY KEY IDENTITY,
    Prod_Id INT NOT NULL,
    DomainId INT NOT NULL,
    ProductLink NVARCHAR(255) NOT NULL UNIQUE,
    Status NVARCHAR(50) NOT NULL,
    -- CHECK (Status IN('Active', 'Inactive', 'Deleted'))
    LastUpdated DATETIME NOT NULL,
    LastScraped DATETIME NOT NULL,
    -- Foreign Key
    FOREIGN KEY (Prod_Id) REFERENCES Product(Id),
    FOREIGN KEY (DomainId) REFERENCES Domain(Id)
);
GO

-- Creating ProductDetails table
-- Id: Primary Key, Identity, Foreign Key
CREATE TABLE ProductDetails (
    Id INT PRIMARY KEY IDENTITY,
    Name_Ar NVARCHAR(50) NOT NULL,
    Name_Eng NVARCHAR(50) NOT NULL,
    Description_Ar NVARCHAR(255) NOT NULL,
    Description_Eng NVARCHAR(255) NOT NULL,
    Price DECIMAL(18, 2) NOT NULL,
    Rating DECIMAL(3, 2) NULL, -- 0.0 to 5.0
    isAvailable BIT NOT NULL,
    Brand NVARCHAR(50) NOT NULL
    -- Foreign Key
    FOREIGN KEY (Id) REFERENCES ProductLinks(Id)
);
GO

-- ALTER TABLE Reviews
-- ADD CONSTRAINT chk_Rating
-- CHECK (Rating >= 0.0 AND Rating <= 5.0);

-- Creating ProductImages table
-- Prod_Id: Primary Key, Identity, Foreign Key
CREATE TABLE ProductImages (
    Prod_Id INT,
    Image NVARCHAR(255) NOT NULL,
    -- Foreign Key
    FOREIGN KEY (Prod_Id) REFERENCES ProductDetails(Id)
);
GO

-- Creating ProductSponsored table
-- Id: Primary Key, Identity
-- Prod_Id: Foreign Key
CREATE TABLE ProductSponsored (
    Id INT PRIMARY KEY IDENTITY,
    Cost DECIMAL(18, 2) NOT NULL,
    StartDate DATETIME NOT NULL,
    Duration INT NOT NULL, -- in days
    Prod_Id INT NOT NULL,
    -- Foreign Key
    FOREIGN KEY (Prod_Id) REFERENCES ProductDetails(Id)
);
GO

-- Creating Users table
-- UserID: Primary Key, Identity
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY,
    FName NVARCHAR(50) NOT NULL,
    LName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(50) NOT NULL,
    Password NVARCHAR(50) NOT NULL, -- Hashed
    Gender NVARCHAR(10) NOT NULL,
-- CHECK (Gender IN('Male', 'Female', 'Unknown'))
    PhoneCode NVARCHAR(10) NOT NULL,
    PhoneNumber NVARCHAR(50) NOT NULL,
    DateOfBirth DATE NOT NULL,
    JoinDate DATE NOT NULL,
    -- LastLogin DATETIME NOT NULL,
    Image NVARCHAR(255) NOT NULL,
    Country NVARCHAR(50) NOT NULL
    -- ,Role NVARCHAR(50) NOT NULL
);
GO

-- Creating SearchValues table
-- UserID: Primary Key, Identity, Foreign Key
CREATE TABLE SearchValues (
    UserID INT,
    SearchValue NVARCHAR(50) NOT NULL,
    -- Foreign Key
    FOREIGN KEY (UserID) REFERENCES Users(Id)
);
GO

-- Creating UserFavProd table
-- UserID: Foreign Key
-- Prod_Id: Foreign Key
CREATE TABLE UserFavProd (
    UserID INT,
    Prod_Id INT NOT NULL
    -- Foreign Key
    FOREIGN KEY (UserID) REFERENCES Users(Id),
    FOREIGN KEY (Prod_Id) REFERENCES Product(Id)
);
GO

-- Creating UserAlertProd table
-- UserID: Foreign Key
-- Prod_Id: Foreign Key
CREATE TABLE UserAlertProd (
    UserID INT,
    Prod_Id INT NOT NULL
    -- Foreign Key
    FOREIGN KEY (UserID) REFERENCES Users(Id),
    FOREIGN KEY (Prod_Id) REFERENCES Product(Id)
);
GO

-- Creating UserHistoryProd table
-- UserID: Foreign Key
-- Prod_Id: Foreign Key
CREATE TABLE UserHistoryProd (
    UserID INT,
    Prod_Id INT NOT NULL
    -- Foreign Key
    FOREIGN KEY (UserID) REFERENCES Users(Id),
    FOREIGN KEY (Prod_Id) REFERENCES Product(Id)
);
GO

