using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuongTrinhChinh
{
    internal class BatchScheduler
    {
        public List<ClassRoom> classrooms { get; set; } = new List<ClassRoom>();                    // Danh sách các lớp 
        public DateTime DismissalTimeBatch { get; set; } = new DateTime(2025, 1, 1, 11, 35, 0);     // Thời gian tan học của một đợt

        private ClassInformation classInformation = XuLiDuLieu.readInforFromFile();

        /// <summary>
        /// Hàm sao chép thông tin từ đợt này vào đợt khác        
        /// </summary>
        /// <returns></returns>
        // Done
        public BatchScheduler DeepCopy()
        {
            return new BatchScheduler
            {
                classrooms = this.classrooms != null ? this.classrooms.Select(lop => new ClassRoom
                {
                    ClassID = lop.ClassID,
                    NameSubject = lop.NameSubject,
                    ClassName = lop.ClassName,
                    Session = lop.Session,
                    StudentCount = lop.StudentCount,
                    Room = lop.Room,
                    check = lop.check,

                }).ToList() : new List<ClassRoom>(),
            };
        }

        /// <summary>
        /// Hàm tính thời gian xử lí của i lớp đầu tiên trong list các lớp 
        /// </summary>
        /// <param name="room"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        // Done
        public double TotalTimeForFirst(int i)
        {
            classrooms = classrooms.OrderBy(x => x.TimeToGate()).ToList();
            if (i == 0) 
                return 0;
            return classrooms[i - 1].ExitTime() + TotalTimeForFirst(i - 1);
        }
        
        /// <summary>
        /// Hàm tính thời gian trống của a lớp đầu tiên trong đợt (Nếu có) 
        /// </summary>
        // Done
        public double TinhTGTrong(int a)
        {
            classrooms = classrooms.OrderBy(x => x.TimeToGate()).ToList();
            double ThoiGianTrong = 0;
            for (int i = 1; i < a; i++)
            {
                if ((classrooms[i].TimeToGate() - classrooms[0].TimeToGate()) < TotalTimeForFirst(i) + ThoiGianTrong)
                    ThoiGianTrong += 0;
                else
                    ThoiGianTrong += (classrooms[i].TimeToGate() - classrooms[0].TimeToGate()) - TotalTimeForFirst(i) - ThoiGianTrong;
            }
            return ThoiGianTrong;
        }

        /// <summary>
        /// Hàm tính thời gian xử lí mỗi đợt
        /// </summary> 
        // Done
        public double ProcessingTime() {
            double ThoiGianXuLi = 0;
            classrooms = classrooms.OrderBy(x => x.TimeToGate()).ToList();
            foreach (var classroom in classrooms)
                ThoiGianXuLi += classroom.ExitTime();
            return ThoiGianXuLi + TinhTGTrong(classrooms.Count);
        }

        /// <summary>
        /// Hàm tính thời gian chờ của 1 đợt
        /// </summary>
        /// <param name="classInformation"></param>
        /// <returns></returns>
        // Done
        public double WaitTime()
        {
            classrooms = classrooms.OrderBy(x => x.TimeToGate()).ToList();
            double ThoiGianCho = 0;
            for(int i = 1; i < classrooms.Count; i++)
            {
                if (TotalTimeForFirst(i) + TinhTGTrong(i) > (classrooms[i].TimeToGate() - classrooms[0].TimeToGate()))
                    ThoiGianCho += TotalTimeForFirst(i) + TinhTGTrong(i) - (classrooms[i].TimeToGate() - classrooms[0].TimeToGate());
                else
                    ThoiGianCho += 0;
            }    
            return Math.Round(ThoiGianCho, 2);
        }

        /// <summary>
        /// Hàm tính tổng số sinh viên trong 1 đợt
        /// </summary>
        /// <param name="classrooms"></param>
        /// <returns></returns>
        public int Count_Student()
        {
            if(classrooms.Count == 0) 
                return 0;
            return classrooms.Sum(c => c.StudentCount);
        }
    }
}
