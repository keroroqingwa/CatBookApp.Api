using System;
using System.Collections.Generic;
using System.Text;

namespace CatBookApp.BookReadRecords.Dto
{
    public class BookReadRecordOutput : BookReadRecordDto
    {
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
