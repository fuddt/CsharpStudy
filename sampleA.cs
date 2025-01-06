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
# 例: x=0, y=1.5m (地面から1.5mの高さ), z=5
camera_position = np.array([0.0, 1.5, 5.0])

# カメラの向き (ヨー・ピッチ)
camera_yaw   = 0.0   # 左右回転 (rad)
camera_pitch = 0.0   # 上下回転 (rad)
# 今回はロールは省略

# ヨーレート (rad/s), ピッチレート (rad/s) など
yaw_rates   = [-0.0045, -0.0048, -0.0060]    # 3フレーム分
pitch_rates = [ 0.0000,  0.0005, -0.0003]    # 例として適当な値を入れる

# 世界座標系の点 (ここでは高さ方向は 0 として地面に置いている例)
points_world = [
    [0, 0,   800],   # 1F目
    [0, 0,   900],   # 2F目
    [10, 0, 1000],   # 3F目
]

# ----------------------
# カメラ座標系に変換
# ----------------------
camera_coords = []

for i, point in enumerate(points_world[1:], start=1):  # 2F, 3Fの点だけ処理
    # 例: z方向に進みつつ、x方向にも少し移動していると仮定
    #     実際には速度の向きを camera_yaw に合わせるなどの計算が必要
    move_dist = speed_m_per_s * frame_time

    # とりあえずシンプルに z方向に進む
    camera_position[2] += move_dist
    # カメラが少し右にズレるなどしたい場合は x 方向も加算:
    # camera_position[0] += move_dist * 0.1  # 例えば0.1だけ右に動く

    # ヨー・ピッチを更新
    camera_yaw   += yaw_rates[i-1]   * frame_time
    camera_pitch += pitch_rates[i-1] * frame_time

    # カメラの3次元回転行列を計算
    # 注意: 回転順序(ヨー→ピッチの順)によって行列が変わります
    # ここでは Y軸周り(ヨー)→X軸周り(ピッチ) の順で回す例
    Ryaw = np.array([
        [ np.cos(camera_yaw),  0, np.sin(camera_yaw)],
        [                  0,  1,                 0],
        [-np.sin(camera_yaw), 0, np.cos(camera_yaw)]
    ])
    Rpitch = np.array([
        [1,               0,                  0],
        [0, np.cos(camera_pitch), -np.sin(camera_pitch)],
        [0, np.sin(camera_pitch),  np.cos(camera_pitch)]
    ])
    rotation_matrix = Rpitch @ Ryaw  # ピッチ→ヨーの順で合成

    # 点をカメラ座標系に変換
    relative_position = np.array(point) - camera_position
    camera_coord = rotation_matrix @ relative_position
    camera_coords.append(camera_coord)

# ----------------------
# カメラ座標系を2D画像座標に変換
# ----------------------
image_coords = []
for coord in camera_coords:
    X_c, Y_c, Z_c = coord
    # Z_c が 0 以下なら、カメラの後ろにある等で投影できないのでスキップ等の処理が必要かも
    u = (focal_length * X_c) / (Z_c * pixel_pitch)
    v = (focal_length * Y_c) / (Z_c * pixel_pitch)
    image_coords.append([u, v])

# ----------------------
# 描画
# ----------------------
image_width = 1980
image_height = 1080

fig, ax = plt.subplots(figsize=(10, 5))
ax.set_xlim(0, image_width)
ax.set_ylim(0, image_height)

base_u, base_v = image_width // 2, image_height // 2

# Frame 1 (画像中央)
ax.scatter(base_u, base_v, color='red', label='Frame 1 (Base)')

for i, (u, v) in enumerate(image_coords):
    ax.scatter(base_u + u, base_v - v, label=f'Frame {i+2}')

ax.legend()
ax.set_title("Projected Points with 3D Camera Pose")
plt.xlabel("u (pixels)")
plt.ylabel("v (pixels)")
plt.gca().invert_yaxis()
plt.show()