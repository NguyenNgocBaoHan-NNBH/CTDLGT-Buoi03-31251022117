using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CTDLGT_Buoi03_31251022117.Bai1
{
    internal class Bai1
    {
        // Trả về -1 nếu chuỗi hợp lệ, ngược lại thì về chỉ số của vị trí lỗi đầu tiên.
        static int KiemTraDauNgoac(string chuoiDauVao)
        {
            // Lưu cặp (loại ngoặc, vị trí) vào Stack để đối chiếu với ngoặc đóng theo LIFO.
            var nganXepChoDong = new Stack<(char loaiNgoac, int viTriXuatHien)>();

            // Duyệt qua từng ký tự của chuỗi từ trái sang phải để kiểm tra tuần tự.
            for (int chiSoHienTai = 0; chiSoHienTai < chuoiDauVao.Length; chiSoHienTai++)
            {
                // Lấy ra ký tự tại vị trí hiện tại để phân loại xử lý.
                char kyTuHienTai = chuoiDauVao[chiSoHienTai];

                // Nếu là ngoặc mở, tạm lưu lại để chờ tìm ngoặc đóng tương ứng.
                if (kyTuHienTai == '(' || kyTuHienTai == '[' || kyTuHienTai == '{')
                {
                    // Đẩy dấu ngoặc mở cùng vị trí của nó vào Stack để quản lý các ngoặc chưa được đóng.
                    nganXepChoDong.Push((kyTuHienTai, chiSoHienTai));
                }
                // Nếu là ngoặc đóng, kiểm tra cặp ngoặc gần nhất.
                else if (kyTuHienTai == ')' || kyTuHienTai == ']' || kyTuHienTai == '}')
                {
                    // Stack rỗng nghĩa là có ngoặc đóng nhưng trước đó không có ngoặc mở nào chờ sẵn.
                    if (nganXepChoDong.Count == 0)
                        return chiSoHienTai;

                    // Lấy ra và xóa khỏi Stack dấu ngoặc mở nằm ở đỉnh vì nó là ngoặc mở gần nhất cần được đóng.
                    var ngoacMoGanNhat = nganXepChoDong.Pop();

                    // Nếu loại ngoặc mở vừa lấy ra không khớp cặp với ngoặc đóng hiện tại.
                    if (!LaCapNgoacHopLe(ngoacMoGanNhat.loaiNgoac, kyTuHienTai))
                        return chiSoHienTai;
                }
            }

            // Sau khi duyệt hết chuỗi, nếu Stack vẫn còn phần tử nghĩa là có dấu ngoặc mở dư.
            if (nganXepChoDong.Count > 0)
            {
                // Khởi tạo giá trị lớn nhất có thể để chuẩn bị cho thuật toán tìm vị trí nhỏ nhất (xuất hiện sớm nhất).
                int viTriLoiDauTien = int.MaxValue;

                // Duyệt qua toàn bộ các ngoặc mở còn sót lại trong Stack để tìm xem ngoặc nào đã mở ra sớm nhất mà chưa đóng.
                foreach (var ngoacChuaDong in nganXepChoDong)
                {
                    // Nếu vị trí của ngoặc hiện tại nhỏ hơn mốc đã tìm thấy, cập nhật mốc mới để lấy vị trí lỗi đầu tiên.
                    if (ngoacChuaDong.viTriXuatHien < viTriLoiDauTien)
                        viTriLoiDauTien = ngoacChuaDong.viTriXuatHien;
                }

                // Trả về vị trí của dấu ngoặc mở đầu tiên không được đóng trong chuỗi.
                return viTriLoiDauTien;
            }

            // Stack rỗng và không vi phạm các điều kiện trên nghĩa là mọi dấu ngoặc đều được đóng/mở đúng cặp.
            return -1;
        }

        // Nhận vào một ngoặc mở và một ngoặc đóng để xác định chúng có cùng một loại hay không.
        static bool LaCapNgoacHopLe(char ngoacMo, char ngoacDong)
        {
            // Trả về true nếu thuộc một trong ba cặp ngoặc tiêu chuẩn (), [], {}, ngược lại trả về false.
            return (ngoacMo == '(' && ngoacDong == ')')
                || (ngoacMo == '[' && ngoacDong == ']')
                || (ngoacMo == '{' && ngoacDong == '}');
        }

        static void Main1()
        {
            // Nhập chuỗi cần kiểm tra
            Console.Write("Nhập chuỗi cần kiểm tra: ");
            // Chuỗi đọc từ bàn phím
            string chuoiCanKiemTra = Console.ReadLine() ?? "";
            // Đo thời gian thực hiện thuật toán kiểm tra
            var stopwatch = Stopwatch.StartNew();
            int ketQuaKiemTra = KiemTraDauNgoac(chuoiCanKiemTra);
            stopwatch.Stop();
            // In kết quả và vị trí lỗi nếu chuỗi không hợp lệ
            Console.WriteLine(ketQuaKiemTra == -1 ? "HỢP LỆ" : $"SAI tại vị trí {ketQuaKiemTra}");
            Console.WriteLine($"Thời gian: {stopwatch.Elapsed.TotalMilliseconds:F3} ms");

            // Ca 9
            string input_Ca9 = new string('(', 500000) + new string(')', 500000);

            Console.WriteLine("CA 9 ");

            //// Khởi tạo đồng hồ bấm giờ
            //var stopwatch = Stopwatch.StartNew();

            //// Truyền biến 'input_Ca9' vào đây để kiểm tra dữ liệu lớn
            //int ketQuaKiemTra = KiemTraDauNgoac(input_Ca9);

            //stopwatch.Stop();

            //// In kết quả
            //Console.WriteLine(ketQuaKiemTra == -1 ? "HỢP LỆ" : $"SAI tại vị trí {ketQuaKiemTra}");
            //Console.WriteLine($"Thời gian: {stopwatch.Elapsed.TotalSeconds:F3} giây");
            //Console.WriteLine();

            // Ca 10
            string input_Ca10 = new string('(', 1000000);

            Console.WriteLine("CA 10");
            //stopwatch.Restart(); // Reset và chạy lại đồng hồ

            //int ketQuaKiemTra10 = KiemTraDauNgoac(input_Ca10);

            //stopwatch.Stop();

            //Console.WriteLine(ketQuaKiemTra10 == -1 ? "HỢP LỆ" : $"SAI tại vị trí {ketQuaKiemTra10}");
            //Console.WriteLine($"Thời gian: {stopwatch.Elapsed.TotalSeconds:F3} giây");
        }
    }
}