
using TransferCenterCore.Context;

namespace TransferCenterWeb.Translators;

public static class PatientTransferInfoTranslator
{
    // Web → Core
    public static TransferCenterCore.Models.PatientTransferInfo ToCoreModel(this Models.GlobalPatientTransfer.PatientTransferInfo source, Guid guid)
    {
        if (source == null) return null!;

        return new TransferCenterCore.Models.PatientTransferInfo
        {
            Id = source.Id,
            UId = guid,
            CaseMgrSwRn = source.CaseMgrSwRn,
            PhoneNumber = source.PhoneNumber,
            FaxNumber = source.FaxNumber,
            RequestingFacility = source.RequestingFacility,
            TransferDate = source.TransferDate,
            ReferringMd = source.ReferringMd,
            ReferringMdPhone = source.ReferringMdPhone,
            ReferringSpecialistPhone = source.ReferringSpecialistPhone,
            ReferringSpecialist = source.ReferringSpecialist,
            AdmitDate = source.AdmitDate,
            Unit = source.Unit,
            UnitPhone = source.UnitPhone,
            IsActive = source.IsActive,
            TransferType = (short)Models.TransferType.GlobalPatientTransfer,
            CreatedBy = CallContextScope.Current?.EmailId ?? String.Empty,
            CreatedOn = source.CreatedOn,
            LastUpdatedOn = source.LastUpdatedOn
        };
    }

    // Core → Web
    public static Models.GlobalPatientTransfer.PatientTransferInfo ToWebModel(this TransferCenterCore.Models.PatientTransferInfo source)
    {
        if (source == null) return null!;

        return new Models.GlobalPatientTransfer.PatientTransferInfo
        {
            Id = source.Id,
            UId = source.UId,
            CaseMgrSwRn = source.CaseMgrSwRn,
            PhoneNumber = source.PhoneNumber,
            FaxNumber = source.FaxNumber,
            RequestingFacility = source.RequestingFacility,
            TransferDate = source.TransferDate,
            ReferringMd = source.ReferringMd,
            ReferringMdPhone = source.ReferringMdPhone,
            ReferringSpecialistPhone = source.ReferringSpecialistPhone,
            ReferringSpecialist = source.ReferringSpecialist,
            AdmitDate = source.AdmitDate,
            Unit = source.Unit,
            UnitPhone = source.UnitPhone,
            IsActive = source.IsActive,
            CreatedBy = source.CreatedBy,
            CreatedOn = source.CreatedOn,
            LastUpdatedOn = source.LastUpdatedOn
        };
    }
}