using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public class Dal_imp                          //תזכורת - סינגלטון ועיקרון השיבוט כפי שמופיע אצל דן 
    {
        

        // private DataSource ds = new DataSource();

        public void AddTester(DO.Tester tester)
        {
            if (!IfExist(tester.ID, "tester"))
                DataSource.Testers.Add(tester);
        }
        //--------------------------------------------------------------
        public void DeleteTester(string TesterID)
        {
            if (IfExist(TesterID, "tester"))
                foreach (var tester in DataSource.Testers)
                    if (tester.ID == TesterID)
                        DataSource.Testers.Remove(tester);
        }
        //------------------------------------------------------------------
        public void AddTrainee(DO.Trainee trainee)
        {
            if (!IfExist(trainee.ID, "trainee"))
                DataSource.Trainies.Add(trainee);
        }
        //------------------------------------------------------------
        public void DeleteTrainee(string TraineeID)
        {
            if (IfExist(TraineeID, "tester"))
                foreach (var trainee in DataSource.Trainies)
                    if (trainee.ID == TraineeID)
                        DataSource.Trainies.Remove(trainee);
        }
        //--------------------------------------------------------------
    }
}

