CREATE PROCEDURE [dbo].[spImportCommunities]
	@CommunityImportId uniqueidentifier 
AS
	DECLARE @CommunityId int
    DECLARE @OrgId int
    DECLARE @OrgName varchar(100)
    DECLARE @Description varchar(500)
	DECLARE @Industry varchar(50)
	DECLARE @CommunityType varchar(50)
    DECLARE @Raiting int
    DECLARE @NumBeds int
    DECLARE @CommunityName varchar(50)
    DECLARE @Facebook varchar(50)
    DECLARE @Twitter varchar(50)
    DECLARE @GooglePlus varchar(50)
    DECLARE @AnnualRevenue int
    DECLARE @NumEmployees int
    DECLARE @WebURL varchar(100)
    DECLARE @OfficePhone varchar(50)
    DECLARE @AltPhone varchar(50)
    DECLARE @TickerSymbol varchar(10)
    DECLARE @BillingLine1 varchar(100)
    DECLARE @BillingLine2 varchar(100)
    DECLARE @BillingCity varchar(50)
    DECLARE @BillingState varchar(50)
    DECLARE @BillingCountry varchar(50)
    DECLARE @BillingPostalCode varchar(50)
    DECLARE @ShippingLine1 varchar(100)
    DECLARE @ShippingLine2 varchar(100)
    DECLARE @ShippingCity varchar(50)
    DECLARE @ShippingState varchar(50)
    DECLARE @ShippingCountry varchar(50)
    DECLARE @ShippingPostalCode varchar(50)
	DECLARE @MainCursor CURSOR
	DECLARE @parentOrgId int = 1 
	DECLARE @newOrgId int
	DECLARE @newBillingId int
	DECLARE @newShippingId int
	DECLARE @IndustryId int = NULL
	DECLARE @CommunityTypeId int = NULL
	
	SET @MainCursor = CURSOR FORWARD_ONLY DYNAMIC
	FOR 
	SELECT 
		CommunityId,
		OrgId,
		OrgName,
		Description,
		Industry,
		CommunityType,
		Raiting,
		NumBeds,
		CommunityName,
		Facebook,
		Twitter,
		GooglePlus,
		AnnualRevenue,
		NumEmployees,
		WebURL,
		OfficePhone,
		AltPhone,
		TickerSymbol,
		BillingLine1,
		BillingLine2,
		BillingCity,
		BillingState,
		BillingCountry,
		BillingPostalCode,
		ShippingLine1,
		ShippingLine2,
		ShippingCity,
		ShippingState,
		ShippingCountry,
		ShippingPostalCode		
	FROM CommunityImport ci
	Where ci.ImportId = @CommunityImportId
	OPEN @MainCursor
	FETCH NEXT FROM @MainCursor INTO 
		@CommunityId,
		@OrgId,
		@OrgName,
		@Description,
		@Industry,
		@CommunityType,
		@Raiting,
		@NumBeds,
		@CommunityName,
		@Facebook,
		@Twitter,
		@GooglePlus,
		@AnnualRevenue,
		@NumEmployees,
		@WebURL,
		@OfficePhone,
		@AltPhone,
		@TickerSymbol,
		@BillingLine1,
		@BillingLine2,
		@BillingCity,
		@BillingState,
		@BillingCountry,
		@BillingPostalCode,
		@ShippingLine1,
		@ShippingLine2,
		@ShippingCity,
		@ShippingState,
		@ShippingCountry,
		@ShippingPostalCode
	WHILE @@FETCH_STATUS = 0
	BEGIN	

	-- Create Billing and Shipping Addresses
		INSERT INTO [dbo].[Address] 
		(
			Line1,
			Line2,
			City,
			State,
			Country,
			PostalCode
		)
		VALUES
		(
			@BillingLine1,
			@BillingLine2,
			@BillingCity,
			@BillingState,
			@BillingCountry,
			@BillingPostalCode
		)
		SET @newBillingId = SCOPE_IDENTITY()

		INSERT INTO [dbo].[Address] 
		(
			Line1,
			Line2,
			City,
			State,
			Country,
			PostalCode
		)
		VALUES
		(
			@ShippingLine1,
			@ShippingLine2,
			@ShippingCity,
			@ShippingState,
			@ShippingCountry,
			@ShippingPostalCode
		)
		SET @newShippingId = SCOPE_IDENTITY()

		SELECT TOP 1 @IndustryId = Id FROM  Industry WHERE Code = @Industry
		SELECT TOP 1 @CommunityTypeId = Id FROM CommunityType WHERE Code = @CommunityType

		-- Create Org Record
		EXEC [dbo].[spOrganizationAdd] 
			@parentOrgId = NULL,
			@name = @OrgName,
			@newId = @newOrgId OUTPUT 

		UPDATE Organization
			SET Facebook =  @Facebook,
			Twitter =  @Twitter,
			GooglePlus =  @GooglePlus, 
			AnnualRevenue =  @AnnualRevenue, 
			NumEmployees =  @NumEmployees, 
			WebURL =  @WebURL, 
			OfficePhone = @OfficePhone, 
			AltPhone =  @AltPhone, 
			TickerSymbol =  @TickerSymbol,
			BillingAddressId = @newBillingId,
			ShippingAddressId = @newShippingId,
			IndustryId = @IndustryId
			
		WHERE Id = @newOrgId 

		-- Create Community Record
		INSERT INTO [dbo].[Community]
		(
			OrgId, 
			Name, 
			Description,    
			Raiting, 
			NumBeds,
			CommunityTypeId
		)
		VALUES
		(
			@newOrgId, 
			@CommunityName, 
			@Description,    
			@Raiting, 
			@NumBeds,
			@CommunityTypeId
		)		
		SET @CommunityId = SCOPE_IDENTITY()
		UPDATE CommunityImport
		SET OrgId = @newOrgId,
			CommunityId = @CommunityId
		WHERE CURRENT OF @MainCursor

	FETCH NEXT FROM @MainCursor INTO 
		@CommunityId,
		@OrgId,
		@OrgName,
		@Description,
		@Industry,
		@CommunityType,
		@Raiting,
		@NumBeds,
		@CommunityName,
		@Facebook,
		@Twitter,
		@GooglePlus,
		@AnnualRevenue,
		@NumEmployees,
		@WebURL,
		@OfficePhone,
		@AltPhone,
		@TickerSymbol,
		@BillingLine1,
		@BillingLine2,
		@BillingCity,
		@BillingState,
		@BillingCountry,
		@BillingPostalCode,
		@ShippingLine1,
		@ShippingLine2,
		@ShippingCity,
		@ShippingState,
		@ShippingCountry,
		@ShippingPostalCode
	END
	CLOSE @MainCursor
	DEALLOCATE @MainCursor

	UPDATE CommunityImport
		SET ProcessedOn = GETUTCDATE()
		WHERE ImportId = @CommunityImportId	

RETURN 0