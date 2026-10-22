
namespace WinFormsHello;

internal sealed class NormalButtonLayout
{

    private readonly string _buttonText;
    private readonly int _left;
    private readonly int _top;
    private readonly Action? _onConfirm;

    public NormalButtonLayout(string btnText, int left, int top, Action? onConfirm = null)
    {
        _buttonText = btnText;
        _left = left;
        _top = top;
        _onConfirm = onConfirm;
        BuildButton();
    }
    public Button MyButton { get; private set; } = null!;

    void BuildButton()
    {
        MyButton = new Button
        {
            Text = _buttonText,
            Location = new Point(_left, _top)
        };
        MyButton.Click += async (sender, e) =>
        {
            _onConfirm?.Invoke();
        };
    }
    
}