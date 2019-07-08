CREATE TABLE Words 
	(
		Id INT IDENTITY(1, 1) NOT NULL,
		Word NVARCHAR(255) NOT NULL
	);

CREATE TABLE UserLog
	(
		UserIP NVARCHAR(255) NOT NULL,
		WordSearched NVARCHAR(255) NOT NULL,
		SearchDate DATETIME NOT NULL DEFAULT (GETDATE())
	);
