namespace AzureAirlines.Components.Pages;

public class GitHubApi
{
    public const string ApiToken = "";
    public const string EndpointUrl = "https://api.github.com/graphql";
    public const string StatusQuery = """
        query{
          node(id: "W_kwDOKla8j84EaNDP") {
            ... on Workflow {
              state
              runs(first: 10) {
                nodes {
                  checkSuite {
                    status
                    conclusion
                  }
                }
              }
            }
          }
        }
        """;
}
