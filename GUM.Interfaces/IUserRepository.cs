using GUM.DataModels;
using GUM.EntityDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUM.Interfaces
{
    public interface IUserRepository
    {
        bool AddUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(long userID);
        UserData GetUserByID(long userID);
        IEnumerable<UserData> GetAllUsers();
        UserData ValidateUser(string email, string password);
        string GetToken(UserData user);
    }
}
