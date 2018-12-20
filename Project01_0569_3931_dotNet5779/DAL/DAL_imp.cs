using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public class Dal_imp : IDal                         //תזכורת - סינגלטון ועיקרון השיבוט כפי שמופיע אצל דן 
    {
        static readonly Dal_imp instance = new Dal_imp();
        Dal_imp() { }
        static Dal_imp() { }

        public static Dal_imp Instance { get { return instance; } }

        // private DataSource ds = new DataSource();

        void AddTester(DO.Tester tester)
        {
            if (!IfExist(tester.ID, "tester"))
                DataSource.Testers.Add(tester);
        }
        //--------------------------------------------------------------
        void DeleteTester(string TesterID)
        {
            if (IfExist(TesterID, "tester"))
                foreach (var tester in DataSource.Testers)
                    if (tester.ID == TesterID)
                        DataSource.Testers.Remove(tester);
        }
        //------------------------------------------------------------------
        void AddTrainee(DO.Trainee trainee)
        {
            if (!IfExist(trainee.ID, "trainee"))
                DataSource.Trainies.Add(trainee);
        }
        //------------------------------------------------------------
        void DeleteTrainee(string TraineeID)
        {
            if (IfExist(TraineeID, "tester"))
                foreach (var trainee in DataSource.Trainies)
                    if (trainee.ID == TraineeID)
                        DataSource.Trainies.Remove(trainee);
        }
        //--------------------------------------------------------------
    }
}

