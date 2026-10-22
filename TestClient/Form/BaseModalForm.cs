namespace WinFormsHello.Forms;

//상속 연습을 위한 공통 클래스 설정
public class BaseModalForm : Form
{
    public BaseModalForm(int width = 400, int height = 250)
    {
        ClientSize = new Size(width, height);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
    }

    // 자식 모달에서 필요한 화면을 구현
    protected virtual void InitControls()
    {
    }
}