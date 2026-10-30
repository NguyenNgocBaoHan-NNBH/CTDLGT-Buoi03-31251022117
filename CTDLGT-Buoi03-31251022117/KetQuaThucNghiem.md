# KẾT QUẢ THỰC NGHIỆM – BÀI TẬP THỰC HÀNH 03

**Sinh viên:** Nguyễn Ngọc Bảo Hân 
**MSSV:** 31251022117 
**Giảng viên:** TS. Nguyễn Thành Huy  

---

## 1. Bài 1 – Kiểm tra cặp ngoặc bằng Stack

| STT | Dữ liệu vào | Kết quả thực tế | Đạt? | Nhận xét |
|---:|---|---|:---:|---|
| 1 | `{(a+b)×[c−d]}` | HỢP LỆ | Có | Ngoặc lồng đúng thứ tự. |
| 2 | `{(a+b]×c)` | SAI tại vị trí 5 | Có | Phát hiện đóng sai loại (] bọc (). |
| 3 | `(a+b` | SAI tại vị trí 0 | Có | Phát hiện mở chưa đóng.|
| 4 | `a+b)` | SAI tại vị trí 3 | Có | Phát hiện mở chưa đóng.|
| 5 | Chuỗi rỗng | HỢP LỆ | Có | Ca biên chuỗi rỗng xử lý chuẩn. |
| 6 | `([)]` | SAI tại vị trí 2 | Có |Các cặp ngoặc đan xen cắt chéo logic. |
| 7 | `a+b` | HỢP LỆ | Có | Chuỗi văn bản không chứa ngoặc. |
| 8 | `{{(` | SAI tại vị trí 0 | Có | Nhiều ngoặc mở chưa đóng, báo cái sớm nhất. |
| 9 | 500.000 ngoặc `(` theo sau bởi 500.000 ngoặc `)` | HỢP LỆ; 0,0046 s | Có | Dữ liệu lớn, thời gian cực ngắn (< 1s). |
| 10 | 1.000.000 ngoặc `(` | SAI tại vị trí 0; 0.065 s | Có | Stack đạt độ sâu tối đa 1 triệu, chạy < 1s. |

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


---
## 4. Bài 4 

| STT | Dữ liệu vào hệ thống | Kết quả mong đợi | Kết quả thực tế | Đạt? | Nhận xét |
| :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | Lần lượt đẩy (Push) các phần tử:<br>5, 3, 7, 3, 8 | Sau mỗi lần nạp, GetMin() lần lượt xuất ra kết quả là:<br>5, 3, 3, 3, 3 | Push(5) -> Min=5<br>Push(3) -> Min=3<br>Push(7) -> Min=3<br>Push(3) -> Min=3<br>Push(8) -> Min=3 | Đạt | Hệ thống duy trì chính xác giá trị nhỏ nhất là 3 khi các phần tử lớn hơn xuất hiện, quản lý tốt dữ liệu trùng lặp. |
| 2 | Thực hiện lệnh Pop() liên tục 4 lần | Sau mỗi lần rút đỉnh, mốc GetMin() còn lại lần lượt là:<br>3, 3, 3, 5 | Pop() ra 8 -> Min=3<br>Pop() ra 3 -> Min=3<br>Pop() ra 7 -> Min=3<br>Pop() ra 3 -> Min=5 | Đạt | Khi rút số 3 thứ hai ở đỉnh, số 3 thứ nhất vẫn giữ vững mốc Min. Khi rút nốt số 3 đầu tiên, mốc Min tự động khôi phục về 5 chuẩn xác. |
| 3 | Tiếp tục gọi Pop() trên Stack trống | Phát sinh ngoại lệ bảo vệ:<br>InvalidOperationException | Bắt được lỗi thành công:<br>Ngăn xếp rỗng. | Đạt | Cơ chế bẫy lỗi ca biên vận hành an toàn. |

