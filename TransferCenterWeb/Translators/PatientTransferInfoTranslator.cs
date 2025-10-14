namespace TransferCenterWeb.Translators
{ 
    public static class PatientTransferInfoTranslator
    {
        // Web → Core
        public static TransferCenterCore.Models.PatientTransferInfo ToCoreModel(this Models.PatientTransferInfo source, Guid guid)
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
                UnitPhone = source.UnitPhone
            };
        }

        // Core → Web
        public static Models.PatientTransferInfo ToWebModel(this TransferCenterCore.Models.PatientTransferInfo source)
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
                UnitPhone = source.UnitPhone
            };
        }
    }
}
