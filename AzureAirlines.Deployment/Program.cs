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

        var servicePrincipal = new ApplicationRegistration("applicationregistration-sp", new ApplicationRegistrationArgs
        {
            DisplayName = "application-registration-test"
        });

        var roleDefinition = new RoleDefinition("roledefinitiontest", new RoleDefinitionArgs {
            RoleName = "role-definition-test"
        });

        var roleAssignment = new RoleAssignment("roleassignmenttest", new RoleAssignmentArgs
        {
            RoleDefinitionId = roleDefinition.Id,
            PrincipalId = servicePrincipal.Id,
            Scope = resourceGroup.Id,
            RoleAssignmentName = "role-assignment-test"
        });
    }
}