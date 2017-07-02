using System.Web.Http;

namespace mocking_with_moq.Controllers
{
    public class RootController : ApiController
    {
        // GET api/values
        public string Get()
        {
            return "API is ready to receive requests";
        }        
    }
}