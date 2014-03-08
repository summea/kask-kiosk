Database and LINQ Issue
=======================

## Problem:
In order to save data to the overall "Applied" database table, we need to (in some way) manually save Applicant, Application, and Job (number) information to the Applied database table. LINQ requires (see _LINQ Error Message_ section below) that there be a Primary Key in the Applied database table. At this time, unfortunately, we do not have a Primary Key in the Applied table.

    Exception details: System.InvalidOperationException: Can't perform Create, Update, or Delete operations on 'Table(Applied)' because it has no primary key.
       at System.Data.Linq.Table`1.CheckReadOnly()
       at System.Data.Linq.Table`1.InsertOnSubmit(TEntity entity)
       at Kask.Services.AESApplicationService.CreateApplied(Applied a) in c:\Users\andy\Desktop\kask-kiosk\Kask.Services\AESApplicationService.svc.cs:line 250
       at SyncInvokeCreateApplied(Object , Object[] , Object[] )</pre>

## Options:
- Create a simple auto incrementing INT primary key in the Applied table. (recommended due to time constraints)
- Use existing columns as a combined primary key.

## Second Problem:
How can we correctly update the primary key column(s) in the Kask Kiosk VS solution?
       
### Debug Notes:

- [Trace Output](http://blogs.msdn.com/b/govindr/archive/2006/11/01/debugging-wcf-traces-and-message-logs.aspx)
- [Optional Debug Switch](http://stackoverflow.com/questions/8315633/turn-on-includeexceptiondetailinfaults-either-from-servicebehaviorattribute-or)