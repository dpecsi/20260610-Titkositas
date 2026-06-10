using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace _20260610_Titkositas.Windows
{
    /// <summary>
    /// Interaction logic for FileOpenForDecryptionWindow.xaml
    /// </summary>
    public partial class FileOpenForDecryptionWindow : Window
    {
        public string Password { get; set; }
        public FileOpenForDecryptionWindow()
        {
            InitializeComponent();
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Titkosított szöveges fájl (*.txt.aes)|*.aes|Minden fájl (*.*)|*.*";
            openFileDialog.Title = "Válaszd ki a fájlt amit de-kódolni akarsz";
            if (openFileDialog.ShowDialog() == true)
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
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (FileStream originalTxt = new FileStream(FilePathTextBox.Text, FileMode.Open))
                using (FileStream decryptedTxt = new FileStream(FilePathTextBox.Text.Substring(0, FilePathTextBox.Text.Length - 4), FileMode.Create))
                using (CryptoStream cryptoStream = new CryptoStream(decryptedTxt, decryptor, CryptoStreamMode.Write))
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
                MessageBox.Show("Hiba a visszafejtés során: " + ex.Message);
                return;
            }
            MessageBox.Show("Sikeres visszafejtés!");
        }
    }
}
