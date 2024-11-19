using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuongTrinhChinh
{
    internal class XuLiDuLieu
    {
        /// <summary>
        /// Đọc file dữ liệu lớp học
        /// </summary>
        /// <param name="filePath"></param> 
        public static void readClassFromFile(string filePath, List<ClassRoom> classRooms)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] columns = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        if (columns.Length == 5)
                        {
                            ClassRoom a = new ClassRoom();
                            a.NameSubject = columns[0];
                            a.ClassName = columns[1];
                            a.Session = columns[2];
                            a.StudentCount = int.Parse(columns[3]);
                            a.Room = columns[4];
                            classRooms.Add(a);
                        }
                        else
                        {
                            Console.WriteLine("Không đúng đầu vào dữ liệu");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đọc file dữ liệu bị lỗi");
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Đọc file dữ liệu thông tin của trường
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="classInformation"></param>
        public static void readInforFromFile(string filePath, ClassInformation classInformation)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] elements = lines[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (i == 0)
                    {
                        classInformation.GateCount = int.Parse(elements[0]);
                        classInformation.StudentProcessingTime = float.Parse(elements[1]);
                    }
                    if (i == 1)
                    {
                        classInformation.H8timing.Time_Stair_1Floor = float.Parse(elements[0]);
                        classInformation.H8timing.Time_Stair_To_Parking = float.Parse(elements[1]);
                        classInformation.H8timing.Time_Parking_To_Gate = float.Parse(elements[2]);
                    }
                    if (i == 2)
                    {
                        classInformation.H9timing.Time_Stair_1Floor = float.Parse(elements[0]);
                        classInformation.H9timing.Time_Stair_To_Parking = float.Parse(elements[1]);
                        classInformation.H9timing.Time_Parking_To_Gate = float.Parse(elements[2]);
                    }
                    if (i == 3)
                    {
                        classInformation.H10timing.Time_Elavator_1Floor = float.Parse(elements[0]);
                        classInformation.H10timing.Time_Stair_1Floor = float.Parse(elements[1]);
                        classInformation.H10timing.Time_Stair_To_Parking = float.Parse(elements[2]);
                        classInformation.H10timing.Time_Parking_To_Gate = float.Parse(elements[3]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đọc file dữ liệu bị lỗi");
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Hàm kiểm tra danh sách các lớp đã được xử lí chưa ?
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static bool Check(List<ClassRoom> a)
        {
            foreach(ClassRoom b in a)
            {
                if(b.check == false) 
                    return false;
            }    
            return true;
        }    

        /// <summary>
        /// Hàm chia các lớp thành các đợt
        /// </summary>
        public static void ChiaLop(List<BatchScheduler> a, List<ClassRoom> b) {
            while(Check(b) == false)
            {
                BatchScheduler x = new BatchScheduler();

            }    
        }
 
        /// <summary>
        /// Hàm tính thời gian xử lí mỗi đợt
        /// </summary>
        public static void TinhTGXuLiMoiDot() { }
        /// <summary>
        /// Hàm tính best time để đưa ra lộ trình các đợt
        /// </summary>
        public static void NhanhCan() { }    }
}
