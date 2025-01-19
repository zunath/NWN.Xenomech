@ECHO off 
SETLOCAL EnableExtensions EnableDelayedExpansion
SET "parent_dir=%~dp0"
if not exist out mkdir out


nwn_crunch.exe -file in\ -outdir out\ -fileformat tga -deep -recreate -noprogress -nostats
