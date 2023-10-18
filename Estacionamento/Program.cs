using Estacionamento.Models;
using Estacionamento.Services;

public class Program
{
    public static void Main(string[] args)
    {
        int motorcyleSlots = 0;
        int carSlots = 0;
        int vanSlots = 0;
        int option = -1;
        int vehicleOption;
        VehicleType vehicleType = VehicleType.NONE;
        string inputUsr;
        int outputExit;
        bool inputExit;
        bool flagStartProgram = true;

        while (flagStartProgram)
        {
            try
            {
                Console.WriteLine("GERENCIAMENTO DE ESTACIONAMENTO");
                Console.WriteLine("===================");
                Console.WriteLine("Configuracao inicial");
                Console.Write("Numero de vagas para estacionamento de motos: ");
                inputUsr = Console.ReadLine();
                motorcyleSlots = int.Parse(inputUsr);

                if (motorcyleSlots < 1)
                {
                    throw new ArgumentException();
                }

                Console.Write("Numero de vagas para estacionamento de carros: ");
                inputUsr = Console.ReadLine();
                carSlots = int.Parse(inputUsr);
                if (carSlots < 1)
                {
                    throw new ArgumentException();
                }

                Console.Write("Numero de vagas para estacionamento de vans: ");
                inputUsr = Console.ReadLine();
                vanSlots = int.Parse(inputUsr);

                if (vanSlots < 1)
                {
                    throw new ArgumentException();
                }

                ParkingLotService manager = new ParkingLotService(motorcyleSlots, carSlots, vanSlots);
                manager.ShowAvailability();
                manager.ShowStatus();

                while (option != 5)
                {
                    Console.WriteLine();
                    Console.WriteLine("=====================================");
                    Console.WriteLine("    SISTEMA DE GESTAO DE ESTACIONAMENTO");
                    Console.WriteLine("=====================================");
                    Console.WriteLine("1. Estacione um veiculo");
                    Console.WriteLine("2. Saida de um veiculo estacionado");
                    Console.WriteLine("3. Mostrar numero de vagas disponiveis");
                    Console.WriteLine("4. Mostrar status geral do estacionamento");
                    Console.WriteLine("5. FIM");
                    Console.WriteLine("=====================================");
                    Console.Write("Sua escolha: ");

                    inputUsr = Console.ReadLine();
                    try
                    {
                        option = int.Parse(inputUsr);
                        if (option < 1 || option > 5)
                        {
                            throw new Exception();
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Opcao invalida");
                        Console.WriteLine();
                    }

                    switch (option)
                    {
                        case 1:
                            Console.Write("Digite a placa do veiculo: ");
                            inputUsr = Console.ReadLine();
                            inputUsr = inputUsr.Trim().ToUpper();

                            vehicleOption = -1;
                            while (vehicleOption == -1)
                            {
                                try
                                {
                                    Console.WriteLine("1. MOTO");
                                    Console.WriteLine("2. CARRO");
                                    Console.WriteLine("3. VAN");
                                    Console.Write("Selecione o tipo do veiculo: ");
                                    vehicleOption = int.Parse(Console.ReadLine());
                                    if (vehicleOption < 1 || vehicleOption > 3)
                                    {
                                        throw new Exception();
                                    }
                                }
                                catch (Exception)
                                {
                                    vehicleOption = -1;
                                }
                            }

                            if (vehicleOption == 1)
                            {
                                vehicleType = VehicleType.MOTO;
                            }
                            else if (vehicleOption == 2)
                            {
                                vehicleType = VehicleType.CARRO;
                            }
                            else
                            {
                                vehicleType = VehicleType.VAN;
                            }

                            manager.Park(vehicleType, inputUsr);
                            break;
                        case 2:
                            Console.Write("Digite a placa do veiculo: ");
                            inputUsr = Console.ReadLine();
                            inputUsr = inputUsr.Trim().ToUpper();
                            manager.Exit(inputUsr);
                            break;
                        case 3:
                            manager.ShowAvailability();
                            break;
                        case 4:
                            manager.ShowStatus();
                            break;
                        case 5:
                            Console.WriteLine("OBRIGADO POR USAR O ESTACIONAMENTO.");
                            Environment.Exit(0);
                            break;
                        default:
                            flagStartProgram = false;
                            break;
                    }
                }
            }
            catch (ArgumentException)
            {
                if (motorcyleSlots < 1)
                {
                    Console.WriteLine("Deve ter pelo menos 1 vaga para moto!");
                    Console.WriteLine();
                }
                else if (carSlots < 1)
                {
                    Console.WriteLine("Deve ter pelo menos 1 vaga para carro!");
                    Console.WriteLine();
                }
                else if (vanSlots < 1)
                {
                    Console.WriteLine("Deve ter pelo menos 1 vaga para van!");
                    Console.WriteLine();
                }

                Console.WriteLine("Deseja continuar? 1 - Sim | 2 - Não");
                inputExit = int.TryParse(Console.ReadLine(), out outputExit);
                if (inputExit)
                {
                    if (outputExit == 1)
                    {
                        Console.Clear();
                        continue;
                    }
                    else if (outputExit == 2)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Entrada inválida");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Entrada inválida");
                    Console.WriteLine();
                }
                flagStartProgram = true;
            }
            catch (Exception)
            {
                Console.WriteLine("Entrada invalida!");
                Console.WriteLine();
            }
        }
    }
}