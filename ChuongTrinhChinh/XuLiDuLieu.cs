using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ChuongTrinhChinh
{
    internal class XuLiDuLieu
    {
        public static ClassInformation classInformation = readInforFromFile();

        // Giới hạn Min: Khả năng xử lí của cổng trong 4 phút
        public static int GioiHanMinSinhVien = (int)(classInformation.StudentProcessingTime / classInformation.GateCount * 240);
        // Giới hạn Max: Khả năng xử lí của cổng trong 5 phút
        public static int GioiHanMaxSinhVien = (int)(classInformation.StudentProcessingTime / classInformation.GateCount * 300);
        // Biến đếm thời gian để các lớp được xử lí
        public static int SegmentTime = 0;

        /// <summary>
        /// Đọc file dữ liệu lớp học
        /// </summary>
        /// <param name="filePath"></param> 
        // Done
        public static List<ClassRoom> readClassFromFile()
        {
            List<ClassRoom> classRooms = new List<ClassRoom>();
            string filePath = "A:\\NghienCuuKhoaHoc\\ChuongTrinhChinh\\ChuongTrinhChinh\\Data\\Input.txt";
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
            return classRooms;
        }

        /// <summary>
        /// Đọc file dữ liệu thông tin của trường
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="classInformation"></param> 
        // Done
        public static ClassInformation readInforFromFile()
        {
            ClassInformation classInformation = new ClassInformation();
            string filePath = "A:\\NghienCuuKhoaHoc\\ChuongTrinhChinh\\ChuongTrinhChinh\\Data\\DataUniversity.txt";
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
            return classInformation;
        }

        /// <summary>
        /// Sắp xếp lớp tăng dần theo thời gian ra khỏi cổng + thời gian xử lí           
        /// </summary>
        /// <param name="classRooms"></param>
        // Done
        public static void SapXep(List<ClassRoom> classRooms)
        {
            classRooms = classRooms.OrderBy(c => c.TimeToGate() + c.ExitTime()).ToList();
        }

        /// <summary>
        /// Hàm đếm xem đợt hiện tại có bao nhiêu tòa         
        /// </summary>
        /// <param name="batchScheduler"></param>
        /// <returns></returns>
        // Done
        public static int IsValidBatch(BatchScheduler batchScheduler)
        {
            int dem = 0;
            if(batchScheduler.classrooms.Any(c => c.Room.Contains("A8")))
                dem++;
            if(batchScheduler.classrooms.Any(c => c.Room.Contains("A9")))
                dem++;
            if (batchScheduler.classrooms.Any(c => c.Room.Contains("A10")))
                dem++;
            return dem;
        }

        /// <summary>
        /// Hàm đếm các lớp trong danh sách có bao nhiêu tòa     
        /// </summary>
        /// <param name="classRooms"></param>
        /// <returns></returns>
        // Done
        public static int CountBuilding(List<ClassRoom> classRooms) {
            int dem = 0;
            if (classRooms.Any(c => !c.check && c.Room.Contains("A8")))
                dem++;

            if (classRooms.Any(c => !c.check && c.Room.Contains("A9")))
                dem++;
            if (classRooms.Any(c => !c.check && c.Room.Contains("A10")))
                dem++;
            return dem;
        }

        /// <summary>
        /// Hàm tìm các đợt tối ưu trong nhánh cận
        /// </summary>
        /// <param name="bestBatches"></param>
        /// <param name="classrooms"></param>
        public static void TimCacDotToiUu(List<BatchScheduler> bestBatches, List<ClassRoom> classrooms) {
            List<BatchScheduler> bestBatchesCurrentRound;
            SapXep(classrooms);
            while (classrooms.Any(c => !c.check)) {                                             // Lặp đến khi xử lí hết các lớp trong danh sách
                bestBatchesCurrentRound = new List<BatchScheduler>();
                var NhomDangXet = new BatchScheduler();
                var NhomDuocChon = new BatchScheduler();
                NhanhCan(NhomDangXet, 0, NhomDuocChon);                                         // Gọi thuật toán nhánh cận, với nhóm mới và bắt đầu từ 0
                if (NhomDuocChon.classrooms.Count > 0)                                          // Kiểm tra nhóm đang xét có lớp hay không
                {                          
                    bestBatches.Add(NhomDuocChon.DeepCopy());
                    foreach (var room in NhomDuocChon.classrooms)
                        classrooms.First(x => x.ClassName == room.ClassName).check = true;      // Đánh dấu các lớp đã được chọn, không xử lí các lớp này trong các đợt tiếp theo  
                }
                else
                    break;
            }

            if(classrooms.Any(c => !c.check)) {                                                 // Còn 1 số lớp chưa được xử lí, sẽ được gom lại thành một nhóm
                var NhomConLai = new BatchScheduler();
                foreach (var room in classrooms) {
                    if (!room.check) {
                        NhomConLai.classrooms.Add(room);
                        room.check = true;
                    }
                }
                bestBatches.Add(NhomConLai.DeepCopy());
            }    

            /// <summary>
            /// Sử dụng nhánh cận để tìm ra nhóm tối ưu
            /// </summary>
            /// <param name="NhomDangXet"></param>
            /// <param name="index">Vị trí lớp đang xét trong danh sách</param>
            void NhanhCan(BatchScheduler NhomDangXet, int index, BatchScheduler NhomDuocChon) {
                // Nếu nhóm đang xét có tổng số lượng sinh viên lớn hơn GioiHanMax thì dừng nhánh này
                if (NhomDangXet.Count_Student() > GioiHanMaxSinhVien)
                    return;
                // Nếu nhóm đang xét hợp lệ và tốt hơn nhóm hiện tại => lưu lại nhóm tốt hơn
                if ((NhomDangXet.Count_Student() >= GioiHanMinSinhVien && IsValidBatch(NhomDangXet) == 3 && CountBuilding(classrooms) == 3))
                {
                    if (bestBatchesCurrentRound.Count == 0 || NhomDangXet.WaitTime() / NhomDangXet.classrooms.Count < bestBatchesCurrentRound.Last().WaitTime()/ bestBatchesCurrentRound.Last().classrooms.Count) {
                        bestBatchesCurrentRound.Clear();
                        bestBatchesCurrentRound.Add(NhomDangXet.DeepCopy());
                        NhomDuocChon.classrooms = NhomDangXet.DeepCopy().classrooms;
                    }                               
                    return;                                                             
                }

                // TH: Một tòa xử lí xong, còn 2 tòa chưa được xử lí,  
                if (NhomDangXet.Count_Student() >= GioiHanMinSinhVien && IsValidBatch(NhomDangXet) == 2 && CountBuilding(classrooms) == 2 ) {
                    if (bestBatchesCurrentRound.Count == 0 || NhomDangXet.WaitTime() / NhomDangXet.classrooms.Count < bestBatchesCurrentRound.Last().WaitTime() / bestBatchesCurrentRound.Last().classrooms.Count)
                    {
                        bestBatchesCurrentRound.Clear();
                        bestBatchesCurrentRound.Add(NhomDangXet.DeepCopy());
                        NhomDuocChon.classrooms = NhomDangXet.DeepCopy().classrooms;
                    }
                    return;
                }



                for (int i = index; i < classrooms.Count; i++)                          // Duyệt qua tất cả các lớp trong danh sách
                {
                    if(classrooms[i].check)                                             // Lớp đã được xử lí, bỏ qua
                        continue;   
                    NhomDangXet.classrooms.Add(classrooms[i]);                          // Thêm nhóm vào lớp
                    NhanhCan(NhomDangXet, i + 1, NhomDuocChon);                         // Gọi đệ quy để thử các lớp tiếp theo, chọn các lớp sau để tránh chọn lớp cũ và trùng tổ hợp
                    NhomDangXet.classrooms.RemoveAt(NhomDangXet.classrooms.Count - 1);  // Quay lui: Bỏ lớp cuối cùng được thêm vào để thử các tổ hợp khác
                }
            }

        }   

        /// <summary>
        /// Tính thời gian tan học cho các lớp
        /// </summary>
        /// <param name="bestBatches"></param>
        public static void TinhThoiGianTanHoc(List<BatchScheduler> bestBatches) {
            for(int i = 0; i < bestBatches.Count - 1; i++) {
                double Time = bestBatches[i].ProcessingTime() + bestBatches[i].classrooms[0].TimeToGate() - bestBatches[i + 1].classrooms[0].TimeToGate();
                bestBatches[i + 1].DismissalTimeBatch = bestBatches[i + 1].DismissalTimeBatch.AddSeconds(Time);
            }
            foreach(var batch in bestBatches) {
                foreach (var classroom in batch.classrooms) {
                    classroom.DismissalTime = batch.DismissalTimeBatch;
                }
            }
        }
    }
}
