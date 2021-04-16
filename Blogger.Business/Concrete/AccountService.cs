using Blogger.Business.Abstract;
using Blogger.Entities;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Blogger.Business.Concrete
{
    public class AccountService : IAccountService
    {
        private IUserService _userService;
        public AccountService(IUserService userService)
        {
            _userService = userService;
        }

        public bool ChangePassword(User user, string currentPassword, string newPassword)
        {
            if (!VerifyPasswordHash(currentPassword, user.PasswordHash, user.PasswordSalt))
            {
                return false;
            }

            // User security information
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            // Update user
            _userService.Update(user);

            return true;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public string GenerateCode()
        {
            string characters = "ABCDEFGHIJKLMNOPRSTUVYZX123456789";
            string code = "";
            
            Random rand = new Random();

            for (int i = 0; i < 6; i++)
            {
                var index = rand.Next(0, characters.Length - 1);
                code += characters[index];
            }

            return code;
        }

        public User Login(string userName, string password)
        {
            var user = _userService.GetUserByUsername(userName);

            if (user == null)
            {
                return null;

            }
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;

            }
            return user;
        }

        public void Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.RoleId = 3; //MemberRole
            user.IsEmailConfirm = false;

            _userService.Add(user);
        }

        public void SendCodeWithEmail(string receiverEmail, string code, string description)
        {
            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("","");
            client.Port = 587;
            client.Host = "mail.genclerinblogu.com";
            client.EnableSsl = false;
            message.To.Add(receiverEmail);
            message.From = new MailAddress("");
            message.Subject = "Gençlerin Blogu";
            message.Body = description + "\n" + "Code : " + code;
            client.Send(message);
        }

        public bool UserExistByEmail(string email)
        {
            var user = _userService.GetUserByEmail(email);

            return user != null ? true : false;
        }

        public bool UserExistByUsername(string userName)
        {
            var user = _userService.GetUserByUsername(userName);

            return user != null ? true : false;
        }

        public bool VerifyPasswordHash(string password, byte[] userPasswordHash, byte[] userPasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(userPasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != userPasswordHash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
