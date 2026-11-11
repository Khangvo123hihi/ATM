using ATM.Models;
using ATM.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ATM.Services
{
    public class AtmAuthenticationService
    {
        private readonly ApplicationDbContext _context;

        private const int MaxFailedAttempts = 3;

        public AtmAuthenticationService(
            ApplicationDbContext context)
        {
            _context = context;
        }

        // SHA256 số thẻ để tìm CardNumberHash
        private static string HashCardNumber(string cardNumber)
        {
            using var sha256 = SHA256.Create();

            byte[] bytes = Encoding.UTF8.GetBytes(cardNumber);

            byte[] hash = sha256.ComputeHash(bytes);

            return Convert.ToHexString(hash).ToLowerInvariant();
        }

        // Xác thực thẻ ATM và PIN
        public async Task<(bool Success, string Message, AtmCard? Card)>
            AuthenticateAsync(CardLoginViewModel model, string? ipAddress = null)
        {
            string cardHash = HashCardNumber(model.CardNumber.Trim());

            var card = await _context.ATMCards
                .Include(x => x.Account)
                .FirstOrDefaultAsync(x =>
                    x.CardNumberHash == cardHash);

            // Không tìm thấy thẻ
            if (card == null)
            {
                return (false, "Số thẻ hoặc mã PIN không chính xác.", null);
            }

            // Thẻ không hoạt động
            if (!card.IsActive)
            {
                return (false, "Thẻ ATM hiện không hoạt động.", null);
            }

            // Thẻ đã bị khóa
            if (card.IsLocked)
            {
                return (false, "Thẻ ATM đã bị khóa.", null);
            }

            // Thẻ hết hạn
            if (card.ExpiryDate.Date < DateTime.UtcNow.Date)
            {
                return (false, "Thẻ ATM đã hết hạn.", null);
            }
            if (card.Account == null)
                return (false, "Không tìm thấy tài khoản ngân hàng.", null);

            if (card.Account.AccountStatus != "Active")
                return (false, "Tài khoản ngân hàng hiện không hoạt động.", null);
            // Tài khoản không hoạt động
            if (card.Account.AccountStatus != "Active")
            {
                return (false, "Tài khoản ngân hàng không hoạt động.", null);
            }

            // Kiểm tra PIN bằng BCrypt
            bool isPinCorrect = BCrypt.Net.BCrypt.Verify(
                model.PIN,
                card.PinHash
            );

            // PIN sai
            if (!isPinCorrect)
            {
                card.FailedPinAttempts++;

                bool shouldLock =
                    card.FailedPinAttempts >= MaxFailedAttempts;

                if (shouldLock)
                {
                    card.IsLocked = true;
                    card.LockedAt = DateTime.UtcNow;
                }

                _context.CardPINAttempts.Add(new CardPinAttempt
                {
                    CardId = card.CardId,
                    IsSuccessful = false,
                    AttemptedAt = DateTime.UtcNow,
                    IpAddress = ipAddress,
                    FailureReason = shouldLock
                        ? "Sai PIN quá 3 lần. Thẻ đã bị khóa."
                        : "Mã PIN không chính xác."
                });

                await _context.SaveChangesAsync();

                if (shouldLock)
                {
                    return (false, "Bạn đã nhập sai PIN 3 lần. Thẻ đã bị khóa.", null);
                }

                return (
                    false,
                    $"Mã PIN không chính xác. Còn {MaxFailedAttempts - card.FailedPinAttempts} lần thử.",
                    null
                );
            }

            // PIN đúng: reset số lần sai
            card.FailedPinAttempts = 0;

            // Ghi nhận đăng nhập thành công
            _context.CardPINAttempts.Add(new CardPinAttempt
            {
                CardId = card.CardId,
                IsSuccessful = true,
                AttemptedAt = DateTime.UtcNow,
                IpAddress = ipAddress,
                FailureReason = null
            });

            await _context.SaveChangesAsync();

            return (true, "Xác thực thẻ ATM thành công.", card);
        }
    }
}