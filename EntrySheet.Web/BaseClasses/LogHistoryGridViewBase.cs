using EntrySheet.Domain.ViewModels;
using Microsoft.AspNetCore.Components;

namespace EntrySheet.Web.BaseClasses
{
    public class LogHistoryGridViewBase : ComponentBase
    {
        [Parameter]
        public LogHistoryInfoViewModel logHistoryInfoViewModel { get; set; }
    }
}
