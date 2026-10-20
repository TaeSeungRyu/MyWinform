using System.Drawing;


namespace WinFormsHello;

internal class MyLayout
{
    public Form? MyForm { get; private set; }

    public void InitHead(int colorCode)
    {
        // 추후 구현
    }

    public void InitBody(int width, int height)
    {
        MyForm = new Form
        {
            Text = "WinForms 연습",
            StartPosition = FormStartPosition.CenterScreen,
            ClientSize = new Size(width, height)
        };
    }
}