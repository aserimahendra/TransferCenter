using TransferCenterCore.Context;

namespace TransferCenterWeb.Translators;

public static class PatientDetailsTranslator
{
    public static TransferCenterCore.Models.PatientDetails ToCoreModel(this Models.GlobalPatientTransfer.PatientDetails patientDetails, Guid guid)
    {
        if (patientDetails == null) return null!;

        return new TransferCenterCore.Models.PatientDetails
        {
            Id = patientDetails.Id,
            UId = guid,
            Name = patientDetails.Name,
            DOB = patientDetails.DOB,
            Gender = patientDetails.Gender,
            IsIsolation = patientDetails.IsIsolation,
            IsolationType = patientDetails.IsolationType,
            // Height conversion: store as feet + (inches / 10)
            // e.g., 5 feet 5 inches = 5.5
            Height = (patientDetails.HeightFeet.HasValue || patientDetails.HeightInches.HasValue)
                ? (double)((patientDetails.HeightFeet ?? 0) + ((patientDetails.HeightInches ?? 0) / 10.0))
                : patientDetails.Height,
            Weight = patientDetails.Weight,
            WeightIn = patientDetails.WeightIn,
            Diagnosis = patientDetails.Diagnosis,
            LevelOfCareNeeded = patientDetails.LevelOfCareNeeded,
            AcceptingPhysician = patientDetails.AcceptingPhysician,
            ReasonForTransfer = patientDetails.ReasonForTransfer,
            Lateral = patientDetails.Lateral,
            HLOC = patientDetails.HLOC,
            PatientInsurance = patientDetails.PatientInsurance,
            IsActive = patientDetails.IsActive,
            TransferType = (short)Models.TransferType.GlobalPatientTransfer,
            CreatedBy = CallContextScope.Current?.EmailId ?? String.Empty,
            CreatedOn = patientDetails.CreatedOn,
            LastUpdatedOn = patientDetails.LastUpdatedOn
            
        };
    }
    public static TransferCenterWeb.Models.GlobalPatientTransfer.PatientDetails ToWebModel(this TransferCenterCore.Models.PatientDetails coreModel)
    {
        if (coreModel == null) return null!;

        return new TransferCenterWeb.Models.GlobalPatientTransfer.PatientDetails
        {
            Id = coreModel.Id,
            UId= coreModel.UId,
            Name = coreModel.Name,
            DOB = coreModel.DOB,
            Gender = coreModel.Gender,
            IsIsolation = coreModel.IsIsolation,
            IsolationType = coreModel.IsolationType,
            Height = coreModel.Height,
            Weight = coreModel.Weight,
            WeightIn = coreModel.WeightIn,
            Diagnosis = coreModel.Diagnosis,
            // Extract feet and inches from Height stored as feet + (inches / 10)
            // e.g., 5.5 = 5 feet and 5 inches
            HeightFeet = (int)Math.Floor(coreModel.Height),
            HeightInches = (int)Math.Round((coreModel.Height - Math.Floor(coreModel.Height)) * 10),
            LevelOfCareNeeded = coreModel.LevelOfCareNeeded,
            AcceptingPhysician = coreModel.AcceptingPhysician,
            ReasonForTransfer = coreModel.ReasonForTransfer,
            Lateral = coreModel.Lateral,
            HLOC = coreModel.HLOC,
            PatientInsurance = coreModel.PatientInsurance,
            IsActive = coreModel.IsActive,
            CreatedBy = coreModel.CreatedBy,
            CreatedOn = coreModel.CreatedOn,
            LastUpdatedOn = coreModel.LastUpdatedOn

        };
    }
}