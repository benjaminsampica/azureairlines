using Pulumi;
using Pulumi.Azure.Batch;
using Pulumi.Azure.Core;

class PulumiService
{
    static Task<int> Main() => Deployment.RunAsync<DeployStack>();
}

public class DeployStack : Stack
{
    public DeployStack()
    {
        var resourceGroup = new ResourceGroup("resourceGroup", new ResourceGroupArgs
        {
            Location = "North Central US",
        });

        var application = new Application("application", new ApplicationArgs
        {
            ResourceGroupName = resourceGroup.Name
        });
    }
}