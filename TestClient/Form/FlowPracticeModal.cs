namespace WinFormsHello.Forms;

public class FlowPracticeModal : BaseModalForm
{

    private readonly Action?[] _todos;
    public FlowPracticeModal(Action?[] todos = null) : base(500, 250)
    {
        Text = "1단계 - FlowLayoutPanel";
        _todos = todos ?? new Action?[3];
        InitControls();
    }

    protected override void InitControls()
    {
        // UI 요소를 순서대로 자동 배치하는 패널 생성
        FlowLayoutPanel panel = new FlowLayoutPanel
        {
            // 부모 Form의 사용 가능한 영역을 모두 채움
            // CSS의 width: 100%; height: 100%와 비슷
            Dock = DockStyle.Fill,

            // UI 요소를 왼쪽에서 오른쪽 순서로 배치
            // CSS의 flex-direction: row와 비슷
            FlowDirection = FlowDirection.LeftToRight,

            // 공간이 부족하면 자동으로 다음 줄에 배치
            // CSS의 flex-wrap: wrap과 비슷
            WrapContents = true,

            // 패널 내부의 상하좌우 여백을 15px로 설정
            // CSS의 padding: 15px와 비슷
            Padding = new Padding(15),

            // 패널 영역보다 내용이 많으면 스크롤바 표시
            // CSS의 overflow: auto와 비슷
            AutoScroll = true
        };

        for (int i = 1; i <= 3; i++)
        {
            Button button = new Button
            {
                Text = $"버튼 {i}",
                AutoSize = true
            };
            if(_todos != null && _todos[i - 1] != null)
            {
                _todos[i - 1]?.Invoke();
            }
            panel.Controls.Add(button);
        }

        Controls.Add(panel);
    }
}