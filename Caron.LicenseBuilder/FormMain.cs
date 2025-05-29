#undef ENABLE_DRAG

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using ProRob;

using Machine.Security;

namespace Caron.LicenseBuilder
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
#if ENABLE_DRAG
            // Enable drag - and - drop operations and
            // add handlers for DragEnter and DragDrop.
            this.AllowDrop = true;
            this.DragDrop += new DragEventHandler(this.FormMain_DragDrop);
            this.DragEnter += new DragEventHandler(this.FormMain_DragEnter);
#endif
        }

        private void BtnGenerateLicense_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxSerial.Text))
                {
                    MessageBox.Show("Warning", "Machine serial is missing", MessageBoxButtons.OK);
                    return;
                }

                if (cbMachineType.SelectedIndex < 0)
                {
                    MessageBox.Show("Warning", "Machine type is not selected", MessageBoxButtons.OK);
                    return;
                }

                var machineType = cbMachineType.SelectedItem as string;
                var machineSerial = textBoxSerial.Text;
                var licenseType = (int)numericUpDown.Value;

                string licenseHash = Machine.License.CreateLicenseHash(machineSerial);

                var license = new Machine.License()
                {
                    MachineType = machineType,
                    MachineSerial = machineSerial,
                    LicenseType = licenseType,
                    LicenseHash = licenseHash
                };

                string json = Json.Serialize(license);

                byte[] bufferEncrypted = Encryption.Encrypt(Encoding.UTF8.GetBytes(json), licenseHash);

                string jsonDecrypted = Encoding.UTF8.GetString(Encryption.Decrypt(bufferEncrypted, licenseHash));

                //if (string.Equals(json,jsonDecrypted))
                //{
                //    MessageBox.Show("Program failed to create license file");
                //    return;
                //}

                var saveFileDialog = new SaveFileDialog()
                {
                    Title = "License",
                    FileName = "caron_license.lic",
                };

                Stream stream;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if ((stream = saveFileDialog.OpenFile()) != null)
                    {
                        stream.Write(bufferEncrypted, 0, bufferEncrypted.Length);
                        stream.Close();

                        MessageBox.Show("License created");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Program failed to create license file");
            }
        }

        #region Drag & Drop
        private void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            // Handle FileDrop data.
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Assign the file names to a string array, in 
                // case the user has selected multiple files.
                string[] filesDrop = (string[])e.Data.GetData(DataFormats.FileDrop);
            }
        }

        private void FormMain_DragEnter(object sender, DragEventArgs e)
        {
            // If the data is a file display the copy cursor.
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void FormMain_DragLeave(object sender, EventArgs e)
        {
            //--
        }

        private void FormMain_DragOver(object sender, DragEventArgs e)
        {
            //--
        }
        #endregion
    }
}
