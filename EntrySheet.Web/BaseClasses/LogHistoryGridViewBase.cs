using EntrySheet.Domain.ViewModels;
using EntrySheet.Web.Data;
using EntrySheet.Web.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace EntrySheet.Web.BaseClasses
{
    public class LogHistoryGridViewBase : ComponentBase
    {
        [Parameter]
        public LogHistoryFilterModel logHistoryInfoViewModel { get; set; }
        public List<UserLog> Logs { get; set; }
        [Inject]
        public IUserLogRepository UserLogRepository { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Logs = new List<UserLog>();
            UpdateState();
        }

        public void Refresh()
        {
            UpdateState();
            StateHasChanged();
        }

        private void UpdateState()
        {
            Logs = UserLogRepository.GetUserLogs(logHistoryInfoViewModel);
        }
    }
}
