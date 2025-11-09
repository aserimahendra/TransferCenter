namespace TransferCenterCore.Translators;

public static class UserTranslator
{
	// Core model -> Store entity
	public static TransferCenterDbStore.Entities.User ToEntity(this Models.User source)
	{
		if (source == null) return null!;

		return new TransferCenterDbStore.Entities.User
		{
			UserId = source.UserId,
			FirstName = source.FirstName,
			LastName = source.LastName,
			EmailId = source.EmailId,
			Password = source.Password,
			DomainID = source.DomainID,
			LoginId = source.LoginId,
			CreatedOn = source.CreatedOn,
			Role = source.Role,
			IsActive = source.IsActive,
			CreatedBy = source.CreatedBy,
		};
	}

	// Store entity -> Core model
	public static Models.User ToCoreModel(this TransferCenterDbStore.Entities.User source)
	{
		if (source == null) return null!;

		return new Models.User
		{
			UserId = source.UserId,
			FirstName = source.FirstName,
			LastName = source.LastName,
			EmailId = source.EmailId,
			Password = source.Password,
			DomainID = source.DomainID,
			LoginId = source.LoginId,
			CreatedOn = source.CreatedOn,
			Role = source.Role,
			IsActive = source.IsActive,
			CreatedBy = source.CreatedBy,
		};
	}
}