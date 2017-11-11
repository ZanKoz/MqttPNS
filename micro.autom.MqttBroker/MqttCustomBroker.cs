using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using micro.autom.MqttBroker.Properties;


namespace micro.autom.MqttBroker
{
    public delegate void MqttCustomBrokerEventHandler(object source, MqttCustomBrokerEventArgs e);

    internal class MqttCustomBroker
    {
        public bool IsRunning { get; internal set; }
        public bool NeedsKill { get;  set; }
        public bool IsStarted { get; internal set; }
        public Process BrokerProcess { get; set; }

        public event MqttCustomBrokerEventHandler OnBrokerStarted;
        public event MqttCustomBrokerEventHandler OnBrokerExited;

       
        public MqttCustomBroker()
        {
            IsRunning = false;
            IsStarted = false;
            NeedsKill = false;
        }

        public  void Start()
        {
            if (string.IsNullOrEmpty(Settings.Default.BrokerPath)|| !Directory.Exists(Settings.Default.BrokerPath))
            {
                var folderdDialog = new FolderBrowserDialog
                {
                SelectedPath = Directory.GetCurrentDirectory(),
                Description = Resources.MqttCustomBroker_Start_folder_Dialog_Message
                };
                if (folderdDialog.ShowDialog() == DialogResult.OK)
                {
                    Settings.Default.BrokerPath = folderdDialog.SelectedPath;
                    Settings.Default.Save();
                }
              
            }
           
          var  brokerThread = new Thread(DoRunBroker);
            brokerThread.Start();
        }
       private void DoRunBroker()
        {

           do        
            {

                if (IsRunning)
                {

                    if (NeedsKill)
                    {
                        if (!BrokerProcess.HasExited)
                        {
                            var name = BrokerProcess.ProcessName;
                            var id = BrokerProcess.Id;
                            BrokerProcess.CloseMainWindow();
                            BrokerProcess.Kill();
                            IsRunning = false;
                            OnBrokerExited?.Invoke(this, new MqttCustomBrokerEventArgs(name, id, false));
                        }
                        else
                        {
                            foreach (var process in Process.GetProcessesByName("mosquitto"))
                            {
                                process.Kill();
                                IsRunning = false;
                                OnBrokerExited?.Invoke(this, new MqttCustomBrokerEventArgs(process.ProcessName,process.Id,false));
                            }

                            
                        }         
                    }
                }
                else
                {
                    string pathstr = Settings.Default.BrokerPath;
                    SpinWait.SpinUntil(NotFileinUse);
                    try
                    {
                        BrokerProcess = new Process
                        {
                            StartInfo =
                        {
                            WorkingDirectory = pathstr,
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            FileName = Path.Combine(pathstr,@"mosquitto.exe"),
                            Arguments = "-c mosquitto.conf",

                        },
                            EnableRaisingEvents = true
                        };

                        IsRunning = BrokerProcess.Start();
                    }
                    catch (Exception)
                    {
                        Settings.Default.BrokerPath=null;
                        BrokerProcess = null;
                    }
                    // Redirect the output stream of the child process.
                    // p.StartInfo.Arguments = "/c DIR";
                    if (BrokerProcess!=null)
                    {
                        if (IsRunning && !BrokerProcess.HasExited)
                        {
                            OnBrokerStarted?.Invoke(this, new MqttCustomBrokerEventArgs(BrokerProcess.ProcessName, BrokerProcess.Id, BrokerProcess.Responding));
                        }
                    }
                   
                }
            } while (IsRunning);
        }

         protected virtual bool NotFileinUse()
        {
            String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "service");
            FileInfo file = new FileInfo($@"{path}\passws");
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                return false;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return true;
        }

    }

   

    public class MqttCustomBrokerEventArgs : EventArgs
    {
        public string ProcessName { get; internal set; }
        public int ProcessId { get; internal set; }
        public bool IsProcessResponding { get; internal set; }
        public MqttCustomBrokerEventArgs(string mtextProcessName, int mProcessId,bool mProcessResponding)
        {
            ProcessName = mtextProcessName;
            ProcessId = mProcessId;
            IsProcessResponding = mProcessResponding;
        }
  
    }
}
