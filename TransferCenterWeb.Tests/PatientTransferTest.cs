using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using TransferCenterCore.Interfaces;
using TransferCenterHelper;
using TransferCenterWeb.Controllers;
using TransferCenterWeb.Models;
using TransferCenterWeb.Translators;


using DocumentStatus = TransferCenterWeb.Models.PatientTransfer.DocumentStatus;
using CorePatientTransferRequest = TransferCenterCore.Models.PatientTransferRequest;
using CorePatientTransferInfo = TransferCenterCore.Models.PatientTransferInfo;
using CorePatientDetails = TransferCenterCore.Models.PatientDetails;
using CoreAdditionalInfo = TransferCenterCore.Models.AdditionalInfo;
using WebPatientTransferRequest = TransferCenterWeb.Models.PatientTransferRequest;
using WebPatientTransferInfo = TransferCenterWeb.Models.PatientTransfer.PatientTransferInfo;
using WebPatientDetails = TransferCenterWeb.Models.PatientTransfer.PatientDetails;
using WebAdditionalInfo = TransferCenterWeb.Models.PatientTransfer.AdditionalInfo;

namespace TransferCenterWeb.Tests;

public class PatientTransferControllerTests
{
    [Fact]
    public async Task Create_ShouldPersistPatientTransferRequest()
    {
        var service = new FakePatientTransferService();
        var pdfExporter = new FakePdfExporter();
        var tempDataProvider = new FakeTempDataProvider();
        var viewRenderService = new FakeViewRenderService();

        var controller = new PatientTransferController(service, pdfExporter, tempDataProvider, viewRenderService)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

    var request = BuildValidPatientTransferRequest();

        var actionResult = await controller.Create(request);

        var partialView = Assert.IsType<PartialViewResult>(actionResult);
        Assert.Equal(Constant.ViewPath.ModalActionResult, partialView.ViewName);

        var persistedCore = await service.Get(request.Id);
        Assert.NotNull(persistedCore);

        var persisted = persistedCore.ToWebModel();

        Assert.Equal(request.Id, persisted.Id);
        AssertPatientTransferInfo(request.PatientTransferInfo, persisted.PatientTransferInfo);
        AssertPatientDetails(request.PatientDetails, persisted.PatientDetails);
        AssertAdditionalInfo(request.AdditionalInfo, persisted.AdditionalInfo);
    }

    private static WebPatientTransferRequest BuildValidPatientTransferRequest()
    {
        var now = new DateTime(2025, 10, 29, 10, 30, 0, DateTimeKind.Utc);
        return new WebPatientTransferRequest
        {
            PatientTransferInfo = new WebPatientTransferInfo
            {
                CaseMgrSwRn = "Case Manager",
                TransferDate = now,
                MR = 987654,
                PrimaryCallerName = "Alice Primary",
                PhoneNumber = "555-0100",
                FaxNumber = "555-0199",
                SecondaryCallerName = "Bob Secondary",
                SecondPhoneNumber = "555-0123",
                SecondFaxNumber = "555-0456",
                RequestingFacility = "General Hospital",
                IsHloc = true,
                ReferringMd = "Dr. Smith",
                ReferringMdPhone = "555-0666",
                ReferringSpecialist = "Dr. Jones",
                ReferringSpecialistPhone = "555-0777",
                AdmitDate = now.Date.AddDays(-1),
                Unit = "ICU-W",
                UnitPhone = "555-0888",
                IsActive = true
            },
            PatientDetails = new WebPatientDetails
            {
                FirstName = "Jane",
                LastName = "Doe",
                DOB = new DateTime(1990, 5, 1),
                Gender = 1,
                Height = 165.5,
                Weight = 60.2,
                WeightIn = (short)WeightIn.Kgs,
                Diagnosis = "Acute condition",
                ReasonForTransfer = "Requires specialized care",
                LevelOfCareNeeded = "ICU",
                Sitter = true,
                JehovahWitness = false,
                Capitated = false,
                IsIsolation = true,
                IsolationType = "Airborne",
                CodeStatus = "Full Code",
                GCS = "15",
                IsActive = true
            },
            AdditionalInfo = new WebAdditionalInfo
            {
                Dialysis = true,
                VTIBDrips = true,
                ServicesAvailable = false,
                LifeImageUploadRequested = DocumentStatus.Sent,
                PFCTTransfer = true,
                SendingFacilityUnableToUseLifeImage = DocumentStatus.Yes,
                ColdOrFluSymptoms = true,
                NewRashUnknownCause = true,
                ContactWithCovidPositive = true,
                DiagnosedCovidOrPositiveLab = true,
                CovidDiagnosisDates = new DateTime(2025, 10, 20),
                SickHouseholdMembers = true,
                ExposedToMeasles = true,
                TraveledOutsideUS = true,
                TraveledArabianPeninsula = true,
                TraveledAfrica = true,
                HasRespiratoryIllnessAfterTravel = true,
                AdmittedToKindredHospital = true,
                MultiDrugResistantInfection = true,
                Microorganisms = "MRSA",
                CommunicableDisease = true,
                DiseaseConditions = "None",
                LabResultsStatus = DocumentStatus.Yes,
                DiagnosticsStatus = DocumentStatus.NA,
                MedicationListStatus = DocumentStatus.Yes,
                FaceSheet = DocumentStatus.Sent,
                HAndP = DocumentStatus.Yes,
                CovidWithin3Days = true,
                MostRecentLabResults = DocumentStatus.Yes,
                RadiologyResults = DocumentStatus.Sent,
                MedicationList = DocumentStatus.Yes,
                IsActive = true
            }
        };
    }

    private static void AssertPatientTransferInfo(WebPatientTransferInfo expected, WebPatientTransferInfo actual)
    {
        Assert.NotNull(actual);
        Assert.Equal(expected.CaseMgrSwRn, actual.CaseMgrSwRn);
        Assert.Equal(expected.TransferDate, actual.TransferDate);
        Assert.Equal(expected.MR, actual.MR);
        Assert.Equal(expected.PrimaryCallerName, actual.PrimaryCallerName);
        Assert.Equal(expected.PhoneNumber, actual.PhoneNumber);
        Assert.Equal(expected.FaxNumber, actual.FaxNumber);
        Assert.Equal(expected.SecondaryCallerName, actual.SecondaryCallerName);
        Assert.Equal(expected.SecondPhoneNumber, actual.SecondPhoneNumber);
        Assert.Equal(expected.SecondFaxNumber, actual.SecondFaxNumber);
        Assert.Equal(expected.RequestingFacility, actual.RequestingFacility);
        Assert.Equal(expected.IsHloc, actual.IsHloc);
        Assert.Equal(expected.ReferringMd, actual.ReferringMd);
        Assert.Equal(expected.ReferringMdPhone, actual.ReferringMdPhone);
        Assert.Equal(expected.ReferringSpecialist, actual.ReferringSpecialist);
        Assert.Equal(expected.ReferringSpecialistPhone, actual.ReferringSpecialistPhone);
        Assert.Equal(expected.AdmitDate, actual.AdmitDate);
        Assert.Equal(expected.Unit, actual.Unit);
        Assert.Equal(expected.UnitPhone, actual.UnitPhone);
    }

    private static void AssertPatientDetails(WebPatientDetails expected, WebPatientDetails actual)
    {
        Assert.NotNull(actual);
        Assert.Equal(expected.FirstName, actual.FirstName);
        Assert.Equal(expected.LastName, actual.LastName);
        Assert.Equal(expected.DOB, actual.DOB);
        Assert.Equal(expected.Gender, actual.Gender);
        Assert.Equal(expected.Height, actual.Height);
        Assert.Equal(expected.Weight, actual.Weight);
        Assert.Equal(expected.WeightIn, actual.WeightIn);
        Assert.Equal(expected.Diagnosis, actual.Diagnosis);
        Assert.Equal(expected.ReasonForTransfer, actual.ReasonForTransfer);
        Assert.Equal(expected.LevelOfCareNeeded, actual.LevelOfCareNeeded);
        Assert.Equal(expected.Sitter, actual.Sitter);
        Assert.Equal(expected.JehovahWitness, actual.JehovahWitness);
        Assert.Equal(expected.Capitated, actual.Capitated);
        Assert.Equal(expected.IsIsolation, actual.IsIsolation);
        Assert.Equal(expected.IsolationType, actual.IsolationType);
        Assert.Equal(expected.CodeStatus, actual.CodeStatus);
        Assert.Equal(expected.GCS, actual.GCS);
    }

    private static void AssertAdditionalInfo(WebAdditionalInfo expected, WebAdditionalInfo actual)
    {
        Assert.NotNull(actual);
        Assert.Equal(expected.Dialysis, actual.Dialysis);
        Assert.Equal(expected.VTIBDrips, actual.VTIBDrips);
        Assert.Equal(expected.ServicesAvailable, actual.ServicesAvailable);
        Assert.Equal(expected.LifeImageUploadRequested, actual.LifeImageUploadRequested);
        Assert.Equal(expected.PFCTTransfer, actual.PFCTTransfer);
        Assert.Equal(expected.SendingFacilityUnableToUseLifeImage, actual.SendingFacilityUnableToUseLifeImage);
        Assert.Equal(expected.ColdOrFluSymptoms, actual.ColdOrFluSymptoms);
        Assert.Equal(expected.NewRashUnknownCause, actual.NewRashUnknownCause);
        Assert.Equal(expected.ContactWithCovidPositive, actual.ContactWithCovidPositive);
        Assert.Equal(expected.DiagnosedCovidOrPositiveLab, actual.DiagnosedCovidOrPositiveLab);
        Assert.Equal(expected.CovidDiagnosisDates, actual.CovidDiagnosisDates);
        Assert.Equal(expected.SickHouseholdMembers, actual.SickHouseholdMembers);
        Assert.Equal(expected.ExposedToMeasles, actual.ExposedToMeasles);
        Assert.Equal(expected.TraveledOutsideUS, actual.TraveledOutsideUS);
        Assert.Equal(expected.TraveledArabianPeninsula, actual.TraveledArabianPeninsula);
        Assert.Equal(expected.TraveledAfrica, actual.TraveledAfrica);
        Assert.Equal(expected.HasRespiratoryIllnessAfterTravel, actual.HasRespiratoryIllnessAfterTravel);
        Assert.Equal(expected.AdmittedToKindredHospital, actual.AdmittedToKindredHospital);
        Assert.Equal(expected.MultiDrugResistantInfection, actual.MultiDrugResistantInfection);
        Assert.Equal(expected.Microorganisms, actual.Microorganisms);
        Assert.Equal(expected.CommunicableDisease, actual.CommunicableDisease);
        Assert.Equal(expected.DiseaseConditions, actual.DiseaseConditions);
        Assert.Equal(expected.LabResultsStatus, actual.LabResultsStatus);
        Assert.Equal(expected.DiagnosticsStatus, actual.DiagnosticsStatus);
        Assert.Equal(expected.MedicationListStatus, actual.MedicationListStatus);
        Assert.Equal(expected.FaceSheet, actual.FaceSheet);
        Assert.Equal(expected.HAndP, actual.HAndP);
        Assert.Equal(expected.CovidWithin3Days, actual.CovidWithin3Days);
        Assert.Equal(expected.MostRecentLabResults, actual.MostRecentLabResults);
        Assert.Equal(expected.RadiologyResults, actual.RadiologyResults);
        Assert.Equal(expected.MedicationList, actual.MedicationList);
    }

    private sealed class FakePdfExporter : IPdfExporter
    {
        public string? LastHtml { get; private set; }
        public string? LastTitle { get; private set; }

        public byte[] ConvertHtmlToPdf(string htmlContent, string? title = null, string? baseUrl = null)
        {
            LastHtml = htmlContent;
            LastTitle = title;
            return Encoding.UTF8.GetBytes(htmlContent ?? string.Empty);
        }

        public void ConvertHtmlToPdfFile(string htmlContent, string outputPath, string? title = null)
        {
            ConvertHtmlToPdf(htmlContent, title, null);
        }
    }

    private sealed class FakeTempDataProvider : ITempDataProvider
    {
        private IDictionary<string, object> _store = new Dictionary<string, object>();

        public IDictionary<string, object> LoadTempData(HttpContext context)
        {
            return new Dictionary<string, object>(_store);
        }

        public void SaveTempData(HttpContext context, IDictionary<string, object> values)
        {
            _store = values != null
                ? new Dictionary<string, object>(values)
                : new Dictionary<string, object>();
        }
    }

    private sealed class FakeViewRenderService : IViewRenderService
    {
        public Task<string> RenderToStringAsync(ActionContext actionContext, string viewName, object model, bool isPartial = false)
        {
            return Task.FromResult($"<html data-view='{viewName}'></html>");
        }
    }

    private sealed class FakePatientTransferService : IPatientTransferService
    {
        private readonly ConcurrentDictionary<Guid, CorePatientTransferRequest> _store = new();

        public Task Save(CorePatientTransferRequest patientTransferViewModel)
        {
            var key = ResolveKey(patientTransferViewModel);
            patientTransferViewModel.Id = key;
            _store[key] = Clone(patientTransferViewModel);
            return Task.CompletedTask;
        }

        public Task<CorePatientTransferRequest> Get(Guid uid)
        {
            _store.TryGetValue(uid, out var stored);
            return Task.FromResult(stored != null ? Clone(stored) : null);
        }

        public Task<(IEnumerable<CorePatientTransferRequest> Items, int TotalCount)> GetList(int page, int pageSize)
        {
            var items = _store.Values.Select(Clone).ToList();
            return Task.FromResult<(IEnumerable<CorePatientTransferRequest>, int)>((items, items.Count));
        }

        public Task Update(CorePatientTransferRequest patientTransferViewModel)
        {
            var key = ResolveKey(patientTransferViewModel);
            if (_store.ContainsKey(key))
            {
                _store[key] = Clone(patientTransferViewModel);
            }

            return Task.CompletedTask;
        }

        public Task Delete(CorePatientTransferRequest patientTransferViewModel)
        {
            var key = ResolveKey(patientTransferViewModel);
            _store.TryRemove(key, out _);
            return Task.CompletedTask;
        }

        private static CorePatientTransferRequest Clone(CorePatientTransferRequest source)
        {
            return new CorePatientTransferRequest
            {
                Id = source.Id,
                PatientTransferInfo = Clone(source.PatientTransferInfo),
                PatientDetails = Clone(source.PatientDetails),
                AdditionalInfo = Clone(source.AdditionalInfo),
                ComorbiditiesAndRiskScore = source.ComorbiditiesAndRiskScore
            };
        }

        private static CorePatientTransferInfo? Clone(CorePatientTransferInfo? source)
        {
            if (source == null) return null;

            return new CorePatientTransferInfo
            {
                Id = source.Id,
                UId = source.UId,
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
                TransferType = source.TransferType,
                IsActive = source.IsActive,
                CreatedBy = source.CreatedBy,
                CreatedOn = source.CreatedOn,
                LastUpdatedOn = source.LastUpdatedOn
            };
        }

        private static CorePatientDetails? Clone(CorePatientDetails? source)
        {
            if (source == null) return null;

            return new CorePatientDetails
            {
                Id = source.Id,
                UId = source.UId,
                Name = source.Name,
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
                GCS = source.GCS,
                PatientInsurance = source.PatientInsurance,
                AcceptingPhysician = source.AcceptingPhysician,
                Lateral = source.Lateral,
                HLOC = source.HLOC,
                WeightIn = source.WeightIn,
                IsActive = source.IsActive,
                TransferType = source.TransferType,
                CreatedBy = source.CreatedBy,
                CreatedOn = source.CreatedOn,
                LastUpdatedOn = source.LastUpdatedOn
            };
        }

        private static CoreAdditionalInfo? Clone(CoreAdditionalInfo? source)
        {
            if (source == null) return null;

            return new CoreAdditionalInfo
            {
                Id = source.Id,
                UId = source.UId,
                TransferType = source.TransferType,
                ServicesAvailable = source.ServicesAvailable,
                SitterRequired = source.SitterRequired,
                VTIBDrips = source.VTIBDrips,
                Dialysis = source.Dialysis,
                PFCTTransfer = source.PFCTTransfer,
                CovidWithin3Days = source.CovidWithin3Days,
                FaceSheet = source.FaceSheet,
                HAndP = source.HAndP,
                CovidTestResults = source.CovidTestResults,
                TransferOrder = source.TransferOrder,
                ProgressNotes = source.ProgressNotes,
                ConsultationNotes = source.ConsultationNotes,
                MostRecentLabResults = source.MostRecentLabResults,
                RadiologyResults = source.RadiologyResults,
                MedicationList = source.MedicationList,
                TreatmentsAndProceduresInED = source.TreatmentsAndProceduresInED,
                InsuranceAuthorization = source.InsuranceAuthorization,
                OtherNotes = source.OtherNotes,
                CodeStatus = source.CodeStatus,
                LifeImageUploadRequested = source.LifeImageUploadRequested,
                SendingFacilityUnableToUseLifeImage = source.SendingFacilityUnableToUseLifeImage,
                ColdOrFluSymptoms = source.ColdOrFluSymptoms,
                NewRashUnknownCause = source.NewRashUnknownCause,
                ContactWithCovidPositive = source.ContactWithCovidPositive,
                DiagnosedCovidOrPositiveLab = source.DiagnosedCovidOrPositiveLab,
                CovidDiagnosisDates = source.CovidDiagnosisDates,
                SickHouseholdMembers = source.SickHouseholdMembers,
                ExposedToMeasles = source.ExposedToMeasles,
                TraveledOutsideUS = source.TraveledOutsideUS,
                TraveledArabianPeninsula = source.TraveledArabianPeninsula,
                TraveledAfrica = source.TraveledAfrica,
                HasRespiratoryIllnessAfterTravel = source.HasRespiratoryIllnessAfterTravel,
                AdmittedToKindredHospital = source.AdmittedToKindredHospital,
                MultiDrugResistantInfection = source.MultiDrugResistantInfection,
                Microorganisms = source.Microorganisms,
                CommunicableDisease = source.CommunicableDisease,
                DiseaseConditions = source.DiseaseConditions,
                LabResultsStatus = source.LabResultsStatus,
                DiagnosticsStatus = source.DiagnosticsStatus,
                MedicationListStatus = source.MedicationListStatus,
                IsActive = source.IsActive,
                CreatedBy = source.CreatedBy,
                CreatedOn = source.CreatedOn,
                LastUpdatedOn = source.LastUpdatedOn
            };
        }

        private static Guid ResolveKey(CorePatientTransferRequest patientTransferViewModel)
        {
            return patientTransferViewModel.PatientTransferInfo?.UId
                   ?? patientTransferViewModel.PatientDetails?.UId
                   ?? patientTransferViewModel.AdditionalInfo?.UId
                   ?? patientTransferViewModel.Id;
        }
    }
}
