# Quickbase
Abstract out connection to Quickbase, much as Dapper does for SQL, and allow for simple manipulation of Quickbase' variable table names and field ids

* Provide an abstracted http client to handle CRUD to Quickbase, using Quickbase API
* Provide attributes to decorate classes to identify the Quickbase DBID
* Provide attributes to decorate properties to idenfity the Quickbase FID
* Create mechanism for mapping DBID/FID at runtime rather than with attirbutes
