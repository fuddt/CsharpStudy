import re

def clean_ocr_text(text: str) -> str:
    """
    OCR後のテキストからノイズを除去する関数。

    - 長い「ー」などの線を削除
    - 無意味な連続記号を削除
    - 制御文字の除去
    - 空白の正規化など
    """
    # 1. 「ーーーー」などの連続線を削除（2つ以上の「ー」）
    text = re.sub(r'ー{2,}', '', text)
    
    # 2. 「＝＝＝」「・・・・」などの無意味な連続記号を削除
    text = re.sub(r'[＝・\.]{2,}', '', text)

    # 3. 制御文字（タブ・改行・キャリッジリターンなど）をスペースに置換
    text = re.sub(r'[\r\n\t]', ' ', text)

    # 4. 空白の連続を1つに統一
    text = re.sub(r'\s{2,}', ' ', text)

    # 5. 前後の空白を除去
    text = text.strip()

    return text