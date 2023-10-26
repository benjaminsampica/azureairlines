﻿using Pulumi;
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

        //var contributor2 = GetRoleDefinition.InvokeAsync(new GetRoleDefinitionArgs
        //{
        //    RoleDefinitionId = "b24988ac-6180-42a0-ab88-20f7382dd24c",
        //    Scope = resourceGroup.Id.Apply()
        //}); 

        //var contributorResult = contributor2.Result;

        var roleAssignment = new RoleAssignment("roleassignmenttest", new RoleAssignmentArgs
        {
            RoleDefinitionId = "/providers/Microsoft.Authorization/roleDefinitions/b24988ac-6180-42a0-ab88-20f7382dd24c", // Contributor role ID
            PrincipalId = servicePrincipal.ObjectId,
            Scope = resourceGroup.Id
        });
    }
}