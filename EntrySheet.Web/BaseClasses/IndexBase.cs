using EntrySheet.Domain.ViewModels;
using EntrySheet.Web.Component;
using Microsoft.AspNetCore.Components;
using System;

namespace EntrySheet.Web.BaseClasses
{
    public class IndexBase : ComponentBase
    {
        public LogEntryViewModel LogEntry { get; set; }
        public LogHistoryFilterModel LogHistory { get; set; }
        public LogHistoryGridview _logView { get; set; }
        protected override void OnInitialized()
        {
            LogEntry = new LogEntryViewModel();
            LogHistory = new LogHistoryFilterModel();
            LogEntry.EntryDate = DateTime.Now;
            LogHistory.StartDate = DateTime.Now.AddDays(-7);
            LogHistory.EndDate = DateTime.Now;
            LogHistory.Project = "New Project";
            LogHistory.UserName = "ahsan.habib";

            base.OnInitialized();
        }

        protected void SubmitLogEntry(LogEntryViewModel model)
        {
            var xx = model.Description;
        }

        protected void UpdateViewLogHistory()
        {
            _logView.Refresh();
        }
    }
}
