namespace WinFormsHello.Forms;

public class ResponsiveModal : BaseModalForm
{
    public ResponsiveModal() : base(600, 300)
    {
        Text = "반응형 레이아웃 연습";
        // 창 크기 변경 허용
        FormBorderStyle = FormBorderStyle.Sizable;
        MaximizeBox = true;
        MinimumSize = new Size(350, 250);
        InitControls();
    }

    protected override void InitControls()
    {
        // 전체 레이아웃
        TableLayoutPanel table = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 3,
            Padding = new Padding(20)
        };

        // 첫 번째 열: 고정 너비 100px
        table.ColumnStyles.Add(
            new ColumnStyle(SizeType.Absolute, 100)); //Absolute는 고정을 의미함!

        // 두 번째 열: 남은 공간 전체
        table.ColumnStyles.Add(
            new ColumnStyle(SizeType.Percent, 100));

        // 각 행 높이 설정
        table.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 50)); //Absolute는 고정을 의미함!

        table.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 50)); //Absolute는 고정을 의미함!

        table.RowStyles.Add(
            new RowStyle(SizeType.Percent, 100));

        //샘플 IP 입력
        TextBox hostInput = new TextBox
        {
            Text = "127.0.0.1",
            Dock = DockStyle.Fill
        };

        //샘플 포트 입력
        TextBox portInput = new TextBox
        {
            Text = "1502",
            Dock = DockStyle.Fill,
            AutoSize = false,
        };

        //샘플 버튼
        Button connectButton = new Button
        {
            Text = "연결",
            AutoSize = true
        };
        Button cancelButton = new Button
        {
            Text = "취소",
            AutoSize = true
        };

        // 버튼 그룹
        FlowLayoutPanel buttonPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.RightToLeft,
            WrapContents = false
        };

        buttonPanel.Controls.Add(AddBackgroundColor(cancelButton));
        buttonPanel.Controls.Add(AddBackgroundColor(connectButton));



        // 테이블에 UI 배치
        table.Controls.Add(new Label
        {
            Text = "서버 IP",
            AutoSize = true
        }, 0, 0);

        table.Controls.Add(AddBackgroundColor(hostInput, Color.BlueViolet), 1, 0);
        table.Controls.Add(new Label
        {
            Text = "포트",
            AutoSize = true
        }, 0, 1);

        table.Controls.Add(portInput, 1, 1);

        // 마지막 행에서 버튼 영역을 두 열에 걸쳐 표시
        table.Controls.Add(buttonPanel, 0, 2);
        table.SetColumnSpan(buttonPanel, 2);

        Controls.Add(table);
    }

    private Panel AddBackgroundColor(Control children, Color? colorCode = null)
    {
        Panel panel = new Panel
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Dock = DockStyle.Fill,
            BackColor = colorCode ?? Color.LightBlue,
            Margin = new Padding(0)
        };  
        panel.Controls.Add(children);
        return panel;      
    }
}