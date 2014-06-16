using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SetPassword
{
    public partial class frmSetPassword : Form
    {
        FileOperator fOper;

        public frmSetPassword()
        {
            InitializeComponent();
        }

        private void btnSelectPlace_Click(object sender, EventArgs e)
        {
            fbdBrowserFolder = new FolderBrowserDialog();
            fbdBrowserFolder.ShowDialog();

            lblPasswordPlace.Text = fbdBrowserFolder.SelectedPath;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMakingPassword_Click(object sender, EventArgs e)
        {
            if (lblPasswordPlace.Text.Trim() != "")
            {
                if (CheckDrive(lblPasswordPlace.Text.Trim()))
                {
                    string pathOfUSBDrive = lblPasswordPlace.Text + "EMSSystem\\Password";
                    string pathOfCDrive = "C:\\WINDOWS\\system";
                    string pathOfApplication = Application.StartupPath;

                    string fileNameInDrives = "\\USBPass.ems";
                    string fileNameInApplication = "\\PasswordMatch.txt";

                    fOper = new FileOperator();
                    fOper.CreateDirectory(pathOfUSBDrive);

                    Guid newPassword = Guid.NewGuid();

                    string[] passwordSets = newPassword.ToString().Split('-');

                    fOper.CreateFiles(pathOfUSBDrive + fileNameInDrives);
                    fOper.CreateFiles(pathOfCDrive + fileNameInDrives);
                    fOper.CreateFiles(pathOfApplication + fileNameInApplication);

                    fOper.WriteData(pathOfUSBDrive + fileNameInDrives, passwordSets[0] + "-" + passwordSets[1] + "-" + passwordSets[2] + "-");
                    fOper.WriteData(pathOfCDrive + fileNameInDrives, passwordSets[0] + "-" + passwordSets[1] + "-" + passwordSets[2] + "-");
                    fOper.WriteData(pathOfApplication + fileNameInApplication, passwordSets[3] + "-" + passwordSets[4]);
                }
                else
                    MessageBox.Show("Please select a right path for password!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Please select a path for password!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnMakingSystemPassword_Click(object sender, EventArgs e)
        {
            string pathOfEMSSystemOne = "C:\\Documents and Settings\\All Users";
            string pathOfEMSSystemTwo = "C:\\Program Files\\EMSSystem";
            string pathOfEMSSystemThree = Application.StartupPath;
            string pathOfPasswordOrder = "C:\\WINDOWS\\system";

            string fileNameInDrives = "\\SystemPass.ems";
            string fileNameInApplication = "\\PasswordCheck.txt";
            string fileNameOfPasswordOrder = "\\PasswordOrder.ems";

            Guid newPassword = Guid.NewGuid();

            string[] passwordSets = newPassword.ToString().Split('-');

            int[] randomSet = new int[5];
            bool fillAll = false, coubeBeZero = true;
            int i = 0;
            while (!fillAll)
            {
                Random randomNum = new Random();
                int number = randomNum.Next(5);

                //Check Normal Number
                if (!randomSet.Contains(number))
                {
                    randomSet[i] = number;
                    i++;
                }

                //Check 0
                if (coubeBeZero && number == 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (randomSet[j] == number)
                            coubeBeZero = false;
                    }

                    if (coubeBeZero)
                    {
                        randomSet[i] = number;
                        i++;
                        coubeBeZero = false;
                    }
                }

                //Check Last Number
                if (i == 4)
                {
                    string notReadyNum = "01234";
                    for (int j = 0; j < i; j++)
                        notReadyNum = notReadyNum.Replace(randomSet[j].ToString(), "");

                    randomSet[i] = int.Parse(notReadyNum);
                    i++;
                }

                if (i > 4)
                    fillAll = true;
            }

            string[] newPasswordSet = new string[5];
            for (int k = 0; k < randomSet.Length; k++)
                newPasswordSet[k] = passwordSets[randomSet[k]];


            fOper = new FileOperator();
            fOper.CreateDirectory(pathOfEMSSystemOne);
            fOper.CreateDirectory(pathOfEMSSystemTwo);

            fOper.CreateFiles(pathOfEMSSystemOne + fileNameInDrives);
            fOper.CreateFiles(pathOfEMSSystemTwo + fileNameInDrives);
            fOper.CreateFiles(pathOfEMSSystemThree + fileNameInApplication);

            fOper.WriteData(pathOfEMSSystemOne + fileNameInDrives, newPasswordSet[0] + "," + newPasswordSet[1]);
            fOper.WriteData(pathOfEMSSystemTwo + fileNameInDrives, newPasswordSet[2] + "," + newPasswordSet[3] );
            fOper.WriteData(pathOfEMSSystemThree + fileNameInApplication, newPasswordSet[4]);

            fOper.WriteData(pathOfPasswordOrder + fileNameOfPasswordOrder, randomSet[0].ToString() + "," + randomSet[1].ToString() + "," + randomSet[2].ToString()
                                                                                                                 + "," + randomSet[3].ToString() + "," + randomSet[4].ToString());
        }

        private bool CheckDrive(string path)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    if (drive.Name == path)
                        return true;
                }
            }

            return false;
        }
    }
}
