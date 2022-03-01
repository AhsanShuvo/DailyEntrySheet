using EntrySheet.Web.Data;
using EntrySheet.Web.Interfaces;
using EntrySheet.Web.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntrySheet.Web.BaseClasses
{
    public class LogHistoryGridViewBase : ComponentBase
    {
        [Parameter]
        public LogHistoryFilterModel logHistoryInfoViewModel { get; set; }
        public List<UserLog> Logs { get; set; }
        [Inject]
        public IUserLogRepository UserLogRepository { get; set; }
        [Parameter]
        public bool IsEditable { get; set; }
        public int SelectedLogId { get; set; }
        [Parameter]
        public EventCallback<UserLog> OnEditLog { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Logs = new List<UserLog>();
            UpdateState();
            SelectedLogId = 0;
        }

        public void Refresh()
        {
            UpdateState();
        }

        private void UpdateState()
        {
            Logs = UserLogRepository.GetUserLogs(logHistoryInfoViewModel);
        }

        protected async Task EditLog(int logId)
        {
            var log = Logs.Where(x => x.Id == logId).FirstOrDefault();
            await OnEditLog.InvokeAsync(log);
        }

        protected void RemoveLog(int logId)
        {
            var singleLog = Logs.Single(x => x.Id == logId);
            Logs.Remove(singleLog);
            UserLogRepository.RemoveUserLog(singleLog);
        }
    }
}
