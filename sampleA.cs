import numpy as np
import matplotlib.pyplot as plt

# 定数
frames = 10
focal_length = 8  # 焦点距離 (mm)
pixel_pitch = 0.005  # 画素ピッチ (mm/pixel)
camera_positions = np.array([[i * 2, 0, 10 - i] for i in range(frames)])  # 各フレームのカメラ座標
yaw_rates = np.linspace(0, 0.5, frames)  # 仮のヨーレート

# 回転行列を計算
def compute_rotation_matrix(yaw_rate):
    theta = np.radians(yaw_rate)
    return np.array([
        [np.cos(theta), -np.sin(theta), 0],
        [np.sin(theta), np.cos(theta), 0],
        [0, 0, 1]
    ])

# 投影変換
def project_to_image(camera_coords, focal_length, pixel_pitch):
    X_c, Y_c, Z_c = camera_coords
    u = (focal_length * X_c) / (Z_c * pixel_pitch)
    v = (focal_length * Y_c) / (Z_c * pixel_pitch)
    return np.array([u, v])

# 1フレーム目に描画
base_position = camera_positions[0]
fig, ax = plt.subplots()
for i in range(1, frames):
    # カメラ座標系に変換
    relative_position = camera_positions[i] - base_position
    rotation_matrix = compute_rotation_matrix(yaw_rates[i])
    transformed_position = rotation_matrix.dot(relative_position)

    # 画像座標系に投影
    image_coords = project_to_image(transformed_position, focal_length, pixel_pitch)

    # 描画
    ax.scatter(image_coords[0], image_coords[1], label=f'Frame {i}')

ax.legend()
ax.set_title("Projected Points on Frame 1")
plt.show()