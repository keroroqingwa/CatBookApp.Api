using System;
using System.Text;

namespace CatBookApp.BookUserPreferences.Dto
{
    public class BookUserPreferenceOutput : BookUserPreferenceDto
    {
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
