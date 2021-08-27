<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128585509/14.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E1484)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [BO.cs](./CS/BO.cs) (VB: [BO.vb](./VB/BO.vb))
* **[Program.cs](./CS/Program.cs) (VB: [Program.vb](./VB/Program.vb))**
<!-- default file list end -->
# How to add a complex index involving XPO service columns (GCRecord and ObjectType)


<p><strong>Scenario</strong><br />This article demonstrates how to use the <a href="http://documentation.devexpress.com/#XPO/clsDevExpressXpoIndexedAttributetopic">IndexedAttribute</a> with the <a href="http://documentation.devexpress.com/#XPO/CustomDocument2632">service XPO columns</a> (ObjectType and GCRecord) to accomplish the following requirements:</p>
<p><strong>1.</strong> Make persistent objects data unique against the class type;<br /><strong>2.</strong> Avoid data duplication with records that are already deleted, but are still physically present in the database due to the enabled <a href="http://documentation.devexpress.com/#XPO/CustomDocument2103">deferred deletion</a>.</p>
<p><br /><strong>Steps to implement<br />1.</strong> Decorate the required data property, which must be unique against the object type with the <em>DevExpress.Xpo.Indexed</em> attribute taking "ObjectType" and Unique = true as parameters.<br /><strong>2.</strong> Decorate the required data property, which must be unique without taking into account deleted records data with the <em>DevExpress.Xpo.Indexed </em>attribute taking "GCRecord" and Unique = true as parameters.<br />Check out the BasePersistentClass and DerivedPersistentClass classes in the <em>BO.xx</em> file and unit tests within the <em>Program.xx</em> file for more details.<br /><br /><strong>IMPORTANT NOTES</strong></p>
<p><strong>1.</strong> Beware of MS Access limitation which lies in skipping NULL values when checking value uniqueness.<br /><strong>2.</strong> The IndexedAttribute/IndicesAttribute involving the service ObjectType and GCRecord columns can be used only in the base persistent class, because XPO creates these columns only in the base table. <br />To add uniqueness on the service columns in the derived class, you should declare additional persistent clone-properties that will return the value of the corresponding source property.  This approach with clone-properties in the descendant table is not very effective, because of data denormalization, so use it only if no other solutions are possible. For instance, as an alternative, it is possible to map descendant class data to the parent table using <a href="https://documentation.devexpress.com/#XPO/CustomDocument2125">Inheritance Mapping</a>. This way, you can create an index based on service columns directly.</p>
<p><br /><strong>See Also:<br /></strong><a href="https://documentation.devexpress.com/#XPO/clsDevExpressXpoIndexedAttributetopic">IndexedAttribute Class</a> <br /><a href="https://documentation.devexpress.com/#XPO/clsDevExpressXpoIndicesAttributetopic">IndicesAttribute Class</a> <br /><a href="http://msdn.microsoft.com/en-us/library/aa214372.aspx?ppud=4">Table Indexes</a></p>

<br/>


