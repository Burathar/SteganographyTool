using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteganographyTool;

namespace SteganograpyWinForms
{
    public partial class Form1 : Form
    {
        private Steganographer steganographer;

        public Form1()
        {
            InitializeComponent();
            steganographer = new Steganographer();
            Console.SetOut(new ControlWriter(tbOutput));
            radioEncrypt.Checked = true;
        }

        private void radioEncryptionDecrypyionChanged(object sender, EventArgs e)
        {
            if (radioEncrypt.Checked == true)
            {
                btnSelSourceImg.Enabled = true;
                tbSelSourceImg.Enabled = true;
                btnSelSourceData.Enabled = true;
                tbSelSourceData.Enabled = true;
                btnSelSaveImg.Enabled = true;
                tbSelSaveImg.Enabled = true;
                btnSelSaveData.Enabled = false;
                tbSelSaveData.Enabled = false;
                checkFillRandom.Enabled = true;
            }
            else
            {
                btnSelSourceImg.Enabled = true;
                tbSelSourceImg.Enabled = true;
                btnSelSourceData.Enabled = false;
                tbSelSourceData.Enabled = false;
                btnSelSaveImg.Enabled = false;
                tbSelSaveImg.Enabled = false;
                btnSelSaveData.Enabled = true;
                tbSelSaveData.Enabled = true;
                checkFillRandom.Enabled = false;
            }
        }

        private void btnSelSourceImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Jpeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG Image|*.png|Any File|*.*";
            ofd.FilterIndex = 5;
            ofd.Title = "Choose the source image";

            if (ofd.ShowDialog() == DialogResult.OK && ofd.CheckPathExists)
            {
                tbSelSourceImg.Text = ofd.FileName;
            }
        }

        private void btnSelSourceData_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text file|*.txt|Any File|*.*";
            ofd.FilterIndex = 1;
            ofd.Title = "Choose the source data";

            if (ofd.ShowDialog() == DialogResult.OK && ofd.CheckPathExists)
            {
                tbSelSourceData.Text = ofd.FileName;
            }
        }

        private void btnSelSaveImg_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Jpeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG Image|*.png|Any File|*.*";
            sfd.FilterIndex = 4;
            sfd.Title = "Choose the source data";

            if (sfd.ShowDialog() == DialogResult.OK && sfd.CheckPathExists)
            {
                tbSelSaveImg.Text = sfd.FileName;
            }
        }

        private void btnSelSaveData_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text file|*.txt|Any File|*.*";
            sfd.FilterIndex = 1;
            sfd.Title = "Choose the source data";

            if (sfd.ShowDialog() == DialogResult.OK && sfd.CheckPathExists)
            {
                tbSelSaveData.Text = sfd.FileName;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            tbOutput.Clear();
            if (radioEncrypt.Checked == true)
            {
                Encrypt();
            }
            else
            {
                Decrypt();
            }
        }

        private void Encrypt()
        {
            if (!File.Exists(tbSelSourceImg.Text))
            {
                MessageBox.Show($"\"{tbSelSourceImg.Text}\" Does not exist!");
                return;
            }
            if (!File.Exists(tbSelSourceData.Text))
            {
                MessageBox.Show($"\"{tbSelSourceData.Text}\" Does not exist!");
                return;
            }
            if (!Directory.Exists(Path.GetDirectoryName(tbSelSaveImg.Text)))
            {
                MessageBox.Show($"The directory \"{tbSelSaveImg.Text}\" Does not exist!");
                return;
            }
            steganographer.NewSteganograph(tbSelSourceImg.Text, tbSelSourceData.Text, tbSelSaveImg.Text, checkGrayscale.Checked, checkFillRandom.Checked, checkAscii.Checked ? EncodingType.ASCII : EncodingType.UTF8);
        }

        private void Decrypt()
        {
            if (!File.Exists(tbSelSourceImg.Text))
            {
                MessageBox.Show($"\"{tbSelSourceImg.Text}\" Does not exist!");
                return;
            }
            if (!Directory.Exists(Path.GetDirectoryName(tbSelSaveData.Text)))
            {
                MessageBox.Show($"The directory \"{tbSelSaveData.Text}\" Does not exist!");
                return;
            }
            steganographer.DecryptSteganograph(tbSelSourceImg.Text, tbSelSaveData.Text, checkGrayscale.Checked, checkAscii.Checked ? EncodingType.ASCII : EncodingType.UTF8);
        }
    }
}