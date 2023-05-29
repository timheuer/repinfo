using System.Text.RegularExpressions;

namespace repinfo;
internal class GitHubHelpers
{
    public record GitHubInfo(string owner, string repo);

    public GitHubInfo GetGithubInfo(string uri)
    {
        var regex = new Regex(@"https:\/\/github.com\/(?<owner>.+)\/(?<repo>(?:(?!\.git$).)*)");
        var match = regex.Match(uri);

        return new GitHubInfo(match.Groups["owner"].Value, match.Groups["repo"].Value);
    }
}
