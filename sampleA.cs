import numpy as np
import matplotlib.pyplot as plt

# ----------------------
# 定数
# ----------------------
focal_length = 8       # 焦点距離 (mm)
pixel_pitch = 0.005    # 画素ピッチ (mm/pixel)
frame_time = 0.048     # フレーム時間 (s)
speed_m_per_s = 30 * 1000 / 3600  # 時速30km → 秒速8.33m/s

# カメラの初期位置 (x, y, z)
camera_position = np.array([0.0, 1.5, 5.0])  # 高さ y=1.5m, 初期位置 (0, 5)

# カメラの向き (ヨー方向)
camera_yaw = 0.0  # 初期カメラのヨー角度 (rad)

# ヨーレート (rad/s)
yaw_rates = [-0.0045, -0.0048, -0.0060]

# 世界座標系の点
points_world = [
    [0, 0, 800],   # 1フレーム目
    [0, 0, 900],   # 2フレーム目
    [10, 0, 1000]  # 3フレーム目
]

# ----------------------
# カメラ座標系に変換
# ----------------------
camera_coords = []

for i, point in enumerate(points_world[1:], start=1):  # 2F, 3Fの点を処理
    # カメラ位置を更新
    dz = speed_m_per_s * frame_time  # カメラが進むZ方向の距離
    camera_position[2] += dz  # Z座標を更新

    # ヨー角度を更新
    camera_yaw += yaw_rates[i-1] * frame_time  # ヨー角の累積適用

    # 2D回転行列を計算 (X-Z平面の回転のみ)
    cos_yaw = np.cos(camera_yaw)
    sin_yaw = np.sin(camera_yaw)

    # 点をカメラ座標系に変換
    dx = point[0] - camera_position[0]
    dz = point[2] - camera_position[2]
    X_c = dx * cos_yaw - dz * sin_yaw
    Z_c = dx * sin_yaw + dz * cos_yaw
    Y_c = point[1] - camera_position[1]  # 高さ方向は単純な差分

    camera_coords.append([X_c, Y_c, Z_c])

# ----------------------
# カメラ座標系を2D画像座標に変換
# ----------------------
image_coords = []
for coord in camera_coords:
    X_c, Y_c, Z_c = coord
    # カメラの背後の点はスキップ (Z_c <= 0)
    if Z_c <= 0:
        continue
    u = (focal_length * X_c) / (Z_c * pixel_pitch)
    v = (focal_length * Y_c) / (Z_c * pixel_pitch)
    image_coords.append([u, v])

# ----------------------
# 描画
# ----------------------
# 1フレーム目の画像サイズ (仮に1980x1080とする)
image_width = 1980
image_height = 1080

# 画像上の点を描画
fig, ax = plt.subplots(figsize=(10, 5))
ax.set_xlim(0, image_width)
ax.set_ylim(0, image_height)

# フレーム1 (画像中央)
base_u, base_v = image_width // 2, image_height // 2
ax.scatter(base_u, base_v, color='red', label='Frame 1 (Base)')

# フレーム2, 3の点を描画
for i, (u, v) in enumerate(image_coords):
    ax.scatter(base_u + u, base_v - v, label=f'Frame {i+2}')

ax.legend()
ax.set_title("Projected Points on Frame 1")
plt.xlabel("u (pixels)")
plt.ylabel("v (pixels)")
plt.gca().invert_yaxis()  # 画像座標系の上下反転
plt.show()