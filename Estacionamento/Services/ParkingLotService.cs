using Estacionamento.Models;

namespace Estacionamento.Services
{
    public class ParkingLotService
    {
        private readonly ParkingSpaceService motorcyleParkingManager;
        private readonly ParkingSpaceService carParkingManager;
        private readonly ParkingSpaceService vanParkingManager;

        public ParkingLotService(int numberOfMotorcyleParking, int numberOfCarParking, int numberOfVanParking)
        {
            int id = 1;
            motorcyleParkingManager = new ParkingSpaceService(id, numberOfMotorcyleParking, VehicleType.MOTO);

            id += numberOfMotorcyleParking;
            carParkingManager = new ParkingSpaceService(id, numberOfCarParking, VehicleType.CARRO);

            id += numberOfCarParking;
            vanParkingManager = new ParkingSpaceService(id, numberOfVanParking, VehicleType.VAN);

            Console.WriteLine("Inicializacao finalizada!");
            Console.WriteLine();
        }

        public void ShowStatus()
        {
            Console.WriteLine("STATUS DO ESTACIONAMENTO");
            Console.WriteLine("==============");
            motorcyleParkingManager.ShowStatus();
            carParkingManager.ShowStatus();
            vanParkingManager.ShowStatus();
            Console.WriteLine();
        }

        public void ShowAvailability()
        {
            motorcyleParkingManager.ShowAvailability();
            carParkingManager.ShowAvailability();
            vanParkingManager.ShowAvailability();
            int freeSpaces = motorcyleParkingManager.GetFreeSpaces()
                + carParkingManager.GetFreeSpaces()
                + vanParkingManager.GetFreeSpaces();
            Console.WriteLine("Numero de vagas disponiveis = " + freeSpaces);

            int total = motorcyleParkingManager.GetSize()
                + carParkingManager.GetSize()
                + vanParkingManager.GetSize();
            Console.WriteLine("Numero total de vagas =  " + total);

            int bikes = motorcyleParkingManager.GetOccupiedLots(VehicleType.MOTO)
                + carParkingManager.GetOccupiedLots(VehicleType.MOTO)
                + vanParkingManager.GetOccupiedLots(VehicleType.MOTO);

            int cars = carParkingManager.GetOccupiedLots(VehicleType.CARRO)
                + vanParkingManager.GetOccupiedLots(VehicleType.CARRO);

            int vans = carParkingManager.GetOccupiedLots(VehicleType.VAN)
                + vanParkingManager.GetOccupiedLots(VehicleType.VAN);

            Console.WriteLine("Numero total de vagas ocupadas por motos = " + bikes);
            Console.WriteLine("Numero total de vagas ocupadas por carros = " + cars);
            Console.WriteLine("Numero total de vagas ocupadas por vans = " + vans);

            if (bikes + cars + vans == 0)
            {
                Console.WriteLine("O estacionamento esta VAZIO!");
            }

            if (bikes + cars + vans == total)
            {
                Console.WriteLine("O estacionamento esta CHEIO!");
            }
            Console.WriteLine();
        }

        public void Park(VehicleType vehicleType, string registrationNumber)
        {
            if (motorcyleParkingManager.HasVehicleWithRegistrationNumber(registrationNumber)
                || carParkingManager.HasVehicleWithRegistrationNumber(registrationNumber)
                || vanParkingManager.HasVehicleWithRegistrationNumber(registrationNumber))
            {
                Console.WriteLine("O veiculo com a placa \"" + registrationNumber + "\" ja esta estacionado!");
                return;
            }

            List<int> parkingLots = new List<int>();

            switch (vehicleType)
            {
                case VehicleType.MOTO:
                    parkingLots = motorcyleParkingManager.Park(registrationNumber, vehicleType);
                    if (parkingLots.Count == 0)
                    {
                        parkingLots = carParkingManager.Park(registrationNumber, vehicleType);
                        if (parkingLots.Count == 0)
                        {
                            parkingLots = vanParkingManager.Park(registrationNumber, vehicleType);
                            if (parkingLots.Count == 0)
                            {
                                Console.WriteLine("Nao ha estacionamento disponivel para " + vehicleType + "!");
                            }
                            else
                            {
                                Console.WriteLine("Vaga de estacionamento atribuída no ID: " + parkingLots[0]);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Vaga de estacionamento atribuida no ID: " + parkingLots[0]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Vaga de estacionamento atribuida no ID : " + parkingLots[0]);
                    }

                    break;
                case VehicleType.CARRO:
                    parkingLots = carParkingManager.Park(registrationNumber, vehicleType);
                    if (parkingLots.Count == 0)
                    {
                        parkingLots = vanParkingManager.Park(registrationNumber, vehicleType);
                        if (parkingLots.Count == 0)
                        {
                            Console.WriteLine("Nao ha estacionamento disponivel para " + vehicleType + "!");
                        }
                        else
                        {
                            Console.WriteLine("Vaga de estacionamento atribuida no ID : " + parkingLots[0]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Vaga de estacionamento atribuida no ID : " + parkingLots[0]);
                    }

                    break;
                case VehicleType.VAN:
                default:
                    parkingLots = vanParkingManager.Park(registrationNumber, vehicleType);
                    if (parkingLots.Count == 0)
                    {
                        parkingLots = carParkingManager.Park(registrationNumber, vehicleType);
                        if (parkingLots.Count == 0)
                        {
                            Console.WriteLine("Nao ha estacionamento disponivel para " + vehicleType + "!");
                        }
                        else
                        {
                            Console.WriteLine("Vaga de estacionamento atribuida em VAGA DE CARRO ID : " + string.Join(",", parkingLots.ToArray()));
                        }
                    }
                    else
                    {
                        Console.WriteLine("Vaga de estacionamento atribuida no ID : " + parkingLots[0]);
                    }
                    break;
            }
        }

        public void Exit(string registrationNumber)
        {
            if (motorcyleParkingManager.HasVehicleWithRegistrationNumber(registrationNumber))
            {
                motorcyleParkingManager.Exit(registrationNumber);
            }
            else if (carParkingManager.HasVehicleWithRegistrationNumber(registrationNumber))
            {
                carParkingManager.Exit(registrationNumber);
            }
            else if (vanParkingManager.HasVehicleWithRegistrationNumber(registrationNumber))
            {
                vanParkingManager.Exit(registrationNumber);
            }
            else
            {
                Console.WriteLine("Nenhum veiculo com placa \"" + registrationNumber + "\" esta estacionado aqui!");
            }
        }
    }
}
