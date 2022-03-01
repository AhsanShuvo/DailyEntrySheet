using EntrySheet.Web.Component;
using EntrySheet.Web.Data;
using EntrySheet.Web.Interfaces;
using EntrySheet.Web.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace EntrySheet.Web.BaseClasses
{
    public class IndexBase : ComponentBase
    {
        public UserLog LogEntry { get; set; }
        public LogHistoryFilterModel LogHistory { get; set; }
        public LogHistoryGridview LogView { get; set; }
        public bool IsEditable { get; set; }
        [Inject]
        public IUserService UserService { get; set; }
        [Inject]
        public IProjectUserRepository ProjectUserRepository { get; set; }

        protected override void OnInitialized()
        {
            

            base.OnInitialized();
        }

        protected override async Task OnInitializedAsync()
        {
            LogEntry = new UserLog();
            LogHistory = new LogHistoryFilterModel();
            LogEntry.EntryDate = DateTime.Now;
            await PopulateLogHistoryFilter();
            IsEditable = true;
        }
        protected void SubmitLogEntry(bool response)
        {
            UpdateViewLogHistory();
        }

        protected void UpdateViewLogHistory()
        {
            LogView.Refresh();
            ClearModel();
        }

        protected void EditLogEntry(UserLog log)
        {
            LogEntry = log;
        }

        protected async Task PopulateLogHistoryFilter()
        {
            LogHistory.StartDate = DateTime.Now.AddDays(-7);
            LogHistory.EndDate = DateTime.Now;

            var claims = await UserService.GetUserClaims();
            var project = ProjectUserRepository.GetAssignedProject(claims[0].Value);
            if(project != null)
            {
                LogHistory.Project = project.ProjectRef.Name;
            }
            LogHistory.UserName = claims[1].Value;
        }

        protected void ClearModel()
        {
            LogEntry.Id = 0;
        }
    }
}
