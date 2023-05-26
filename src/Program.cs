using LibGit2Sharp;
using repinfo;
using Spectre.Console;

string path = Directory.GetCurrentDirectory();

while (!Directory.Exists(Path.Combine(path, ".git")))
{
    path = Path.GetFullPath(Path.Combine(path, ".."));
}

try
{
    using (var repo = new Repository(path))
    {
        var headRemote = repo.Head.RemoteName;
        var remote = repo.Network.Remotes.FirstOrDefault(r => r.Name == headRemote);
        AnsiConsole.MarkupLine($"[bold green]Repository:[/] {repo.Info.WorkingDirectory}");
        AnsiConsole.MarkupLine($"[bold green]Branch:[/] {repo.Head.FriendlyName}");
        AnsiConsole.MarkupLine($"[bold green]Remote:[/] {headRemote}");
        AnsiConsole.MarkupLine($"[bold green]Remote URL:[/] {remote.Url}");

        //extracting org/repo from github url
        if (remote.Url.Contains("github"))
        {
            GitHubHelpers gh = new GitHubHelpers();
            var ghinfo = gh.GetGithubInfo(remote.Url);
            AnsiConsole.MarkupLine($"[bold green]GitHub Owner:[/] {ghinfo.owner}");
            AnsiConsole.MarkupLine($"[bold green]GitHub Repo:[/] {ghinfo.repo}");
        }

        AnsiConsole.MarkupLine($"[bold green]Upstream branch:[/] {repo.Head.TrackedBranch.FriendlyName}");
        AnsiConsole.MarkupLine($"[bold green]Has uncommitted changes:[/] {repo.RetrieveStatus().IsDirty}");
    }
}
catch (RepositoryNotFoundException _)
{
    AnsiConsole.MarkupLine($"[bold red]Not a valid repository root: {path}[/]");
}