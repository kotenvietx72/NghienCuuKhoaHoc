using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuongTrinhChinh
{
    internal class ClassInformation {
        private int GateCount { get; set; }                     // Số lượng cổng
        private float GateProcessingTime { get; set; }          // Thời gian xử lí 1 cổng
        private class H8Timing {
            private float Time_Stair_1Floor { get; set; }       // Thời gian đi 1 tầng
            private float Time_Stair_To_Parking { get; set; }   // Thời gian đi từ tầng 1 tới nhà để xe
            private float Time_Parking_To_Gate { get; set; }    // Thời gian đi từ nhà để xe tới cổng soát vé
        }
        private class H9Timing {
            private float Time_Stair_1Floor { get; set; }       // Thời gian đi 1 tầng
            private float Time_Stair_To_Parking { get; set; }   // Thời gian đi từ tầng 1 tới nhà để xe
            private float Time_Parking_To_Gate { get; set; }    // Thời gian đi từ nhà để xe tới cổng soát vé
        }
        private class H10Timing {
            private float Time_Elavator_1Floor { get; set; }    // Thời gian đi 1 tầng bằng thang máy
            private float Time_Stair_1Floor { get; set; }       // Thời gian đi 1 tầng
            private float Time_Stair_To_Parking { get; set; }   // Thời gian đi từ tầng 1 tới nhà để xe
            private float Time_Parking_To_Gate { get; set; }    // Thời gian đi từ nhà để xe tới cổng soát vé
        }
    }
}
