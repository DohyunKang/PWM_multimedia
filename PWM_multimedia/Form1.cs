using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks; // System.Threading.Tasks.Task 명확히 사용
using System.Windows.Forms;
using NationalInstruments.DAQmx;
using System.Collections.Generic;

namespace PWM_multimedia
{
    public partial class Form1 : Form
    {
        private NationalInstruments.DAQmx.Task writeTask; // AO Task
        private NationalInstruments.DAQmx.Task analogReadTask; // AI Task
        private AnalogSingleChannelWriter writer;
        private AnalogSingleChannelReader analogReader;

        private double outputVoltage; // PWM output voltage
        private double inputVoltage; // AI input voltage
        private double frequency; // Frequency of the PWM signal
        private double dutyCycle; // Duty cycle of the PWM signal (0-100%)
        private bool pwmStateHigh;
        private double highTime; // Time the signal is high
        private double lowTime;  // Time the signal is low
        private double pwmElapsed; // Elapsed time

        private double HighV;    // Voltage when signal is high
        private double LowV;     // Voltage when signal is low

        private DateTime lastPwmTime = DateTime.Now;
        private DateTime cycleStartTime = DateTime.Now; // 주기 시작 시간 기록

        private double previousVoltage = 0; // 이전 전압 상태
        private DateTime lastEdgeTime = DateTime.Now; // Edge time record

        private bool flag = false; // Switch state flag

        public Form1()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            writeTask = new NationalInstruments.DAQmx.Task();
            analogReadTask = new NationalInstruments.DAQmx.Task();

            // AO channel for PWM signal generation
            writeTask.AOChannels.CreateVoltageChannel("Dev1/ao0", "", 0.0, 5.0, AOVoltageUnits.Volts);

            // AI channel for reading the PWM signal
            analogReadTask.AIChannels.CreateVoltageChannel("Dev1/ai0", "", AITerminalConfiguration.Rse, 0.0, 5.0, AIVoltageUnits.Volts);

            writer = new AnalogSingleChannelWriter(writeTask.Stream);
            analogReader = new AnalogSingleChannelReader(analogReadTask.Stream);

            frequency = 50;  // Default frequency to 50Hz
            dutyCycle = 50;  // Default duty cycle 50%
            HighV = 5;       // Maximum voltage 5V
            LowV = 0;        // Minimum voltage 0V

            UpdatePWMParameters(); // Initialize PWM parameters
        }

        private void StartMultimediaTimer()
        {
            // 비동기적으로 PWM 및 AI 데이터 처리 시작
            System.Threading.Tasks.Task.Run(() => GeneratePWMAndReadAIAsync());
        }

        private void StopMultimediaTimer()
        {
            flag = false;
        }

        // 비동기 메서드로 Task 반환
        private async System.Threading.Tasks.Task GeneratePWMAndReadAIAsync()
        {
            flag = true;
            while (flag)
            {
                DateTime currentTime = DateTime.Now;
                TimeSpan deltaTime = currentTime - lastPwmTime;
                double elapsedSeconds = deltaTime.TotalSeconds;

                pwmElapsed += elapsedSeconds; // 경과 시간 추가
                lastPwmTime = currentTime;    // 마지막 PWM 발생 시간 갱신

                // PWM 신호 생성
                if (pwmStateHigh && pwmElapsed >= highTime)
                {
                    outputVoltage = LowV; // 신호를 Low로 설정
                    writer.WriteSingleSample(true, LowV); // 실제 전압 출력
                    pwmStateHigh = false;
                    pwmElapsed = 0;
                }
                else if (!pwmStateHigh && pwmElapsed >= lowTime)
                {
                    outputVoltage = HighV; // 신호를 High로 설정
                    writer.WriteSingleSample(true, HighV); // 실제 전압 출력
                    pwmStateHigh = true;
                    pwmElapsed = 0;
                }

                // AI 채널에서 PWM 신호 읽기
                inputVoltage = analogReader.ReadSingleSample();

                // 주기 및 듀티 계산
                if (previousVoltage <= (LowV+0.1) && inputVoltage >= (HighV-0.1))
                {
                    TimeSpan periodTime = currentTime - lastEdgeTime; // 주기 계산
                    lastEdgeTime = currentTime;

                    double period = periodTime.TotalSeconds * 1000; // 주기(ms)
                    double calculatedFrequency = 1000 / period; // 주파수(Hz)

                    // 실제 듀티 사이클 계산
                    double actualDutyCycle = (highTime / periodTime.TotalSeconds) * 100;

                    // 로그 출력으로 값 확인
                    Console.WriteLine("주기: {period} ms, 주파수: {calculatedFrequency} Hz, 듀티: {actualDutyCycle}%");

                    // UI에 주기, 주파수, 듀티 사이클 값 업데이트 (Invoke로 UI 스레드에서 업데이트)
                    this.Invoke((MethodInvoker)delegate
                    {
                        lblPeriod.Text = period.ToString("F2");  // 주기(ms)
                        lblFrequency.Text = calculatedFrequency.ToString("F2"); // 주파수(Hz)
                        lblDuty.Text = actualDutyCycle.ToString("F2");   // 듀티 사이클(%)
                    });
                }

                previousVoltage = inputVoltage; // 이전 전압 상태 갱신

                // 실시간 그래프 업데이트
                this.Invoke((MethodInvoker)delegate
                {
                    ContinuousWfg.PlotYAppend(inputVoltage, elapsedSeconds);
                });

                // 1ms 대기
                await System.Threading.Tasks.Task.Delay(1);
            }
        }

        // PWM 주기 및 듀티 사이클 업데이트
        private void UpdatePWMParameters()
        {
            double period = 1.0 / frequency; // 전체 주기(초)
            highTime = period * (dutyCycle / 100.0); // 듀티 사이클에 따른 High 상태 유지 시간
            lowTime = period - highTime; // 나머지 시간은 Low 상태 유지
            pwmElapsed = 0;
            pwmStateHigh = true; // 초기 상태를 High로 설정
        }

        // Apply 버튼 클릭 시, Edit에서 입력받은 값을 가져와 PWM 파라미터 업데이트
        private void ApplyButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                frequency = 1000 / (double)PeriodEdit.Value; // 주기(ms)의 역수로 주파수 계산
                dutyCycle = (double)DutyEdit.Value;   // 듀티 사이클 (%)
                HighV = (double)HighEdit.Value;       // 최대 전압
                LowV = (double)LowEdit.Value;         // 최소 전압

                UpdatePWMParameters(); // 새로운 파라미터로 PWM 신호 설정

                MessageBox.Show("파라미터 업데이트 완료");
            }
            catch (Exception ex)
            {
                MessageBox.Show("파라미터 업데이트 중 오류 발생: " + ex.Message);
            }
        }

        // 스위치 상태 변경에 따라 PWM 신호 제어
        private void switch1_StateChanged(object sender, NationalInstruments.UI.ActionEventArgs e)
        {
            if (!flag)
            {
                StartMultimediaTimer(); // 비동기식으로 PWM 및 AI 데이터 처리 시작
            }
            else
            {
                StopMultimediaTimer(); // 타이머 중지
            }
        }

        private void CaptureButton_Click_1(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, EventArgs>(CaptureButton_Click_1), sender, e);
                return;
            }

            List<double> capturedSignal = new List<double>();  // 한 주기의 신호를 캡처할 리스트

            // Parameter 기반으로 주기와 듀티 사이클 가져오기
            double calculatedPeriod = double.Parse(lblPeriod.Text);      // 주기 (ms)
            double calculatedDutyCycle = double.Parse(lblDuty.Text);     // 듀티 사이클 (%)

            // High와 Low 상태에서 유지해야 할 시간 계산
            double highTime = (calculatedDutyCycle / 100.0) * calculatedPeriod;  // High 상태 유지 시간 (ms)
            double lowTime = calculatedPeriod - highTime;                        // Low 상태 유지 시간 (ms)

            // 샘플링 시간 (1ms 간격으로 샘플링)
            double timeStep = 1.0;  // 샘플링 간격을 1ms로 설정

            // 한 주기 동안 신호 캡처 (highTime 동안 HighV, lowTime 동안 LowV)
            for (double t = 0; t < calculatedPeriod; t += timeStep)
            {
                if (t < highTime)
                {
                    // High 상태 유지
                    capturedSignal.Add(HighV);
                }
                else
                {
                    // Low 상태 유지
                    capturedSignal.Add(LowV);
                }
            }

            // 캡처된 신호가 있으면 CaptureWfg에 출력
            if (capturedSignal.Count > 0)
            {
                CaptureWfg.PlotY(capturedSignal.ToArray()); // 캡처된 신호를 그래프에 출력
            }
        }


        private void ResetButton_Click(object sender, EventArgs e)
        {
            StopMultimediaTimer(); // 타이머 중지
            writer.WriteSingleSample(true, 0); // PWM 신호를 0V로 설정
            ContinuousWfg.ClearData(); // 그래프 초기화
        }
    }
}
