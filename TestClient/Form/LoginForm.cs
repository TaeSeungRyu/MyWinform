using System;
using System.Drawing;
using System.Windows.Forms;

public class LoginForm : Form
{
    private TextBox _idInput = null!;
    private TextBox _passwordInput = null!;

    public LoginForm()
    {
        Text = "로그인";
        ClientSize = new Size(800, 600);
        MinimumSize = new Size(400, 450);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.WhiteSmoke;
        InitControls();
    }

    private void InitControls()
    {
        // 1. 전체 화면 레이아웃
        TableLayoutPanel root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 1
        };

        // 2. 로그인 영역
        TableLayoutPanel loginPanel = CreateLoginPanel();

        // 3. 로그인 영역을 가운데 배치
        root.Controls.Add(loginPanel, 0, 0);

        Controls.Add(root);
    }

    private TableLayoutPanel CreateLoginPanel()
    {
        TableLayoutPanel panel = new TableLayoutPanel
        {
            Width = 320,
            Height = 340,
            ColumnCount = 1,
            RowCount = 6,
            // TableLayoutPanel 셀 가운데 정렬
            Anchor = AnchorStyles.None,
            BackColor = Color.White,
            Padding = new Padding(25)
        };

        // 행 높이 설정
        panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
        panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
        panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
        panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
        panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
        panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        // 제목
        Label title = new Label
        {
            Text = "LOGIN",
            Font = new Font("맑은 고딕", 18, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Fill
        };

        // 아이디 입력
        _idInput = new TextBox
        {
            Dock = DockStyle.Fill,
            PlaceholderText = "아이디를 입력하세요"
        };

        // 비밀번호 입력
        _passwordInput = new TextBox
        {
            Dock = DockStyle.Fill,
            UseSystemPasswordChar = true,
            PlaceholderText = "비밀번호를 입력하세요"
        };

        // 로그인 버튼
        Button loginButton = new Button
        {
            Text = "로그인",
            Dock = DockStyle.Fill,
            BackColor = Color.SteelBlue,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Height = 45
        };

        loginButton.Click += OnLoginClick;

        // 컨트롤 배치
        panel.Controls.Add(title, 0, 0);

        panel.Controls.Add(
            CreateLabel("아이디"), 0, 1);

        panel.Controls.Add(_idInput, 0, 2);

        panel.Controls.Add(
            CreateLabel("비밀번호"), 0, 3);

        panel.Controls.Add(_passwordInput, 0, 4);

        panel.Controls.Add(loginButton, 0, 5);

        return panel;
    }

    //공통 라벨 함수
    private Label CreateLabel(string text)
    {
        return new Label
        {
            Text = text,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft
        };
    }

    //액션
    private void OnLoginClick(object? sender, EventArgs e)
    {
        string id = _idInput.Text.Trim();
        string password = _passwordInput.Text;

        if (string.IsNullOrWhiteSpace(id))
        {
            MessageBox.Show("아이디를 입력하세요.");
            _idInput.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            MessageBox.Show("비밀번호를 입력하세요.");
            _passwordInput.Focus();
            return;
        }

        // 학습용: 실제 인증은 아직 구현하지 않음
        MessageBox.Show($"로그인 요청: {id}");
    }
}