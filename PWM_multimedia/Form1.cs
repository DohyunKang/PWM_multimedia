using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
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

        private double outputVoltage;
        private double inputVoltage;
        private double frequency;
        private double dutyCycle;
        private bool pwmStateHigh;
        private double highTime;
        private double lowTime;
        private double pwmElapsed;

        private double HighV;
        private double LowV;

        private DateTime lastPwmTime = DateTime.Now;
        private DateTime cycleStartTime = DateTime.Now;

        private double previousVoltage = 0;
        private DateTime lastEdgeTime = DateTime.Now;

        private bool flag = false;
        private int pwmIndex = 0; // 프로그램 종료 시 0로 초기화됨
        private int currentPwmId = 0; // 자동 증가되는 p_index 관리

        public Form1()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            writeTask = new NationalInstruments.DAQmx.Task();
            analogReadTask = new NationalInstruments.DAQmx.Task();

            writeTask.AOChannels.CreateVoltageChannel("Dev1/ao0", "", 0.0, 5.0, AOVoltageUnits.Volts);
            analogReadTask.AIChannels.CreateVoltageChannel("Dev1/ai0", "", AITerminalConfiguration.Rse, 0.0, 5.0, AIVoltageUnits.Volts);

            writer = new AnalogSingleChannelWriter(writeTask.Stream);
            analogReader = new AnalogSingleChannelReader(analogReadTask.Stream);

            frequency = 50;
            dutyCycle = 50;
            HighV = 5;
            LowV = 0;

            UpdatePWMParameters();
        }

        private void StartMultimediaTimer()
        {
            System.Threading.Tasks.Task.Run(() => GeneratePWMAndReadAIAsync());
        }

        private void StopMultimediaTimer()
        {
            flag = false;
        }

        private async System.Threading.Tasks.Task GeneratePWMAndReadAIAsync()
        {
            flag = true;
            while (flag)
            {
                DateTime currentTime = DateTime.Now;
                TimeSpan deltaTime = currentTime - lastPwmTime;
                double elapsedSeconds = deltaTime.TotalSeconds;

                pwmElapsed += elapsedSeconds;
                lastPwmTime = currentTime;

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

                inputVoltage = analogReader.ReadSingleSample();

                if (previousVoltage <= (LowV + 0.1) && inputVoltage >= (HighV - 0.1))
                {
                    TimeSpan periodTime = currentTime - lastEdgeTime;
                    lastEdgeTime = currentTime;

                    double period = periodTime.TotalSeconds * 1000;
                    double calculatedFrequency = 1000 / period;

                    double actualDutyCycle = (highTime / periodTime.TotalSeconds) * 100;

                    this.Invoke((MethodInvoker)delegate
                    {
                        lblPeriod.Text = period.ToString("F2");
                        lblFrequency.Text = calculatedFrequency.ToString("F2");
                        lblDuty.Text = actualDutyCycle.ToString("F2");
                    });
                }

                previousVoltage = inputVoltage;

                this.Invoke((MethodInvoker)delegate
                {
                    ContinuousWfg.PlotYAppend(inputVoltage, elapsedSeconds);
                });

                await System.Threading.Tasks.Task.Delay(1);
            }
        }

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
                frequency = 1000 / (double)PeriodEdit.Value;
                dutyCycle = (double)DutyEdit.Value;
                HighV = (double)HighEdit.Value;
                LowV = (double)LowEdit.Value;

                UpdatePWMParameters();

                pwmIndex++; // Apply할 때마다 증가
                currentPwmId = InsertPwmDataToDatabase((float)PeriodEdit.Value, (float)(1000 / PeriodEdit.Value), (float)HighEdit.Value, (float)DutyEdit.Value, 1, pwmIndex);

                MessageBox.Show("파라미터 업데이트 완료");
            }
            catch (Exception ex)
            {
                MessageBox.Show("파라미터 업데이트 중 오류 발생: " + ex.Message);
            }
        }

        private void CaptureButton_Click_1(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, EventArgs>(CaptureButton_Click_1), sender, e);
                return;
            }

            double calculatedPeriod = double.Parse(lblPeriod.Text);
            double calculatedDutyCycle = double.Parse(lblDuty.Text);

            double highTime = (calculatedDutyCycle / 100.0) * calculatedPeriod;
            double lowTime = calculatedPeriod - highTime;

            List<double> capturedSignal = new List<double>();
            double timeStep = 1.0;

            for (double t = 0; t < calculatedPeriod; t += timeStep)
            {
                if (t < highTime)
                {
                    capturedSignal.Add(HighV);
                }
                else
                {
                    capturedSignal.Add(LowV);
                }
            }

            if (capturedSignal.Count > 0)
            {
                CaptureWfg.PlotY(capturedSignal.ToArray());

                // Capture 시점의 p_index 값을 c_index로 참조
                int pIndexFromDatabase = GetLastInsertedPwmIndex();
                InsertCalculateDataToDatabase((float)calculatedPeriod, (float)(1000 / calculatedPeriod), (float)HighV, (float)calculatedDutyCycle, pIndexFromDatabase, pwmIndex);
            }
        }

        private int GetLastInsertedPwmIndex()
        {
            try
            {
                using (SqlCeConnection conn = new SqlCeConnection(@"Data Source = C:\Users\kangdohyun\Desktop\세미나\2주차\PWM_multimedia\MyDatabase#1.sdf"))
                {
                    conn.Open();

                    string query = "SELECT MAX(p_index) FROM PwmData";
                    using (SqlCeCommand cmd = new SqlCeCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        return (int)(result ?? 0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("p_index 조회 중 오류 발생: " + ex.Message);
                return -1;
            }
        }

        private int InsertPwmDataToDatabase(float period, float frequency, float voltage, float duty, int switchState, int pwmIndex)
        {
            try
            {
                using (SqlCeConnection conn = new SqlCeConnection(@"Data Source = C:\Users\kangdohyun\Desktop\세미나\2주차\PWM_multimedia\MyDatabase#1.sdf"))
                {
                    conn.Open();

                    string query = "INSERT INTO PwmData (Period, Frequency, Voltage, Duty, Switch, Time, pwmIndex) VALUES (@Period, @Frequency, @Voltage, @Duty, @Switch, @Time, @pwmIndex)";
                    using (SqlCeCommand cmd = new SqlCeCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Period", period);
                        cmd.Parameters.AddWithValue("@Frequency", frequency);
                        cmd.Parameters.AddWithValue("@Voltage", voltage);
                        cmd.Parameters.AddWithValue("@Duty", duty);
                        cmd.Parameters.AddWithValue("@Switch", switchState);
                        cmd.Parameters.AddWithValue("@Time", lastPwmTime);
                        cmd.Parameters.AddWithValue("@pwmIndex", pwmIndex); // Apply 누를 때 증가하는 pwmIndex 저장

                        cmd.ExecuteNonQuery();
                    }

                    return GetLastInsertedPwmIndex();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("데이터 삽입 중 오류 발생: " + ex.Message);
                return -1;
            }
        }

        private int InsertCalculateDataToDatabase(float c_period, float c_frequency, float c_voltage, float c_duty, int p_index, int pwmIndex)
        {
            try
            {
                using (SqlCeConnection conn = new SqlCeConnection(@"Data Source = C:\Users\kangdohyun\Desktop\세미나\2주차\PWM_multimedia\MyDatabase#1.sdf"))
                {
                    conn.Open();

                    string query = "INSERT INTO Calculate (C_Period, C_Frequency, C_Voltage, C_Duty, Time, c_index, pwmIndex) VALUES (@C_Period, @C_Frequency, @C_Voltage, @C_Duty, @Time, @c_index, @pwmIndex)";
                    using (SqlCeCommand cmd = new SqlCeCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@C_Period", c_period);
                        cmd.Parameters.AddWithValue("@C_Frequency", c_frequency);
                        cmd.Parameters.AddWithValue("@C_Voltage", c_voltage);
                        cmd.Parameters.AddWithValue("@C_Duty", c_duty);
                        cmd.Parameters.AddWithValue("@Time", lastPwmTime);
                        cmd.Parameters.AddWithValue("@c_index", p_index); // c_index가 p_index를 참조
                        cmd.Parameters.AddWithValue("@pwmIndex", pwmIndex); // pwmIndex도 저장

                        cmd.ExecuteNonQuery();
                    }

                    return p_index;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Calculate 데이터 삽입 중 오류 발생: " + ex.Message);
                return -1;
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            StopMultimediaTimer();
            writer.WriteSingleSample(true, 0);
            ContinuousWfg.ClearData();
        }

        private void switch1_StateChanged(object sender, EventArgs e)
        {
            if (!flag)
            {
                StartMultimediaTimer(); // 타이머 시작
                flag = true; // PWM 신호를 활성화 상태로 설정
            }
            else
            {
                StopMultimediaTimer(); // 타이머 중지
                flag = false; // PWM 신호를 비활성화 상태로 설정
            }
        }
    }
}
