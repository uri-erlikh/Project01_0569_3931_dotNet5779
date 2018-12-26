using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DO;
using BO;

namespace BL
{
    class BLImplementation
    {
        public class BL_imp : IBL
        {
            IDal dl = DAL_Factory.GetDL("data");
            //-------------------------------------------------------------
            public void AddTester(BO.Tester tester, bool[,] matrix)
            {

                try
                {
                    if (tester.age >= Configuration.MIN_TESTER_AGE && tester.age < Configuration.MAX_TESTER_AGE)
                        try
                        {
                            dl.AddTester(Convert(tester), matrix);
                        }
                        catch (DuplicateWaitObjectException e)
                        {
                            throw;
                        }
                    else if (tester.age < Configuration.MIN_TESTER_AGE)
                        throw new BO.InvalidDataException("Too young tester");
                    else throw new BO.InvalidDataException("Too old tester");
                }
                catch (BO.InvalidDataException e) { throw; }
            }
            //---------------------------------------------------------
            public void DeleteTester(string TesterID)
            {
                try
                {
                    dl.DeleteTester(TesterID);
                }
                catch (KeyNotFoundException e)
                { throw; }
            }
            //--------------------------------------------------------------------
            public void UpdateTester(string testerID, string field, params object[] info) { }
            //---------------------------------------------------------------
            public BO.Tester GetOneTester(string ID)
            {
                try
                {
                    return Convert(dl.GetOneTester(ID));
                }
                catch (KeyNotFoundException e)
                {
                    throw;
                }
            }
            //----------------------------------------------------------------
            public void AddTrainee(BO.Trainee trainee)
            {
                try
                {
                    if (trainee.age < Configuration.MIN_TRAINEE_AGE)
                        throw new InvalidDataException("Too young trainee");
                }
                catch (InvalidDataException e)
                {
                    throw;
                }
                try
                {
                    dl.AddTrainee(Convert(trainee));
                }
                catch
                    (DuplicateWaitObjectException e)
                { throw; }
            }
            //------------------------------------------------------------------------
            public void DeleteTrainee(string TraineeID)
            {
                try
                {
                    dl.DeleteTrainee(TraineeID);
                }
                catch (KeyNotFoundException e)
                {
                    throw;
                }
            }
            //--------------------------------------------------------------------
            public void UpdateTrainee(string traineeID, string field, params object[] info) { }
            //---------------------------------------------------------------------
            public BO.Trainee GetOneTrainee(string ID)
            {
                try
                {
                    return convert(dl.GetOneTrainee(ID));
                }
                catch (KeyNotFoundException e)
                {
                    throw;
                }
            }
            //------------------------------------------------------------------            
            private DO.Tester Convert(BO.Tester tester)
            {
                return new DO.Tester(tester.ID)
                {
                    FamilyName = tester.FamilyName,
                    PrivateName = tester.PrivateName,
                    Phone = tester.Phone,
                    DayOfBirth = tester.DayOfBirth,
                    PersonGender = (DO.Gender)tester.PersonGender,
                    PersonAddress = new DO.Address(tester.PersonAddress.City,tester.PersonAddress.Street,tester.PersonAddress.NumOfBuilding),
                    TesterExperience = tester.TesterExperience,
                    TesterVehicle = (DO.Vehicle)tester.TesterVehicle,
                    MaxWeeklyTests = tester.MaxWeeklyTests,
                    RangeToTest = tester.RangeToTest
                };
            }
            //------------------------------------------------------------------
            private DO.Trainee Convert(BO.Trainee trainee)
            {
                return new DO.Trainee(trainee.ID)
                {
                    FamilyName=trainee.FamilyName,
                    PrivateName=trainee.PrivateName,
                    Phone=trainee.Phone,
                    DayOfBirth=trainee.DayOfBirth,
                    PersonGender=(DO.Gender)trainee.PersonGender,
                    PersonAddress=new DO.Address(trainee.PersonAddress.City, trainee.PersonAddress.Street, trainee.PersonAddress.NumOfBuilding),
                    TraineeVehicle= (DO.Vehicle)trainee.TraineeVehicle,
                    TraineeGear=(DO.GearBox)trainee.TraineeGear,
                    School=trainee.School,
                    Teacher=trainee.Teacher,
                    DrivingLessonsNum=trainee.DrivingLessonsNum,
                };
            }
            //------------------------------------------------------------------            
            private DO.Test Convert(BO.Test test)
            {
                return new DO.Test(test.Tester.ID, test.TraineeId)
                {
                    TestDate = test.TestDate,
                    TestHour = test.TestHour,
                    TestAddress = new DO.Address(test.TestAddress.City, test.TestAddress.Street, test.TestAddress.NumOfBuilding),
                    Mirrors = test.Mirrors,
                    Brakes = test.Brakes,
                    ReverseParking = test.ReverseParking,
                    Distance = test.Distance,
                    Vinkers = test.Vinkers,
                    TrafficSigns = test.TrafficSigns,
                    PassedTest=test.PassedTest,
                    TesterNote=test.TesterNote,
                };
            }
            //--------------------------------------------------------------------------
            private BO.Tester Convert(DO.Tester tester)
            {
                return new BO.Tester
                {
                    ID = tester.ID,
                    FamilyName = tester.FamilyName,
                    PrivateName = tester.PrivateName,
                    DayOfBirth = tester.DayOfBirth,
                    PersonGender = (BO.Gender)tester.PersonGender,
                    Phone = tester.Phone,
                    PersonAddress = new BO.Address(tester.PersonAddress.City, tester.PersonAddress.Street, tester.PersonAddress.NumOfBuilding),
                    TesterExperience = tester.TesterExperience,
                    MaxWeeklyTests = tester.MaxWeeklyTests,
                    TesterVehicle = (BO.Vehicle)tester.TesterVehicle,
                    RangeToTest = tester.RangeToTest,
                    Schedule = dl.GetSchedule(tester.ID),

                };
            }

        }
    }
}
