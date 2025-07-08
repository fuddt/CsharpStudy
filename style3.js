<table class="soft-blue-table">
  <thead>
    <tr>
      <th>列A</th>
      <th>列B</th>
      <th>列C</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>短いテキスト</td>
      <td>すごく長いテキストです。すごく長いテキストです。すごく長いテキストです。すごく長いテキストです。すごく長いテキストです。</td>
      <td>短い</td>
    </tr>
    <tr>
      <td>これも短い</td>
      <td>めちゃめちゃ長い文章がここに入ります。めちゃめちゃ長い文章がここに入ります。めちゃめちゃ長い文章がここに入ります。</td>
      <td>短文</td>
    </tr>
  </tbody>
</table>

<style>
.soft-blue-table {
  border-collapse: separate;
  border-spacing: 0;
  width: 100%;
  font-family: "Segoe UI", Arial, sans-serif;
  background: #fafdff;
  border: 2px solid #b9e2fa;
  border-radius: 12px;
  box-shadow: 0 1px 8px rgba(128, 192, 255, 0.10);
  overflow: hidden;
}

.soft-blue-table th, .soft-blue-table td {
  border: 1px solid #d0ebff;
  padding: 10px 16px;
  text-align: left;
  font-size: 15px;
  background: transparent;
  height: 80px;                /* 80px固定 */
  max-height: 80px;            /* 最大80px */
  word-break: break-all;       /* 折り返し */
  white-space: normal;         /* 折り返し有効 */
  vertical-align: top;         /* 上揃え */
  overflow-y: auto;            /* セルごとに縦スクロール */
}

.soft-blue-table thead th {
  background: #eaf6ff;
  color: #25609e;
  font-weight: 600;
  font-size: 16px;
  letter-spacing: 0.05em;
  border-bottom: 2px solid #b9e2fa;
  height: 48px;
  max-height: 48px;
}

.soft-blue-table tbody tr {
  background: #fafdff;
  transition: background 0.2s;
}

.soft-blue-table tbody tr:hover {
  background: #eaf6ff;
}

.soft-blue-table td {
  vertical-align: top;
}

.soft-blue-table {
  margin: 24px 0;
}
</style>