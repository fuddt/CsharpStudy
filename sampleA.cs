using System;
using System.Collections.Generic;
using System.Windows.Forms;

public class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.Text = "設定ファイルから生成されたフォーム";
        this.Size = new System.Drawing.Size(400, 300);
        this.Load += new EventHandler(Form_Load);
    }

    private void Form_Load(object sender, EventArgs e)
    {
        INIParser parser = new INIParser("config.ini");
        var data = parser.GetData();

        int yOffset = 10;

        foreach (var section in data)
        {
            GroupBox groupBox = new GroupBox();
            groupBox.Text = section.Key;
            groupBox.Size = new System.Drawing.Size(360, 100);
            groupBox.Location = new System.Drawing.Point(10, yOffset);

            CheckedListBox checkedListBox = new CheckedListBox();
            checkedListBox.Items.AddRange(section.Value.ToArray());
            checkedListBox.Size = new System.Drawing.Size(340, 60);
            checkedListBox.Location = new System.Drawing.Point(10, 20);

            groupBox.Controls.Add(checkedListBox);
            this.Controls.Add(groupBox);

            yOffset += 110; // 次のグループボックスの位置を調整
        }
    }

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainForm());
    }
}

using System;
using System.Collections.Generic;
using System.IO;

public class INIParser
{
    private Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();

    public INIParser(string filePath)
    {
        Load(filePath);
    }

    private void Load(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        string currentSection = "";

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith(";"))
                continue;

            if (line.StartsWith("[") && line.EndsWith("]"))
            {
                currentSection = line.Trim('[', ']');
                if (!data.ContainsKey(currentSection))
                {
                    data[currentSection] = new List<string>();
                }
            }
            else
            {
                var parts = line.Split('=');
                if (parts.Length == 2 && parts[0].Trim() == "選択肢")
                {
                    data[currentSection].AddRange(parts[1].Split(','));
                }
            }
        }
    }

    public Dictionary<string, List<string>> GetData()
    {
        return data;
    }
}
