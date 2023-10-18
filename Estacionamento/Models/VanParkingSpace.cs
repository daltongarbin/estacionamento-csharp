namespace Estacionamento.Models
{
    public class VanParkingSpace : ParkingSpace
    {
        public VanParkingSpace(int ID)
        {
            id = ID;
            VehicleType = VehicleType.NONE;
            RegistrationNumber = null;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is VanParkingSpace))
            {
                return false;
            }

            return ID == ((VanParkingSpace)obj).ID;
        }

        public override int GetHashCode()
        {
            return ID;
        }
    }
}
