using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        // 設定ファイルのパスを指定
        string filePath = "rules.txt";

        // 設定ファイルからルール番号とファイルパスを格納する辞書を作成
        Dictionary<string, string> rules = new Dictionary<string, string>();

        // ファイルを1行ずつ読み込み
        foreach (string line in File.ReadLines(filePath))
        {
            // 行をキーと値に分割（"="を区切りとして使用）
            string[] parts = line.Split('=');

            if (parts.Length == 2)
            {
                // キーと値をトリムして、余計な空白を削除
                string key = parts[0].Trim();
                string value = parts[1].Trim().Trim('"'); // 値から二重引用符を削除

                // 辞書にキーと値を追加
                rules[key] = value;
            }
        }

        // ルール番号とファイルパスを表示
        foreach (var rule in rules)
        {
            Console.WriteLine($"ルール番号: {rule.Key}, ファイルパス: {rule.Value}");
        }
    }
}