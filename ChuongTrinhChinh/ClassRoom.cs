﻿using System;
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

        public ClassRoom()
        {
            ClassID = null;
            NameSubject = null;
            ClassName = null;
            Session = null;
            StudentCount = 0;
            Room = null;
            check = false;
        }

        /// <summary>
        /// Hàm tính thời gian đi từ lớp tới cổng soát vé 
        /// </summary>
        // Done
        public float TimeToGate(ClassInformation classInformation) {
            if(Room?.Substring(2,2) == "A8")
            {
                int SoTang = int.Parse(Room.Substring(5,1));
                int Index = int.Parse(Room.Substring(6, 2)) switch
                {
                    01 or 02 or 10 => 0,
                    03 or 04 or 08 or 09 => 1,
                    05 or 06 or 07 => 2,
                    _ => 0
                };
                return 60 + 5 * Index + (SoTang - 1) * classInformation.H8timing.Time_Stair_1Floor + classInformation.H8timing.Time_Stair_To_Parking + classInformation.H8timing.Time_Parking_To_Gate;
            }
            else if(Room?.Substring(2, 2) == "A9")
            {
                int SoTang = int.Parse(Room.Substring(5, 1));
                int Index = int.Parse(Room.Substring(6, 2)) switch
                {
                    01 or 02 or 10 => 0,
                    03 or 04 or 08 or 09 => 1,
                    05 or 06 or 07 => 2,
                    _ => 0
                };
                return 60 + 5 * Index + (SoTang - 1) * classInformation.H9timing.Time_Stair_1Floor + classInformation.H9timing.Time_Stair_To_Parking + classInformation.H9timing.Time_Parking_To_Gate;
            }
            else if(Room?.Substring(2, 3) == "A10")
            {
                if(Room.Length == 10)
                {
                    int SoTang = int.Parse(Room.Substring(6, 2));
                    int Index = int.Parse(Room.Substring(8, 2)) switch
                    {
                        05 or 06 or 07 => 0,
                        03 or 04 or 08 or 09 => 1,
                        01 or 02 or 10 or 11 => 2,
                        _ => 0
                    };
                    return 60 + 180 + 5 * Index + (SoTang - 1) * classInformation.H10timing.Time_Elavator_1Floor + classInformation.H10timing.Time_Stair_To_Parking + classInformation.H10timing.Time_Parking_To_Gate;
                }  
                else if(Room.Length == 9 && int.Parse(Room.Substring(6, 1)) > 6 )
                {
                    int SoTang = int.Parse(Room.Substring(6, 1));
                    int Index = int.Parse(Room.Substring(7, 2)) switch
                    {
                        05 or 06 or 07 => 0,
                        03 or 04 or 08 or 09 => 1,
                        01 or 02 or 10 or 11 => 2,
                        _ => 0
                    };
                    return 60 + 180 + 5 * Index + (SoTang - 1) * classInformation.H10timing.Time_Elavator_1Floor + classInformation.H10timing.Time_Stair_To_Parking + classInformation.H10timing.Time_Parking_To_Gate;
                }
                else
                {
                    int SoTang = int.Parse(Room.Substring(6, 1));
                    int Index = int.Parse(Room.Substring(7, 2)) switch
                    {
                        01 or 02 or 05 or 06 or 07 => 0,
                        03 or 04 or 08 or 09 => 1,
                        10 or 11 => 2,
                        _ => 0
                    };
                    return 60 + 5 * Index + (SoTang - 1) * classInformation.H10timing.Time_Stair_1Floor + classInformation.H10timing.Time_Stair_To_Parking + classInformation.H10timing.Time_Parking_To_Gate;
                }
            }    
            else { return 0; }
        }

        /// <summary>
        /// Hàm tính thời gian 1 lớp đi ra hết khỏi cổng soát vé 
        /// </summary>
        // Done
        public float ExitTime(ClassInformation classInformation) {
            return ActualVehicalCount() * classInformation.StudentProcessingTime / classInformation.GateCount;
        }

        /// <summary>
        /// Hàm tính số lượng xe thực tế của 1 lớp 
        /// </summary>
        // Done
        public int ActualVehicalCount() {
            return (int)Math.Ceiling(StudentCount * 0.93);
        }

        /// <summary>
        /// Hàm lấy số tiết học của 1 lớp 
        /// </summary>
        // Done
        public int GetSessionCount() {
            return Session switch
            {
                "1->6" => 6,
                "2->6" => 5,
                "3->6" => 4,
                "4->6" => 3,
                "5->6" => 2,
                _ => 0
            };
        }
    }
}
