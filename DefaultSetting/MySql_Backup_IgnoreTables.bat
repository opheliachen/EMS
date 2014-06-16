@ECHO off

mysqldump --databases --default-character-set=big5 --no-create-db --no-create-info --extended-insert=false educatemanagement --ignore-table=educatemanagement.systemlogs --user=root --password=123456 > backupfile.sql