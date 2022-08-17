CREATE DATABASE BCS
GO

USE BCS
GO

CREATE TABLE FavoriteCharacters
(
     ID int primary key identity,
     FirstName nvarchar(50),
     LastName nvarchar(50),
)
GO

INSERT INTO FavoriteCharacters VALUES ('Jimmy', 'McGill')
INSERT INTO FavoriteCharacters VALUES ('Saul', 'Goodman')
INSERT INTO FavoriteCharacters VALUES ('Gene', 'Takovic')
GO

CREATE TABLE FavoriteEpisodes
(
     ID int primary key identity,
     Season int,
     NumberWithInSeason int,
     Title nvarchar(50)
)
GO

INSERT INTO FavoriteEpisodes VALUES (01, 01, 'Uno')
INSERT INTO FavoriteEpisodes VALUES (06, 13, 'Saul Gone')
GO