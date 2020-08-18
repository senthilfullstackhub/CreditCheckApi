namespace CreditCheck.UnitTest.ControllerTest
{
    using CreditCheck.Models;
    using FluentAssertions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.TestHost;
    using Moq;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class CustomerContollerTest
    {
        private TestServer server;
        public HttpClient Client { get; private set; }

        public CustomerContollerTest()
        {
            server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = server.CreateClient();
        }

        [Fact]
        public async Task Test_Get_All_Customers()
        {
            using (var client = new CustomerContollerTest().Client)
            {
                var response = await client.GetAsync("/api/customers");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }
        
        [Fact]
        public async Task Test_Post_Customer_EmptySalary()
        {
            using (var client = new CustomerContollerTest().Client)
            {
                var response = await client.PostAsync("/api/customers"
                        , new StringContent(
                        JsonConvert.SerializeObject(new Customer()
                        {
                            FirstName = "Test",
                            LastName = "Api",
                            DateOfBirth = DateTime.Now,
                            Salary = 0
                        }),
                        Encoding.UTF8,
                        "application/json"));

                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            }
        }

        [Fact]
        public async Task Test_Post_Customer_NullFirstName()
        {
            using (var client = new CustomerContollerTest().Client)
            {
                var response = await client.PostAsync("/api/customers"
                        , new StringContent(
                        JsonConvert.SerializeObject(new Customer()
                        {
                            FirstName = null,
                            LastName = "Api",
                            DateOfBirth = DateTime.Now,
                            Salary = 5000
                        }),
                        Encoding.UTF8,
                        "application/json"));

                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            }
        }

        [Fact]
        public async Task Test_Post_Customer_NullLastName()
        {
            using (var client = new CustomerContollerTest().Client)
            {
                var response = await client.PostAsync("/api/customers"
                        , new StringContent(
                        JsonConvert.SerializeObject(new Customer()
                        {
                            FirstName = "Test",
                            LastName = null,
                            DateOfBirth = DateTime.Now,
                            Salary = 30000
                        }),
                        Encoding.UTF8,
                        "application/json"));

                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            }
        }

        [Fact]
        public async Task Test_Post_Customer_MinLengthName()
        {
            using (var client = new CustomerContollerTest().Client)
            {
                var response = await client.PostAsync("/api/customers"
                        , new StringContent(
                        JsonConvert.SerializeObject(new Customer()
                        {
                            FirstName = "T",
                            LastName = "A",
                            DateOfBirth = DateTime.Now,
                            Salary = 32000
                        }),
                        Encoding.UTF8,
                        "application/json"));

                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            }
        }

        [Fact]
        public async Task Test_Post_Customer_TodayOrGreaterDOB()
        {
            using (var client = new CustomerContollerTest().Client)
            {
                var response = await client.PostAsync("/api/customers"
                        , new StringContent(
                        JsonConvert.SerializeObject(new Customer()
                        {
                            FirstName = "Test",
                            LastName = "Api",
                            DateOfBirth = DateTime.Now,
                            Salary = 32000
                        }),
                        Encoding.UTF8,
                        "application/json"));

                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            }
        }
        
        public void Dispose()
        {
            server?.Dispose();
            Client?.Dispose();
        }
    }
}
