CREATE PROCEDURE [dbo].[spImportVendors]
	@VendorImportId uniqueidentifier 
AS
	DECLARE @VendorId int
    DECLARE @OrgId int
    DECLARE @OrgName varchar(100)
    DECLARE @VendorName varchar(50)
	DECLARE @Industry varchar(50)
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
	DECLARE @VendorTypeId int = NULL
	
	SET @MainCursor = CURSOR FORWARD_ONLY DYNAMIC
	FOR 
	SELECT 
		VendorId,
		OrgId,
		OrgName,
		VendorName,
		Industry,
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
	FROM VendorImport
	Where ImportId = @VendorImportId
	OPEN @MainCursor
	FETCH NEXT FROM @MainCursor INTO 
		@VendorId,
		@OrgId,
		@OrgName,
		@VendorName,
		@Industry,
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

		-- Create Vendor Record
		INSERT INTO [dbo].[Vendor]
		(
			OrgId, 
			Name
		)
		VALUES
		(
			@newOrgId, 
			@VendorName
		)		
		SET @VendorId = SCOPE_IDENTITY()
		UPDATE VendorImport
		SET OrgId = @newOrgId,
			VendorId = @VendorId
		WHERE CURRENT OF @MainCursor

	FETCH NEXT FROM @MainCursor INTO 
		@VendorId,
		@OrgId,
		@OrgName,
		@VendorName,
		@Industry,
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

	UPDATE VendorImport
		SET ProcessedOn = GETUTCDATE()
		WHERE ImportId = @VendorImportId	

RETURN 0