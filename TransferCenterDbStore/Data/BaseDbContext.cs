using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferCenterDbStore.Entities;

namespace TransferCenterDbStore.Data;

public class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options) { }
    public DbSet<User> User { get; set; }
    public DbSet<AdditionalInfo> AdditionalInfo { get; set; }
    public DbSet<PatientDetails> PatientDetails { get; set; }
    public DbSet<PatientTransferInfo> PatientTransferInfo { get; set; }
    public DbSet<AuditLog> AuditLog { get; set; }
    public DbSet<ComorbiditiesAndRiskScore> ComorbiditiesAndRiskScores { get; set; }
}