using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDal
    {
        void AddTester(DO.Tester tester);
        void DeleteTester(string TesterID);
        void UpdateTester(string testerID,string field, params object[] info);
        
        DO.Tester GetOneTester(string ID);
        //---------------------------------------------
        void AddTrainee(DO.Trainee trainee);
        void DeleteTrainee(string TraineeID);
        void UpdateTrainee(string traineeID, string field, params object[] info);
        DO.Trainee GetOneTrainee(string ID);
//-------------------------------------------------------
        void AddTest(DO.Test test);
        void UpdateTestResult(int NumOfTest,string field,  object result);
        DO.Test GetOneTest(int TestNum);


        List<DO.Tester> GetTesters();
        List<DO.Trainee> GetTrainees();
        List<DO.Test> GetTests();

        List<DO.Test> GetSomeTests(Predicate<bool> func );
        List<DO.Tester> GetSomeTesters(Predicate<bool> func );
        List<DO.Trainee> GetSomeTrainees(Predicate<bool> func );

        Dictionary<String, Object> getConfig();
        void SetConfig(String parm, Object value);        
    }
}

