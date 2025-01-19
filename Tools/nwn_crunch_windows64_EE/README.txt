NWN crunch is a command line tool for texture compression, based on the unity fork of crunch.
NWN crunch uses the zlib license: https://opensource.org/licenses/zlib-license.php



################
# Introduction #
################

NWN crunch itself is a command line tool, but ofeers some scripts for the most common operations
with NWN.
- All scripts will read files from the "in" directory and write files to the "out" directory
- The directory structure from the "in" folder will be re-created in the "out" folder.
- Supported file formats: png, tga, bmp, jpeg, dds (standard and NWN), ktx, crn



###########
# Scripts #
###########


convert_dds
Converts all files to (bioware/NWN) dds format.
Compression options are chosen based on file suffix:
_d  =>  Diffuse Texture
_n  =>  Normal Map
_s  =>  Specular Map
_m  =>  Metallicness (converted to specular)
_r  =>  Roughness
_g  =>  Glossisness (converted to roughness by inverting)
_h  =>  Height/Displacement
_i  =>  Illumination
Files without suffix are interpreted as diffuse maps.
The default delimiter "_" can be changed in the settings.ini, see below.
NOTE: These suffixes can be customized in the settings.ini


convert_dds_mtr
Same as convert_dds. In addition material files (*mtr) will be generated.


convert_dds_portraits
converts portraits to dds format, without mipmaps. Disregards file suffix.


convert_png
Converts all files to png format.


convert_tga
Converts all files to tga format.



############
# Settings #
############

[OPTIONS]
USE_METALLICNESS=1

Reads metallicness textures (suffix "_m"), which NWN doesn't support and
converts them to specular textures instead.
Enabled by default. Set to 0 to disable.


[OPTIONS]
USE_GLOSSINESS=1

Reads glossiness textures (suffix "_g"), which NWN doesn't support and
converts them to roughness textures instead (by inverting).
Enabled by default. Set to 0 to disable.


[OPTIONS]
FILENAME_LIMIT=0

Cuts off filenames, if they exceed 16 characters. They will be cut in the
middle to preserve suffixes. NOTE: May interfere with generating mtr files.
Disabled by default. Set to 1 to enable.


[OPTIONS]
AUTO_FLIP=1

Auto flips standard dds textures to match NWN requirements. This is not
necessary for NWNs own dds format.
Enabled by default. Set to 0 to enable.


[OPTIONS]
DROP_EMPTY_ALPHA=1

If the image has an "empty" alpha layer, i.e. which isn't actually used. This setting will remove it.
Enabled by default. Set to 0 to enable.


[TEXTURE_TYPE]
SUFFIX_DELIMITER=_

Delimiter which seperated the suffix from the filename. Only a single character
is allowed, very limited regular expression support.
Default delimiter is an underscore.
NOTE: Only modify if you know what you're doing.
EXAMPLE: Set to "[0-9]" (without quotes) to use a single Number, this results
         in "myfile123d.tga" being recognized as diffuse texture. This also
         requires the MTR_REMOVE_DELIMITER option to be disabled.


[TEXTURE_TYPE]
SUFFIX_REMOVE_DELIMITER=1

Determine whether the Delimiter should be kept when creating the mtr filename.
This is required in the above example.
Enabled by default. Set to 0 to keep the delimiter.


[TEXTURE_TYPE]
SUFFIX_DIFFUSE=d
SUFFIX_NORMAL=n
SUFFIX_SPECULAR=s
SUFFIX_METALLICNESS=m
SUFFIX_ROUGHNESS=r
SUFFIX_GLOSSINESS=g
SUFFIX_HEIGHT=h
SUFFIX_ILLUMINATION=i

Determines texture type and ultimately the compression settings.


[MTR]
MTR_SHADER_FS=

Fragment shader to add to mtr files. Leave empty to add no customshaderfs parameter to the mtr.


[MTR]
MTR_SHADER_VS=

Vertex shader to add to mtr files. Leave empty to add no customshaderfs parameter to the mtr.


[MTR]
MTR_RENDERHINT=NormalAndSpecMapped

Renderhint to add to mtr files. Leave empty to add no renderhint to the mtr.
Valid values: NormalTangents or NormalAndSpecMapped
