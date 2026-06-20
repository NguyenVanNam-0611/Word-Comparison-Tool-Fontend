# Word-Comparison-Tool

Đây là phần C# ASP.NET Core Web API của hệ thống Word Document Comparison. Project này đóng vai trò là lớp trung gian giữa frontend React và backend xử lý so sánh tài liệu Word.

Ứng dụng chịu trách nhiệm tiếp nhận request từ frontend, xử lý authentication, quản lý luồng gọi API, chuyển tiếp file upload sang backend so sánh tài liệu, theo dõi trạng thái job xử lý và hỗ trợ export báo cáo kết quả so sánh.

## Tổng quan

Hệ thống Word Comparison gồm các phần chính:

* React Frontend: giao diện người dùng
* C# ASP.NET Core Web API: API Gateway / Backend trung gian
* Python FastAPI Service: service xử lý so sánh tài liệu Word

Trong kiến trúc này, C# Web API không trực tiếp xử lý logic so sánh file Word. Phần xử lý như trích xuất nội dung, so sánh heading, paragraph, table, image, shape và sinh kết quả JSON được thực hiện ở Python backend.

C# service tập trung vào việc cung cấp API cho frontend, quản lý request, xác thực người dùng và điều phối dữ liệu giữa frontend và Python service.

## Chức năng chính

* Cung cấp API cho frontend React
* Xử lý đăng nhập và xác thực người dùng
* Nhận request upload hai tài liệu Word từ frontend
* Gửi file sang Python backend để tạo job so sánh
* Theo dõi trạng thái xử lý job
* Trả kết quả so sánh về frontend
* Gọi API export từ backend
* Trả file báo cáo kết quả so sánh cho người dùng tải về
* Quản lý cấu hình API endpoint, authentication và kết nối service

## Luồng xử lý chính

1. Người dùng đăng nhập trên frontend
2. Frontend gửi request đăng nhập tới C# Web API
3. C# Web API xác thực thông tin và trả token cho frontend
4. Người dùng upload hai file Word
5. Frontend gửi file lên C# Web API
6. C# Web API chuyển tiếp file sang Python backend
7. Python backend tạo job xử lý và trả về `job_id`
8. Frontend dùng `job_id` để gọi API trạng thái thông qua C# Web API
9. C# Web API lấy trạng thái job từ Python backend và trả về frontend
10. Khi job hoàn thành, frontend nhận kết quả JSON để hiển thị giao diện so sánh
11. Khi người dùng export, frontend gửi request export tới C# Web API
12. C# Web API gọi backend export và trả file báo cáo cho người dùng

## Vai trò của C# Web API

Project này đóng vai trò là lớp điều phối trong hệ thống.

Các nhiệm vụ chính gồm:

* Chuẩn hóa API cho frontend
* Ẩn endpoint nội bộ của Python backend
* Quản lý authentication
* Chuyển tiếp request upload file
* Proxy request trạng thái job
* Proxy request export báo cáo
* Xử lý lỗi giữa frontend và backend
* Trả response thống nhất cho frontend

## Công nghệ sử dụng

* C#
* ASP.NET Core Web API
* .NET
* Entity Framework Core
* SQLite
* JWT Authentication
* HttpClient
* REST API

## Cấu trúc thư mục chính

```text
Word-Comparison-Tool/
├── Controllers/
├── Data/
├── DTOs/
├── Migrations/
├── Models/
├── Properties/
├── Services/
├── Templates/
├── text-compare-client/
├── appsettings.json
├── appsettings.Development.json
├── Program.cs
├── Diff_tool.csproj
├── Diff_tool.sln
└── README.md
```

## Mô tả các thành phần

### Controllers

Chứa các API endpoint để frontend gọi.

Các controller chính có thể bao gồm:

* Auth Controller
* Compare Controller
* Export Controller
* Status Controller

### Services

Chứa logic xử lý nghiệp vụ ở phía C#.

Ví dụ:

* Xử lý đăng nhập
* Tạo JWT token
* Gọi Python backend bằng HttpClient
* Chuyển tiếp file upload
* Nhận file export từ backend
* Xử lý response trả về frontend

### DTOs

Chứa các object dùng để nhận request và trả response giữa frontend và backend.

### Models

Chứa các model dữ liệu chính của hệ thống.

### Data

Chứa cấu hình database context và các thành phần liên quan đến lưu trữ dữ liệu.

### Migrations

Chứa migration của Entity Framework Core nếu project có sử dụng database.

### Templates

Chứa các file template phục vụ cho chức năng export hoặc các chức năng liên quan đến báo cáo.

### text-compare-client

Chứa frontend React của hệ thống nếu frontend được đặt chung trong project C#.

## Cài đặt và chạy project

Khôi phục package:

```bash
dotnet restore
```

Build project:

```bash
dotnet build
```

Chạy project:

```bash
dotnet run
```

Hoặc mở file solution bằng Visual Studio:

```text
Diff_tool.sln
```

Sau đó chạy project bằng IIS Express hoặc Kestrel tùy cấu hình.

## Cấu hình

Các cấu hình chính nằm trong:

```text
appsettings.json
appsettings.Development.json
```

Một số cấu hình thường dùng:

* Connection string
* JWT settings
* Python backend endpoint
* Upload settings
* Export endpoint
* CORS policy

Ví dụ nhóm cấu hình có thể có:

```json
{
  "PythonBackend": {
    "BaseUrl": "http://localhost:8000"
  },
  "Jwt": {
    "Key": "your-secret-key",
    "Issuer": "word-comparison-tool",
    "Audience": "word-comparison-client"
  }
}
```

## Lưu ý khi push source

Không nên push các file build, cache, dữ liệu runtime hoặc thư viện sinh ra tự động.

Không nên push:

```text
bin/
obj/
.vs/
node_modules/
dist/
build/
*.db
*.sqlite3
uploads/
exports/
logs/
.env
```

Nên push:

```text
Controllers/
Data/
DTOs/
Migrations/
Models/
Properties/
Services/
Templates/
text-compare-client/src/
text-compare-client/public/
Program.cs
Diff_tool.csproj
Diff_tool.sln
appsettings.json
appsettings.Development.json
README.md
.gitignore
```

Nếu trong `appsettings.json` có secret key, mật khẩu hoặc connection string thật, nên tạo file cấu hình mẫu như `appsettings.example.json` và không push file cấu hình thật lên repository.

## Ghi chú

Project C# này không xử lý trực tiếp thuật toán so sánh tài liệu Word. Logic so sánh chi tiết được thực hiện ở Python backend. C# Web API đóng vai trò trung gian để frontend giao tiếp với backend xử lý, đồng thời quản lý authentication, request flow và export result.

Cách tách này giúp hệ thống rõ ràng hơn:

* Frontend tập trung vào giao diện
* C# Web API tập trung vào API, xác thực và điều phối request
* Python backend tập trung vào xử lý tài liệu Word và thuật toán so sánh
