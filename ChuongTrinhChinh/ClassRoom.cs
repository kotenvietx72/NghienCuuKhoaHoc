using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuongTrinhChinh
{
    internal class ClassRoom
    {
        private string? ClassID {  get; set; }      // Id lớp (Tự đặt)
        private string? ClassName { get; set; }     // Tên lớp
        private string? Session {  get; set; }      // Số tiết lấy từ sql
        private int StudentCount { get; set; }      // Số lượng sinh viên
        private string? Room {  get; set; }         // Phòng học
        private bool check {  get; set; }           // Đánh dấu đã được xử lí chưa

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
