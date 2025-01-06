import numpy as np
import matplotlib.pyplot as plt

# ----------------------
# 定数
# ----------------------
focal_length = 8  # 焦点距離 (mm)
pixel_pitch = 0.005  # 画素ピッチ (mm/pixel)
frame_time = 0.048  # フレーム時間 (s)
speed_m_per_s = 30 * 1000 / 3600  # 時速30km → 秒速8.33m/s

# カメラ初期位置と焦点距離
camera_position = np.array([0, 0, 5])  # 1フレーム目のカメラ位置
camera_angle = 0.0  # 初期カメラの向き（ヨーレート初期値）

# ヨーレート (rad/s)
yaw_rates = [-0.0045, -0.0048, -0.0060]

# 世界座標系の点の位置
points_world = [
    [10, 0, 800],   # 1フレーム目
    [30, 50, 800],  # 2フレーム目
    [50, 100, 800]  # 3フレーム目
]

# ----------------------
# カメラ座標系に変換
# ----------------------
camera_coords = []
for i, point in enumerate(points_world[1:], start=1):  # 2F, 3Fの点を処理
    # カメラ位置を更新 (速度を反映)
    dz = speed_m_per_s * frame_time  # カメラが進むZ方向の距離
    camera_position[2] += dz  # Z座標を更新

    # ヨーレートでカメラの向きを更新
    camera_angle += yaw_rates[i-1] * frame_time  # 累積ヨーレート適用

    # 回転行列を計算 (カメラが向きを変えた場合の座標変換)
    rotation_matrix = np.array([
        [np.cos(camera_angle), 0, -np.sin(camera_angle)],
        [0, 1, 0],
        [np.sin(camera_angle), 0, np.cos(camera_angle)]
    ])

    # 点をカメラ座標系に変換
    relative_position = np.array(point) - camera_position
    camera_coord = rotation_matrix @ relative_position  # 回転適用
    camera_coords.append(camera_coord)

# ----------------------
# カメラ座標系を2D画像座標に変換
# ----------------------
image_coords = []
for coord in camera_coords:
    X_c, Y_c, Z_c = coord
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
