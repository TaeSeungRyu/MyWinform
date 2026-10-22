

namespace WinFormsHello;

using WinFormsHello.Forms;
using WinFormsHello.Services;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        MyLogger.Init();
        MyLogger.Info("프로그램 시작");
        MyLayout.Instance.InitBody(800, 800);
        InitSystem();
        Application.Run(MyLayout.Instance.MyForm);
    }

    static void InitSystem()
    {
        var label = new Label
        {
            Text = "Hello, C# WinForms!",
            AutoSize = true,
            Font = new Font("Segoe UI", 12),
            Location = new Point(90, 75)
        };

        var resultBox = new TextBox
        {
            Multiline = true,
            ReadOnly = true,
            ScrollBars = ScrollBars.Vertical,
            Location = new Point(90, 200),
            Size = new Size(600, 400)
        };  
        ButtonLayout buttonLayout = new ButtonLayout();
        buttonLayout.BuildButton(resultBox);

        TcpButtonLayout tcpButtonLayout = new TcpButtonLayout();
        tcpButtonLayout.BuildButton(resultBox);

        NormalButtonLayout normalButtonLayout = new NormalButtonLayout("토스트",left:400,top:120, onConfirm: () =>
        {
            AlertModal modal = new AlertModal("Alert 창 띄우기!");
            modal.ShowDialogAsync();
        });

        MyLayout.Instance.MyForm!.Controls.Add(label);      
        MyLayout.Instance.MyForm!.Controls.Add(resultBox);  
        MyLayout.Instance.MyForm!.Controls.Add(buttonLayout.MyButton);        
        MyLayout.Instance.MyForm!.Controls.Add(tcpButtonLayout.MyButton);     
        MyLayout.Instance.MyForm!.Controls.Add(normalButtonLayout.MyButton);           
    }
}
