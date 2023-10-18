namespace Estacionamento.Models
{
    public class ParkingSpace
    {
        protected int id;
        public int ID { get { return id; } }
        public string? RegistrationNumber { get; set; }
        public VehicleType VehicleType { get; set; }

        public override string ToString()
        {
            string result = "ID : " + ID + ", ";
            if (RegistrationNumber == null)
            {
                result += "VAZIO";
            }
            else
            {
                result += VehicleType.ToString() + " : " + RegistrationNumber;
            }
            return result;
        }
    }
}
