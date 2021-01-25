using EntrySheet.Domain.ViewModels;
using Microsoft.AspNetCore.Components;
using System;

namespace EntrySheet.Web.BaseClasses
{
    public class IndexBase : ComponentBase
    {
        public LogEntryViewModel LogEntry { get; set; } = new LogEntryViewModel();
        protected override void OnInitialized()
        {
            LogEntry.Day = DateTime.Now.Day;
            LogEntry.Month = DateTime.Now.Month.ToString();
            LogEntry.Year = DateTime.Now.Year;

            base.OnInitialized();
        }

        protected void SubmitLogEntry(LogEntryViewModel model)
        {
            var xx = model.Description;
        }
    }
}
