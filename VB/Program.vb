Imports Microsoft.VisualBasic
Imports System.Data
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo.DB.Exceptions
Imports DevExpress.Xpo.Metadata
Imports NUnit.Framework
Imports DevExpress.Xpo.DB.Helpers

Namespace ConsoleApplication1
	Friend Class Program
		Shared Sub Main(ByVal args() As String)
		End Sub
	End Class
	<TestFixture> _
	Public Class E1484_Test
		<SetUp> _
		Public Sub Setup()
			Dim dict As XPDictionary = New ReflectionDictionary()
			dict.GetDataStoreSchema(GetType(BasePersistentClass).Assembly)
			XpoDefault.DataLayer = New SimpleDataLayer(dict, New InMemoryDataStore(AutoCreateOption.DatabaseAndSchema))
			XpoDefault.Session = Nothing
		End Sub
		<Test, ExpectedException(GetType(ConstraintViolationException))> _
		Public Sub Test_Requirement_1_BaseTypes()
			Using uow As New UnitOfWork()
				Dim obj1 As New BasePersistentClass(uow)
				obj1.UniqueAgainstObjectTypeInBaseClass = "non-unique"
				'other obj1 properties are initialized with NULL values by default.
				Dim obj2 As New BasePersistentClass(uow)
				obj2.UniqueAgainstObjectTypeInBaseClass = "non-unique"
				obj2.UniqueAgainstGCRecordInBaseClass = "unique"
				uow.CommitChanges()
			End Using
		End Sub
		<Test, ExpectedException(GetType(ConstraintViolationException))> _
		Public Sub Test_Requirement_1_DerivedTypes()
			Using uow As New UnitOfWork()
				Dim obj1 As New DerivedPersistentClass(uow)
				obj1.UniqueAgainstObjectTypeInDerivedClass = "non-unique"
				'other obj1 properties are initialized with NULL values by default.
				Dim obj2 As New DerivedPersistentClass(uow)
				obj2.UniqueAgainstObjectTypeInBaseClass = "unique"
				obj2.UniqueAgainstObjectTypeInDerivedClass = "non-unique"
				obj2.UniqueAgainstGCRecordInBaseClass = "unique"
				obj2.UniqueAgainstGCRecordInDerivedClass = "unique"
				uow.CommitChanges()
			End Using
		End Sub
		<Test> _
		Public Sub Test_Requirement_1_BaseAndDerivedTypes()
			Using uow As New UnitOfWork()
				Dim obj1 As New BasePersistentClass(uow)
				obj1.UniqueAgainstObjectTypeInBaseClass = "same non-unique value is allowed for records of different types"
				'other obj1 properties are initialized with NULL values by default.
				Dim obj2 As New DerivedPersistentClass(uow)
				obj2.UniqueAgainstObjectTypeInBaseClass = "same non-unique value is allowed for records of different types"
				obj2.UniqueAgainstGCRecordInBaseClass = "unique"
				uow.CommitChanges()
			End Using
		End Sub
		<Test> _
		Public Sub Test_Requirement_2_BaseTypes()
			Using uow As New UnitOfWork()
				Dim obj1 As New BasePersistentClass(uow)
				obj1.UniqueAgainstGCRecordInBaseClass = "same value is allowed, because deleted records are not taken into account"
				'other obj1 properties are initialized with NULL values by default.
				uow.CommitChanges()
				uow.Delete(obj1)
				uow.CommitChanges()
				Dim obj2 As New BasePersistentClass(uow)
				obj2.UniqueAgainstGCRecordInBaseClass = "same value is allowed, because deleted records are not taken into account"
				obj2.UniqueAgainstObjectTypeInBaseClass = "unique"
				uow.CommitChanges()
			End Using
		End Sub
		<Test> _
		Public Sub Test_Requirement_2_DerivedTypes()
			Using uow As New UnitOfWork()
				Dim obj1 As New DerivedPersistentClass(uow)
				obj1.UniqueAgainstGCRecordInDerivedClass = "same value is allowed, because deleted records are not taken into account"
				'other obj1 properties are initialized with NULL values by default.
				uow.CommitChanges()
				uow.Delete(obj1)
				uow.CommitChanges()
				Dim obj2 As New DerivedPersistentClass(uow)
				obj2.UniqueAgainstGCRecordInDerivedClass = "same value is allowed, because deleted records are not taken into account"
				obj2.UniqueAgainstGCRecordInBaseClass = "unique"
				obj2.UniqueAgainstObjectTypeInBaseClass = "unique"
				obj2.UniqueAgainstObjectTypeInDerivedClass = "unique"
				uow.CommitChanges()
			End Using
		End Sub
		<Test> _
		Public Sub Test_Requirement_2_BaseAndDerivedTypes()
			Using uow As New UnitOfWork()
				Dim obj1 As New BasePersistentClass(uow)
				obj1.UniqueAgainstGCRecordInBaseClass = "same value is allowed, because deleted records are not taken into account"
				'other obj1 properties are initialized with NULL values by default.
				uow.CommitChanges()
				uow.Delete(obj1)
				uow.CommitChanges()
				Dim obj2 As New DerivedPersistentClass(uow)
				obj2.UniqueAgainstGCRecordInBaseClass = "same value is allowed, because deleted records are not taken into account"
				obj2.UniqueAgainstObjectTypeInBaseClass = "unique"
				uow.CommitChanges()
			End Using
		End Sub
	End Class
End Namespace