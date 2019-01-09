using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDal
    {
        void AddTester(DO.Tester tester, bool [,]matrix);
        void DeleteTester(string TesterID);
        void UpdateTester(string testerID,string field, params object[] info);        
        DO.Tester GetOneTester(string ID);
        //---------------------------------------------
        void AddTrainee(DO.Trainee trainee);
        void DeleteTrainee(string TraineeID, DO.Vehicle vehicle);
        void UpdateTrainee(string traineeID, string field, params object[] info);
        DO.Trainee GetOneTrainee(string ID, DO.Vehicle vehicle);
        //-------------------------------------------------------
        string AddTest(DO.Test test);
        void UpdateTestResult(int numoftest);
        DO.Test GetOneTest(int TestNum);
        void DeleteTest(int numOfTest);
        //-----------------------------------------        
        List<DO.Tester> GetTesters();
        List<DO.Trainee> GetTrainees();
        List<DO.Test> GetTests();        
        //---------------------------------------------
        List<DO.Test> GetSomeTests(Predicate<DO.Test>someFunc );
        List<DO.Tester> GetSomeTesters(Predicate<DO.Tester> func );
        List<DO.Trainee> GetSomeTrainies(Predicate<DO.Trainee> func );

        Dictionary<string, object> getConfig();
        void SetConfig(string parm, object value);
        bool[,] GetSchedule(string ID);
    }
}

