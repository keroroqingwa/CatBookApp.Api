using CatBookApp.EntityFrameworkCore;

namespace CatBookApp.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly CatBookAppDbContext _context;

        public TestDataBuilder(CatBookAppDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            //create test data here...
        }
    }
}