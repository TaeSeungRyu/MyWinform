namespace WinFormsHello.Forms;

//상속 연습을 위한 공통 클래스 설정
public class ControlsModal : BaseModalForm
{

    public ControlsModal( int width = 1080, int height = 500) : base(width, height)
    {
        Text = "Controls 레이아웃";        
        InitControls();
    }

    //오버라이드
    protected override void InitControls()
    {
        // 1. 텍스트 입력
        TextBox nameInput = new TextBox
        {
            PlaceholderText = "이름 입력",
            Location = new Point(30, 20),
            Width = 200
        };

        // 2. 비밀번호 입력
        TextBox passwordInput = new TextBox
        {
            PlaceholderText = "비밀번호",
            UseSystemPasswordChar = true,
            Location = new Point(30, 60),
            Width = 200
        };

        // 3. 라디오 버튼 (같은 Panel 안에서는 하나만 선택)
        RadioButton radioMale = new RadioButton
        {
            Text = "남성",
            Location = new Point(10, 10),
            Checked = true
        };

        RadioButton radioFemale = new RadioButton
        {
            Text = "여성",
            Location = new Point(110, 10)
        };

        Panel radioPanel = new Panel
        {
            Location = new Point(30, 100),
            Size = new Size(250, 35)
        };

        radioPanel.Controls.Add(radioMale);
        radioPanel.Controls.Add(radioFemale);

        // 4. 체크박스
        CheckBox agreeCheck = new CheckBox
        {
            Text = "동의합니다",
            Location = new Point(30, 145),
            AutoSize = true
        };

        // 5. 드롭다운
        ComboBox roleSelect = new ComboBox
        {
            Location = new Point(30, 185),
            Width = 200,
            DropDownStyle = ComboBoxStyle.DropDownList
        };

        roleSelect.Items.AddRange(
            new object[] { "사용자", "관리자" }
        );
        roleSelect.SelectedIndex = 0;

        // 6. 숫자 입력
        NumericUpDown ageInput = new NumericUpDown
        {
            Location = new Point(30, 225),
            Minimum = 0,
            Maximum = 120,
            Value = 30
        };

        // 7. 버튼
        Button submitButton = new Button
        {
            Text = "확인",
            Location = new Point(30, 270),
            Width = 100
        };

        submitButton.Click += (sender, e) =>
        {
            string name = nameInput.Text;
            bool isMale = radioMale.Checked;
            bool agreed = agreeCheck.Checked;
            string role = roleSelect.SelectedItem?.ToString() ?? "";
            decimal age = ageInput.Value;

            MessageBox.Show(
                $"이름: {name}\n" +
                $"성별: {(isMale ? "남성" : "여성")}\n" +
                $"권한: {role}\n" +
                $"나이: {age}\n" +
                $"동의: {agreed}"
            );
        };

        // TextBox 값 변경
        nameInput.TextChanged += (sender, e) =>
        {
            Console.WriteLine("TextBox 변화: "+nameInput.Text);
        };

        // TextBox 값 변경
        passwordInput.TextChanged += (sender, e) =>
        {
            Console.WriteLine("TextBox(pwd) 변화: "+passwordInput.Text);
        };

        // RadioButton 선택 변경
        radioMale.CheckedChanged += (sender, e) =>
        {
            Console.WriteLine("RadioButton 변화: "+radioMale.Checked);
        };

        // ComboBox 선택 변경
        roleSelect.SelectedIndexChanged += (sender, e) =>
        {
            Console.WriteLine("ComboBox 변화: "+roleSelect.SelectedItem);
        };

        // CheckBox 상태 변경
        agreeCheck.CheckedChanged += (sender, e) =>
        {
            Console.WriteLine("CheckBox 변화: "+agreeCheck.Checked);
        };        

        // 폼에 컨트롤 추가
        Controls.Add(nameInput);
        Controls.Add(passwordInput);
        Controls.Add(radioPanel);
        Controls.Add(agreeCheck);
        Controls.Add(roleSelect);
        Controls.Add(ageInput);
        Controls.Add(submitButton);
    }
}