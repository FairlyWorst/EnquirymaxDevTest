namespace EnquirymaxDevTest.Controllers
{
    using EMaxDevTest.Core;
    using EMaxDevTest.Data;
    using System.Web.Http;

    [Route("api/[controller]")]
    public class CustomerController : ApiController
    {
        private readonly CustomerService customerService;

        public CustomerController(CustomerService service)
        {
            this.customerService = service;
        }

        [HttpPost]
        public IHttpActionResult CreateCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.CustomerId = Guid.NewGuid();
                customerService.CreateCustomer(customer);
                return Ok(customer);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IHttpActionResult GetCustomerById(Guid customerid) => Ok(customerService.GetCustomerById(customerid));

        [HttpGet]
        public IHttpActionResult GetCustomerBySearchCriteria(SearchCriteria searchCriteria) => Ok(customerService.GetCustomerBySearchCriteria(searchCriteria));

        [HttpPut]
        public IHttpActionResult UpdateCustomer(Customer newCustomer)
        {
            if (ModelState.IsValid)
            {
                customerService.UpdateCustomer(newCustomer);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteCustomer(string customerId)
        {
            customerService.DeleteCustomer(Guid.Parse(customerId));
            return Ok();
        }
    }
}
