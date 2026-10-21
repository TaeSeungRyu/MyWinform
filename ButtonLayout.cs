
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
            await MyMailService.Instance.SendMailAsync(
                to: "receiver@example.com",
                subject: "WinForms 메일 테스트",
                body: "네이버 SMTP를 이용한 테스트 메일입니다."
            );
        };
    }
    
}