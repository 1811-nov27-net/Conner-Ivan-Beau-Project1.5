DROP TABLE UserDLC;
DROP TABLE UserGame;
DROP TABLE GameTag;
DROP TABLE Tag;
DROP TABLE DLC;
DROP TABLE Game;
DROP TABLE Developer;
DROP TABLE [User];

select * from Developer;

CREATE TABLE [User](
	UserName	NVARCHAR(100),
	FirstName	NVARCHAR(100),
	LastName	NVARCHAR(100),
	Password	NVARCHAR(100) NOT NULL,
	Wallet		MONEY DEFAULT(0),
	CreditCard	NVARCHAR(100),
	Admin		BIT NOT NULL,
	CONSTRAINT  PK_Username PRIMARY KEY (Username)	
);

CREATE TABLE Developer(
	DeveloperId	INT IDENTITY(1,1),
	Name		NVARCHAR(100) NOT NULL,
	FoundingDate	DATETIME2 NOT NULL,
	Website	NVARCHAR(MAX) NOT NULL,
	CONSTRAINT PK_Developer PRIMARY KEY( DeveloperId)
)

CREATE TABLE Game(
	GameId		INT IDENTITY(1,1),
	Name		NVARCHAR(100) NOT NULL,
	Price		MONEY NOT NULL,
	Description	NVARCHAR(max),
	DeveloperId	INT,
	Image		NVARCHAR(max),
	Trailer		NVARCHAR(max),
	CONSTRAINT	PK_Game PRIMARY KEY (GameId),
	CONSTRAINT	FK_GameDeveloper FOREIGN KEY (DeveloperId) REFERENCES Developer(DeveloperId) ON DELETE SET NULL
);

CREATE TABLE DLC(
	DLCId		int identity(1,1),
	Name		NVARCHAR(100) NOT NULL,
	Price		money not null,
	GameId		int not null,
	CONSTRAINT  PK_DLC PRIMARY KEY (DLCId),
	CONSTRAINT  FK_DLCGame FOREIGN KEY (GameId) REFERENCES Game(GameId) ON DELETE CASCADE
);

CREATE TABLE Tag(
	TagId		INT IDENTITY(1,1),
	GenreName	NVARCHAR(100) NOT NULL,
	CONSTRAINT PK_Tag PRIMARY KEY (TagId)
);

CREATE TABLE GameTag(
	GameId		INT,
	TagId		INT,
	CONSTRAINT	PK_GameTag PRIMARY KEY (GameId,TagId),
	CONSTRAINT	FK_GameTagGame FOREIGN KEY (GameId) REFERENCES Game(GameId) ON DELETE CASCADE,
	CONSTRAINT	FK_GameTagTag FOREIGN KEY (TagId) REFERENCES Tag(TagId) ON DELETE CASCADE
);

CREATE TABLE UserGame(
	GameId			int,
	UserName		nvarchar(100) not null,
	DatePurchased	DateTime2 not null,
	Score			int,
	Review			nvarchar(max),
	CONSTRAINT		PK_UserGame PRIMARY KEY (UserName,GameId),
	CONSTRAINT		FK_UserGameUser FOREIGN KEY (UserName) REFERENCES [User](UserName) on delete cascade,
	CONSTRAINT		FK_UserGameGame FOREIGN KEY (GameId) REFERENCES Game(GameId) on delete cascade
);

CREATE TABLE UserDLC(
	UserName	NVARCHAR(100),
	DLCId		int,
	CONSTRAINT PK_UserDLC PRIMARY KEY (UserName,DLCId),
	CONSTRAINT    FK_UserDLCUser FOREIGN KEY (UserName) REFERENCES [User](UserName) on delete cascade,
	CONSTRAINT    FK_UserDLCDLC FOREIGN KEY (DLCId) REFERENCES DLC(DLCId) on delete cascade,
	 
);
