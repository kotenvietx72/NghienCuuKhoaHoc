﻿using System;
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
        /// Hàm tính thời gian trống (Nếu có) của mỗi đợt
        /// </summary>
        public float TinhTGTrong()
        {
            return 0;
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
    }
}
