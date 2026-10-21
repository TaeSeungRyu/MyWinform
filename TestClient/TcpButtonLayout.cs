
namespace WinFormsHello;

using WinFormsHello.Services;

internal sealed class TcpButtonLayout
{

    public Button MyButton { get; private set; } = null!;

    public void BuildButton(TextBox textBox)
    {
        MyButton = new Button
        {
            Text = "TCP 조회",
            Location = new Point(250, 120),
            Size = new Size(120, 35)
        };
        
        MyButton.Click += async (sender, e) =>
        {
            MyButton.Enabled = false;
            try
            {
                ushort[] registers =
                    await MyTcpService.Instance.ReadHoldingRegistersAsync(
                        host: "127.0.0.1",
                        port: 1502,
                        startAddress: 0,
                        count: 3
                    );

                double temperature = registers[0] / 10.0;
                double pressure = registers[1] / 100.0;
                bool isRunning = registers[2] == 1;

                string result =
                    $"온도: {temperature:F1} °C\n" +
                    $"압력: {pressure:F2} bar\n" +
                    $"가동 상태: {(isRunning ? "ON" : "OFF")}";

                textBox.Text = result;
                MyLogger.Info(result);
            }
            catch (Exception ex)
            {
                MyLogger.Error("Modbus TCP 통신 실패", ex);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MyButton.Enabled = true;
            }
        };
    }
    
}