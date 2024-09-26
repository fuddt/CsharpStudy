import streamlit as st
import pandas as pd
import matplotlib.pyplot as plt
import numpy as np

# サンプルデータを作成
data = {
    '参加者': ['Aさん', 'Bさん', 'Cさん', 'Dさん'],
    '国語': [80, 70, 90, 60],
    '英語': [75, 85, 65, 95]
}

df = pd.DataFrame(data)

# グラフを作成
fig, ax = plt.subplots(figsize=(8, 6))

# バーの幅と位置を設定
x = np.arange(len(df['参加者']))  # 参加者のインデックス
width = 0.35  # バーの幅

# 国語の点数の棒グラフを作成
ax.bar(x - width/2, df['国語'], width, label='国語', color='skyblue')

# 英語の点数の棒グラフを作成
ax.bar(x + width/2, df['英語'], width, label='英語', color='lightgreen')

# X軸のラベルを参加者名に設定
ax.set_xticks(x)
ax.set_xticklabels(df['参加者'])

# ラベルとタイトルを設定
ax.set_xlabel('参加者')
ax.set_ylabel('点数')
ax.set_title('参加者ごとの国語と英語の点数')
ax.legend()

# グラフを表示
st.pyplot(fig)