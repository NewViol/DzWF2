using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Questionnaire
{
    public partial class Form1 : Form
    {

        List<User> users = new List<User>();
        bool IsEditing = false;
        User TmpUser = new User();
        public Form1()
        {
            InitializeComponent();
            ReadToFile();
        }

        private void SaveBt_Click(object sender, EventArgs e)
        {
            User DopUser = new User();

            bool IsIncorrect = true;

            if (textBoxIm.Text != null)
            {
                if (textBoxFam.Text != null)
                {
                    if(textBoxMail.Text != null)
                    {
                        bool dop = false;
                        for (int i =0;i < textBoxMail.Text.Length; i++)
                        {
                            if(textBoxMail.Text[i] == '@')
                            {
                                dop = true;
                            }
                        }
                        if(dop == false)
                        {
                            MessageBox.Show("Incorrect input");
                            IsIncorrect = false;
                        }
                        if(dop == true)
                        {
                            if (textBoxTel.Text != null)
                            {
                                bool isletter = false;
                                if (textBoxTel.Text.Any(c => char.IsLetter(c)))
                                {
                                    isletter = true;
                                }
                                if(isletter == true)
                                {
                                    MessageBox.Show("Incorrect input");
                                    IsIncorrect = false;
                                }
                                if(textBoxTel.Text.Length != 10)
                                {
                                    MessageBox.Show("Incorrect input");
                                    IsIncorrect = false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Incorrect input");
                                IsIncorrect = false;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Incorrect input");
                        IsIncorrect = false;
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect input");
                    IsIncorrect = false;
                }
            }
            else
            {
                MessageBox.Show("Incorrect input");
                IsIncorrect = false;
            }

            if (IsIncorrect == true)
            {
                DopUser.Name = textBoxIm.Text;
                DopUser.SurName = textBoxFam.Text;
                DopUser.mail = textBoxMail.Text;
                DopUser.phone = textBoxTel.Text;
                users.Add(DopUser);
                listBox1.Items.Add(DopUser);
                listBox1.ValueMember = "SurName";
                listBox1.DisplayMember = "SurName";
                textBoxIm.Clear();
                textBoxMail.Clear();
                textBoxFam.Clear();
                textBoxTel.Clear();

                SaveToFile();
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            var user = (User)listBox1.SelectedItem;
            textBoxIm.Text = user.Name;
            textBoxFam.Text = user.SurName;
            textBoxMail.Text = user.mail;
            textBoxTel.Text = user.phone;
            IsEditing = true;
            TmpUser.Name = textBoxIm.Text;
            TmpUser.SurName = textBoxFam.Text;
            TmpUser.mail = textBoxMail.Text;
            TmpUser.phone = textBoxTel.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxIm.Clear();
            textBoxMail.Clear();
            textBoxFam.Clear();
            textBoxTel.Clear();
        }
        
        private void SaveToFile()
        {

         

            StreamWriter sw = new StreamWriter("Users.txt", false);


            foreach (var i in users)
            {
                sw.WriteLine(i.Name);
                sw.WriteLine(i.SurName);
                sw.WriteLine(i.mail);
                sw.WriteLine(i.phone);
            }
            sw.Close();
        }
        private void ReadToFile()
        {
            
              FileStream file = new FileStream("Users.txt", FileMode.Open);
              StreamReader rf = new StreamReader(file);
            while (!rf.EndOfStream)
            {
                User DopUser = new User();
                DopUser.Name = rf.ReadLine();
                DopUser.SurName = rf.ReadLine();
                DopUser.mail = rf.ReadLine();
                DopUser.phone = rf.ReadLine();

                users.Add(DopUser);
            }
              rf.Close();
            foreach (var i in users)
            {
                listBox1.Items.Add(i);
                listBox1.ValueMember = "SurName";
                listBox1.DisplayMember = "SurName";                
            }

        }


            private void btEditing_Click(object sender, EventArgs e)
        {
            if ((textBoxIm.Text != null) && (textBoxFam.Text != null) && (textBoxMail.Text != null) && (textBoxTel.Text != null))
            {
                if (IsEditing == true)
                {
                    for(int i=0;i < users.Count; i++)
                    {
                        if ((users[i].Name == TmpUser.Name) &&
                            (users[i].SurName == TmpUser.SurName) &&
                            (users[i].mail == TmpUser.mail) &&
                            (users[i].phone == TmpUser.phone))
                        {
                            users[i].Name = textBoxIm.Text;
                            users[i].SurName = textBoxFam.Text;
                            users[i].mail = textBoxMail.Text;
                            users[i].phone = textBoxTel.Text;
                        }
                    }
                }
            }
            textBoxIm.Clear();
            textBoxMail.Clear();
            textBoxFam.Clear();
            textBoxTel.Clear();
        }
    }
}
