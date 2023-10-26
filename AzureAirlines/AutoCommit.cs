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

        //using (Process process = new Process())
        //{
        //    ProcessStartInfo startInfo = new ProcessStartInfo
        //    {
        //        FileName = "git",
        //        WorkingDirectory = _repository,
        //        RedirectStandardInput = true,
        //        RedirectStandardError = true,
        //        RedirectStandardOutput = true,
        //        UseShellExecute = false,
        //        CreateNoWindow = true
        //    };

        //    process.StartInfo = startInfo; ;
        //    process.Start();

        //    process.StandardInput.WriteLine("git add .");
        //    process.StandardInput.WriteLine($"git commit -m \"{message}\"");

        //    process.StandardInput.WriteLine("exit");

        //    string output = process.StandardOutput.ReadToEnd();
        //    string error = process.StandardError.ReadToEnd();

        //    process.WaitForExit();

        //    Console.WriteLine("output - " + output);
        //    Console.WriteLine("error - " + error);
        //}
    }
}