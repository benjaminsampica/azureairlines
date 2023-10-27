﻿using Pulumi.AzureAD;
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

        var resourceGroup = new ResourceGroup($"${environment}-ncus-{appName}-rg-01", new ResourceGroupArgs
        {
            Location = "North Central US",
            ResourceGroupName = $"${environment}-ncus-{appName}-rg-01"
        });

        var applicationRegistration = new Application($"${environment}-ncus-{appName}-sp", new ApplicationArgs
        {
            DisplayName = $"${environment}-ncus-{appName}-sp"
        });

        var servicePrincipal = new ServicePrincipal($"${environment}-ncus-{appName}-sp", new()
        {
            ApplicationId = applicationRegistration.ApplicationId,
            AppRoleAssignmentRequired = false,
            Owners = new[]
            {
                current.Apply(gccr => gccr.ObjectId),
            },
        });

        var roleAssignment = new RoleAssignment(nameof(RoleDefinitions.Contributor), new RoleAssignmentArgs
        {
            RoleDefinitionId = $"/providers/Microsoft.Authorization/roleDefinitions/{RoleDefinitions.Contributor}",
            PrincipalId = servicePrincipal.ObjectId,
            Scope = resourceGroup.Id,
            PrincipalType = "ServicePrincipal"
        });
    }
}
