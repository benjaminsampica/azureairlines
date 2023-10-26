using Pulumi;
using Pulumi.AzureAD;
using Pulumi.AzureNative.Authorization;
using Pulumi.AzureNative.Resources;

await Pulumi.Deployment.RunAsync<DeployStack>();

public class DeployStack : Stack
{
    public DeployStack()
    {
        var resourceGroup = new ResourceGroup("resourcegrouptest", new ResourceGroupArgs
        {
            Location = "North Central US",
            ResourceGroupName = "resource-group-test"
        });

        var servicePrincipal = new ServicePrincipal("serviceprincipaltest");

        var roleAssignment = new RoleAssignment("roleassignmenttest", new RoleAssignmentArgs
        {
            PrincipalId = servicePrincipal.Id,
            Scope = resourceGroup.Id
        });
    }
}