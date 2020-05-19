using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mvc2NorthwindSortOrder;
using Mvc2NorthwindSortOrder.Infrastructure;
using Mvc2NorthwindSortOrder.Models;
using Mvc2NorthwindSortOrder.ViewModels;
using AutoFixture;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1 : BaseTest
    {
        [TestMethod]
        public void AutoMapperConfigurationIsOk()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddMaps(typeof(Startup).Assembly));
            configuration.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void  Customer_should_map_to_customereditviewmodel_ok()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(typeof(CustomerProfile)));
            var mapper = configuration.CreateMapper();
            var source = fixture.Create<Customers>();
            source.ContactName = "Stefan Holmberg";                            

            var dest = mapper.Map<Customers, CustomerEditViewModel>(source);
            Assert.AreEqual(source.CustomerId, dest.CustomerId);
            Assert.AreEqual("STEFAN HOLMBERG",  dest.ContactNameCapitalLetters);
        }

        [TestMethod]
        public void Customereditviewmodel_should_map_to_customer_ok()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(typeof(CustomerProfile)));
            var mapper = configuration.CreateMapper();
            var source = fixture.Create<CustomerEditViewModel>();
            source.ContactNameCapitalLetters = "STEFAN HOLMBERG";

            var dest = mapper.Map<CustomerEditViewModel, Customers>(source);
            Assert.AreEqual(source.CustomerId, dest.CustomerId);
            Assert.AreEqual("Stefan Holmberg", dest.ContactName);
        }

    }
}
