using EntrySheet.Domain.ViewModels;
using EntrySheet.Web.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace EntrySheet.Web.BaseClasses
{
    public class LogEntryFormBase : ComponentBase
    {
        [Parameter]
        public LogEntryViewModel LogEntry { get; set; }
        [Parameter]
        public EventCallback<LogEntryViewModel> OnSubmitLogEntry { get; set; }
        [Inject]
        public IUserLogRepository UserLogRepository { get; set; }

        protected async Task HandleValidSubmit()
        {
            await OnSubmitLogEntry.InvokeAsync(LogEntry);
        }
    }
}
