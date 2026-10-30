# BÀI TẬP THỰC HÀNH 03 – CẤU TRÚC DỮ LIỆU VÀ GIẢI THUẬT

* **Họ và tên:** Nguyễn Ngọc Bảo Hân
* **Mã số sinh viên:** 31251022117
* **Lớp học phần:** 26C1INF50900706
* **Giảng viên hướng dẫn:** TS. Nguyễn Thành Huy
* **Trường:** Đại học Kinh tế Thành phố Hồ Chí Minh 

## 1. CẤU TRÚC THƯ MỤC REPOSITORY
```text
CTDLGT-Buoi03-31251022117/
├── Bai1/                 
├── Bai2/                  
├── Bai3/                  
├── Bai4/                  
├── README.md             
└── KetQuaThucNghiem.md
```
- Bai1: Dự án Console kiểm tra dấu ngoặc cân bằng (Stack)
- Bai2: Dự án Console mô phỏng quầy dịch vụ (Queue)
- Bai3: Dự án Console tự cài đặt hàng đợi vòng (Circular Queue)
- Bai4: Dự án Console thử thách tự chọn - Phương án B (MinStack)
- README.md: File thông tin tổng quan và hướng dẫn vận hành
- KetQuaThucNghiem.md: File tổng hợp số liệu thực nghiệm

---
## 2. HƯỚNG DẪN BIÊN DỊCH VÀ VẬN HÀNH

### Yêu cầu môi trường
* .NET SDK 6.0 hoặc các phiên bản mới hơn.
* Trình biên dịch: Visual Studio 2022 / VS Code cài kèm tiện ích mở rộng C#.
* Mỗi thư mục bài cần có project Console C# tương ứng (`.csproj`).

### Cách chạy chương trình bằng Terminal / CLI
Di chuyển vào thư mục của bài tập cần kiểm thử và thực thi lệnh:
```bash
cd Bai1
dotnet run
```
*(Thực hiện tương tự cho `Bai2`, `Bai3`, và `Bai4`).*
- Bai1: Chương trình nhập một chuỗi, tự động bỏ qua các ký tự thường và kiểm tra tính cân bằng của ba loại dấu ngoặc (), [], {}. Nếu chuỗi đúng, in HỢP LỆ; nếu sai, chỉ ra chính xác vị trí (chỉ số) gây lỗi đầu tiên
- Bai2: Nhập dòng 1 gồm n và k. Mỗi dòng trong n dòng tiếp theo gồm hai số nguyên a và s: thời điểm đến và thời gian phục vụ. Với (`n = 100000`, `k = 10`), chương trình tự tạo dữ liệu theo quy luật khách thứ `i` đến tại thời điểm `i - 1` và có thời gian phục vụ bằng `10`; không cần nhập thêm 100.000 dòng dữ liệu.
- Bai3: Chương trình tự chạy 7 ca kiểm thử được viết trong `Main()`.

## 3. TÓM TẮT GIẢI PHÁP VÀ ĐỘ PHỨC TẠP
| Bài tập | Giải pháp cấu trúc dữ liệu | Độ phức tạp Thời gian | Độ phức tạp Bộ nhớ phụ |
| :--- | :--- | :--- | :--- |
| **Bài 1** | Dùng `Stack<(char, int)>` để lưu cặp (ngoặc mở, vị trí). Khi gặp ngoặc đóng, kiểm tra Stack rỗng để báo lỗi thừa, hoặc lấy đỉnh Stack ra so khớp để báo lỗi sai loại. Nếu duyệt hết chuỗi mà Stack vẫn còn dư phần tử, trả về vị trí ngoặc mở nằm ở đáy Stack (ngoặc chưa đóng xuất hiện sớm nhất).|  `Θ(n)` | `O(n)`|
| **Bài 2** | Dùng `Queue<Customer>` của .NET mô phỏng hàng chờ FIFO. Duyệt mảng trạng thái để phân phối quầy dịch vụ rảnh sớm nhất, nếu nhiều quầy cùng rảnh thì giữ quầy có số thứ tự nhỏ nhất. Thời điểm bắt đầu phục vụ là giá trị lớn hơn giữa thời điểm khách đến và lúc quầy rảnh. |  `Θ(n·k)` | `Θ(n + k)` |
| **Bài 3** | Tự cài đặt `CircularQueue<T>` bằng một mảng, dùng ba biến front, rear và count. Khi mảng đầy, lớp tự mở rộng mảng gấp đôi nhưng vẫn giữ đúng thứ tự FIFO. | - Enqueue: `O(1)` thông thường, `O(n)` khi mở rộng; độ phức tạp khấu hao `O(1)`. - `Dequeue()`: `O(1)`. - `Peek()`: `O(1)`.`O(n)`|`O(n)`|
| **Bài 4 (B)**| Song song hóa Ngăn xếp (`_mainStack` và `_minStack`) để lưu vết lịch sử biến động cực tiểu theo tiến trình thời gian thực. | Push/Pop/GetMin: `O(1)` | `O(n)` |

---
## 4. Ca kiểm thử và kết quả thực nghiệm

Các ca kiểm thử chi tiết, kết quả mong đợi, kết quả thực tế và ảnh chụp Terminal được trình bày trong báo cáo Word. 
File `KetQuaThucNghiem.md` tổng hợp các lần chạy và thời gian đo được.

Một số ca dữ liệu lớn cần ghi nhận thời gian chạy:

- **Bài 1:** 500 000 dấu ( rồi 500 000 dấu ); 1 000 000 dấu (.
- **Bài 2:** n = 100 000, k = 10, khách thứ i đến lúc i − 1, s = 10.
- **Bài 3:** Lặp 1 000 000 lần: Enqueue rồi Dequeue/ Enqueue 100 000 phần tử rồi Dequeue hết.
## 5. Khai báo sử dụng AI
Tôi đã sử dụng ChatGPT/Gemini để hỗ trợ định dạng và trình bày file báo cáo, file README.md, file KetQuaThucNghiem.md, tham khảo giải thích thuật toán và cách kiểm thử. Tôi tự kiểm tra, đối chiếu mã nguồn và chịu trách nhiệm về nội dung bài nộp.
