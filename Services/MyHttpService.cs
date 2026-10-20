
namespace WinFormsHello.Services;
using Newtonsoft.Json;

internal sealed class MyHttpService
{
    // 싱글톤 인스턴스
    public static MyHttpService Instance { get; } = new MyHttpService();

    //Http 클라이언트
    private static readonly HttpClient httpClient = new();

    // 외부 객체 생성 방지
    private MyHttpService()
    {
    }


    public async Task<string> GetJsonData()
    {
        try
        {
            string json = await httpClient.GetStringAsync(
                "https://jsonplaceholder.typicode.com/users/1"
            );
            MyJsonUser? user = JsonConvert.DeserializeObject<MyJsonUser>(json);
            Console.WriteLine(user?.Name);
            Console.WriteLine(user?.Id);
            return json;
        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show(ex.Message);
        }        
        return "";
    }

}