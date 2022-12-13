using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace String_Methods_With_WinForms
{
    public partial class Form1 : Form
    {
        Size SCREENSIZE = Screen.PrimaryScreen.Bounds.Size;
        bool switchWinState = false;
        bool ctrl = false, F = false, Q = false;
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
                Q = true;
            if (e.KeyCode == Keys.ControlKey)
                ctrl = true;
            else if(e.KeyCode == Keys.F)
                F = true;
            //---------------------------------------
            if (ctrl && F)
            {
                ctrl = false;
                F = false;
                if (switchWinState == false)
                {
                    WindowState = FormWindowState.Maximized;
                    switchWinState = true;
                }
                else if (switchWinState == true)
                {
                    WindowState = FormWindowState.Normal;
                    switchWinState = false;
                }
            }
            else if (ctrl && Q)
                Close();
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
                ctrl = false;
            else if (e.KeyCode == Keys.F)
                F = false;
            else if (e.KeyCode == Keys.Q)
                Q = false;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Location = new Point((SCREENSIZE.Width / 2)-(Size.Width / 2),
                (SCREENSIZE.Height / 2)-(Size.Height / 2));
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                richTextBox2.Text = richTextBox1.Text.ToUpper();
            else if (radioButton2.Checked)
                richTextBox2.Text = richTextBox1.Text.ToLower();
            else if (radioButton3.Checked)
                richTextBox2.Text = richTextBox1.Text.Replace(textBox1.Text, textBox2.Text);
            else if (radioButton4.Checked)
                richTextBox2.Text = richTextBox1.Text.Length.ToString();
            else if (radioButton5.Checked)
                richTextBox2.Text = richTextBox1.Text.Insert(Convert.ToInt32(textBox4.Text), textBox3.Text);
            else if (radioButton6.Checked)
                richTextBox2.Text = richTextBox1.Text.Substring(Convert.ToInt32(textBox6.Text), Convert.ToInt32(textBox7.Text));
        }
    }
}
