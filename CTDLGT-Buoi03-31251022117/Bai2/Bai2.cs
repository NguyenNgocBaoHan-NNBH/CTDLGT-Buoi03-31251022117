using System;
using System.Collections.Generic;
using System.Globalization;
using System.Diagnostics;

class Customer
{
    // Mã số định danh của từng khách hàng nhằm kiểm soát việc theo dõi thứ tự.
    public int Id { get; }

    // Thời gian khách bước vào hệ thống, dùng để tính toán thời gianchờ đợi.
    public long Arrival { get; }

    // Tổng thời lượng để nhân viên hoàn thành xử lý các yêu cầu của khách này.
    public long ServiceTime { get; }

    // Hàm khởi tạo giúp đóng gói các thông số của một khách hàng khi được nạp vào.
    public Customer(int id, long arrival, long serviceTime)
    {
        Id = id;
        Arrival = arrival;
        ServiceTime = serviceTime;
    }
}

class Bai2
{
    static void Main2()
    {
        // Ép hệ thống dùng dấu chấm (.) làm ký hiệu thập phân.
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        // Thông báo hướng dẫn người dùng nhập dữ liệu khách hàng từ bàn phím.
        Console.WriteLine("Nhập dữ liệu từng khách hàng (mỗi khách một dòng: Thời_điểm_đến Thời_gian_phục_vụ):");

        // Đọc dòng cấu hình đầu tiên từ bàn phím và tách các từ dựa trên khoảng trắng.
        string[] dongDauTien = (Console.ReadLine() ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries);

        // Số lượng khách hàng từ chuỗi nhập.
        int tongSoKhach = int.Parse(dongDauTien[0]);

        // Tổng số lượng quầy phục vụ hiện có để cấp phát kích thước cho mảng quản lý quầy.
        int tongSoQuay = int.Parse(dongDauTien[1]);

        // Hàng đợi FIFO chứa danh sách khách theo đúng trình tự xuất hiện.
        var hangChoKhachHang = new Queue<Customer>();


        // Vòng lặp chạy qua số lượng khách mong muốn để nạp dữ liệu lần lượt cho từng người.
        //for (int i = 0; i < tongSoKhach; i++)
        //{
        //    // Đọc một dòng văn bản từ bàn phím, loại bỏ khoảng trắng thừa rồi tách thành mảng các chuỗi con.
        //    string[] dongKhach = (Console.ReadLine() ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries);

        //    // Phần tử đầu tiên (vị trí số 0) đại diện cho thời điểm khách bước vào hệ thống.
        //    long thoiDiemDen = long.Parse(dongKhach[0]);

        //    // Phần tử thứ hai (vị trí số 1) đại diện cho thời gian để nhân viên phục vụ khách này.
        //    long thoiGianPhucVu = long.Parse(dongKhach[1]);

        //    // Khởi tạo đối tượng Customer mới với mã số (id = i + 1) tăng dần và đẩy vào hàng đợi FIFO.
        //    hangChoKhachHang.Enqueue(new Customer(i + 1, thoiDiemDen, thoiGianPhucVu));
        //}
        for (int i = 0; i < tongSoKhach; i++)
        {
            // Đưa khách vào hàng chờ với mã tăng dần từ 1, thời điểm đến tăng (i) và thời gian phục vụ cố định (10).
            hangChoKhachHang.Enqueue(new Customer(i + 1, i, 10));
        }

        // Mảng lưu mốc thời gian hoàn thành công việc của từng quầy; tất cả phần tử tự động bằng 0 khi khởi tạo.
        long[] thoiDiemQuayRanh = new long[tongSoQuay];

        // Biến tổng thời gian chờ của toàn bộ khách hàng.
        long tongThoiGianCho = 0;

        // Biến lưu thời gian chờ lâu nhất của một khách.
        long thoiGianChoLauNhat = 0;

        // Biến ghi nhận thời gian muộn nhất mà một quầy hoàn thành.
        long thoiDiemXongMuonNhat = 0;

        // Danh sách lưu trữ thời gian chờ của từng khách hàng.
        List<long> danhSachThoiGianCho = new List<long>();

        // Bắt đầu đồng hồ bấm giờ.
        var dongHoBamGio = Stopwatch.StartNew();

        // Vòng lặp xử lý luân phiên cho đến khi không còn khách nào trong hàng đợi FIFO.
        while (hangChoKhachHang.Count > 0)
        {
            // Lấy khách đầu hàng đợi FIFO ra xử lý và xoá khỏi danh sách chờ.
            Customer khachHang = hangChoKhachHang.Dequeue();

            // Giả định quầy số 0 hiện tại là quầy rảnh sớm nhất để so sánh.
            int quayDuocChon = 0;

            // Quét qua các quầy còn lại từ chỉ số 1 đến hết để tìm ra quầy có thời gian rảnh sớm nhất hệ thống.
            for (int j = 1; j < tongSoQuay; j++)
            {
                // Nếu quầy j rảnh sớm hơn quầy đang chọn, cập nhật lại chỉ số quầy.
                if (thoiDiemQuayRanh[j] < thoiDiemQuayRanh[quayDuocChon])
                    // Lưu lại số hiệu quầy mới tìm thấy; dấu "<" giúp ưu tiên quầy số nhỏ hơn nếu trùng thời gian rảnh.
                    quayDuocChon = j;
            }

            // Thời điểm bắt đầu dịch vụ là số lớn hơn giữa lúc khách đến và lúc quầy rảnh vì một trong hai phải đợi nhau.
            long thoiDiemBatDau = Math.Max(khachHang.Arrival, thoiDiemQuayRanh[quayDuocChon]);

            // Thời gian chờ của khách bằng thời điểm được phục vụ trừ đi thời điểm họ xuất hiện tại cửa hàng.
            long thoiGianCho = thoiDiemBatDau - khachHang.Arrival;

            // Thời điểm hoàn thành xong cho khách bằng bắt đầu cộng với thời gian phục vụ.
            long thoiDiemHoanThanh = thoiDiemBatDau + khachHang.ServiceTime;

            // Cập nhật lại trạng thái bận của quầy được chọn bằng thời gian hoàn thành của khách hiện tại.
            thoiDiemQuayRanh[quayDuocChon] = thoiDiemHoanThanh;

            // Cộng dồn thời gian chờ của khách hiện tại vào tổng thời gian chờ của toàn hệ thống.
            tongThoiGianCho += thoiGianCho;

            // Thêm thời gian chờ vừa tính được vào danh sách phục vụ.
            danhSachThoiGianCho.Add(thoiGianCho);

            // Cập nhật nếu thời gian chờ của khách này lâu hơn trước đó.
            thoiGianChoLauNhat = Math.Max(thoiGianChoLauNhat, thoiGianCho);

            // Cập nhật mốc hoàn thành cuối cùng nếu tiến độ xử lý của ca này muộn hơn các ca trước.
            thoiDiemXongMuonNhat = Math.Max(thoiDiemXongMuonNhat, thoiDiemHoanThanh);
        }

        // Dừng phép đo thời gian chạy của thuật toán.
        dongHoBamGio.Stop();

        // In kết quả thời gian của thuật toán với 3 chữ số thập phân.
        Console.WriteLine($"Thời gian chạy: {dongHoBamGio.Elapsed.TotalMilliseconds:F3} ms");

        // Tính toán chỉ số thời gian chờ trung bình bằng tổng thời gian chia cho lượng khách. Để tránh lỗi chia cho 0).
        // Khai báo biến lưu giá trị thời gian chờ trung bình của khách hàng.
        double choTrungBinh;
        // Kiểm tra nếu tổng số khách bằng 0 thì không thể thực hiện phép chia.
        if (tongSoKhach == 0)
        {
            // Không có khách nào thì thời gian chờ trung bình mặc định bằng 0.
            choTrungBinh = 0;
        }
        else
        {
            // Có khách thì lấy tổng thời gian chia cho tổng số khách. 
            choTrungBinh = (double)tongThoiGianCho / tongSoKhach;
        }

        // Chỉ hiển thị dãy thời gian chờ chi tiết nếu dữ liệu nhỏ.
        if (tongSoKhach <= 100)
            // Gộp danh sách thời gian chờ thành một chuỗi văn bản cách bằng dấu phẩy .
            Console.WriteLine($"Chờ: {string.Join(", ", danhSachThoiGianCho)}");

        // In dòng bao gồm thời gian trung bình (2 chữ số thập phân), chờ lâu nhất và thời điểm xong.
        Console.WriteLine($"TB {choTrungBinh:F2}; lâu nhất {thoiGianChoLauNhat}; xong lúc {thoiDiemXongMuonNhat}");

        Console.ReadLine();
    }
}
