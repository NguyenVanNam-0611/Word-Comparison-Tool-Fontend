# Text Compare Client

Đây là phần frontend React của hệ thống so sánh tài liệu Word. Ứng dụng cung cấp giao diện cho người dùng upload hai tài liệu, theo dõi trạng thái xử lý, hiển thị kết quả so sánh song song và xuất báo cáo khi cần.

Frontend được xây dựng bằng React và Vite. Phần xử lý so sánh tài liệu được thực hiện ở backend, frontend chỉ chịu trách nhiệm gọi API, nhận dữ liệu kết quả và render giao diện cho người dùng.

## Chức năng chính

* Upload hai tài liệu Word
* Gửi request so sánh tài liệu lên backend
* Theo dõi trạng thái xử lý
* Hiển thị kết quả so sánh theo dạng song song
* Highlight nội dung được thêm, xóa hoặc chỉnh sửa
* Hiển thị thay đổi trong đoạn văn, bảng, hình ảnh và shape
* Hiển thị thông tin trang của thay đổi
* Hỗ trợ xuất báo cáo kết quả so sánh

## Công nghệ sử dụng

* React
* Vite
* JavaScript
* CSS
* REST API

## Cấu trúc source

```text
text-compare-client/
├── public/
├── src/
├── index.html
├── package.json
├── package-lock.json
├── vite.config.js
├── eslint.config.js
├── README.md
└── .gitignore
```

## Cài đặt

```bash
npm install
```

## Chạy project

```bash
npm run dev
```

Sau đó mở địa chỉ Vite hiển thị trên terminal, thường là:

```text
http://localhost:5173
```

## Build

```bash
npm run build
```

Kết quả build nằm trong thư mục:

```text
dist/
```

## Lưu ý khi push source

Chỉ cần push source code và file cấu hình.

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
