
ポイント整理すると：

⸻

1. 基本ルール：軸スケールは Altair の指定が優先

まず大前提として、

chart = (
    alt.Chart(df)
    .mark_circle()
    .encode(
        x=alt.X(
            'x:Q',
            scale=alt.Scale(domain=[0, 400]),   # ここで表示範囲を固定
            axis=alt.Axis(values=[0, 100, 200, 300, 400])  # ここで目盛り位置を固定
        ),
        y='y:Q'
    )
)

みたいに scale.domain と axis.values を明示しておけば、
Vega-Lite 的には「これを守る」のが仕様。

Streamlit で描画しても、ここは「勝手に変えない」のが基本。

⸻

2. Streamlit が“いい感じにしてくる”ところ

ただし、Streamlit 側にはこういう要素がある：

(1) use_container_width=True で横幅が可変

st.altair_chart(chart, use_container_width=True)

これをやると「横幅に合わせてレイアウト再計算」されるから、
ラベルの表示間隔・回転・省略は変わりやすい。
	•	目盛りの「値」は axis.values を守る
	•	でも ラベルが全部出るとは限らない（詰まりすぎると間引かれる）

「いい感じに調整されちゃう」って感覚はここが怪しい。

⸻

3. 「Altair では効いてるけど、Streamlit で効いてない気がする」チェック方法

一番手っ取り早いのは：
	1.	Altair 単体で Jupyter / VSCode 上で chart を表示してみる
	2.	同じコードを st.altair_chart(chart) で表示してみる

このとき、
	•	Altair では x 軸の目盛り／スケールが思った通り
	•	Streamlit だとラベル間引き・回転などが起きて「なんか勝手に変えてない？」感が出る

というパターンが多い。

⸻

4. それでも制御したいときのテク

A. ラベルの間引きをなるべく減らす

x = alt.X(
    'x:Q',
    scale=alt.Scale(domain=[0, 400]),
    axis=alt.Axis(
        values=[0, 100, 200, 300, 400],
        labelOverlap=False  # ラベルの重なり自動処理をオフ
    )
)

labelOverlap=False で「重なってもいいから出せ」と言える。

B. どうしても Streamlit が気になるなら、幅を固定する

chart = chart.properties(width=600)  # 幅を固定する

st.altair_chart(chart, use_container_width=False)

	•	幅を固定しつつ、use_container_width=False にすると
Streamlit の「横いっぱいに伸ばすロジック」から逃げられる。

これでだいぶ「Altair本来の姿」に近くなるはず。

⸻

5. x軸を「圧縮」したい話とのつなぎ

前の話とも絡むけど、
	•	距離そのものを縮めたい → scale.domain を狭くする or データを変換する（cm→m）
	•	見た目のラベル感を調整したい → axis.values / axis.labelOverlap / width / use_container_width

Streamlit で「いい感じに調整されちゃう」というのは
たいてい 後者（ラベルの見せ方・レイアウト）の話。

⸻

ざっくりサンプル（Streamlit 想定）

import streamlit as st
import altair as alt
import pandas as pd

df = pd.DataFrame({
    "x": [0, 300],
    "y": [5, 10],
})

chart = (
    alt.Chart(df)
    .mark_circle(size=100)
    .encode(
        x=alt.X(
            "x:Q",
            scale=alt.Scale(domain=[0, 400]),
            axis=alt.Axis(
                values=[0, 100, 200, 300, 400],
                labelOverlap=False,
            ),
        ),
        y="y:Q",
    )
    .properties(width=600, height=400)
)

st.altair_chart(chart, use_container_width=False)

これで挙動どう変わるか、一回試してみてほしい。
もしまだ「勝手に直されてる」感が残るようなら、
そのときのコード断片を貼ってくれれば、ピンポイントで見にいくよ。