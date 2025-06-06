#undef EXCEPTION_CATCHER
#undef BUILD_DEFAULT_SETTINGS
#undef CREATE_TEST_DATABASE
#undef PRESS_A_KEY_TO_EXIT

using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Runtime.ExceptionServices;
using System.Diagnostics;

using ProRob;
using ProRob.Log;
using ProRob.OperatingSystems.Signals;
using ProRob.Extensions.Double;

using Machine;
using Machine.Utility;
using Machine.Control.LowLevel.MachineController;

using Caron.Cradle.Control.LowLevel;
using Caron.Cradle.Control.HighLevel;
using Caron.Cradle.Control.Database;
using Microsoft.Win32;
//GPIx164
using System.Net.Sockets;
using System.Net;
using Caron.Cradle.Control.LowLevel.Communication;
//GPFx164

namespace Caron.Cradle.Control
{
    public class MachineControllerApplication
    {
        public static MachineController Cradle { get; set; }
        private static IDisposable CradleWebApi { get; set; }

        public static ProLogLiteTXT HighLevelLogX { get; set; }
        public static ProLogTextWriter ProLogTextWriterX { get; set; }
        public static ConsoleRedirectWriter ConsoleRedirectWriterX { get; set; }
        public static ProLogLiteTXT UiLogX { get; set; }
        public static ProLogLiteTXT LowLevelLogX { get; set; }

        //GPIx7
        public static volatile bool appIsSwitchingOff = false;
        //GPFx7

        //GPI18
        public static bool NoInitCheckPhotocell { get; set; }
        //GPF18

        //GPIx7
        public static volatile bool reboot = true;
        //GPFx7


        //////funzione delegata per istanza-evento ConsoleRedirectWriterX.OnWrite:
        //public delegate void OnWriteMethod(string value);
        //public delegate void Action<in T>(T value);
        ////// Create a method for a delegate.
        public static void DelegateOnWriteMethod(string value)
        {
            //Console.WriteLine(value);
            ProLogTextWriterX.Write(value);
        }


        [STAThread]
        static void Main(string[] args)
        {
            //GPIx130
            if (File.Exists(System.IO.Path.Combine(Constants.Path.BinFolder, "control\\ritardo.ini")))
            {
                string str01 = File.ReadAllText(System.IO.Path.Combine(Constants.Path.BinFolder, "control\\ritardo.ini"));
                try
                {
                    int value = int.Parse(str01);
                    Thread.Sleep(value);
                }
                catch (Exception e)
                {

                }
            }
            //GPFx130

            //GPIx164
            if (File.Exists(System.IO.Path.Combine(Constants.Path.LogsFolder, "log_control.db")))
            {
                try
                { File.Delete(System.IO.Path.Combine(Constants.Path.LogsFolder, "log_control.db")); }
                catch (Exception e) { }
            }
            if (File.Exists(System.IO.Path.Combine(Constants.Path.LogsFolder, "log_low_level_control.db")))
            {
                try
                { File.Delete(System.IO.Path.Combine(Constants.Path.LogsFolder, "log_low_level_control.db")); }
                catch (Exception e) { }
            }
            if (File.Exists(System.IO.Path.Combine(Constants.Path.LogsFolder, "log_ui.db")))
            {
                try
                { File.Delete(System.IO.Path.Combine(Constants.Path.LogsFolder, "log_ui.db")); }
                catch (Exception e) { }
            }
            //GPIx164

            //GPI18
            MachineControllerApplication.NoInitCheckPhotocell = true;
            //GPF18

            //-----------------------------------------------------------
            // Regional setting FIX
            //-----------------------------------------------------------
            #region Regional_setting_FIX
            //se true blocco programma e correggo impostazioni internazionali:
            Boolean bInternationalOptions = false;

            //
            //this is how your key will look like
            //the 2nd argument (true) is indicating that the key is writable
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);

            //check sDecimal regional setting key is the decimal separator:
            if (key.GetValue("sDecimal") == null)
            {
                //code if key Not Exist
                Console.WriteLine($"the registry key sDecimal doesn't exist!!!");
            }
            else
            {
                //code if key Exist
                string data = key.GetValue("sDecimal").ToString();

                if (!data.Equals(","))
                {
                    //adding/editing a value 
                    key.SetValue("sDecimal", ",");
                    Console.WriteLine($"the registry key sDecimal is fixed!");
                    bInternationalOptions = true;
                }
                else
                {
                    Console.WriteLine($"sDecimal key is ok!");
                }
            }

            //check sThousand regional setting key is the decimal separator:
            if (key.GetValue("sThousand") == null)
            {
                //code if key Not Exist
                Console.WriteLine($"the registry key sThousand doesn't exist!!!");
            }
            else
            {
                //code if key Exist
                string data = key.GetValue("sThousand").ToString();  //returns the text found in 'someValue'

                if (!data.Equals("."))
                {
                    //adding/editing a value 
                    key.SetValue("sThousand", "."); //sets 'someData' in 'someValue'
                    Console.WriteLine($"the registry key sThousand is fixed!");
                    bInternationalOptions = true;
                }
                else
                {
                    Console.WriteLine($"sThousand key is ok!");
                }
            }

            //and finally, you close the key
            key.Close();

            if (bInternationalOptions)
            {
                Console.WriteLine("Wrong Regional Options fixed, exiting to restart application!");
                Thread.Sleep(10000);
                return;
            }

            #endregion

            //-----------------------------------------------------------
            // Console
            //-----------------------------------------------------------
            #region Console
#if !DEBUG
            ProConsole.MinimizeConsoleWindow();
            //ProConsole.HideConsoleWindow();
#endif
            #endregion

            //-----------------------------------------------------------
            // ApplicationInfo
            //-----------------------------------------------------------
            #region ApplicationInfo
            ApplicationInfo.SetApplicationName("CARON Cradle Control");
            ApplicationInfo.SetApplicationVersion(Constants.ApplicationVersion);
            #endregion

            //-----------------------------------------------------------
            // Guid Session
            //-----------------------------------------------------------
            LogId.SetGuidSession(HighLevel.ControlStatus.GuidSession);
            MachineEvent.SetGuidSession(HighLevel.ControlStatus.GuidSession);

            //-----------------------------------------------------------
            // Check if process is already running
            //-----------------------------------------------------------
            #region Check if process is already running
            if (Machine.ApplicationManager.CheckIfProcessIsAlreadyRunning(ApplicationInfo.ProcessName) == ProcessStatus.Running)
            {
                return;
            }
            #endregion

            //-----------------------------------------------------------
            // Machine Data
            //-----------------------------------------------------------
            #region Machine Data

            Directory.CreateDirectory(Machine.Constants.Path.TmpFolder);          
            Directory.CreateDirectory(Constants.Path.SettingsDefaultFolder);

            bool buildSettings = false;
            if (!Directory.Exists(Constants.Path.SettingsFolder))
            {
                Directory.CreateDirectory(Constants.Path.SettingsFolder);
                buildSettings = true;
            }
            else if (Directory.EnumerateFiles(Constants.Path.SettingsFolder).Count() == 0)
            {
                buildSettings = true;
            }

            MachineData.Initialize(Constants.Path.SettingsFolder);
            if (buildSettings)
            {
                Console.WriteLine("Building defaults settings");
                BuilderSettings.CreateDefaultSettings();
            }

            //GPIx101 NEW DIGITAL INPUT
            BuilderSettings.CreateDefaultSettingsNewDigitalInput();
            //GPFx101
            #endregion

            //-----------------------------------------------------------
            // Builders
            //-----------------------------------------------------------
            #region Builders
#if BUILD_DEFAULT_SETTINGS
                FileSystem.DeleteDirectory(Constants.Path.SettingsFolder);
                Directory.CreateDirectory(Constants.Path.SettingsFolder);
                Control.HighLevel.Builders.MachineSettings.CreateDefaultSettings();
                MessageBox.Show("BUILD_DEFAULT_SETTINGS");
#endif
#if CREATE_TEST_DATABASE && TEST
                DatabaseWorkingsBuilder.CreateTestDatabase();
                MessageBox.Show("CREATE_TEST_DATABASE");
#endif
#if BUILD_DEFAULT_SETTINGS || CREATE_TEST_DATABASE
                return;
#endif
            #endregion

            //-----------------------------------------------------------
            // IO Maps
            //-----------------------------------------------------------
            #region IO Maps
            Directory.CreateDirectory(Constants.Path.SettingsIOFolder);

            if (File.Exists(Constants.Path.LowLevelControl.DigitalInputsMapFile) == false)
            {
                string contents = @"fuse_check_motors                    :01
titan_limit                          :02
photocell_op_side                    :03
photocell_mt_side                    :04
photocell_material                   :05
limit_cutter_op_side                 :06
limit_cutter_mt_side                 :07
limit_dancer                         :08
limit_alignment_op_side              :09
limit_alignment_mt_side              :10
limit_overturning_op_side_load       :11
limit_overturning_op_side_unload     :12
limit_spoon_up                       :13
limit_spoon_down                     :14
limit_overturning_mt_side_load       :15
limit_overturning_mt_side_unload     :16
zund_enable                          :17
zund_cut_off                         :18
photocell_roll_presence              :19
input_driver04                       :20";

                File.WriteAllText(Constants.Path.LowLevelControl.DigitalInputsMapFile, contents);
            }
            //GPIx133
            else
            {
                var contents = File.ReadAllLines(Constants.Path.LowLevelControl.DigitalInputsMapFile);

                contents = contents.Select(x => x.Replace("input_driver03", "photocell_roll_presence")).ToArray();

                File.WriteAllLines(Constants.Path.LowLevelControl.DigitalInputsMapFile, contents);
            }
            //GPFx133

            if (File.Exists(Constants.Path.LowLevelControl.DigitalInputsTypeMapFile) == false)
            {
                string contents = @"fuse_check_motors                    :0
titan_limit                          :0
photocell_op_side                    :1
photocell_mt_side                    :1
photocell_material                   :1
limit_cutter_op_side                 :0
limit_cutter_mt_side                 :0
limit_dancer                         :0
limit_alignment_op_side              :0
limit_alignment_mt_side              :0
limit_overturning_op_side_load       :0
limit_overturning_op_side_unload     :0
limit_spoon_up                       :0
limit_spoon_down                     :0
limit_overturning_mt_side_load       :0
limit_overturning_mt_side_unload     :0
zund_enable                          :1
zund_cut_off                         :1
photocell_roll_presence              :1
input_driver04                       :1";

                File.WriteAllText(Constants.Path.LowLevelControl.DigitalInputsTypeMapFile, contents);
            }
            //GPIx133
            else
            {
                var contents = File.ReadAllLines(Constants.Path.LowLevelControl.DigitalInputsTypeMapFile);

                contents = contents.Select(x => x.Replace("input_driver03", "photocell_roll_presence")).ToArray();

                File.WriteAllLines(Constants.Path.LowLevelControl.DigitalInputsTypeMapFile, contents);
            }
            //GPFx133

            if (File.Exists(Constants.Path.LowLevelControl.DigitalOutputsMapFile) == false)
            {
                string contents = @"motor_overturning_op_side_load     :01
motor_overturning_op_side_unload   :02
motor_alignment_op_side            :03
motor_alignment_mt_side            :04
titan_up                           :05
titan_down                         :06
motor_spoon_up                     :09
motor_spoon_down                   :10
motor_overturning_mt_side_load     :11
motor_overturning_mt_side_unload   :12
march_enabled                      :13
axis_cradle_to_cutter_exchange     :14
output07                           :07
output08                           :08
output15                           :15
output16                           :16
zund_error                         :17
zund_status                        :18
cradle_cutter_lock                 :19
output_driver04                    :20";

                File.WriteAllText(Constants.Path.LowLevelControl.DigitalOutputsMapFile, contents);
            }

            if (File.Exists(Constants.Path.LowLevelControl.AnalogInputsMapFile) == false)
            {
                string contents = @"dancer		:1";

                File.WriteAllText(Constants.Path.LowLevelControl.AnalogInputsMapFile, contents);
            }

            //            if (File.Exists(Constants.Path.LowLevelControl.AnLowLewHighLevMapFile) == false)
            //            {
            //                string contents = @"dancer		                    :DanceBar
            //knob		                    :Knob
            //laser		                    :Laser";

            //                File.WriteAllText(Constants.Path.LowLevelControl.AnLowLewHighLevMapFile, contents);
            //            }

            #endregion

            //-----------------------------------------------------------
            // LOG
            //-----------------------------------------------------------
            #region LOG 
            //ProLogLiteDB highLevelLog = new ProLogLiteDB(Constants.Path.Log.ControlLogFile);
            //ProLogLiteTXT highLevelLog = new ProLogLiteTXT(Constants.Path.LogsFolder, Constants.Path.Log.ControlLogFilename);
            HighLevelLogX = new ProLogLiteTXT(Constants.Path.LogsFolder, Constants.Path.Log.ControlLogFilename);
            //ProLogTextWriter proLogTextWriter = new ProLogTextWriter(highLevelLog);
            ProLogTextWriterX = new ProLogTextWriter(HighLevelLogX);
            //ConsoleRedirectWriter consoleRedirectWriter = new ConsoleRedirectWriter();
            ConsoleRedirectWriterX = new ConsoleRedirectWriter();

            Action<string> OnWriteMethod1 = new Action<string>(DelegateOnWriteMethod);
            //Action<string> OnWriteMethod2 = DelegateOnWriteMethod;

            //consoleRedirectWriter.OnWrite += delegate (string value)
            //{
            //    proLogTextWriter.Write(value);
            //};
            ConsoleRedirectWriterX.OnWrite += OnWriteMethod1;

            //ProLogLiteDB uiLog = new ProLogLiteDB(Constants.Path.Log.UILogFile);
            //ProLogLiteTXT uiLog = new ProLogLiteTXT(Constants.Path.LogsFolder, Constants.Path.Log.UILogFilename);
            UiLogX = new ProLogLiteTXT(Constants.Path.LogsFolder, Constants.Path.Log.UILogFilename);

            //ProLogLiteDB lowLevelLog = new ProLogLiteDB(Constants.Path.Log.LowLevelControlLogFile);
            //ProLogLiteTXT lowLevelLog = new ProLogLiteTXT(Constants.Path.LogsFolder, Constants.Path.Log.LowLevelControlLogFilename);
            LowLevelLogX = new ProLogLiteTXT(Constants.Path.LogsFolder, Constants.Path.Log.LowLevelControlLogFilename, addNewLine: true);

#if !(TEST || DEBUG)
            UdpBackgroundReceiver udpBackgroundReceiver = new UdpBackgroundReceiver(Machine.Constants.Networking.LowLevelControlLoggerUdpPort);
            udpBackgroundReceiver.Start();
            udpBackgroundReceiver.OnDataReceive += delegate (byte[] dataPacket)
            {
                Task.Run(() =>
                {
                    Thread.CurrentThread.Priority = ThreadPriority.Lowest;
                    string message = Encoding.ASCII.GetString(dataPacket.TakeWhile(x => x != 0).ToArray()).TrimEnd('\r', '\n');
                    //lowLevelLog.AddLog(message);
                    LowLevelLogX.AddLog(message);
                    //Console.WriteLine($"proLowLevelLogger:{message}");
                });
            };
#else
            UdpBackgroundReceiver udpBackgroundReceiver = null;
#endif
            #endregion

            //-----------------------------------------------------------
            // Shell commands
            //-----------------------------------------------------------
            #region Shell commands
            Console.WriteLine("De-linking \"pdf\" folder..");
            ProcessHelper.ExecuteShellCommand(@"rmdir -r c:\caron\machine_cradle\www\pdf");
            FileSystem.DeleteDirectory(@"c:\caron\machine_cradle\www\pdf");

            Console.WriteLine("Linking \"pdf\" folder..");
            ProcessHelper.ExecuteShellCommand(@"mklink /D c:\caron\machine_cradle\www\pdf c:\caron\machine_cradle\assets\pdfs");
#if !TEST
            if (Process.GetProcessesByName("httpd").Count() == 0)
            {
                Console.WriteLine("Starting Web Server..");

                var httpd = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = Path.Combine(Caron.Cradle.Constants.Path.BinServerFolder, @"httpd\bin\httpd.exe"),
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden
                    }
                };

                httpd.Start();
            }
#endif
            #endregion

            try
            {
                //-----------------------------------------------------------
                // Start Application
                //-----------------------------------------------------------
                ProConsole.WriteLine(ApplicationInfo.Instance, ConsoleColor.Red);
                ProConsole.WriteLine($"GuidSession: {HighLevel.ControlStatus.GuidSession}", ConsoleColor.Red);

                //-----------------------------------------------------------
                // Initialization
                //-----------------------------------------------------------
                #region Current Process

                System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.High;

                #endregion

                #region System
                //Console.WriteLine("fsutil dirty set c:");
                #endregion

                //-----------------------------------------------------------
                // Send shutdown command (Low Level Controller)
                //-----------------------------------------------------------
                #region Send shutdown command
#if !TEST
                if (Machine.Control.LowLevel.Communicator.SendShutdownCommand())
                {
                    Console.WriteLine("[ LowLevel.Communicator] SendShutdownCommand()");
                    Thread.Sleep(Machine.Constants.Intervals.WaitAfterLowLevelControlShutdownCommand);
                }
#endif
                #endregion

                //-----------------------------------------------------------
                // Exceptions Catcher
                //-----------------------------------------------------------
                #region Exceptions Catcher
#if EXCEPTION_CATCHER
                EventHandler<FirstChanceExceptionEventArgs> eventHandler = delegate (object o, FirstChanceExceptionEventArgs e)
                {
                    string exception = ProRob.Exceptions.Handlers.FirstChanceExceptionLoggerHandler(e);

                    if (!string.IsNullOrEmpty(exception))
                    {
#if DEBUG
                        ProConsole.WriteLine($"[EXCEPTION_CATCHER: {exception}]", ConsoleColor.DarkYellow);
#endif
                        proLogTextWriter.Write(exception, LogType.Exception);
                    }
                };

                AppDomain.CurrentDomain.FirstChanceException += eventHandler;
#endif
                #endregion

                //-----------------------------------------------------------
                // Send Low Level Control Firmware
                //-----------------------------------------------------------
                #region Send Low Level Control Firmware
#if !TEST

                var ftp = new ProRob.Net.Ftp(Machine.Constants.Networking.IPAddressLowLevelControl);

                //GPIx7
                //if (true)     //////forzatura reboot twincat2 macchine per test.
                if (MachineControllerManager.SendHello() == false)
                {
                    if (reboot)
                    {
                        string rebootFilename = "reboot.txt";
                        string rebootFile = System.IO.Path.Combine(Constants.Path.MachineFolder, rebootFilename);

                        if (File.Exists(rebootFile) == false)
                        {
                            string contents = @"";
                            File.WriteAllText(rebootFile, contents);
                        }

                        try
                        {
                            //reboot file:
                            ftp.UploadFile(
                               rebootFile,
                               rebootFilename,
                               true);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("{0} Exception caught.", e);
                            Console.WriteLine("{0} Exception Message.", e.Message);
                            //Console.WriteLine("{0} Exception Message.", e.StackTrace);
                        }
                        Thread.Sleep(Machine.Constants.Intervals.WaitAfterUploadFiles);

                        Console.WriteLine("Rebooting Machine..");  //rebooting twincat2 machine.
                        //riposo 80 secondi intanto che fa il reboot!!!
                        Thread.Sleep(80000);  //GPIx130

                        reboot = false;

                        //Constants.Path.MachineFolder
                        //public static readonly string MachineFolder = System.IO.Path.Combine(RootFolder, $"machine_{MachineShortName.ToLower()}");
                    }
                }
                //GPFx7

                while (MachineControllerManager.SendHello() == false)
                {
                    Console.WriteLine("Waiting LowLevelMachineManager..");
                }
                Thread.Sleep(Machine.Constants.Intervals.WaitAfterLowLevelMachineManagerHelloCommand);

                //GPIx21
                //Digital Inputs Map
                ftp.UploadFile(
                   Constants.Path.LowLevelControl.DigitalInputsMapFile,
                   Constants.Path.LowLevelControl.DigitalInputsMapFilename,
                   true);
                Thread.Sleep(Machine.Constants.Intervals.WaitAfterUploadFiles);

                //Digital Inputs Map Type
                ftp.UploadFile(
                   Constants.Path.LowLevelControl.DigitalInputsTypeMapFile,
                   Constants.Path.LowLevelControl.DigitalInputsTypeMapFilename,
                   true);
                Thread.Sleep(Machine.Constants.Intervals.WaitAfterUploadFiles);

                //Digital Outputs Map
                ftp.UploadFile(
                   Constants.Path.LowLevelControl.DigitalOutputsMapFile,
                   Constants.Path.LowLevelControl.DigitalOutputsMapFilename,
                   true);
                Thread.Sleep(Machine.Constants.Intervals.WaitAfterUploadFiles);

                //Analog Inputs Map
                ftp.UploadFile(
                   Constants.Path.LowLevelControl.AnalogInputsMapFile,
                   Constants.Path.LowLevelControl.AnalogInputsMapFilename,
                   true);
                Thread.Sleep(Machine.Constants.Intervals.WaitAfterUploadFiles);
                //GPFx21

                //GPIx130
                String LowLevelControlFileX;
                if (ftp.FileExists("X86.txt"))
                {
                    LowLevelControlFileX = System.IO.Path.Combine(Constants.Path.LowLevelControlBinFolder + "\\X86", Constants.Path.LowLevelControl.LowLevelControlFilename);
                }
                else
                {
                    LowLevelControlFileX = System.IO.Path.Combine(Constants.Path.LowLevelControlBinFolder + "\\ARM", Constants.Path.LowLevelControl.LowLevelControlFilename);
                }

                //Control
                ftp.UploadFile(
                    //Constants.Path.LowLevelControl.LowLevelControlFile,
                    LowLevelControlFileX,
                    Constants.Path.LowLevelControl.LowLevelControlFilename,
                    true);
                Thread.Sleep(Machine.Constants.Intervals.WaitAfterUploadFiles);
                //GPFx130

                MachineControllerManager.SendStartLowLevelControlProcess();
                Thread.Sleep(1500);
#endif
                #endregion

                //inserire qua controllo porte 5000 e 10000 se no reset e reinvii -> Send Low Level Control Firmware:   //GPIx164  
                //GPIx164		rimando via FTP file dopo procedura di reset:
                #region Send Low Level Control Firmware after reset PLC with UDP 10000 or 5000 not working fine:
#if !TEST
                //inizializzazione porta UDP 10000,5000 nel basso livello gli do 5 secondi:
                //Thread.Sleep(5000);
                Thread.Sleep(3500);

                //inserire qui check udp 5000 e 10000:
                //----------------------------------------------------------
                //check porta 10000 UDP:
                IPEndPoint ipEndPointX;
                UdpClient udpClientTestAvvio;   //non usare udpClient fai udpClientTestAvvio
                //GPIx129 se il socket è in uso riutilizza l'indirizzo (porta):
                udpClientTestAvvio = new UdpClient(Machine.Constants.Networking.LowLevelControlUdpPort);
                //udpClientTestAvvio.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                ipEndPointX = new IPEndPoint(IPAddress.Parse(Machine.Constants.Networking.IPAddressLowLevelControl), Machine.Constants.Networking.LowLevelControlUdpPort);
                //GPFx129
                udpClientTestAvvio.Connect(ipEndPointX);
                udpClientTestAvvio.Client.ReceiveTimeout = (int)Machine.Constants.Timeouts.LowLevelTimeoutCommunication.TotalMilliseconds;

                bool testUdp10000 = false;
                try
                {
                    #region Low Level Data Packet Receive
                    //-----> fai un ciclo for con 10 chiamate alla porta 10000	con uno sleep(100) alla fine del ciclo!!!	
                    for (int i = 0; i < 10; i++)
                    {
                        byte[] buffer = udpClientTestAvvio.Receive(ref ipEndPointX);
                        if (buffer.Length == 0)
                        {
                            continue;
                        }
                        //Console.Write(i);
                        Thread.Sleep(100);
                    }
                    #endregion
                }
                catch
                {
                    testUdp10000 = true;
                }

                udpClientTestAvvio.Close();
                //----------------------------------------------------------
                //check porta 5000 UDP:
                //creo un comunicator sulla porta udp 5000 di test per vedere se la porta 5000 funziona:
                Communicator CommunicatorTest;
                bool testUdp5000 = false;
                CommunicatorTest = new Communicator(
                                Machine.Constants.Networking.IPAddressLowLevelControl,
                                Machine.Constants.Networking.LowLevelControlTcpPort,
                                null,
                                null,
                                ref testUdp5000);   // aggiungi nuovo public Communicator(string server, int port, Control.LowLevel.ControlStatus lowLevel, Control.HighLevel.ControlStatus highLevel,ref bool test)
                                                    // per aggiungere variabile booleana se non avviene connessione metti testUdp5000=false se no testUdp5000=true !!!!

                //////QUESTA PARTE NON VOGLIO ESEGUIRLA IN QUANTO SE CHIUDO LA COMUNICAZIONE FUNZIONATE VA IN EMERGENZA E IL BASSO LIVELLO
                ///     SI SPEGNE. IO NON VOGLIO CHIUDERE E SE NON COMUNICA CI PENSA IL RESET:
                /*
                CommunicatorTest.Close();
                CommunicatorTest.Dispose();
                //////riavvio il programma controllore di basso livello perchè il close lo spegne dopo aver controllato che la porta udp 5000 e 10000 funzionano: 
                Thread.Sleep(10000);
                MachineControllerManager.SendStartLowLevelControlProcess();
                Thread.Sleep(5000);
                */
                //----------------------------------------------------------

                var ftpT = new ProRob.Net.Ftp(Machine.Constants.Networking.IPAddressLowLevelControl);

                //GPIx4
                //if (true)     //////forzatura reboot twincat2 macchine per test.
                //////if (MachineControllerManager.SendHello() == false)
                if (testUdp10000 || testUdp5000)  //check udp 10000 + check udp 5000
                {
                    reboot = true;
                    if (reboot)
                    {
                        string rebootFilename = "reboot.txt";
                        string rebootFile = System.IO.Path.Combine(Constants.Path.MachineFolder, rebootFilename);

                        if (File.Exists(rebootFile) == false)
                        {
                            string contents = @"";
                            File.WriteAllText(rebootFile, contents);
                        }

                        try
                        {
                            //reboot file:
                            ftpT.UploadFile(
                               rebootFile,
                               rebootFilename,
                               true);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("{0} Exception caught.", e);
                            Console.WriteLine("{0} Exception Message.", e.Message);
                            //Console.WriteLine("{0} Exception Message.", e.StackTrace);
                        }
                        Thread.Sleep(Machine.Constants.Intervals.WaitAfterUploadFiles);

                        Console.WriteLine("Rebooting Machine..");  //rebooting twincat2 machine.
                        //riposo 80 secondi intanto che fa il reboot!!!
                        Thread.Sleep(80000);

                        reboot = false;

                        //Constants.Path.MachineFolder
                        //public static readonly string MachineFolder = System.IO.Path.Combine(RootFolder, $"machine_{MachineShortName.ToLower()}");
                    }

                    //GPFx4	

                    while (MachineControllerManager.SendHello() == false)
                    {
                        Console.WriteLine("Waiting LowLevelMachineManager II..");
                        Thread.Sleep(Machine.Constants.Intervals.SlowWaitSleep);
                    }
                    Thread.Sleep(Machine.Constants.Intervals.WaitAfterLowLevelMachineManagerHelloCommand);

                    //Digital Inputs Map
                    ftpT.UploadFile(
                       Constants.Path.LowLevelControl.DigitalInputsMapFile,
                       Constants.Path.LowLevelControl.DigitalInputsMapFilename,
                       true);
                    Thread.Sleep(Machine.Constants.Intervals.WaitAfterUploadFiles);

                    //Digital Inputs Map Type
                    ftpT.UploadFile(
                       Constants.Path.LowLevelControl.DigitalInputsTypeMapFile,
                       Constants.Path.LowLevelControl.DigitalInputsTypeMapFilename,
                       true);
                    Thread.Sleep(Machine.Constants.Intervals.WaitAfterUploadFiles);

                    //Digital Outputs Map
                    ftpT.UploadFile(
                       Constants.Path.LowLevelControl.DigitalOutputsMapFile,
                       Constants.Path.LowLevelControl.DigitalOutputsMapFilename,
                       true);
                    Thread.Sleep(Machine.Constants.Intervals.WaitAfterUploadFiles);

                    //Analog Inputs Map
                    ftpT.UploadFile(
                       Constants.Path.LowLevelControl.AnalogInputsMapFile,
                       Constants.Path.LowLevelControl.AnalogInputsMapFilename,
                       true);
                    Thread.Sleep(Machine.Constants.Intervals.WaitAfterUploadFiles);

                    //GPIx130
                    if (ftp.FileExists("X86.txt"))
                    {
                        LowLevelControlFileX = System.IO.Path.Combine(Constants.Path.LowLevelControlBinFolder + "\\X86", Constants.Path.LowLevelControl.LowLevelControlFilename);
                    }
                    else
                    {
                        LowLevelControlFileX = System.IO.Path.Combine(Constants.Path.LowLevelControlBinFolder + "\\ARM", Constants.Path.LowLevelControl.LowLevelControlFilename);
                    }

                    //Control
                    ftp.UploadFile(
                        //Constants.Path.LowLevelControl.LowLevelControlFile,
                        LowLevelControlFileX,
                        Constants.Path.LowLevelControl.LowLevelControlFilename,
                        true);
                    Thread.Sleep(Machine.Constants.Intervals.WaitAfterUploadFiles);
                    //GPFx130

                    MachineControllerManager.SendStartLowLevelControlProcess();
                    Thread.Sleep(1500);
                }
#endif
                #endregion
                //GPFx164

                //-----------------------------------------------------------
                // Machine Event
                //-----------------------------------------------------------
                DatabaseWorkings.Add(new MachineEvent(MachineEventType.MachinePowerUp));

                //-----------------------------------------------------------
                // Cradle
                //-----------------------------------------------------------
                //Cradle = new MachineController(lowLevelLog, highLevelLog, uiLog);
                Cradle = new MachineController(LowLevelLogX, HighLevelLogX, UiLogX);

                //GPIx164
                ProConsole.WriteLine($"-------------", ConsoleColor.Red);
                ProConsole.WriteLine($"MachineSerial: {Cradle.HighLevel.Configuration.MachineSerial}", ConsoleColor.Red);
                ProConsole.WriteLine($"-------------", ConsoleColor.Red);
                //GPFx164

                //-----------------------------------------------------------
                // DB
                //-----------------------------------------------------------
                #region DB              
                Console.WriteLine($"WorkingStatistics: count={DatabaseWorkings.GetCount()}");
                #endregion

                //-----------------------------------------------------------
                // Web Api
                //-----------------------------------------------------------
                #region Web Api 
                var url = String.Format("http://{0}:{1}", Machine.Constants.Networking.IPAddressHighLevelControl, Machine.Constants.Networking.WebApiPort);
                //var url = $"http://0.0.0.0:{Machine.Constants.Networking.WebApiPort}";
                var cts = new CancellationTokenSource();
                var apiTask = Task.Run(() => Caron.Cradle.Control.Api.WebApiHost.RunAsync(url, cts.Token));

                Api.CradleApiController.SetMachineController(Cradle);
                
                Console.WriteLine($"[WebApi] url: {url}");

                //ProRob.WebApi.Helpers.PrintControllersList(System.Reflection.Assembly.GetExecutingAssembly());
                #endregion

                //-----------------------------------------------------------
                // Exit Signal
                //-----------------------------------------------------------
                #region Exit Signal
                bool exitSignalFired = false;
                ExitSignal.Instance.Exit += (e, a) =>
                {
                    exitSignalFired = true;

                    if (Cradle != null)
                    {
                        Cradle.Application.Stop();
                    }

                    HandlingClosingOperations();
                };
                #endregion

                //-----------------------------------------------------------
                // Tasks
                //-----------------------------------------------------------
                #region Task(DEBUG)
#if DEBUG || TEST
                Task.Run(() =>
                {
                    while (Cradle.Application.IsRunning)
                    {
#pragma warning disable 162
                        //Console.WriteLine($"{Cradle.LowLevelControl.Info.MachineState} {Cradle.LowLevelControl.Info.CutterState}");
#pragma warning restore 162
                        //Console.WriteLine(Cradle.LowLevel.Info.Dancebar.ToString("0.00"));
                        Thread.Sleep(Machine.Constants.Intervals.LongWaitSleep);
                    }
                });
#endif
                #endregion

                //-----------------------------------------------------------
                // Wait Exit
                //-----------------------------------------------------------
                #region Wait Exit
                while (Cradle.Application.IsRunning)
                {
                    Thread.Sleep(Machine.Constants.Intervals.LongWaitSleep);
                }

                Console.WriteLine($"Exited from Cradle Control ({Cradle.Application.Status})...");

                if (exitSignalFired)
                {
                    while (true)
                    {
                        Thread.Sleep(Machine.Constants.Intervals.LongWaitSleep);
                    }
                }
                #endregion

                //-----------------------------------------------------------
                // Handling Closing Operations
                //-----------------------------------------------------------
                HandlingClosingOperations();

                //-----------------------------------------------------------
                // Close / Restart / Shutdown
                //-----------------------------------------------------------
                #region Close/Restart/Shutdown
                if (Cradle.Application.IsRestarting)
                {
                    ProConsole.WriteLine("\n\n\nRestarting..", ConsoleColor.DarkRed);
                    System.Windows.Forms.Application.Restart();
                }
                else if (Cradle.Application.IsShutdowing)
                {
                    ProConsole.WriteLine("\n\n\nShutdowing..", ConsoleColor.DarkRed);
                    ProcessHelper.ExecuteShellCommand("shutdown -s /t 15 /c \"Bye bye..\"");
                }
                else if (Cradle.Application.IsRebooting)
                {
                    ProConsole.WriteLine("\n\n\nRebooting..", ConsoleColor.DarkRed);
                    ProcessHelper.ExecuteShellCommand("shutdown -r /t 15 /c \"Bye bye..\"");
                }
                #endregion

                Console.WriteLine("[END]");
            }
            catch (System.Exception e)
            {
                #region Exceptions Handling
                var exception = Json.Serialize(ProRob.Exceptions.ExceptionDataLog.Compose(e));
                Console.WriteLine($"[IRREVERSIBLE ERROR]\n{exception}");
                File.WriteAllText("last_irreversible_error.txt", exception);
                //highLevelLog.AddLog(exception, LogType.IrreversibleException);
                HighLevelLogX.AddLog(exception, LogType.IrreversibleException);
                MessageBox.Show($"{ApplicationInfo.ApplicationName}", $"IRREVERSIBLE ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
                #endregion
            }

            void HandlingClosingOperations()
            {
                Console.WriteLine("[Handling closing operations] Waiting deinitialization..");

                bool promiseToDeleteSettings = Cradle.HighLevel.Signals.DeleteSettingsAtShutdown;

                CradleWebApi?.Dispose();

                //-----------------------------------------------------------
                // Databases
                //-----------------------------------------------------------
                #region Saving Data
                ProConsole.WriteLine("Saving data..", ConsoleColor.Cyan);

                DatabaseWorkings.Add(new MachineEvent(MachineEventType.MachinePowerDown));

                #region Machine Endurance                
                Cradle.HighLevel.MachineEndurance.WorkingHours.PowerOnHours += Cradle.Uptime.TotalHours;

                if (Cradle.HighLevel.Settings.HighLevel.EnduranceLimits.WorkingHours.WorkingFakeHours.NearlyEquals(0))
                {
                    Cradle.HighLevel.MachineEndurance.WorkingHours.WorkingFakeHours = 0;
                }
                else
                {
                    Cradle.HighLevel.MachineEndurance.WorkingHours.WorkingFakeHours += Cradle.Uptime.TotalHours;
                }

                Cradle.HighLevel.MachineEndurance.Statistics.NumberPowerOff++;
                DatabaseSettings.Update(Cradle.HighLevel.MachineEndurance);
                #endregion

                #region WorkingContext
                DatabaseSettings.Update(Cradle.HighLevel.WorkingContext);
                #endregion

                #region WorkingStatistics
                DatabaseSettings.Update(Cradle.HighLevel.Working);
                #endregion

                #endregion

                DatabaseSettings.Close();
                DatabaseWorkings.Close();

                MachineData.Close();

                //------------------------------------------------------------------------
                // Stopping Web Server
                //------------------------------------------------------------------------      
                #region Stopping Web Server
#if !TEST
                Console.WriteLine("Stopping Web Server..");
                foreach (Process proc in Process.GetProcessesByName("httpd"))
                {
                    proc.Kill();
                }
#endif
                #endregion

                Cradle.Close();
                Cradle.Dispose();

                Console.WriteLine("[Handling closing operations] Cradle deinitialized..");

                udpBackgroundReceiver?.Stop();
                //proLogTextWriter.Close();
                ProLogTextWriterX.Close();
                //highLevelLog.Stop();
                HighLevelLogX.Stop();
                //lowLevelLog.Stop();
                LowLevelLogX.Stop();
                //uiLog.Stop();
                UiLogX.Stop();

                //proLogTextWriter.Dispose();
                ProLogTextWriterX.Dispose();

                #region Delete Settings

                if (promiseToDeleteSettings)
                {
                    Console.WriteLine("Deleting settings..");
                    ProRob.FileSystem.DeleteDirectory(Constants.Path.SettingsFolder);
                    Directory.CreateDirectory(Constants.Path.SettingsFolder);
                }
                #endregion

                Console.Beep();
                Console.WriteLine("[END]");
                Thread.Sleep(2000);
            }

            #region PRESS_A_KEY_TO_EXIT
#if PRESS_A_KEY_TO_EXIT
            Console.WriteLine("Press a key to exit..");
            Console.ReadKey();
#endif
            #endregion
        }
    }
}
