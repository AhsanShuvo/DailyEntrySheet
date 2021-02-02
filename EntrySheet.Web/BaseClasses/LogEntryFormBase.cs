using EntrySheet.Web.Data;
using EntrySheet.Web.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace EntrySheet.Web.BaseClasses
{
    public class LogEntryFormBase : ComponentBase
    {
        [Parameter]
        public UserLog LogEntry { get; set; }
        [Parameter]
        public EventCallback<bool> OnSubmitLogEntry { get; set; }
        [Inject]
        public IUserLogRepository UserLogRepository { get; set; }
        [Inject]
        public IUserService UserService { get; set; }
        [Inject]
        public IProjectUserRepository ProjectUserRepository { get; set; }

        protected async Task HandleValidSubmit()
        {
            await FillLogEntry();
            await OnSubmitLogEntry.InvokeAsync(true);
        }

        protected async Task FillLogEntry()
        {
            var response = await UserService.GetUserClaims();
            var projectUserRef = ProjectUserRepository.GetAssignedProject(response[0].Value.ToString());
            if(projectUserRef != null && projectUserRef.ProjectRef != null)
            {
                LogEntry.ProjectRef = projectUserRef.ProjectRef;
                LogEntry.UserRef = projectUserRef.UserRef;
                if(LogEntry.Id > 0)
                {
                    UserLogRepository.UpdateUserLog(LogEntry);
                }
                else
                {
                    UserLogRepository.AddUserLog(LogEntry);
                }
            }
            else
            {
            }
        }
    }
}
