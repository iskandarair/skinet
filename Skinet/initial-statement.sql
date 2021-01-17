CREATE TABLE Products (
    Id int IDENTITY(1,1)Primary Key,
    Name varchar(255)
);
--DROP TABLE Products

INSERT INTO Products(Name) VALUES ('Product one')
INSERT INTO Products(Name) VALUES ('Product two')
INSERT INTO Products(Name) VALUES ('Product three')