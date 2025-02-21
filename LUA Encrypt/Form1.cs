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

namespace LUA_Encrypt
{
    public partial class Form1 : Form
    {
        string filePath;

        public Form1()
        {
            InitializeComponent();
            //textBox2.Text = "D:\\source";
            LoadSaveSettings();
        }

        private void LoadSaveSettings ()
        {
            textBox3.Text = Properties.Settings.Default.savePath.ToString();           
        }

        private void OpenFileDialog()
        {
            // Create a new instance of OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set filter options and filter index
            openFileDialog.Filter = "LUA Files (*.lua)|*.lua|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;

            // Set the title of the dialog
            openFileDialog.Title = "Select a File";

            // Show the dialog and get the result
            DialogResult result = openFileDialog.ShowDialog();

            // Check if the user selected a file
            if (result == DialogResult.OK)
            {
                // Get the selected file's path
                filePath = openFileDialog.FileName;
                //Console.WriteLine("Selected file: " + filePath);
                textBox1.Text = filePath.ToString();
                richTextBox1.Text = System.IO.File.ReadAllText(filePath);
            }
            else
            {
                MessageBox.Show("No file selected.");
            }
        }

        public void Obfuscate()
        {
                
            string luaScript = richTextBox1.Text;
            string obfuscatedScript = Convert.ToBase64String(Encoding.UTF8.GetBytes(luaScript));

            //Console.WriteLine("Obfuscated Lua Script:");
            //Console.WriteLine(obfuscatedScript);
            if (!File.Exists(textBox3.Text + "\\" + textBox2.Text))
            {
                System.IO.File.WriteAllText(textBox3.Text + "\\" + textBox2.Text, obfuscatedScript);
                MessageBox.Show("Encryption Successful");
                pictureBox1.BackColor = System.Drawing.Color.Green;
                //Task.Delay(10000);
                //pictureBox1.BackColor = System.Drawing.Color.Red;
            }
            else
            { MessageBox.Show("Already Exists"); }
           
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
           pictureBox1.BackColor= System.Drawing.Color.Red;
           OpenFileDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) { Deobfuscate(); } else {  }
            if (checkBox2.Checked == true) { Obfuscate(); }   else { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog filePath2 = new FolderBrowserDialog();
            if (filePath2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // shows the path to the selected folder in the folder dialog
                MessageBox.Show(filePath2.SelectedPath);
                textBox3.Text = filePath2.SelectedPath.ToString();
                Properties.Settings.Default.savePath = filePath2.SelectedPath.ToString();
                Properties.Settings.Default.Save();

            }
               
        }

        public void Deobfuscate()
        {
            // Encrypted Lua script (Base64 encoded)
            string luaScript = richTextBox1.Text;

            // Decrypt the Lua script
            string decryptedLuaScript = LuaDeobfuscate(luaScript);
                       
            if (!File.Exists(textBox3.Text + "\\" + textBox2.Text))
            {
                System.IO.File.WriteAllText(textBox3.Text + "\\" + textBox2.Text, decryptedLuaScript);
                MessageBox.Show("Decryption Successful");
                pictureBox1.BackColor = System.Drawing.Color.Green;
                //Task.Delay(10000);
                //pictureBox1.BackColor = System.Drawing.Color.Red;
            }
            else
            { MessageBox.Show("Already Exists"); }


        }

        private string LuaDeobfuscate(string encryptedText)
        {
            byte[] base64EncodedBytes = Convert.FromBase64String(encryptedText);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }




}
    
    

