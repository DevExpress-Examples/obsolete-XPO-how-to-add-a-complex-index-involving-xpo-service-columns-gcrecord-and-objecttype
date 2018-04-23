Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Xpo
Imports System.ComponentModel

Namespace ConsoleApplication1
	Public Class BasePersistentClass
		Inherits XPObject
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub

		Private privateUniqueAgainstObjectTypeInBaseClass As String
		<Indexed("ObjectType", Unique := True)> _
		Public Property UniqueAgainstObjectTypeInBaseClass() As String
			Get
				Return privateUniqueAgainstObjectTypeInBaseClass
			End Get
			Set(ByVal value As String)
				privateUniqueAgainstObjectTypeInBaseClass = value
			End Set
		End Property

		Private privateUniqueAgainstGCRecordInBaseClass As String
		<Indexed("GCRecord", Unique := True)> _
		Public Property UniqueAgainstGCRecordInBaseClass() As String
			Get
				Return privateUniqueAgainstGCRecordInBaseClass
			End Get
			Set(ByVal value As String)
				privateUniqueAgainstGCRecordInBaseClass = value
			End Set
		End Property
	End Class
	Public Class DerivedPersistentClass
		Inherits BasePersistentClass
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub

		Private privateUniqueAgainstObjectTypeInDerivedClass As String
		<Indexed("ObjectTypeCopy", Unique := True)> _
		Public Property UniqueAgainstObjectTypeInDerivedClass() As String
			Get
				Return privateUniqueAgainstObjectTypeInDerivedClass
			End Get
			Set(ByVal value As String)
				privateUniqueAgainstObjectTypeInDerivedClass = value
			End Set
		End Property

		Private privateUniqueAgainstGCRecordInDerivedClass As String
		<Indexed("GCRecordCopy", Unique := True)> _
		Public Property UniqueAgainstGCRecordInDerivedClass() As String
			Get
				Return privateUniqueAgainstGCRecordInDerivedClass
			End Get
			Set(ByVal value As String)
				privateUniqueAgainstGCRecordInDerivedClass = value
			End Set
		End Property

		' To add uniqueness on the service columns in the derived class, 
		' you should declare additional *persistent* clone-properties 
		' that will return the value of corresponding source property.
		<Persistent, Browsable(False)> _
		Protected ReadOnly Property ObjectTypeCopy() As XPObjectType
			Get
				Return Session.GetObjectType(Me)
			End Get
		End Property
		<Persistent, Browsable(False)> _
		Protected ReadOnly Property GCRecordCopy() As Integer?
			Get
				Return GetPropertyValue(Of Integer?)("GCRecord")
			End Get
		End Property
	End Class
End Namespace
