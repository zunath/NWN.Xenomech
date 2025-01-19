@ECHO off
SETLOCAL EnableExtensions EnableDelayedExpansion
SET "parent_dir=%~dp0"
if not exist out mkdir out
IF EXIST log.txt del log.txt


REM Internal options
REM We'll keep everything in a single base script and change options for every script
REM Switch between standard and bioware dds files
SET "opt_bioware_dds=0"


REM Options
REM Default values
SET "opt_auto_flip=1"
REM Read from settings.ini
FOR /f "tokens=1,2 delims==" %%a IN (settings.ini) DO (
IF %%a==AUTO_FLIP SET opt_auto_flip=%%b
)


REM Different options for bioware and standard dds files
if !opt_bioware_dds! GTR 0 (
    REM Conversion options for bioware dds files
    SET "crunch_options= -noprogress -nostats -fileformat nwn -DXT1 -deep -recreate -mipmode none -logfile log.txt"
) ELSE (
    REM Conversion options for standard dds files
    SET "crunch_options= -noprogress -nostats -fileformat dds -DXT1 -deep -recreate -mipmode none -logfile log.txt"
    REM Standard dds need to be flipped
    IF !opt_auto_flip! GTR 0 (
        SET "crunch_options=!crunch_options! -unflip -yflip"
    )
)


nwn_crunch.exe -file in\ -outdir out\ !crunch_options!
