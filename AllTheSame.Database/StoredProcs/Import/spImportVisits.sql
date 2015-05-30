CREATE PROCEDURE [dbo].[spImportVisits]
	@VisitImportId uniqueidentifier 
AS
	-- VisitImport mapping
	DECLARE 
		@VisitId int,
		@TimeIn datetime,
		@TimeOut datetime,
		@VisitType varchar(100),
		@CommunityName varchar(100),
		@CommunityPhone varchar(50),
		@ResidentFirstName varchar(50),
		@ResidentLastName varchar(50),
		@ResidentEmail varchar(100),
		@ResidentPhone varchar(50),
		@VendorCompanyName varchar(50),
		@VendorCompanyPhone varchar(50),
		@VendorType varchar(50),
		@VendorWorkerFirstName varchar(50),
		@VendorWorkerLastName varchar(50),
		@VendorWorkerEmail varchar(50),
		@VendorWorkerPhone varchar(50),
		@VisitorFirstName varchar(50),
		@VisitorLastName varchar(50),
		@VisitorEmail varchar(50),
		@VisitorPhone varchar(50)
	
	-- Cursor Variables
	DECLARE @MainCursor CURSOR,
			@VendorTypeId int,
			@VisitTypeId int,
			@CommunityId int,
			@ResidentId int,
			@PersonId int,
			@VendorId int,
			@VendorWorkerId int,
			@VisitorId int		
	
	SET @MainCursor = CURSOR FORWARD_ONLY DYNAMIC
	FOR 
	SELECT 
		[Id],
		[TimeIn],
		[TimeOut],
		[VisitType],
		[CommunityName],
		[CommunityPhone],
		[ResidentFirstName],
		[ResidentLastName],
		[ResidentEmail],
		[ResidentPhone],
		[VendorCompanyName],
		[VendorCompanyPhone],
		[VendorType],
		[VendorWorkerFirstName],
		[VendorWorkerLastName],
		[VendorWorkerEmail],
		[VendorWorkerPhone],
		[VisitorFirstName],
		[VisitorLastName],
		[VisitorEmail],
		[VisitorPhone]
  FROM [VisitImport]
	Where ImportId = @VisitImportId
	OPEN @MainCursor
	FETCH NEXT FROM @MainCursor INTO 
		@VisitId,
		@TimeIn,
		@TimeOut,
		@VisitType,
		@CommunityName,
		@CommunityPhone,
		@ResidentFirstName,
		@ResidentLastName,
		@ResidentEmail,
		@ResidentPhone,
		@VendorCompanyName,
		@VendorCompanyPhone,
		@VendorType,
		@VendorWorkerFirstName,
		@VendorWorkerLastName,
		@VendorWorkerEmail,
		@VendorWorkerPhone,
		@VisitorFirstName,
		@VisitorLastName,
		@VisitorEmail,
		@VisitorPhone
	WHILE @@FETCH_STATUS = 0
	BEGIN	
		-- Fetch Community or Error
		SELECT @CommunityId = c.Id
		FROM Community c JOIN Organization o on c.OrgId = o.Id
		WHERE c.Name = @CommunityName OR o.OfficePhone = @CommunityPhone

		IF @CommunityId IS NULL

			--TODO: Insert Record into ImportErrorLog
			CONTINUE		

		-- Fetch Resident or Create New
		-- Search for Resident of this community or and existing Person who matches
		SELECT	@ResidentId = r.Id, @PersonId = p.Id
		FROM	Resident r 
		right join Person p on r.PersonId = p.Id 
		WHERE	(r.CommunityId = @CommunityId OR r.Id IS NULL )AND (
					p.Email = @ResidentEmail OR p.HomePhone = @ResidentPhone OR p.MobilePhone = @ResidentPhone 
					OR (p.FirstName = @ResidentFirstName AND p.LastName = @ResidentLastName )
				)
		IF @PersonId IS Null		
		BEGIN
			INSERT INTO	Person (FirstName, LastName, Email, HomePhone)
			Values (@ResidentFirstName, @ResidentLastName, @ResidentEmail, @ResidentPhone)
			SELECT @PersonId = SCOPE_IDENTITY()
		END

		IF @ResidentId IS NULL
		BEGIN
			INSERT INTO Resident (PersonId, CommunityId)
			VALUES (@PersonId, @CommunityId)
			SELECT @ResidentId = SCOPE_IDENTITY()
		END

		SET @PersonId = NULL -- Reset for loading VendorWorker
				
		-- Determine if VendorWorker or Visitor
		SELECT @VendorId = v.Id
		FROM Vendor v JOIN Organization o on v.OrgId = o.Id
		WHERE v.Name = @VendorCompanyName OR o.OfficePhone = @VendorCompanyPhone

		IF @VendorId <> NULL
		-- Fetch VendorWorker or Create New				
		BEGIN
			SELECT @VendorWorkerId = vw.Id, @PersonId = p.Id
			FROM VendorWorker vw
			RIGHT JOIN Person p on vw.PersonId = p.Id
			WHERE (vw.VendorId = @VendorId OR vw.Id IS NULL ) AND (
						p.Email = @ResidentEmail OR p.HomePhone = @ResidentPhone OR p.MobilePhone = @ResidentPhone 
						OR (p.FirstName = @ResidentFirstName AND p.LastName = @ResidentLastName )
					)
			IF @PersonId IS Null		
			-- Create Person
			BEGIN
				INSERT INTO	Person (FirstName, LastName, Email, HomePhone)
				Values (@VendorWorkerFirstName, @VendorWorkerLastName, @VendorWorkerEmail, @VendorWorkerPhone)
				SELECT @PersonId = SCOPE_IDENTITY()
			END
		
			IF @VendorWorkerId IS NULL
			-- Create Vendor Worker
			BEGIN
				INSERT INTO VendorWorker (VendorId, PersonId)
				VALUES(@VendorID, @PersonId)
			END			
		
		END
		ELSE IF @VisitorFirstName <> NULL AND @VisitorLastName <> NULL
		BEGIN
			SELECT @VisitorId = v.Id, @PersonId = p.Id
			FROM Person p 
			LEFT JOIN Visitor v on p.Id = v.PersonId
			WHERE  
				p.Email = @VisitorEmail OR p.HomePhone = @VisitorPhone OR p.MobilePhone = @VisitorPhone 
				OR (p.FirstName = @VisitorFirstName AND p.LastName = @VisitorLastName )

			IF @PersonId IS Null		
			-- Create Person
			BEGIN
				INSERT INTO	Person (FirstName, LastName, Email, HomePhone)
				Values (@VendorWorkerFirstName, @VendorWorkerLastName, @VendorWorkerEmail, @VendorWorkerPhone)
				SELECT @PersonId = SCOPE_IDENTITY()
			END

			IF @VisitorId IS NULL
			BEGIN
				INSERT INTO Visitor(PersonId) VALUES(@PersonId)
				SELECT @VisitorId = SCOPE_IDENTITY();

			END
		END
		ELSE
			CONTINUE --TOOD: Insert into ImportErrorLog
			
				
		-- Finally we can create the visit
		INSERT INTO Visit(ResidentId, VendorWorkerId, VisitorId, TimeIn, [TimeOut], VisitType)
		VALUES(@ResidentId, @VendorWorkerId, @VisitorId, @TimeIn, @TimeOut, @VisitType)

		
		SET @VisitId = SCOPE_IDENTITY()
		UPDATE VisitImport
		SET	VisitId = @VisitId
		WHERE CURRENT OF @MainCursor

		-- Reset Cursor Iteration variables
		SET @CommunityId = NULL
		SET	@ResidentId = NULL
		SET @PersonId = NULL
		SET @VisitorId = NULL
		
	FETCH NEXT FROM @MainCursor INTO 
		@VisitId,
		@TimeIn,
		@TimeOut,
		@VisitType,
		@CommunityName,
		@CommunityPhone,
		@ResidentFirstName,
		@ResidentLastName,
		@ResidentEmail,
		@ResidentPhone,
		@VendorCompanyName,
		@VendorCompanyPhone,
		@VendorType,
		@VendorWorkerFirstName,
		@VendorWorkerLastName,
		@VendorWorkerEmail,
		@VendorWorkerPhone,
		@VisitorFirstName,
		@VisitorLastName,
		@VisitorEmail,
		@VisitorPhone
	END
	CLOSE @MainCursor
	DEALLOCATE @MainCursor

	UPDATE VisitImport
		SET ProcessedOn = GETUTCDATE()
		WHERE ImportId = @VisitImportId


RETURN 0