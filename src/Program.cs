// get the current working directory
using LibGit2Sharp;
using repinfo;

string path = Directory.GetCurrentDirectory();

try
{
    using (var repo = new Repository(path))
    {
        var headRemote = repo.Head.RemoteName;
        var remote = repo.Network.Remotes.FirstOrDefault(r => r.Name == headRemote);
        Console.WriteLine($"Repository is at {repo.Info.WorkingDirectory}");
        Console.WriteLine($"Branch is {repo.Head.FriendlyName}");
        Console.WriteLine($"Remote is {headRemote}");
        Console.WriteLine($"Remote URL is {remote.Url}");

        if (remote.Url.Contains("github"))
        {
            GitHubHelpers gh = new GitHubHelpers();
            var ghinfo = gh.GetGithubInfo(remote.Url);
            Console.WriteLine($"GitHub Owner: {ghinfo.owner}");
            Console.WriteLine($"GitHub Repo: {ghinfo.repo}");
        }

        Console.WriteLine($"Upstream branch is {repo.Head.TrackedBranch.FriendlyName}");
        Console.WriteLine($"Has uncommitted changes: {repo.RetrieveStatus().IsDirty}");
    }
}
catch (RepositoryNotFoundException _)
{
    Console.WriteLine($"Not a valid repository root: {path}");
}