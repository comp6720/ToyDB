﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToyDB
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

      


        private void button1_Click(object sender, EventArgs e)
        {
            //define local variables from the user inputs 
            string user = nametxtbox.Text;
            string pass = pwdtxtbox.Text;

            LoginAuth.LoginValidator login = new LoginAuth.LoginValidator(user, pass);
            //check if eligible to be logged in 
            if (login.IsLoggedIn(user, pass))
            {
                MessageBox.Show("You are logged in successfully");
            }
            else
            {
                //show default login error message 
                MessageBox.Show("Login Error!");
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //enter your code for forget password case 
            MessageBox.Show("Under development");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Enter your code for registration form of your choice 
            MessageBox.Show("Under development");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //define local variables from the user inputs 
            string user = nametxtbox.Text;
            string pass = pwdtxtbox.Text;

            LoginAuth.LoginValidator login = new LoginAuth.LoginValidator(user, pass);
            //check if eligible to be logged in 
            if (login.IsLoggedIn(user, pass))
            {
               
                MessageBox.Show("You are logged in successfully");
                TOYODB obj1 = new TOYODB();
                Login obj2 = new Login();
                this.Hide();
                obj1.Show();
                
            }
            else
            {
                //show default login error message 
                MessageBox.Show("Login Error!");
            }

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void pwdtxtbox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
