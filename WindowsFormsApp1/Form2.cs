using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form2: Form
    {
        public string SelectedComboBoxItem { get; private set; }
        public List<string> SelectedCheckBoxItems { get; private set; }

        public Form2()
        {
            InitializeComponent();
            SelectedCheckBoxItems = new List<string>();
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(new string[] { "Option 1", "Option 2", "Option 3" });
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                SelectedComboBoxItem = comboBox1.SelectedItem.ToString();
            }

            if (checkBox1.Checked)
            {
                SelectedCheckBoxItems.Add(checkBox1.Text);
            }
            if (checkBox2.Checked)
            {
                SelectedCheckBoxItems.Add(checkBox2.Text);
            }
            if (checkBox3.Checked)
            {
                SelectedCheckBoxItems.Add(checkBox3.Text);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
