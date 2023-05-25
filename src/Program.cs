// get the current working directory
using LibGit2Sharp;
using System.Runtime.CompilerServices;

string path = Directory.GetCurrentDirectory();

try
{
    using (var repo = new Repository(path))
    {
        Console.WriteLine($"Repository is at {repo.Info.WorkingDirectory}");
        Console.WriteLine($"Branch is {repo.Head.FriendlyName}");
        Console.WriteLine($"Upstream branch is {repo.Head.TrackedBranch.FriendlyName}");
        Console.WriteLine($"Has uncommitted changes: {repo.RetrieveStatus().IsDirty}");
    }
}
catch (RepositoryNotFoundException _)
{
    Console.WriteLine($"Not a valid repository root: {path}");
}