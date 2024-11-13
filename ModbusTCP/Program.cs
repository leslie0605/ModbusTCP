using EasyModbus;

class Program
{
    static void Main(string[] args)
    {

        ModbusClient modbusClient = new("127.0.0.1",502);

        try
        {

            modbusClient.Connect();
            Console.WriteLine("Connected to Modbus Server.");

            // Read from coils
            int coilStartAddress = 0;
            int numberOfCoils = 20;
            bool[] coils = modbusClient.ReadCoils(coilStartAddress, numberOfCoils);
            Console.WriteLine("Read Coils:");
            for (int i = 0; i < coils.Length; i++)
            {
                Console.WriteLine($"Register {coilStartAddress + i + 1}: {coils[i]}");
            }

            // Write to coils
            int coilAddress = 16;
            bool state = true;
            modbusClient.WriteSingleCoil(coilAddress-1, state);
            Console.WriteLine($"Written state {state} to Coil {coilAddress}");

            // Read from holding registers
            int startAddress = 0;
            int numberOfRegisters = 20;
            int[] registers = modbusClient.ReadHoldingRegisters(startAddress, numberOfRegisters);
            Console.WriteLine("Read Holding Registers:");
            for (int i = 0; i < registers.Length; i++)
            {
                Console.WriteLine($"Register {startAddress + i + 1}: {registers[i]}");
            }

            // Write to a single holding register
            int registerAddress = 8;
            int value = 68;
            modbusClient.WriteSingleRegister(registerAddress-1, value);
            Console.WriteLine($"Written value {value} to Register {registerAddress}");

            modbusClient.Disconnect();
            Console.WriteLine("Disconnected.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }
}