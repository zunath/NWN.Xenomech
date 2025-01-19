@ECHO off
SETLOCAL EnableExtensions EnableDelayedExpansion
SET "parent_dir=%~dp0"
if not exist out mkdir out
IF EXIST log.txt del log.txt


REM Internal options
REM We'll keep everything in a single base script and change options for every script
REM Wether to generate mtr files
SET "opt_generate_mtr=0"
REM Switch between standard and bioware dds files
SET "opt_bioware_dds=0"


REM Supported file extensions
SET "supported_ext=.tga .bmp .png .jpg"


REM Options
REM Default values
SET "opt_use_metallicness=1"
SET "opt_use_glossiness=1"
SET "opt_filename_limit=0"
SET "opt_auto_flip=1"
SET "opt_drop_empty_alpha=1"
SET "opt_suffix_delimiter=_"
SET "opt_suffix_remove_delimiter=1"
SET "opt_mtr_shader_vs="
SET "opt_mtr_shader_fs="
SET "opt_mtr_shader_gs="
SET "opt_mtr_renderhint=NormalAndSpecMapped"
REM Read from settings.ini
FOR /f "tokens=1,2 delims==" %%a IN (settings.ini) DO (
IF %%a==USE_METALLICNESS SET opt_use_metallicness=%%b
IF %%a==USE_GLOSSINESS SET opt_use_glossiness=%%b
IF %%a==FILENAME_LIMIT SET opt_filename_limit=%%b
IF %%a==AUTO_FLIP SET opt_auto_flip=%%b
IF %%a==DROP_EMPTY_ALPHA SET opt_drop_empty_alpha=%%b
IF %%a==SUFFIX_DELIMITER SET opt_suffix_delimiter=%%b
IF %%a==SUFFIX_REMOVE_DELIMITER SET opt_suffix_remove_delimiter=%%b
IF %%a==MTR_SHADER_VS SET opt_mtr_shader_vs=%%b
IF %%a==MTR_SHADER_FS SET opt_mtr_shader_fs=%%b
IF %%a==MTR_SHADER_GS SET opt_mtr_shader_gs=%%b
IF %%a==MTR_RENDERHINT SET opt_mtr_renderhint=%%b
)


REM Texture suffixes indicating their type
REM NOTE: # No suffix = diffuse, but suffix_diffuse takes precendence for writing to mtr
SET "suffix_diffuse=d"
SET "suffix_normal=n"
SET "suffix_specular=s"
SET "suffix_metallicness=m"
SET "suffix_roughness=r"
SET "suffix_glossiness=g"
SET "suffix_height=h"
SET "suffix_illumination=i"
FOR /f "tokens=1,2 delims==" %%a IN (settings.ini) DO (
IF %%a==SUFFIX_DIFFUSE SET suffix_diffuse=%%b
IF %%a==SUFFIX_NORMAL SET suffix_normal=%%b
IF %%a==SUFFIX_SPECULAR SET suffix_specular=%%b
IF %%a==SUFFIX_METALLICNESS SET suffix_metallicness=%%b
IF %%a==SUFFIX_ROUGHNESS SET suffix_roughness=%%b
IF %%a==SUFFIX_GLOSSINESS SET suffix_glossiness=%%b
IF %%a==SUFFIX_HEIGHT SET suffix_height=%%b
IF %%a==SUFFIX_ILLUMINATION SET suffix_illumination=%%b
)


REM Different options for bioware and standard dds files
IF !opt_bioware_dds! GTR 0 (
    REM Conversion options for all bioware dds files
    SET "copt_all= -quiet -nostats -fileformat nwn -logfile log.txt"
    REM Additional options by texture type
    SET "copt_diffuse= "
    SET "copt_normal= -DXT1 -normalize -uniformMetrics"
    SET "copt_specular= -DXT1 -setRGBAtoR -uniformMetrics"
    SET "copt_metallicness= -DXT1 -minvalue 5 -setRGBAtoR -uniformMetrics"
    SET "copt_roughness= -DXT1 -setRGBAtoR -uniformMetrics"
    SET "copt_glossiness= -DXT1 -invertcolors -setRGBAtoR -uniformMetrics"
    SET "copt_height= -DXT1 -setRGBAtoR -uniformMetrics"
    SET "copt_illumination= -DXT1"
) ELSE (
    REM Conversion options for all standard dds files
    SET "copt_all= -quiet -nostats -fileformat dds -logfile log.txt"
    REM Standard dds need to be flipped
    IF !opt_auto_flip! GTR 0 (
        SET "copt_all=!copt_all! -unflip -yflip"
    )
    REM Additional options by texture type
    SET "copt_diffuse= "
    SET "copt_normal= -DXN -normalize -uniformMetrics"
    SET "copt_specular= -DXT5A -setAtoY"
    SET "copt_metallicness= -DXT5A -setAtoY -minvalue 5"
    SET "copt_roughness= -DXT5A -setAtoY"
    SET "copt_glossiness= -DXT5A -setAtoY -invertcolors"
    SET "copt_height= -DXT5A -setAtoY"
    SET "copt_illumination= -DXT1"
)
IF !opt_drop_empty_alpha! GTR 0 (
    SET "copt_all=!copt_all! -dropEmptyAlpha"
)


REM Helper to select file types
SET "valid_ext=%supported_ext:.=in\*%"
REM Helper to detect suffixes
SET "suffix_matcher=%opt_suffix_delimiter%[%suffix_diffuse%%suffix_normal%%suffix_specular%%suffix_roughness%%suffix_height%%suffix_illumination%"
REM Optional: Metallicness can be converted to specular (doesn't add the suffix when disabled)
IF !opt_use_metallicness! GTR 0 (
    SET "suffix_matcher=!suffix_matcher!%suffix_metallicness%"
)
REM Optional: Glossiness can be converted to roughness (doesn't add the suffix when disabled)
IF !opt_use_glossiness! GTR 0 (
    SET "suffix_matcher=!suffix_matcher!%suffix_glossiness%"
)
SET "suffix_matcher=!suffix_matcher!]$"


REM Process files
SET "previous_basename=###"
SET "previous_out_dir=out/"
SET "mtr_tex0=null"
SET "mtr_tex1=null"
SET "mtr_tex2=null"
SET "mtr_tex3=null"
SET "mtr_tex4=null"
SET "mtr_tex5=null"
FOR /f "tokens=*" %%F IN ('dir /s /b /o:n %valid_ext%') DO (
    SET "tex_fullpath=%%F"
    SET "tex_name=%%~nF"
    SET "tex_ext=%%~xF"
    REM get the subdir within the in/ directory
    SET "tex_dir=!tex_fullpath:%parent_dir%in=!"
    CALL SET "tex_dir=%%tex_dir:!tex_name!!tex_ext!=%%"

    REM Try to match filename with a suffix to get the basename
    ECHO !tex_name!| FINDSTR /r /i !suffix_matcher!>nul
    If !ERRORLEVEL! EQU 0 (
        REM Found suffix, split filename into basename + suffix
        IF !opt_suffix_remove_delimiter! GTR 0 (
            REM remove both suffix and delimiter
            SET "tex_basename=!tex_name:~0,-2!"
        ) ELSE (
            REM remove only suffix
            SET "tex_basename=!tex_name:~0,-1!"
        )
        SET "tex_suffix=!tex_name:~-1!"
    ) ELSE (
        REM No suffix, assume diffuse (basename = filename)
        SET "tex_basename=!tex_name!"
        SET "tex_suffix=###"
    )

    REM Flush mtr values to file if basename or directory has changed
    IF NOT "!previous_basename!" == "###" (
        SET "flush_mtr="
        IF NOT "!out_dir!" == "!previous_out_dir!" SET flush_mtr=1
        IF NOT "!tex_basename!" == "!previous_basename!" SET flush_mtr=1
        IF DEFINED flush_mtr (
            IF !opt_generate_mtr! GTR 0 (
                IF !opt_filename_limit! GTR 0 (
                    SET "shortened_filename=error"
                    CALL :SHORTEN_FILENAME shortened_filename previous_basename
                    SET "mtr_fullpath=!previous_out_dir!!shortened_filename!.mtr"
                ) ELSE (
                    SET "mtr_fullpath=!previous_out_dir!!previous_basename!.mtr"
                )
                CALL :WRITE_MTR "!mtr_fullpath!" "!mtr_tex0!" "!mtr_tex1!" "!mtr_tex2!" "!mtr_tex3!" "!mtr_tex4!" "!mtr_tex5!"
            )
            SET "mtr_tex0=null"
            SET "mtr_tex1=null"
            SET "mtr_tex2=null"
            SET "mtr_tex3=null"
            SET "mtr_tex4=null"
            SET "mtr_tex5=null"
        )
    )

    ECHO --------------------------------------------------
    ECHO Processing: in!tex_dir!!tex_name!!tex_ext!

    REM If shorten filename is on we need to do it now, before adding the mtr values
    IF !opt_filename_limit! GTR 0 (
        SET "shortened_filename=error"
        CALL :SHORTEN_FILENAME shortened_filename tex_name
        SET tex_name=!shortened_filename!
    )

    REM Generate directory for output file
    SET out_dir=out!tex_dir!
    IF NOT EXIST "!out_dir!" mkdir "!out_dir!"

    REM Set options based on suffix
    SET "out_options=%copt_all%"
    IF /I !tex_suffix! == %suffix_diffuse% (
        ECHO Type: Diffuse
        SET "out_options=!out_options!%copt_diffuse%"
        SET "mtr_tex0=!tex_name!"
    ) ELSE IF /I !tex_suffix! == %suffix_normal% (
        ECHO Type: Normal
        SET "out_options=!out_options!%copt_normal%"
        SET "mtr_tex1=!tex_name!"
    ) ELSE IF /I !tex_suffix! == %suffix_specular% (
        ECHO Type: Specular
        SET "out_options=!out_options!%copt_specular%"
        SET "mtr_tex2=!tex_name!"
    ) ELSE IF /I !tex_suffix! == %suffix_metallicness% (
        ECHO Type: Metallicness ^(Convert to Specular^)
        REM Rename (metallicness will be used as specular)
        if !opt_suffix_remove_delimiter! GTR 0 (
            REM Delimiter was previously removed, re-add it
            SET "tex_name=!tex_basename!%opt_suffix_delimiter%%suffix_specular%"
        ) ELSE (
            SET "tex_name=!tex_basename!%suffix_specular%"
        )
        REM If shorten filename is on we need to re-do it, as we created a new name
        IF !opt_filename_limit! GTR 0 (
            SET "shortened_filename=error"
            CALL :SHORTEN_FILENAME shortened_filename tex_name
            SET tex_name=!shortened_filename!
        )
        SET "out_options=!out_options!%copt_metallicness%"
        SET "mtr_tex2=!tex_name!"
    ) ELSE IF /I !tex_suffix! == %suffix_roughness% (
        ECHO Type: Roughness
        SET "out_options=!out_options!%copt_roughness%"
        SET "mtr_tex3=!tex_name!"
    ) ELSE IF /I !tex_suffix! == %suffix_glossiness% (
        ECHO Type: Glossiness ^(Convert to Roughness^)
        REM Rename (glossiness will be inverted to roughness)
        if !opt_suffix_remove_delimiter! GTR 0 (
            REM Delimiter was previously removed, re-add it
            SET "tex_name=!tex_basename!%opt_suffix_delimiter%%suffix_roughness%"
        ) ELSE (
            SET "tex_name=!tex_basename!%suffix_roughness%"
        )
        REM If shorten filename is on we need to re-do it, as we created a new name
        IF !opt_filename_limit! GTR 0 (
            SET "shortened_filename=error"
            CALL :SHORTEN_FILENAME shortened_filename tex_name
            SET tex_name=!shortened_filename!
        )
        SET "out_options=!out_options!%copt_glossiness%"
        SET "mtr_tex3=!tex_name!"
    ) ELSE IF /I !tex_suffix! == %suffix_height% (
        ECHO Type: Height ^(Displacement^)
        SET "out_options=!out_options!%copt_height%"
        SET "mtr_tex4=!tex_name!"
    ) ELSE IF /I !tex_suffix! == %suffix_illumination% (
        ECHO Type: Illumination
        SET "out_options=!out_options!%copt_illumination%"
        SET "mtr_tex5=!tex_name!"
    ) ELSE (
        ECHO Type: Diffuse ^(No Suffix^)
        SET "out_options=!out_options!%copt_diffuse%"
        REM Don't overwrite existing (suffixed diffuse) in mtr
        IF !mtr_tex0! == null (
            SET "mtr_tex0=!tex_name!"
        )
    )

    REM Generate path for output file
    SET "out_fullpath=!out_dir!!tex_name!.dds"
    ECHO Writing to: !out_fullpath!

    nwn_crunch -file "!tex_fullpath!" -out "!out_fullpath!" !out_options!
    SET "previous_basename=!tex_basename!"
    SET "previous_out_dir=!out_dir!"
)
REM Flush last mtr values
IF !opt_generate_mtr! GTR 0 (
    IF !opt_filename_limit! GTR 0 (
        SET "shortened_filename=error"
        CALL :SHORTEN_FILENAME shortened_filename previous_basename
        SET "mtr_fullpath=!previous_out_dir!!shortened_filename!.mtr"
    ) ELSE (
        SET "mtr_fullpath=!previous_out_dir!!previous_basename!.mtr"
    )
    CALL :WRITE_MTR "!mtr_fullpath!" "!mtr_tex0!" "!mtr_tex1!" "!mtr_tex2!" "!mtr_tex3!" "!mtr_tex4!" "!mtr_tex5!"
)

ECHO --------------------------------------------------
@pause
EXIT /B %ERRORLEVEL%


:WRITE_MTR
SET "filepath=%~1"
ECHO --------------------------------------------------
ECHO Creating MTR: !mtr_fullpath!
SET "tex0=%~2"
SET "tex1=%~3"
SET "tex2=%~4"
SET "tex3=%~5"
SET "tex4=%~6"
SET "tex5=%~7"
ECHO. > "%filepath%"
IF NOT "%opt_mtr_shader_vs%" == "" (
    ECHO customshadervs %opt_mtr_shader_vs%>> "%filepath%"
)
IF NOT "%opt_mtr_shader_fs%" == "" (
    ECHO customshaderfs %opt_mtr_shader_fs%>> "%filepath%"
)
IF NOT "%opt_mtr_shader_gs%" == "" (
    ECHO customshadergs %opt_mtr_shader_gs%>> "%filepath%"
)
IF NOT "%opt_mtr_renderhint%" == "" (
    ECHO renderhint %opt_mtr_renderhint%>> "%filepath%"
)
ECHO. >> "%filepath%"
ECHO // Textures >> "%filepath%"
ECHO texture0 %tex0%>> "%filepath%"
ECHO texture1 %tex1%>> "%filepath%"
ECHO texture2 %tex2%>> "%filepath%"
ECHO texture3 %tex3%>> "%filepath%"
ECHO texture4 %tex4%>> "%filepath%"
ECHO texture5 %tex5%>> "%filepath%"
EXIT /B 0


:STRLEN <resultVar> <stringVar>
(
    SETLOCAL EnableDelayedExpansion
    SET "s=!%~2!#"
    SET "len=0"
    FOR %%P in (4096 2048 1024 512 256 128 64 32 16 8 4 2 1) do (
        IF "!s:~%%P,1!" NEQ "" (
            SET /a "len+=%%P"
            SET "s=!s:~%%P!"
        )
    )
)
(
    ENDLOCAL
    SET "%~1=%len%"
    EXIT /B
)


:SHORTEN_FILENAME <new_name> <old_name>
(
    SETLOCAL EnableDelayedExpansion
    SET "new_tex_name=!%~2!"
    CALL :strlen filename_length new_tex_name
    REM Cut something off in the middle to preserve file suffix
    IF !filename_length! GTR 16 (
        SET "new_tex_name=!new_tex_name:~0,9!!new_tex_name:~-7!"
    )
)
(
    ENDLOCAL
    SET "%~1=%new_tex_name%"
    EXIT /B
)