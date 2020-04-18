using System;
using System.Collections.Generic;
using System.Text;

namespace CatBookApp.BookApiWhiteLists.Dto
{
    public class BookApiWhiteListOutput: BookApiWhiteListDto
    {
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
