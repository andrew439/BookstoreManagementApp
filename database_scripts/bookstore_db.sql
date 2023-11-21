/* check whether the databse exists; if so, drop it */

IF EXISTS (SELECT 1 FROM master.dbo.sysdatabases
				WHERE name = 'bookstore_db')
BEGIN	
	DROP DATABASE bookstore_db
	print '' print '*** dropping database bookstore_db'
END
GO


print '' print '*** creating database bookstore_db'
GO
CREATE DATABASE bookstore_db
GO


print '' print '*** using bookstore_db'
GO
USE [bookstore_db]
GO

/* Employee table */
print '' print '*** creating employee table'
GO
CREATE TABLE [dbo].[Employee]
(
	[EmployeeID]	[int] IDENTITY(100000,1)	NOT NULL,
	[GivenName]		[nvarchar](50)				NOT NULL,
	[FamilyName]	[nvarchar](100)				NOT NULL,
	[Phone]			[nvarchar](13)				NOT NULL,
	[Email]			[nvarchar](100)				NOT NULL,
	[PasswordHash]	[nvarchar](100)				NOT NULL DEFAULT
		'9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
	[ClockedIn]		[bit]						NOT NULL DEFAULT 0,
	[Active]		[bit]						NOT NULL DEFAULT 1,
	CONSTRAINT [pk_EmployeeID] PRIMARY KEY ([EmployeeID]),
	CONSTRAINT [fk_Email] UNIQUE ([Email])
)
GO

print '' print '*** Adding Index for FamilyName on Employee Table'
GO
CREATE NONCLUSTERED INDEX [ix_lastName] ON [Employee]([FamilyName] ASC)
GO

/* Employee test records */
print '' print '*** creating employee test records'
GO
INSERT INTO [dbo].[Employee]
		([GivenName], [FamilyName], [Phone], [Email])
	VALUES
		('System', 'Admin', '13191230000','admin@boxesofbooks.com'),
		('Sarah', 'Williams', '13191231111','sarah@boxesofbooks.com'),
		('Glen', 'Williams', '13191232222','glen@boxesofbooks.com'),
		('Dan', 'Morris', '13191233333','dan@boxesofbooks.com'),
		('Joanne', 'Smith', '13195551111', 'joanne@boxesofbooks.com'),
		('Martin', 'Taylor', '13195552222', 'martin@boxesofbooks.com'),
		('Betty', 'Linn', '13195553333', 'betty@boxesofbooks.com'),
		('Sonia', 'Hernandez', '13195554444', 'sonia@boxesofbooks.com'),
		('Christopher', 'Rawi', '13195555555', 'chris@boxesofbooks.com'),
		('Louis', 'Bloom', '13196676676', 'louis@boxesofbooks.com')
GO


print '' print '*** creating roles table'
GO
CREATE TABLE [dbo].[Role]
(
	[RoleID]		[nvarchar](50)			NOT NULL,
	[Description]	[nvarchar](250)			NULL,
	CONSTRAINT [pk_RoleID] PRIMARY KEY ([RoleID])
)
GO


print '' print '*** inserting sample role records'
GO
INSERT INTO [dbo].[Role]
		([RoleID], [Description])
	VALUES
		('Administrator', 'administers user accounts and assigns roles'),
		('Manager', 'manages the inventory of books, customer records, and personnel'),
		('Cashier', 'rings up purchases and handles financial transactions'),
		('StockClerk', 'stocks shelves, misc. cleaning tasks'),
		('Purchaser', 'analyzes used books and purchases them from customers'),
		('Maintenance', 'performs maintenance and repairs on the building'),
		('Customer', 'Customers of the store with accounts for shopping')
GO


CREATE TABLE [dbo].[EmployeeRole]
(
	[EmployeeID]	[int]				NOT NULL,
	[RoleID]		[nvarchar](50)		NOT NULL,
	CONSTRAINT [fk_Employee_EmployeeID]	FOREIGN KEY ([EmployeeID])
		REFERENCES [dbo].[Employee]([EmployeeID]),
	CONSTRAINT [fk_EmployeeRole_RoleID]	FOREIGN KEY ([RoleID])
		REFERENCES [dbo].[Role]([RoleID]),
	CONSTRAINT [pk_EmployeeRole] PRIMARY KEY ([EmployeeID], [RoleID])
)
GO


print '' print '*** inserting sample EmployeeRole records'
GO
INSERT INTO [dbo].[EmployeeRole]
		([EmployeeID], [RoleID])
	VALUES
		(100000, 'Administrator'),
		(100001, 'Administrator'),
		(100001, 'Manager'),
		(100002, 'Administrator'),
		(100002, 'Manager'),
		(100003, 'Manager'),
		(100004, 'Cashier'),
		(100005, 'Cashier'),
		(100006, 'Customer'),
		(100007, 'Cashier'),
		(100007, 'StockClerk'),
		(100007, 'Purchaser'),
		(100008, 'StockClerk'),
		(100009, 'Maintenance')
GO

print '' print '*** Creating Sample Deactivated Employee'
GO
INSERT INTO [dbo].[Employee]
	([GivenName], [FamilyName], [Phone], [Email], [Active])
	VALUES
	('Boris', 'Badworker', '13191239999','boris@company.com', 0)
GO


print '' print '*** creating ZIPCode table'
GO
CREATE TABLE [dbo].[ZIPCode]
(
	[ZIP]			[nvarchar](10),
	[City]			[nvarchar](50),
	[State]			[char](2),
	CONSTRAINT [pk_ZIP] PRIMARY KEY ([ZIP])
)


print '' print '*** inserting sample ZIPCode records'
GO
INSERT INTO [dbo].[ZIPCode]
		([ZIP], [City], [State])
	VALUES
		('52401', 'Cedar Rapids', 'IA'),
		('52402', 'Cedar Rapids', 'IA'),
		('52403', 'Cedar Rapids', 'IA'),
		('52404', 'Cedar Rapids', 'IA'),
		('52405', 'Cedar Rapids', 'IA'),
		('52302', 'Marion', 'IA'),
		('52233', 'Hiawatha', 'IA'),
		('52241', 'Coralville', 'IA'),
		('52240', 'Iowa City', 'IA')
GO


print '' print '*** creating Address table'
GO
CREATE TABLE [dbo].[Address]
(
	[AddressID]		[int] IDENTITY(100000,1)	NOT NULL,
	[AddressLine1]	[nvarchar](250)				NOT NULL,
	[AddressLine2]	[nvarchar](250)				NULL,
	[ZIP]			[nvarchar](10)				NOT NULL,
	CONSTRAINT [fk_Address_ZIP]	FOREIGN KEY ([ZIP])
		REFERENCES [dbo].[ZIPCode]([ZIP]),
	CONSTRAINT [pk_AddressID] PRIMARY KEY ([AddressID])
)
GO


print '' print '*** inserting sample Address records'
GO
INSERT INTO [dbo].[Address]
		([AddressLine1], [AddressLine2], [ZIP])
	VALUES
		('532 5th Ave NE', NULL, '52401'),
		('9654 Otis Rd. SE', NULL, '52402'),
		('8004 Willow Ct. NW', NULL, '52405'),
		('244 Clark St.', 'Apt. 6', '52233'),
		('3030 Tower Terrace Rd.', NULL, '52302'),
		('111 South St.', 'Unit 2B', '52240'),
		('773 3rd St. SE', NULL, '52402')
GO



print '' print '*** creating EmployeeAddress table'
GO

CREATE TABLE [dbo].[EmployeeAddress]
(
	[EmployeeID]	[int]				NOT NULL,
	[AddressID]		[int]				NOT NULL,
	[StartDate]		[datetime]			NOT NULL,
	[EndDate]		[datetime]			NULL,
	CONSTRAINT [fk_EmployeeAddress_EmployeeID] FOREIGN KEY ([EmployeeID])
		REFERENCES [dbo].[Employee]([EmployeeID]),
	CONSTRAINT [fk_EmployeeAddress_AddressID] FOREIGN KEY ([AddressID])
		REFERENCES [dbo].[Address]([AddressID]),
	CONSTRAINT [pk_EmployeeAddress] PRIMARY KEY ([EmployeeID], [AddressID])
)
GO


print '' print '*** inserting sample EmployeeAddress records'
GO
INSERT INTO [dbo].[EmployeeAddress]
		([EmployeeID], [AddressID], [StartDate], [EndDate])
	VALUES
		(100000, 100004, '2003-01-01', NULL),
		(100001, 100002, '2006-06-01', NULL),
		(100002, 100003, '2017-03-01', NULL),
		(100003, 100000, '2002-06-01', NULL),
		(100004, 100001, '2021-09-01', NULL),
		(100005, 100004, '1999-10-01', '2002-01-01'),
		(100001, 100005, '2022-01-01', NULL)
GO


/* login-related stored procedures */

print '' print '*** creating sp_insert_employee'
GO
CREATE PROCEDURE [sp_insert_employee]
(
	@GivenName		[nvarchar](50),
	@FamilyName		[nvarchar](50),
	@Phone		 	[nvarchar](11),
	@Email 			[nvarchar](250)
)
AS
BEGIN
	INSERT INTO [dbo].[Employee]
		([GivenName], [FamilyName], [Phone], [Email])
	VALUES
		(@GivenName, @FamilyName, @Phone, @Email)
	SELECT SCOPE_IDENTITY()
END
GO

print '' print '*** creating sp_authenticate_user'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user]
(
	@Email				[nvarchar](100),
	@PasswordHash		[nvarchar](100)
)
AS
	BEGIN
		SELECT COUNT([EmployeeID]) AS 'Authenticated'
		FROM	[Employee]
		WHERE	@Email = [Email]
		  AND	@PasswordHash = [PasswordHash]
		  AND	[Active] = 1
	END
GO


print '' print '*** creating sp_select_employee_by_email'
GO
CREATE PROCEDURE [sp_select_employee_by_email]
(
	@Email				[nvarchar](100)
)
AS
	BEGIN
		SELECT	[EmployeeID], [GivenName], [FamilyName],
				[Phone], [ClockedIn], [Active]
		FROM	[Employee]
		WHERE	@Email = [Email]
	END
GO


print '' print '*** creating sp_select_roles_by_employeeID'
GO
CREATE PROCEDURE [dbo].[sp_select_roles_by_employeeID]
(
	@EmployeeID				[int]
)
AS
	BEGIN
		SELECT	[RoleID]
		FROM	[EmployeeRole]
		WHERE	@EmployeeID = [EmployeeID]
	END
GO

print '' print '*** Creating sp_insert_employee_role'
GO
CREATE PROCEDURE [sp_insert_employee_role]
(
	@EmployeeID			[int],
	@RoleID				[nvarchar](50)
)
AS
BEGIN
INSERT INTO [dbo].[EmployeeRole]
	([EmployeeID], [RoleID])
	VALUES
	(@EmployeeID, @RoleID)
END
GO

print '' print '*** Creating sp_delete_employee_role'
GO
CREATE PROCEDURE [sp_delete_employee_role]
(
	@EmployeeID			[int],
	@RoleID				[nvarchar](50)
)
AS
BEGIN
	DELETE FROM [dbo].[EmployeeRole]
	WHERE [EmployeeID] =	@EmployeeID
	  AND [RoleID] = 		@RoleID
END
GO

print '' print '*** Creating sp_select_all_roles'
GO
CREATE PROCEDURE [sp_select_all_roles]
AS
BEGIN
	SELECT [RoleID]
	FROM [dbo].[Role]
	ORDER BY [RoleID]
END
GO


print '' print '*** creating sp_update_passwordHash'
GO
CREATE PROCEDURE [dbo].[sp_update_passwordHash]
(
	@EmployeeID				[int],
	@PasswordHash			[nvarchar](100),
	@OldPasswordHash		[nvarchar](100)
)
AS
	BEGIN
		UPDATE	[Employee]
		SET		[PasswordHash] = @PasswordHash
		WHERE	@EmployeeID = [EmployeeID]
		AND		@OldPasswordHash = [PasswordHash]
		
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_update_password'
GO
CREATE PROCEDURE [sp_update_password]
(
	@EmployeeID			[int],
	@OldPasswordHash	[nvarchar](100),
	@NewPasswordHash	[nvarchar](100)
)
AS
BEGIN
	UPDATE 	[dbo].[Employee]
	SET 	[PasswordHash] 	= @NewPasswordHash
	WHERE	[EmployeeID] 	= @EmployeeID
	  AND	[PasswordHash] 	= @OldPasswordHash
	  AND	[Active] = 1
	RETURN @@ROWCOUNT
END
GO

print '' print '*** Creating sp_select_users_by_active'
GO
CREATE PROCEDURE [sp_select_users_by_active]
(
	@Active			[bit]
)
AS
BEGIN
	SELECT 		[EmployeeID],[GivenName],[FamilyName],[Phone],[Email],[Active]
	FROM 		[dbo].[Employee]
	WHERE 		[Active] = @Active
	ORDER BY 	[FamilyName]
END
GO




/* Employee stored procedures*/

print '' print '*** creating sp_select_all_active_employees'
GO
CREATE PROCEDURE [dbo].[sp_select_all_active_employees]
AS
	BEGIN
		SELECT	[Employee].[EmployeeID], [GivenName], [FamilyName], [Phone], [Email], [ClockedIn],
				[Active], [AddressLine1], [AddressLine2], [City], [State], [Address].[ZIP]
		FROM 	[dbo].[Employee]
		JOIN	[EmployeeAddress]
			ON	[Employee].[EmployeeID] = [EmployeeAddress].[EmployeeID]
		JOIN	[Address]
			ON	[EmployeeAddress].[AddressID] = [Address].[AddressID]
		JOIN	[ZIPCode]
			ON	[Address].[ZIP] = [ZIPCode].[ZIP]
		WHERE	[Employee].[Active] = 1
	END
GO

print '' print '*** Creating sp_deactivate_employee'
GO
CREATE PROCEDURE [sp_deactivate_employee]
(
	@EmployeeID			[int]
)
AS
BEGIN
	UPDATE [dbo].[Employee]
		SET [Active] = 		0
	WHERE 	[EmployeeID] =	@EmployeeID  
	RETURN @@ROWCOUNT
END
GO

print '' print '*** Creating sp_reactivate_employee'
GO
CREATE PROCEDURE [sp_reactivate_employee]
(
	@EmployeeID			[int]
)
AS
BEGIN
	UPDATE [dbo].[Employee]
		SET [Active] = 		1
	WHERE 	[EmployeeID] =	@EmployeeID  
	RETURN @@ROWCOUNT
END
GO

print '' print '*** creating sp_select_employee_by_employeeID'
GO
CREATE PROCEDURE [dbo].[sp_select_employee_by_employeeID]
(
	@EmployeeID				[int]
)
AS
	BEGIN
		SELECT	[Employee].[EmployeeID], [GivenName], [FamilyName], [Phone], [Email], [ClockedIn],
				[Active], [AddressLine1], [AddressLine2], [City], [State], [Address].[ZIP]
		FROM 	[dbo].[Employee]
		JOIN	[EmployeeAddress]
			ON	[Employee].[EmployeeID] = [EmployeeAddress].[EmployeeID]
		JOIN	[Address]
			ON	[EmployeeAddress].[AddressID] = [Address].[AddressID]
		JOIN	[ZIPCode]
			ON	[Address].[ZIP] = [ZIPCode].[ZIP]
		WHERE	[Employee].[EmployeeID] = @EmployeeID
	END
GO

print '' print '*** Creating sp_update_employee'
GO
CREATE PROCEDURE [sp_update_employee]
(
	@EmployeeID			[int],

	@NewGivenName		[nvarchar](50),
	@NewFamilyName		[nvarchar](50),
	@NewPhone		 	[nvarchar](11),
	@NewEmail 			[nvarchar](250),
	
	@OldGivenName		[nvarchar](50),
	@OldFamilyName		[nvarchar](50),
	@OldPhone		 	[nvarchar](11),
	@OldEmail 			[nvarchar](250)
)
AS
BEGIN
	UPDATE [dbo].[Employee]
		SET [GivenName]  = 	@NewGivenName,
			[FamilyName] = 	@NewFamilyName,
			[Phone]		 =  @NewPhone,
			[Email] 	 =  @NewEmail
	WHERE 	[EmployeeID] =	@EmployeeID  
	  AND	[GivenName]  = 	@OldGivenName
	  AND	[FamilyName] = 	@OldFamilyName
	  AND	[Phone]		 =  @OldPhone
	  AND	[Email] 	 = 	@OldEmail
	 
	RETURN @@ROWCOUNT
END
GO

/* Book tables and stored procedures */

/* BookCategory table */
print '' print '** creating BookCategory table'
GO
CREATE TABLE [dbo].[BookCategory]
(
	[BookCategoryID]		[nvarchar](25)			NOT NULL,
	[Description]			[nvarchar](1000)		NOT NULL,
	CONSTRAINT [pk_BookCategoryID] PRIMARY KEY ([BookCategoryID])
)
GO


print '' print '*** inserting sample BookCategory records'
GO
INSERT INTO [dbo].[BookCategory]
		([BookCategoryID], [Description])
	VALUES
		('Fiction', 'Literary works comprised of narratives that are not factual but are, instead, products of the imaginations of the authors.'),
		('Nonfiction', 'Literary works composed of prose writing which is based on facts. Also, includes poetry.')
GO


/* BookCondition table */
print '' print '** creating BookCondition table'
GO
CREATE TABLE [dbo].[BookCondition]
(
	[BookConditionID]		[nvarchar](25)			NOT NULL,
	[Description]			[nvarchar](1000)		NOT NULL,
	CONSTRAINT [pk_BookConditionID] PRIMARY KEY ([BookConditionID])
)
GO


print '' print '*** inserting sample BookCondition records'
GO
INSERT INTO [dbo].[BookCondition]
		([BookConditionID], [Description])
	VALUES
		('New', 'Brand new, never previously owned'),
		('Like New', 'A book that is in the same perfect condition as when it was published.'),
		('Very Good', 'A book that shows some small signs of wear - but no tears - on either binding or paper.'),
		('Good', 'The average used worn book that has all pages or leaves present.'),
		('Fair', 'A worn book that has complete text pages but may lack endpapers, half-title, etc.'),
		('Poor', 'A book that is sufficiently worn, scuffed, stained, or spotted'),
		('Not for sale', 'A book not for sale that has been set aside for repair or some other reason.')
GO


/* BookGenre table */
print '' print '** creating BookGenre table'
GO
CREATE TABLE [dbo].[BookGenre]
(
	[BookGenreID]			[nvarchar](25)			NOT NULL,
	[Description]			[nvarchar](1000)		NOT NULL,
	CONSTRAINT [pk_BookGenreID] PRIMARY KEY ([BookGenreID])
)
GO


print '' print '*** inserting sample BookGenre records'
GO
INSERT INTO [dbo].[BookGenre]
		([BookGenreID], [Description])
	VALUES
		('Fantasy', 'Fictional works characterized by elements of magic or the supernatural and often inspired by mythology or folklore.'),
		('Horror', 'Fictional works that create a sense of dread or doom for the reader. Gothic, gore, and psychological are subgenres.'),
		('Romance', 'Fictional works that place primary focus on the relationship and romantic love between people, and usually have an emotionally satisfying and optimistic ending.'),
		('Sci-fi', 'Fictional works distinguished by a preoccupation with real or real-feeling science. Stories about time travel and space exploration.'),
		('Literary', 'Fictional works that are thought to have considerable artistic value. Provide personal or social commentary on a “serious” theme. Classics.'),
		('Financial', 'Nonfictional works that give financial advice or explain financial concepts.'),
		('Autobiography', 'Nonfictional works about a persons life and experiences, written by the person themselves.'),
		('Biography', 'Nonfictional work that gives an account of a persons life written, composed, or produced by another.'),
		('Self Help', 'Nonfictional works which encourage personal improvement and confidence.'),
		('Textbook', 'Nonfictional works used as required reading for classwork or other topic studying.'),
		('History', 'Nonfictional works that relate known facts about a historical era, event, or figure.'),
		('Topical', 'Nonfictional works that investigate and explain a given topic.')
		/*
		  ('YA', 'Fictional works written for readers from 12 to 18 years of age.'),
		  ('Children', 'Fictional or nonfictional works and accompanying illustrations produced in order to entertain or instruct adolescent children.'),
		  ('Western', 'Fictional works  set in the American Old West frontier and typically set from the late eighteenth to the late nineteenth century.'),
		  ('Mystery', 'Fictional works in which the reader is challenged to solve a puzzle before the detective explains it at the end'),
		  ('Thriller', 'Fictional works characterized by the moods they elicit, giving readers heightened feelings of suspense, excitement, surprise, anticipation, and anxiety.'),
			
		*/
GO


/* Book table */
print '' print '*** creating Book table'
GO
CREATE TABLE [dbo].[Book]
(
	[ISBN]				[nvarchar](50)			NOT NULL,
	[Title]				[nvarchar](100)			NOT NULL,
	[BookCategoryID]	[nvarchar](25)			NOT NULL,
	[BookConditionID]	[nvarchar](25)			NOT NULL,
	[BookGenreID]		[nvarchar](25)			NOT NULL,
	[Publisher]			[nvarchar](150)			NOT NULL,
	[PublicationDate]	[datetime]				NOT NULL,
	[WholesalePrice]	[decimal](6,2)			NOT NULL,
	[SalePrice]			[decimal](6,2)			NOT NULL,
	[Quantity]			[int]					NOT NULL,
	[QuantityByTitle]	[int]					NOT NULL,
	[LocationID]		[nvarchar](50)			NOT NULL,
	[Active]			[bit] 					NOT NULL DEFAULT 1,
	CONSTRAINT [pk_ISBN] PRIMARY KEY ([ISBN]),
	CONSTRAINT [fk_BookCategoryID] FOREIGN KEY ([BookCategoryID])
		REFERENCES [dbo].[BookCategory]([BookCategoryID]) 
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	CONSTRAINT [fk_BookConditionID] FOREIGN KEY ([BookConditionID])
		REFERENCES [dbo].[BookCondition]([BookConditionID])
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	CONSTRAINT [fk_BookGenreID] FOREIGN KEY ([BookGenreID])
		REFERENCES [dbo].[BookGenre]([BookGenreID])
		ON DELETE CASCADE
		ON UPDATE CASCADE
)	
GO


print '' print '** inserting data into Book table'
GO
INSERT INTO [dbo].[Book]
(
	[ISBN], [Title], [BookCategoryID], [BookConditionID], [BookGenreID],[Publisher], [PublicationDate],
	[WholesalePrice], [SalePrice], [Quantity], [QuantityByTitle], [LocationID], [Active]
)
	VALUES
		('0-812-51685-0', 'Flux', 'Fiction', 'Fair', 'Sci-fi', 'Tom Doherty Associates, Inc.', '1992-01-01', 5.99, 2.99, 1, 1, 'FL08A', 0),
		('0-415-92715-3', 'From The Brink of the Apocalypse', 'Nonfiction', 'Good', 'History', 'Routledge', '2001-01-01', 3.99, 5.99, 1, 1, 'BR05Z', 1),
		('0-671-70863-5', 'The 7 Habits of Highly Effective People', 'Nonfiction', 'Like New', 'Self Help', 'Simon & Schuster Inc.', '1990-01-01', 4.99, 10.99, 1, 2, 'FL09C', 1),
		('978-1-61016-133-6', 'Financial Management', 'Nonfiction', 'Good', 'Financial', 'Mises Institute', '2016-01-01', 0.99, 2.99, 1, 1, 'FL09D', 1),
		('0-345-33971-1', 'The Two Towers', 'Fiction', 'Good', 'Fantasy', 'Ballantine Books', '1994-01-01', 3.99, 10.99, 2, 3, 'FL08D', 1),
		('0-670-85869-2', 'Rose Madder', 'Fiction', 'Like New', 'Horror', 'Penguin Group', '1995-01-01', 8.99, 15.99, 1, 1, 'FL00A', 0),
		('978-0-87140-426-8', 'The Death of Ivan Ilyich & Confession', 'Fiction', 'Poor', 'Literary', 'Liveright Publishing Company', '2014-01-01', 8.99, 2.99, 1, 1, 'BR04C', 1),
		('978-0-06-172903-4', 'Writing Places', 'Nonfiction', 'Like New', 'Autobiography', 'HarperCollins Publishers', '2009-01-01', 8.99, 15.99, 1, 1, 'FL07A', 1),
		('0-07-222460-6', 'UML: A Beginners Guide', 'Nonfiction', 'Very Good', 'Textbook', 'McGraw-Hill', '2003-01-01', 8.99, 19.49, 1, 1, 'BR05Z', 1),
		('0-670-69199-2', 'The Talisman', 'Fiction', 'Good', 'Horror', 'Viking Penguin Inc.', '1984-01-01', 7.99, 14.99, 1, 2, 'FL00A', 1),
		('0-679-72202-5', 'A Bend in the River', 'Fiction', 'New', 'Literary', 'Random House, Inc.', '1979-05-01', 5.99, 17.99, 1, 1, 'FL00A', 1),
		('978-1-4391-9874-2', 'Gifts of the Crow', 'Nonfiction', 'New', 'Topical', 'Simon & Schuster, Inc.', '2013-01-01', 8.99, 18.00, 10, 10, 'FL09C', 1),
		('978-1-335-44930-6', 'The Daughter of Auschwitz', 'Nonfiction', 'New', 'Autobiography', 'Hanover Square Press', '2022-01-01', 8.99, 20.00, 8, 8, 'FL09C', 1),
		('978-0385029988', 'R Is For Rocket', 'Fiction', 'Fair', 'Sci-fi', 'Doubleday & Company, Inc.', '1962-01-01', 5.99, 6.99, 1, 1, 'FL00A', 1),
		('978-0-385-33348-1', "Cat's Cradle", 'Fiction', 'Like New', 'Literary', 'Oragami Express, LLC', '2010-01-01', 5.99, 18.00, 1, 2, 'FL00A', 1),
		('0-14-01-7738-8', 'Cannery Row', 'Fiction', 'Good', 'Literary', 'Penguin Books', '1992-01-01', 1.99, 5.95, 1, 1, 'FL00A', 1),
		('0-449-91121-7', 'In the Beauty of the Lilies', 'Fiction', 'New', 'Literary', 'Ballantine Books', '1996-01-01', 4.99, 15.99, 5, 1, 'FL00A', 1),
		('0-446-60895-5', 'A Walk To Remember', 'Fiction', 'Good', 'Romance', 'Warner Books', '1999-01-01', 2.99, 7.50, 1, 1, 'BR05Z', 1)
GO


/* Book stored procedures */

print '' print '*** creating sp_select_all_books'
GO
CREATE PROCEDURE [dbo].[sp_select_all_books]
AS
	BEGIN
		SELECT	[ISBN], [Title], [BookCategoryID], [BookConditionID], [BookGenreID],[Publisher], [PublicationDate],
				[WholesalePrice], [SalePrice], [Quantity], [QuantityByTitle], [LocationID], [Active]
		FROM 	[dbo].[Book]
		WHERE 	[Active] = 1
	END
GO


print '' print '*** creating sp_select_all_book_categories'
GO
CREATE PROCEDURE [dbo].[sp_select_all_book_categories]
AS
	BEGIN
		SELECT	[BookCategoryID], [Description]
		FROM 	[dbo].[BookCategory]
	END
GO


print '' print '*** creating sp_select_all_book_conditions'
GO
CREATE PROCEDURE [dbo].[sp_select_all_book_conditions]
AS
	BEGIN
		SELECT [BookConditionID], [Description]
		FROM 	[dbo].[BookCondition]
	END
GO


print '' print '*** creating sp_select_all_book_genres'
GO
CREATE PROCEDURE [dbo].[sp_select_all_book_genres]
AS
	BEGIN
		SELECT	[BookGenreID], [Description]
		FROM 	[dbo].[BookGenre]
	END
GO


print '' print '*** creating sp_select_books_by_bookCategoryID'
GO
CREATE PROCEDURE [dbo].[sp_select_books_by_bookCategoryID]
(
	@BookCategoryID		[nvarchar](25)
)
AS
	BEGIN
		SELECT	[ISBN], [Title], [BookCategoryID], [BookConditionID], [BookGenreID],[Publisher], [PublicationDate],
				[WholesalePrice], [SalePrice], [Quantity], [QuantityByTitle], [LocationID], [Active]					
		FROM 	[dbo].[Book]
		WHERE	@BookCategoryID = [BookCategoryID]
		AND		[Active] = 1
	END
GO


print '' print '*** creating sp_select_books_by_bookConditionID'
GO
CREATE PROCEDURE [dbo].[sp_select_books_by_bookConditionID]
(
	@BookConditionID	[nvarchar](25)
)
AS
	BEGIN
		SELECT	[ISBN], [Title], [BookCategoryID], [BookConditionID], [BookGenreID],[Publisher], [PublicationDate],
				[WholesalePrice], [SalePrice], [Quantity], [QuantityByTitle], [LocationID], [Active]										
		FROM 	[dbo].[Book]
		WHERE	@BookConditionID = [BookConditionID]
		AND		[Active] = 1
	END
GO


print '' print '*** creating sp_select_books_by_bookGenreID'
GO
CREATE PROCEDURE [dbo].[sp_select_books_by_bookGenreID]
(
	@BookGenreID		[nvarchar](25)
)
AS
	BEGIN
		SELECT	[ISBN], [Title], [BookCategoryID], [BookConditionID], [BookGenreID],[Publisher], [PublicationDate], 
				[WholesalePrice], [SalePrice], [Quantity], [QuantityByTitle], [LocationID], [Active]
		FROM 	[dbo].[Book]
		WHERE	@BookGenreID = [BookGenreID]
		AND		[Active] = 1
	END
GO


print '' print '*** creating sp_insert_book'
GO
CREATE PROCEDURE [dbo].[sp_insert_book]
(
	@ISBN					[nvarchar](50),
	@Title					[nvarchar](100),
	@BookCategoryID			[nvarchar](25),
	@BookConditionID		[nvarchar](25),
	@BookGenreID			[nvarchar](25),
	@Publisher				[nvarchar](150),
	@PublicationDate		[datetime],
	@WholesalePrice			[decimal](6,2),
	@SalePrice				[decimal](6,2),
	@Quantity				[int],
	@QuantityByTitle		[int],
	@LocationID				[nvarchar](50)
)
AS	
	BEGIN
		INSERT INTO	[Book]
		(
			[ISBN], [Title], [BookCategoryID], [BookConditionID], [BookGenreID],[Publisher],
			[PublicationDate], [WholesalePrice], [SalePrice], [Quantity], [QuantityByTitle], [LocationID]
		)
		VALUES
		(
			@ISBN, @Title, @BookCategoryID, @BookConditionID, @BookGenreID, @Publisher,	
			@PublicationDate, @WholesalePrice, @SalePrice, @Quantity, @QuantityByTitle, @LocationID
		)
	END
GO


print '' print '*** creating sp_update_book'
GO
CREATE PROCEDURE [dbo].[sp_update_book]
(
	@ISBN					[nvarchar](50),
	@OldTitle				[nvarchar](100),
	@OldBookCategoryID		[nvarchar](25),
	@OldBookConditionID		[nvarchar](25),
	@OldBookGenreID			[nvarchar](25),
	@OldPublisher			[nvarchar](150),
	@OldPublicationDate		[datetime],
	@OldWholesalePrice		[decimal](6,2),
	@OldSalePrice			[decimal](6,2),
	@OldQuantity			[int],
	@OldQuantityByTitle		[int],
	@OldLocationID			[nvarchar](50),
	@OldActive				[bit],
	@NewTitle				[nvarchar](100),
	@NewBookCategoryID		[nvarchar](25),
	@NewBookConditionID		[nvarchar](25),
	@NewBookGenreID			[nvarchar](25),
	@NewPublisher			[nvarchar](150),
	@NewPublicationDate		[datetime],
	@NewWholesalePrice		[decimal](6,2),
	@NewSalePrice			[decimal](6,2),
	@NewQuantity			[int],
	@NewQuantityByTitle		[int],
	@NewLocationID			[nvarchar](50),
	@NewActive				[bit]
)
AS
	BEGIN
		UPDATE	[Book]
		SET		[Title] 			= @NewTitle,
				[BookCategoryID] 	= @NewBookCategoryID,
				[BookConditionID] 	= @NewBookConditionID,
				[BookGenreID] 		= @NewBookGenreID,
				[Publisher] 		= @NewPublisher,
				[PublicationDate]	= @NewPublicationDate,
				[WholesalePrice] 	= @NewWholesalePrice,
				[SalePrice] 		= @NewSalePrice,
				[Quantity] 			= @NewQuantity,
				[QuantityByTitle] 	= @NewQuantityByTitle,
				[LocationID] 		= @NewLocationID,
				[Active]			= @NewActive
		WHERE	[ISBN] 				= @ISBN
		  AND	[Title] 			= @OldTitle
		  AND	[BookCategoryID] 	= @OldBookCategoryID
		  AND	[BookConditionID]	= @OldBookConditionID
		  AND	[BookGenreID]		= @OldBookGenreID
		  AND	[Publisher]			= @OldPublisher
		  AND	[PublicationDate]	= @OldPublicationDate
		  AND	[WholesalePrice]	= @OldWholesalePrice
		  AND	[SalePrice]			= @OldSalePrice
		  AND	[Quantity]			= @OldQuantity
		  AND	[QuantityByTitle]	= @OldQuantityByTitle
		  AND	[LocationID]		= @OldLocationID
		  AND	[Active]			= @OldActive
		RETURN @@ROWCOUNT
	END
GO



/* Author tables and stored procedures */

/* Author table */
print '' print '*** creating Author table'
GO
CREATE TABLE [dbo].[Author]
(
	[AuthorID]		[int] IDENTITY(100000,1)	NOT NULL,
	[GivenName]		[nvarchar](50)				NOT NULL,
	[FamilyName]	[nvarchar](100)				NOT NULL
	CONSTRAINT [pk_AuthorID] PRIMARY KEY ([AuthorID])
)
GO


print '' print '*** inserting data into Author table'
GO
INSERT INTO [dbo].[Author]
		([GivenName], [FamilyName])
	VALUES
		('J.R.R.', 'Tolkien'),
		('Stephen', 'King'),
		('Ludwig', 'Von Mises'),
		('Stephen R.', 'Covey'),
		('William', 'Zinsser'),
		('Jason T.', 'Roff'),
		('Leo', 'Tolstoy'),
		('Orson Scott', 'Card'),
		('John', 'Aberth'),
		('Kurt', 'Vonnegut'),
		('Peter', 'Straub'),
		('V.S.', 'Naipaul'),
		('John', 'Marzluff'),
		('Tony', 'Angell'),
		('Tova', 'Friedman'),
		('Malcolm', 'Brabant'),
		('Ray', 'Bradbury'),
		('John', 'Steinbeck'),
		('John', 'Updike'),
		('Nicholas', 'Sparks')
GO


/* BookAuthor tables */
print '' print '*** creating BookAuthor table'
GO

CREATE TABLE [dbo].[BookAuthor]
(
	[ISBN]			[nvarchar](50)		NOT NULL,
	[AuthorID]		[int]				NOT NULL,
	CONSTRAINT [fk_BookAuthor_ISBN]	FOREIGN KEY ([ISBN])
		REFERENCES [dbo].[Book]([ISBN]),
	CONSTRAINT [fk_BookAuthor_AuthorID]	FOREIGN KEY ([AuthorID])
		REFERENCES [dbo].[Author]([AuthorID]),
	CONSTRAINT [pk_BookAuthor] PRIMARY KEY ([ISBN], [AuthorID])
)
GO


print '' print '*** inserting sample BookAuthor records'
GO
INSERT INTO [dbo].[BookAuthor]
		([ISBN], [AuthorID])
	VALUES
		('0-345-33971-1', 100000),
		('0-670-85869-2', 100001),
		('0-670-69199-2', 100001),
		('978-1-61016-133-6', 100002),
		('0-671-70863-5', 100003),
		('978-0-06-172903-4', 100004),
		('0-07-222460-6', 100005),
		('978-0-87140-426-8', 100006),
		('0-812-51685-0', 100007),
		('0-415-92715-3', 100008),
		('0-670-69199-2', 100010),
		('0-679-72202-5', 100011),
		('978-1-4391-9874-2', 100012),
		('978-1-4391-9874-2', 100013),
		('978-1-335-44930-6', 100014),
		('978-1-335-44930-6', 100015),
		('978-0385029988', 100016),
		('978-0-385-33348-1', 100009),
		('0-14-01-7738-8', 100017),
		('0-449-91121-7', 100018),
		('0-446-60895-5', 100019)
GO


/* Author stored procedures */

print '' print '*** creating sp_insert_author'
GO
CREATE PROCEDURE [dbo].[sp_insert_author]
(
	@GivenName				[nvarchar](50),
	@FamilyName				[nvarchar](100)
)
AS	
	BEGIN
		INSERT INTO	[Author]
		([GivenName], [FamilyName])
		VALUES
		(@GivenName, @FamilyName)
		SELECT SCOPE_IDENTITY()
	END
GO


print '' print '*** creating sp_update_author'
GO
CREATE PROCEDURE [dbo].[sp_update_author]
(
	@OldAuthorID			int,
	@OldGivenName			[nvarchar](50),
	@OldFamilyName			[nvarchar](100),
	@NewGivenName			[nvarchar](50),
	@NewFamilyName			[nvarchar](100) 	
)
AS
	BEGIN
		UPDATE	[Author]
		SET		[GivenName]	 = @NewGivenName,
				[FamilyName] = @NewFamilyName
		WHERE	[AuthorID] 	 = @OldAuthorID
		  AND	[GivenName]  = @OldGivenName
		  AND	[FamilyName] = @OldFamilyName
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** creating sp_select_authors_by_isbn'
GO
CREATE PROCEDURE [dbo].[sp_select_authors_by_isbn]
(
	@ISBN			[nvarchar](50)
)
AS
	BEGIN
		SELECT	[Author].[AuthorID], [Author].[GivenName], [Author].[FamilyName]												
		FROM 	[Author] JOIN [BookAuthor]
			ON [Author].[AuthorID] = [BookAuthor].[AuthorID]
		WHERE	@ISBN = [BookAuthor].[ISBN]
	END
GO


print '' print '*** creating sp_insert_bookAuthor'
GO
CREATE PROCEDURE [dbo].[sp_insert_bookAuthor]
(
	@ISBN					[nvarchar](50),
	@AuthorID				[int]
)
AS	
	BEGIN
		INSERT INTO	[BookAuthor]
		([ISBN], [AuthorID])
		VALUES
		(@ISBN, @AuthorID)
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** creating sp_update_bookAuthor'
GO
CREATE PROCEDURE [dbo].[sp_update_bookAuthor]
(
	@OldISBN					[nvarchar](50),
	@OldAuthorID				[int],
	@NewISBN					[nvarchar](50),
	@NewAuthorID				[int]
)
AS	
	BEGIN
		UPDATE	[BookAuthor]
		SET		[ISBN]	 	 = @NewISBN,
				[AuthorID] 	 = @NewAuthorID
		WHERE	[ISBN]	 	 = @OldISBN
		  AND	[AuthorID]   = @OldAuthorID
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** creating sp_delete_bookAuthor'
GO
CREATE PROCEDURE [dbo].[sp_delete_bookAuthor]
(
	@ISBN						[nvarchar](50),
	@AuthorID					[int]
)
AS	
	BEGIN
		DELETE FROM [BookAuthor]
		WHERE	[ISBN]	 	 = @ISBN
		  AND	[AuthorID]   = @AuthorID
		RETURN @@ROWCOUNT
	END
GO


/* SELECT EXCECUTE SCALAR INT32 */
print '' print '*** creating sp_select_authors_by_authorID'
GO
CREATE PROCEDURE [dbo].[sp_select_authors_by_authorID]
(
	@AuthorID			[nvarchar](50)
)
AS
	BEGIN
		SELECT	[Author].[GivenName], [Author].[FamilyName]												
		FROM 	[Author]
		WHERE	@AuthorID = [Author].[AuthorID]
	END
GO


/* CUSTOMER TABLES AND STORED PROCEDURES */

/* Customer table */
print '' print '*** creating Customer table'
GO
CREATE TABLE [dbo].[Customer]
(
	[CustomerID]	[int] IDENTITY(100000,1)	NOT NULL,
	[GivenName]		[nvarchar](50)				NOT NULL,
	[FamilyName]	[nvarchar](100)				NOT NULL,
	[Phone]			[nvarchar](13)				NOT NULL,
	[Email]			[nvarchar](100)				NOT NULL,
	[OkToContact]	[bit]						NOT NULL DEFAULT 1,
	[Active]		[bit]						NOT NULL DEFAULT 1,
	CONSTRAINT [pk_CustomerID] PRIMARY KEY ([CustomerID]),
	CONSTRAINT [fk_Customer_Email] UNIQUE ([Email])
)
GO


/* Customer test records */
print '' print '*** creating Customer test records'
GO
INSERT INTO [dbo].[Customer]
		([GivenName], [FamilyName], [Phone], [Email])
	VALUES
		('Mike', 'West', '3195551112', 'mike@customer.com'),
		('Hellen', 'Gonzalez', '3195552223', 'hellen@customer.com'),
		('Aaron', 'Caspian', '3195553334', 'aaron@customer.com'),
		('Eliza', 'Salco', '3195554445', 'eliza@customer.com'),
		('Tyson', 'Bartlett', '3195555556', 'tyson@customer.com'),
		('Elle', 'Wethington', '3196676677', 'elle@customer.com')
GO


/* Customer stored procedures */
print '' print '*** creating sp_select_all_customers'
GO
CREATE PROCEDURE [dbo].[sp_select_all_customers]
AS
	BEGIN
		SELECT	[CustomerID], [GivenName], [FamilyName], [Phone], [Email], [OkToContact], [Active]
		FROM 	[dbo].[Customer]
	END
GO


print '' print '*** creating sp_select_all_active_customers'
GO
CREATE PROCEDURE [dbo].[sp_select_all_active_customers]
AS
	BEGIN
		SELECT	[CustomerID], [GivenName], [FamilyName], [Phone], [Email], [OkToContact], [Active]
		FROM 	[dbo].[Customer]
		WHERE	[Active] = 1
	END
GO
























