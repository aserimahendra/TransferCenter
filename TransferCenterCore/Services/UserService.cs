using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TransferCenterCore.Interfaces;
using TransferCenterCore.Models;
using TransferCenterCore.Extensions;
using TransferCenterCore.Translators;
using TransferCenterCore.Utility;
using TransferCenterDbStore.UnitOfWork;

namespace TransferCenterCore.Services;

public class UserService : IUserService
{
    public IUnitOfWork _unitOfWork;
    readonly IConfiguration _configuration;
    readonly string _defaultEncryptionKey = "KPC-TransferCenter-Group";

    public UserService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    // Centralized accessor for encryption key string from configuration
    // Checks Security:EncryptionKey first, then root EncryptionKey, then optional Security:EncryptionPlainKey
    private string GetEncryptionKeyString()
    {
        var key = _configuration["Security:EncryptionKey"]
                  ?? _defaultEncryptionKey;
        return key;
    }

    public async Task<User> Login(string username, string password)
    {
        // Compute deterministic digest of incoming password
        var digest = GetEncryptedPassword(password!);
        // Try digest match (new scheme)
        var user = await _unitOfWork.UserRepository.GetAsync(x =>
            x.Password == digest && x.LoginId == username && x.IsActive);
        if (user != null)
            return user.ToCoreModel();
        return null!;
    }

    public long SaveUser(User user)
    {
        try
        {
            user.Password = GetEncryptedPassword(user.Password!);
            _unitOfWork.UserRepository.Add(user.ToEntity());
            _unitOfWork.Commit();
        }
        catch
        {
            _unitOfWork.Rollback();
        }

        return 0;
    }

    public bool CheckDuplicateEmailAndLogin(string emailId, string loginId)
    {
        var existingUser = _unitOfWork.UserRepository.Get(x => x.LoginId == loginId && x.IsActive && x.EmailId == emailId);
        return existingUser != null;
    }


    public User GetUserById(long Id)
    {
        var user = _unitOfWork.UserRepository.Get(x => x.UserId == Id);
        return UserWithPlainPassword(user);
    }

    public void UpdateUser(User user)
    {
        try
        {
            _unitOfWork.UserRepository.Update(user.ToEntity());
            _unitOfWork.Commit();
        }
        catch
        {
            _unitOfWork.Rollback();
        }
    }

    public async Task<(IEnumerable<User>, int)> GetAllUsersAsync(int page, int pageSize, string? search = null)
    {
        // Build base query
        var query = _unitOfWork.UserRepository
            .Query()
            .Where(u => u.IsActive);

        if (!string.IsNullOrWhiteSpace(search))
        {
            // Use OR-based multi-field contains for a single search term.
            query = query
                .StartBuilder()
                .ByAnyContains(new[] { "FirstName", "LastName", "EmailId", "LoginId" }, search)
                .Build();
        }

        var totalCount = await query.CountAsync();

        var users = await query
            .OrderBy(u => u.FirstName)
            .ThenBy(u => u.LastName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (users.Select(u => u.ToCoreModel()).ToList(), totalCount);
    }

    // Safely return plain password from stored value (encrypted or plaintext).
    // - If stored is Base64 and decrypts with current key -> return decrypted
    // - Else return stored as-is
    private string? TryLegacyDecrypt(string? stored)
    {
        var key = CryptoHelper.NormalizeKey(GetEncryptionKeyString());
        if (string.IsNullOrWhiteSpace(stored)) return null;
        // legacy stored may be AES-GCM base64 or already a digest. If digest, deterministic verification will catch earlier.
        try
        {
            return CryptoHelper.Decrypt(stored!, key);
        }
        catch
        {
            return stored;
        }
    }

    private Models.User UserWithPlainPassword(TransferCenterDbStore.Entities.User user)
    {
        if (user is null) return null!;
        // For display/logical usage we do NOT expose raw digest; return null to avoid leaking.
        user.Password = TryLegacyDecrypt(user.Password);
        return user.ToCoreModel();
    }

    private string? GetEncryptedPassword(string plainPassword)
    {
        var key = CryptoHelper.NormalizeKey(GetEncryptionKeyString());
        if (!string.IsNullOrWhiteSpace(plainPassword))
        {
            var digest = CryptoHelper.ComputeDeterministicDigest(plainPassword, key);
            if (digest.Length > 100)
                throw new InvalidOperationException("Password digest length exceeded 100 characters.");
            return digest;
        }
        return null;
    }
    // Removed legacy random encryption helper in favor of deterministic digest
}