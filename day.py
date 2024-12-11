import numpy as np

# 配列 a と b を定義
a = np.array([1, 2, 3, 4, 5])
b = np.array([2, 4, 6])

# 配列 b に含まれる要素だけを a から取り出す
result = a[np.isin(a, b)]

# 取り出した結果の形状で True 配列を作成
true_array = np.full(result.shape, True, dtype=bool)

print("元の配列 a:", a)
print("取り出した結果:", result)
print("全て True の配列:", true_array)