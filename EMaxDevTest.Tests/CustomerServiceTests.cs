using EMaxDevTest.Core;
using EMaxDevTest.Data;
using EMaxHttpClient;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace EMaxDevTest.Tests
{
    public class Tests
    {
        private CustomerService _customerService;
        private Customer _customer;
        private Customer _customer2;
        [SetUp]
        public void Setup()
        {
            MyHttpClient.client.BaseAddress = new Uri("https://localhost:5000");
            MyHttpClient.client.DefaultRequestHeaders.Accept.Clear();
            MyHttpClient.client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var options = new DbContextOptionsBuilder<EmaxContext>()
                .UseInMemoryDatabase("MockDB")
                .Options;
            var context = new EmaxContext(options);

            _customerService = new CustomerService(context);
            _customer = new Customer
            {
                CustomerId = Guid.NewGuid(),
                Forename = "Morgan",
                Surname = "Fairhurst",
                DateOfBirth = System.DateTime.Parse("5/5/20"),
                Address = new Address
                {
                    HouseNo = 30,
                    Street = "Hallam Road",
                    City = "New Ollerton",
                    PostCode = "NG22 9TL"
                },
                ContactInformation = new List<ContactInformation>()
                {
                    new ContactInformation{ Type = ContactType.EmailAddress, Value = "morgan_winters@hotmail.com"},
                    new ContactInformation{ Type = ContactType.Telephone, Value = "07794476206"}
                }
            };

            _customer2 = new Customer
            {
                CustomerId = Guid.NewGuid(),
                Forename = "Naomi",
                Surname = "Fairhurst",
                DateOfBirth = System.DateTime.Parse("5/5/20"),
                Address = new Address
                {
                    HouseNo = 30,
                    Street = "Hallam Road",
                    City = "New Ollerton",
                    PostCode = "NG22 9TL"
                },
                ContactInformation = new List<ContactInformation>()
                {
                    new ContactInformation{ Type = ContactType.EmailAddress, Value = "naomirevill@gmail.com"},
                    new ContactInformation{ Type = ContactType.Telephone, Value = "07794476206"}
                }
            };

            MyHttpClient.RunAsync(_customer).GetAwaiter().GetResult();
        }

        [Test]
        public void CreateTest()
        {
            var url = MyHttpClient.CreateResult;

            url.Should().NotBeNull();
            url.Should().NotBe("");
            _customerService.GetAllCustomers().Should().HaveCountGreaterThan(0);
        }

        [Test]
        public async void GetByIdTest()
        {
            var customer = MyHttpClient.GetByIdResult;
            
            customer.Should().BeEquivalentTo(_customer);
        }

        [Test]
        public async void GetByCriteriaTest()
        {
            var searchCriteria = new SearchCriteria
            {
                Forename = "Morgan",
                Surname = "fairhurst",
                EmailAddress = "morgan_winters@hotmail.com",
                PostCode = "ng229tl"
            };

            _customerService.CreateCustomer(_customer2);

            var results = MyHttpClient.GetByCriteriaResult;
            results.Should().HaveCount(1);
            results.First().Should().BeEquivalentTo(_customer);
        }

        [Test]
        public async void UpdateTest()
        {
            _customerService.CreateCustomer(_customer);
            var newCustomer = _customer;
            newCustomer.Forename = "Mia";

            var result = MyHttpClient.UpdateCustomerResult;

            result.Should().BeEquivalentTo(newCustomer);
        }

        [Test]
        public void DeleteTest()
        {
            _customerService.CreateCustomer(_customer);
            var count = _customerService.GetAllCustomers().Count();

            var results = _customerService.GetAllCustomers();

            results.Should().HaveCount(count-1);
        }
    }
}