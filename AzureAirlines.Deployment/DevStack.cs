using Pulumi;
using System.Reflection;

namespace AzureAirlines.Deployment;

public class DevStack : Stack
{
    public DevStack()
    {
        var stackBuildersClasses = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetInterfaces().Any(i => i.Name == nameof(IAzureDevStackBuilder)));

        foreach(var stackBuilderClass in stackBuildersClasses)
        {
            var stackBuilder = Activator.CreateInstance(stackBuilderClass)! as IAzureDevStackBuilder;

            stackBuilder!.Build();
        }
    }
}