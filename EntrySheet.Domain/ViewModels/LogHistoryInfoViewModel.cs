using System;

namespace EntrySheet.Domain.ViewModels
{
    public class LogHistoryInfoViewModel
    {
        public string UserName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Project { get; set; }
    }
}
