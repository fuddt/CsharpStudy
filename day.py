import matplotlib.pyplot as plt
import pandas as pd
import numpy as np
from matplotlib.animation import FuncAnimation

# データフレームを作成
data = {'x': np.random.rand(50)}  # ランダムなデータを50個
df = pd.DataFrame(data)

# アニメーション用の設定
fig, ax = plt.subplots()
sc = ax.scatter([], [], s=50, c='blue')
ax.set_xlim(0, 1)
ax.set_ylim(0, 1)
ax.set_title("Animation Example")
ax.set_xlabel("X")
ax.set_ylabel("Y")

# 初期化関数
def init():
    sc.set_offsets([])
    return sc,

# 更新関数
def update(frame):
    x = df.iloc[:frame]['x'].values  # 現在のフレームまでのデータ
    y = np.random.rand(frame)  # ランダムなy座標を生成
    points = np.column_stack((x, y))
    sc.set_offsets(points)
    return sc,

# アニメーションの設定
ani = FuncAnimation(fig, update, frames=len(df) + 1, init_func=init, blit=True, interval=100)

plt.show()