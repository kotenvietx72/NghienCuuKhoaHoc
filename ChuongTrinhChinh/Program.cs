using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.Common;
using System.Diagnostics;

namespace ChuongTrinhChinh
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            List<ClassRoom> classRooms = new List<ClassRoom>(); ClassInformation a = new ClassInformation();
            List<ClassRoom> classHA8 = new List<ClassRoom>(); List<ClassRoom> classHA9 = new List<ClassRoom>(); List<ClassRoom> classHA10 = new List<ClassRoom>();
            {
                string filepath1 = "A:\\NghienCuuKhoaHoc\\ChuongTrinhChinh\\ChuongTrinhChinh\\Data\\Input.txt";
                string filepath2 = "A:\\NghienCuuKhoaHoc\\ChuongTrinhChinh\\ChuongTrinhChinh\\Data\\DataUniversity.txt";
                XuLiDuLieu.readClassFromFile(filepath1, classRooms); XuLiDuLieu.readInforFromFile(filepath2, a);
            }
            XuLiDuLieu.ChiaLopTheoToa(classRooms, classHA8, classHA9, classHA10);
        }
    }
}
