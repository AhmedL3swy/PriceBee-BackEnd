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
    Name_Local NVARCHAR(255) NOT NULL,
    Name_Global VARCHAR(255) NOT NULL
);
GO

-- Creating SubCategory table
-- SubCategoryID: Primary Key, Identity
-- CategoryID: Foreign Key
CREATE TABLE SubCategory (
    Id INT PRIMARY KEY IDENTITY,
    Name_Local NVARCHAR(255) NOT NULL,
    Name_Global VARCHAR(255) NOT NULL,
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
    Name_Local NVARCHAR(255) NOT NULL,
    Name_Global VARCHAR(255) NOT NULL,
    Description_Local NVARCHAR(MAX) NULL,
    Description_Global VARCHAR(MAX) NULL,
    SubCategoryId INT NOT NULL,
    -- Foreign Key
    FOREIGN KEY (SubCategoryId) REFERENCES SubCategory(Id)
);
GO

-- Creating PriceHistory table
-- ProdId: Primary Key, Identity, Foreign Key
CREATE TABLE PriceHistory (
    Id INT PRIMARY KEY IDENTITY,
    ProdId INT NOT NULL,
    Price DECIMAL(18, 2) NOT NULL,
    Date DATETIME NOT NULL,
    -- Foreign Key
    FOREIGN KEY (ProdId) REFERENCES Product(Id)
);
GO

-- Creating Domain table
-- DomainID: Primary Key, Identity
CREATE TABLE Domain (
    Id INT PRIMARY KEY IDENTITY,
    Name_Local NVARCHAR(255) NOT NULL,
    Name_Global VARCHAR(255) NOT NULL,
    Description_Local NVARCHAR(MAX) NULL,
    Description_Global VARCHAR(MAX) NULL,
    Url NVARCHAR(MAX) NOT NULL,
    Logo NVARCHAR(MAX) NOT NULL
);
GO

-- Creating ProductLinks table
-- Id: Primary Key, Identity
-- ProdId: Foreign Key
-- DomainID: Foreign Key
CREATE TABLE ProductLinks (
    Id INT PRIMARY KEY IDENTITY,
    ProdId INT NOT NULL,
    DomainId INT NOT NULL,
    ProductLink NVARCHAR(MAX) NOT NULL,
    Status NVARCHAR(255) NOT NULL,
    -- CHECK (Status IN('Active', 'Inactive', 'Deleted'))
    LastUpdated DATETIME NOT NULL,
    LastScraped DATETIME NOT NULL,
    -- Foreign Key
    FOREIGN KEY (ProdId) REFERENCES Product(Id),
    FOREIGN KEY (DomainId) REFERENCES Domain(Id)
);
GO

-- Creating ProductDetails table
-- Id: Primary Key, Identity, Foreign Key
CREATE TABLE ProductDetails (
    Id INT PRIMARY KEY IDENTITY,
    Name_Local NVARCHAR(255) NOT NULL,
    Name_Global NVARCHAR(255) NOT NULL,
    Description_Local NVARCHAR(MAX) NOT NULL,
    Description_Global NVARCHAR(MAX) NOT NULL,
    Price DECIMAL(18, 2) NOT NULL,
    Rating DECIMAL(3, 2) NULL, -- 0.0 to 5.0
    isAvailable BIT NOT NULL,
    Brand NVARCHAR(255) NULL
    -- Foreign Key
    FOREIGN KEY (Id) REFERENCES ProductLinks(Id)
);
GO

-- ALTER TABLE Reviews
-- ADD CONSTRAINT chk_Rating
-- CHECK (Rating >= 0.0 AND Rating <= 5.0);

-- Creating ProductImages table
-- ProdId: Primary Key, Identity, Foreign Key
CREATE TABLE ProductImages (
    Id INT PRIMARY KEY IDENTITY,
    ProdId INT NOT NULL,
    Image NVARCHAR(MAX) NOT NULL,
    -- Foreign Key
    FOREIGN KEY (ProdId) REFERENCES ProductDetails(Id)
);
GO

-- Creating ProductSponsored table
-- Id: Primary Key, Identity
-- ProdId: Foreign Key
CREATE TABLE ProductSponsored (
    Id INT PRIMARY KEY IDENTITY,
    Cost DECIMAL(18, 2) NOT NULL, -- Priority
    StartDate DATETIME NOT NULL,
    Duration INT NOT NULL, -- in days
    ProdId INT NOT NULL,
    -- Foreign Key
    FOREIGN KEY (ProdId) REFERENCES ProductDetails(Id)
);
GO

-- Creating Users table
-- UserID: Primary Key, Identity
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY,
    FName NVARCHAR(255) NOT NULL,
    LName NVARCHAR(255) NOT NULL,
    -- UserName NVARCHAR(255) NOT NULL UNIQUE,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL, -- Hashed
    Gender NVARCHAR(10) NOT NULL,
    -- CHECK (Gender IN('Male', 'Female', 'Unknown'))
    Country NVARCHAR(255) NOT NULL,
    -- CHECK (Country IN('Egypt', Saudi Arabia'))
    JoinDate DATE NOT NULL,
    PhoneCode NVARCHAR(255) NULL,
    -- CHECK (list of phone codes)
    PhoneNumber NVARCHAR(255) NULL,
    DateOfBirth DATE NULL,
    -- LastLogin DATETIME NOT NULL,
    Image NVARCHAR(MAX) NULL,
    Role NVARCHAR(255) NULL
    -- CHECK (Role IN('Admin', 'User'))

);
GO

-- Creating SearchValues table
-- UserID: Primary Key, Identity, Foreign Key
CREATE TABLE SearchValues (
    Id INT PRIMARY KEY IDENTITY,
    UserID INT NOT NULL,
    SearchValue NVARCHAR(255) NULL,
    -- Foreign Key
    FOREIGN KEY (UserID) REFERENCES Users(Id),
);
GO

-- Creating UserFavProd table
-- UserID: Foreign Key
-- ProdId: Foreign Key
CREATE TABLE UserFavProd (
    UserID INT NOT NULL,
    ProdId INT NOT NULL
    -- Foreign Key
    FOREIGN KEY (UserID) REFERENCES Users(Id),
    FOREIGN KEY (ProdId) REFERENCES Product(Id),
    PRIMARY KEY (UserID, ProdId)
);
GO

-- Creating UserAlertProd table
-- UserID: Foreign Key
-- ProdId: Foreign Key
CREATE TABLE UserAlertProd (
    UserID INT NOT NULL,
    ProdId INT NOT NULL
    -- Foreign Key
    FOREIGN KEY (UserID) REFERENCES Users(Id),
    FOREIGN KEY (ProdId) REFERENCES Product(Id),
    PRIMARY KEY (UserID, ProdId)
);
GO

-- Creating UserHistoryProd table
-- UserID: Foreign Key
-- ProdId: Foreign Key
CREATE TABLE UserHistoryProd (
    UserID INT NOT NULL,
    ProdId INT NOT NULL
    -- Foreign Key
    FOREIGN KEY (UserID) REFERENCES Users(Id),
    FOREIGN KEY (ProdId) REFERENCES Product(Id),
    PRIMARY KEY (UserID, ProdId)
);
GO

