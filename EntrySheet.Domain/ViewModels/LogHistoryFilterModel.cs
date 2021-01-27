using System;

namespace EntrySheet.Domain.ViewModels
{
    public class LogHistoryFilterModel
    {
        public string UserName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Project { get; set; }
    }
}
