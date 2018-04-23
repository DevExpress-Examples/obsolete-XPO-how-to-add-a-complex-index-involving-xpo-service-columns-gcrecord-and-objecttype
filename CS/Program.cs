using System.Data;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.DB.Exceptions;
using DevExpress.Xpo.Metadata;
using NUnit.Framework;
using DevExpress.Xpo.DB.Helpers;

namespace ConsoleApplication1 {
    class Program {
        static void Main(string[] args) { }
    }
    [TestFixture]
    public class E1484_Test {
        [SetUp]
        public void Setup() {
            XPDictionary dict = new ReflectionDictionary();
            dict.GetDataStoreSchema(typeof(BasePersistentClass).Assembly);
            XpoDefault.DataLayer = new SimpleDataLayer(dict, new InMemoryDataStore(AutoCreateOption.DatabaseAndSchema));
            XpoDefault.Session = null;
        }
        [Test, ExpectedException(typeof(ConstraintViolationException))]
        public void Test_Requirement_1_BaseTypes() {
            using(UnitOfWork uow = new UnitOfWork()) {
                BasePersistentClass obj1 = new BasePersistentClass(uow);
                obj1.UniqueAgainstObjectTypeInBaseClass = "non-unique";
                //other obj1 properties are initialized with NULL values by default.
                BasePersistentClass obj2 = new BasePersistentClass(uow);
                obj2.UniqueAgainstObjectTypeInBaseClass = "non-unique";
                obj2.UniqueAgainstGCRecordInBaseClass = "unique";
                uow.CommitChanges();
            }
        }
        [Test, ExpectedException(typeof(ConstraintViolationException))]
        public void Test_Requirement_1_DerivedTypes() {
            using(UnitOfWork uow = new UnitOfWork()) {
                DerivedPersistentClass obj1 = new DerivedPersistentClass(uow);
                obj1.UniqueAgainstObjectTypeInDerivedClass = "non-unique";
                //other obj1 properties are initialized with NULL values by default.
                DerivedPersistentClass obj2 = new DerivedPersistentClass(uow);
                obj2.UniqueAgainstObjectTypeInBaseClass = "unique";
                obj2.UniqueAgainstObjectTypeInDerivedClass = "non-unique";
                obj2.UniqueAgainstGCRecordInBaseClass = "unique";
                obj2.UniqueAgainstGCRecordInDerivedClass = "unique";
                uow.CommitChanges();
            }
        }
        [Test]
        public void Test_Requirement_1_BaseAndDerivedTypes() {
            using(UnitOfWork uow = new UnitOfWork()) {
                BasePersistentClass obj1 = new BasePersistentClass(uow);
                obj1.UniqueAgainstObjectTypeInBaseClass = "same non-unique value is allowed for records of different types";
                //other obj1 properties are initialized with NULL values by default.
                DerivedPersistentClass obj2 = new DerivedPersistentClass(uow);
                obj2.UniqueAgainstObjectTypeInBaseClass = "same non-unique value is allowed for records of different types";
                obj2.UniqueAgainstGCRecordInBaseClass = "unique";
                uow.CommitChanges();
            }
        }
        [Test]
        public void Test_Requirement_2_BaseTypes() {
            using(UnitOfWork uow = new UnitOfWork()) {
                BasePersistentClass obj1 = new BasePersistentClass(uow);
                obj1.UniqueAgainstGCRecordInBaseClass = "same value is allowed, because deleted records are not taken into account";
                //other obj1 properties are initialized with NULL values by default.
                uow.CommitChanges();
                uow.Delete(obj1);
                uow.CommitChanges();
                BasePersistentClass obj2 = new BasePersistentClass(uow);
                obj2.UniqueAgainstGCRecordInBaseClass = "same value is allowed, because deleted records are not taken into account";
                obj2.UniqueAgainstObjectTypeInBaseClass = "unique";
                uow.CommitChanges();
            }
        }
        [Test]
        public void Test_Requirement_2_DerivedTypes() {
            using(UnitOfWork uow = new UnitOfWork()) {
                DerivedPersistentClass obj1 = new DerivedPersistentClass(uow);
                obj1.UniqueAgainstGCRecordInDerivedClass = "same value is allowed, because deleted records are not taken into account";
                //other obj1 properties are initialized with NULL values by default.
                uow.CommitChanges();
                uow.Delete(obj1);
                uow.CommitChanges();
                DerivedPersistentClass obj2 = new DerivedPersistentClass(uow);
                obj2.UniqueAgainstGCRecordInDerivedClass = "same value is allowed, because deleted records are not taken into account";
                obj2.UniqueAgainstGCRecordInBaseClass = "unique";
                obj2.UniqueAgainstObjectTypeInBaseClass = "unique";
                obj2.UniqueAgainstObjectTypeInDerivedClass = "unique";
                uow.CommitChanges();
            }
        }
        [Test]
        public void Test_Requirement_2_BaseAndDerivedTypes() {
            using(UnitOfWork uow = new UnitOfWork()) {
                BasePersistentClass obj1 = new BasePersistentClass(uow);
                obj1.UniqueAgainstGCRecordInBaseClass = "same value is allowed, because deleted records are not taken into account";
                //other obj1 properties are initialized with NULL values by default.
                uow.CommitChanges();
                uow.Delete(obj1);
                uow.CommitChanges();
                DerivedPersistentClass obj2 = new DerivedPersistentClass(uow);
                obj2.UniqueAgainstGCRecordInBaseClass = "same value is allowed, because deleted records are not taken into account";
                obj2.UniqueAgainstObjectTypeInBaseClass = "unique";
                uow.CommitChanges();
            }
        }
    }
}