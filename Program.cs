

namespace WinFormsHello;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        MyLayout myLayout = new MyLayout();

        myLayout.InitBody(400, 200);

        var label = new Label
        {
            Text = "Hello, C# WinForms!",
            AutoSize = true,
            Font = new Font("Segoe UI", 16),
            Location = new Point(90, 75)
        };
        myLayout.MyForm!.Controls.Add(label);
        Application.Run(myLayout.MyForm);
    }
}
