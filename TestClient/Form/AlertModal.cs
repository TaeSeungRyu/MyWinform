namespace WinFormsHello.Forms;

//상속 연습을 위한 공통 클래스 설정
public class AlertModal : BaseModalForm
{

    private readonly string _message;
    public AlertModal(string msg, int width = 400, int height = 250) : base(width, height)
    {
        _message = msg;
        Text = "알림";        
        InitControls();
    }

    //오버라이드
    protected override void InitControls()
    {
        Label label = new Label
        {
            Text = _message
        };
        Controls.Add(label);        
    }
}