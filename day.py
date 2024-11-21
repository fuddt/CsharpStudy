import pandas as pd

# 開始日と終了日
start = '2023-01-01'
end = '2023-01-31'

# 終了日を調整
adjusted_end = pd.Timestamp(end) + pd.Timedelta(days=6 - pd.Timestamp(end).weekday())

# 週単位で生成
periods = pd.period_range(start=start, end=adjusted_end, freq='W')

print(periods)