using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//test!
namespace BL
{
    public interface IBL
    {
        void AddTester(BO.Tester tester);
        void DeleteTester(string TesterID);
        void UpdateTester(string testerFamilyName, string TesterPrivateName, int num, params object[] info);
        //לפי סיפרת הביקורת נדע איך להשתמש.
        //נוכל להיעזר בפונקציית gettypecode וכמובן לבדוק חריגות.
        //כל דרך אחרת תתקבל בברכה...
        void AddTrainee(BO.Trainee trainee);
        void DeleteTrainee(string TraineeID);
        void UpdateTrainee(string traineeFamilyName, string TraineePrivateName, int num, params object[] info);

        void AddTest(BO.Test test);
        void UpdateTestResult(bool Passed);

        List<BO.Tester> GetTesters();
        List<BO.Trainee> GetTrainees();
        List<BO.Test> GetTests();

        BO.Tester GetOneTester(string ID);
        BO.Trainee GetOneTrainee(string ID);
        BO.Test GetOneTest(string IDTester, string IDTrainee);
        bool IfExist(string ID, string type);
        void AddCriterion(string newCrit);//פונקציה להוספת קריטריון בכל המבחנים הרשומים במערכת. אני מציע כי הערך 
                                          //של הנתון הבוליאני יהיה אמת באופן דיפולטיבי


    }
}

