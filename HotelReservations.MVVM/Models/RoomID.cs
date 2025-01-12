using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Models
{
    public sealed record RoomID(int FloorNumber, int RoomNumber)
    {
        public override string ToString()
        {
            return $"{FloorNumber}/{RoomNumber}";
        }
    }
}
