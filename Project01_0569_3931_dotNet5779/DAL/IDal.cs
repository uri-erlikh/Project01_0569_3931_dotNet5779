using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDal
    {//
        void AddTester(DO.Tester tester, bool [,]matrix);
        void DeleteTester(string TesterID);
        void UpdateTester(DO.Tester tester);        
        DO.Tester GetOneTester(string ID);
        //---------------------------------------------
        void AddTrainee(DO.Trainee trainee);
        void DeleteTrainee(string traineeID, DO.Vehicle vehicle);
        void UpdateTrainee(DO.Trainee trainee);
        DO.Trainee GetOneTrainee(string ID, DO.Vehicle vehicle);
        //-------------------------------------------------------
        string AddTest(DO.Test test);
        void UpdateTestResult(DO.Test test);
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

        Dictionary<string, object> GetConfig();
        void SetConfig(string parm, object value);
        bool[,] GetSchedule(string ID);
        void SetSchedule(bool[,] schedule, string testerID);
    }
}

