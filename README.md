# MyWinform

C# .NET 10과 Windows Forms(WinForms)를 학습하기 위한 연습 프로젝트입니다.

버튼, 화면 구성, HTTP 통신, TCP 통신 등 기본 기능을 직접 구현하고 있습니다.

## 개발 환경

- C# / .NET 10
- Windows Forms
- Visual Studio Code
- Windows

## 주요 기능

- WinForms 화면 및 버튼 이벤트
- HTTP API 요청 및 JSON 데이터 처리
- Serilog를 이용한 로그 기록
- MailKit을 이용한 이메일 전송
- Modbus TCP 방식의 데이터 송수신
- 가상 TCP 서버를 통한 통신 테스트

## 프로젝트 구조

```text
MyWinform/
├── TestClient/
│   ├── Program.cs
│   ├── Services/
│   └── WinFormsHello.csproj
├── TestTcpServer/
│   ├── Program.cs
│   └── TestTcpServer.csproj
└── README.md
```

## 실행 방법

**1. TCP 테스트 서버 실행**

```powershell
dotnet run --project .\TestTcpServer\TestTcpServer.csproj
```

**2. WinForms 실행**

```powershell
dotnet run --project .\TestClient\WinFormsHello.csproj
```

TCP 테스트 서버는 `127.0.0.1:1502`에서 실행됩니다.

## Modbus TCP 테스트

WinForms에서 TCP 서버에 Holding Register 읽기를 요청합니다.

| 주소 | 데이터 | 의미 |
|---|---|---|
| 0 | 230 | 온도 23.0°C |
| 1 | 1250 | 압력 12.50 bar |
| 2 | 1 | 장비 가동 중 |

실제 장비가 아닌 가상 데이터를 사용하는 학습용 예제입니다.

## 목적

C#과 WinForms의 기본 구조를 익히고, 간단한 통신 모니터 프로그램을 만들어 보는 것이 목표입니다.