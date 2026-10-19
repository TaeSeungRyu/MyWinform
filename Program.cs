using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsHello;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var form = new Form
        {
            Text = "WinForms 연습",
            StartPosition = FormStartPosition.CenterScreen,
            ClientSize = new Size(400, 200)
        };

        var label = new Label
        {
            Text = "Hello, C# WinForms!",
            AutoSize = true,
            Font = new Font("Segoe UI", 16),
            Location = new Point(90, 75)
        };

        form.Controls.Add(label);
        Application.Run(form);
    }
}
