using System.Collections.Generic;
using SmartMed.Business.Models;
using SmartMed.Data.Repositories;

namespace SmartMed.Business.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository _repo = new CustomerRepository();

        public List<Customer> GetAll()
        {
            var list = new List<Customer>();
            foreach (var c in _repo.GetAll())
                list.Add(Customer.FromDataModel(c));
            return list;
        }

        public Customer GetById(int id) => Customer.FromDataModel(_repo.GetById(id));

        public void Update(Customer customer) => _repo.Update(customer.ToDataModel());
    }
}
