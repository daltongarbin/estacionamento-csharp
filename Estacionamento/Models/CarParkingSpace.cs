namespace Estacionamento.Models
{
    public class CarParkingSpace : ParkingSpace
    {
        public CarParkingSpace(int ID)
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

            if (!(obj is CarParkingSpace))
            {
                return false;
            }

            return ID == ((CarParkingSpace)obj).ID;
        }

        public override int GetHashCode()
        {
            return ID;
        }
    }
}
