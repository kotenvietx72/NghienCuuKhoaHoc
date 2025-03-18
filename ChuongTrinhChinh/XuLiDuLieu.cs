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
        public static double SegmentTime = 0;

        /// <summary>
        /// Đọc file dữ liệu lớp học
        /// </summary>
        /// <param name="filePath"></param> 
        // Done
        public static List<ClassRoom> readClassFromFile()
        {
            List<ClassRoom> classRooms = new List<ClassRoom>();
            string filePath = "A:\\NghienCuuKhoaHoc\\ChuongTrinhChinh\\ChuongTrinhChinh\\Data\\Input_Full.txt";
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
        /// Hàm đếm số lượng sinh viên trong danh sách
        /// </summary>
        /// <param name="classRooms"></param>
        /// <returns></returns>
        public static int CountStudent(List<ClassRoom> classRooms)
        {
            int totalStudents = 0;
            foreach (var classRoom in classRooms)
                totalStudents += classRoom.StudentCount;
            return totalStudents;
        }

        /// <summary>
        /// Lấy danh sách các lớp đến tiết được xử lí     
        /// </summary>
        /// <param name="classRooms"></param>
        /// <returns></returns>
        // Done
        public static List<ClassRoom> GetClassRoomsSession(List<ClassRoom> classRooms)
        {
            int SoTietConLai = 6 - (int)(SegmentTime / 300);
            return classRooms.Where(c => c.GetSessionCount() >= SoTietConLai && !c.check).ToList();
        }

        public static void setTime1(List<BatchScheduler> bestBatches)
        {
            if (bestBatches.Count < 2)
                return;
                
            int lastIndex = bestBatches.Count - 1;
            int secondLastIndex = lastIndex - 1;

            double minTimePrev = bestBatches[secondLastIndex].classrooms.Min(c => c.TimeToGate());
            double minTimeCurrent = bestBatches[lastIndex].classrooms.Min(c => c.TimeToGate());

            SegmentTime += bestBatches[secondLastIndex].ProcessingTime() + minTimePrev - minTimeCurrent;
        }

        public static void setTime2(List<ClassRoom> SelectedClasses)
        {
            if (SegmentTime < 300 && SelectedClasses.All(c => c.check))
            {
                SegmentTime = 300;
                return;
            }
            if (SegmentTime < 600 && SelectedClasses.All(c => c.check))
            {
                SegmentTime = 600;
                return;
            }
            if (SegmentTime < 900 && SelectedClasses.All(c => c.check))
            {
                SegmentTime = 900;
                return;
            }
            if (SegmentTime < 1200 && SelectedClasses.All(c => c.check))
            {
                SegmentTime = 1200;
                return;
            }
        }

        /// <summary>
        /// Hàm tìm các đợt tối ưu trong nhánh cận
        /// </summary>
        /// <param name="bestBatches"></param>
        /// <param name="classrooms"></param>
        public static void TimCacDotToiUu(List<BatchScheduler> bestBatches, List<ClassRoom> classrooms) {
            List<BatchScheduler> bestBatchesCurrentRound;
            List<ClassRoom> SelectedClasses;                                                    // Thêm các lớp đến giờ được xử lí

            while (classrooms.Any(c => !c.check) && SegmentTime <= 1200) {                      // Lặp đến khi xử lí hết các lớp trong danh sách hoặc chưa đến giờ xử lí
                double SegmentTimeOld = SegmentTime;

                bestBatchesCurrentRound = new List<BatchScheduler>();
                var NhomDangXet = new BatchScheduler();
                var NhomDuocChon = new BatchScheduler();
                SelectedClasses = GetClassRoomsSession(classrooms);
                SapXep(SelectedClasses);

                NhanhCan(NhomDangXet, 0, NhomDuocChon);                                             // Gọi thuật toán nhánh cận, với nhóm mới và bắt đầu từ 0

                if (NhomDuocChon.classrooms.Count > 0)
                {
                    bestBatches.Add(NhomDuocChon.DeepCopy());
                    foreach (var room in NhomDuocChon.classrooms)
                        classrooms.First(x => x.ClassName == room.ClassName).check = true;          // Đánh dấu các lớp đã được chọn, không xử lí các lớp này trong các đợt tiếp theo  
                    setTime1(bestBatches);
                    if (bestBatches.Count > 1 && SegmentTime - bestBatches[bestBatches.Count - 2].TimeCheck > 300)
                        bestBatches[bestBatches.Count - 1].TimeCheck = SegmentTimeOld;
                    else
                        bestBatches[bestBatches.Count - 1].TimeCheck = SegmentTime;
                    continue;
                }
                setTime2(SelectedClasses);
                
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
                bestBatches[bestBatches.Count -1].TimeCheck = 1200;
            }
            
            TinhThoiGianTanHoc(bestBatches);

            /// <summary>
            /// Sử dụng nhánh cận để tìm ra nhóm tối ưu
            /// </summary>
            /// <param name="NhomDangXet"></param>
            /// <param name="index">Vị trí lớp đang xét trong danh sách</param>
            void NhanhCan(BatchScheduler NhomDangXet, int index, BatchScheduler NhomDuocChon) {
                // Nếu nhóm đang xét có tổng số lượng sinh viên lớn hơn GioiHanMax thì dừng nhánh này
                if (NhomDangXet.Count_Student() > GioiHanMaxSinhVien)
                    return;

                // Nếu nhánh hiện tại có thời gian đợi lớn hơn bestBatchesCurrentRound => cắt nhánh này luôn
                if (bestBatchesCurrentRound.Count > 0 && NhomDangXet.WaitTime() > bestBatchesCurrentRound.Last().WaitTime()) {
                    return;
                }

                if (CountStudent(SelectedClasses) < 240)
                {
                    foreach (var classRoom in SelectedClasses)
                        NhomDangXet.classrooms.Add(classRoom);
                    bestBatchesCurrentRound.Add(NhomDangXet.DeepCopy());
                    NhomDuocChon.classrooms = NhomDangXet.DeepCopy().classrooms;
                    return;
                }

                // Nếu nhóm đang xét hợp lệ và tốt hơn nhóm hiện tại => lưu lại nhóm tốt hơn
                if ((NhomDangXet.Count_Student() >= GioiHanMinSinhVien && IsValidBatch(NhomDangXet) == 3 && CountBuilding(SelectedClasses) == 3) || (NhomDangXet.Count_Student() >= GioiHanMinSinhVien && IsValidBatch(NhomDangXet) == 2 && CountBuilding(SelectedClasses) == 2) || (NhomDangXet.Count_Student() >= GioiHanMinSinhVien && IsValidBatch(NhomDangXet) == 1 && CountBuilding(SelectedClasses) == 1))
                {
                    // Nếu NhomDangXet có tỉ lệ thời gian xử lí/ tổng số lớp < đợt hiện tại => Lưu NhomDangXet thay thế bestBatches
                    if (bestBatchesCurrentRound.Count == 0 || NhomDangXet.WaitTime() / NhomDangXet.classrooms.Count < bestBatchesCurrentRound.Last().WaitTime()/ bestBatchesCurrentRound.Last().classrooms.Count) {
                        bestBatchesCurrentRound.Clear();
                        bestBatchesCurrentRound.Add(NhomDangXet.DeepCopy());
                        NhomDuocChon.classrooms = NhomDangXet.DeepCopy().classrooms;
                    }
                    // Nếu NhomDangXet có tỉ lệ thời gian xử lí/ tổng số lớp = đợt hiện tại => Xét tiếp thời gian xử lí
                    if (NhomDangXet.WaitTime() / NhomDangXet.classrooms.Count == bestBatchesCurrentRound.Last().WaitTime() / bestBatchesCurrentRound.Last().classrooms.Count)
                    {
                        if(NhomDangXet.ProcessingTime() / NhomDangXet.classrooms.Count < bestBatchesCurrentRound.Last().ProcessingTime() / bestBatchesCurrentRound.Last().classrooms.Count)
                        {
                            bestBatchesCurrentRound.Clear();
                            bestBatchesCurrentRound.Add(NhomDangXet.DeepCopy());
                            NhomDuocChon.classrooms = NhomDangXet.DeepCopy().classrooms;
                        }
                    }
                    return;                                                             
                }

                

                for (int i = index; i < SelectedClasses.Count; i++)                          // Duyệt qua tất cả các lớp trong danh sách
                {
                    if(SelectedClasses[i].check)                                             // Lớp đã được xử lí, bỏ qua
                        continue;   
                    NhomDangXet.classrooms.Add(SelectedClasses[i]);                          // Thêm nhóm vào lớp
                    NhanhCan(NhomDangXet, i + 1, NhomDuocChon);                             // Gọi đệ quy để thử các lớp tiếp theo, chọn các lớp sau để tránh chọn lớp cũ và trùng tổ hợp
                    NhomDangXet.classrooms.RemoveAt(NhomDangXet.classrooms.Count - 1);      // Quay lui: Bỏ lớp cuối cùng được thêm vào để thử các tổ hợp khác
                }
            }

        }   

        /// <summary>
        /// Tính thời gian tan học cho các lớp mới được thêm vào danh sách
        /// </summary>
        /// <param name="bestBatches"></param>
        public static void TinhThoiGianTanHoc(List<BatchScheduler> bestBatches) {
            foreach (var batch in bestBatches) {
                {
                    DateTime newDismissalTime;
                    if (batch.TimeCheck > 1200)
                        newDismissalTime = batch.DismissalTimeBatch.AddSeconds(1200);
                    else
                        newDismissalTime = batch.DismissalTimeBatch.AddSeconds(batch.TimeCheck);
                    batch.DismissalTimeBatch = newDismissalTime;
                    foreach (var classRooms in batch.classrooms)
                        classRooms.DismissalTime = batch.DismissalTimeBatch;   
                }
            }
        }
    }
}
