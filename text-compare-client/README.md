# Word-Comparison-Tool-Frontend

Đây là phần frontend React của hệ thống Word Comparison Tool. Ứng dụng được xây dựng để cung cấp giao diện cho người dùng upload hai tài liệu Word, theo dõi trạng thái xử lý, hiển thị kết quả so sánh song song giữa tài liệu gốc và tài liệu đã chỉnh sửa, đồng thời hỗ trợ xuất báo cáo kết quả so sánh.

Frontend không trực tiếp xử lý logic so sánh tài liệu Word. Phần xử lý chính được thực hiện ở backend. React client sẽ gọi API của C# ASP.NET Core Web API, nhận dữ liệu kết quả dạng JSON và render thành giao diện trực quan cho người dùng kiểm tra.

## Chức năng chính

* Upload hai tài liệu Word để so sánh
* Gửi request so sánh tài liệu lên C# Web API
* Theo dõi trạng thái xử lý job
* Nhận kết quả so sánh dạng JSON
* Hiển thị kết quả so sánh theo dạng song song
* Highlight nội dung được thêm, xóa hoặc chỉnh sửa
* Hiển thị thay đổi của heading, paragraph, table, image và shape
* Hiển thị thông tin trang của thay đổi
* Hỗ trợ export báo cáo kết quả so sánh

## Luồng xử lý frontend

1. Người dùng chọn tài liệu gốc và tài liệu chỉnh sửa
2. Frontend gửi hai file Word lên C# Web API
3. C# Web API chuyển tiếp request xử lý sang backend so sánh tài liệu
4. Backend trả về `job_id`
5. Frontend sử dụng `job_id` để theo dõi trạng thái xử lý
6. Khi job hoàn thành, frontend nhận kết quả JSON
7. Frontend render kết quả so sánh theo giao diện side-by-side
8. Người dùng có thể xem chi tiết thay đổi hoặc xuất báo cáo

## Công nghệ sử dụng

* React
* Vite
* JavaScript
* CSS
* REST API

## Cài đặt

```bash
npm install
```

## Chạy project

```bash
npm run dev
```

Địa chỉ mặc định của Vite thường là:

```text
http://localhost:5173
```

## Build project

```bash
npm run build
```

Kết quả build sẽ nằm trong thư mục:

```text
dist/
```

## Lưu ý khi push source

Chỉ cần push source code và file cấu hình cần thiết.

Nên push:

```text
src/
public/
index.html
package.json
package-lock.json
vite.config.js
eslint.config.js
README.md
.gitignore
```

Không nên push:

```text
node_modules/
dist/
build/
*.zip
.env
```

`node_modules` không cần đưa lên repository vì có thể cài lại bằng lệnh `npm install`.
