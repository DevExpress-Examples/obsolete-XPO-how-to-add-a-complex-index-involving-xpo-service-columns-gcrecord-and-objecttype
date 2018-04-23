using System;
using DevExpress.Xpo;
using System.ComponentModel;

namespace ConsoleApplication1 {
    public class BasePersistentClass : XPObject {
        public BasePersistentClass(Session session) : base(session) { }

        [Indexed("ObjectType", Unique = true)]
        public string UniqueAgainstObjectTypeInBaseClass { get; set; }
        
        [Indexed("GCRecord", Unique = true)]
        public string UniqueAgainstGCRecordInBaseClass { get; set; }
    }
    public class DerivedPersistentClass : BasePersistentClass {
        public DerivedPersistentClass(Session session) : base(session) { }
        
        [Indexed("ObjectTypeCopy", Unique = true)]
        public string UniqueAgainstObjectTypeInDerivedClass { get; set; }
        
        [Indexed("GCRecordCopy", Unique = true)]
        public string UniqueAgainstGCRecordInDerivedClass { get; set; }

        // To add uniqueness on the service columns in the derived class, 
        // you should declare additional *persistent* clone-properties 
        // that will return the value of corresponding source property.
        [Persistent, Browsable(false)]
        protected XPObjectType ObjectTypeCopy {
            get { return Session.GetObjectType(this); }
        }
        [Persistent, Browsable(false)]
        protected int? GCRecordCopy {
            get { return GetPropertyValue<int?>("GCRecord"); }
        }
    }
}
