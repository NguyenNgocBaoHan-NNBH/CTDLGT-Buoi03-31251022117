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
            private T[] _array;
            private int _front;
            private int _rear;
            private int _count;

            public int Count => _count;
            public int Capacity => _array.Length;

            // Khởi tạo hàng đợi với dung lượng ban đầu là 4
            public CircularQueue()
            {
                _array = new T[4];
                _front = 0;
                _rear = 0;
                _count = 0;
            }

            // Thêm phần tử vào cuối hàng đợi
            public void Enqueue(T value)
            {
                if (_count == Capacity)
                {
                    Resize(Capacity * 2);
                }

                _array[_rear] = value;
                _rear = (_rear + 1) % Capacity;
                _count++;
            }

            // Lấy và xóa phần tử ở đầu hàng đợi
            public T Dequeue()
            {
                if (_count == 0)
                {
                    throw new InvalidOperationException("Hàng đợi rỗng!");
                }

                T value = _array[_front];
                _array[_front] = default(T); // Xóa tham chiếu để tối ưu bộ nhớ (GC)
                _front = (_front + 1) % Capacity;
                _count--;

                return value;
            }

            // Xem phần tử ở đầu hàng đợi mà không xóa
            public T Peek()
            {
                if (_count == 0)
                {
                    throw new InvalidOperationException("Hàng đợi rỗng!");
                }
                return _array[_front];
            }

            // Hàm mở rộng mảng gấp đôi và sắp xếp lại vị trí logic
            private void Resize(int newCapacity)
            {
                T[] newArray = new T[newCapacity];

                // Chép các phần tử từ mảng cũ sang mảng mới theo đúng thứ tự logic từ front
                for (int i = 0; i < _count; i++)
                {
                    newArray[i] = _array[(_front + i) % Capacity];
                }

                _array = newArray;
                _front = 0;
                _rear = _count; // Vị trí rear mới sẽ nằm ngay sau phần tử cuối cùng
            }
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== CHƯƠNG TRÌNH KIỂM THỬ HÀNG ĐỢI VÒNG (CIRCULAR QUEUE) ===\n");

            // --- CA KIỂM THỬ 1 ---
            Console.WriteLine("STT 1: Tạo hàng mới; Dequeue; Peek");
            var q1 = new CircularQueue<string>();
            Console.WriteLine($"-> Khởi tạo: Count = {q1.Count}, Capacity = {q1.Capacity}");
            try { q1.Dequeue(); }
            catch (InvalidOperationException ex) { Console.WriteLine($"-> Thử Dequeue: {ex.Message}"); }
            try { q1.Peek(); }
            catch (InvalidOperationException ex) { Console.WriteLine($"-> Thử Peek: {ex.Message}"); }
            Console.WriteLine();

            // --- CA KIỂM THỬ 2 ---
            Console.WriteLine("STT 2: Enqueue A, B, C, D; Peek");
            var q2 = new CircularQueue<string>();
            q2.Enqueue("A"); q2.Enqueue("B"); q2.Enqueue("C"); q2.Enqueue("D");
            Console.WriteLine($"-> Sau khi Enqueue 4 phần tử: Count = {q2.Count}, Capacity = {q2.Capacity}");
            Console.WriteLine($"-> Peek: {q2.Peek()}");
            Console.WriteLine();

            // --- CA KIỂM THỬ 3 ---
            Console.WriteLine("STT 3: Enqueue A, B, C, D; Dequeue 2 lần; Enqueue E, F; Dequeue 4 lần");
            var q3 = new CircularQueue<string>();
            q3.Enqueue("A"); q3.Enqueue("B"); q3.Enqueue("C"); q3.Enqueue("D");
            Console.WriteLine($"-> Lấy ra lần lượt: {q3.Dequeue()}, {q3.Dequeue()}");
            q3.Enqueue("E"); q3.Enqueue("F");
            Console.WriteLine($"-> Trạng thái hiện tại: Count = {q3.Count}, Capacity = {q3.Capacity}");
            Console.WriteLine($"-> Lấy ra tiếp: {q3.Dequeue()}, {q3.Dequeue()}, {q3.Dequeue()}, {q3.Dequeue()}");
            Console.WriteLine($"-> Trạng thái cuối: Count = {q3.Count}, Capacity = {q3.Capacity}");
            Console.WriteLine();

            // --- CA KIỂM THỬ 4 ---
            Console.WriteLine("STT 4: Enqueue 1, 2, 3, 4, 5; Dequeue 5 lần (Mở rộng khi front = 0)");
            var q4 = new CircularQueue<int>();
            q4.Enqueue(1); q4.Enqueue(2); q4.Enqueue(3); q4.Enqueue(4);
            Console.WriteLine($"-> Trước khi tràn: Capacity = {q4.Capacity}");
            q4.Enqueue(5); // Kích hoạt Resize khi front đang ở vị trí 0
            Console.WriteLine($"-> Sau khi Enqueue phần tử thứ 5: Capacity = {q4.Capacity}");
            Console.Write("-> Lấy ra: ");
            while (q4.Count > 0) Console.Write(q4.Dequeue() + " ");
            Console.WriteLine("\n");

            // --- CA KIỂM THỬ 5 ---
            Console.WriteLine("STT 5: Enqueue A, B, C, D; Dequeue 2 lần; Enqueue E, F, G; Dequeue 5 lần (Mở rộng khi front khác 0)");
            var q5 = new CircularQueue<string>();
            q5.Enqueue("A"); q5.Enqueue("B"); q5.Enqueue("C"); q5.Enqueue("D");
            q5.Dequeue(); q5.Dequeue(); // lúc này front dịch sang vị trí chỉ số 2
            q5.Enqueue("E"); q5.Enqueue("F"); // lấp đầy mảng vòng
            Console.WriteLine($"-> Trước khi tràn (đang bị lệch vòng): Count = {q5.Count}, Capacity = {q5.Capacity}");
            q5.Enqueue("G"); // Kích hoạt Resize khi front != 0
            Console.WriteLine($"-> Sau khi Enqueue G: Count = {q5.Count}, Capacity = {q5.Capacity}");
            Console.Write("-> Lấy ra: ");
            while (q5.Count > 0) Console.Write(q5.Dequeue() + " ");
            Console.WriteLine("\n");

            // --- CA KIỂM THỬ 6 ---
            Console.WriteLine("STT 6: Lặp 1 000 000 lần: Enqueue rồi Dequeue");
            var q6 = new CircularQueue<int>();
            bool isSuccess = true;
            for (int i = 0; i < 1000000; i++)
            {
                q6.Enqueue(i);
                if (q6.Dequeue() != i)
                {
                    isSuccess = false;
                    break;
                }
            }
            Console.WriteLine($"-> Kết quả chạy: {(isSuccess ? "Thành công (Không lỗi)" : "Thất bại")}");
            Console.WriteLine($"-> Trạng thái cuối: Count = {q6.Count}, Capacity = {q6.Capacity}");
            Console.WriteLine();

            // --- CA KIỂM THỬ 7 ---
            Console.WriteLine("STT 7: Enqueue 100 000 phần tử rồi Dequeue hết (Ca lớn)");
            var q7 = new CircularQueue<int>();
            Stopwatch sw = Stopwatch.StartNew();

            for (int i = 1; i <= 100000; i++)
            {
                q7.Enqueue(i);
            }

            bool orderCorrect = true;
            for (int i = 1; i <= 100000; i++)
            {
                if (q7.Dequeue() != i)
                {
                    orderCorrect = false;
                }
            }
            sw.Stop();
            Console.WriteLine($"-> Thứ tự lấy ra: {(orderCorrect ? "Chính xác (Đúng FIFO)" : "Sai thứ tự")}");
            Console.WriteLine($"-> Capacity cuối cùng: {q7.Capacity}");
            Console.WriteLine($"-> Tổng thời gian thực hiện: {sw.ElapsedMilliseconds} ms");

            Console.ReadLine();
        }
    }
}
