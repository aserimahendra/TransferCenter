using PatientTransferInfo = TransferCenterWeb.Models.PatientTransfer.PatientTransferInfo;

namespace TransferCenterWeb.Translators.PatientTransfer;

public static class PatientTransferInfoTranslator
{
    public static PatientTransferInfo ToWebModel(this TransferCenterCore.Models.PatientTransferInfo source)
    {
        if (source == null) return null!;

        return new PatientTransferInfo
        {
            Id = source.Id,
            UId = source.UId,
            CaseMgrSwRn = source.CaseMgrSwRn,
            TransferDate = source.TransferDate,
            MR =  source.MR ?? 0,
            PrimaryCallerName = source.PrimaryCallerName ?? string.Empty,
            PhoneNumber = source.PhoneNumber ?? string.Empty,
            FaxNumber = source.FaxNumber ?? string.Empty,
            SecondaryCallerName = source.SecondaryCallerName ?? string.Empty,
            SecondPhoneNumber = source.SecondPhoneNumber ?? string.Empty,
            SecondFaxNumber = source.SecondFaxNumber ?? string.Empty,
            RequestingFacility = source.RequestingFacility,
            IsHloc = source.IsHloc,
            ReferringMd = source.ReferringMd,
            ReferringMdPhone = source.ReferringMdPhone,
            ReferringSpecialist = source.ReferringSpecialist,
            ReferringSpecialistPhone = source.ReferringSpecialistPhone,
            AdmitDate = source.AdmitDate,
            Unit = source.Unit,
            UnitPhone = source.UnitPhone
        };
    }

    public static TransferCenterCore.Models.PatientTransferInfo ToCoreModel(this PatientTransferInfo source, Guid guid)
    {
        if (source == null) return new  TransferCenterCore.Models.PatientTransferInfo();

        return new TransferCenterCore.Models.PatientTransferInfo
        {
            Id = source.Id,
            UId = guid,
            CaseMgrSwRn = source.CaseMgrSwRn,
            TransferDate = source.TransferDate,
            MR = source.MR,
            PrimaryCallerName = source.PrimaryCallerName,
            PhoneNumber = source.PhoneNumber,
            FaxNumber = source.FaxNumber,
            SecondaryCallerName = source.SecondaryCallerName,
            SecondPhoneNumber = source.SecondPhoneNumber,
            SecondFaxNumber = source.SecondFaxNumber,
            RequestingFacility = source.RequestingFacility,
            IsHloc = source.IsHloc,
            ReferringMd = source.ReferringMd,
            ReferringMdPhone = source.ReferringMdPhone,
            ReferringSpecialist = source.ReferringSpecialist,
            ReferringSpecialistPhone = source.ReferringSpecialistPhone,
            AdmitDate = source.AdmitDate,
            Unit = source.Unit,
            UnitPhone = source.UnitPhone,
            TransferType = (short)Models.TransferType.PatientTransfer
        };
    }
}