namespace TransferCenterWeb.Translators
{
    public static class PatientDetailsTranslator
    {
        public static TransferCenterCore.Models.PatientDetails ToCoreModel(this Models.PatientDetails patientDetails)
        {
            if (patientDetails == null) return null!;

            return new TransferCenterCore.Models.PatientDetails
            {
                Id = patientDetails.Id,
                Name = patientDetails.Name,
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
            };
        }
        public static TransferCenterWeb.Models.PatientDetails ToWebModel(this TransferCenterCore.Models.PatientDetails coreModel)
        {
            if (coreModel == null) return null!;

            return new TransferCenterWeb.Models.PatientDetails
            {
                Id = coreModel.Id,
                Name = coreModel.Name,
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
            };
        }
    }
}
