WITH Seed(n) AS (
    SELECT v.n FROM (VALUES
        (1),(2),(3),(4),(5),(6),(7),(8),(9),(10),(11),(12)
    ) AS v(n)
)
INSERT INTO dbo.ComorbiditiesAndRiskScores
(
    None, StrokeTIA, CHF, CKD_ESRD, HTN, DiabetesOrSkinIssues, MI_Angina_CAD, COPDOrRespiratoryFailure,
    Ventilated, PulmonaryHTN, PulmonaryHTN_Prostacyclin, ImmunocompromisedOrHIV, PsychBackground,
    NonCompliantWithCare, UnableToPerformADLs, DrugOrAlcoholDependence, LymphomaLeukemiaCancer,
    MalnutritionObesityDigestiveDisease, UnconsciousOrALOC, LOSMoreThan2Weeks, OutOfServiceArea,
    DNRCodeStatus, CovidPositive, RecentSurgeryAtUCI, RecentSurgeryOutsideUCI,
    TotalPoints, Comorbidities, RiskScore, CreatedDate, UpdatedDate, IsActive, CreatedBy, CreatedOn, LastUpdatedOn, UId
)
SELECT
    CAST((n % 2) AS bit), CAST(((n+1) % 2) AS bit), CAST(((n+2) % 2) AS bit), CAST(((n+3) % 2) AS bit),
    CAST(((n+4) % 2) AS bit), CAST(((n+5) % 2) AS bit), CAST(((n+6) % 2) AS bit), CAST(((n+7) % 2) AS bit),
    CAST(((n+8) % 2) AS bit), CAST(((n+9) % 2) AS bit), CAST(((n+10) % 2) AS bit), CAST(((n+11) % 2) AS bit),
    CAST(((n+12) % 2) AS bit), CAST(((n+13) % 2) AS bit), CAST(((n+14) % 2) AS bit), CAST(((n+15) % 2) AS bit),
    CAST(((n+16) % 2) AS bit), CAST(((n+17) % 2) AS bit), CAST(((n+18) % 2) AS bit), CAST(((n+19) % 2) AS bit),
    CAST(((n+20) % 2) AS bit), CAST(((n+21) % 2) AS bit), CAST(((n+22) % 2) AS bit), CAST(((n+23) % 2) AS bit),
    n * 2,
    CONCAT('HTN;DM;COPD #', n),
    1.0 + (n * 0.5),
    DATEADD(day, n, SYSUTCDATETIME()), DATEADD(day, n+1, SYSUTCDATETIME()),
    CAST(1 AS bit), 'seed', DATEADD(day, n, SYSUTCDATETIME()), NULL, NEWID()
FROM Seed;
GO