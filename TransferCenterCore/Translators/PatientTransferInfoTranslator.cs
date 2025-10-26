namespace TransferCenterCore.Translators;

public static class PatientTransferInfoTranslator
{
    public static TransferCenterDbStore.Entities.PatientTransferInfo ToEntity(this Models.PatientTransferInfo source)
    {
        if (source == null) return null!;

        return new TransferCenterDbStore.Entities.PatientTransferInfo
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
            TransferType = source.TransferType,
        };
    }

    public static Models.PatientTransferInfo ToCoreModel(this TransferCenterDbStore.Entities.PatientTransferInfo source)
    {
        if (source == null) return null!;

        return new Models.PatientTransferInfo
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
            TransferType = source.TransferType,
        };
    }
}