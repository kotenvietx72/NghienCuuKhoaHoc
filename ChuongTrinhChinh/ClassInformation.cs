using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuongTrinhChinh
{
    internal class ClassInformation {
        public int GateCount { get; set; }                              // Số lượng cổng
        public float StudentProcessingTime { get; set; }                // Thời gian xử lí 1 sinh viên qua cổng
        public H8Timing H8timing { get; set; } = new H8Timing();
        public class H8Timing {
            public float Time_Stair_1Floor { get; set; }       // Thời gian đi 1 tầng
            public float Time_Stair_To_Parking { get; set; }   // Thời gian đi từ tầng 1 tới nhà để xe
            public float Time_Parking_To_Gate { get; set; }    // Thời gian đi từ nhà để xe tới cổng soát vé
        }                                       // Thông tin thời gian HA8
        public H9Timing H9timing { get; set; } = new H9Timing();
        public class H9Timing {
            public float Time_Stair_1Floor { get; set; }       // Thời gian đi 1 tầng
            public float Time_Stair_To_Parking { get; set; }   // Thời gian đi từ tầng 1 tới nhà để xe
            public float Time_Parking_To_Gate { get; set; }    // Thời gian đi từ nhà để xe tới cổng soát vé
        }                                       // Thông tin thời gian HA9
        public H10Timing H10timing { get; set; } = new H10Timing();
        public class H10Timing {
            public float Time_Elavator_1Floor { get; set; }    // Thời gian đi 1 tầng bằng thang máy
            public float Time_Stair_1Floor { get; set; }       // Thời gian đi 1 tầng
            public float Time_Stair_To_Parking { get; set; }   // Thời gian đi từ tầng 1 tới nhà để xe
            public float Time_Parking_To_Gate { get; set; }    // Thời gian đi từ nhà để xe tới cổng soát vé
        }                                      // Thông tin thời gian HA10
    }
}
