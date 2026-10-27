namespace WinFormsHello.State;

public class UserNameState
{
    private string _userName = "Guest";

    // 상태 변경 알림
    public event Action<string>? UserNameChanged;

    public string UserName
    {
        get => _userName;
        set
        {
            if (_userName == value)
                return;

            _userName = value;

            // 상태가 변경되었다고 구독자에게 알림
            UserNameChanged?.Invoke(value);
        }
    }
}