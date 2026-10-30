using System;
using System.Collections.Generic;
using System.Text;

namespace CTDLGT_Buoi03_31251022117.Bai4
{
    public class MinStack
    {
        // Ngăn xếp chính để lưu trữ tất cả các phần tử được đưa vào theo nguyên lý LIFO.
        private Stack<int> _mainStack = new Stack<int>();
        // Ngăn xếp phụ lưu trữ các giá trị nhỏ nhất tương ứng với từng trạng thái của ngăn xếp chính.
        private Stack<int> _minStack = new Stack<int>();

        // Truy xuất số lượng phần tử hiện tại của ngăn xếp chính.
        public int Count => _mainStack.Count;

        // Thêm phần tử vào ngăn xếp với chi phí thời gian cố định O(1).
        public void Push(int x)
        {
            // Thêm phần tử mới vào đỉnh ngăn xếp chính để duy trì cấu trúc dữ liệu.
            _mainStack.Push(x);

            // Nếu ngăn xếp phụ đang rỗng hoặc phần tử mới nhỏ hơn hoặc bằng giá trị nhỏ nhất hiện tại.
            if (_minStack.Count == 0 || x <= _minStack.Peek())
            {
                // Đẩy phần tử này vào ngăn xếp phụ để cập nhật giá trị nhỏ nhất mới cho trạng thái hiện tại.
                _minStack.Push(x);
            }
        }

        // Lấy và xóa phần tử khỏi đỉnh ngăn xếp với chi phí thời gian cố định O(1).
        public int Pop()
        {
            // Nếu ngăn xếp chính không có phần tử nào thì không thể xóa.
            if (_mainStack.Count == 0)
                // Ném ra ngoại lệ để ngăn chặn truy xuất sai quy tắc trên cấu trúc rỗng.
                throw new InvalidOperationException("Ngăn xếp rỗng.");

            // Loại bỏ và trích xuất giá trị nằm ở đỉnh ngăn xếp chính ra ngoài.
            int poppedValue = _mainStack.Pop();

            // Nếu giá trị vừa rút ra trùng với giá trị nhỏ nhất hiện tại đang nằm trên đỉnh ngăn xếp phụ.
            if (poppedValue == _minStack.Peek())
            {
                // Loại bỏ phần tử đó khỏi ngăn xếp phụ để trả lại giá trị nhỏ nhất của trạng thái trước đó.
                _minStack.Pop();
            }

            // Trả về giá trị phần tử vừa được loại bỏ.
            return poppedValue;
        }

        // Truy xuất giá trị nhỏ nhất hiện tại trong ngăn xếp với chi phí thời gian cố định O(1).
        public int GetMin()
        {
            // Nếu ngăn xếp phụ trống, nghĩa là toàn bộ hệ thống đang không có phần tử nào.
            if (_minStack.Count == 0)
                // Ném ra thông báo lỗi ngoại lệ.
                throw new InvalidOperationException("Ngăn xếp rỗng.");

            // Trả về phần tử nằm ở đỉnh ngăn xếp phụ là giá trị nhỏ nhất của trạng thái hiện tại mà không làm thay đổi cấu trúc dữ liệu.
            return _minStack.Peek();
        }

        static void Main()
        {
            // Khởi tạo đối tượng ngăn xếp để tìm giá trị nhỏ nhất O(1).
            MinStack stack = new MinStack();

            // Khai báo mảng chứa các giá trị kiểm thử.
            int[] pushValues = { 5, 3, 7, 3, 8 };
            // Duyệt qua từng số trong mảng dữ liệu mẫu để nạp vào ngăn xếp.
            foreach (var x in pushValues)
            {
                // Đưa số hiện tại vào ngăn xếp.
                stack.Push(x);
                // In ra màn hình giá trị nhỏ nhất được cập nhật sau mỗi lượt thêm.
                Console.WriteLine($"Push({x}) -> GetMin() hiện tại = {stack.GetMin()}");
            }

            // Vòng lặp đểbrút phần tử 4 lần liên tiếp từ đỉnh ngăn xếp.
            for (int i = 1; i <= 4; i++)
            {
                // Rút một phần tử ra khỏi ngăn xếp.
                int popped = stack.Pop();
                // Hiển thị phần tử vừa lấy và giá trị nhỏ nhất còn lại để chứng minh Min được khôi phục đúng đắn.
                Console.WriteLine($"Pop() ra giá trị {popped} -> GetMin() còn lại = {stack.GetMin()}");
            }

            Console.WriteLine("GetMin và Pop trên Stack rỗng");
            // Bẫy lỗi để kiểm tra hành vi ném ngoại lệ bảo vệ hệ thống.
            try
            {
                // Lấy ra phần tử cuối cùng duy nhất còn sót lại (số 5) khiến ngăn xếp chính thức trống rỗng.
                stack.Pop();
                // Xóa trên ngăn xếp rỗng để kích hoạt điều kiện ném lỗi InvalidOperationException.
                stack.Pop();
            }
            // Hứng ngoại lệ InvalidOperationException do hàm Pop phát ra.
            catch (InvalidOperationException ex)
            {
                // In thông báo lỗi 
                Console.WriteLine($"Bắt được lỗi thành công: {ex.Message}");
            }
        }
    }
}