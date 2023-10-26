﻿using Pulumi;
using Pulumi.AzureAD;
using Pulumi.AzureNative.Authorization;
using Pulumi.AzureNative.Resources;

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