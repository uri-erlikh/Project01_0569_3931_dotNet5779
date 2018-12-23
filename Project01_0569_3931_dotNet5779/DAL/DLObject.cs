using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DAL
{
    class DLObject:IDal
    {
        static  DLObject instance = null;
        private DLObject() { }
        //static DLObject() { }

        //public static DLObject Instance// { get { return instance; } }

        public static DLObject GetInstance()
        {
            if (instance == null)
                instance = new DLObject();
            return instance;
        }


        public void AddTester(DO.Tester tester)
        {
            if (!IfExist(tester.ID, "tester"))
                DataSource.Testers.Add(tester);
            else throw new Exception();
        }
        //--------------------------------------------------------------
        public void DeleteTester(string TesterID)
        {
            if (IfExist(TesterID, "tester"))
            {
                foreach (var tester in DataSource.Testers)
                    if (tester.ID == TesterID)
                        DataSource.Testers.Remove(tester);
            }
            else throw new Exception();
        }
        //------------------------------------------------------------------
        public void AddTrainee(DO.Trainee trainee)
        {
            if (!IfExist(trainee.ID, "trainee"))
                DataSource.Trainies.Add(trainee);
            else throw new Exception();
        }
        //------------------------------------------------------------
        public void DeleteTrainee(string TraineeID)
        {
            if (IfExist(TraineeID, "tester"))
            {
                foreach (var trainee in DataSource.Trainies)
                    if (trainee.ID == TraineeID)
                        DataSource.Trainies.Remove(trainee);
            }
            else throw new Exception();
        }
        //--------------------------------------------------------------

    }
}
