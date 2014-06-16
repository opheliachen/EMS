@ECHO off

set outputpath="sqls\"
set un="root"
set pw="123456"
set dbname="educatemanagement"

::db structure
mysql --user=%un% --password=%pw% < EducateManagement_BasicCreate.sql

::basic data
mysql --default-character-set=utf8 --user=%un% --password=%pw% %dbname% < %outputpath%classcategory.sql
mysql --default-character-set=utf8 --user=%un% --password=%pw% %dbname% < %outputpath%class.sql
mysql --default-character-set=utf8 --user=%un% --password=%pw% %dbname% < %outputpath%companyinfo.sql
mysql --default-character-set=utf8 --user=%un% --password=%pw% %dbname% < %outputpath%expansecategory.sql
mysql --default-character-set=utf8 --user=%un% --password=%pw% %dbname% < %outputpath%expanse.sql
mysql --default-character-set=utf8 --user=%un% --password=%pw% %dbname% < %outputpath%stafftype.sql
mysql --default-character-set=utf8 --user=%un% --password=%pw% %dbname% < %outputpath%staffrole.sql
mysql --default-character-set=utf8 --user=%un% --password=%pw% %dbname% < %outputpath%staffaccount.sql
mysql --default-character-set=utf8 --user=%un% --password=%pw% %dbname% < %outputpath%staff.sql
mysql --default-character-set=utf8 --user=%un% --password=%pw% %dbname% < %outputpath%student.sql
::mysql --default-character-set=utf8 --user=%un% --password=%pw% %dbname% < %outputpath%systemlogs.sql

::refer data
mysql --default-character-set=utf8 --user=%un% --password=%pw% %dbname% < %outputpath%classpayment.sql
mysql --default-character-set=utf8 --user=%un% --password=%pw% %dbname% < %outputpath%classrefunded.sql
mysql --default-character-set=utf8 --user=%un% --password=%pw% %dbname% < %outputpath%classrefunddetail.sql
mysql --default-character-set=utf8 --user=%un% --password=%pw% %dbname% < %outputpath%prepaidhistory.sql
mysql --default-character-set=utf8 --user=%un% --password=%pw% %dbname% < %outputpath%studentinclass.sql

pause