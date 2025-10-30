import json
import streamlit as st
from pathlib import Path

SETTINGS_PATH = Path("settings.txt")

def load_settings() -> dict:
    """設定ファイルを読み込む。なければデフォルトを返す。"""
    if SETTINGS_PATH.exists():
        return json.loads(SETTINGS_PATH.read_text(encoding="utf-8"))
    # 初期値（好きに変えてOK）
    return {
        "env": "dev",
        "batch_size": 50,
        "enable_feature_x": False,
    }

def save_settings(data: dict) -> None:
    """設定ファイルに書き込む。"""
    SETTINGS_PATH.write_text(json.dumps(data, indent=2, ensure_ascii=False), encoding="utf-8")


st.set_page_config(page_title="設定", layout="centered")

st.title("設定ページ（カスタムUI版）")

# ① まずPython側で設定を読む
if "settings" not in st.session_state:
    st.session_state.settings = load_settings()

settings = st.session_state.settings

# ② HTMLに渡すためにJSON化しておく
settings_json = json.dumps(settings)

# ③ HTML/JSを直接書く
#    → JSから設定を編集して「保存」ボタンでstreamlitにPOSTするイメージ
#    → 今回は window.parent.postMessage を使って streamlit に値を返す
component_html = f"""
<div id="app" style="font-family: sans-serif;">
  <h3 style="margin-bottom: 0.5rem;">アプリ設定</h3>
  <p style="color: #777; margin-top: 0;">settings.txt を編集するUIのサンプル</p>

  <!-- env のセレクト -->
  <label style="display:block; margin-top:1rem;">環境 (env)</label>
  <select id="env" style="width: 200px; padding: 4px;">
    <option value="dev">dev</option>
    <option value="staging">staging</option>
    <option value="prod">prod</option>
  </select>

  <!-- batch_size の入力 -->
  <label style="display:block; margin-top:1rem;">バッチサイズ</label>
  <input id="batch_size" type="number" min="1" value="50" style="width: 200px; padding: 4px;" />

  <!-- enable_feature_x のチェックボックス -->
  <label style="display:block; margin-top:1rem;">
    <input id="enable_feature_x" type="checkbox" />
    機能Xを有効にする
  </label>

  <button id="saveBtn" style="margin-top:1.5rem; padding: 6px 16px;">保存</button>

  <p id="status" style="color: green; margin-top: 1rem; display:none;">保存しました</p>
</div>

<script>
  // Pythonから渡された初期値
  const initialSettings = {settings_json};

  // 初期値を反映
  document.getElementById("env").value = initialSettings.env;
  document.getElementById("batch_size").value = initialSettings.batch_size;
  document.getElementById("enable_feature_x").checked = initialSettings.enable_feature_x;

  // 保存ボタンクリック時に streamlit 側に値を返す
  document.getElementById("saveBtn").addEventListener("click", function() {{
    const data = {{
      env: document.getElementById("env").value,
      batch_size: Number(document.getElementById("batch_size").value),
      enable_feature_x: document.getElementById("enable_feature_x").checked
    }};

    // Streamlit にメッセージを送る
    const jsonStr = JSON.stringify(data);
    window.parent.postMessage({{isStreamlitMessage: true, type: "streamlit:setComponentValue", value: jsonStr}}, "*");

    // 画面上のメッセージ
    document.getElementById("status").style.display = "block";
    setTimeout(() => {{
      document.getElementById("status").style.display = "none";
    }}, 2000);
  }});
</script>
"""

# ④ コンポーネントとして描画
#    → returned_value には JS 側から渡した値が入る
returned_value = st.components.v1.html(
    component_html,
    height=420,
)

# ⑤ 値が返ってきたらファイルに保存
if returned_value:
    try:
        new_settings = json.loads(returned_value)
        st.session_state.settings = new_settings
        save_settings(new_settings)
        st.success("設定を保存しました。")
    except Exception as e:
        st.error(f"保存に失敗しました: {e}")

# ⑥ Python側にも普通の表示しておくとデバッグしやすい
st.write("現在の設定:", st.session_state.settings)