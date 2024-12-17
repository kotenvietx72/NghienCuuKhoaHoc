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
        public int Count_Student_Max { get; set; }           // Số lượng sinh viên tối đa có thể xử lí 1 đợt
        
        /// <summary>
        /// Hàm tính tổng số sinh viên của n lớp trong classrooms, n được truyền vào biến số  
        /// </summary>
        /// <param name="room"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        // Done
        public float TinhTongSoSVYC(int a)
        {
            if(a == 0)
                return 0;
            return classrooms[a - 1].ActualVehicalCount() + TinhTongSoSVYC(a - 1);
        }
        
        /// <summary>
        /// Hàm tính thời gian trống của n lớp trong 1 đợt (Nếu có) 
        /// </summary>
        // Done
        public float TinhTGTrong(ClassInformation classInformation, int a)
        {
            float ThoiGianTrong = 0;
            for (int i = 1; i < a; i++)
            {
                if ((classrooms[i].TimeToGate(classInformation) - classrooms[0].TimeToGate(classInformation)) < TinhTongSoSVYC(i) + ThoiGianTrong)
                    ThoiGianTrong += 0;
                else
                    ThoiGianTrong += (classrooms[i].TimeToGate(classInformation) - classrooms[0].TimeToGate(classInformation)) - TinhTongSoSVYC(i) - ThoiGianTrong;
            }
            return ThoiGianTrong;
        }
        public BatchScheduler()
        {
            classrooms = new List<ClassRoom>();
            Count_Student_Max = 0;
        }

        /// <summary>
        /// Hàm sao chép thông tin từ 1 BatchScheduler vào 1 BatchScheduler khác
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
                Count_Student_Max = this.Count_Student_Max,
            };
        }

        /// <summary>
        /// Hàm tính thời gian xử lí mỗi đợt
        /// </summary> 
        // Done
        public float ProcessingTime(ClassInformation classInformation) {
            float ThoiGianXuLi = 0;
            foreach(var classroom in classrooms)
                ThoiGianXuLi += classroom.ActualVehicalCount() * classInformation.StudentProcessingTime;   
            return ThoiGianXuLi;
        }

        /// <summary>
        /// Hàm tính thời gian chờ của 1 đợt (Dùng để xét nhánh cận) 
        /// </summary>
        /// <param name="classInformation"></param>
        /// <returns></returns>
        // Done
        public float WaitTime(ClassInformation classInformation)
        {
            float ThoiGianCho = 0;
            for(int i = 0; i < classrooms.Count; i++)
            {
                if (TinhTongSoSVYC(i) + TinhTGTrong(classInformation, i) > (classrooms[i].TimeToGate(classInformation) - classrooms[0].TimeToGate(classInformation)))
                    ThoiGianCho += TinhTongSoSVYC(i) + TinhTGTrong(classInformation, i) - (classrooms[i].TimeToGate(classInformation) - classrooms[0].TimeToGate(classInformation));
                else
                    ThoiGianCho += 0;
            }    
            return ThoiGianCho;
        }
    }
}
