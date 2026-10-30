using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CTDLGT_Buoi03_31251022117.Bai3
{
    internal class Bai3
    {
        public class CircularQueue<T>
        {
            // Mảng một chiều để lưu trữ dữ liệu của hàng đợi vòng
            private T[] _array;
            // Chỉ số lưu vị trí của phần tử đầu hàng đợi, dùng để xác định ô sẽ bị xóa khi Dequeue
            private int _front;
            // Chỉ số lưu vị trí của ô trống tiếp theo ở cuối hàng, dùng để nạp phần tử mới khi Enqueue
            private int _rear;
            // Biến đếm quản lý số lượng phần tử thực tế đang có
            private int _count;
            // Số lượng phần tử hiện có trong vòng đợi
            public int Count => _count;
            // Dung lượng hiện tại của mảng 
            public int Capacity => _array.Length;

            // Khởi tạo hàng đợi với dung lượng ban đầu là 4 
            public CircularQueue()
            {
                // Mảng ban đầu có kích thước là 4 ô nhớ
                _array = new T[4];
                // Ban đầu hàng đợi trống nên thiết lập chỉ số đầu hàng xuất phát từ vị trí 0
                _front = 0;
                // Vị trí đón phần tử đầu tiên cũng xuất phát tại ô chỉ số 0
                _rear = 0;
                // Đặt số lượng phần tử hiện có bằng 0 khi chưa có dữ liệu được nạp vào
                _count = 0;
            }

            // Thêm phần tử vào cuối hàng đợi
            public void Enqueue(T value)
            {
                // Nếu số phần tử bằng dung lượng mảng: hàng đợi đã đầy, cần kích hoạt mở rộng
                if (_count == Capacity)
                {
                    // Tạo mảng mới gấp đôi dung lượng hiện tại và sắp xếp lại trật tự dữ liệu
                    Resize(Capacity * 2);
                }
                // Đưa giá trị mới vào ô trống tại vị trí _rear hiện tại ở cuối hàng đợi
                _array[_rear] = value;
                // Tăng chỉ số _rear lên 1 đơn vị, dùng phép chia lấy dư để quay vòng về vị trí 0 nếu vượt quá biên mảng
                _rear = (_rear + 1) % Capacity;
                // Tăng tổng số lượng phần tử thực tế đang quản lý lên 1 đơn vị
                _count++;
            }

            // Lấy và xóa phần tử ở đầu hàng đợi
            public T Dequeue()
            {
                // Nếu không có phần tử nào trong hàng đợi thì không thể xóa
                if (_count == 0)
                {
                    throw new InvalidOperationException("Hàng đợi rỗng!");
                }

                // Tạm lưu giá trị tại vị trí đầu hàng (_front) trước khi xóa bỏ phần tử đó
                T value = _array[_front];
                // Xóa dữ liệu cũ tại vị trí vừa lấy
                _array[_front] = default(T);
                // Di chuyển _front, dùng phép chia lấy dư % Capacity để quay vòng về vị trí đầu mảng nếu chạm biên
                _front = (_front + 1) % Capacity;
                // Giảm tổng số lượng phần tử thực tế đang quản lý đi 1 đơn vị
                _count--;

                // Trả về giá trị của phần tử cũ vừa được rút ra khỏi đầu hàng đợi
                return value;
            }

            // Xem phần tử ở đầu hàng đợi mà không xóa
            public T Peek()
            {
                // Nếu không có phần tử nào trong hàng đợi thì không thể thực hiện hành động xem dữ liệu
                if (_count == 0)
                {
                    // Thốt ra ngoại lệ thông báo lỗi để ngăn chặn hành vi đọc vùng nhớ trống rỗng
                    throw new InvalidOperationException("Hàng đợi rỗng!");
                }
                // Trả về giá trị của phần tử nằm tại vị trí đầu hàng (_front) mà không dịch chuyển hay xóa chỉ số
                return _array[_front];
            }

            // Hàm mở rộng mảng gấp đôi và sắp xếp lại vị trí logic
            private void Resize(int newCapacity)
            {
                // Cấp một vùng nhớ mảng mới có kích thước lớn hơn theo tham số truyền vào
                T[] newArray = new T[newCapacity];

                // Chép các phần tử từ mảng cũ sang mảng mới theo đúng thứ tự LIFO
                for (int i = 0; i < _count; i++)
                {
                    // Trích dữ liệu từ mảng cũ theo thứ tự xoay vòng bắt đầu từ vị trí _front sang vị trí i tuyến tính của mảng mới
                    newArray[i] = _array[(_front + i) % Capacity];
                }

                // Trỏ biến _array sang vùng nhớ mảng mới vừa thiết lập xong để thay thế mảng cũ
                _array = newArray;
                // Đặt lại chỉ số đầu hàng về vị trí xuất phát 0 vì dữ liệu ở mảng mới đã được xếp phẳng thẳng hàng
                _front = 0;
                // Rear trỏ đến vị trí trống tiếp theo
                _rear = _count;
            }
        }
        static void Main3()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine(" Tạo hàng mới; Dequeue; Peek ");
            // Khởi tạo một hàng đợi vòng mới kiểu chuỗi với dung lượng  4
            var q1 = new CircularQueue<string>();
            Console.WriteLine($"Số phần tử = {q1.Count}, Dung lượng = {q1.Capacity}");
            // Sử dụng try-catch để kiểm tra Dequeue rỗng 
            try { q1.Dequeue(); }
            // Bắt đúng loại ngoại lệ InvalidOperationException được ném ra từ trong cấu trúc hàng đợi khi hàng rỗng
            catch (InvalidOperationException ex) { Console.WriteLine($"-> Thử Dequeue: {ex.Message}"); }
            // Sử dụng try-catch để kiểm tra Peek rỗng
            try { q1.Peek(); }
            // Bắt ngoại lệ thông báo lỗi khi xem phần tử đầu của hàng rỗng 
            catch (InvalidOperationException ex) { Console.WriteLine($"-> Thử Peek: {ex.Message}"); }
            Console.WriteLine();

            Console.WriteLine(" \nEnqueue A, B, C, D; Peek ");
            // Khởi tạo hàng đợi vòng mới 
            var q2 = new CircularQueue<string>();
            // Lần lượt nạp 4 phần tử A, B, C, D vào để lấp đầy dung lượng ban đầu mà không làm kích hoạt hàm mở rộng
            q2.Enqueue("A"); q2.Enqueue("B"); q2.Enqueue("C"); q2.Enqueue("D");
            // In ra số lượng phần tử thực tế (4) và dung lượng hiện có (4) để xác minh hàng đợi đã đạt ngưỡng giới hạn tối đa
            Console.WriteLine($"Sau khi Enqueue 4 phần tử: Count = {q2.Count}, Capacity = {q2.Capacity}");
            // In phần tử ở đầu hàng đợi (phần tử "A") bằng lệnh Peek để xác minh dữ liệu nạp trước được xếp ở đầu
            Console.WriteLine($"Peek: {q2.Peek()}");

            Console.WriteLine(" \nEnqueue A, B, C, D; Dequeue 2 lần; Enqueue E, F; Dequeue 4 lần ");
            // Khởi tạo hàng đợi vòng mới 
            var q3 = new CircularQueue<string>();
            // Nạp 4 phần tử ban đầu để làm đầy bộ nhớ mảng
            q3.Enqueue("A"); q3.Enqueue("B"); q3.Enqueue("C"); q3.Enqueue("D");
            // Rút 2 phần tử đầu tiên ra (A và B), lúc này chỉ số _front sẽ dịch lên vị trí 2, giải phóng 2 ô nhớ ở đầu mảng vật lý
            Console.WriteLine($"-> Lấy ra lần lượt: {q3.Dequeue()}, {q3.Dequeue()}");
            // Nạp tiếp E và F, nhờ cơ chế chia dư % nên biến _rear sẽ quay vòng về vị trí 0 và 1 để lấp vào khoảng trống 
            q3.Enqueue("E"); q3.Enqueue("F");
            Console.WriteLine($"-> Trạng thái hiện tại: Count = {q3.Count}, Capacity = {q3.Capacity}");
            // Lấy ra toàn bộ 4 phần tử còn lại để kiểm chứng xem trật tự logic (C, D, E, F) có được bảo toàn chính xác hay không
            Console.WriteLine($"-> Lấy ra tiếp: {q3.Dequeue()}, {q3.Dequeue()}, {q3.Dequeue()}, {q3.Dequeue()}");
            // In trạng thái cuối cùng để kiểm tra hàng đợi đã trở về trạng thái trống rỗng hoàn toàn sau chuỗi thao tác liên tục
            Console.WriteLine($"-> Trạng thái cuối: Count = {q3.Count}, Capacity = {q3.Capacity}");
            Console.WriteLine();

            Console.WriteLine("\n Enqueue 1, 2, 3, 4, 5; Dequeue 5 lần ");
            // Tạo một hàng đợi chứa số nguyên
            var hangDoi4 = new CircularQueue<int>();
            // Lần lượt nạp 4 phần tử đầu tiên vào 
            hangDoi4.Enqueue(1); hangDoi4.Enqueue(2); hangDoi4.Enqueue(3); hangDoi4.Enqueue(4);
            // In dung lượng trước khi nạp thêm 
            Console.WriteLine($"-> Trước khi tràn: Capacity = {hangDoi4.Capacity}");
            // Nạp phần tử thứ 5, vượt ngưỡng 4 phần tử nên lệnh Resize sẽ chạy, nhân đôi mảng lên kích thước 8 ô nhớ khi _front = 0
            hangDoi4.Enqueue(5);
            // In dung lượng mới sau khi nạp 
            Console.WriteLine($"-> Sau khi Enqueue phần tử thứ 5: Capacity = {hangDoi4.Capacity}");
            // Chạy vòng lặp kiểm tra khi nào hàng đợi vẫn còn dữ liệu thì tiếp tục lấy ra để hiển thị xem trật tự có đúng từ 1 đến 5 không
            while (hangDoi4.Count > 0) Console.Write(hangDoi4.Dequeue() + " ");
            Console.WriteLine("\n");

            Console.WriteLine("\n Enqueue A, B, C, D; Dequeue 2 lần; Enqueue E, F, G; Dequeue 5 lần ");
            // Tạo hàng đợi chứa chuỗi văn bản
            var hangDoi5 = new CircularQueue<string>();
            // Nạp 4 phần tử ban đầu 
            hangDoi5.Enqueue("A"); hangDoi5.Enqueue("B"); hangDoi5.Enqueue("C"); hangDoi5.Enqueue("D");
            // Rút 2 phần tử đầu ra để đẩy vị trí _front đến chỉ số 2 trong mảng
            hangDoi5.Dequeue(); hangDoi5.Dequeue();
            // Thêm E và F để chỉ số _rear về đầu mảng (vị trí 0 và 1), lúc này hàng đợi đầy ở trạng thái đứt đoạn logic
            hangDoi5.Enqueue("E"); hangDoi5.Enqueue("F");
            // In trạng thái ghi nhận dung lượng mốc cũ là 4 
            Console.WriteLine($"-> Trước khi tràn (đang bị lệch vòng): Count = {hangDoi5.Count}, Capacity = {hangDoi5.Capacity}");
            // Nạp thêm phần tử G, ép thuật toán gọi hàm Resize để sắp xếp phẳng lại mảng từ vị trí lệch vòng sang mảng mới thẳng hàng
            hangDoi5.Enqueue("G");
            // In trạng thái số lượng tăng lên 5 và sức chứa thành 8
            Console.WriteLine($"-> Sau khi Enqueue G: Count = {hangDoi5.Count}, Capacity = {hangDoi5.Capacity}");
            // Vòng lặp lấy ra toàn bộ dữ liệu để kiểm tra xem mảng sau khi Resize lệch vòng có giữ đúng trật tự logic (C, D, E, F, G) không
            while (hangDoi5.Count > 0) Console.Write(hangDoi5.Dequeue() + " ");
            Console.WriteLine("\n");

            Console.WriteLine("\n Lặp 1 000 000 lần: Enqueue rồi Dequeue");
            // Tạo hàng đợi mới 
            var hangDoi6 = new CircularQueue<int>();
            // Biến dùng để đánh dấu trạng thái đúng đắn của tiến trình di chuyển dữ liệu
            bool chayThanhCong = true;
            // Chạy vòng lặp 1 triệu lần liên tiếp 
            for (int chiSoVongLap = 0; chiSoVongLap < 1000000; chiSoVongLap++)
            {
                // Thêm chỉ số vòng lặp hiện tại vào cuối hàng đợi
                hangDoi6.Enqueue(chiSoVongLap);
                // Lấy ra và kiểm tra xem giá trị lấy ra có khớp với giá trị vừa nạp vào hay không
                if (hangDoi6.Dequeue() != chiSoVongLap)
                {
                    // Nếu phát hiện dữ liệu lấy ra bị lệch hoặc sai trật tự -> false
                    chayThanhCong = false;
                    // Ngưng vòng lặp vì đã phát hiện lỗi hệ thống
                    break;
                }
            }
            // Xuất kết quả trạng thái để xác minh thuật toán không lỗi logic
            Console.WriteLine($"-> Kết quả chạy: {(chayThanhCong ? "Thành công (Không lỗi)" : "Thất bại")}");
            // In trạng thái cuối để xem số lượng phần tử và sức chứa không bị phình to do nạp-rút luân phiên
            Console.WriteLine($"-> Trạng thái cuối: Count = {hangDoi6.Count}, Capacity = {hangDoi6.Capacity}");
            Console.WriteLine();

            Console.WriteLine("\n Enqueue 100 000 phần tử rồi Dequeue hết ");
            // Tạo hàng đợi mới 
            var hangDoiThuNghiemLon = new CircularQueue<int>();
            // Khởi tạo đồng hồ bấm giờ để đo lường thời gian 
            Stopwatch dongHoDoThoiGian = Stopwatch.StartNew();

            // Vòng lặp chạy từ 1 đến 100.000 
            for (int chiSoNap = 1; chiSoNap <= 100000; chiSoNap++)
            {
                // Nạp giá trị chiSoNap vào hàng đợi, hành động này sẽ kích hoạt hàm Resize nhân đôi dung lượng nhiều lần liên tiếp
                hangDoiThuNghiemLon.Enqueue(chiSoNap);
            }

            // Khởi tạo biến để kiểm tra xem cấu trúc dữ liệu có đảm bảo đúng nguyên tắc FIFO hay không.
            bool dungThuTuLogic = true;

            // Chạy vòng lặp 100.000 lần để lấy toàn bộ dữ liệu ra khỏi hàng đợi và đối chiếu thứ tự.
            for (int chiSoVongLap = 1; chiSoVongLap <= 100000; chiSoVongLap++)
            {
                // Lấy phần tử ở đầu hàng đợi ra, nếu giá trị lấy ra không khớp với số thứ tự khi nạp vào thì phát hiện lỗi lệch dữ liệu.
                if (hangDoiThuNghiemLon.Dequeue() != chiSoVongLap)
                {
                    // Hạ cờ xuống false để ghi nhận hàng đợi đã làm sai lệch trật tự dữ liệu đầu vào.
                    dungThuTuLogic = false;
                }
            }

            dongHoDoThoiGian.Stop();

            Console.WriteLine($"-> Thứ tự lấy ra: {(dungThuTuLogic ? "Chính xác (Đúng FIFO)" : "Sai thứ tự")}");

            Console.WriteLine($"-> Capacity cuối cùng: {hangDoiThuNghiemLon.Capacity}");

            Console.WriteLine($"-> Tổng thời gian thực hiện: {dongHoDoThoiGian.ElapsedMilliseconds} ms");

            Console.ReadLine();
        }
    }
}
