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
        static void Main()
        {
            List<ClassRoom> classRooms = XuLiDuLieu.readClassFromFile(); List<BatchScheduler> batchSchedulers = new List<BatchScheduler>();
            
            XuLiDuLieu.TimCacDotToiUu(batchSchedulers, classRooms);
            XuLiDuLieu.TinhThoiGianTanHoc(batchSchedulers);

            foreach (BatchScheduler a in batchSchedulers)
            {
                foreach (ClassRoom room in a.classrooms)
                    Console.Write(room.ClassName + " ");
                Console.WriteLine(a.Count_Student() + " " + a.WaitTime() + " " + a.ProcessingTime());
            }

            Console.WriteLine();

            foreach(ClassRoom room in classRooms)
                Console.WriteLine(room.ClassName + " " + room.DismissalTime.Hour + "h " + room.DismissalTime.Minute + "p " + room.DismissalTime.Second + "s ");    
        }

     }
}
