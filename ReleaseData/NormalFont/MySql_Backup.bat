@ECHO off
cls
set DBchoice=%1%
set User=%2%
set Password=%3%
set pathchoice=%4%

@REM Remove double quotes from the path
@REM SET pathchoice=%pathchoice:"=%
@REM SET pathchoice=%pathchoice:"=%

mysqldump --routines --add-drop-table -B %DBchoice% -u %User% --password=%Password%  > %pathchoice%