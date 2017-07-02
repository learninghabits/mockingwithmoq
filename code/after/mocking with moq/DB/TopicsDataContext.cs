using System.Data.Entity;

namespace mocking_with_moq.DB
{
    public class TopicsDataContext : DbContext, ITopicsDataContext
    {
        public IDbSet<Topic> Topics { get; set; }
        public IDbSet<Tutorial> Tutorials { get; set; }
    }
}