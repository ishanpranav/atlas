using System.Resources;
using System.Runtime.CompilerServices;

namespace Atlas.Server;

public class FormattedStrings
{
    private readonly ResourceManager _resourceManager = new ResourceManager(typeof(FormattedStrings));

    private string GetString([CallerMemberName] string key = "")
    {
        string? result = _resourceManager.GetString(key);

        if (result == null)
        {
            throw new InvalidOperationException();
        }

        return result;
    }

    public string WelcomeText => GetString();
    public string CreatedPasswordText => GetString();

    public string GetCreatingPasswordText(string username)
    {
        return string.Format(GetString("CreatingPasswordText{0}")!, username);
    }

    public string GetAuthenticatingText(string username)
    {
        return string.Format(GetString("AuthenticatingText{0}"), username);
    }

    public string GetUnauthenticatedText(string username)
    {
        return string.Format(GetString("UnauthenticatedText{0}"), username);
    }
}
