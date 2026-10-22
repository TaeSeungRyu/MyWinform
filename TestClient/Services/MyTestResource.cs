
namespace WinFormsHello.Services;

//인터페이스 상속 연습
public class MyTestResource : IDisposable 
{

    private readonly string _logTitle;
    public MyTestResource(string logTitle = "객체 생성")
    {
        _logTitle = logTitle;
        Console.WriteLine(_logTitle);
    }

    // IDisposable 인터페이스에서 요구하는 메서드
    public void Dispose()
    {
        Console.WriteLine(_logTitle + " 자원 정리");
    }
}