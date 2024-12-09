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
            {
                string filepath1 = "A:\\NghienCuuKhoaHoc\\ChuongTrinhChinh\\ChuongTrinhChinh\\Data\\Test.txt";
                string filepath2 = "A:\\NghienCuuKhoaHoc\\ChuongTrinhChinh\\ChuongTrinhChinh\\Data\\DataUniversity.txt";
                XuLiDuLieu.readClassFromFile(filepath1, classRooms); 
                XuLiDuLieu.readInforFromFile(filepath2, a);
            }
            List<BatchScheduler> batchSchedulers = new List<BatchScheduler>();
            
            XuLiDuLieu.ChiaDotNhanhCan(batchSchedulers, classRooms, 300, 250);


            foreach (BatchScheduler batchScheduler in batchSchedulers)
            {
                Console.WriteLine(batchScheduler.Count_Student_Max);
                foreach (ClassRoom b in classRooms)
                {
                    Console.WriteLine("\t" + b.ClassName + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
