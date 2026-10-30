using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CTDLGT_Buoi03_31251022117.Bai1
{
    internal class Bai1
    {
        static void Main()
        {
            // Thiết lập bảng mã UTF-8 để màn hình Console hiển thị đúng các ký tự tiếng Việt có dấu.
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Duy trì vòng lặp vô hạn giúp người dùng thực hiện kiểm thử nhiều ca liên tục mà không cần chạy lại chương trình.
            while (true)
            {
                Console.WriteLine("1. Tự nhập chuỗi ký tự");
                Console.WriteLine("2. Chạy tự động Ca lớn 9 (500k cặp ngoặc)");
                Console.WriteLine("3. Chạy tự động Ca lớn 10 (1 triệu ngoặc mở)");
                Console.WriteLine("4. Thoát chương trình");
                Console.Write("Nhập lựa chọn của bạn (1-4): ");

                // Đọc lựa chọn từ bàn phím, nếu null thì thay bằng chuỗi rỗng nhằm tránh lỗi sập chương trình.
                string luaChon = Console.ReadLine() ?? "";

                // Xử lý kịch bản 1: Người dùng tự nhập dữ liệu kiểm thử thủ công từ bàn phím.
                if (luaChon == "1")
                {
                    Console.Write("Nhập chuỗi ký tự cần kiểm tra: ");
                    // Đọc toàn bộ chuỗi ký tự do người dùng nhập để chuẩn bị đưa vào hàm kiểm tra cấu trúc ngoặc.
                    string chuoiNhap = Console.ReadLine() ?? "";

                    // Gọi hàm thuật toán xử lý chuỗi vừa nhập và hứng chuỗi thông báo kết quả trả về.
                    string ketQuaNguoiDung = KiemTraNgoacCanBang(chuoiNhap);
                    // In kết quả kiểm tra lên màn hình cho người dùng đối chiếu tính đúng đắn.
                    Console.WriteLine($"-> Kết quả: {ketQuaNguoiDung}");
                }
                // Xử lý kịch bản 2: Đo hiệu năng tự động với chuỗi lớn gồm 1 triệu ký tự đối xứng hoàn hảo.
                else if (luaChon == "2")
                {
                    Console.WriteLine("Đang sinh chuỗi và kiểm thử Ca 9, vui lòng đợi...");
                    // Kích hoạt thu gom rác chủ động và đợi hoàn tất để giải phóng bộ nhớ, tránh sai số do tài nguyên tồn dư trước khi đo.
                    GC.Collect(); GC.WaitForPendingFinalizers();

                    // Khởi động bộ đếm thời gian với độ chính xác cao ngay trước khi cấp phát dữ liệu và thực thi thuật toán.
                    Stopwatch boDoThoiGian9 = Stopwatch.StartNew();
                    // Tạo một chuỗi lớn bằng cách ghép liên tiếp nửa triệu ngoặc mở và nửa triệu ngoặc đóng để thử nghiệm ca thông thường quy mô lớn.
                    string chuoiCa9 = new string('(', 500000) + new string(')', 500000);
                    // Thực thi thuật toán trên chuỗi cực đại vừa sinh để kiểm tra xem có vượt quá giới hạn thời gian chạy hay không.
                    string ketQuaCa9 = KiemTraNgoacCanBang(chuoiCa9);
                    // Dừng bộ đếm thời gian ngay khi hàm thuật toán hoàn thành việc xử lý dữ liệu.
                    boDoThoiGian9.Stop();

                    Console.WriteLine($"-> Kết quả Ca 9: {ketQuaCa9}");
                    // In tổng thời gian chạy thực tế dưới dạng số thực lấy 4 chữ số thập phân để sinh viên điền vào bảng thực nghiệm.
                    Console.WriteLine($"-> Thời gian chạy: {boDoThoiGian9.Elapsed.TotalSeconds:F4} giây");
                }
                // Xử lý kịch bản 3: Đo hiệu năng tự động với chuỗi lỗi nặng nhất làm đẩy Stack lên độ sâu tối đa.
                else if (luaChon == "3")
                {
                    Console.WriteLine("Đang sinh chuỗi và kiểm thử Ca 10, vui lòng đợi...");
                    // Thu dọn toàn bộ các đối tượng rác trong bộ nhớ HEAP nhằm đảm bảo tính công bằng và chính xác cho phép đo thời gian tiếp theo.
                    GC.Collect(); GC.WaitForPendingFinalizers();

                    // Bắt đầu kích hoạt đồng hồ bấm giờ hệ thống để theo dõi sát sao thời gian phản hồi của ca kiểm thử biên.
                    Stopwatch boDoThoiGian10 = Stopwatch.StartNew();
                    // Sinh chuỗi chứa đúng 1 triệu dấu ngoặc mở nhằm ép Stack liên tục Push phần tử mà không hề Pop, đẩy dung lượng Stack lên cực hạn.
                    string chuoiCa10 = new string('(', 1000000);
                    // Chạy hàm kiểm tra trên chuỗi 1 triệu ngoặc mở để xác định khả năng chịu tải và tốc độ quét của Stack.
                    string ketQuaCa10 = KiemTraNgoacCanBang(chuoiCa10);
                    // Ngắt bộ đo thời gian ngay sau khi hàm kiểm tra hoàn tất việc rút kết luận và trả về kết quả.
                    boDoThoiGian10.Stop();

                    Console.WriteLine($"-> Kết quả Ca 10: {ketQuaCa10}");
                    // Định dạng và hiển thị thời gian chạy tính bằng giây với 4 chữ số thập phân để chứng minh tốc độ xử lý đạt dưới 1 giây.
                    Console.WriteLine($"-> Thời gian chạy: {boDoThoiGian10.Elapsed.TotalSeconds:F4} giây");
                }
                // Xử lý kịch bản 4: Người dùng kết thúc phiên làm việc.
                else if (luaChon == "4")
                {
                    Console.WriteLine("Đã thoát chương trình. Tạm biệt!");
                    // Phá vỡ vòng lặp vô hạn `while(true)` giúp luồng chính của ứng dụng dừng lại và đóng cửa sổ Console một cách an toàn.
                    break;
                }
                // Xử lý tình huống lỗi: Người dùng gõ sai ký tự điều hướng menu.
                else
                {
                    Console.WriteLine("Lựa chọn không hợp lệ! Vui lòng chọn lại từ 1 đến 4.");
                }
            }
        }

        // Định nghĩa hàm kiểm tra tính cân bằng cấu trúc ngoặc đạt độ phức tạp thời gian tuyến tính Θ(n).
        static string KiemTraNgoacCanBang(string chuoiVao)
        {
            // Khởi tạo một Stack lưu Tuple gồm ký tự và vị trí ban đầu của nó nhằm giữ lại vết để báo lỗi chính xác khi thiếu ngoặc đóng tương ứng.
            Stack<(char kyTu, int viTri)> nganXep = new Stack<(char kyTu, int viTri)>();

            // Duyệt tuần tự từng ký tự trong chuỗi từ trái qua phải theo chỉ số tăng dần để thực hiện bộ lọc phân loại ngoặc.
            for (int i = 0; i < chuoiVao.Length; i++)
            {
                // Trích xuất ký tự tại vị trí chỉ số hiện tại để giảm bớt chi phí truy xuất mảng nhiều lần trong các lệnh rẽ nhánh phía dưới.
                char kyTuHienTai = chuoiVao[i];

                // Áp dụng cơ chế LIFO: Ngoặc mở nào xuất hiện sau cùng sẽ nằm trên đỉnh Stack để chờ được khớp với ngoặc đóng xuất hiện đầu tiên.
                if (kyTuHienTai == '(' || kyTuHienTai == '[' || kyTuHienTai == '{')
                {
                    // Đẩy cặp dữ liệu gồm ký tự ngoặc mở và vị trí `i` vào đỉnh ngăn xếp nhằm làm cơ sở đối chiếu loại và vị trí với ngoặc đóng sau này.
                    nganXep.Push((kyTuHienTai, i));
                }
                // Xử lý tình huống khi con trỏ duyệt trúng một trong các loại ký tự thuộc nhóm ngoặc đóng.
                else if (kyTuHienTai == ')' || kyTuHienTai == ']' || kyTuHienTai == '}')
                {
                    // LỖI ĐÓNG THỪA: Gặp ngoặc đóng nhưng Stack trống rỗng, chứng tỏ không có bất kỳ ngoặc mở nào phía trước để ghép cặp.
                    if (nganXep.Count == 0)
                    {
                        // Trả về ngay chuỗi thông báo lỗi kèm vị trí chỉ số `i` hiện tại và lập tức ngắt hàm, không cần duyệt tiếp phần chuỗi còn lại.
                        return $"SAI tai vi tri {i}";
                    }

                    // Truy xuất thông tin của dấu ngoặc mở nằm ở đỉnh ngăn xếp để kiểm tra tính tương thích loại mà không xóa phần tử đó ra khỏi Stack.
                    var dinh = nganXep.Peek();

                    // Tiến hành đối chiếu xem ký tự ngoặc đóng hiện tại có tạo thành một cặp đồng dạng, hợp lệ với dấu ngoặc mở ở đỉnh Stack hay không.
                    if ((kyTuHienTai == ')' && dinh.kyTu == '(') ||
                        (kyTuHienTai == ']' && dinh.kyTu == '[') ||
                        (kyTuHienTai == '}' && dinh.kyTu == '{'))
                    {
                        // Loại bỏ dấu ngoặc mở ra khỏi ngăn xếp vì nó đã tìm thấy đúng ngoặc đóng tương ứng, giúp thu hẹp phạm vi kiểm tra các cặp ngoặc bên ngoài.
                        nganXep.Pop();
                    }
                    // Xử lý tình huống hai dấu ngoặc không cùng chủng loại (ví dụ dấu đóng đan xen sai quy tắc).
                    else
                    {
                        // LỖI ĐÓNG SAI LOẠI: Ngoặc đóng hiện tại phá vỡ cấu trúc lồng nhau hợp lệ, trả về vị trí lỗi tại chính vị trí ngoặc đóng sai này.
                        return $"SAI tai vi tri {i}";
                    }
                }
            }

            // LỖI MỞ CHƯA ĐÓNG: Vòng lặp duyệt hết toàn bộ chuỗi ký tự nhưng kích thước Stack vẫn lớn hơn 0, nghĩa là tồn tại ngoặc mở không được đóng.
            if (nganXep.Count > 0)
            {
                // Khởi tạo biến lưu vết vị trí với giá trị mặc định để chuẩn bị hứng dữ liệu từ các phần tử được rút ra từ ngăn xếp.
                int viTriSomNhat = -1;
                // Thực hiện rút dần tất cả phần tử trong Stack từ đỉnh xuống đáy nhằm tìm ra dấu ngoặc mở nằm ở vị trí sâu nhất (đáy Stack).
                while (nganXep.Count > 0)
                {
                    // Liên tục cập nhật vị trí của phần tử vừa Pop, phần tử cuối cùng được lấy ra từ đáy chính là dấu ngoặc mở xuất hiện sớm nhất trong chuỗi.
                    viTriSomNhat = nganXep.Pop().viTri;
                }
                // Trả về chuỗi thông báo lỗi chỉ định chính xác vị trí của dấu ngoặc mở xuất hiện sớm nhất không được đóng theo đúng yêu cầu đề bài.
                return $"SAI tai vi tri {viTriSomNhat}";
            }
            // Chuỗi vượt qua mọi chốt chặn kiểm tra lỗi, chứng tỏ tất cả các ngoặc đều được mở và đóng hoàn toàn khớp cặp, đúng thứ tự quy định.
            return "HOP LE";
        }
    }
}