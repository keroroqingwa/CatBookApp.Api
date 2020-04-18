using System;
using System.Collections.Generic;
using System.Text;

namespace CatBookApp.BookApiWhiteLists.Dto
{
    public class CreateBookApiWhiteListDto : BookApiWhiteListDto
    {
        public new int? Id => null;
    }
}
