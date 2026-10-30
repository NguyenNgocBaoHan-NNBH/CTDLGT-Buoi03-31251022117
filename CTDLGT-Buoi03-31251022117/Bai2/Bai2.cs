// Định nghĩa cấu trúc bản ghi lưu trữ đầy đủ thông tin của một khách hàng bao gồm: Số thứ tự, Thời điểm đến, Thời gian dịch vụ.
using System.Diagnostics;
using System.Globalization;

public record KhachHang(int MaSo, long ThoiDiemDen, long ThoiGianPhucVu);

class Program
{
    static void Main(string[] args)
    {
        // Ép buộc hệ thống sử dụng định dạng chuẩn quốc tế (dấu chấm thập phân) để hiển thị số thực đồng nhất trên mọi máy tính.
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

        Console.WriteLine("=== MÔ PHỎNG QUẦY DỊCH VỤ ===");
        Console.Write("Nhap so khach hang (n) va so quay (k), cach nhau bang khoang trang: ");

        // Đọc dòng đầu tiên chứa n và k, sau đó cắt chuỗi dựa trên khoảng trắng hoặc ký tự tab để lấy ra các tham số.
        string[] dongDauTien = Console.ReadLine().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        // Phòng ngừa lỗi: Nếu người dùng nhập thiếu dữ liệu đầu vào thì dừng chương trình lập tức để tránh lỗi sập hệ thống.
        if (dongDauTien.Length < 2) return;

        // Chuyển đổi chuỗi text nhận được thành số nguyên để lấy số lượng khách hàng cần phục vụ.
        int tongSoKhach = int.Parse(dongDauTien[0]);
        // Chuyển đổi chuỗi text nhận được thành số nguyên để lấy tổng số quầy dịch vụ đang hoạt động.
        int tongSoQuay = int.Parse(dongDauTien[1]);

        // Khởi tạo một cấu trúc dữ liệu Hàng đợi (FIFO) nhằm đảm bảo nguyên tắc phục vụ: Khách hàng nào đến trước sẽ được xử lý trước.
        Queue<KhachHang> hangChoKhachHang = new Queue<KhachHang>();

        Console.WriteLine($"\nNhap {tongSoKhach} dong tiếp theo (moi dong gom: thoi diem den 'a' va thoi gian phuc vu 's'):");
        // Tiến hành vòng lặp để thu thập thông tin đầu vào chi tiết cho từng khách hàng theo đúng số lượng đã khai báo.
        for (int i = 1; i <= tongSoKhach; i++)
        {
            Console.Write($"Khach hang {i}: ");
            // Đọc dòng dữ liệu chứa thời điểm đến và thời gian phục vụ của khách hàng thứ i.
            string[] dongDuLieu = Console.ReadLine().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            // Trích xuất mốc thời gian khách đến (kiểu long để tránh tràn số khi dữ liệu mốc thời gian lên đến 10^9).
            long thoiDiemDen_A = long.Parse(dongDuLieu[0]);
            // Trích xuất thời gian quầy cần để phục vụ xong cho vị khách này.
            long thoiGianPhucVu_S = long.Parse(dongDuLieu[1]);
            // Đẩy vị khách vừa khởi tạo vào cuối hàng đợi để chờ đến lượt phục vụ theo đúng trình tự thời gian.
            hangChoKhachHang.Enqueue(new KhachHang(i, thoiDiemDen_A, thoiGianPhucVu_S));
        }

        // Kích hoạt đồng hồ bấm giờ độ chính xác cao nhằm theo dõi sát sao thời gian xử lý lõi của thuật toán mô phỏng quầy.
        Stopwatch dongHoBamGio = Stopwatch.StartNew();

        // Mảng lưu mốc thời gian bắt đầu rảnh của từng quầy, ban đầu tất cả các quầy đều sẵn sàng phục vụ tại mốc 0.
        long[] thoiDiemRanhCuaQuay = new long[tongSoQuay];
        // Biến tích lũy tổng thời gian chờ của tất cả khách hàng, dùng kiểu long để phòng ngừa lỗi tràn số khi cộng dồn dữ liệu lớn.
        long tongThoiGianCho = 0;
        // Biến lưu vết thời gian chờ đợi lớn nhất được phát hiện trong suốt quá trình mô phỏng hệ thống.
        long thoiGianChoLauNhat = 0;
        // Biến theo dõi mốc thời gian muộn nhất mà toàn bộ các quầy hoàn thành xong mọi công việc được giao.
        long thoiDiemHoanThanhToanBo = 0;

        Console.WriteLine("\n--- KET QUA CHI TIET ---");
        // Vòng lặp liên tục rút từng vị khách ra xử lý cho đến khi không còn ai đứng chờ trong hệ thống hàng đợi.
        while (hangChoKhachHang.Count > 0)
        {
            // Lấy vị khách đứng đầu hàng đợi ra phục vụ và xóa họ khỏi danh sách hàng chờ (Nguyên lý FIFO).
            KhachHang khach = hangChoKhachHang.Dequeue();

            // Giả định quầy số 0 (chỉ số 0) hiện tại là quầy có thời điểm rảnh sớm nhất để làm mốc so sánh.
            int quayDuocChon = 0;
            // Lấy mốc thời gian rảnh của quầy số 0 để chuẩn bị thực hiện vòng lặp tìm giá trị nhỏ nhất (Min).
            long thoiDiemRanhNhoNhat = thoiDiemRanhCuaQuay[0];

            // Quét qua các quầy còn lại từ chỉ số 1 đến k-1 để tìm ra quầy rảnh sớm nhất, hoặc chọn quầy có số thứ tự nhỏ hơn nếu cùng rảnh.
            for (int q = 1; q < tongSoQuay; q++)
            {
                // Nếu phát hiện một quầy rảnh sớm hơn mốc đang lưu, lập tức cập nhật lại mốc thời gian rảnh nhỏ nhất và số hiệu quầy mới.
                if (thoiDiemRanhCuaQuay[q] < thoiDiemRanhNhoNhat)
                {
                    thoiDiemRanhNhoNhat = thoiDiemRanhCuaQuay[q];
                    quayDuocChon = q;
                }
            }

            // Thời điểm bắt đầu phục vụ là giá trị lớn hơn giữa mốc quầy bắt đầu rảnh và mốc khách hàng bước chân đến tiệm.
            long thoiDiemBatDau = Math.Max(thoiDiemRanhNhoNhat, khach.ThoiDiemDen);
            // Thời gian khách phải chờ bằng thời điểm họ thực sự được phục vụ trừ đi thời điểm họ xuất hiện.
            long thoiGianCho = thoiDiemBatDau - khach.ThoiDiemDen;
            // Thời điểm kết thúc phục vụ bằng thời điểm bắt đầu cộng thêm khoảng thời gian nhân viên xử lý dịch vụ.
            long thoiDiemXong = thoiDiemBatDau + khach.ThoiGianPhucVu;

            // Cập nhật lại trạng thái của quầy được chọn: Quầy này sẽ bận liên tục cho đến mốc thời gian kết thúc của vị khách hiện tại.
            thoiDiemRanhCuaQuay[quayDuocChon] = thoiDiemXong;

            // Cộng dồn thời gian chờ của vị khách này vào tổng quỹ thời gian chờ toàn hệ thống để tính trung bình ở cuối bài.
            tongThoiGianCho += thoiGianCho;
            // Cập nhật kỷ lục thời gian chờ lâu nhất nếu thời gian chờ của khách hiện tại lớn hơn giá trị đỉnh trước đó.
            if (thoiGianCho > thoiGianChoLauNhat) thoiGianChoLauNhat = thoiGianCho;
            // Cập nhật mốc hoàn thành toàn bộ hệ thống bằng mốc kết thúc muộn nhất được ghi nhận qua các lượt phục vụ.
            if (thoiDiemXong > thoiDiemHoanThanhToanBo) thoiDiemHoanThanhToanBo = thoiDiemXong;

            // Chốt chặn hiệu năng: Chỉ in chi tiết nếu số khách <= 100 nhằm tránh nghẽn I/O Console ở ca lớn 100k dòng, giúp code đạt tốc độ < 1 giây.
            if (tongSoKhach <= 100)
            {
                // In kết quả chi tiết từng dòng, số hiệu quầy cộng thêm 1 vì chỉ số mảng chạy từ 0 nhưng hiển thị quầy chạy từ 1.
                Console.WriteLine($"KH {khach.MaSo}: den {khach.ThoiDiemDen}, quay {quayDuocChon + 1}, bat dau {thoiDiemBatDau}, cho {thoiGianCho}, xong {thoiDiemXong}");
            }
        }

        // Ngắt đồng hồ đo hiệu năng ngay sau khi hệ thống xử lý hoàn tất cho vị khách cuối cùng.
        dongHoBamGio.Stop();

        // Tính toán thời gian chờ đợi trung bình của một khách hàng bằng cách lấy tổng thời gian chia cho tổng số lượng người.
        double choTrungBinh = (double)tongThoiGianCho / tongSoKhach;
        Console.WriteLine("\n--- THONG KE TONG HOP ---");
        // Hiển thị thời gian chờ trung bình với định dạng chính xác 2 chữ số thập phân dựa trên thiết lập InvariantCulture.
        Console.WriteLine($"Cho trung binh: {choTrungBinh:F2}");
        Console.WriteLine($"Cho lau nhat: {thoiGianChoLauNhat}");
        Console.WriteLine($"Hoan thanh luc: {thoiDiemHoanThanhToanBo}");

        // Hiển thị thời gian thực thi thực tế tính bằng giây để phục vụ việc làm báo cáo thực nghiệm các ca dữ liệu lớn.
        Console.WriteLine($"Thoi gian chay: {dongHoBamGio.Elapsed.TotalSeconds:F4} giay");
    }
}