using Pulumi;
using Pulumi.AzureAD;
using Pulumi.AzureNative.Authorization;
using Pulumi.AzureNative.Resources;

namespace AzureAirlines.Deployment;

public class devjam5Stack : Stack
{
    public devjam5Stack()
    {
        var resourceGroup = new ResourceGroup("dev-ncus-devjam5-rg-01", new ResourceGroupArgs
        {
            Location = "North Central US",
        });
        
        var servicePrincipal = new ServicePrincipal("serviceprincipaltest");
        
        var roleAssignment = new RoleAssignment("roleassignmenttest", new RoleAssignmentArgs
        {
            PrincipalId = servicePrincipal.Id,
            Scope = resourceGroup.Id
        });
    }
}