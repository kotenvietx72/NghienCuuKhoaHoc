using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.Common;

namespace ChuongTrinhChinh
{
    internal class Program
    {
        /// <summary>
        /// Đọc file dữ liệu lớp học
        /// </summary>
        /// <param name="filePath"></param>
        public static void readClassFromFile(string filePath, List<ClassRoom> classRooms)
        {
            try {
                using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.UTF8)) {
                    string line;
                    while ((line = sr.ReadLine()) != null) {
                        string[] columns = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        if (columns.Length == 5) {
                            ClassRoom a = new ClassRoom();
                                a.NameSubject = columns[0];
                                a.ClassName = columns[1];
                                a.Session = columns[2];
                                a.StudentCount = int.Parse(columns[3]);
                                a.Room = columns[4];
                            classRooms.Add(a);
                        }
                        else {
                            Console.WriteLine("Không đúng đầu vào dữ liệu");
                        }    
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Đọc file dữ liệu bị lỗi");
                Console.WriteLine(ex.Message);
            }
        }


        public static void Main(string[] args)
        {
            List<ClassRoom> classRooms = new List<ClassRoom>();
            string filePath = "A:\\NghienCuuKhoaHoc\\ChuongTrinhChinh\\ChuongTrinhChinh\\Input.txt";
            readClassFromFile(filePath, classRooms);
        }
    }
}
