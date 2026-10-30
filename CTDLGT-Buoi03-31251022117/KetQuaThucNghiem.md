# KẾT QUẢ THỰC NGHIỆM – BÀI TẬP THỰC HÀNH 03

**Sinh viên:** Nguyễn Ngọc Bảo Hân 
**MSSV:** 31251022117 
**Giảng viên:** TS. Nguyễn Thành Huy  

---

## 1. Bài 1 – Kiểm tra cặp ngoặc bằng Stack

| STT | Dữ liệu vào | Kết quả thực tế | Đạt? | Nhận xét |
|---:|---|---|:---:|---|
| 1 | `{(a+b)×[c−d]}` | HỢP LỆ | Có | Các cặp ngoặc đúng loại và đúng thứ tự. |
| 2 | `{(a+b]×c)` | SAI tại vị trí 5 | Có | Ngoặc đóng `]` không khớp với ngoặc mở `(` trên đỉnh Stack. |
| 3 | `(a+b` | SAI tại vị trí 0 | Có | Còn ngoặc mở `(` chưa được đóng. |
| 4 | `a+b)` | SAI tại vị trí 3 | Có | Gặp ngoặc đóng `)` khi Stack đang rỗng. |
| 5 | Chuỗi rỗng | HỢP LỆ | Có | Không có ngoặc nên không phát sinh lỗi. |
| 6 | `([)]` | SAI tại vị trí 2 | Có | Ngoặc `)` không khớp với ngoặc `[` ở đỉnh Stack. |
| 7 | `a+b` | HỢP LỆ | Có | Các ký tự không phải ngoặc được bỏ qua. |
| 8 | `{{(` | SAI tại vị trí 0 | Có | Có nhiều ngoặc mở chưa được đóng; chương trình báo vị trí ngoặc mở sớm nhất. |
| 9 | 500.000 ngoặc `(` theo sau bởi 500.000 ngoặc `)` | HỢP LỆ; 13,955 ms | Có | Kiểm tra chuỗi lớn gồm 1.000.000 ký tự. |
| 10 | 1.000.000 ngoặc `(` | SAI tại vị trí 0; 17,795 ms | Có | Tất cả đều là ngoặc mở chưa được đóng; vị trí lỗi sớm nhất là 0. |

**Nhận xét:** Các ca kiểm thử bao phủ chuỗi hợp lệ, chuỗi rỗng, ký tự không phải ngoặc, ngoặc đóng thừa, đóng sai loại, mở chưa đóng và dữ liệu kích thước lớn. Kết quả Terminal phù hợp với kết quả mong đợi của từng ca.

---

## 2. Bài 2 – Mô phỏng quầy phục vụ bằng Queue

| STT | Dữ liệu vào | Kết quả thực tế | Đạt? | Nhận xét |
|---:|---|---|:---:|---|
| 1 | `n=3, k=1`; khách: `(0,5), (1,3), (2,4)` | Chờ: `0, 4, 6`; TB `3.33`; lâu nhất `6`; xong lúc `12` | Có | Một quầy nên khách được phục vụ lần lượt theo FIFO. |
| 2 | `n=3, k=2`; khách: `(0,5), (1,3), (2,4)` | Chờ: `0, 0, 2`; TB `0.67`; lâu nhất `2`; xong lúc `8` | Có | Hai quầy giúp giảm thời gian chờ. |
| 3 | `n=3, k=2`; khách: `(0,3), (0,3), (0,3)` | Chờ: `0, 0, 3`; TB `1.00`; lâu nhất `3`; xong lúc `6` | Có | Kiểm tra nhiều khách đến cùng thời điểm và quy tắc chọn quầy. |
| 4 | `n=2, k=1`; khách: `(0,2), (10,3)` | Chờ: `0, 0`; TB `0.00`; lâu nhất `0`; xong lúc `13` | Có | Quầy đã rảnh trước khi khách tiếp theo đến. |
| 5 | `n=1, k=3`; khách: `(4,6)` | Chờ: `0`; TB `0.00`; lâu nhất `0`; xong lúc `10` | Có | Kiểm tra trường hợp số quầy lớn hơn số khách. |
| 6 | `n=100000, k=10`; khách thứ `i` đến lúc `i−1`, thời gian phục vụ `10` | Thời gian chạy: `3.731 ms`; TB `0.00`; lâu nhất `0`; xong lúc `100009` | Có | Ca dữ liệu lớn. Dữ liệu được tạo tự động trong chương trình. |

**Lưu ý về lần chạy đầu của ca 6:** Ảnh Terminal có ghi nhận một lần chạy lỗi `IndexOutOfRangeException` khi chương trình đang đọc dữ liệu theo cách cũ. Sau đó sinh viên chạy lại bằng phiên bản tạo tự động dữ liệu cho 100.000 khách và nhận được kết quả nêu ở bảng. Kết quả thực tế được ghi nhận ở đây là lần chạy thành công sau khi sửa.

**Nhận xét:** Các ca kiểm thử xác nhận thứ tự FIFO, cách chọn quầy rảnh sớm nhất, xử lý trường hợp quầy rảnh trước khi khách đến và khả năng chạy với 100.000 khách hàng.

---

## 3. Bài 3 – Cài đặt CircularQueue<T>

| STT | Thao tác | Kết quả thực tế | Đạt? | Nhận xét |
|---:|---|---|:---:|---|
| 1 | Tạo hàng đợi mới; gọi `Dequeue()` và `Peek()` khi rỗng | Số phần tử `0`, dung lượng `4`; cả hai thao tác ném `InvalidOperationException` | Có | Kiểm tra trạng thái rỗng và xử lý ngoại lệ. |
| 2 | Enqueue `A, B, C, D`; gọi `Peek()` | Số phần tử `4`, dung lượng `4`, phần tử đầu `A` | Có | Hàng đợi đầy đúng dung lượng ban đầu, Peek không xóa phần tử. |
| 3 | Dequeue hai lần; Enqueue `E, F`; lấy hết phần tử còn lại | Lấy ra `A, B`; còn lại `C, D, E, F`; dung lượng `4` | Có | Kiểm tra quay vòng chỉ số mà không cần mở rộng mảng. |
| 4 | Enqueue `1, 2, 3, 4, 5`; lấy hết | Dung lượng `8`; lấy ra `1, 2, 3, 4, 5` | Có | Kiểm tra mở rộng mảng khi hàng đầy và `front = 0`. |
| 5 | Enqueue `A, B, C, D`; Dequeue hai lần; Enqueue `E, F, G`; lấy hết | Dung lượng `8`; lấy ra `C, D, E, F, G` | Có | Kiểm tra mở rộng khi dữ liệu đã quay vòng và `front` khác 0. |
| 6 | Lặp 1.000.000 lần thao tác Enqueue rồi Dequeue | Số phần tử `0`, dung lượng `4`; không phát sinh lỗi | Có | Kiểm tra quay vòng liên tục với số lần thao tác lớn. |
| 7 | Enqueue 100.000 phần tử rồi Dequeue hết | Đúng thứ tự `True`; số phần tử `0`; dung lượng cuối `131072`; thời gian chạy `3.030 ms` | Có | Kiểm tra thứ tự FIFO và nhiều lần mở rộng với dữ liệu lớn. |

**Nhận xét:** Các ca kiểm thử xác nhận hàng đợi xử lý đúng trạng thái rỗng, thứ tự FIFO, thao tác Peek, quay vòng chỉ số và mở rộng mảng. Đặc biệt, ca 5 kiểm tra việc sao chép đúng thứ tự khi `front` khác 0, còn ca 7 kiểm tra với 100.000 phần tử.

---

## 4. Tổng hợp thời gian chạy ở các ca lớn

| Bài | Ca kiểm thử | Thời gian ghi nhận |
|---|---|---:|
| Bài 1 | 500.000 ngoặc mở và 500.000 ngoặc đóng | 13,955 ms |
| Bài 1 | 1.000.000 ngoặc mở | 17,795 ms |
| Bài 2 | 100.000 khách hàng, 10 quầy | 3.731 ms |
| Bài 3 | 1.000.000 lượt Enqueue/Dequeue | Không hiển thị số đo trong ảnh kết quả được cung cấp |
| Bài 3 | Thêm và lấy 100.000 phần tử | 3.030 ms |

> **Ghi chú:** Dấu phẩy trong các số đo thời gian được giữ theo cách hiển thị trên Terminal. Các ảnh chụp Terminal gốc được chèn trong báo cáo Word để làm minh chứng; file này dùng để tổng hợp kết quả thực nghiệm.
