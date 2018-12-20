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
        void AddTrainee(DO.Trainee trainee);
        void DeleteTrainee(string TraineeID);
        void UpdateTrainee(string traineeFamilyName, string TraineePrivateName, int num, params object[] info);

        void AddTest(DO.Test test);
        void UpdateTestResult(bool Passed);

        List<DO.Tester> GetTesters();
        List<DO.Trainee> GetTrainees();
        List<DO.Test> GetTests();

        DO.Tester GetOneTester(string ID);
        DO.Trainee GetOneTrainee(string ID);
        DO.Test GetOneTest(string IDTester, string IDTrainee);
        bool IfExist(string ID, )
        void AddCriterion(string newCrit);//פונקציה להוספת קריטריון בכל המבחנים הרשומים במערכת. אני מציע כי הערך 
        //של הנתון הבוליאני יהיה אמת באופן דיפולטיבי
    }
}

