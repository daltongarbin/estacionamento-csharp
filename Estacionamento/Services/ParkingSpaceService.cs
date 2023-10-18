using Estacionamento.Models;

namespace Estacionamento.Services
{
    public class ParkingSpaceService
    {
        private readonly SortedSet<int> availableParkingIds;
        private readonly SortedSet<int> occupiedParkingIds;
        private readonly SortedDictionary<int, ParkingSpace> parkingSpaceMap;
        private readonly Dictionary<string, List<int>> vehicleMapping;
        private VehicleType vehicleType;

        public ParkingSpaceService(int startingId, int numberOfSpaces, VehicleType vehicleType)
        {
            this.vehicleType = vehicleType;
            availableParkingIds = new SortedSet<int>();
            occupiedParkingIds = new SortedSet<int>();
            parkingSpaceMap = new SortedDictionary<int, ParkingSpace>();
            vehicleMapping = new Dictionary<string, List<int>>();

            for (int i = 0; i < numberOfSpaces; i++)
            {
                ParkingSpace space = Generate(startingId + i, vehicleType);
                availableParkingIds.Add(i + startingId);
                parkingSpaceMap.Add(i + startingId, space);
            }
        }

        public static ParkingSpace Generate(int id, VehicleType vehicleType)
        {
            switch (vehicleType)
            {
                case VehicleType.MOTO:
                    return new MotorcyleParkingSpace(id);
                case VehicleType.CARRO:
                    return new CarParkingSpace(id);
                case VehicleType.VAN:
                default:
                    return new VanParkingSpace(id);
            }
        }

        public void ShowStatus()
        {
            Console.WriteLine("ESTACIONAMENTO PARA " + vehicleType);
            foreach (int key in parkingSpaceMap.Keys)
            {
                Console.WriteLine(parkingSpaceMap[key].ToString());
            }
            Console.WriteLine();
        }

        public int GetFreeSpaces()
        {
            return availableParkingIds.Count;
        }

        public int GetSize()
        {
            return parkingSpaceMap.Count;
        }

        public int GetOccupiedLots(VehicleType vehicleTypeToBeSearched)
        {
            int count = 0;
            foreach (int key in parkingSpaceMap.Keys)
            {
                if (parkingSpaceMap[key].VehicleType == vehicleTypeToBeSearched)
                {
                    ++count;
                }
            }
            return count;
        }

        public void ShowAvailability()
        {
            if (availableParkingIds.Count == 0)
            {
                Console.WriteLine("ESTACIONAMENTO CHEIO PARA " + vehicleType);
            }
            else
            {
                Console.WriteLine("VAGAS DE ESTACIONAMENTO DISPONIVEIS PARA CARRO " + vehicleType + " : " + availableParkingIds.Count);
            }
        }

        public List<int> Park(string registrationNumber, VehicleType vehicleTypeToBeParked)
        {
            int id = -1;
            switch (vehicleType)
            {
                case VehicleType.MOTO:
                    if (vehicleTypeToBeParked != VehicleType.MOTO)
                    {
                        return new List<int>();
                    }
                    else
                    {
                        id = AllocateOneSpace(registrationNumber, vehicleTypeToBeParked);
                        if (id == -1)
                        {
                            return new List<int>();
                        }
                        else
                        {
                            return new List<int> { id };
                        }
                    }

                case VehicleType.CARRO:
                    if (vehicleTypeToBeParked == VehicleType.VAN)
                    {
                        id = -1;
                        foreach (int parkingLotId in availableParkingIds)
                        {
                            if (availableParkingIds.Contains(parkingLotId + 1)
                                && availableParkingIds.Contains(parkingLotId + 2))
                            {
                                id = parkingLotId;
                                break;
                            }
                        }

                        if (id == -1)
                        {
                            return new List<int>();
                        }
                        else
                        {
                            List<int> result = new List<int>();
                            for (int i = 0; i < 3; i++)
                            {
                                availableParkingIds.Remove(id + i);
                                occupiedParkingIds.Add(id + i);

                                ParkingSpace space = parkingSpaceMap[id + i];
                                space.VehicleType = vehicleTypeToBeParked;
                                space.RegistrationNumber = registrationNumber;
                                parkingSpaceMap[id + i] = space;
                                result.Add(id + i);
                            }

                            vehicleMapping.Add(registrationNumber, result);
                            return result;
                        }
                    }
                    else
                    {
                        id = AllocateOneSpace(registrationNumber, vehicleTypeToBeParked);
                        if (id == -1)
                        {
                            return new List<int>();
                        }
                        else
                        {
                            return new List<int> { id };
                        }
                    }

                case VehicleType.VAN:
                default:
                    id = AllocateOneSpace(registrationNumber, vehicleTypeToBeParked);
                    if (id == -1)
                    {
                        return new List<int>();
                    }
                    else
                    {
                        return new List<int> { id };
                    }
            }
        }

        public bool HasVehicleWithRegistrationNumber(string registrationNumber)
        {
            return vehicleMapping.ContainsKey(registrationNumber);
        }

        public bool Exit(string registrationNumber)
        {
            if (vehicleMapping.ContainsKey(registrationNumber))
            {
                List<int> parkingSpaces = vehicleMapping[registrationNumber];
                vehicleMapping.Remove(registrationNumber);
                VehicleType parkedVehicle = VehicleType.NONE;
                foreach (int id in parkingSpaces)
                {
                    occupiedParkingIds.Remove(id);
                    availableParkingIds.Add(id);
                    ParkingSpace space = parkingSpaceMap[id];
                    space.RegistrationNumber = null;
                    parkedVehicle = space.VehicleType;
                    space.VehicleType = VehicleType.NONE;
                    parkingSpaceMap[id] = space;
                }
                Console.WriteLine(parkedVehicle + " com a placa \"" + registrationNumber + "\" saiu do estacionamento.");
                return true;
            }
            else
            {
                return false;
            }
        }

        private int AllocateOneSpace(string registrationNumber, VehicleType vehicleType)
        {
            if (availableParkingIds.Count == 0)
            {
                return -1;
            }
            else
            {
                int id = availableParkingIds.First();
                availableParkingIds.Remove(id);
                occupiedParkingIds.Add(id);

                ParkingSpace space = parkingSpaceMap[id];
                space.VehicleType = vehicleType;
                space.RegistrationNumber = registrationNumber;
                parkingSpaceMap[id] = space;
                vehicleMapping.Add(registrationNumber, new List<int>() { id });

                return id;
            }
        }
    }
}
