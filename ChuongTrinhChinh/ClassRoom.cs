using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuongTrinhChinh
{
    internal class ClassRoom
    {
        public string? ClassID {  get; set; }      // Id lớp (Tự đặt)
        public string? NameSubject { get; set; }   // Tên môn học
        public string? ClassName { get; set; }     // Tên lớp
        public string? Session {  get; set; }      // Số tiết lấy từ sql
        public int StudentCount { get; set; }      // Số lượng sinh viên
        public string? Room {  get; set; }         // Phòng học
        public bool check {  get; set; }           // Đánh dấu đã được xử lí chưa

        /// <summary>
        /// Hàm tính thời gian đi từ lớp tới cổng soát vé 
        /// </summary>
        public float TimeToGate() {
            return 1;
        }
        /// <summary>
        /// Hàm tính thời gian 1 lớp đi ra hết khỏi cổng soát vé 
        /// </summary>
        public float ExitTime() {
            return 0;
        }
        /// <summary>
        /// Hàm tính số lượng xe thực tế của 1 lớp 
        /// </summary>
        public int ActualVehicalCount() {
            return 0;
        }
        /// <summary>
        /// Hàm lấy số tiết học của 1 lớp 
        /// </summary>
        public int GetSessionCount() {
            return 0;
        }
    }
}
