namespace MZM_Rev_Aug_04
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cmdConnect = new System.Windows.Forms.Button();
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.cmdDisconnect = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblConnected = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtQRCode = new System.Windows.Forms.TextBox();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lbAlarm = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAlmReset = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.almLedOutput = new LBSoft.IndustrialCtrls.Leds.LBLed();
            this.label15 = new System.Windows.Forms.Label();
            this.almLedDB = new LBSoft.IndustrialCtrls.Leds.LBLed();
            this.almLedCAM = new LBSoft.IndustrialCtrls.Leds.LBLed();
            this.almLedWeight = new LBSoft.IndustrialCtrls.Leds.LBLed();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.LblLocalTime = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.LblLocalDate = new System.Windows.Forms.Label();
            this.onlineLedCAM_2 = new LBSoft.IndustrialCtrls.Leds.LBLed();
            this.label1 = new System.Windows.Forms.Label();
            this.onlineLedDB = new LBSoft.IndustrialCtrls.Leds.LBLed();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.onlineLedCAM = new LBSoft.IndustrialCtrls.Leds.LBLed();
            this.onlineLedWeight = new LBSoft.IndustrialCtrls.Leds.LBLed();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnImg2 = new System.Windows.Forms.Button();
            this.btnLog = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnImg = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtQRCode2 = new System.Windows.Forms.TextBox();
            this.imgBox2 = new System.Windows.Forms.PictureBox();
            this.txtQRCode1 = new System.Windows.Forms.TextBox();
            this.imgBox = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnDBConnect = new System.Windows.Forms.Button();
            this.dbDataSet = new MZM_Rev_Aug_04.dbDataSet();
            this.dbDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.lblStatus2 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblConnected2 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.tcpServer1 = new tcpServer.TcpServer(this.components);
            this.tcpServer2 = new tcpServer.TcpServer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbDataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdConnect
            // 
            this.cmdConnect.Location = new System.Drawing.Point(376, 600);
            this.cmdConnect.Name = "cmdConnect";
            this.cmdConnect.Size = new System.Drawing.Size(125, 40);
            this.cmdConnect.TabIndex = 0;
            this.cmdConnect.Text = "Connect";
            this.cmdConnect.UseVisualStyleBackColor = true;
            this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
            // 
            // txtDisplay
            // 
            this.txtDisplay.Location = new System.Drawing.Point(798, 616);
            this.txtDisplay.Multiline = true;
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.Size = new System.Drawing.Size(78, 22);
            this.txtDisplay.TabIndex = 1;
            // 
            // cmdDisconnect
            // 
            this.cmdDisconnect.Location = new System.Drawing.Point(507, 600);
            this.cmdDisconnect.Name = "cmdDisconnect";
            this.cmdDisconnect.Size = new System.Drawing.Size(125, 40);
            this.cmdDisconnect.TabIndex = 2;
            this.cmdDisconnect.Text = "DisConnect";
            this.cmdDisconnect.UseVisualStyleBackColor = true;
            this.cmdDisconnect.Click += new System.EventHandler(this.cmdDisconnect_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(125, 831);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(96, 13);
            this.lblStatus.TabIndex = 26;
            this.lblStatus.Text = "PORT NOT OPEN";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 831);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Camera status:";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblConnected
            // 
            this.lblConnected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblConnected.AutoSize = true;
            this.lblConnected.BackColor = System.Drawing.SystemColors.Control;
            this.lblConnected.Location = new System.Drawing.Point(328, 831);
            this.lblConnected.Name = "lblConnected";
            this.lblConnected.Size = new System.Drawing.Size(13, 13);
            this.lblConnected.TabIndex = 34;
            this.lblConnected.Text = "0";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(232, 831);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 13);
            this.label9.TabIndex = 33;
            this.label9.Text = "Connected Conn:";
            // 
            // txtQRCode
            // 
            this.txtQRCode.BackColor = System.Drawing.Color.Green;
            this.txtQRCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQRCode.Font = new System.Drawing.Font("Impact", 33F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQRCode.ForeColor = System.Drawing.Color.White;
            this.txtQRCode.Location = new System.Drawing.Point(52, 61);
            this.txtQRCode.Name = "txtQRCode";
            this.txtQRCode.Size = new System.Drawing.Size(509, 61);
            this.txtQRCode.TabIndex = 35;
            this.txtQRCode.Text = "HKWB0001";
            this.txtQRCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtWeight
            // 
            this.txtWeight.BackColor = System.Drawing.Color.Green;
            this.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWeight.Font = new System.Drawing.Font("Impact", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWeight.ForeColor = System.Drawing.Color.White;
            this.txtWeight.Location = new System.Drawing.Point(64, 61);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(509, 66);
            this.txtWeight.TabIndex = 36;
            this.txtWeight.Text = "45.678";
            this.txtWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightGray;
            this.groupBox1.Controls.Add(this.groupBox12);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox9);
            this.groupBox1.Controls.Add(this.groupBox8);
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.groupBox1.Location = new System.Drawing.Point(7, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1895, 983);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.groupBox3);
            this.groupBox12.Controls.Add(this.groupBox2);
            this.groupBox12.Location = new System.Drawing.Point(576, 651);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(1298, 208);
            this.groupBox12.TabIndex = 55;
            this.groupBox12.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtWeight);
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Black;
            this.groupBox3.Location = new System.Drawing.Point(661, 22);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(631, 166);
            this.groupBox3.TabIndex = 38;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Weight";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtQRCode);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(23, 22);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(609, 166);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "QR Number ";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lbAlarm);
            this.groupBox4.Location = new System.Drawing.Point(15, 651);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(549, 107);
            this.groupBox4.TabIndex = 58;
            this.groupBox4.TabStop = false;
            // 
            // lbAlarm
            // 
            this.lbAlarm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbAlarm.AutoSize = true;
            this.lbAlarm.BackColor = System.Drawing.Color.LightGray;
            this.lbAlarm.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAlarm.ForeColor = System.Drawing.Color.Red;
            this.lbAlarm.Location = new System.Drawing.Point(14, 52);
            this.lbAlarm.Name = "lbAlarm";
            this.lbAlarm.Size = new System.Drawing.Size(17, 13);
            this.lbAlarm.TabIndex = 57;
            this.lbAlarm.Text = "--";
            this.lbAlarm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label16);
            this.groupBox9.Controls.Add(this.label5);
            this.groupBox9.Controls.Add(this.btnAlmReset);
            this.groupBox9.Controls.Add(this.label12);
            this.groupBox9.Controls.Add(this.almLedOutput);
            this.groupBox9.Controls.Add(this.label15);
            this.groupBox9.Controls.Add(this.almLedDB);
            this.groupBox9.Controls.Add(this.almLedCAM);
            this.groupBox9.Controls.Add(this.almLedWeight);
            this.groupBox9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox9.ForeColor = System.Drawing.Color.Black;
            this.groupBox9.Location = new System.Drawing.Point(15, 765);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(549, 94);
            this.groupBox9.TabIndex = 49;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Alarm Status";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Gainsboro;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(316, 47);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(55, 16);
            this.label16.TabIndex = 54;
            this.label16.Text = "OUTPUT";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Gainsboro;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(229, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 16);
            this.label5.TabIndex = 49;
            this.label5.Text = "DB";
            // 
            // btnAlmReset
            // 
            this.btnAlmReset.Enabled = false;
            this.btnAlmReset.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlmReset.ForeColor = System.Drawing.Color.Red;
            this.btnAlmReset.Location = new System.Drawing.Point(396, 18);
            this.btnAlmReset.Name = "btnAlmReset";
            this.btnAlmReset.Size = new System.Drawing.Size(133, 66);
            this.btnAlmReset.TabIndex = 47;
            this.btnAlmReset.Text = "ALARM RESET";
            this.btnAlmReset.UseVisualStyleBackColor = true;
            this.btnAlmReset.Click += new System.EventHandler(this.btnAlmReset_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Gainsboro;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(119, 65);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 16);
            this.label12.TabIndex = 50;
            this.label12.Text = "WEIGHT";
            // 
            // almLedOutput
            // 
            this.almLedOutput.BackColor = System.Drawing.Color.Transparent;
            this.almLedOutput.BlinkInterval = 500;
            this.almLedOutput.Label = "";
            this.almLedOutput.LabelPosition = LBSoft.IndustrialCtrls.Leds.LBLed.LedLabelPosition.Bottom;
            this.almLedOutput.LedColor = System.Drawing.Color.Red;
            this.almLedOutput.LedSize = new System.Drawing.SizeF(75F, 75F);
            this.almLedOutput.Location = new System.Drawing.Point(306, 24);
            this.almLedOutput.Name = "almLedOutput";
            this.almLedOutput.Renderer = null;
            this.almLedOutput.Size = new System.Drawing.Size(79, 61);
            this.almLedOutput.State = LBSoft.IndustrialCtrls.Leds.LBLed.LedState.On;
            this.almLedOutput.Style = LBSoft.IndustrialCtrls.Leds.LBLed.LedStyle.Rectangular;
            this.almLedOutput.TabIndex = 48;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Gainsboro;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(19, 65);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(63, 16);
            this.label15.TabIndex = 51;
            this.label15.Text = "CAMERA";
            // 
            // almLedDB
            // 
            this.almLedDB.BackColor = System.Drawing.Color.Transparent;
            this.almLedDB.BlinkInterval = 500;
            this.almLedDB.Label = "";
            this.almLedDB.LabelPosition = LBSoft.IndustrialCtrls.Leds.LBLed.LedLabelPosition.Bottom;
            this.almLedDB.LedColor = System.Drawing.Color.Lime;
            this.almLedDB.LedSize = new System.Drawing.SizeF(30F, 30F);
            this.almLedDB.Location = new System.Drawing.Point(213, 24);
            this.almLedDB.Name = "almLedDB";
            this.almLedDB.Renderer = null;
            this.almLedDB.Size = new System.Drawing.Size(52, 61);
            this.almLedDB.State = LBSoft.IndustrialCtrls.Leds.LBLed.LedState.On;
            this.almLedDB.Style = LBSoft.IndustrialCtrls.Leds.LBLed.LedStyle.Circular;
            this.almLedDB.TabIndex = 53;
            // 
            // almLedCAM
            // 
            this.almLedCAM.BackColor = System.Drawing.Color.Transparent;
            this.almLedCAM.BlinkInterval = 500;
            this.almLedCAM.Label = "";
            this.almLedCAM.LabelPosition = LBSoft.IndustrialCtrls.Leds.LBLed.LedLabelPosition.Bottom;
            this.almLedCAM.LedColor = System.Drawing.Color.Lime;
            this.almLedCAM.LedSize = new System.Drawing.SizeF(30F, 30F);
            this.almLedCAM.Location = new System.Drawing.Point(28, 24);
            this.almLedCAM.Name = "almLedCAM";
            this.almLedCAM.Renderer = null;
            this.almLedCAM.Size = new System.Drawing.Size(52, 61);
            this.almLedCAM.State = LBSoft.IndustrialCtrls.Leds.LBLed.LedState.On;
            this.almLedCAM.Style = LBSoft.IndustrialCtrls.Leds.LBLed.LedStyle.Circular;
            this.almLedCAM.TabIndex = 48;
            // 
            // almLedWeight
            // 
            this.almLedWeight.BackColor = System.Drawing.Color.Transparent;
            this.almLedWeight.BlinkInterval = 500;
            this.almLedWeight.Label = "";
            this.almLedWeight.LabelPosition = LBSoft.IndustrialCtrls.Leds.LBLed.LedLabelPosition.Bottom;
            this.almLedWeight.LedColor = System.Drawing.Color.Lime;
            this.almLedWeight.LedSize = new System.Drawing.SizeF(30F, 30F);
            this.almLedWeight.Location = new System.Drawing.Point(119, 24);
            this.almLedWeight.Name = "almLedWeight";
            this.almLedWeight.Renderer = null;
            this.almLedWeight.Size = new System.Drawing.Size(52, 61);
            this.almLedWeight.State = LBSoft.IndustrialCtrls.Leds.LBLed.LedState.On;
            this.almLedWeight.Style = LBSoft.IndustrialCtrls.Leds.LBLed.LedStyle.Circular;
            this.almLedWeight.TabIndex = 52;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.LblLocalTime);
            this.groupBox8.Controls.Add(this.label7);
            this.groupBox8.Controls.Add(this.LblLocalDate);
            this.groupBox8.Controls.Add(this.onlineLedCAM_2);
            this.groupBox8.Controls.Add(this.label1);
            this.groupBox8.Controls.Add(this.onlineLedDB);
            this.groupBox8.Controls.Add(this.label3);
            this.groupBox8.Controls.Add(this.label2);
            this.groupBox8.Controls.Add(this.onlineLedCAM);
            this.groupBox8.Controls.Add(this.onlineLedWeight);
            this.groupBox8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.ForeColor = System.Drawing.Color.Black;
            this.groupBox8.Location = new System.Drawing.Point(15, 870);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(549, 94);
            this.groupBox8.TabIndex = 48;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Online Device";
            // 
            // LblLocalTime
            // 
            this.LblLocalTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblLocalTime.AutoSize = true;
            this.LblLocalTime.BackColor = System.Drawing.Color.LightGray;
            this.LblLocalTime.Font = new System.Drawing.Font("Impact", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblLocalTime.ForeColor = System.Drawing.Color.Green;
            this.LblLocalTime.Location = new System.Drawing.Point(397, 14);
            this.LblLocalTime.Name = "LblLocalTime";
            this.LblLocalTime.Size = new System.Drawing.Size(133, 41);
            this.LblLocalTime.TabIndex = 43;
            this.LblLocalTime.Text = "18:00:23";
            this.LblLocalTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Gainsboro;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(108, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 16);
            this.label7.TabIndex = 49;
            this.label7.Text = "CAMERA 2";
            // 
            // LblLocalDate
            // 
            this.LblLocalDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblLocalDate.AutoSize = true;
            this.LblLocalDate.BackColor = System.Drawing.Color.LightGray;
            this.LblLocalDate.Font = new System.Drawing.Font("Impact", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblLocalDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.LblLocalDate.Location = new System.Drawing.Point(398, 58);
            this.LblLocalDate.Name = "LblLocalDate";
            this.LblLocalDate.Size = new System.Drawing.Size(131, 26);
            this.LblLocalDate.TabIndex = 42;
            this.LblLocalDate.Text = " 08 /Aug/2018";
            this.LblLocalDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // onlineLedCAM_2
            // 
            this.onlineLedCAM_2.BackColor = System.Drawing.Color.Transparent;
            this.onlineLedCAM_2.BlinkInterval = 1000;
            this.onlineLedCAM_2.Label = "";
            this.onlineLedCAM_2.LabelPosition = LBSoft.IndustrialCtrls.Leds.LBLed.LedLabelPosition.Bottom;
            this.onlineLedCAM_2.LedColor = System.Drawing.Color.Red;
            this.onlineLedCAM_2.LedSize = new System.Drawing.SizeF(30F, 30F);
            this.onlineLedCAM_2.Location = new System.Drawing.Point(119, 27);
            this.onlineLedCAM_2.Name = "onlineLedCAM_2";
            this.onlineLedCAM_2.Renderer = null;
            this.onlineLedCAM_2.Size = new System.Drawing.Size(52, 61);
            this.onlineLedCAM_2.State = LBSoft.IndustrialCtrls.Leds.LBLed.LedState.On;
            this.onlineLedCAM_2.Style = LBSoft.IndustrialCtrls.Leds.LBLed.LedStyle.Circular;
            this.onlineLedCAM_2.TabIndex = 48;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(316, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 16);
            this.label1.TabIndex = 46;
            this.label1.Text = "DB";
            // 
            // onlineLedDB
            // 
            this.onlineLedDB.BackColor = System.Drawing.Color.Transparent;
            this.onlineLedDB.BlinkInterval = 1000;
            this.onlineLedDB.Label = "";
            this.onlineLedDB.LabelPosition = LBSoft.IndustrialCtrls.Leds.LBLed.LedLabelPosition.Bottom;
            this.onlineLedDB.LedColor = System.Drawing.Color.Red;
            this.onlineLedDB.LedSize = new System.Drawing.SizeF(30F, 30F);
            this.onlineLedDB.Location = new System.Drawing.Point(306, 27);
            this.onlineLedDB.Name = "onlineLedDB";
            this.onlineLedDB.Renderer = null;
            this.onlineLedDB.Size = new System.Drawing.Size(52, 61);
            this.onlineLedDB.State = LBSoft.IndustrialCtrls.Leds.LBLed.LedState.On;
            this.onlineLedDB.Style = LBSoft.IndustrialCtrls.Leds.LBLed.LedStyle.Circular;
            this.onlineLedDB.TabIndex = 47;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Gainsboro;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(210, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 16);
            this.label3.TabIndex = 46;
            this.label3.Text = "WEIGHT";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 16);
            this.label2.TabIndex = 46;
            this.label2.Text = "CAMERA 1";
            // 
            // onlineLedCAM
            // 
            this.onlineLedCAM.BackColor = System.Drawing.Color.Transparent;
            this.onlineLedCAM.BlinkInterval = 1000;
            this.onlineLedCAM.Label = "";
            this.onlineLedCAM.LabelPosition = LBSoft.IndustrialCtrls.Leds.LBLed.LedLabelPosition.Bottom;
            this.onlineLedCAM.LedColor = System.Drawing.Color.Red;
            this.onlineLedCAM.LedSize = new System.Drawing.SizeF(30F, 30F);
            this.onlineLedCAM.Location = new System.Drawing.Point(28, 27);
            this.onlineLedCAM.Name = "onlineLedCAM";
            this.onlineLedCAM.Renderer = null;
            this.onlineLedCAM.Size = new System.Drawing.Size(52, 61);
            this.onlineLedCAM.State = LBSoft.IndustrialCtrls.Leds.LBLed.LedState.On;
            this.onlineLedCAM.Style = LBSoft.IndustrialCtrls.Leds.LBLed.LedStyle.Circular;
            this.onlineLedCAM.TabIndex = 45;
            // 
            // onlineLedWeight
            // 
            this.onlineLedWeight.BackColor = System.Drawing.Color.Transparent;
            this.onlineLedWeight.BlinkInterval = 1000;
            this.onlineLedWeight.Label = "";
            this.onlineLedWeight.LabelPosition = LBSoft.IndustrialCtrls.Leds.LBLed.LedLabelPosition.Bottom;
            this.onlineLedWeight.LedColor = System.Drawing.Color.Red;
            this.onlineLedWeight.LedSize = new System.Drawing.SizeF(30F, 30F);
            this.onlineLedWeight.Location = new System.Drawing.Point(213, 27);
            this.onlineLedWeight.Name = "onlineLedWeight";
            this.onlineLedWeight.Renderer = null;
            this.onlineLedWeight.Size = new System.Drawing.Size(52, 61);
            this.onlineLedWeight.State = LBSoft.IndustrialCtrls.Leds.LBLed.LedState.On;
            this.onlineLedWeight.Style = LBSoft.IndustrialCtrls.Leds.LBLed.LedStyle.Circular;
            this.onlineLedWeight.TabIndex = 46;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnImg2);
            this.groupBox7.Controls.Add(this.btnLog);
            this.groupBox7.Controls.Add(this.btnReset);
            this.groupBox7.Controls.Add(this.btnImg);
            this.groupBox7.Location = new System.Drawing.Point(576, 870);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(1292, 94);
            this.groupBox7.TabIndex = 44;
            this.groupBox7.TabStop = false;
            // 
            // btnImg2
            // 
            this.btnImg2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImg2.ForeColor = System.Drawing.Color.Black;
            this.btnImg2.Location = new System.Drawing.Point(359, 22);
            this.btnImg2.Name = "btnImg2";
            this.btnImg2.Size = new System.Drawing.Size(262, 56);
            this.btnImg2.TabIndex = 47;
            this.btnImg2.Text = "2. UN-DETECTED CAM 2";
            this.btnImg2.UseVisualStyleBackColor = true;
            this.btnImg2.Click += new System.EventHandler(this.btnImg2_Click);
            // 
            // btnLog
            // 
            this.btnLog.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLog.ForeColor = System.Drawing.Color.Black;
            this.btnLog.Location = new System.Drawing.Point(667, 22);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(262, 56);
            this.btnLog.TabIndex = 45;
            this.btnLog.Text = "3. DATA HISTORY";
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.Black;
            this.btnReset.Location = new System.Drawing.Point(984, 22);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(262, 56);
            this.btnReset.TabIndex = 44;
            this.btnReset.Text = "4. EXIT";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnImg
            // 
            this.btnImg.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImg.ForeColor = System.Drawing.Color.Black;
            this.btnImg.Location = new System.Drawing.Point(55, 22);
            this.btnImg.Name = "btnImg";
            this.btnImg.Size = new System.Drawing.Size(262, 56);
            this.btnImg.TabIndex = 46;
            this.btnImg.Text = "1. UN-DETECTED CAM 1";
            this.btnImg.UseVisualStyleBackColor = true;
            this.btnImg.Click += new System.EventHandler(this.btnImg_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtQRCode2);
            this.groupBox5.Controls.Add(this.imgBox2);
            this.groupBox5.Controls.Add(this.txtQRCode1);
            this.groupBox5.Controls.Add(this.imgBox);
            this.groupBox5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(15, 22);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1859, 624);
            this.groupBox5.TabIndex = 39;
            this.groupBox5.TabStop = false;
            // 
            // txtQRCode2
            // 
            this.txtQRCode2.BackColor = System.Drawing.Color.Silver;
            this.txtQRCode2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQRCode2.Font = new System.Drawing.Font("Impact", 33F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQRCode2.ForeColor = System.Drawing.Color.Gray;
            this.txtQRCode2.Location = new System.Drawing.Point(652, 575);
            this.txtQRCode2.Name = "txtQRCode2";
            this.txtQRCode2.Size = new System.Drawing.Size(173, 61);
            this.txtQRCode2.TabIndex = 35;
            this.txtQRCode2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtQRCode2.Visible = false;
            this.txtQRCode2.TextChanged += new System.EventHandler(this.txtQRCode2_TextChanged);
            // 
            // imgBox2
            // 
            this.imgBox2.BackColor = System.Drawing.Color.Gainsboro;
            this.imgBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgBox2.InitialImage = null;
            this.imgBox2.Location = new System.Drawing.Point(938, 29);
            this.imgBox2.Name = "imgBox2";
            this.imgBox2.Padding = new System.Windows.Forms.Padding(2);
            this.imgBox2.Size = new System.Drawing.Size(900, 575);
            this.imgBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgBox2.TabIndex = 1;
            this.imgBox2.TabStop = false;
            this.imgBox2.WaitOnLoad = true;
            this.imgBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.imgBox2_Paint);
            // 
            // txtQRCode1
            // 
            this.txtQRCode1.BackColor = System.Drawing.Color.Silver;
            this.txtQRCode1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQRCode1.Font = new System.Drawing.Font("Impact", 33F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQRCode1.ForeColor = System.Drawing.Color.Gray;
            this.txtQRCode1.Location = new System.Drawing.Point(452, 575);
            this.txtQRCode1.Name = "txtQRCode1";
            this.txtQRCode1.Size = new System.Drawing.Size(175, 61);
            this.txtQRCode1.TabIndex = 35;
            this.txtQRCode1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtQRCode1.Visible = false;
            this.txtQRCode1.TextChanged += new System.EventHandler(this.txtQRCode1_TextChanged);
            // 
            // imgBox
            // 
            this.imgBox.BackColor = System.Drawing.Color.Gainsboro;
            this.imgBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgBox.InitialImage = null;
            this.imgBox.Location = new System.Drawing.Point(19, 30);
            this.imgBox.Name = "imgBox";
            this.imgBox.Padding = new System.Windows.Forms.Padding(2);
            this.imgBox.Size = new System.Drawing.Size(900, 575);
            this.imgBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgBox.TabIndex = 0;
            this.imgBox.TabStop = false;
            this.imgBox.WaitOnLoad = true;
            this.imgBox.Paint += new System.Windows.Forms.PaintEventHandler(this.imgBox_Paint_1);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(698, 627);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 41;
            this.label6.Text = "label6";
            // 
            // btnDBConnect
            // 
            this.btnDBConnect.Location = new System.Drawing.Point(459, 609);
            this.btnDBConnect.Name = "btnDBConnect";
            this.btnDBConnect.Size = new System.Drawing.Size(101, 36);
            this.btnDBConnect.TabIndex = 40;
            this.btnDBConnect.Text = "DB CONNET";
            this.btnDBConnect.UseVisualStyleBackColor = true;
            this.btnDBConnect.Click += new System.EventHandler(this.btnDBConnect_Click);
            // 
            // dbDataSet
            // 
            this.dbDataSet.DataSetName = "dbDataSet";
            this.dbDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dbDataSetBindingSource
            // 
            this.dbDataSetBindingSource.DataSource = this.dbDataSet;
            this.dbDataSetBindingSource.Position = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(638, 602);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 41);
            this.button1.TabIndex = 42;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblStatus2
            // 
            this.lblStatus2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus2.AutoSize = true;
            this.lblStatus2.BackColor = System.Drawing.Color.Red;
            this.lblStatus2.Location = new System.Drawing.Point(125, 852);
            this.lblStatus2.Name = "lblStatus2";
            this.lblStatus2.Size = new System.Drawing.Size(96, 13);
            this.lblStatus2.TabIndex = 44;
            this.lblStatus2.Text = "PORT NOT OPEN";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(25, 852);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 13);
            this.label13.TabIndex = 43;
            this.label13.Text = "Weigther status:";
            // 
            // lblConnected2
            // 
            this.lblConnected2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblConnected2.AutoSize = true;
            this.lblConnected2.BackColor = System.Drawing.SystemColors.Control;
            this.lblConnected2.Location = new System.Drawing.Point(328, 852);
            this.lblConnected2.Name = "lblConnected2";
            this.lblConnected2.Size = new System.Drawing.Size(13, 13);
            this.lblConnected2.TabIndex = 46;
            this.lblConnected2.Text = "0";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(232, 852);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(90, 13);
            this.label14.TabIndex = 45;
            this.label14.Text = "Connected Conn:";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerReportsProgress = true;
            this.backgroundWorker2.WorkerSupportsCancellation = true;
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 2000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // tcpServer1
            // 
            this.tcpServer1.Encoding = ((System.Text.Encoding)(resources.GetObject("tcpServer1.Encoding")));
            this.tcpServer1.IdleTime = 50;
            this.tcpServer1.IsOpen = false;
            this.tcpServer1.MaxCallbackThreads = 100;
            this.tcpServer1.MaxSendAttempts = 3;
            this.tcpServer1.Port = -1;
            this.tcpServer1.VerifyConnectionInterval = 100;
            this.tcpServer1.OnConnect += new tcpServer.tcpServerConnectionChanged(this.tcpServer1_OnConnect);
            this.tcpServer1.OnDataAvailable += new tcpServer.tcpServerConnectionChanged(this.tcpServer1_OnDataAvailable);
            // 
            // tcpServer2
            // 
            this.tcpServer2.Encoding = ((System.Text.Encoding)(resources.GetObject("tcpServer2.Encoding")));
            this.tcpServer2.IdleTime = 50;
            this.tcpServer2.IsOpen = false;
            this.tcpServer2.MaxCallbackThreads = 100;
            this.tcpServer2.MaxSendAttempts = 3;
            this.tcpServer2.Port = -1;
            this.tcpServer2.VerifyConnectionInterval = 100;
            this.tcpServer2.OnConnect += new tcpServer.tcpServerConnectionChanged(this.tcpServer2_OnConnect_1);
            this.tcpServer2.OnDataAvailable += new tcpServer.tcpServerConnectionChanged(this.tcpServer2_OnDataAvailable);
            // 
            // timer3
            // 
            this.timer3.Interval = 500;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // backgroundWorker3
            // 
            this.backgroundWorker3.WorkerReportsProgress = true;
            this.backgroundWorker3.WorkerSupportsCancellation = true;
            this.backgroundWorker3.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker3_DoWork);
            this.backgroundWorker3.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker3_ProgressChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1550, 874);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDBConnect);
            this.Controls.Add(this.cmdDisconnect);
            this.Controls.Add(this.txtDisplay);
            this.Controls.Add(this.cmdConnect);
            this.Controls.Add(this.lblConnected2);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lblStatus2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lblConnected);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Misumi Automatic amd Online  Conveyor System 3.0.27-1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdConnect;
        private System.Windows.Forms.TextBox txtDisplay;
        private System.Windows.Forms.Button cmdDisconnect;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer1;
        private tcpServer.TcpServer tcpServer1;
        private System.Windows.Forms.Label lblConnected;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtQRCode;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnDBConnect;
        private dbDataSet dbDataSet;
        private System.Windows.Forms.BindingSource dbDataSetBindingSource;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox imgBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label LblLocalDate;
        private System.Windows.Forms.Label LblLocalTime;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnLog;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnImg;
        private tcpServer.TcpServer tcpServer2;
        private System.Windows.Forms.Label lblStatus2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblConnected2;
        private System.Windows.Forms.Label label14;
        private LBSoft.IndustrialCtrls.Leds.LBLed onlineLedWeight;
        private LBSoft.IndustrialCtrls.Leds.LBLed onlineLedCAM;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button btnAlmReset;
        private LBSoft.IndustrialCtrls.Leds.LBLed almLedOutput;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label15;
        private LBSoft.IndustrialCtrls.Leds.LBLed almLedDB;
        private LBSoft.IndustrialCtrls.Leds.LBLed almLedCAM;
        private LBSoft.IndustrialCtrls.Leds.LBLed almLedWeight;
        private System.Windows.Forms.Label label16;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private LBSoft.IndustrialCtrls.Leds.LBLed onlineLedDB;
        private System.Windows.Forms.Label lbAlarm;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private LBSoft.IndustrialCtrls.Leds.LBLed onlineLedCAM_2;
        private System.Windows.Forms.PictureBox imgBox2;
        private System.Windows.Forms.TextBox txtQRCode2;
        private System.Windows.Forms.TextBox txtQRCode1;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.Button btnImg2;
        private System.Windows.Forms.Timer timer3;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
    }
}

