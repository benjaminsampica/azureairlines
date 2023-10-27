using Pulumi.AzureAD;
using Pulumi.AzureNative.Authorization;
using Pulumi.AzureNative.Resources;

namespace AzureAirlines.Deployment;

internal class TestDevStackBuilder : IAzureDevStackBuilder
{
    public void Build()
    {
        var appName = "test";
        var environment = "dev";
        var current = Pulumi.AzureAD.GetClientConfig.Invoke();
        var tags = new Dictionary<string, string>()
        {
            { "Criticality", "CriticalityTest" },
            { "Cost Center", "CostCenterTest" },
            { "Environment", "EnvironmentTest" },
            { "Owner", "OwnerTest" },
            { "OwnerEmailDL", "OwnerEmailDLTest" },
        };

        var resourceGroup = new ResourceGroup($"{environment}-ncus-{appName}-rg-01", new ResourceGroupArgs
        {
            Location = "North Central US",
            ResourceGroupName = $"{environment}-ncus-{appName}-rg-01",
            Tags = tags
        });

        var applicationRegistration1 = new Application($"{environment}-ncus-{appName}-sp", new ApplicationArgs
        {
            DisplayName = $"{environment}-ncus-{appName}-sp"
        });

        var servicePrincipal1 = new ServicePrincipal($"{environment}-ncus-{appName}-sp", new()
        {
            ApplicationId = applicationRegistration1.ApplicationId,
            AppRoleAssignmentRequired = false,
            Owners = new[]
            {
                current.Apply(gccr => gccr.ObjectId),
            },
        });

        var federatedCredentials = new ApplicationFederatedIdentityCredential("deployment-connection", new ApplicationFederatedIdentityCredentialArgs
        {
            ApplicationId = applicationRegistration1.ApplicationId,
            Subject = "repo:benjaminsampica/azureairlines:ref:refs/heads/main",
            Issuer = "https://token.actions.githubusercontent.com",
            ApplicationObjectId = applicationRegistration1.ApplicationId,
            Audiences = ["api://AzureADTokenExchange"]
        });

        var roleAssignment = new RoleAssignment(nameof(RoleDefinitions.Contributor), new RoleAssignmentArgs
        {
            RoleDefinitionId = $"/providers/Microsoft.Authorization/roleDefinitions/{RoleDefinitions.Contributor}",
            PrincipalId = servicePrincipal1.ObjectId,
            Scope = resourceGroup.Id,
            PrincipalType = "ServicePrincipal"
        });

        var applicationRegistration2 = new Application($"{environment}-ncus-{appName}-app", new ApplicationArgs
        {
            DisplayName = $"{environment}-ncus-{appName}-app"
        });

        var servicePrincipal2 = new ServicePrincipal($"{environment}-ncus-{appName}-app", new()
        {
            ApplicationId = applicationRegistration2.ApplicationId,
            AppRoleAssignmentRequired = false,
            Owners = new[]
            {
                current.Apply(gccr => gccr.ObjectId),
            },
        });
    }
}
