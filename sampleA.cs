import numpy as np
import matplotlib.pyplot as plt

# 定数
focal_length = 8  # 焦点距離 (mm)
pixel_pitch = 0.005  # 画素ピッチ (mm/pixel)
frame_time = 0.048  # フレーム時間 (s)

# 速度情報（各フレームの速度 m/s）
speeds = [5.0, 5.1, 5.2, 5.3, 5.4, 5.5, 5.6, 5.7, 5.8, 5.9]  # 仮のデータ
yaw_rates = [0.0, 0.1, 0.2, 0.1, 0.0, -0.1, -0.2, -0.1, 0.0, 0.1]  # 仮のヨーレート (°/s)
yaw_rates = np.radians(yaw_rates)  # ラジアンに変換

# 点の絶対座標（例）
absolute_points = [
    [10, 5, 50],  # フレーム1
    [10, 5, 45],  # フレーム2
    [10, 5, 40],  # フレーム3
    [10, 5, 35],  # フレーム4
    [10, 5, 30]   # フレーム5
]

# カメラの初期位置と向き
camera_position = np.array([0.0, 0.0, 0.0])
camera_angle = 0.0  # 初期向き（角度）

# カメラ座標を格納するリスト
camera_coords = []

# 各フレームの計算
for i, speed in enumerate(speeds):
    # 移動距離を計算
    dx = speed * frame_time * np.cos(camera_angle)
    dz = speed * frame_time * np.sin(camera_angle)
    camera_position += np.array([dx, 0, dz])

    # ヨーレートでカメラの向きを更新
    camera_angle += yaw_rates[i] * frame_time

    # 点をカメラ座標系に変換
    relative_point = np.array(absolute_points[min(i, len(absolute_points)-1)]) - camera_position
    camera_coords.append(relative_point)

# カメラ座標から画像座標への変換
image_coords = []
for coord in camera_coords:
    X_c, Y_c, Z_c = coord
    u = (focal_length * X_c) / (Z_c * pixel_pitch)
    v = (focal_length * Y_c) / (Z_c * pixel_pitch)
    image_coords.append([u, v])

# 結果をプロット
fig, ax = plt.subplots()
for i, (u, v) in enumerate(image_coords):
    ax.scatter(u, v, label=f'Frame {i+1}')

ax.legend()
ax.set_title("Projected Points on Frame 1")
plt.xlabel("u (pixels)")
plt.ylabel("v (pixels)")
plt.show()