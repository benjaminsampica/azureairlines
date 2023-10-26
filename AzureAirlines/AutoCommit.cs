using System.Diagnostics;
using LibGit2Sharp;

public class AutoCommit
{
    
    private readonly string _repository = @"C:\Users\sg57737\Developer\AzureAirlines";
    public void Commit()
    {
        var message = "Testing 1";

        using(var repo = new Repository(_repository))
        {
            //Commands.Stage(repo, filePath);
            Commands.Stage(repo, "*");
            Signature author = new Signature("Sumesh", "sgaud4@gmail.com", DateTimeOffset.Now);
            Signature commiter = author;

            repo.Commit(message, author, commiter);
            Console.WriteLine("Changes committed successfully");
        }
    }
}