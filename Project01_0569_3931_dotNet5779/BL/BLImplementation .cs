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
                        try {
                            dl.AddTester(convert(tester),matrix);
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
                {throw;}
            }
            //--------------------------------------------------------------------
            void UpdateTester(string testerID, string field, params object[] info) { }
            //---------------------------------------------------------------
            public BO.Tester GetOneTester(string ID)
            {
                try
                {
                   return convert(dl.GetOneTester(ID));
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
                catch(InvalidDataException e)
                {
                    throw;
                }
                try
                {
                    dl.AddTrainee(convert(trainee));
                }
                catch
                    (DuplicateWaitObjectException e) { throw; }         
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
            //private DAL.DO.Tester BOtoDOTester(BO.Tester OldTester)
            //{
            //    DAL.DO.Tester DOTester = new DAL.DO.Tester
            //    {
            //        ID = OldTester.ID,
            //        FamilyName = OldTester.FamilyName,
            //        PrivateName = OldTester.PrivateName,
            //        DayOfBirth = OldTester.DayOfBirth,
            //        PersonGender = OldTester.PersonGender,
            //        Phone = OldTester.Phone,
            //        PersonAddress = OldTester.PersonAddress,
            //    };

            //    return DOTester;
            //}

        }
    }
}
