IF NOT EXISTS (SELECT * FROM sysobjects where name = 'Words')
	CREATE TABLE Words 
	(
		Id INT IDENTITY(1, 1) NOT NULL,
		Word NVARCHAR(255) NOT NULL
	);

IF NOT EXISTS (SELECT * FROM sysobjects where name = 'UserLog')
	CREATE TABLE UserLog
	(
		UserIP NVARCHAR(255) NOT NULL,
		WordSearched NVARCHAR(255) NOT NULL,
		SearchDate DATETIME NOT NULL DEFAULT (GETDATE())
	);

IF NOT EXISTS (SELECT * FROM sysobjects where name = 'CachedWords')
	CREATE TABLE CachedWords
		(
			Word VARCHAR(255) NOT NULL,
			Id INT NOT NULL
		);

