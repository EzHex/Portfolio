using System;
using System.Collections.Generic;
using System.IO;

namespace AutomobiliuParkas
{
    public class InOut
    {
        /// <summary>
        /// Reads all file info
        /// </summary>
        /// <param name="txtFiles"> All file paths</param>
        /// <returns>Registers list </returns>
        public static List<TransportRegister> ReadFiles(string[] txtFiles)
        {
            List<TransportRegister> registers = new List<TransportRegister>();
            foreach (var item in txtFiles)
            {
                registers.Add(ReadFile(item));
            }
            return registers;
        }
        /// <summary>
        /// Read one file data from the path
        /// </summary>
        /// <param name="path">path of the file</param>
        /// <returns>Register</returns>
        private static TransportRegister ReadFile(string path)
        {
            string[] allLines = File.ReadAllLines(path);
            List<Transport> transports = new List<Transport>();

            string city = allLines[0];
            string address = allLines[1];
            string email = allLines[2];

            for (int i = 3; i < allLines.Length; i++)
            {
                string[] line = allLines[i].Split(';');
                string licensePlate = line[1];
                string manufactor = line[2];
                string model = line[3];
                DateTime yearOfManufaction = DateTime.Parse(line[4]);
                DateTime technicalInspection = DateTime.Parse(line[5]);
                string fuel = line[6];
                double averageConsumption = double.Parse(line[7]);
                switch (line[0].ToLower())
                {
                    case "a":
                        {
                            Car car = new Car(licensePlate, manufactor, model, yearOfManufaction, technicalInspection, fuel,
                                averageConsumption, double.Parse(line[8]));
                            transports.Add(car);
                            break;
                        }
                    case "k":
                        {
                            Truck truck = new Truck(licensePlate, manufactor, model, yearOfManufaction, technicalInspection, fuel,
                                averageConsumption, double.Parse(line[8]));
                            transports.Add(truck);
                            break;
                        }
                    case "m":
                        {
                            Microbus microbus = new Microbus(licensePlate, manufactor, model, yearOfManufaction, technicalInspection, fuel,
                                averageConsumption, int.Parse(line[8]));
                            transports.Add(microbus);
                            break;
                        }
                    default:
                        break;
                }

            }

            TransportRegister register = new TransportRegister(city, address, email, transports);

            return register;
        }


    }
}