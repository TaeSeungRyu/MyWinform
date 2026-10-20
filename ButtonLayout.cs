
namespace WinFormsHello;

using WinFormsHello.Services;

internal sealed class ButtonLayout
{

    public Button MyButton { get; private set; } = null!;

    public void BuildButton(TextBox textBox)
    {
        MyButton = new Button
        {
            Text = "HTTP 조회",
            Location = new Point(90, 120),
            Size = new Size(120, 35)
        };
        MyButton.Click += async (sender, e) =>
        {
            string json = await MyHttpService.Instance.GetJsonData();
            textBox.Text = json;
        };
    }
    
}