namespace WinFormsHello.State;

using System.ComponentModel;
using System.Runtime.CompilerServices;

public class UserNameState  : INotifyPropertyChanged
{
    private string _userName = "Guest";

    // 상태 변경 알림
    public event Action<string>? UserNameChanged;

    // 속성 변경 알림 이벤트
    public event PropertyChangedEventHandler? PropertyChanged;    

    public string UserName
    {
        get => _userName;
        set
        {
            if (_userName == value)
                return;

            _userName = value;

            // 상태가 변경되었다고 구독자에게 알림
            //UserNameChanged?.Invoke(value);
            //OnPropertyChanged("UserName");
            OnPropertyChanged();  //CallerMemberName 속성을 사용했으므로
        }
    }
    private void OnPropertyChanged(
        [CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName)
        );
    }    
}