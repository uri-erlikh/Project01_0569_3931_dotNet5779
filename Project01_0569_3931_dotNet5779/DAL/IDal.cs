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
        void UpdateTester(string testerFamilyName, string TesterPrivateName, int num, params object[] info);
        //לפי סיפרת הביקורת נדע איך להשתמש.
        //נוכל להיעזר בפונקציית gettypecode וכמובן לבדוק חריגות.
        //כל דרך אחרת תתקבל בברכה...
        DO.Tester GetOneTester(string ID);
        //---------------------------------------------
        void AddTrainee(DO.Trainee trainee);
        void DeleteTrainee(string TraineeID);
        void UpdateTrainee(string traineeFamilyName, string TraineePrivateName, int num, params object[] info);
        DO.Trainee GetOneTrainee(string ID);
//-------------------------------------------------------
        void AddTest(DO.Test test);
        void UpdateTestResult(bool Passed);
        DO.Test GetOneTest(int TestNum);


        List<DO.Tester> GetTesters();
        List<DO.Trainee> GetTrainees();
        List<DO.Test> GetTests();

        List<DO.Test> GetSomeTests(Predicate<bool> func );
        List<DO.Tester> GetSomeTesters(Predicate<bool> func );
        List<DO.Trainee> GetSomeTrainees(Predicate<bool> func );

        Dictionary<String, Object> getConfig();
        void SetConfig(String parm, Object value)

         bool IfExist(string ID, )
        void AddCriterion(string newCrit);//פונקציה להוספת קריטריון בכל המבחנים הרשומים במערכת. אני מציע כי הערך 
        //של הנתון הבוליאני יהיה אמת באופן דיפולטיבי
    }
}

