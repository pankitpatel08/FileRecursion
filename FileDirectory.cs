using RecursionSolver;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FileRecursion
{
    public partial class FileDirectory : Form
    {
        OrbitoryOperation objOrbOpe = new OrbitoryOperation();

        #region Constructor
        public FileDirectory()
        {
            InitializeComponent();
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Find File from folders recursion function
        /// </summary>
        /// <param name="path"></param>
        private void FindFiles(string path)
        {
            var files = Directory.GetFiles(path).Where(s => s.EndsWith(".doc") || s.EndsWith(".docx") || s.EndsWith(".txt"));
            foreach (string fileName in files)
            {
                lstFiles.Items.Add(fileName);
            }
            foreach (string directory in Directory.GetDirectories(path))
            {
                FindFiles(directory);
            }
        }

        /// <summary>
        /// Get Selected file from ListBox
        /// </summary>
        /// <returns></returns>
        private string GetSelectedFile()
        {
            return lstFiles.Items[lstFiles.SelectedIndex].ToString();
        }

        /// <summary>
        /// Reload file after operation
        /// </summary>
        private void ReloadFiles()
        {
            lstFiles.Items.Clear();
            FindFiles(txtFilePath.Text);
        }
        #endregion

        #region Private form Events
        /// <summary>
        /// Browse Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            using (var fldrDlg = new FolderBrowserDialog())
            {
                if (fldrDlg.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = fldrDlg.SelectedPath;
                    lstFiles.Items.Clear();
                    FindFiles(txtFilePath.Text);
                }
            }
        }

        /// <summary>
        /// Button Rename Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRename_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedItem = GetSelectedFile();
                objOrbOpe.RenameFile(selectedItem);
                MessageBox.Show("File renamed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReloadFiles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Rename File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Button Delete Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            string selectedItem = GetSelectedFile();
            objOrbOpe.DeleteFile(selectedItem);
            MessageBox.Show("File deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ReloadFiles();
        }

        /// <summary>
        /// Button Modify Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModify_Click(object sender, EventArgs e)
        {
            string selectedItem = GetSelectedFile();
            objOrbOpe.ModifyFile(selectedItem);
            MessageBox.Show("File modified!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ReloadFiles();
        }

        /// <summary>
        /// Button Copy Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCopy_Click(object sender, EventArgs e)
        {
            string selectedItem = GetSelectedFile();
            objOrbOpe.CloneFile(selectedItem);
            MessageBox.Show("File copied!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ReloadFiles();
        }

        /// <summary>
        /// Application Exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion
    }
}
