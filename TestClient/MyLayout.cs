
namespace WinFormsHello;

internal sealed class MyLayout
{
    // 싱글톤 인스턴스
    public static MyLayout Instance { get; } = new MyLayout();

    // Form
    public Form MyForm { get; private set; } = null!;

    // 외부에서 객체 생성 방지
    private MyLayout()
    {
    }

    public void InitHead(int colorCode)
    {
        // 추후 구현
    }

    public void InitBody(int width = 300, int height = 300)
    {
        MyForm = new Form
        {
            Text = "WinForms 연습",
            StartPosition = FormStartPosition.CenterScreen,
            ClientSize = new Size(width, height)
        };
    }
}