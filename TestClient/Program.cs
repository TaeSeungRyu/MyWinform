

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
            Location = new Point(90, 25)
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
            //모달 띄우기 연습
            using AlertModal modal = new AlertModal("Alert 창 띄우기!");
            modal.ShowDialog();
            //인터페이스 상속하여 테스트
            using IDisposable resource = new MyTestResource("IDisposable로 받기");
            using MyTestResource resource2 = new MyTestResource("MyTestResource로 받기");
        });


        NormalButtonLayout normalButtonLayout2 = new NormalButtonLayout("컨트롤 모달 실행",left:550,top:120, onConfirm: () =>
        {
            //모달 띄우기 연습
            using ControlsModal modal = new ControlsModal();
            modal.ShowDialog();
        });

        NormalButtonLayout normalButtonLayout3 = new NormalButtonLayout("FlowPracticeModal 모달 실행",left:400,top:60, onConfirm: () =>
        {
            //모달 띄우기 연습
            using FlowPracticeModal modal = new FlowPracticeModal();
            modal.ShowDialog();
        });        


        NormalButtonLayout normalButtonLayout4 = new NormalButtonLayout("반응형 같은 모달 실행",left:100,top:60, onConfirm: () =>
        {
            //모달 띄우기 연습
            using ResponsiveModal modal = new ResponsiveModal();
            modal.ShowDialog();
        });               

        NormalButtonLayout normalButtonLayout5 = new NormalButtonLayout("로그인폼",left:700,top:60, onConfirm: () =>
        {
            //모달 띄우기 연습
            using LoginForm modal = new LoginForm();
            modal.ShowDialog();
        });                


        MyLayout.Instance.MyForm!.Controls.Add(label);      
        MyLayout.Instance.MyForm!.Controls.Add(resultBox);  
        MyLayout.Instance.MyForm!.Controls.Add(buttonLayout.MyButton);        
        MyLayout.Instance.MyForm!.Controls.Add(tcpButtonLayout.MyButton);     
        MyLayout.Instance.MyForm!.Controls.Add(normalButtonLayout.MyButton);           
        MyLayout.Instance.MyForm!.Controls.Add(normalButtonLayout2.MyButton);      
        MyLayout.Instance.MyForm!.Controls.Add(normalButtonLayout3.MyButton);      
        MyLayout.Instance.MyForm!.Controls.Add(normalButtonLayout4.MyButton);      
        MyLayout.Instance.MyForm!.Controls.Add(normalButtonLayout5.MyButton);      
    }
}
