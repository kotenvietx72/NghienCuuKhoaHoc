﻿using System;
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
            List<ClassRoom> classRooms = new List<ClassRoom>(); ClassInformation a = new ClassInformation(); List<BatchScheduler> batchSchedulers = new List<BatchScheduler>();
            {
                string filepath1 = "A:\\NghienCuuKhoaHoc\\ChuongTrinhChinh\\ChuongTrinhChinh\\Data\\Test.txt";
                string filepath2 = "A:\\NghienCuuKhoaHoc\\ChuongTrinhChinh\\ChuongTrinhChinh\\Data\\DataUniversity.txt";
                XuLiDuLieu.readClassFromFile(filepath1, classRooms); 
                XuLiDuLieu.readInforFromFile(filepath2, a);
            }     
            XuLiDuLieu.ChiaDotNhanhCan(batchSchedulers, classRooms, a, 300, 250);

            foreach (var x in batchSchedulers)
            {
                foreach(var y in x.classrooms)
                    Console.Write("\t" + y.ClassName);
                Console.Write(" " + x.WaitTime(a));
                Console.WriteLine();
            }
            XuLiDuLieu.NhanhCan(batchSchedulers, a);
        }
    }
}
