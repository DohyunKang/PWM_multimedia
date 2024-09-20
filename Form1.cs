using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NationalInstruments.DAQmx;
using System.Collections.Generic;
using System.Data.SqlServerCe;

namespace PWM_multimedia
{
    public partial class Form1 : Form
    {
        private NationalInstruments.DAQmx.Task writeTask;
        private NationalInstruments.DAQmx.Task analogReadTask;
        private AnalogSingleChannelWriter writer;
        private AnalogSingleChannelReader analogReader;

        private double frequency;
        private double dutyCycle;
        private bool pwmStateHigh;
        private double highTime;
        private double lowTime;
        private double pwmElapsed;

        private double HighV;
        private double LowV;
        private double outputVoltage;

        private int timerID;
        private bool flag = false;

        // Multimedia Timer 콜백 선언
        private TimerCallback timerCallbackDelegate;

        // Multimedia Timer P/Invoke 선언
        [DllImport("winmm.dll", SetLastError = true)]
        private static extern uint timeBeginPeriod(uint period);
        [DllImport("winmm.dll", SetLastError = true)]
        private static extern uint timeEndPeriod(uint period);
        [DllImport("winmm.dll", SetLastError = true)]
        private static extern int timeSetEvent(uint msDelay, uint msResolution, TimerCallback callback, IntPtr user, uint eventType);
        [DllImport("winmm.dll", SetLastError = true)]
        private static extern int timeKillEvent(int uTimerID);

        private delegate void TimerCallback(uint uTimerID, uint uMsg, IntPtr dwUser, IntPtr dw1, IntPtr dw2);

        public Form1()
        {
            InitializeComponent();
            timerCallbackDelegate = new TimerCallback(TimerTick);
            Init();
        }

        public void Init()
        {
            try
            {
                writeTask = new NationalInstruments.DAQmx.Task();
                analogReadTask = new NationalInstruments.DAQmx.Task();
                analogReadTask.AIChannels.CreateVoltageChannel("Dev1/ai0", "", AITerminalConfiguration.Rse, 0.0, 5.0, AIVoltageUnits.Volts);
                writeTask.AOChannels.CreateVoltageChannel("Dev1/ao0", "", 0.0, 5.0, AOVoltageUnits.Volts);
                writer = new AnalogSingleChannelWriter(writeTask.Stream);
                analogReader = new AnalogSingleChannelReader(analogReadTask.Stream);
                writer.WriteSingleSample(true, 0);

                frequency = 50;  // 기본 주파수
                dutyCycle = 10;  // 기본 듀티 사이클
                HighV = 5;       // 기본 최대 전압
                LowV = 0;        // 기본 최소 전압

                UpdatePWMParameters();
                timeBeginPeriod(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("DAQ 초기화 중 오류 발생: " + ex.Message);
            }
        }

        private void StartMultimediaTimer()
        {
            if (timerID == 0)
            {
                // 타이머 간격을 주기(period)로 설정
                timerID = timeSetEvent((uint)(highTime * 1000), 1, timerCallbackDelegate, IntPtr.Zero, 1);
            }
        }

        private void StopMultimediaTimer()
        {
            if (timerID != 0)
            {
                timeKillEvent(timerID);
                timerID = 0;
            }
        }

        private void TimerTick(uint uTimerID, uint uMsg, IntPtr dwUser, IntPtr dw1, IntPtr dw2)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<uint, uint, IntPtr, IntPtr, IntPtr>(TimerTick), uTimerID, uMsg, dwUser, dw1, dw2);
                    return;
                }

                pwmElapsed += 1.0 / 1000.0; // 1ms 간격으로 호출

                if (pwmStateHigh && pwmElapsed >= highTime)
                {
                    outputVoltage = LowV;
                    writer.WriteSingleSample(true, LowV);
                    pwmStateHigh = false;
                    pwmElapsed = 0;
                }
                else if (!pwmStateHigh && pwmElapsed >= lowTime)
                {
                    outputVoltage = HighV;
                    writer.WriteSingleSample(true, HighV);
                    pwmStateHigh = true;
                    pwmElapsed = 0;
                }

                ContinuousWfg.PlotYAppend(outputVoltage); // 그래프 업데이트
                SavePwmDataToDatabase(outputVoltage); // 데이터베이스 저장
            }
            catch (Exception ex)
            {
                StopMultimediaTimer();
                MessageBox.Show("타이머 처리 중 오류 발생: " + ex.Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            writer.WriteSingleSample(true, 0);
            if (writeTask != null) writeTask.Dispose();
            if (analogReadTask != null) analogReadTask.Dispose();
            StopMultimediaTimer();
            timeEndPeriod(1);
        }

        // PWM 파라미터 업데이트
        private void UpdatePWMParameters()
        {
            double period = 1.0 / frequency;
            highTime = period * (dutyCycle / 100.0);
            lowTime = period - highTime;
            pwmElapsed = 0;
            pwmStateHigh = true;
        }

        private void ApplyButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                frequency = (double)FrequencyEdit.Value;
                dutyCycle = (double)DutyEdit.Value;
                HighV = (double)HighEdit.Value;
                LowV = (double)LowEdit.Value;

                UpdatePWMParameters();
                lblPeriod.Text = (1000 / frequency).ToString("F2") + " ms"; // 주기(ms)로 표시
                lblFrequency.Text = frequency.ToString("F2") + " Hz";
                lblDuty.Text = dutyCycle.ToString("F2") + " %";

                MessageBox.Show("파라미터 업데이트 완료");
            }
            catch (Exception ex)
            {
                MessageBox.Show("파라미터 업데이트 중 오류 발생: " + ex.Message);
            }
        }

        private void switch1_StateChanged(object sender, NationalInstruments.UI.ActionEventArgs e)
        {
            if (!flag)
            {
                StartMultimediaTimer();
                flag = true;
                SavePwmDataToDatabase(outputVoltage); // 스위치 상태 저장
            }
            else
            {
                StopMultimediaTimer();
                writer.WriteSingleSample(true, 0);
                ContinuousWfg.ClearData();
                flag = false;
            }
        }

        private void CaptureButton_Click_1(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, EventArgs>(CaptureButton_Click_1), sender, e);
                return;
            }

            List<double> capturedSignal = new List<double>();
            double timeStep = 1.0 / 1000.0;

            for (double t = 0; t < (highTime + lowTime); t += timeStep)
            {
                capturedSignal.Add(t < highTime ? HighV : LowV);
            }

            CaptureWfg.PlotY(capturedSignal.ToArray());
        }

        // PWM 데이터를 데이터베이스에 저장
        public void SavePwmDataToDatabase(double voltage)
        {
            string connectionString = @"Data Source=C:\Users\kangdohyun\Desktop\세미나\2주차\PWM_multimedia\MyDatabase#1.sdf";
            try
            {
                using (SqlCeConnection conn = new SqlCeConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO PwmData (Period, Frequency, Voltage, Duty, Switch, Time) " +
                                   "VALUES (@Period, @Frequency, @Voltage, @Duty, @Switch, @Time)";
                    using (SqlCeCommand cmd = new SqlCeCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Period", 1000 / frequency);
                        cmd.Parameters.AddWithValue("@Frequency", frequency);
                        cmd.Parameters.AddWithValue("@Voltage", voltage);
                        cmd.Parameters.AddWithValue("@Duty", dutyCycle);
                        cmd.Parameters.AddWithValue("@Switch", flag ? 1 : 0);
                        cmd.Parameters.AddWithValue("@Time", DateTime.Now);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("데이터베이스 저장 중 오류 발생: " + ex.Message);
            }
        }
    }
}
