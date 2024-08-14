using Duende.IdentityServer.Models;

namespace AuthGuard.Infrastructure
{
    public static class Config
    {
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("Employee.Write","Employee write permission"),
                new ApiScope("Employee.Read","Employee read permission"),
                new ApiScope("Employee.Admin","All permissions")
            };
            
        }
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("Employee"){Scopes={"Employee.Write","Employee.Read","Employee.Admin"}}
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId="Employee",
                    ClientName="EmployeeClient",
                    ClientSecrets={new Secret("employee".Sha256())},
                    AllowedGrantTypes={GrantType.ClientCredentials},
                    AllowedScopes={"Employee.Write","Employee.Read"}
                  
                },
                new Client
                {
                    ClientId="EmployeeAdmin",
                    ClientName="EmployeeClient",
                    ClientSecrets={new Secret("employee".Sha256())},
                    AllowedGrantTypes={GrantType.ClientCredentials},
                    AllowedScopes={"Employee.Admin"}

                },
                new Client
                {
                    ClientId="EmployeeRead", 
                    ClientName="EmployeeClient",
                    ClientSecrets={new Secret("employee".Sha256())},
                    AllowedGrantTypes={GrantType.ClientCredentials},
                    AllowedScopes={"Employee.Read"}

                }
            };
        }
    }
}
 