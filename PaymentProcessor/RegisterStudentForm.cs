﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaymentProcessor
{
    public partial class RegisterStudentForm : Form
    {
        public RegisterStudentForm()
        {
            InitializeComponent();
        }

        private void maskedTextBoxCPF_TextChanged(object sender, EventArgs e)
        {
            //maskedTextBoxCPF.Text = maskedTextBoxCPF.Text.PadLeft(11, '0');
            //TODO mascarar CPF
        }

        private void buttonCartao_Click(object sender, EventArgs e)
        {
            FormCard formCard = new FormCard(this);
            this.Hide();
            formCard.ShowDialog();
            this.Show();
        }

        public string labelSwipedCardText
        {
            set
            {
                labelSwipedCard.Text = value;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Student student;
            string errorMsg = "";

            if (textBoxNome.Text == "")
                errorMsg = "Nome vazio";
            else if (textBoxSobrenome.Text == "")
                errorMsg = "Sobrenome vazio";

            if (errorMsg == "")
            {
                try
                {
                    //TODO passar cartão
                    student = new Student(textBoxNome.Text, textBoxSobrenome.Text, maskedTextBoxCPF.Text, dateTimePickerNascimento.Value, "");
                }
                catch (InvalidCPFException)
                {
                    errorMsg = "CPF inválido";
                }

                if ((errorMsg == "") && (!labelSwipedCard.Visible))
                    errorMsg = "Cadastre o cartão";
            }

            if (errorMsg != "") //at least one field with error
            {
                SystemSounds.Beep.Play();
                MessageBox.Show(errorMsg);
                return;
            }

            //TODO salvar student no db
            this.Close();
        }

        private void RegisterStudentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Esta ação cancelará o cadastro. Confirma?", "Fechar cadastro", MessageBoxButtons.YesNo) != DialogResult.Yes)
                e.Cancel = true;
        }
    }
}
