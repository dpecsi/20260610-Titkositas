using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Security.Cryptography;

namespace _20260610_Titkositas
{
    /// <summary>
    /// Interaction logic for FileOpenForCryptionWindow.xaml
    /// </summary>
    public partial class FileOpenForCryptionWindow : Window
    {
        public string Password { get; set; }
        public FileOpenForCryptionWindow()
        {
            InitializeComponent();
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Szöveges fájl (*.txt)|*.txt|Minden fájl (*.*)|*.*";
            openFileDialog.Title = "Válaszd ki a fájlt amit kódolni akarsz";
            if(openFileDialog.ShowDialog() == true)
            {
                FilePathTextBox.Text = Path.GetFullPath(openFileDialog.FileName);
            }
        }

        private void CyptionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Aes aes = Aes.Create();
                byte[] pass = Encoding.UTF8.GetBytes(this.Password);
                aes.SetKey(SHA256.HashData(pass));
                aes.IV = MD5.HashData(pass);
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (FileStream originalTxt = new FileStream(FilePathTextBox.Text, FileMode.Open))
                using (FileStream encryptedTxt = new FileStream(FilePathTextBox.Text + ".aes", FileMode.Create))
                using (CryptoStream cryptoStream = new CryptoStream(encryptedTxt, encryptor, CryptoStreamMode.Write))
                {
                    int data;
                    while ((data = originalTxt.ReadByte()) != -1)
                    {
                        cryptoStream.WriteByte((byte)data);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba a titkosítás során: "+ex.Message);
                return;
            }
            MessageBox.Show("Sikeres titkosítás!");
        }
    }
}
