
namespace WinFormsHello;

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

        var httpClient = new HttpClient();
        MyButton.Click += async (sender, e) =>
        {
            try
            {
                string json = await httpClient.GetStringAsync(
                    "https://jsonplaceholder.typicode.com/users/1"
                );
                textBox.Text = json;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
        };
    }
    
}