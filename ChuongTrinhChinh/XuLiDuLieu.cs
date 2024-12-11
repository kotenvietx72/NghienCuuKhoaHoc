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
        // Done
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
        // Done
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
        /// Hàm kiểm tra lớp đã được xử lí chưa ?
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns> 
        // Done
        public static bool Check(ClassRoom a)
        { 
            if(a.check == false) 
                return false; 
            return true;
        }    

        /// <summary>
        /// Hàm check danh sách lớp đã được xử lí hết chưa
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        // Done
        public static bool CheckClassXuLi(List<ClassRoom> x)
        {
            foreach(ClassRoom r in x)
                if(Check(r) == false) return false;  
            return true;
        }

        /// <summary>
        /// Hàm chia các lớp thành các đợt
        /// </summary> 
        public static void ChiaDotNhanhCan(List<BatchScheduler> a, List<ClassRoom> b, ClassInformation classInformation, int GioiHanMaxSinhVien, int GioiHanMinSinhVien)
        {
            var classHA8 = b.Where(lop => lop.Room?.Substring(2, 2) == "A8" && Check(lop) == false).ToList();
            var classHA9 = b.Where(lop => lop.Room?.Substring(2, 2) == "A9" && Check(lop) == false).ToList();
            var classHA10 = b.Where(lop => lop.Room?.Substring(2, 3) == "A10" && Check(lop) == false).ToList();
            List<BatchScheduler> test = new List<BatchScheduler>();
            void TimDotHienTai(BatchScheduler DotHienTai, List<ClassRoom> LopConLai, int TongSiSo)
            {
                var checkHA8 = DotHienTai.classrooms.Any(lop => lop.Room?.Substring(2, 2) == "A8") && classHA8.Count() > 0;
                var checkHA9 = DotHienTai.classrooms.Any(lop => lop.Room?.Substring(2, 2) == "A9") && classHA9.Count() > 0;
                var checkHA10 = DotHienTai.classrooms.Any(lop => lop.Room?.Substring(2, 3) == "A10") && classHA10.Count() > 0;

                for (int i = 0; i < LopConLai.Count; i++)
                {
                    ClassRoom LopDuocChon = LopConLai[i];
                    var LopMoiConLai = LopConLai.Skip(i + 1).ToList();

                    DotHienTai.classrooms.Add(LopDuocChon);
                    TimDotHienTai(DotHienTai, LopMoiConLai, TongSiSo + LopDuocChon.StudentCount);
                    DotHienTai.classrooms.RemoveAt(DotHienTai.classrooms.Count - 1);
                }

                if (TongSiSo > GioiHanMaxSinhVien || TongSiSo < GioiHanMinSinhVien)
                    return;

                if(checkHA10 && checkHA9 && checkHA8)
                {
                    DotHienTai.Count_Student_Max = TongSiSo;
                    DotHienTai.classrooms.Sort((x, y) => x.TimeToGate(classInformation).CompareTo(y.TimeToGate(classInformation))); // Sắp xếp tăng dần theo thời gian đi ra cổng
                    a.Add(DotHienTai.DeepCopy());
                    return;
                }    
            }
            TimDotHienTai(new BatchScheduler(), b, 0);
        }
 
        /// <summary>
        /// Hàm tính best time để đưa ra lộ trình các đợt
        /// </summary> 
        public static void NhanhCan() { }    
    }
}
