using System;
using System.Collections.Generic;
using System.Text;

namespace CatBookApp.BookUsers.Dto
{
    public class BookUserOutput : BookUserDto
    {
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
