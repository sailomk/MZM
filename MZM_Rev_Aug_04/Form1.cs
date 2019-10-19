using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Data.SqlClient;
using System.Diagnostics;
using NsMvVisionControlSDK;
using System.Net.NetworkInformation;
using System.Linq;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Globalization;

namespace MZM_Rev_Aug_04
{
    public partial class Form1 : Form
    {
        public delegate void invokeDelegate();
       // public String IMG_LOCATION = "d:/cognex";
        public int maxImage = 10;

        public string CAM_IP, CAM_IP_2, WEIGHT_IP, IMG_LOCATION, DB_NAME, DB_USER, DB_PASSWORD, DB_IP, COMPENSATE, DBP_IP, WATCHDOG, CAM_1_BUF, IMG_LOCATION_C1, IMG_LOCATION_C2,WAIT_CAM;
        public int DBLOOP_COUNT, DBFAIL_COUNT, DBHB_TIME;    // @OCt 2019 for special DB Checking
        public int noDBFailCount,indexDBCount;                               // @OCt 2019 for special DB Checking
        public string imgStatus, imgStatus_2, curImage, img_F_Name, img_F_Name_2 = "";
        public bool alarmFlag = false;
        ArrayList weightList = new ArrayList();
        ArrayList qrList = new ArrayList();
        public bool threadSTART=false;
        public bool mettlerConState=false;
        public string readData = null;
        public string lastqr,lastWeight,keepWeight = "";
        bool isRunning = false;



//        bool m_bOpenSerial = false;
//        bool m_bInit = true;

        public Form1()
        {
          
           
            InitializeComponent();
           // this.Size = new Size(1920, 1080);
            this.Width = 1920;
            this.Height = 1029;
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            //Form1.ActiveForm.Width = 1920;
            //Form1.ActiveForm.Height = 1080;

            try
            {


                MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "USR", "----------------------------------------------------");
                MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "USR", "Program is started");

                initGPIO();
                Thread.Sleep(3000);
                setGPIO(false);

                ReadConfiguration();
                initLED();
                txtQRCode.Text = "";


                noDBFailCount = 0;
                indexDBCount = 0;


             //   txtQRCode1.Text = "";
             //   txtQRCode2.Text = "";


                System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
                tmr.Interval = 1000;//ticks every 1 second
                tmr.Tick += new EventHandler(tmr_Tick);
                tmr.Start();   //Realtime display current date/time


                

                btnChangePort_Click(null, null);
                timer1.Enabled = true;

                backgroundWorker1.RunWorkerAsync();
                backgroundWorker3.RunWorkerAsync();  //@OCt 2019 --Special DB Checking
                startMettler();
                timer2.Interval = Convert.ToInt32(WATCHDOG);
                
            }
            catch (Exception ex) {
                MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", ex.Message);            
            }
        }

        private void initLED()
        {
            this.onlineLedDB.State = LBSoft.IndustrialCtrls.Leds.LBLed.LedState.On;
            this.onlineLedCAM.State = LBSoft.IndustrialCtrls.Leds.LBLed.LedState.On;
            this.onlineLedCAM_2.State = LBSoft.IndustrialCtrls.Leds.LBLed.LedState.On;
            this.onlineLedWeight.State = LBSoft.IndustrialCtrls.Leds.LBLed.LedState.On;
            txtQRCode.Text = "";
            txtQRCode.ReadOnly = true;
            txtQRCode.BackColor = System.Drawing.SystemColors.Window;
            txtWeight.Text = "";
            txtWeight.ReadOnly = true;
            txtWeight.BackColor = System.Drawing.SystemColors.Window;
            this.onlineLedDB.LedColor = Color.Lime;
        }

        private void setGPIO(bool p)
        {
            try
            {

                CMvVisionControlSDK.MV_VC_SINGLE_PARAM stSingleParam = new CMvVisionControlSDK.MV_VC_SINGLE_PARAM();
                stSingleParam.nPortNumber |= (Byte)CMvVisionControlSDK.MV_VC_PORT_NUMBER.MV_VC_PORT_1;
                stSingleParam.nPortNumber |= (Byte)CMvVisionControlSDK.MV_VC_PORT_NUMBER.MV_VC_PORT_2;
                stSingleParam.nPortNumber |= (Byte)CMvVisionControlSDK.MV_VC_PORT_NUMBER.MV_VC_PORT_3;
                stSingleParam.nPortNumber |= (Byte)CMvVisionControlSDK.MV_VC_PORT_NUMBER.MV_VC_PORT_4;
                if (p)
                {
                    stSingleParam.nValidLevel = (Byte)CMvVisionControlSDK.MV_VC_LEVEL.MV_VC_LEVEL_HIGH;
                    alarmFlag = true;
                }
                else
                {
                    stSingleParam.nValidLevel = (Byte)CMvVisionControlSDK.MV_VC_LEVEL.MV_VC_LEVEL_LOW;
                    alarmFlag =false;
                }
                UInt16 nDurationTime = 0;
                stSingleParam.nDurationTime = nDurationTime;
                int nRet = CMvVisionControlSDK.MV_VC_SINGLE_SetParam_CS(ref stSingleParam);
                if (CMvVisionControlSDK.MV_OK != nRet)
                {
                  //  Console.WriteLine("Send SINGLE parameter command fail!");
                    MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "USR", "Send SINGLE parameter command fail!");
                    return;
                }

                // Console.WriteLine("Send SINGLE parameter command succeed!");
                MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "USR", "Send SINGLE parameter command succeed!");

            }
            catch (Exception ex) {
                //Console.WriteLine("setGPIO Loop :    " + ex.Message);
                MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", ex.Message);
            }
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            LblLocalDate.Text = DateTime.Now.ToString("dd/MMM/yyyy ");
            LblLocalTime.Text = DateTime.Now.ToString("HH:mm:ss ");
           // LblUTCTime.Text = DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss");
        }

        private void btnChangePort_Click(object sender, EventArgs e)
        {
            try
            {
                openTcpPort();
            }
            catch (FormatException)
            {
                MessageBox.Show("Port must be an integer", "Invalid Port", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            catch (OverflowException)
            {
                MessageBox.Show("Port is too large", "Invalid Port", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void openTcpPort()
        {
            try
            {
                tcpServer1.Close();
                tcpServer1.Port = Convert.ToInt32("3000");
                tcpServer1.Open();
                MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "USR", " QR CAM#1 Receiver Port 3000 is Started");
                tcpServer2.Close();
                tcpServer2.Port = Convert.ToInt32("3001");
                tcpServer2.Open();
                MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "USR", " QR CAM#2 Receiver Port 3001 is Started");

                displayTcpServerStatus();
            }
            catch (Exception ex) { MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", ex.Message); }
        }

        private void displayTcpServerStatus()
        {
            if (tcpServer1.IsOpen)
            {
                lblStatus.Text = "PORT OPEN";
                lblStatus.BackColor = Color.Lime;
        }
            else
            {
                lblStatus.Text = "PORT NOT OPEN";
                lblStatus.BackColor = Color.Red;
           }
            if (tcpServer2.IsOpen)
            {
                lblStatus2.Text = "PORT OPEN";
                lblStatus2.BackColor = Color.Lime;
            }
            else
            {
                lblStatus2.Text = "PORT NOT OPEN";
                lblStatus2.BackColor = Color.Red;
            }
        }

        private void cmdDisconnect_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            tcpServer1.Close();
            MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "USR", " Program is Closed");
           // tcpServer2.Close();
           // Close();
        }

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            btnChangePort_Click(null, null);
            timer1.Enabled = true;

        }

        private void tcpServer1_OnConnect(tcpServer.TcpServerConnection connection)
        {
            invokeDelegate setText = () => lblConnected.Text = tcpServer1.Connections.Count.ToString();

            Invoke(setText);
        }
    
        private void tcpServer1_OnDataAvailable(tcpServer.TcpServerConnection connection)
        {
            byte[] data = readStream(connection.Socket);

            if (data != null)
            {
                string dataStr = Encoding.ASCII.GetString(data);

                invokeDelegate del = () =>
                {
                    logData(false, dataStr);
                };
                Invoke(del);

                data = null;
            }
        }

        protected byte[] readStream(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            if (stream.DataAvailable)
            {
                byte[] data = new byte[client.Available];

                int bytesRead = 0;
                try
                {
                    bytesRead = stream.Read(data, 0, data.Length);
                }
                catch (System.IO.IOException ex)
                {
                    MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", ex.Message);
                }

                if (bytesRead < data.Length)
                {
                    byte[] lastData = data;
                    data = new byte[bytesRead];
                    Array.ConstrainedCopy(lastData, 0, data, 0, bytesRead);
                }
                return data;
            }
            return null;
        }

        protected byte[] readStream2(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            if (stream.DataAvailable)
            {
                byte[] data = new byte[client.Available];

                int bytesRead = 0;
                try
                {
                    bytesRead = stream.Read(data, 0, data.Length);
                }
                catch (System.IO.IOException ex)
                {
                    MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", ex.Message);
                }

                if (bytesRead < data.Length)
                {
                    byte[] lastData = data;
                    data = new byte[bytesRead];
                    Array.ConstrainedCopy(lastData, 0, data, 0, bytesRead);
                }
                return data;
            }
            return null;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            displayTcpServerStatus();
            lblConnected.Text = tcpServer1.Connections.Count.ToString();
            lblConnected2.Text = tcpServer2.Connections.Count.ToString();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void PHOENIX_ENGINE()
            {
                try
                {
                    #region CASE#1  NORMAL CASE
                    if ((qrList.Count > 0) & (weightList.Count > 0))
                    {

                        string rawQRtxt = (qrList[0].ToString());
                        rawQRtxt = rawQRtxt.Replace("\r\n", "");
                        Console.WriteLine("Raw QR Text == ", rawQRtxt);
                        string[] strSplit = rawQRtxt.Split(',');
                        string qrVal = strSplit[0];
                        string qrTime = strSplit[1];

                        string rawWeighttxt = (weightList[0].ToString());
                        rawWeighttxt = rawWeighttxt.Replace("\r\n", "");
                        string[] weightSplit = rawWeighttxt.Split(',');
                        string weightVal = weightSplit[0];
                        string weightTime = weightSplit[1];

                        DateTime qrcam_dt = DateTime.ParseExact(qrTime, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        DateTime weight_dt = DateTime.ParseExact(weightTime, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        double diffTime = qrcam_dt.Subtract(weight_dt).TotalMilliseconds;
                        Console.WriteLine(" Time Difffff -- ", diffTime);


                        if (diffTime <= Convert.ToDouble(COMPENSATE))
                        {
                            // NORMAL CASE & TIME FOR QR and WEIGHT NOT LATE THAN COMPENSATION TIME    
                            updateMZMDB();
                            Console.WriteLine(" :) NORMAL");
                            // popLastIMG();
                        }
                        else
                        {
                            // TIME FOR QR and WEIGHT OVER THAN COMPENSATION TIME || MIGHT BE QR OR WEIGHT LOSS
                            if (diffTime < 0)
                            {
                                // DETECTION AS QR LOSS
                                Console.WriteLine("1. We lost QR but can get weight " + weightVal);
                                txtQRCode.Text = "--"; // Mod Sep 9 ,2018
                                updateHistory(DateTime.Now.ToString("dd-MM-yy HH:mm:ss"), "N/A", weightVal, "F", "F", "F");
                                // textBox1.Text = textBox1.Text + "QR = F " + "..| WEI = F.... " + weightVal + " .. " + weightList[0] + Environment.NewLine;
                                //lbAlarm.Text = "!!! QR data is older than Weight  " + diffTime + " ms !!!   " + DateTime.Now.ToString("dd-MM-yyy HH:mm:ss");
                                string AlarmText = "!!! WEIGHT IS DETECTED BUT MISSED QR !!!  " + diffTime;
                                MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "CASE#1 !!! WEIGHT IS DETECTED BUT MISSED QR !!! diffTIme ", diffTime.ToString());
                                setAlarmLedCAM(true, AlarmText);
                                weightList.RemoveAt(0);
                            }
                            else
                            {
                                // DETECTION AS WEIGHT LOSS
                                Console.WriteLine("1. We lost Weight but can get QR  " + qrVal);
                                txtWeight.Text = "--";// Mod Sep 9 ,2018
                                txtWeight.BackColor = Color.Red;
                                if (qrVal != "F")
                                { // QR CAM JOB IS SUCESS
                                    string itemExist = WaybillChecking(qrVal);
                                    itemExist = itemExist.ToUpper();
                                    if (itemExist == "OK")
                                    {
                                        string updateRet = UpdateAutoWeight(qrVal, "0.00", 99);
                                        updateRet = updateRet.ToUpper();
                                        if (updateRet == "OK")
                                        {
                                            Console.WriteLine("1 : No weight found || Success to update Item is DB " + qrVal + "--" + "99");
                                            // textBox1.Text = textBox1.Text + "QR = " + qrVal + "..| WEI = F " + " .. " + Environment.NewLine;
                                            updateHistory(DateTime.Now.ToString("dd-MM-yy HH:mm:ss"), qrVal, "N/A", "T", "T", "F");
                                            // lbAlarm.Text = "!!! Weight data is older than QR  " + diffTime + " ms !!!   " + DateTime.Now.ToString("dd-MM-yyy HH:mm:ss");
                                            string AlarmText = "!!! QR IS DETECTED BUT MISSED WEIGHT !!!  " + diffTime;

                                            setAlarmLedWeight(true, AlarmText);
                                        }
                                        else
                                        {
                                            Console.WriteLine("1 :Fail to update Item is DB " + qrVal + "--" + "99");
                                            string AlarmText = "!!! QR IS DETECTED BUT MISSED WEIGHT !!!  " + diffTime;
                                            setAlarmLedWeight(true, AlarmText);
                                        }
                                        qrList.RemoveAt(0);
                                    }
                                    else
                                    {
                                        //   textBox1.Text = textBox1.Text + "QR = " + qrVal + "..| WEI = F " + " .. " + Environment.NewLine;
                                        updateHistory(DateTime.Now.ToString("dd-MM-yy HH:mm:ss"), qrVal, "N/A", "F", "F", "F");
                                        //lbAlarm.Text = "!!! Weight data is older than QR  " + diffTime + " ms !!!   " + DateTime.Now.ToString("dd-MM-yyy HH:mm:ss");
                                        string AlarmText = "!!! QR IS DETECTED BUT MISSED WEIGHT !!!  " + diffTime;
                                        setAlarmLedWeight(true, AlarmText);
                                    }
                                }
                                else
                                { // QR CAM JOB IS FAIL
                                    //  textBox1.Text = textBox1.Text + "QR = F | WEI = F " + " .. " + Environment.NewLine;
                                    updateHistory(DateTime.Now.ToString("dd-MM-yy HH:mm:ss"), "N/A", "N/A", "F", "F", "F");
                                    // lbAlarm.Text = "!!! Weight data is older than QR  " + diffTime + " ms !!!   " + DateTime.Now.ToString("dd-MM-yyy HH:mm:ss");
                                    string AlarmText = "!!! QR IS DETECTED BUT MISSED WEIGHT !!!  " + diffTime;
                                    setAlarmLedWeight(true, AlarmText);
                                    qrList.RemoveAt(0);
                                }
                            }
                        }


                        Console.WriteLine(" TIME DIFF " + qrcam_dt.Subtract(weight_dt).TotalMilliseconds);

                    }
                    #endregion CASE#1  NORMAL CASE
                    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }

                try
                {
                    #region CASE#2 LONGTIME LOST WEIGHT
                    if ((qrList.Count > 1) & (weightList.Count == 0))
                    {
                        // Weight Lost 
                        Console.WriteLine("2. We lost Weight but can get QR " + qrList[0]);
                        MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "CASE#2 LONGTIME LOST WEIGHT ", qrList[0].ToString());
                        string AlarmText = "!!! QR IS DETECTED BUT LONG MISSED WEIGHT !!! ";
                        txtWeight.Text = "---";// Mod Sep 9 ,2018
                        txtWeight.BackColor = Color.Red;
                        setAlarmLedWeight(true, AlarmText);

                        string rawQRtxt = (qrList[0].ToString());
                        rawQRtxt = rawQRtxt.Replace("\r\n", "");                      
                        string[] strSplit = rawQRtxt.Split(',');                        
                        string qrVal = strSplit[0];
                     
                        string itemExist = WaybillChecking(qrVal);
                    itemExist = itemExist.ToUpper();
                    if (itemExist == "OK")
                    {
                        string updateRet = UpdateAutoWeight(qrVal, "0.00", 99);
                        updateRet = updateRet.ToUpper();
                        if (updateRet == "OK")
                        {
                            Console.WriteLine("2. We lost Weight but can get QR  " + qrVal + "--" + "99");
                            updateHistory(DateTime.Now.ToString("dd-MM-yy HH:mm:ss"), qrVal, "N/A", "T", "T", "F");
                        }
                        else
                        {
                            Console.WriteLine("2. We lost Weight but can get QR " + qrVal + "N/A" + "99");
                            updateHistory(DateTime.Now.ToString("dd-MM-yy HH:mm:ss"), qrVal, "N/A", "F", "F", "F");
                        }
                    }
                        qrList.RemoveAt(0);

                    }}
                    #endregion CASE#2 LOST WEIGHT
                    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            #region CASE#3 LONGTIME LOST QR

            if((qrList.Count == 0) & (weightList.Count > 1))
            {
            // QR Lost  
                Console.WriteLine("3. We lost QR but can get weight " + weightList[0]);
                string AlarmText = "!!! WEIGHT IS DETECTED BUT LONG MISSED QR !!! ";
                txtQRCode.Text = "---";// Mod Sep 9 ,2018
                setAlarmLedCAM(true, AlarmText);
                updateHistory(DateTime.Now.ToString("dd-MM-yy HH:mm:ss"), "N/A", weightList[0].ToString(), "F", "F", "F");
                weightList.RemoveAt(0);
            }
            #endregion CASE#3 LONGTIME LOST QR
            try
            {
                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            #region CASE#4 LOST WEIGHT
            
            if((qrList.Count == 1) & (weightList.Count == 0))
            {
            // Weight Lost 
            if(lastqr != qrList[0].ToString())
                {
                lastqr = qrList[0].ToString();
                }
            else
                {
                Console.WriteLine("4. We lost Weight but can get QR " + qrList[0]);
                string rawQRtxt = (qrList[0].ToString());
                rawQRtxt = rawQRtxt.Replace("\r\n", "");
                Console.WriteLine("QR 2 Detected as ",rawQRtxt);
                Console.WriteLine( rawQRtxt);
                string[] strSplit = rawQRtxt.Split(',');
                string qrVal ,qrTime;
             
                     qrVal = strSplit[0];
                     qrTime = strSplit[1];
          
         

                txtWeight.Text = "----";// Mod Sep 9 ,2018
                txtWeight.BackColor = Color.Red;
                if (qrVal != "F")
                { // QR CAM JOB IS SUCESS
                    string itemExist = WaybillChecking(qrVal);
                    itemExist = itemExist.ToUpper();
                    if (itemExist == "OK")
                    {
                        string updateRet = UpdateAutoWeight(qrVal, "0.00", 99);
                        updateRet = updateRet.ToUpper();
                        if (updateRet == "OK")
                        {
                            Console.WriteLine("4 : No weight found || Success to update Item in DB " + qrVal + "--" + "99");
                          //  textBox1.Text = textBox1.Text + "QR = " + qrVal + "..| WEI = F " + " .. " + Environment.NewLine;
                            updateHistory(DateTime.Now.ToString("dd-MM-yy HH:mm:ss"), qrVal, "N/A", "T", "T", "F");
                            string AlarmText = "!!! QR IS DETECTED BUT MISSED WEIGHT IN " + WATCHDOG + " ms !!! ";
                            setAlarmLedWeight(true, AlarmText);
                        }
                        else
                        { Console.WriteLine("4 :Fail to update Item in DB " + qrVal + "N/A" + "99");
                        updateHistory(DateTime.Now.ToString("dd-MM-yy HH:mm:ss"), qrVal, "N/A", "F", "F", "F");
                        }
                    }
                    else {
                     //  textBox1.Text = textBox1.Text + "QR = " + qrVal + "..| WEI = F " + " .. " + Environment.NewLine;
                        updateHistory(DateTime.Now.ToString("dd-MM-yy HH:mm:ss"), qrVal, "N/A", "F", "F", "F");
                        //lbAlarm.Text = "!!! QR is dectected but No weight data coming in " + WATCHDOG + " ms !!!   " + DateTime.Now.ToString("dd-MM-yyy HH:mm:ss");
                        string AlarmText = "!!! QR IS DETECTED BUT MISSED WEIGHT IN " + WATCHDOG + " ms !!! ";
                        setAlarmLedWeight(true, AlarmText);
                    }
                }
                else
                { // QR CAM JOB IS FAIL
               //     textBox1.Text = textBox1.Text + "QR = F | WEI = F " + " .. " + Environment.NewLine;
                    updateHistory(DateTime.Now.ToString("dd-MM-yy HH:mm:ss"), "N/A", "N/A", "F", "F", "F");
                    string AlarmText = "!!! QR IS DETECTED BUT MISSED WEIGHT IN " + WATCHDOG + " ms !!! ";
                    setAlarmLedWeight(true, AlarmText);
                }
                lastqr = "";
                qrList.RemoveAt(0);
                }
            }
            #endregion CASE#4 LOST WEIGHT
            try
            {
                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                #region CASE#5 LOST QR
                if ((qrList.Count == 0) & (weightList.Count == 1))
                {
                    // QR Lost  
                    if (lastWeight != weightList[0].ToString())
                    {
                        lastWeight = weightList[0].ToString();
                    }
                    else
                    {
                        string rawWeighttxt = (weightList[0].ToString());
                        rawWeighttxt = rawWeighttxt.Replace("\r\n", "");
                        string[] weightSplit = rawWeighttxt.Split(',');
                        string weightVal = weightSplit[0];
                        string weightTime = weightSplit[1];
                        txtQRCode.Text = "-----";// Mod Sep 9 ,2018
                        Console.WriteLine("5. We lost QR but can get weight " + weightList[0]);
                        updateHistory(DateTime.Now.ToString("dd-MM-yy HH:mm:ss"), "N/A", weightVal, "F", "F", "F");
                        //    textBox1.Text = textBox1.Text + "QR = F " + "..| WEI = F.... " + weightVal + " .. " + weightList[0] + Environment.NewLine;
                        // lbAlarm.Text = "!!! Weight is dectected but No QR data coming in " + WATCHDOG + " ms !!!   " + DateTime.Now.ToString("dd-MM-yyy HH:mm:ss");
                        string AlarmText = "!!! WEIGHT IS DETECTED BUT MISSED QR IN " + WATCHDOG + " ms !!! ";
                        MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "CASE#5 LOST QR", AlarmText);
                        setAlarmLedCAM(true, AlarmText);
                        weightList.RemoveAt(0);
                        lastWeight = "";
                    }


                }
                #endregion CASE#5 LOST QR
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            }

        // Main DB UPdate 
        private void updateMZMDB()
            {
                keepWeight = "";   // Add for fixed NO QR in DB
               
            if(qrList.Count > 0)
                {
               string rawQRtxt = (qrList[0].ToString());
               rawQRtxt = rawQRtxt.Replace("\r\n", "");

               #region QR-Dection
               try { 
                   string[] strSplit = rawQRtxt.Split(',');
                   string qrVal = strSplit[0];

                   if(qrVal != "F") // Job is Success
                       { //Do can get QR Code
                   #region QR is GOOD
                       string itemExist = WaybillChecking(qrVal);
                       itemExist = itemExist.ToUpper();
                       //Thread.Sleep(Convert.ToInt32(COMPENSATE));  

                       if(itemExist == "OK")
                           {
                           Console.WriteLine("uPDateMZMDB :Found Item in DB " + qrVal);
                           // do read weight
                           ////////////////////////  Compensate the delay
                           //Thread.Sleep(Convert.ToInt32(COMPENSATE));                           
                           ///////////////////////
                           if(weightList.Count > 0)
                           #region QR+WEIGHT+DB
                               {
                               string rawWeighttxt = (weightList[0].ToString());
                               rawWeighttxt = rawWeighttxt.Replace("\r\n", "");
                               try
                                   {
                                   string[] weightSplit = rawWeighttxt.Split(',');
                                   string weightVal = weightSplit[0];
                                   weightVal = weightVal.ToUpper();
                                   weightVal = weightVal.Replace("KG", "");
                                   string updateRet = UpdateAutoWeight(qrVal, weightVal,1);
                                   updateRet = updateRet.ToUpper();
                                   if (updateRet == "OK"){
                                        Console.WriteLine("uPDateMZMDB :Success to update Item in DB " + qrVal + "--"+ weightVal);
                                      //  textBox1.Text = textBox1.Text + "QR = "+ qrVal +"..| WEI = "+ weightVal + " .. " + weightList[0] + Environment.NewLine;
                                        weightList.RemoveAt(0);
                                        updateHistory(DateTime.Now.ToString("dd-MM-y HH:mm:ss"),qrVal,weightVal,"T","T","T");
                                       }else 
                                       {
                                       weightList.RemoveAt(0);
                                       Console.WriteLine("uPDateMZMDB :Fail to update Item in DB " + qrVal + "--" + weightVal);
                                       }
                                   }
                               catch(Exception ex2)
                                   { Console.WriteLine("uPDateMZMDB ::" +ex2.Message); }
                               #endregion QR+WEIGHT+DB

                               qrList.RemoveAt(0);
                            }
                           else
                            {
                              #region NO Weight Data
                               Console.WriteLine("uPDateMZMDB :: No weight found");
                               
                               string updateRet = UpdateAutoWeight(qrVal, "0.00",99);
                               updateRet = updateRet.ToUpper();
                               if(updateRet == "OK")
                                   {
                                   Console.WriteLine("uPDateMZMDB : No weight found || Success to update Item is DB " + qrVal + "--" + "99");
                               //    textBox1.Text = textBox1.Text + "QR = " + qrVal + "..| WEI = F " +  " .. " + Environment.NewLine;
                                   updateHistory(DateTime.Now.ToString("dd-MM-y HH:mm:ss"), qrVal, "N/A", "T", "T", "F");                                   
                                   }
                               else
                                   {
                                   Console.WriteLine("uPDateMZMDB :Fail to update Item is DB " + qrVal + "--" + "99");
                                   }
                               }
                            #endregion NO Weight Data

                           }
                           //=====================================================================
                       else
                           {
                           #region Not Exist in DB
                           Console.WriteLine("uPDateMZMDB ::NOT Found Item in DB");
                        //   Thread.Sleep(Convert.ToInt32(COMPENSATE));
                           Console.WriteLine("txtBox weight "+ txtWeight.Text);
                           if(weightList.Count > 0)
                               { // Found Weight
                               Console.WriteLine("uPDateMZMDB ::NOT Found Item in DB  || HAVE WEIGHT");
                               string rawWeighttxt = (weightList[0].ToString());
                               rawWeighttxt = rawWeighttxt.Replace("\r\n", "");
                               try
                                   {
                                   string[] weightSplit = rawWeighttxt.Split(',');
                                   string weightVal = weightSplit[0];
                                   weightVal = weightVal.ToUpper();
                                   weightVal = weightVal.Replace("KG", "");
                               //    textBox1.Text = textBox1.Text + "QR = " + qrVal + "..| WEI = " + weightVal + " .. " + weightList[0] + Environment.NewLine;
                                   updateHistory(DateTime.Now.ToString("dd-MM-yy HH:mm:ss"), qrVal, weightVal, "F", "F", "F");
                                   }
                               catch(Exception ex)
                                   {
                                   Console.WriteLine("updateMZMDB :: " + ex.Message);
                                   }
                               Console.WriteLine("Remove weight --1--" + weightList[0]);
                               weightList.RemoveAt(0);
                               qrList.RemoveAt(0);
                               }
                           else { // Not Found Weight
                           Console.WriteLine("uPDateMZMDB ::NOT Found Item in DB  || NO WEIGHT FOUND");
                       //    textBox1.Text = textBox1.Text + "QR = " + qrVal + "..| WEI = F " + " .. " + "N/A" + Environment.NewLine;
                           updateHistory(DateTime.Now.ToString("dd-MM-yy HH:mm:ss"), qrVal, "N/A", "F", "F", "F");
 
                               }

                           #endregion Not Exist in DB
                           }
                       }
                   #endregion QR is GOOD
                   else   // Job is Fail |
                       { // Do can't get QR Code  
                   #region QR is BAD
                       Console.WriteLine("updateMZMDB :: Can't  READ QR" );
                      // Thread.Sleep(Convert.ToInt32(COMPENSATE));
                       if(weightList.Count > 0)
                           { // Found Weight
                           Console.WriteLine("updateMZMDB :: Can't  READ QR and Weight Found");
                           string rawWeighttxt = (weightList[0].ToString());
                           rawWeighttxt = rawWeighttxt.Replace("\r\n", "");
                           try
                               {
                               string[] weightSplit = rawWeighttxt.Split(',');
                               string weightVal = weightSplit[0];
                               weightVal = weightVal.ToUpper();
                               weightVal = weightVal.Replace("KG", "");
                               updateHistory(DateTime.Now.ToString("dd-MM-y HH:mm:ss"), "N/A", weightVal, "F", "F", "F");
                            //   textBox1.Text = textBox1.Text + "QR = F " + "..| WEI = F.... " + weightVal + " .. " + weightList[0] + Environment.NewLine;
                               Console.WriteLine("N/A  with Weight" + weightVal);
                               Console.WriteLine("N/A remove weight " + weightList[0]);
                               // weightList.RemoveAt(0);
                               }
                           catch(Exception ex)
                           { Console.WriteLine("updateMZMDB :: " + ex.Message); Console.WriteLine("No of weight 1= " + weightList.Count); }
                           weightList.RemoveAt(0);
                           qrList.RemoveAt(0);
                           }
                       else { // No Weight Found 
                       Console.WriteLine("updateMZMDB :: Can't  READ QR and Weight NOT Found");
                               Console.WriteLine("N/A  with Weight" );
                            //   textBox1.Text = textBox1.Text + "QR = F " + "..| WEI = F.... " + Environment.NewLine;
                               updateHistory(DateTime.Now.ToString("dd-MM-y HH:mm:ss"), "N/A", "N/A", "F", "F", "F");
                              
                           }
                   }
               #endregion QR is BAD  // Job is Fail
               }
               catch(Exception ex)
               { //Console.WriteLine("updateMZMDB :: " + ex.Message); Console.WriteLine("No of weight 2= " + weightList.Count); 
                   MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", ex.Message);
               }
               #endregion QR-Detion

              // qrList.RemoveAt(0);

               // if(weightList.Count > 0) { weightList.RemoveAt(0); }
                }
                // No Data Come from QA CAM
            else { MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "USR", " Empty QR"); }

            }
        // End Main DB UPdate

        private void updateHistory(string p1, string qrVal, string weightVal, string p2, string p3, string p4)
            {
            string connetionString = null;
            SqlConnection cnn;
            SqlCommand cmd;
            string sql = null;
            string result = "NG";
            keepWeight = "";
            connetionString = "Data Source=" + DBP_IP + ";Initial Catalog=" + "conveyor" + ";User ID=" + "sa" + ";Password=" + "leibboos";

            //      connetionString = "Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password";
            sql = "INSERT into conveyor.dbo.Box_History (dt,QR_str,Weight,bExist,bSave,jStatus) VALUES (convert(datetime,'" + p1 + "',5),'" + qrVal + "','" + weightVal + "','" + p2 + "','" + p3 + "','" + p4 + "')";

            cnn = new SqlConnection(connetionString);
            try
                {
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                var firstColumn = cmd.ExecuteScalar();
                if(firstColumn != null)
                    {
                    result = firstColumn.ToString();
                    }

                cmd.Dispose();
                cnn.Close();
                

                //   MessageBox.Show(" No. of Rows " + count);
                }
            catch(Exception ex)
                {
                //MessageBox.Show("updateHistory ::Can not open connection ! " + ex.Message);
                    MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", ex.Message);                
                }
            }

        private string UpdateAutoWeight(string qrVal, string weightVal, Int16 flag)
            { // Update SP as Aug 24,2018
            string connetionString = null;
            SqlConnection cnn;
            SqlCommand cmd;
            string sql = null;
            string result = "NG";
           connetionString = "Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAME + ";User ID=" + DB_USER + ";Password=" + DB_PASSWORD;
         //   connetionString = "Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAME + ";User ID=" + DB_USER + ";Password=P@ssw0rd";
            //      connetionString = "Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password";
            //  sql = "EXEC UpdateAutoWeight @comet_seq='" + qrVal + "' ,  @box_gross_weight=" + weightVal ;
          
            
           sql = "EXEC UpdateAutoWeight @comet_seq='" + qrVal + "' ,  @box_gross_weight=" + weightVal + ",@upd_by='Mettler',@verify_flag=" + @flag + ",@oResult=''";

           // sql = "EXEC UpdateAutoWeight @comet_seq='" + qrVal + "' ,  @box_gross_weight=" + weightVal + ",@upd_by='Mettler',@verify_flag='" + @flag + "'";
            

            cnn = new SqlConnection(connetionString);
            try
                {
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                var firstColumn = cmd.ExecuteScalar();
                if(firstColumn != null)
                    {
                    result = firstColumn.ToString();
                    }

                cmd.Dispose();
                cnn.Close();
                keepWeight = "";
                return result;

                //   MessageBox.Show(" No. of Rows " + count);
                }
            catch(Exception ex)
                {
              //  MessageBox.Show("UpdateAutoWeight ::Can not open connection ! " + ex.Message);
                    MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS--UpdateAutoWeight ", ex.Message);
                    keepWeight = "";
                return result;
                }
            }

        private string WaybillChecking(string p)
        {
            string connetionString = null;
            SqlConnection cnn;
            SqlCommand cmd;
            string sql = null;
            string result = "NG";
            connetionString = "Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAME + ";User ID=" + DB_USER + ";Password=" + DB_PASSWORD;
            // connetionString = "Data Source=" + DB_IP + ";Initial Catalog=" + DB_NAME + ";User ID=" + DB_USER + ";Password=M1sum120018" ;
            //      connetionString = "Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password";

              sql = "EXEC WaybillChecking @comet_seq='" + p + "'" + ",@oResult=''";         //***  Not Sure Oct 23,3018
           // sql = "EXEC WaybillChecking @comet_seq='" + p + "'";
              cnn = new SqlConnection(connetionString);                                   //***  Not Sure Oct 23,3018
            try
            {
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                var firstColumn = cmd.ExecuteScalar();
                if (firstColumn != null)
                {
                    result = firstColumn.ToString();
                }

                cmd.Dispose();
                cnn.Close();
                return result;

                //   MessageBox.Show(" No. of Rows " + count);
            }
            catch (Exception ex)
            {
                // MessageBox.Show("WaybillChecking :: Can not open connection ! " + ex.Message);
                {
                    MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS-WaybillChecking--", ex.Message);
                    setAlarmLedDB(true, ex.Message);
                    if (alarmFlag == false) { this.almLedDB.LedColor = Color.Red; }
                    return result;
                }
            }
        }
            
            


        private void btnLog_Click(object sender, EventArgs e)
        {
            String curDir = @System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            curDir = curDir + @"\DataLogger.exe";
            try
            {
                System.Diagnostics.Process.Start(@curDir);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Phoenix-VA Company Limited", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            
        }

        private void ReadConfiguration() {

            try {
            string appPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var lines = System.IO.File.ReadAllLines(appPath + @"\config.txt");
                // var lines = System.IO.File.ReadAllLines(@"c:\config.txt");
                foreach (var line in lines)
                {
                    string[] lineConfig = line.Split('=');
                    switch (lineConfig[0].ToString().ToUpper())
                    {
                        case "CAM_IP": 
                            CAM_IP = lineConfig[1];
                            break;
                        case "CAM_IP_2":
                            CAM_IP_2 = lineConfig[1];
                            break;
                        case "WEIGHT_IP":
                            WEIGHT_IP = lineConfig[1];
                            break;
                        case "IMG_LOCATION":
                            IMG_LOCATION = lineConfig[1];
                            IMG_LOCATION_C1 = IMG_LOCATION + "/CAM_1";
                            IMG_LOCATION_C2 = IMG_LOCATION + "/CAM_2";
                            break;
                        case "DB_NAME":
                            DB_NAME = lineConfig[1];
                            break;
                        case "DB_USER":
                            DB_USER = lineConfig[1];
                            break;
                        case "DB_PASSWORD":
                            DB_PASSWORD = lineConfig[1];
                            break;
                        case "DB_IP":
                            DB_IP = lineConfig[1];
                            break;
                        case "DBP_IP":
                            DBP_IP = lineConfig[1];
                            break;
                        case "COMPENSATE":
                            COMPENSATE = lineConfig[1];
                            break;
                        case "WATCHDOG":
                            WATCHDOG = lineConfig[1];
                            break;
                        case "WAIT_CAM":
                            WAIT_CAM = lineConfig[1];
                            break;

                        // @Oct 2019 for Special DB Checking
                        case "DBLOOP_COUNT":
                            DBLOOP_COUNT = Convert.ToInt16(lineConfig[1]);
                            break;
                        case "DBFAIL_COUNT":
                            DBFAIL_COUNT = Convert.ToInt16(lineConfig[1]);
                            break;
                        case "DBHB_TIME":
                            DBHB_TIME = Convert.ToInt16(lineConfig[1]);
                            break;                          
                            //
                    }
                }

             //   System.Collections.Generic.IEnumerable<String> lines = File.ReadLines("c:\\config.txt");

            }
            catch (Exception ex) {
                MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", ex.Message);
                MessageBox.Show(ex.Message);
            }
         
        
        
        }

        private void imgBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            string text = imgStatus;
            Font stringFont = new Font("Tahoma", 30);
            SizeF textSize = e.Graphics.MeasureString(text, stringFont);
            PointF locationToDraw = new PointF();
            locationToDraw.X = (imgBox.Width / 2) - (textSize.Width / 2);
            locationToDraw.Y = (imgBox.Height / 2) - (textSize.Height / 2);
            if (imgStatus == "FAIL")
            {
                e.Graphics.DrawRectangle(new Pen(Color.Red, 1), locationToDraw.X, locationToDraw.Y, textSize.Width, textSize.Height);
                e.Graphics.DrawRectangle(new Pen(Color.Red, 6), 0.0F, 0.0F, imgBox.Width - 3, imgBox.Height - 3);

                e.Graphics.DrawString(text, stringFont, Brushes.Red, locationToDraw);

            }
            else
            {
                //  e.Graphics.DrawRectangle(new Pen(Color.Lime, 1), locationToDraw.X, locationToDraw.Y, textSize.Width, textSize.Height);
                e.Graphics.DrawRectangle(new Pen(Color.Lime, 6), 0.0F, 0.0F, imgBox.Width - 3, imgBox.Height - 3);
                e.Graphics.DrawString(text, stringFont, Brushes.Lime, locationToDraw);
            }
        }

        private void imgBox_Paint_1(object sender, PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            string text = imgStatus;
            Font stringFont = new Font("Tahoma", 30);
            SizeF textSize = e.Graphics.MeasureString(text, stringFont);
            PointF locationToDraw = new PointF();
            locationToDraw.X = (imgBox.Width / 2) - (textSize.Width / 2);
            locationToDraw.Y = (imgBox.Height / 2) - (textSize.Height / 2);
            if (imgStatus == "FAIL")
            {
                e.Graphics.DrawRectangle(new Pen(Color.Red, 1), locationToDraw.X, locationToDraw.Y, textSize.Width, textSize.Height);
                e.Graphics.DrawRectangle(new Pen(Color.Red, 6), 0.0F, 0.0F, imgBox.Width - 3, imgBox.Height - 3);

                e.Graphics.DrawString(text, stringFont, Brushes.Red, locationToDraw);

            }
            else
            {
                //  e.Graphics.DrawRectangle(new Pen(Color.Lime, 1), locationToDraw.X, locationToDraw.Y, textSize.Width, textSize.Height);
                e.Graphics.DrawRectangle(new Pen(Color.Lime, 6), 0.0F, 0.0F, imgBox.Width - 3, imgBox.Height - 3);
                e.Graphics.DrawString(text, stringFont, Brushes.Lime, locationToDraw);
            }
        }

        private void imgBox2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            string text = imgStatus_2;
            Font stringFont = new Font("Tahoma", 30);
            SizeF textSize = e.Graphics.MeasureString(text, stringFont);
            PointF locationToDraw = new PointF();
            locationToDraw.X = (imgBox.Width / 2) - (textSize.Width / 2);
            locationToDraw.Y = (imgBox.Height / 2) - (textSize.Height / 2);
            if (imgStatus_2 == "FAIL")
            {
                e.Graphics.DrawRectangle(new Pen(Color.Red, 1), locationToDraw.X, locationToDraw.Y, textSize.Width, textSize.Height);
                e.Graphics.DrawRectangle(new Pen(Color.Red, 6), 0.0F, 0.0F, imgBox.Width - 3, imgBox.Height - 3);

                e.Graphics.DrawString(text, stringFont, Brushes.Red, locationToDraw);

            }
            else
            {
                e.Graphics.DrawRectangle(new Pen(Color.Lime, 6), 0.0F, 0.0F, imgBox.Width - 3, imgBox.Height - 3);
                e.Graphics.DrawString(text, stringFont, Brushes.Lime, locationToDraw);
            }
        }

        private void btnImg_Click(object sender, EventArgs e)
        {
            String curDir = @System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            curDir = curDir + @"\UndetectedBoxCAM1.exe";
            try
            {
                System.Diagnostics.Process.Start(@curDir);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Phoenix-VA Company Limited", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", ex.Message);
            }
        }

        private void btnImg2_Click(object sender, EventArgs e)
        {
            String curDir = @System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            curDir = curDir + @"\UndetectedBoxCAM2.exe";
            try
            {
                System.Diagnostics.Process.Start(@curDir);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Phoenix-VA Company Limited", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", ex.Message);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
                
            Close();
        }

        private void initGPIO() 
        {

            #region serialCOM
            Int32 nRet;
        try
            {
            CMvVisionControlSDK.MV_VC_Close_CS();
            CMvVisionControlSDK.MV_VC_SERIAL stSerial = new CMvVisionControlSDK.MV_VC_SERIAL();
            stSerial.strComName = "Com2";
            stSerial.enBaudRate = CMvVisionControlSDK.MV_VC_BAUDRATE.MV_VC_BAUD_RATE_115200;
            stSerial.enDataBits = CMvVisionControlSDK.MV_VC_DATABITS.MV_VC_DATA_BITS_8;
            stSerial.enParityScheme = CMvVisionControlSDK.MV_VC_PARITY_SCHEME.MV_VC_PARITY_SCHEME_NONE;
            stSerial.enStopBits = CMvVisionControlSDK.MV_VC_STOPBITS.MV_VC_STOP_BITS_1;

             nRet = CMvVisionControlSDK.MV_VC_Open_CS(ref stSerial);
            if(CMvVisionControlSDK.MV_OK != nRet)
                {
                //MessageBox.Show("Open serial port " + stSerial.strComName + " Fail!");
                Console.WriteLine("Open serial port FAIL :    ");
                MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "USR"," Open serial port FAIL  ");
                return;
                }

            //MessageBox.Show("Open serial port" + stSerial.strComName + "Succeed!");
           // Console.WriteLine("Open serial port SUCCESS :    ");
            MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "USR", " Open serial port SUCCESS  ");
            //m_bOpenSerial = true;

            CMvVisionControlSDK.MV_VC_PATTERN_SELECT stPatternSelect = new CMvVisionControlSDK.MV_VC_PATTERN_SELECT();
             stPatternSelect.nPatternSelect = (Byte)CMvVisionControlSDK.MV_VC_PATTERN.MV_VC_PATTERN_ALL;
            //stPatternSelect.nPatternSelect = (Byte)CMvVisionControlSDK.MV_VC_PATTERN.MV_VC_PATTERN_I_O;

            nRet = CMvVisionControlSDK.MV_VC_PATTERN_Select_CS(ref stPatternSelect);
            if(CMvVisionControlSDK.MV_OK != nRet)
                {
             //   Console.WriteLine("Send pattern select command fail!");
                MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "USR", " Send pattern select command fail!   ");
                return;
                }
           // Console.WriteLine("Send pattern select command succeed!");
            MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "USR", " Send pattern select command succeed!   ");

            }catch (Exception ex){
          //  MessageBox.Show("GPIO INIT: " + ex.Message);
                MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", ex.Message);
            }
            #endregion

    
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            bool loop = true;
            string retCAM, retCAM_2, retWEIGHT, retDB, retRES = "0";
            
            do
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    try
                    {
                        try
                        {
                            #region PING_CAMERA
                            Ping myPing = new Ping();

                            try
                            {
                                PingReply reply = myPing.Send(CAM_IP, 1000);
                                if ((reply != null) & (reply.Status == IPStatus.Success))
                                {
                                    retCAM = "1";
                                }
                                else
                                {
                                    retCAM = "0";
                                    // MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "USR", " PING_CAMERA FAIL!");
                                }
                            }
                            catch (Exception ex) {
                                retCAM = "0";
                                MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", ex.Message + "[PING_CAMERA]");
                            }
                          
                            #endregion

                            #region PING_CAMERA_2                          

                            try
                            {
                                PingReply reply_Cam_2 = myPing.Send(CAM_IP_2, 1000);
                                if ((reply_Cam_2 != null) & (reply_Cam_2.Status == IPStatus.Success))
                                {
                                    retCAM_2 = "1";
                                }
                                else
                                {
                                    retCAM_2 = "0";
                                    // MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "USR", " PING_CAMERA FAIL!");
                                }
                            }
                            catch (Exception ex)
                            {
                                retCAM_2 = "0";
                                MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", ex.Message + "[PING_CAMERA_2]");
                            }

                            #endregion 

                            #region PING_WEIGHT
                            try
                            {

                                PingReply reply_w = myPing.Send(WEIGHT_IP, 1000);
                                //   Console.WriteLine(reply.ToString());
                                //   Console.WriteLine(reply.Status.ToString());
                                if ((reply_w != null) & (reply_w.Status == IPStatus.Success))
                                {
                                    retWEIGHT = "1";
                                    threadSTART = true;
                                }
                                else
                                {
                                    retWEIGHT = "0";
                                    threadSTART = false;
                                }
                            }
                            catch (Exception ex) {
                                retWEIGHT = "0";
                                threadSTART = false;
                                MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", ex.Message + "[PING_WEIGHT]");
                            }
                            #endregion
                            /*
                            #region PING_DB
                            try
                            {
                                PingReply reply_db = myPing.Send(DB_IP, 1000);
                                if ((reply_db != null) & (reply_db.Status == IPStatus.Success))
                                {
                                    retDB = "1";
                                }
                                else
                                {
                                    retDB = "0";
                                }
                            }
                            catch (Exception ex) {
                                retDB = "0";
                                MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", ex.Message + "[PING_DB]");
                            }
                            #endregion
                            */
                            retDB = "1";
                            retRES = retCAM + retWEIGHT + retDB + retCAM_2;
                        worker.ReportProgress(Int32.Parse(retRES));
                        }
                        catch (Exception ex)
                        {
                           // Console.WriteLine("DoWork Ping Loop :    " + ex.Message);
                            MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", ex.Message+"[DO PING 999]");
                            worker.ReportProgress(999);
                        }

                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine("Main DoWork Loop :    " + ex.Message);
                        MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", ex.Message + "[DO MAIN 999]");
                        worker.ReportProgress(999);
                    }
                }
                System.Threading.Thread.Sleep(1000);
            }
            while (loop);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //Console.WriteLine("Progress Update Value ===   " + e.ProgressPercentage);
            try
            {
                if (e.ProgressPercentage != 999)
                {
                    string retLED = e.ProgressPercentage.ToString();
                 //   if (retLED.Length == 2) { retLED = "0" + retLED; }
                 //   if (retLED.Length == 1) { retLED = "00" + retLED; }
                    if (retLED.Length == 3) { retLED = "0" + retLED; }
                    if (retLED.Length == 2) { retLED = "00" + retLED; }
                    if (retLED.Length == 1) { retLED = "000" + retLED; }
                 
                    char[] arrayLED = retLED.ToCharArray();

                    #region CAM ALARM LED

                    if (arrayLED[0] == '0')
                    {
                        this.onlineLedCAM.LedColor = Color.Red;
                        setAlarmLedCAM(true,"!!! QR CAM IS NOT READY !!!");
                       // MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "USR", "Send SINGLE parameter command succeed!");
                        if (alarmFlag == false) { this.almLedCAM.LedColor = Color.Red; }
                    }
                    else
                    {
                        this.onlineLedCAM.LedColor = Color.Lime;
                        setAlarmLedCAM(false,"--");
                    }

                    #endregion CAM ALARM LED

                    #region WEIGHT ALARM LED
                    if (arrayLED[1] == '0')
                    {
                        this.onlineLedWeight.LedColor = Color.Red;
                        setAlarmLedWeight(true, "!!! METTLER IS NOT READY !!! ");
                        if (alarmFlag == false) { this.almLedWeight.LedColor = Color.Red; }
                        //this.almLedWeight.LedColor = Color.Red;
                    }
                    else
                    {
                    if(mettlerConState)
                        {
                        this.onlineLedWeight.LedColor = Color.Lime;
                        setAlarmLedWeight(false,"--");
                        }

                    }

                    #endregion WEIGHT ALARM LED

                    /*
                    #region DB ALARM LED

                    if (arrayLED[2] == '0')
                    {
                        this.onlineLedDB.LedColor = Color.Red;
                        setAlarmLedDB(true,"!!! DB IS NOT READY !!!");
                        if (alarmFlag == false) { this.almLedDB.LedColor = Color.Red; }
                     //   this.almLedDB.LedColor = Color.Red;
                    }
                    else
                    {
                        this.onlineLedDB.LedColor = Color.Lime;
                        setAlarmLedDB(false,"--");

                    }

                    #endregion WEIGHT ALARM LED
                    */
                    #region CAM ALARM LED 2

                    if (arrayLED[3] == '0')
                    {
                        this.onlineLedCAM_2.LedColor = Color.Red;
                        setAlarmLedCAM(true, "!!! QR CAM 2 IS NOT READY !!!");
                        // MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "USR", "Send SINGLE parameter command succeed!");
                        if (alarmFlag == false) { this.almLedCAM.LedColor = Color.Red; }
                    }
                    else
                    {
                        this.onlineLedCAM_2.LedColor = Color.Lime;
                        setAlarmLedCAM(false, "--");
                    }

                    #endregion CAM ALARM LED 2

                }
                else
                { // PING RETUNE !SUCCESS
                   // this.onlineLedDB.LedColor = Color.Yellow;
                    this.onlineLedCAM.LedColor = Color.Yellow;
                    this.onlineLedCAM_2.LedColor = Color.Yellow;
                    this.onlineLedWeight.LedColor = Color.Yellow;
                    this.almLedCAM.LedColor = Color.Red;
                    this.almLedWeight.LedColor = Color.Red;
                   // this.almLedDB.LedColor = Color.Red;
                }
            }
            catch (Exception ex) {
               // Console.WriteLine("Main BG Progress update Loop :    " + ex.Message);
                MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", ex.Message);
            }
        }

        private void setAlarmLedDB(bool p,string reason)
        {
            if (alarmFlag)
            {
                // Nothing to do 
            }
            else
            {
                if (p) { this.almLedDB.LedColor = Color.Red; this.almLedOutput.State = LBSoft.IndustrialCtrls.Leds.LBLed.LedState.On; setGPIO(true); btnAlmReset.Enabled = true; lbAlarm.Text = reason + "   "+ DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"); }
                else { this.almLedDB.LedColor = Color.Lime; this.almLedOutput.State = LBSoft.IndustrialCtrls.Leds.LBLed.LedState.Off; lbAlarm.Text = "--"; }
            }
        }

        private void setAlarmLedWeight(bool p,string reason)
        {
            if (alarmFlag)
            {
                // Nothing to do 
            }
            else
            {
                if (p) { this.almLedWeight.LedColor = Color.Red; this.almLedOutput.State = LBSoft.IndustrialCtrls.Leds.LBLed.LedState.On; setGPIO(true); btnAlmReset.Enabled = true; lbAlarm.Text = reason + "   " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"); }
                else { this.almLedWeight.LedColor = Color.Lime; this.almLedOutput.State = LBSoft.IndustrialCtrls.Leds.LBLed.LedState.Off; lbAlarm.Text = "--"; }
            }
        }

        private void setAlarmLedCAM(bool p,string reason)
        {
            if (alarmFlag) 
            { 
                // Nothing to do 
            }
            else {
                if (p) { this.almLedCAM.LedColor = Color.Red; this.almLedOutput.State = LBSoft.IndustrialCtrls.Leds.LBLed.LedState.On; setGPIO(true); btnAlmReset.Enabled = true; lbAlarm.Text = reason + "   " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"); }
                else { this.almLedCAM.LedColor = Color.Lime; this.almLedOutput.State = LBSoft.IndustrialCtrls.Leds.LBLed.LedState.Off; lbAlarm.Text = "--"; }
            }
        }

        private void btnAlmReset_Click(object sender, EventArgs e)
        {
            setGPIO(false);
            btnAlmReset.Enabled = false;
            this.almLedOutput.State = LBSoft.IndustrialCtrls.Leds.LBLed.LedState.Off;
            lastWeight = "";
            lbAlarm.Text="--";
            qrList.Clear();
            weightList.Clear();

        }

        private void popLastIMGCreate()
        {
            #region showAllImage
            if (Directory.Exists(IMG_LOCATION))
            {
                DirectoryInfo dir = new DirectoryInfo(IMG_LOCATION);
                FileInfo[] files = dir.GetFiles("*.bmp").OrderByDescending(p => p.CreationTime).ToArray();
                files = dir.GetFiles("*.bmp").OrderByDescending(p => p.CreationTime).ToArray();
                try
                {
                    foreach (FileInfo file in files)
                    {

                        DateTime creation = File.GetCreationTime(file.FullName);
                        if (Convert.ToInt32((DateTime.Now - creation).TotalMinutes) < 1)
                            try
                            {
                                Console.WriteLine(file.FullName);

                                if (imgBox.Image != null)
                                {
                                    imgBox.Image.Dispose();
                                }
                                Thread.Sleep(200);
                                imgBox.Image = Image.FromFile(@file.FullName);

                            }
                            catch (Exception ex2)
                            {
                                //  MessageBox.Show("Populte Image :: " + ex2.Message);
                                MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "USR", ex2.Message);
                            }
                        imgBox.BackgroundImage = null;
                        break;
                    }
                }



                catch (Exception e)
                {
                    // MessageBox.Show(e.Message, "Populate Image", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", e.Message);
                }
            }
            else
            {
                MessageBox.Show(IMG_LOCATION + " :: Image Path Not Found", "Phoenix-VA Company Limited", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            #endregion

            #region delImage
            if (Directory.Exists(IMG_LOCATION))
            {
                DirectoryInfo dir = new DirectoryInfo(IMG_LOCATION);
                FileInfo[] files = dir.GetFiles("P*.bmp").OrderByDescending(p => p.CreationTime).ToArray();
                files = dir.GetFiles("P*.bmp").OrderByDescending(p => p.CreationTime).ToArray();
                try
                {
                    foreach (FileInfo file in files)
                    {

                        DateTime creation = File.GetCreationTime(file.FullName);
                        {
                            delIMG(file.FullName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            #endregion
        }

        private void popLastIMG()
        {
            #region showAllImage
            Thread.Sleep(200);
            if (Directory.Exists(IMG_LOCATION_C1))
            {
                DirectoryInfo dir = new DirectoryInfo(IMG_LOCATION_C1);
                FileInfo[] files = dir.GetFiles("*.bmp").OrderByDescending(p => p.LastWriteTime).ToArray();
                //   FileInfo[] files = dir.GetFiles("*.bmp").OrderByDescending(p => p.CreationTime).ToArray();
                //DateTime lastModified = System.IO.File.GetLastWriteTime(strFilePath);
                // files = dir.GetFiles("*.bmp").OrderByDescending(p => p.CreationTime).ToArray();
                files = dir.GetFiles("*.bmp").OrderByDescending(p => p.LastWriteTime).ToArray();
                try
                {
                    foreach (FileInfo file in files)
                    {
                       
                        //DateTime creation = File.GetCreationTime(file.FullName); 
                        DateTime creation = File.GetLastWriteTime(file.FullName);
                      //      if (Convert.ToInt32((DateTime.Now - creation).TotalMinutes) < 1)
                        Int32 dif = Convert.ToInt32((DateTime.Now - creation).TotalSeconds);
                        //if (dif < 300)
                        if (true == true)
                        {
                            try
                            {
                                Console.WriteLine(file.FullName);
                                img_F_Name = file.FullName; //Keep Image Name 
                             //   lbAlarm.Text = @file.FullName + "---" + dif.ToString(); ;
                                if (imgBox.Image != null)
                                {
                                    imgBox.Image.Dispose();
                                }
                         //       Thread.Sleep(200);
                                imgBox.Image = Image.FromFile(@file.FullName);

                            }
                            catch (Exception ex2)
                            {
                                //  MessageBox.Show("Populte Image :: " + ex2.Message);
                                MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "USR", ex2.Message);
                            }
                            break;
                        }
                        else
                        {
                            imgBox.BackgroundImage = null;
                           // lbAlarm.Text = "Not found Image" + dif.ToString();
                        }
                    
                    } //end for each

                }



                catch (Exception e)
                {
                   // MessageBox.Show(e.Message, "Populate Image", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", e.Message);
                }
            }
            else
            {
                MessageBox.Show(IMG_LOCATION_C1 + " :: Image Path Not Found", "Phoenix-VA Company Limited", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            #endregion 

            #region delImage
            if (Directory.Exists(IMG_LOCATION_C1))
                {
                    DirectoryInfo dir = new DirectoryInfo(IMG_LOCATION_C1);
                FileInfo[] files = dir.GetFiles("P*.bmp").OrderByDescending(p => p.CreationTime).ToArray();
                files = dir.GetFiles("P*.bmp").OrderByDescending(p => p.CreationTime).ToArray();
                try
                    {
                    foreach(FileInfo file in files)
                        {

                        DateTime creation = File.GetCreationTime(file.FullName);
                            {
                             delIMG(file.FullName);
                            }                        
                        }
                    }
                catch(Exception ex)
                    {
                    Console.WriteLine(ex.Message);
                    }
                }
            #endregion 
        }

        private void popLastIMG_2()
        {
            #region showAllImage
            Thread.Sleep(200);
            if (Directory.Exists(IMG_LOCATION))
            {
                DirectoryInfo dir = new DirectoryInfo(IMG_LOCATION_C2);
                FileInfo[] files = dir.GetFiles("*.bmp").OrderByDescending(p => p.LastWriteTime).ToArray();
                //   FileInfo[] files = dir.GetFiles("*.bmp").OrderByDescending(p => p.CreationTime).ToArray();
                //DateTime lastModified = System.IO.File.GetLastWriteTime(strFilePath);
                // files = dir.GetFiles("*.bmp").OrderByDescending(p => p.CreationTime).ToArray();
                files = dir.GetFiles("*.bmp").OrderByDescending(p => p.LastWriteTime).ToArray();
                try
                {
                    foreach (FileInfo file in files)
                    {

                        //DateTime creation = File.GetCreationTime(file.FullName); 
                        DateTime creation = File.GetLastWriteTime(file.FullName);
                        //      if (Convert.ToInt32((DateTime.Now - creation).TotalMinutes) < 1)
                        Int32 dif = Convert.ToInt32((DateTime.Now - creation).TotalSeconds);
                        //if (dif < 300)
                        if (true == true)
                        {
                            try
                            {
                                Console.WriteLine(file.FullName);
                                img_F_Name_2 = file.FullName; //Keep Image Name 
                                //   lbAlarm.Text = @file.FullName + "---" + dif.ToString(); ;
                                if (imgBox2.Image != null)
                                {
                                    imgBox2.Image.Dispose();
                                }
                                //       Thread.Sleep(200);
                                imgBox2.Image = Image.FromFile(@file.FullName);

                            }
                            catch (Exception ex2)
                            {
                                //  MessageBox.Show("Populte Image :: " + ex2.Message);
                                MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "USR", ex2.Message);
                            }
                            break;
                        }
                        else
                        {
                            imgBox2.BackgroundImage = null;
                            // lbAlarm.Text = "Not found Image" + dif.ToString();
                        }

                    } //end for each

                }



                catch (Exception e)
                {
                    // MessageBox.Show(e.Message, "Populate Image", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", e.Message);
                }
            }
            else
            {
                MessageBox.Show(IMG_LOCATION_C2 + " :: Image Path Not Found", "Phoenix-VA Company Limited", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            #endregion

            #region delImage
            if (Directory.Exists(IMG_LOCATION_C2))
            {
                DirectoryInfo dir = new DirectoryInfo(IMG_LOCATION_C2);
                FileInfo[] files = dir.GetFiles("P*.bmp").OrderByDescending(p => p.CreationTime).ToArray();
                files = dir.GetFiles("P*.bmp").OrderByDescending(p => p.CreationTime).ToArray();
                try
                {
                    foreach (FileInfo file in files)
                    {

                        DateTime creation = File.GetCreationTime(file.FullName);
                        {
                            delIMG(file.FullName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            #endregion
        }
        
        private void delIMG(string p)
        {    // Not implement yet
            try
            {
               // File.Copy("c:\\temp.txt", "c:\\copytemp.txt", true);
               // File.Delete("c:\\copytemp.txt");
                File.Delete(p);
            }
            catch (System.IO.FileNotFoundException ex)
            {
               // MessageBox.Show(ex.Message);
                MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name,"SYS",ex.Message);
            }
        }


        private void MZM_Log(string mod_name, string level, string msg)
        {
            try
            {
                string appPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                string logName = appPath + @"\MZM_" + DateTime.Today.ToString("ddMMyyyy") + ".log";

                using (StreamWriter sw = new StreamWriter(logName, true))
                {
                    sw.WriteLine(DateTime.Now.ToString("dd-MMM-yyy HH:mm:ss ") + ":" + level + ":" + mod_name + ":" + msg);
                    sw.Flush();
                    sw.Close();                  
                }
     
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        // Mettler Connection
        #region Mettler 
        private void getWeight()
            {
            if(this.InvokeRequired)
                this.Invoke(new MethodInvoker(getWeight));
            else
                {
                string[] strSplit = readData.Split(',');
                txtWeight.BackColor = Color.Green;
                txtWeight.ForeColor = Color.White;
                txtWeight.Text = strSplit[0];
                keepWeight = strSplit[0];


                //var numAlpha = new Regex("(?<Numeric>[0-9.]*)(?<Alpha>[a-zA-Z]*)");
                //var match = numAlpha.Match(strSplit[0]);
                //var num = match.Groups["Numeric"].Value;

                 string num = strSplit[0].ToString();
                num=num.ToUpper();
                num=num.Replace("KG","");
                if (num != "")
                {
                    weightList.Add(num + "," + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                    Console.WriteLine("--> getWeight Routine ::: " + num);
                    MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "USR --GetWeight -- ", num);
                }
                //txtWeight.Text = num;
                //textBox1.Text = strSplit[0];
                //if(textBox1.Lines.Length > 500)
                //    {
                //    string[] temp = new string[500];
                //    Array.Copy(textBox1.Lines, textBox1.Lines.Length - 500, temp, 0, 500);
                //    textBox1.Lines = temp;
                //    }
                //textBox1.SelectionStart = textBox1.Text.Length;
                //textBox1.ScrollToCaret();

                }

            }
        //---------------------------------------------------------------------
        public bool IsMachineConnected(TcpClient client)
            {
            //  get
                {
                try
                    {
                    if(client != null && client.Client != null && client.Client.Connected)
                        {
                        /* pear to the documentation on Poll:
                         * When passing SelectMode.SelectRead as a parameter to the Poll method it will return 
                         * -either- true if Socket.Listen(Int32) has been called and a connection is pending;
                         * -or- true if data is available for reading; 
                         * -or- true if the connection has been closed, reset, or terminated; 
                         * otherwise, returns false
                         */

                        // Detect if client disconnected
                        if(client.Client.Poll(0, SelectMode.SelectRead))
                            {
                            byte[] buff = new byte[1];
                            if(client.Client.Receive(buff, SocketFlags.Peek) == 0)
                                {
                                // Client disconnected
                                return false;
                                }
                            else
                                {
                                return true;
                                }
                            }

                        return true;
                        }
                    else
                        {
                        return false;
                        }
                    }
                catch
                    {
                    return false;
                    }
                }
            }
        //---------------------------------------------------------------------
        private void connectWeight()
            {

            System.Net.Sockets.TcpClient weightSocket = new System.Net.Sockets.TcpClient();
            NetworkStream weightStream = default(NetworkStream);
            weightSocket.SendTimeout = 300;
            weightSocket.ReceiveTimeout = 300;
             weightSocket.NoDelay = true;

            #region retryToConnect   // 50x200 =10000 mseconds  --> 10 secs
            int retryConnect = 0;
            while(IsMachineConnected(weightSocket) != true)
                {
                try
                { weightSocket.Connect(WEIGHT_IP, 23); threadSTART = true; }
                catch(Exception ex)
                    {
                    //Console.WriteLine(ex.Message);
                    MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", ex.Message +" [PIPE IS BROKEN]");
                    invokeDelegate del = () => { WeightConnLED(true); };
                    Invoke(del);
                    threadSTART = false;
                    }

                retryConnect += 1;
                if(retryConnect > 50)
                    {
                    this.threadSTART = false;
                   // Console.WriteLine("MaxRetry");
                    invokeDelegate del = () => { WeightConnLED(true); };
                    Invoke(del);
                   // lbAlarm.Text = "!!! Maximum retry to make a  Mettler Connection  !!!" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                    MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "USR", "  Weight Max Retry timed out 50x200 = 10 Secs");
                    setAlarmLedWeight(true, "!!! Maximum retry to make a  Mettler Connection [10 secs] !!!");
                    break;

                    }
               
                System.Threading.Thread.Sleep(200);
                }//end while connected
            #endregion endretryToConnect


            #region readWeightAllTime
            while(threadSTART)
                {
                while(IsMachineConnected(weightSocket))
                    {
                    if(IsMachineConnected(weightSocket))
                        {
                        invokeDelegate del = () => { WeightConnLED(false); };   //Good Connection
                        try
                            {
                            Invoke(del);
                            }
                        catch(Exception ex) { Console.WriteLine(ex.Message); }
                        }
                    else
                        {
                        invokeDelegate del = () => { WeightConnLED(true); };
                        try
                            {
                            Invoke(del);
                            }
                        catch(Exception ex) { Console.WriteLine(ex.Message); }
                        break;
                        }

                    weightSocket.NoDelay = true;
                    try
                        {
                        weightStream = weightSocket.GetStream();
                        int buffSize = 0;
                        //   byte[] inStream = new byte[10025];


                        buffSize = weightSocket.ReceiveBufferSize;
                        byte[] inStream = new byte[buffSize];

                        while((weightStream.DataAvailable) & IsMachineConnected(weightSocket))
                            {
                            weightStream.Read(inStream, 0, buffSize);
                            }
                        string returndata = System.Text.Encoding.ASCII.GetString(inStream);
                        weightStream.Flush();
                       // Console.WriteLine(IsMachineConnected(weightSocket));
                        returndata = returndata.Replace("\0", "");
                        readData = "" + returndata;
                      //   Console.WriteLine("Weight Data = " +readData);
                        Thread.Sleep(10);

                        if(readData != "")
                            {
                            getWeight();
                            }

                        }
                    catch(Exception ex)
                        {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(weightSocket.Connected.ToString());
                        }
                    } //end while ... isConnntected

                #region tryToReConnect
                retryConnect = 0;
                while(IsMachineConnected(weightSocket) != true)
                    {
                    try
                        {
                        //this.clientSocket3.Close();
                        // this.clientSocket3.Client.Close();
                        //  this.clientSocket3.Client
                        // serverStream = clientSocket3.GetStream();
                        weightStream.Close();
                        weightSocket.Close();
                        // weightSocket.Client.Shutdown(SocketShutdown.Both);
                        //weightSocket.Client.Disconnect(true);
                        weightSocket = new System.Net.Sockets.TcpClient();
                        weightSocket.Connect(WEIGHT_IP, 23);
                        weightSocket.SendTimeout = 300;
                        weightSocket.ReceiveTimeout = 300;
                        threadSTART = true;
                        }
                    catch(Exception ex)
                        {
                        Console.WriteLine(ex.Message);
                        invokeDelegate del = () => { WeightConnLED(true); };
                        try
                            {
                            Invoke(del);
                            }
                        catch(Exception ex2) { Console.WriteLine(ex2.Message); }
                        threadSTART = false;
                        }

                    retryConnect += 1;
                    if(retryConnect > 60)
                        {
                        this.threadSTART = false;
                        Console.WriteLine("MaxRetry");
                        invokeDelegate del = () => { WeightConnLED(true); };
                        try
                            {
                            Invoke(del);
                            }
                        catch(Exception ex) { Console.WriteLine(ex.Message); }
                        break;
                        }
                    System.Threading.Thread.Sleep(1000);
                    }//end while connected
                #endregion tryToReconnect

                } //end while ... threadSTART
            #endregion endreadWeightAlltime


            }

        private void WeightConnLED(bool p)
            {  // True == Bad Connection  | False = Good Connection
              //  lbAlarm.Text = "!!! Lost Mettler Connection  !!!" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            if(p)
                {
                //pictureBox1.BackColor = Color.Red;
               //almLedWeight.LedColor = Color.Yellow;
                    setAlarmLedWeight(true, "!!! Mettler Lost Connection  !!!");
               if (alarmFlag == false) { this.almLedWeight.LedColor = Color.Red; }
                mettlerConState = false;
                }
            else 
                {
                if(!alarmFlag)
                    {
                    almLedWeight.LedColor = Color.Lime;
                    lbAlarm.Text="--";
                    }
                mettlerConState = true; }
                }

        private void startMettler() {
            //threadSTART = true;
            //Thread ctThread = new Thread(connectWeight);
           // ctThread.IsBackground = true;
        // ctThread.Start();


            backgroundWorker2.RunWorkerAsync();

        }

        
        #endregion Mettler

  
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
            {
            connectWeight();
            }

        private void timer2_Tick(object sender, EventArgs e)
            {
            PHOENIX_ENGINE();
            }
        
        public void logData(bool sent, string text)
        {
            txtQRCode1.Text = "";
            text = text.Replace("\r\n", "");
            txtQRCode1.Text =text;
           // Console.WriteLine("CAM1 logdata = " + text);
            try
            {
                string[] strSplit = text.Split(',');
               // txtQRCode.Text = strSplit[0];

             

                // Pass = 1 Fail =0 
                if (strSplit[1] == "0")
                {
                    imgStatus = "FAIL";
                }
                else
                {
                    imgStatus = "PASS";
                }

            }
            catch (Exception ex) { MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", ex.Message); }
            try
            {
                popLastIMG();
            }
            catch (Exception ex) { MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", ex.Message + "[PopLastImage]"); }
        }

        public void logData2(bool sent, string text)
        {          
           txtQRCode2.Text = "";
                #region CAM_2
                text = text.Replace("\r\n", "");
                txtQRCode2.Text = text;
                try
                {
                    string[] strSplit = text.Split(',');
                    txtQRCode.Text = strSplit[0];

                  //  if (keepWeight == "")
                  //  {
                  //      txtWeight.Text = "";
                  //  }

                    // Pass = 1 Fail =0 
                    if (strSplit[1] == "0")
                    {
                        imgStatus_2 = "FAIL";
                    }
                    else
                    {   
                        imgStatus_2 = "PASS";     
                    }
                }
                catch (Exception ex) { MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", ex.Message); }
                try
                {
                    popLastIMG_2();
                }
                catch (Exception ex) { MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", ex.Message + "[PopLastImage]"); }
                #endregion  //CAM_2
        
        }

        private void tcpServer2_OnDataAvailable(tcpServer.TcpServerConnection connection)
        {

            byte[] data = readStream(connection.Socket);

            if (data != null)
            {
                string dataStr = Encoding.ASCII.GetString(data);

                invokeDelegate del2 = () =>
                {
                    logData2(false, dataStr);
                };
                Invoke(del2);

                data = null;
            }
        }

        private void tcpServer2_OnConnect_1(tcpServer.TcpServerConnection connection)
        {
            invokeDelegate setText2 = () => lblConnected2.Text = tcpServer2.Connections.Count.ToString();

            Invoke(setText2);
        }
           
        private void txtQRCode1_TextChanged(object sender, EventArgs e)
        {            
            if (!isRunning)
            {
                //Start Timer
                timer3.Interval = Convert.ToInt32(WAIT_CAM);
                timer3.Enabled = true;
                isRunning = true;
                Console.WriteLine("QR 1 come ");
            }
        }

        private void txtQRCode2_TextChanged(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                //Start Timer
                timer3.Interval = Convert.ToInt32(WAIT_CAM);
                timer3.Enabled = true;
                isRunning = true;
                Console.WriteLine("QR 2 come ");
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {

            if (txtQRCode1.Text == txtQRCode2.Text)
            {
                string[] arrQR = txtQRCode1.Text.Split(',');
                if (arrQR.Length < 2)
                {
                    // txtQR is blank
                    txtQRCode.Text = "**";
                    MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "USR", " !!! QR is blank !!! -- Job fail");
                }
                else
                {
                    // txtQR is T,T or F,F
                    if (arrQR[1] == "0")
                    {
                        // Fail QR
                        txtQRCode.BackColor = Color.Red;
                        txtQRCode.Text = "--";
                     
                        setAlarmLedCAM(true, "!!! CAN NOT DETECT QR CODE from both CAM  !!!");
                        qrList.Add("F," + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));     
                        MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "USR", " !!! QR is Fail [CAM 1 and CAM 2]  !!! -- Job fail");

                    }
                    else
                    {
                        // Good QR
                        txtQRCode.BackColor = Color.Green;
                      
                        txtQRCode.Text = arrQR[0];
                        qrList.Add(arrQR[0] + "," + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                        Console.WriteLine("Add QR into Qr Queue " + arrQR[0] + "," + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                    }

                }
            }
            else {
                if (txtQRCode1.TextLength > txtQRCode2.TextLength)
                {
                    // QR1 is good , QR2 is bad
                    string[] arrQR = txtQRCode1.Text.Split(',');
                    txtQRCode.BackColor = Color.Green;
            

                    txtQRCode.Text = arrQR[0];
                    qrList.Add(arrQR[0] + "," + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                    Console.WriteLine("Add QR into Qr Queue  QR1 Good , QR2 Bad  " + arrQR[0] + "," + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                }
                else { 
                // QR1 is bad , QR2 is good
                    string[] arrQR = txtQRCode2.Text.Split(',');
                    txtQRCode.BackColor = Color.Green;
                 
                    txtQRCode.Text = arrQR[0];
                    qrList.Add(arrQR[0] + "," + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                    Console.WriteLine("Add QR into Qr Queue  QR1 Bad , QR2 Good  " + arrQR[0] + "," + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                }     
            }
            timer3.Enabled = false;
            isRunning = false;
        }


        //@Oct 2019
        private void MZM_DB_CHK_LOG(string mod_name, string level, string msg)
        {
            try
            {
                string appPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                string logName = appPath + @"\MZM_DB_CHK_" + DateTime.Today.ToString("ddMMyyyy") + ".log";

                using (StreamWriter sw = new StreamWriter(logName, true))
                {
                    sw.WriteLine(DateTime.Now.ToString("dd-MMM-yyy HH:mm:ss ") + ":" + level + ":" + mod_name + ":" + msg);
                    sw.Flush();
                    sw.Close();
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }



        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            bool loop = true;
            string retDB = "0";

            do
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                        try
                        {
                            Ping myPing = new Ping();
  
                            #region PING_DB
                            try
                            {
                                PingReply reply_db = myPing.Send(DB_IP, 250);
                                if ((reply_db != null) & (reply_db.Status == IPStatus.Success))
                                {
                                    retDB = "1";
                                }
                                else
                                {
                                    retDB = "0";
                                    noDBFailCount = noDBFailCount + 1;
                                    MZM_DB_CHK_LOG(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", "Checking DB faliure HB Number --> " + indexDBCount.ToString() +"  "+ DB_IP.ToString());
                                }
                               
                            }
                            catch (System.Net.NetworkInformation.PingException ex) {
                                retDB = "0";
                                noDBFailCount = noDBFailCount + 1;
                                MZM_DB_CHK_LOG(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", ex.Message + "[PING_DB failure]  " + indexDBCount.ToString() +"  "+ DB_IP.ToString());
                              //  MZM_DB_CHK_LOG(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", "Fail on DB Checking HB Number --> " + indexDBCount.ToString());
                            }

                            indexDBCount = indexDBCount + 1;

                            #endregion

                            if (indexDBCount > DBLOOP_COUNT) {
                                worker.ReportProgress(noDBFailCount);
                                indexDBCount = 0;
                                noDBFailCount = 0;
                            }

                        }
                        catch (Exception ex)
                        {
                            MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", ex.Message + "[DO PING failure ]");
                            worker.ReportProgress(999);
                        }
                    }

                System.Threading.Thread.Sleep(1000);
            }
            while (loop);
        }

        private void backgroundWorker3_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                if (e.ProgressPercentage != 999) {
                    if (e.ProgressPercentage > DBFAIL_COUNT)
                    {
                        this.onlineLedDB.LedColor = Color.Red;
                        setAlarmLedDB(true, "!!! DB IS NOT READY !!!");
                        if (alarmFlag == false) { this.almLedDB.LedColor = Color.Red; }
                        MZM_DB_CHK_LOG(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", "Alarm for DB checking , The failure over than threshold [" + e.ProgressPercentage.ToString() + "/" + DBFAIL_COUNT.ToString() + "]");
                        //   this.almLedDB.LedColor = Color.Red;
                    }
                    else
                    {
                        this.onlineLedDB.LedColor = Color.Lime;
                        setAlarmLedDB(false, "--");
                       // MZM_DB_CHK_LOG(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", "Normal state for DB checking , The failure not over than threshold [" + e.ProgressPercentage.ToString() + "/" + DBFAIL_COUNT.ToString() + "]");
         

                    }

                }
                else
                { // PING RETUNE !SUCCESS
                    this.onlineLedDB.LedColor = Color.Yellow;
                    this.almLedDB.LedColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MZM_Log(System.Reflection.MethodBase.GetCurrentMethod().Name, "SYS", ex.Message);
            }
        }
        
        //-------------------------------------------------------------------------------------------------
    

        #region SPARE

        private void btnDBConnect_Click(object sender, EventArgs e)
        {
            SqlConnection objConn = new SqlConnection();
            SqlCommand objCmd = new SqlCommand();
            string strConnString = null;
            string strSQL = null;
            strConnString = "Server=localhost;UID=sa;PASSWORD=leibboos;Database=master;Max Pool Size=400;Connect Timeout=600;";
            objConn.ConnectionString = strConnString;
            objConn.Open();
            strSQL = "SELECT COUNT(*) FROM conveyor.dbo.product_info ";
            objCmd = new SqlCommand(strSQL, objConn);
            //label6.Text = Convert.ToInt32(objCmd.ExecuteScalar().
            using (SqlDataReader reader = objCmd.ExecuteReader())
            {
                // while there is another record present
                while (reader.Read())
                {
                    // write the data on to the screen
                    //Console.WriteLine(String.Format("{0} \t ",
                    // call the objects from their index
                    //reader[0]));
                    label6.Text = Convert.ToString(reader[0]);
                }

                objConn.Close();
                objConn = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            imgBox.ImageLocation = IMG_LOCATION + "/master.bmp";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WaybillChecking("1234");

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            setGPIO(false);
            btnAlmReset.Enabled = false;
            this.almLedOutput.State = LBSoft.IndustrialCtrls.Leds.LBLed.LedState.Off;
            lastWeight = "";
            lbAlarm.Text = "--";
        }

        #endregion





    }
}
