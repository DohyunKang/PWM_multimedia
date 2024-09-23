namespace PWM_multimedia
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.switch1 = new NationalInstruments.UI.WindowsForms.Switch();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ResetButton = new System.Windows.Forms.Button();
            this.LowEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.HighEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.DutyEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.PeriodEdit = new NationalInstruments.UI.WindowsForms.NumericEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.CaptureButton = new System.Windows.Forms.Button();
            this.CaptureWfg = new NationalInstruments.UI.WindowsForms.WaveformGraph();
            this.waveformPlot2 = new NationalInstruments.UI.WaveformPlot();
            this.xAxis2 = new NationalInstruments.UI.XAxis();
            this.yAxis2 = new NationalInstruments.UI.YAxis();
            this.ContinuousWfg = new NationalInstruments.UI.WindowsForms.WaveformGraph();
            this.waveformPlot1 = new NationalInstruments.UI.WaveformPlot();
            this.xAxis1 = new NationalInstruments.UI.XAxis();
            this.yAxis1 = new NationalInstruments.UI.YAxis();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lblDuty = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblFrequency = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.switch1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LowEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HighEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DutyEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PeriodEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CaptureWfg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContinuousWfg)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // switch1
            // 
            this.switch1.Location = new System.Drawing.Point(955, 387);
            this.switch1.Name = "switch1";
            this.switch1.OffColor = System.Drawing.Color.Yellow;
            this.switch1.OnColor = System.Drawing.Color.DeepSkyBlue;
            this.switch1.Size = new System.Drawing.Size(118, 96);
            this.switch1.SwitchStyle = NationalInstruments.UI.SwitchStyle.HorizontalSlide3D;
            this.switch1.TabIndex = 3;
            this.switch1.StateChanged += new NationalInstruments.UI.ActionEventHandler(this.switch1_StateChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(976, 484);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "Start/Stop";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ResetButton);
            this.groupBox1.Controls.Add(this.LowEdit);
            this.groupBox1.Controls.Add(this.HighEdit);
            this.groupBox1.Controls.Add(this.DutyEdit);
            this.groupBox1.Controls.Add(this.PeriodEdit);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ApplyButton);
            this.groupBox1.Controls.Add(this.CaptureButton);
            this.groupBox1.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.Location = new System.Drawing.Point(879, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 372);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control";
            // 
            // ResetButton
            // 
            this.ResetButton.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ResetButton.Location = new System.Drawing.Point(100, 327);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 39);
            this.ResetButton.TabIndex = 17;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // LowEdit
            // 
            this.LowEdit.Location = new System.Drawing.Point(100, 244);
            this.LowEdit.Name = "LowEdit";
            this.LowEdit.Size = new System.Drawing.Size(120, 25);
            this.LowEdit.TabIndex = 16;
            // 
            // HighEdit
            // 
            this.HighEdit.Location = new System.Drawing.Point(100, 184);
            this.HighEdit.Name = "HighEdit";
            this.HighEdit.Size = new System.Drawing.Size(120, 25);
            this.HighEdit.TabIndex = 15;
            // 
            // DutyEdit
            // 
            this.DutyEdit.Location = new System.Drawing.Point(100, 126);
            this.DutyEdit.Name = "DutyEdit";
            this.DutyEdit.Size = new System.Drawing.Size(120, 25);
            this.DutyEdit.TabIndex = 14;
            // 
            // PeriodEdit
            // 
            this.PeriodEdit.Location = new System.Drawing.Point(100, 68);
            this.PeriodEdit.Name = "PeriodEdit";
            this.PeriodEdit.Size = new System.Drawing.Size(120, 25);
            this.PeriodEdit.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(19, 244);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "LowVolt";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(17, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "HighVolt";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(18, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "듀티(%)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(14, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "주기(ms)";
            // 
            // ApplyButton
            // 
            this.ApplyButton.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ApplyButton.Location = new System.Drawing.Point(171, 280);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(75, 39);
            this.ApplyButton.TabIndex = 3;
            this.ApplyButton.Text = "Apply";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click_1);
            // 
            // CaptureButton
            // 
            this.CaptureButton.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.CaptureButton.Location = new System.Drawing.Point(22, 280);
            this.CaptureButton.Name = "CaptureButton";
            this.CaptureButton.Size = new System.Drawing.Size(75, 39);
            this.CaptureButton.TabIndex = 2;
            this.CaptureButton.Text = "Capture";
            this.CaptureButton.UseVisualStyleBackColor = true;
            this.CaptureButton.Click += new System.EventHandler(this.CaptureButton_Click_1);
            // 
            // CaptureWfg
            // 
            this.CaptureWfg.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.CaptureWfg.Location = new System.Drawing.Point(55, 221);
            this.CaptureWfg.Name = "CaptureWfg";
            this.CaptureWfg.Plots.AddRange(new NationalInstruments.UI.WaveformPlot[] {
            this.waveformPlot2});
            this.CaptureWfg.Size = new System.Drawing.Size(751, 168);
            this.CaptureWfg.TabIndex = 21;
            this.CaptureWfg.UseColorGenerator = true;
            this.CaptureWfg.XAxes.AddRange(new NationalInstruments.UI.XAxis[] {
            this.xAxis2});
            this.CaptureWfg.YAxes.AddRange(new NationalInstruments.UI.YAxis[] {
            this.yAxis2});
            // 
            // waveformPlot2
            // 
            this.waveformPlot2.XAxis = this.xAxis2;
            this.waveformPlot2.YAxis = this.yAxis2;
            // 
            // ContinuousWfg
            // 
            this.ContinuousWfg.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ContinuousWfg.Location = new System.Drawing.Point(55, 12);
            this.ContinuousWfg.Name = "ContinuousWfg";
            this.ContinuousWfg.Plots.AddRange(new NationalInstruments.UI.WaveformPlot[] {
            this.waveformPlot1});
            this.ContinuousWfg.Size = new System.Drawing.Size(751, 168);
            this.ContinuousWfg.TabIndex = 20;
            this.ContinuousWfg.UseColorGenerator = true;
            this.ContinuousWfg.XAxes.AddRange(new NationalInstruments.UI.XAxis[] {
            this.xAxis1});
            this.ContinuousWfg.YAxes.AddRange(new NationalInstruments.UI.YAxis[] {
            this.yAxis1});
            // 
            // waveformPlot1
            // 
            this.waveformPlot1.HistoryCapacity = 10000;
            this.waveformPlot1.XAxis = this.xAxis1;
            this.waveformPlot1.YAxis = this.yAxis1;
            // 
            // xAxis1
            // 
            this.xAxis1.Mode = NationalInstruments.UI.AxisMode.StripChart;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.lblDuty);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.lblFrequency);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.lblPeriod);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox2.Location = new System.Drawing.Point(76, 415);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(704, 86);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Parameter";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(644, 41);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(19, 15);
            this.label11.TabIndex = 8;
            this.label11.Text = "%";
            // 
            // lblDuty
            // 
            this.lblDuty.AutoSize = true;
            this.lblDuty.Location = new System.Drawing.Point(575, 41);
            this.lblDuty.Name = "lblDuty";
            this.lblDuty.Size = new System.Drawing.Size(16, 15);
            this.lblDuty.TabIndex = 7;
            this.lblDuty.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(494, 41);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(39, 15);
            this.label13.TabIndex = 6;
            this.label13.Text = "듀티";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(415, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 15);
            this.label8.TabIndex = 5;
            this.label8.Text = "Hz";
            // 
            // lblFrequency
            // 
            this.lblFrequency.AutoSize = true;
            this.lblFrequency.Location = new System.Drawing.Point(344, 41);
            this.lblFrequency.Name = "lblFrequency";
            this.lblFrequency.Size = new System.Drawing.Size(16, 15);
            this.lblFrequency.TabIndex = 4;
            this.lblFrequency.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(249, 41);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 15);
            this.label10.TabIndex = 3;
            this.label10.Text = "주파수";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(179, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "ms";
            // 
            // lblPeriod
            // 
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Location = new System.Drawing.Point(112, 41);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(16, 15);
            this.lblPeriod.TabIndex = 1;
            this.lblPeriod.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "주기";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.Location = new System.Drawing.Point(812, 168);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 16);
            this.label9.TabIndex = 9;
            this.label9.Text = "(s)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label12.Location = new System.Drawing.Point(812, 377);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(46, 16);
            this.label12.TabIndex = 22;
            this.label12.Text = "(ms)";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label14.Location = new System.Drawing.Point(12, 12);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(33, 16);
            this.label14.TabIndex = 23;
            this.label14.Text = "(V)";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label15.Location = new System.Drawing.Point(12, 221);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(33, 16);
            this.label15.TabIndex = 24;
            this.label15.Text = "(V)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1182, 508);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.CaptureWfg);
            this.Controls.Add(this.ContinuousWfg);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.switch1);
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.switch1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LowEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HighEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DutyEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PeriodEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CaptureWfg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContinuousWfg)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NationalInstruments.UI.WindowsForms.Switch switch1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private NationalInstruments.UI.WindowsForms.NumericEdit LowEdit;
        private NationalInstruments.UI.WindowsForms.NumericEdit HighEdit;
        private NationalInstruments.UI.WindowsForms.NumericEdit DutyEdit;
        private NationalInstruments.UI.WindowsForms.NumericEdit PeriodEdit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.Button CaptureButton;
        private NationalInstruments.UI.WindowsForms.WaveformGraph CaptureWfg;
        private NationalInstruments.UI.WaveformPlot waveformPlot2;
        private NationalInstruments.UI.XAxis xAxis2;
        private NationalInstruments.UI.YAxis yAxis2;
        private NationalInstruments.UI.WindowsForms.WaveformGraph ContinuousWfg;
        private NationalInstruments.UI.WaveformPlot waveformPlot1;
        private NationalInstruments.UI.XAxis xAxis1;
        private NationalInstruments.UI.YAxis yAxis1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblDuty;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblFrequency;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button ResetButton;
    }
}

