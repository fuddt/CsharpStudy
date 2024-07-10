using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // コンボボックスとラジオボタンの初期設定
            comboBox1.Items.AddRange(new string[] { "Option 1", "Option 2", "Option 3" });
            radioButton1.Text = "Option A";
            radioButton2.Text = "Option B";
            radioButton3.Text = "Option C";
            comboBox1.Visible = false;
            groupBox1.Visible = false;
        }

        private void showOptionsButton_Click(object sender, EventArgs e)
        {
            using (Form2 optionsForm = new Form2())
            {
                if (optionsForm.ShowDialog() == DialogResult.OK)
                {
                    string selectedComboBoxItem = optionsForm.SelectedComboBoxItem;
                    List<string> selectedCheckBoxItems = optionsForm.SelectedCheckBoxItems;

                    // 選択された項目を利用して処理を行う（例：PowerPointに出力）
                    string result = "ComboBox: " + selectedComboBoxItem + "\nCheckBoxes: " + string.Join(", ", selectedCheckBoxItems);
                    CreatePowerPoint(result);
                }
            }
        }


        private void submitButton_Click(object sender, EventArgs e)
        {
            string selectedText = "";

            // コンボボックスの選択項目を取得
            if (comboBox1.SelectedItem != null)
            {
                selectedText = comboBox1.SelectedItem.ToString();
            }

            // ラジオボタンの選択項目を取得
            if (radioButton1.Checked)
            {
                selectedText = radioButton1.Text;
            }
            else if (radioButton2.Checked)
            {
                selectedText = radioButton2.Text;
            }
            else if (radioButton3.Checked)
            {
                selectedText = radioButton3.Text;
            }

            if (!string.IsNullOrEmpty(selectedText))
            {
                // PowerPointに出力
                CreatePowerPoint(selectedText);
            }
        }

        private void CreatePowerPoint(string text)
        {
            PowerPoint.Application pptApp = new PowerPoint.Application();
            Presentation pptPresentation = pptApp.Presentations.Add(MsoTriState.msoTrue);
            Slides slides = pptPresentation.Slides;
            Slide slide = slides.Add(1, PpSlideLayout.ppLayoutText);
            TextRange objText = slide.Shapes[1].TextFrame.TextRange;
            objText.Text = text;

            pptApp.Visible = MsoTriState.msoTrue;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // コンボボックスの選択項目が変更されたときの処理（ここでは特に何もする必要はありません）
        }
    }
}
