using System.Web.Http;
using Entities;

namespace CustomerApi.Host.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomerController : ApiController
    {
        [HttpGet, Route("{id}")]
        public Customer Get(int id)
        {
            if (id > 0)
            {
                return new Customer
                {
                    Id = 1,
                    FullName = "Gökhan Gökalp"
                };
            }

            return null;
        }
    }
}