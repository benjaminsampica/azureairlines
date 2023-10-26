using Pulumi;
using Pulumi.AzureAD;
using Pulumi.AzureNative.Authorization;
using Pulumi.AzureNative.Resources;

await Pulumi.Deployment.RunAsync<DeployStack>();

public class DeployStack : Stack
{
    public DeployStack()
    {
        var current = Pulumi.AzureAD.GetClientConfig.Invoke();

        var resourceGroup = new ResourceGroup("resourcegrouptest", new ResourceGroupArgs
        {
            Location = "North Central US",
            ResourceGroupName = "resource-group-test"
        });

        var appreg = new Application("applicationregistration-sp", new ApplicationArgs
        {
            DisplayName = "application-registration-test"
        });

        var sp = new ServicePrincipal("exampleServicePrincipal", new()
        {
            ApplicationId = appreg.ApplicationId,
            AppRoleAssignmentRequired = false,
            Owners = new[]
            {
                current.Apply(gccr => gccr.ObjectId),
            },
        });

        var roleAssignment = new RoleAssignment("roleassignmenttest", new RoleAssignmentArgs
        {
            RoleDefinitionId = "/providers/Microsoft.Authorization/roleDefinitions/b24988ac-6180-42a0-ab88-20f7382dd24c", // Contributor role ID
            PrincipalId = sp.ObjectId,
            Scope = resourceGroup.Id,
            PrincipalType = "ServicePrincipal"
        });
    }
}