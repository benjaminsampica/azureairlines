using Pulumi.AzureAD;
using Pulumi.AzureNative.Authorization;
using Pulumi.AzureNative.Resources;

namespace AzureAirlines.Deployment;

internal class TestDevStackBuilder : IAzureDevStackBuilder
{
    public void Build()
    {
        var current = Pulumi.AzureAD.GetClientConfig.Invoke();

        var resourceGroup = new ResourceGroup("resourcegrouptest", new ResourceGroupArgs
        {
            Location = "North Central US",
            ResourceGroupName = "resource-group-test"
        });

        var appreg = new Application("applicationregistration-sp", new ApplicationArgs
        {
            DisplayName = "application-registration-sp"
        });

    }
}
