using EntrySheet.Domain.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace EntrySheet.Web.BaseClasses
{
    public class LogEntryFormBase : ComponentBase
    {
        [Parameter]
        public LogEntryViewModel LogEntry { get; set; }
        protected int DaysOfMonth { get; set; }
        [Parameter]
        public EventCallback<LogEntryViewModel> OnSubmitLogEntry { get; set; }

        protected override void OnInitialized()
        {
            DaysOfMonth = DateTime.DaysInMonth(LogEntry.Year, DateTime.Now.Month) + 1;

            base.OnInitialized();
        }

        protected async Task HandleValidSubmit()
        {
            await OnSubmitLogEntry.InvokeAsync(LogEntry);
        }
    }
}
