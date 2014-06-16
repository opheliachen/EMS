@ECHO off

mysql --user=root --password=123456 < EducateManagement_BasicCreate.sql

mysql --default-character-set=utf8 --user=root --password=123456 educatemanagement < usbbackupfile.sql