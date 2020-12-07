using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GUM.DAL.Security;
using GUM.DataModels;
using GUM.EntityDataModel;
using GUM.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace GUM.DAL
{
    public class UserRepository : IUserRepository
    {
        readonly GetUniqueMerchandiseEntities dbEntity = new GetUniqueMerchandiseEntities();

        #region Add New User
        public bool AddUser(User user)
        {
            try
            {
                //user.Password = SecurityProvider.Encrypt(user.Password);
                dbEntity.Users.Add(user);
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        #endregion

        #region Delete User
        public bool DeleteUser(long userID)
        {
            try
            {
                var user = dbEntity.Users.Find(userID);
                if (user == null)
                    return false;
                else
                {
                    dbEntity.Users.Remove(user);
                    dbEntity.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        #endregion

        #region Get All Users
        public IEnumerable<UserData> GetAllUsers()
        {
            try
            {
                var users = dbEntity.Users.ToList();
                if (users == null)
                    return null;
                else
                {
                    var result = new List<UserData>();
                    foreach(var user in users)
                    {
                        result.Add(new UserData
                        {
                            UserID = user.UserID,
                            FirstName = user.FirstName,
                            MiddleName = user.MiddleName,
                            LastName = user.LastName,
                            Email = user.Email,
                            Phone = user.Phone,
                            RoleName = dbEntity.Users.Find(user.UserID).Role.RoleName,
                            Password = user.Password
                        });
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        #endregion

        #region Get User by ID
        public UserData GetUserByID(long userID)
        {
            try
            {
                var user = dbEntity.Users.Find(userID);
                if (user == null)
                    return null;
                else
                {
                    return new UserData {
                        UserID = user.UserID,
                        FirstName = user.FirstName,
                        MiddleName = user.MiddleName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Phone=user.Phone,
                        RoleName=dbEntity.Users.Find(user.UserID).Role.RoleName,
                        Password=user.Password
                        }; 
                }
                    
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        #endregion

        #region Update Existing User
        public bool UpdateUser(User user)
        {
            try
            {
                var userFound = dbEntity.Users.Find(user.UserID);
                if (userFound == null)
                    return false;
                else
                {
                    userFound.FirstName = user.FirstName;
                    userFound.MiddleName = user.MiddleName;
                    userFound.LastName = user.LastName;
                    userFound.Password = user.Password;
                    //userFound.Password = SecurityProvider.Encrypt(user.Password);
                    userFound.Phone = user.Phone;
                    userFound.Email = user.Email;
                    dbEntity.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        #endregion

        #region Validate User
        public UserData ValidateUser(string email,string password)
        {
            try
            {
                foreach(var user in dbEntity.Users)
                {
                    if (user.Email == email && user.Password == password)//SecurityProvider.Encrypt(password))
                    {
                        return new UserData
                        {
                            UserID = user.UserID,
                            FirstName = user.FirstName,
                            MiddleName = user.MiddleName,
                            LastName = user.LastName,
                            Email = user.Email,
                            Phone = user.Phone,
                            RoleName = dbEntity.Users.Find(user.UserID).Role.RoleName,
                            Password = user.Password
                        };
                    }
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        #endregion

        #region Get Token
        public string GetToken(UserData user)
        {

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("GetUniqueMerchandise@07"));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:44388",
                audience:"https://localhost:4200",
                //audience: "http://192.168.0.102:4200",
                claims: new[] {
                        new Claim("UserID",user.UserID.ToString()),
                        new Claim("Name", user.FirstName + " " + user.LastName),
                        new Claim("Email",user.Email),
                        new Claim("Role",user.RoleName),
                        new Claim("Phone",user.Phone),
                        new Claim(ClaimTypes.Role,user.RoleName)
                },
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signingCredentials
                );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;
        }
        #endregion
    }
}
