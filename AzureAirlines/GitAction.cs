using Octokit;

namespace AzureAirlines;

public class GitAction
{
    private readonly string _owner = "benjaminsampica";
    private readonly string _repoName = "azureairlines";
    private readonly string _token = "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";

    public async Task UploadFileAsync(string fileContent, string fileName)
    {
        var github = new GitHubClient(new ProductHeaderValue(_owner));
        github.Credentials = new Credentials(_token);

        var repo = await github.Repository.Get(_owner, _repoName);
        if (repo != null)
        {
            string filePath = $"//{_owner}//{_repoName}//AzureAirlines//PulumiStackFiles//{fileName}";

            var createFileRequest = new CreateFileRequest("new file " + fileName, fileContent, "main", true);
            _ = await github.Repository.Content.CreateFile(owner: _owner, _repoName, filePath, createFileRequest);
        }
    }
}