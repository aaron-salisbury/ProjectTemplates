using AvaloniaApp.Business;

namespace AvaloniaApp.Presentation.Desktop.Base;

public class Session : ISessionValueProvider
{
    private int? _endUser;

    public int? GetUserID()
    {
        return _endUser;
    }

    public void SetUserID(int id)
    {
        _endUser = id;
    }
}
