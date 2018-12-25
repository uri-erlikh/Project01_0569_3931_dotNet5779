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
        //dddd

        internal static Dictionary<String, ConfigurationParameter> Configuration = new Dictionary<string, ConfigurationParameter>();
        
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
                Schedule = new bool[5, 6]
                {
                    { true, true, false,true,false ,false},
                    {true, true, false,true,false ,false},
                    { true, true, false,true,false,true},
                    { true, true, false,true,false,false},
                    {false, true, false,true,false,true }
                }
            });
            Testers.Add(new DO.Tester
            {
                PrivateName = "Uri",
                FamilyName = "Erliche",
                ID = "206543731",
                BirthOfDay = new DateTime(1985, 04, 12),
                Phone = "0501444567",
                PersonGender = DO.Gender.male,
                PersonAddress =    ,
                MaxWeeklyTests = 25,
                RangeToTest = 40,
                TesterExperience = 10,
                TesterVehicle = DO.Vehicle.truck,
                Schedule = new bool[5, 6]
                {
                    { true, false, false,true,false ,false},
                    {true, true, false,true,true ,true},
                    { false, true, false,true,false,true},
                    { true, false, true,true,false,true},
                    {true, true, false,true,false,true }
                }
            });
            Testers.Add(new DO.Tester
            {
                PrivateName = "Yosi",
                FamilyName = "Israel",
                ID = "305343234",
                BirthOfDay = new DateTime(1995, 04, 15),
                Phone = "0501234222",
                PersonGender = DO.Gender.male,
                PersonAddress =    ,
                MaxWeeklytTests = 20,
                RangeToTest = 30,
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
            Testers.Add(new DO.Tester
            {
                PrivateName = "Sara",
                FamilyName = "Cohen",
                ID = "456743931",
                BirthOfDay = new DateTime(1975, 01, 12),
                Phone = "0545634567",
                PersonGender = DO.Gender.female,
                PersonAddress =    ,
                MaxWeeklytTests = 15,
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
            Testers.Add(new DO.Tester
            {
                PrivateName = "Israel",
                FamilyName = "Israeli",
                ID = "649843931",
                BirthOfDay = new DateTime(1985, 04, 12),
                Phone = "0501235632",
                PersonGender = DO.Gender.male,
                PersonAddress =    ,
                MaxWeeklytTests = 20,
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

            Trainies.Add(new DO.Trainee
            {
                PrivateName = "Noam",
                FamilyName = "Atias",
                ID = "123543786",
                Phone = "05325674653",
                DayOfBirth = new DateTime(1998, 07, 13),
                School = "keren",
                Teacher = "Moshe Gil",
                CurrentTestNum = 3,
                DrivingLessonsNum = 35,
                PersonGender = Gender.male,
                PersonAddress =  ,
                TraineeGear = GearBox.auto,
                TraineeVehicle = Vehicle.privateCar
            });
            Trainies.Add(new DO.Trainee
            {
                PrivateName = "Noam",
                FamilyName = "Atias",
                ID = "123543786",
                Phone = "05325674653",
                DayOfBirth = new DateTime(1998, 07, 13),
                School = "keren",
                Teacher = "Moshe Gil",
                CurrentTestNum = 3,
                DrivingLessonsNum = 35,
                PersonGender = Gender.male,
                PersonAddress =  ,
                TraineeGear = GearBox.auto,
                TraineeVehicle = Vehicle.privateCar
            });
            Trainies.Add(new DO.Trainee
            {
                PrivateName = "Noam",
                FamilyName = "Atias",
                ID = "123543786",
                Phone = "05325674653",
                DayOfBirth = new DateTime(1998, 07, 13),
                School = "keren",
                Teacher = "Moshe Gil",
                CurrentTestNum = 3,
                DrivingLessonsNum = 35,
                PersonGender = Gender.male,
                PersonAddress =  ,
                TraineeGear = GearBox.auto,
                TraineeVehicle = Vehicle.privateCar
            });
            Trainies.Add(new DO.Trainee
            {
                PrivateName = "Noam",
                FamilyName = "Atias",
                ID = "123543786",
                Phone = "05325674653",
                DayOfBirth = new DateTime(1998, 07, 13),
                School = "keren",
                Teacher = "Moshe Gil",
                CurrentTestNum = 3,
                DrivingLessonsNum = 35,
                PersonGender = Gender.male,
                PersonAddress =  ,
                TraineeGear = GearBox.auto,
                TraineeVehicle = Vehicle.privateCar
            });
            Trainies.Add(new DO.Trainee
            {
                PrivateName = "Noam",
                FamilyName = "Atias",
                ID = "123543786",
                Phone = "05325674653",
                BirthOfDay = new DateTime(1998, 07, 13),
                School = "keren",
                Teacher = "Moshe Gil",
                CurrentTestNum = 3,
                DrivingLessonsNum = 35,
                PersonGender = DO.Gender.male,
                PersonAddress =  ,
                TraineeGear = GearBox.auto,
                TraineeVehicle = Vehicle.privateCar
            });

            Tests.Add(new DO.Test
            {
                TesterId = "305343931",
                TraineeId = "123543786",
                TestDate = new DateTime(2018, 12, 25),
                TestAddress =  ,
                PassedTest = true,
                TesterNote = "no note",
                TestHour = new DateTime(2018, 12, 25, 12, 54, 45),
                Criterions = new List<DO.Criterion>()
            });
            Tests.Add(new DO.Test
            {
                TesterId = "305343931",
                TraineeId = "123543786",
                TestDate = new DateTime(2018, 12, 25),
                TestAddress =  ,
                PassedTest = true,
                TesterNote = "no note",
                TestHour = new DateTime(2018, 12, 25, 12, 54, 45),
                Criterions = new List<Criterion>()
            });
            Tests.Add(new DO.Test
            {
                TesterId = "305343931",
                TraineeId = "123543786",
                TestDate = new DateTime(2018, 12, 25),
                TestAddress =  ,
                PassedTest = true,
                TesterNote = "no note",
                TestHour = new DateTime(2018, 12, 25, 12, 54, 45),
                Criterions = new List<Criterion>()
            });
            Tests.Add(new DO.Test
            {
                TesterId = "305343931",
                TraineeId = "123543786",
                TestDate = new DateTime(2018, 12, 25),
                TestAddress =  ,
                PassedTest = true,
                TesterNote = "no note",
                TestHour = new DateTime(2018, 12, 25, 12, 54, 45),
                Criterions = new List<Criterion>()
            });
            Tests.Add(new DO.Test
            {
                TesterId = "305343931",
                TraineeId = "123543786",
                TestDate = new DateTime(2018, 12, 25),
                TestAddress =  ,
                PassedTest = true,
                TesterNote = "no note",
                TestHour = new DateTime(2018, 12, 25, 12, 54, 45),
                Criterions = new List<Criterion>()
            });

        }



    }
}
