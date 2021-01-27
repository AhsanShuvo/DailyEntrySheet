using System;

namespace EntrySheet.Domain.ViewModels
{
    public class LogEntryViewModel
    {
        public DateTime EntryDate { get; set; }
        public string Description { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
    }
}
