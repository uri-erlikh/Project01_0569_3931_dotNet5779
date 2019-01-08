using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IBL
    {
        void AddTester(BO.Tester tester, bool[,] matrix);
        void DeleteTester(string TesterID);
        void UpdateTester(string testerID, string field, params object[] info);
        BO.Tester GetOneTester(string ID);
        //---------------------------------------------
        void AddTrainee(BO.Trainee trainee);
        void DeleteTrainee(string TraineeID, BO.Vehicle vehicle);
        void UpdateTrainee(string traineeID, string field, params object[] info);
        BO.Trainee GetOneTrainee(string ID, BO.Vehicle vehicle);
        //-------------------------------------------------------
        string AddTest(BO.Test test);
        void UpdateTestResult(int NumOfTest, bool[]result,string note);
        BO.Test GetOneTest(int TestNum);
        //-----------------------------------------
        List<BO.Tester> GetTesters();
        List<BO.Trainee> GetTrainees();
        List<BO.Test> GetTests();
        //---------------------------------------------
        List<BO.Tester> GetCloseTester(BO.Address address, double x);
        List<BO.Tester> GetTestersByDate(DateTime hour);
        List<BO.Test> GetSomeTests(Predicate<BO.Test> someFunc);
        
        int NumOfTest(string id, BO.Vehicle vehicle);
        bool IfPassed(string id, BO.Vehicle vehicle);
        List<BO.Test> TestsPerDate(DateTime date);
        List<BO.Test> TestsPerMonth(DateTime date);
        List<BO.TesterTest> GetFutureTestForTester(string ID);

        //List<BO.Tester> TestersByVehicle(bool flag);

        Dictionary<string, object> GetConfig();
        void SetConfig(string parm, object value);
        List<IGrouping<BO.Vehicle, BO.Tester> > TestersByVehicle(bool flag);
        List<IGrouping<string, BO.Trainee>> TraineesBySchool (bool flag);
        List<IGrouping<string, BO.Trainee>> TraineesByTeacher(bool flag);
        List<IGrouping<int, BO.Trainee>> TraineesByTests(bool flag);
    }
    
}

