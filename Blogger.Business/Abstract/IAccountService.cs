using Blogger.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogger.Business.Abstract
{
    public interface IAccountService
    {
        void Register(User user, string password);
        User Login(string userName, string password);
        bool VerifyPasswordHash(string password, byte[] userPasswordHash, byte[] userPasswordSalt);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool UserExistByUsername(string userName);
        bool UserExistByEmail(string email);
        string GenerateCode();
        void SendCodeWithEmail(string receiverEmail, string code, string description);
        bool ChangePassword(User user, string currentPassword, string newPassword);
    }
}
