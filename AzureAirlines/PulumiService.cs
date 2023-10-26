﻿using Pulumi;
using Pulumi.AzureNative.Resources;
using Pulumi.AzureNative.Authorization;
using Pulumi.AzureAD;

class PulumiService
{
    static Task<int> Main() => Pulumi.Deployment.RunAsync<PulumiStack>();
}

public class PulumiStack : Stack
{
    public PulumiStack()
    {
        var resourceGroup = new ResourceGroup("resourcegrouptest", new ResourceGroupArgs
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