namespace Estacionamento.Models
{
    public class MotorcyleParkingSpace : ParkingSpace
    {
        public MotorcyleParkingSpace(int ID)
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

            if (!(obj is MotorcyleParkingSpace))
            {
                return false;
            }

            return ID == ((MotorcyleParkingSpace)obj).ID;
        }

        public override int GetHashCode()
        {
            return ID;
        }
    }
}
