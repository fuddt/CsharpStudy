using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class OptionsForm : Form
    {
        public List<string> SelectedCheckedListBox1Items { get; private set; }
        public List<string> SelectedCheckedListBox2Items { get; private set; }

        public OptionsForm()
        {
            InitializeComponent();
            SelectedCheckedListBox1Items = new List<string>();
            SelectedCheckedListBox2Items = new List<string>();
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            checkedListBox1.Items.AddRange(new string[] { "Option 1", "Option 2", "Option 3" });
            checkedListBox2.Items.AddRange(new string[] { "Option A", "Option B", "Option C" });
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            foreach (object item in checkedListBox1.CheckedItems)
            {
                SelectedCheckedListBox1Items.Add(item.ToString());
            }

            foreach (object item in checkedListBox2.CheckedItems)
            {
                SelectedCheckedListBox2Items.Add(item.ToString());
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}