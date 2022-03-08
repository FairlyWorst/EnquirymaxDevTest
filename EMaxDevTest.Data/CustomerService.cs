namespace EMaxDevTest.Data
{
    using EMaxDevTest.Core;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    public class CustomerService
    { 
        private EmaxContext _context;

        public CustomerService(EmaxContext context)
        {
            _context = context;
        }
        public void CreateCustomer(Customer customer)
        {  
            if (customer.CustomerId == Guid.Empty)
                customer.CustomerId = Guid.NewGuid();

            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public Customer GetCustomerById(Guid customerId)
        {            
            var customer = _context.Customers
                .FirstOrDefault(c => c.CustomerId == customerId);

            return customer ?? null;
        }

        public IEnumerable<Customer> GetAllCustomers() => _context.Customers.Any() ? _context.Customers : null;


        public IEnumerable<Customer> GetCustomerBySearchCriteria(SearchCriteria criteria)
        {
            var customers = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(criteria.Forename))
                customers = customers.Where(c => c.Forename.ToLower().Contains(criteria.Forename.ToLower()));

            if (!string.IsNullOrEmpty(criteria.Surname))
                customers = customers.Where(c => c.Surname.ToLower().Contains(criteria.Surname.ToLower()));

            if (!string.IsNullOrEmpty(criteria.PostCode))
                customers = customers.Where(c => c.Address.PostCode.ToLower().Replace(" ", "").Contains(criteria.PostCode.ToLower().Replace(" ","")));

            if (!string.IsNullOrEmpty(criteria.EmailAddress))
                customers = customers.Where(c => c.ContactInformation.Any(v => v.Value.ToLower().Contains(criteria.EmailAddress.ToLower()) && v.Type == ContactType.EmailAddress));

            return customers.ToList();            
        }

        public void UpdateCustomer(Customer newCustomer)
        {
            _context.Customers.Attach(newCustomer);
            _context.Entry(newCustomer).State = EntityState.Modified;
            _context.SaveChanges();

        }

        public void DeleteCustomer(Guid customerId)
        {
            _context.Customers.Remove(this.GetCustomerById(customerId));
            _context.SaveChanges();
        }
    }
}