using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;

namespace RestoreMySQL
{
    public partial class RestoreMySQL : Form
    {
        FileOperator fOper;

        public RestoreMySQL()
        {
            InitializeComponent();
        }

        private void btnRestoreOriginalSetting_Click(object sender, EventArgs e)
        {
            ExecuteOriginalBatchFile();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            //string[] argumentsToBatchFile = { "root", "123456", "educatemanagement", "backupfile.sql", "localhost" };
            //ExecuteBatchFile("MySql_Restore.bat", argumentsToBatchFile);
            //ExecuteRestoreViaStreamReader();
            ExecuteBatchFile("MySql_Restore.bat");
        }

        /// <summary>
        ///  Restore Via Solid Batch Restore File
        /// </summary>
        private void ExecuteBatchFile(string fileName)
        {
            DialogResult result = MessageBox.Show("是否確定還原?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process processBackup = new System.Diagnostics.Process();
                    processBackup.EnableRaisingEvents = false;
                    processBackup.StartInfo.FileName = fileName;
                    processBackup.Start();
                    processBackup.WaitForExit();
                }
                catch
                {
                }
            }
        }

        private void ExecuteOriginalBatchFile()
        {
            DialogResult result = MessageBox.Show("是否確定還原?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                DialogResult resultConfirm = MessageBox.Show("所有資料將會被刪除\r\n無法救援!! 是否確定還原?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultConfirm == DialogResult.Yes)
                {
                    try
                    {
                        System.Diagnostics.Process processBackup = new System.Diagnostics.Process();
                        processBackup.EnableRaisingEvents = false;
                        processBackup.StartInfo.FileName = "MySql_OriginalRestore.bat";
                        processBackup.Start();
                        processBackup.WaitForExit();
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void btnRestoreLastTimeBackupByUSB_Click(object sender, EventArgs e)
        {
            fOper = new FileOperator();
            string filePath = "EMSSystem\\Backup\\";
            bool hasFound = false;

            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    if (!drive.Name.Contains("C"))
                    {
                        if (Directory.Exists(drive.Name + filePath))
                        {
                            if (fOper.CheckFileExist(drive.Name + filePath + "usbbackupfile.sql"))
                            {
                                fOper.DeleteFile("usbbackupfile.sql");
                                File.Copy(drive.Name + filePath + "usbbackupfile.sql", "usbbackupfile.sql");
                                ExecuteBatchFile("MySql_USBRestore.BAT");
                                hasFound = true;
                            }
                        }
                    }
                }
            }

            if (!hasFound)
                MessageBox.Show("指定之外接硬碟不存在!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        ///  Restore Via Variable Batch Restore File
        /// </summary>
        /// <param name="batchFileName"></param>
        /// <param name="argumentsToBatchFile"></param>
        /// <returns></returns>
        protected bool ExecuteBatchFile(string batchFileName, string[] argumentsToBatchFile)
        {
            string argumentsString = string.Empty;
            try
            {
                //Add up all arguments as string with space separator between the arguments
                if (argumentsToBatchFile != null)
                {
                    for (int count = 0; count < argumentsToBatchFile.Length; count++)
                    {
                        argumentsString += " ";
                        argumentsString += argumentsToBatchFile[count];
                        //argumentsString += "\"";
                    }
                }

                //Create process start information
                System.Diagnostics.ProcessStartInfo DBProcessStartInfo = new System.Diagnostics.ProcessStartInfo(batchFileName, argumentsString);

                //Redirect the output to standard window
                DBProcessStartInfo.RedirectStandardOutput = true;

                //The output display window need not be falshed onto the front.
                DBProcessStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                DBProcessStartInfo.UseShellExecute = false;

                //Create the process and run it
                System.Diagnostics.Process dbProcess;
                dbProcess = System.Diagnostics.Process.Start(DBProcessStartInfo);

                //Catch the output text from the console so that if error happens, the output text can be logged.
                System.IO.StreamReader standardOutput = dbProcess.StandardOutput;

                /* Wait as long as the DB Backup or Restore or Repair is going on. 
                Ping once in every 2 seconds to check whether process is completed. */
                while (!dbProcess.HasExited)
                    dbProcess.WaitForExit(2000);

                if (dbProcess.HasExited)
                {
                    string consoleOutputText = standardOutput.ReadToEnd();
                    //TODO - log consoleOutputText to the log file.

                }

                return true;
            }
            // Catch the SQL exception and throw the customized exception made out of that
            catch (SqlException ex)
            {
                return false;
                //ExceptionManager.Publish(ex);
                //throw SQLExceptionClassHelper.GetCustomMsSqlException(ex.Number);
            }
            // Catch all general exceptions
            catch (Exception ex)
            {
                return false;
                //ExceptionManager.Publish(ex);
                //throw new CustomizedException(ARCExceptionManager.ErrorCodeConstants.generalError, ex.Message);
            }
        }

        /// <summary>
        ///  Restore Via SteamReader
        /// </summary>
        StreamReader inputStream;
        private void ExecuteRestoreViaStreamReader()
        {
            //string dbname = "--routines educatemanagement"; //dummy-name of the database --routines Even backups the stored procedures
            string user = "root";
            string passwd = "123456";
            string location = "localhost";

            string filePath = Application.StartupPath + "\\backupfile.sql";
            inputStream = new StreamReader(filePath);

            try
            {
                System.Diagnostics.Process.Start(@"cmd.exe",
                "/c \"" + Application.StartupPath + "\\mysql.exe\" -u " + user + " -p" + passwd + " -h" + location + " < " + filePath);

                //string mysqldumpstring = string.Format("--user={0} --password={1}", user, passwd);
                //ProcessStartInfo info = new ProcessStartInfo("mysqlimport");
                //info.Arguments = mysqldumpstring;
                //info.UseShellExecute = false;
                //info.CreateNoWindow = true;
                //info.RedirectStandardError = true;
                //info.RedirectStandardOutput = true;

                //Process processRestore = new Process();
                //processRestore.StartInfo = info;
                //processRestore.Start();
                //processRestore.WaitForExit();
            }
            catch { }
            finally
            {
                inputStream.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
