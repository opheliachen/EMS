::ECHO off

set outputpath="sqls\"
set un="root"
set pw="123456"
set dbname="educatemanagement"
set startdate="2013-12-01"
set enddate="2014-06-30"

mysqldump -t --default-character-set=big5 --no-create-db --no-create-info --extended-insert=false --user=%un% --password=%pw% %dbname% class > %outputpath%class.sql
mysqldump -t --default-character-set=big5 --no-create-db --no-create-info --extended-insert=false --user=%un% --password=%pw% %dbname% classcategory > %outputpath%classcategory.sql
mysqldump -t --default-character-set=big5 --no-create-db --no-create-info --extended-insert=false --user=%un% --password=%pw% %dbname% classpayment --where="PayDate >= '%startdate%' and PayDate <= '%enddate%' Order by PayDate" > %outputpath%classpayment.sql
mysqldump -t --default-character-set=big5 --no-create-db --no-create-info --extended-insert=false --user=%un% --password=%pw% %dbname% classrefunddetail --where="RefundId In (Select Id From classrefunded Where RefundDate >= '%startdate%' and RefundDate <= '%enddate%')" > %outputpath%classrefunddetail.sql
mysqldump -t --default-character-set=big5 --no-create-db --no-create-info --extended-insert=false --user=%un% --password=%pw% %dbname% classrefunded --where="RefundDate >= '%startdate%' and RefundDate <= '%enddate%' Order by RefundDate" > %outputpath%classrefunded.sql
mysqldump -t --default-character-set=big5 --no-create-db --no-create-info --extended-insert=false --user=%un% --password=%pw% %dbname% companyinfo > %outputpath%companyinfo.sql
mysqldump -t --default-character-set=big5 --no-create-db --no-create-info --extended-insert=false --user=%un% --password=%pw% %dbname% expanse --where="InsertDate >= '%startdate%' and InsertDate <= '%enddate%' Order by InsertDate" > %outputpath%expanse.sql
mysqldump -t --default-character-set=big5 --no-create-db --no-create-info --extended-insert=false --user=%un% --password=%pw% %dbname% expansecategory > %outputpath%expansecategory.sql
mysqldump -t --default-character-set=big5 --no-create-db --no-create-info --extended-insert=false --user=%un% --password=%pw% %dbname% prepaidhistory --where="Date >= '%startdate%' and Date <= '%enddate%' Order by Date" > %outputpath%prepaidhistory.sql
mysqldump -t --default-character-set=big5 --no-create-db --no-create-info --extended-insert=false --user=%un% --password=%pw% %dbname% staff > %outputpath%staff.sql
mysqldump -t --default-character-set=big5 --no-create-db --no-create-info --extended-insert=false --user=%un% --password=%pw% %dbname% staffaccount > %outputpath%staffaccount.sql
mysqldump -t --default-character-set=big5 --no-create-db --no-create-info --extended-insert=false --user=%un% --password=%pw% %dbname% staffrole > %outputpath%staffrole.sql
mysqldump -t --default-character-set=big5 --no-create-db --no-create-info --extended-insert=false --user=%un% --password=%pw% %dbname% stafftype > %outputpath%stafftype.sql
mysqldump -t --default-character-set=big5 --no-create-db --no-create-info --extended-insert=false --user=%un% --password=%pw% %dbname% student > %outputpath%student.sql
mysqldump -t --default-character-set=big5 --no-create-db --no-create-info --extended-insert=false --user=%un% --password=%pw% %dbname% studentinclass --where="EndDate >= '%startdate%' and EndDate <= '%enddate%' Order by EndDate" > %outputpath%studentinclass.sql
::mysqldump -t --default-character-set=big5 --no-create-db --no-create-info --extended-insert=false --user=%un% --password=%pw% %dbname% systemlogs > %outputpath%systemlogs.sql

pause
