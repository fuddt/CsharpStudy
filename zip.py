import os
import zipfile

def create_zip_with_subdirectories(zip_name, directories):
    """
    指定されたディレクトリを `data/` 配下に格納したZIPファイルを作成。

    Parameters:
        zip_name (str): 作成するZIPファイル名
        directories (list): 圧縮対象のディレクトリリスト
    """
    with zipfile.ZipFile(zip_name, 'w', zipfile.ZIP_DEFLATED) as zipf:
        for directory in directories:
            if os.path.isdir(directory):
                for root, dirs, files in os.walk(directory):
                    # ZIP内のパスを作成 (data/ 以下に配置)
                    relative_path = os.path.relpath(root, os.path.dirname(directory))
                    zip_path = os.path.join("data", relative_path)
                    # ファイルを圧縮
                    for file in files:
                        file_path = os.path.join(root, file)
                        arcname = os.path.join(zip_path, file)
                        zipf.write(file_path, arcname)
            else:
                print(f"Warning: '{directory}' はディレクトリではありません。スキップします。")

# 使用例
directories_to_zip = ['A', 'B']  # 圧縮したいディレクトリ
zip_file_name = 'data.zip'      # 作成するZIPファイル名

create_zip_with_subdirectories(zip_file_name, directories_to_zip)

print(f"{zip_file_name} が作成されました！")