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
      <td>データ1</td>
      <td>データ2</td>
      <td>データ3</td>
    </tr>
    <tr>
      <td>データ4</td>
      <td>データ5</td>
      <td>データ6</td>
    </tr>
    <!-- ... -->
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
  border: none;
  padding: 10px 16px;
  text-align: left;
  font-size: 15px;
  background: transparent;
}

.soft-blue-table thead th {
  background: #eaf6ff;
  color: #25609e;
  font-weight: 600;
  font-size: 16px;
  letter-spacing: 0.05em;
  border-bottom: 2px solid #b9e2fa;
}

.soft-blue-table tbody tr {
  background: #fafdff;
  transition: background 0.2s;
}

.soft-blue-table tbody tr:hover {
  background: #eaf6ff;
}

.soft-blue-table td {
  vertical-align: middle;
}

.soft-blue-table {
  margin: 24px 0;
}
</style>