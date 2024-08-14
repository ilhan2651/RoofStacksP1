using AuthGuard.Infrastructure;
using NUnit.Framework;
using System.Security.Cryptography.X509Certificates;

namespace AuthGuardTestProject
{
    public class ConfigTests
    {
        
        
        [Test]
        public void GetApiScopes_ShouldReturnExpectedScopes()
        {
            var apiScopes = Config.GetApiScopes();

            Assert.IsNotEmpty(apiScopes);
            Assert.IsTrue(apiScopes.Any(scope=>scope.Name=="Employee.Write"));
            Assert.IsTrue(apiScopes.Any(scope => scope.Name == "Employee.Read"));
            Assert.IsTrue(apiScopes.Any(scope => scope.Name == "Employee.Admin"));
        }
        [Test]
        public void GetApiResources_ShouldReturnExpectedResources()
        {
            var apiResources = Config.GetApiResources();
            var apiResource = apiResources.Single();

            Assert.AreEqual("Employee", apiResource.Name);
            Assert.IsTrue(apiResource.Scopes.Contains("Employee.Write"));
            Assert.IsTrue(apiResource.Scopes.Contains("Employee.Read"));
            Assert.IsTrue(apiResource.Scopes.Contains("Employee.Admin"));


        }
        [Test]
         public void GetClients_ShouldReturnExceptedClients()
        {
            var clients = Config.GetClients();
            Assert.IsTrue(clients.Any(client => client.ClientId == "Employee"));
            Assert.IsTrue(clients.Any(client => client.ClientId == "EmployeeAdmin"));
            Assert.IsTrue(clients.Any(client => client.ClientId == "EmployeeRead"));


        }
    }
}