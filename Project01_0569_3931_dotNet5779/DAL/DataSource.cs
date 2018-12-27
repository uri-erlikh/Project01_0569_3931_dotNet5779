using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DAL
{
    class DataSource
    {
        internal class ConfigurationParameter
        {
            internal bool Readable;
            internal bool Writable;
            internal object value;
        }

        internal static Dictionary<string, ConfigurationParameter> Configuration = new Dictionary<string, ConfigurationParameter>();
        
        internal static Dictionary<string, bool[,]> Schedules=new Dictionary<string, bool[,]>();// עי new צריך לאתחל את מיערך

        internal static List<DO.Tester> Testers = new List<DO.Tester>();
        internal static List<DO.Trainee> Trainies = new List<DO.Trainee>();
        internal static List<DO.Test> Tests = new List<DO.Test>();
        static DataSource()
        {
            Configuration.Add("MIN_LESSONS", new ConfigurationParameter() { Readable = true, Writable = false, value = 28 });
            Configuration.Add("MAX_TESTER_AGE", new ConfigurationParameter() { Readable = true, Writable = false, value = 67 });
            Configuration.Add("MIN_TRAINEE_AGE", new ConfigurationParameter() { Readable = true, Writable = false, value = 16 });
            Configuration.Add("MIN_GAP_TEST", new ConfigurationParameter() { Readable = true, Writable = false, value = 30 });
            Configuration.Add("MIN_TESTER_AGE", new ConfigurationParameter() { Readable = true, Writable = false, value = 40 });
            Configuration.Add("Number", new ConfigurationParameter() { Readable = true, Writable = true, value = 10000000 });

            Testers.Add(new DO.Tester("305343931")
            {
                PrivateName = "Moshe",
                FamilyName = "Weizman",
                DayOfBirth = new DateTime(1965, 04, 12),
                Phone = "0501234567",
                PersonGender = DO.Gender.male,
                PersonAddress = new DO.Address { City = "Elad", NumOfBuilding = 12, Street = "shmaia" },
                MaxWeeklyTests = 20,
                RangeToTest = 30,
                TesterExperience = 10,
                TesterVehicle = DO.Vehicle.privateCar,
            });
                

                Testers.Add(new DO.Tester("206543731")
                {
                    PrivateName = "Uri",
                    FamilyName = "Erliche",
                    DayOfBirth = new DateTime(1985, 04, 12),
                    Phone = "0501444567",
                    PersonGender = DO.Gender.male,
                    PersonAddress = new DO.Address { City = "Elad", NumOfBuilding = 3, Street = "shamai" },
                    MaxWeeklyTests = 25,
                    RangeToTest = 40.5,
                    TesterExperience = 10,
                    TesterVehicle = DO.Vehicle.truck,
                }
                );

            Testers.Add(new DO.Tester("305343234")
            {
                PrivateName = "Yosi",
                FamilyName = "Israel",
                DayOfBirth = new DateTime(1995, 04, 15),
                Phone = "0501234222",
                PersonGender = DO.Gender.male,
                PersonAddress = new DO.Address { City = "Elad", NumOfBuilding = 2, Street = "shmaia" },
                MaxWeeklyTests = 20,
                RangeToTest = 30.7,
                TesterExperience = 10,
                TesterVehicle = DO.Vehicle.motorcycle,
                Schedule = new bool[5, 6]
                {
                    { true, true, false,true,false ,false},
                    {false, true, true,true,false ,true},
                    { true, true, false,true,false,true},
                    { false, true, false,true,false,true},
                    {true, true, true,true,false,true}
                }
            });
            Testers.Add(new DO.Tester("456743931")
            {
                PrivateName = "Sara",
                FamilyName = "Cohen",
                DayOfBirth = new DateTime(1975, 01, 12),
                Phone = "0545634567",
                PersonGender = DO.Gender.female,
                PersonAddress = new DO.Address { City = "Elad", NumOfBuilding = 20, Street = "ben zaqai" },
                MaxWeeklyTests = 15,
                RangeToTest = 12,
                TesterExperience = 24,
                TesterVehicle = DO.Vehicle.privateCar,
                Schedule = new bool[5, 6]
                {
                    { true, true, false,true,false ,false},
                    {true, true, false,true,false ,true},
                    { true, true, false,true,false,true},
                    { true, true, false,true,false,true},
                    {true, true, false,true,false,true }
                }
            });
            Testers.Add(new DO.Tester("649843931")
            {
                PrivateName = "Israel",
                FamilyName = "Israeli",
                DayOfBirth = new DateTime(1985, 04, 12),
                Phone = "0501235632",
                PersonGender = DO.Gender.male,
                PersonAddress = new DO.Address { City = "bnei brak", NumOfBuilding = 1, Street = "hshoner" },
                MaxWeeklyTests = 20,
                RangeToTest = 30,
                TesterExperience = 10,
                TesterVehicle = DO.Vehicle.heavyTruck,
                Schedule = new bool[5, 6]
                {
                    { true, true, true,true,false ,false},
                    {true, true, false,true,true ,true},
                    { true, true, false,true,false,false},
                    { true, true, true,true,true,true},
                    {true, true, false,true,false,true }
                }
            });
            //------------------------------------------------------------------------------------------------------------------
            Schedules.Add("305343931", new bool[5, 6]
                {
                    { true, true, false, true, false, false },
                { true, true, false, true, false, false },
                { true, true, false, true, false, true },
                { true, true, false, true, false, false },
                { false, true, false, true, false, true }
            }
                );
            
            //-----------------------------------------------------------------------
            Trainies.Add(new DO.Trainee("123543786")
            {
                PrivateName = "Noam",
                FamilyName = "Atias",
                Phone = "05325674653",
                DayOfBirth = new DateTime(1998, 07, 13),
                School = "keren",
                Teacher = "Gil Dan",
                DrivingLessonsNum = 35,
                PersonGender = Gender.male,
                PersonAddress = new DO.Address { City = "Elad", NumOfBuilding = 12, Street = "shmaia" },
                TraineeGear = GearBox.auto,
                TraineeVehicle = Vehicle.privateCar
            });
            Trainies.Add(new DO.Trainee("657847659")
            {
                PrivateName = "Yoel",
                FamilyName = "Atias",
                Phone = "05325674653",
                DayOfBirth = new DateTime(1998, 07, 13),
                School = "keren",
                Teacher = "Moshe Gil",
                DrivingLessonsNum = 35,
                PersonGender = Gender.male,
                PersonAddress = new DO.Address { City = "Ramat Gan", NumOfBuilding = 7, Street = "shmaia" },
                TraineeGear = GearBox.auto,
                TraineeVehicle = Vehicle.truck
            });
            Trainies.Add(new DO.Trainee("098768574")
            {
                PrivateName = "Noam",
                FamilyName = "Atias",
                Phone = "05325674653",
                DayOfBirth = new DateTime(1998, 07, 13),
                School = "keren",
                Teacher = "Dan Gil",
                DrivingLessonsNum = 35,
                PersonGender = Gender.male,
                PersonAddress = new DO.Address { City = "Elad", NumOfBuilding = 12, Street = "heelel" },
                TraineeGear = GearBox.auto,
                TraineeVehicle = Vehicle.motorcycle
            });
            Trainies.Add(new DO.Trainee("145674664")
            {
                PrivateName = "Naor",
                FamilyName = "Ati",
                Phone = "05325674653",
                DayOfBirth = new DateTime(1998, 07, 13),
                School = "keren",
                Teacher = "Noah Cohen",
                DrivingLessonsNum = 35,
                PersonGender = Gender.male,
                PersonAddress = new DO.Address { City = "Netanya", NumOfBuilding = 12, Street = "shmaia" },
                TraineeGear = GearBox.auto,
                TraineeVehicle = Vehicle.privateCar
            });
            Trainies.Add(new DO.Trainee("784765489")
            {
                PrivateName = "Dani",
                FamilyName = "Atias",
                Phone = "05325674653",
                DayOfBirth = new DateTime(1998, 07, 13),
                School = "keren",
                Teacher = "Nir Gil",
                DrivingLessonsNum = 35,
                PersonGender = DO.Gender.male,
                PersonAddress = new DO.Address { City = "Jerusalem", NumOfBuilding = 12, Street = "shmaia" },
                TraineeGear = GearBox.auto,
                TraineeVehicle = Vehicle.heavyTruck
            });
            //--------------------------------------------------------------------------------------------------------
            Tests.Add(new DO.Test("649843931", "784765489")
            {
                TestDate = new DateTime(2018, 12, 25),
                TestAddress = new DO.Address { City = "Tel aviv", NumOfBuilding = 50, Street = "shmaia" },
                PassedTest = true,
                TesterNote = "no note",
                TestHour = new DateTime(2018, 12, 25, 12, 54, 45),
                Mirrors = true,
                Brakes = true,
                ReverseParking=true,
                Distance=true,
                Vinkers=false,
                TrafficSigns=true
            });
            Tests.Add(new DO.Test("456743931", "145674664")
            {
                TestDate = new DateTime(2019, 12, 25),
                TestAddress = new DO.Address { City = "Jerusalem", NumOfBuilding = 12, Street = "Jafo" },
                PassedTest = true,
                TesterNote = "no note",
                TestHour = new DateTime(2018, 12, 25, 12, 54, 45),
                Mirrors = true,
                Brakes = true,
                ReverseParking=false,
                Distance=true,
                Vinkers=false,
                TrafficSigns=true
            });
            Tests.Add(new DO.Test("305343234", "098768574" )
            {
                TestDate = new DateTime(2018, 12, 10),
                TestAddress = new DO.Address { City = "Elad", NumOfBuilding = 70, Street = "shmaia" },
                PassedTest = false,
                TesterNote = "no note",
                TestHour = new DateTime(2018, 12, 25, 12, 54, 45),
                Mirrors = true,
                Brakes = true,
                ReverseParking=false,
                Distance=false,
                Vinkers=false,
                TrafficSigns=false
            });
            Tests.Add(new DO.Test("206543731", "657847659" )
            {
                TestDate = new DateTime(2020, 12, 25),
                TestAddress = new DO.Address { City = "Elad", NumOfBuilding = 12, Street = "Tena" },
                PassedTest = false,
                TesterNote = "no note",
                TestHour = new DateTime(2018, 12, 25, 12, 54, 45),
                Mirrors = false,
                Brakes = true,
                ReverseParking=false,
                Distance=false,
                Vinkers=true,
                TrafficSigns=false
            });
            Tests.Add(new DO.Test("305343931"," 123543786")
            {
                TestDate = new DateTime(2021, 12, 25),
                TestAddress = new DO.Address { City = "Elad", NumOfBuilding = 78, Street = "Tena" },
                PassedTest = true,
                TesterNote = "no note",
                TestHour = new DateTime(2018, 12, 25, 12, 54, 45),
                Mirrors = true,
                Brakes = true,
                ReverseParking=true,
                Distance=true,
                Vinkers=true,
                TrafficSigns=false
            });

        }



    }
}
