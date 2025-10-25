using System;
using TransferCenterCore.Models;
using TransferCenterWeb.Models.PatientTransfer;
using AdditionalInfo = TransferCenterCore.Models.AdditionalInfo;

namespace TransferCenterWeb.Translators.PatientTransfer
{
    public static class PatientDetailsTranslator
    {
        // Web to Core
        public static TransferCenterCore.Models.PatientDetails ToCoreModel(this TransferCenterWeb.Models.PatientTransfer.PatientDetails source, Guid guid)
        {
            if (source == null) return new TransferCenterCore.Models.PatientDetails();

            return new TransferCenterCore.Models.PatientDetails
            {
                Id = source.Id,
                UId = guid,
                Name = $"{source.FirstName} {source.LastName}",
                DOB = source.DOB,
                Gender = source.Gender,
                Height = source.Height,
                Weight = source.Weight,
                Diagnosis = source.Diagnosis,
                ReasonForTransfer = source.ReasonForTransfer,
                LevelOfCareNeeded = source.LevelOfCareNeeded,
                Sitter = source.Sitter,
                JehovahWitness = source.JehovahWitness,
                Capitated = source.Capitated,
                IsIsolation = source.IsIsolation,
                IsolationType = source.IsolationType,
                CodeStatus = source.CodeStatus,
                GCS = source.GCS
            };
        }

        // Core to Web
        public static TransferCenterWeb.Models.PatientTransfer.PatientDetails ToWebModel(this TransferCenterCore.Models.PatientDetails source)
        {
            if (source == null) new TransferCenterWeb.Models.PatientTransfer.PatientDetails();

            var nameParts = source.Name?.Split(' ', 2);

            return new TransferCenterWeb.Models.PatientTransfer.PatientDetails
            {
                Id = source.Id,
                UId = source.UId,
                FirstName = nameParts?.Length > 0 ? nameParts[0] : string.Empty,
                LastName = nameParts?.Length > 1 ? nameParts[1] : string.Empty,
                DOB = source.DOB,
                Gender = source.Gender,
                Height = source.Height,
                Weight = source.Weight,
                Diagnosis = source.Diagnosis,
                ReasonForTransfer = source.ReasonForTransfer,
                LevelOfCareNeeded = source.LevelOfCareNeeded,
                Sitter = source.Sitter,
                JehovahWitness = source.JehovahWitness,
                Capitated = source.Capitated,
                IsIsolation = source.IsIsolation,
                IsolationType = source.IsolationType,
                CodeStatus = source.CodeStatus ?? string.Empty,
                GCS = source.GCS ?? string.Empty
            };
        }
    }
}