namespace TransferCenterCore.Translators;

public static class PatientDetailsTranslator
{
    public static TransferCenterDbStore.Entities.PatientDetails ToEntity(this Models.PatientDetails patientDetails)
    {
        if (patientDetails == null) return null!;

        return new TransferCenterDbStore.Entities.PatientDetails
        {
            UId = patientDetails.UId,
            Id = patientDetails.Id,
            Name = patientDetails.Name,
            TransferType = patientDetails.TransferType,
            DOB = patientDetails.DOB,
            Gender = patientDetails.Gender,
            IsIsolation = patientDetails.IsIsolation,
            IsolationType = patientDetails.IsolationType,
            Height = patientDetails.Height,
            Weight = patientDetails.Weight,
            Diagnosis = patientDetails.Diagnosis,
            LevelOfCareNeeded = patientDetails.LevelOfCareNeeded,
            AcceptingPhysician = patientDetails.AcceptingPhysician,
            ReasonForTransfer = patientDetails.ReasonForTransfer,
            Lateral = patientDetails.Lateral,
            HLOC = patientDetails.HLOC,
            PatientInsurance = patientDetails.PatientInsurance,
            CodeStatus = patientDetails.CodeStatus,
            Sitter = patientDetails.Sitter,
            JehovahWitness = patientDetails.JehovahWitness,
            Capitated = patientDetails.Capitated,
            GCS = patientDetails.GCS,
            WeightIn = patientDetails.WeightIn,
            IsActive = patientDetails.IsActive,
            CreatedOn = patientDetails.CreatedOn,
            CreatedBy = patientDetails.CreatedBy,
            LastUpdatedOn = patientDetails.LastUpdatedOn
        };
    }
    public static Models.PatientDetails ToCoreModel(this TransferCenterDbStore.Entities.PatientDetails coreModel)
    {
        if (coreModel == null) return null!;

        return new Models.PatientDetails
        {
            Id = coreModel.Id,
            UId = coreModel.UId,
            Name = coreModel.Name,
            TransferType = coreModel.TransferType,
            DOB = coreModel.DOB,
            Gender = coreModel.Gender,
            IsIsolation = coreModel.IsIsolation,
            IsolationType = coreModel.IsolationType,
            Height = coreModel.Height,
            Weight = coreModel.Weight,
            Diagnosis = coreModel.Diagnosis,
            LevelOfCareNeeded = coreModel.LevelOfCareNeeded,
            AcceptingPhysician = coreModel.AcceptingPhysician,
            ReasonForTransfer = coreModel.ReasonForTransfer,
            Lateral = coreModel.Lateral,
            HLOC = coreModel.HLOC,
            PatientInsurance = coreModel.PatientInsurance,
            CodeStatus = coreModel.CodeStatus,
            Sitter = coreModel.Sitter,
            JehovahWitness = coreModel.JehovahWitness,
            Capitated = coreModel.Capitated,
            GCS = coreModel.GCS,
            WeightIn = coreModel.WeightIn,
            IsActive = coreModel.IsActive,
            CreatedOn = coreModel.CreatedOn,
            CreatedBy = coreModel.CreatedBy,
            LastUpdatedOn = coreModel.LastUpdatedOn,
            
        };
    }
}