# PWM with Multimedia Timer(2주차 추가 기능 및 보완)

## 설명
Week2의 PWM 생성 프로젝트의 보완 버전입니다.

## 기능
- 사용자가 설정한 주기와 듀티 사이클에 따라 PWM 신호 생성 (AO)
- 설정된 전압 수준으로 PWM 신호 생성 (High Voltage 및 Low Voltage)
- AO로부터 받은 AI의 PWM 신호 측정
- 주기 및 듀티 사이클의 범위 확인 (주파수 : 0.5Hz ~ 10Hz, 듀티: 0% ~ 100%)
- 생성된 PWM 신호를 실시간 그래프로 표시 (Continuous Graph)
- 현재 그래프의 데이터를 캡처하여 별도 그래프로 표시 (Capture Graph)
- 오실로스코프를 사용하여 생성된 PWM 신호의 시각적 비교

## UI
### [Operation]

<img width="500" alt="2" src= "https://github.com/user-attachments/assets/f96598d1-7d15-4735-a23e-f940b52eedb5">

## HardWare Setting
<img width="500" alt="2" src= "https://github.com/user-attachments/assets/bf3dfe52-5251-47e6-8fe8-253b2e81a229">

<img width="500" alt="2" src= "https://github.com/user-attachments/assets/ec5136ba-c9e1-4e02-afd9-3699ffb34f77">

## DB 저장

### PwmData DB
<img width="500" alt="2" src= "https://github.com/user-attachments/assets/8ee28c59-f048-42da-8af9-ae06b5161a12">

### calculate DB
<img width="500" alt="2" src= "https://github.com/user-attachments/assets/5bc78c5a-20c2-47ac-bed4-b57499013053">

## 설치 방법
1. 이 저장소를 클론합니다:
   ```bash
   git clone https://github.com/your-username/ni-pwm-project.git
   ```

2. Visual Studio에서 프로젝트를 엽니다.
3. 프로젝트를 빌드하고 실행합니다.

## 사용 방법
1. 주기, 듀티 사이클, High/Low 전압을 설정합니다.
2. `Apply` 버튼을 눌러 설정을 적용합니다.
3. `Start` 스위치를 켜서 PWM 신호를 생성합니다.
4. `Capture` 버튼을 눌러 현재 그래프의 데이터를 캡처하여 별도 그래프에 표시합니다.
5. `Reset` 버튼으로 초기화 할 수 있습니다.
6. 오실로스코프를 사용하여 실시간 신호를 확인하고, 그래프와 비교합니다.

## 참고 문서
- [NI 공식 문서](https://www.ni.com)
- [Measurement & Automation Explorer (MAX) 사용법](https://www.ni.com/ko-kr/support/downloads/software-products/download.ni-measurement-automation-explorer-(max).html)
- [PWM의 이해와 활용](https://your-document-link)
- [Week2_DH.pptx](https://github.com/user-attachments/files/17144200/Week2_DH.pptx)

### 추가 정보
- 이 프로젝트는 C# 및 .NET Framework를 사용하여 개발되었습니다.
- NI DAQmx 라이브러리를 통해 NI 하드웨어와 통신합니다.
- 모든 설정 값은 UI를 통해 실시간으로 조정 가능하며, 사용자 친화적인 인터페이스를 제공합니다.

이와 같은 README 파일을 통해 프로젝트의 주요 기능과 설치 방법을 명확하게 전달할 수 있습니다. 사용자는 제공된 설명에 따라 프로젝트를 설치하고, 제공된 예제 코드를 기반으로 NI 하드웨어를 제어하고 데이터를 시각화하는 방법을 쉽게 이해할 수 있습니다.
