using System.ComponentModel;

namespace TransferCenterWeb.Models;

public enum WeightIn
{
    Lbs,
    Kgs
}

public enum DocumentStatus
{
    Sent,
    Yes,
    [Description("N/A")]
    NA
}

public enum TransferType
{
    GlobalPatientTransfer = 1,
    PatientTransfer = 2
}

public enum RoleType : short
{
    Admin = 1,
    User = 2
}