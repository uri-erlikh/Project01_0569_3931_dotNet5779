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
        void UpdateTester(BO.Tester tester);
        BO.Tester GetOneTester(string ID);
        //---------------------------------------------
        void AddTrainee(BO.Trainee trainee);
        void DeleteTrainee(string TraineeID, BO.Vehicle vehicle);
        void UpdateTrainee(BO.Trainee trainee);
        BO.Trainee GetOneTrainee(string ID, BO.Vehicle vehicle);
        //-------------------------------------------------------
        string AddTest(BO.Test test);
        void UpdateTestResult(BO.Test test);
        BO.Test GetOneTest(int TestNum);
        void DeleteTest(int numOfTest);
        //-----------------------------------------
        List<BO.Tester> GetTesters();
        List<BO.Trainee> GetTrainees();
        List<BO.Test> GetTests();
        //---------------------------------------------
        List<BO.Tester> GetCloseTester(BO.Address address, double x);
        List<BO.Tester> GetTestersByDate(DateTime hour);
        List<BO.Test> GetSomeTests(Predicate<BO.Test> someFunc);
         List<DateTime> GetDateOfTests(DateTime fromDate, DateTime untilDate, string city, string street, int numbilding, BO.Vehicle vehicle);

        int NumOfTest(string id, BO.Vehicle vehicle);
        bool IfPassed(string id, BO.Vehicle vehicle);
        List<BO.Test> TestsPerDate(DateTime date);
        List<BO.Test> TestsPerMonth(DateTime date);
        List<BO.TesterTest> GetFutureTestForTester(string ID);
        List<BO.TraineeTest> GetFutureTestForTrainee(string ID, BO.Vehicle vehicle);

        //List<BO.Tester> TestersByVehicle(bool flag);

        Dictionary<string, object> GetConfig();
        void SetConfig(string parm, object value);
        List<IGrouping<BO.Vehicle, BO.Tester>> TestersByVehicle(bool flag);
        List<IGrouping<string, BO.Trainee>> TraineesBySchool(bool flag);
        List<IGrouping<string, BO.Trainee>> TraineesByTeacher(bool flag);
        List<IGrouping<int, BO.Trainee>> TraineesByTests(bool flag);
    }

}

