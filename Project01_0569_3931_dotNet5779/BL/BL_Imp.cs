using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BL
{
    public class BL_imp : IBL
    {
        IDal dl = DAL_Factory.GetDL("data");

        public void AddTester(BO.Tester tester)
        {
            try
            {
                if (!IfExist(tester.ID, "tester"))
                {
                    try
                    {
                        if (tester.Age >= 40 && tester.Age < 67)
                            dl.AddTester(BOtoDOTester(tester));
                        else if (tester.Age < 40)
                            throw new Exception("Too young tester");
                        else throw new Exception("Too old tester");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else throw new Exception("Allready exist");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //---------------------------------------------------------
        public void DeleteTester(string TesterID)
        {
            try
            {
                if (IfExist(TesterID, "tester"))
                    dl.DeleteTester(TesterID);
                else
                    throw new Exception("not exist");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        //--------------------------------------------------------------------
        void UpdateTester(string testerFamilyName, string TesterPrivateName, int num, params object[] info) { }
        //----------------------------------------------------------------
        public void AddTrainee(BO.Trainee trainee)
        {
            try
            {
                if (!IfExist(trainee.ID, "trainee"))
                {
                    try
                    {
                        if (trainee.Age < 18)
                            dl.AddTrainee(trainee);
                        else if (trainee.Age < 18)
                            throw new Exception("Too young trainee");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else throw new Exception("Allready exist");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //------------------------------------------------------------------------
        public void DeleteTrainee(string TraineeID)
        {
            try
            {
                if (IfExist(TraineeID, "trainee"))
                    dl.DeleteTrainee(TraineeID);
                else
                    throw new Exception("not exist");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);             
            }
        }
        //--------------------------------------------------------------------
        private DAL.DO.Tester BOtoDOTester(BO.Tester OldTester)
        {
            DAL.DO.Tester DOTester = new DAL.DO.Tester { ID = OldTester.ID,
            FamilyName=OldTester.FamilyName,
            PrivateName =OldTester.PrivateName,
            DayOfBirth=OldTester.DayOfBirth,
            PersonGender=OldTester.PersonGender,
            Phone=OldTester.Phone,
            PersonAddress=OldTester.PersonAddress,
            };

            return DOTester;
        }

    }
}
