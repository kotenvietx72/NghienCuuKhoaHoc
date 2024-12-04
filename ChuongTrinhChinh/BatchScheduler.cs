using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuongTrinhChinh
{
    internal class BatchScheduler
    {
        public List<ClassRoom>? classrooms { get; set; }     // Danh sách các lớp 
        public float ProcessingTime { get; set; }            // Thời gian xử lí 
        public int Count_Student_Max { get; set; }           // Số lượng sinh viên tối đa có thể xử lí 1 đợt       
        /// <summary>
        /// Hàm tính thời gian trống (Nếu có) của mỗi đợt
        /// </summary>
        public float TinhTGTrong() {
            float ThoiGianTrong = 0;
            for (int i = 1; i < classrooms?.Count(); i++)
            {
                float ThoiGianXuLi = classrooms[0].TimeToGate();
                for (int j = 0; j < i; j++)
                    ThoiGianXuLi += classrooms[j].ExitTime();
                if(ThoiGianXuLi < classrooms[i].ExitTime())
                    ThoiGianTrong = classrooms[i].ExitTime() - ThoiGianXuLi;
            }
            return ThoiGianTrong;   
        }

    }
}
