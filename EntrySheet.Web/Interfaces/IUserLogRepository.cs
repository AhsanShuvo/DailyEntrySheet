using EntrySheet.Domain.ViewModels;
using EntrySheet.Web.Data;
using System.Collections.Generic;

namespace EntrySheet.Web.Interfaces
{
    public interface IUserLogRepository
    {
        bool AddUserLog(UserLog logDetails);
        List<UserLog> GetUserLogs(LogHistoryFilterModel model);
        void RemoveUserLog(UserLog logDetails);
        bool UpdateUserLog(UserLog logDetails);
    }
}
