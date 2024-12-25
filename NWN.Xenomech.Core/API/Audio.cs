using NWN.Xenomech.Core.API.Enum;
using NWN.Xenomech.Core.Interop;

namespace NWN.Xenomech.Core.API
{
    public partial class NWScript
    {
        /// <summary>
        /// Assign one of the available audio streams to play a specific file. This mechanism can be used
        /// to replace regular music playback, and synchronize it between clients.
        /// * There is currently no way to get playback state from clients.
        /// * Audio streams play in the streams channel which has its own volume setting in the client.
        /// * nStreamIdentifier is one of AUDIOSTREAM_IDENTIFIER_*.
        /// * Currently, only MP3 CBR files are supported and properly seekable.
        /// * Unlike regular music, audio streams do not pause on load screens.
        /// * If fSeekOffset is at or beyond the end of the stream, the seek offset will wrap around, even if the file is configured not to loop.
        /// * fFadeTime is in seconds to gradually fade in the audio instead of starting directly.
        /// * Only one type of fading can be active at once, for example:
        ///   If you call StartAudioStream() with fFadeTime = 10.0f, any other audio stream functions with a fade time >0.0f will have no effect
        ///   until StartAudioStream() is done fading.
        /// </summary>
        public static void StartAudioStream(
            uint oPlayer,
            AudioStreamIdentifierType nStreamIdentifier,
            string sResRef,
            bool bLooping = false,
            float fFadeTime = 0.0f,
            float fSeekOffset = -1.0f,
            float fVolume = 1.0f)
        {
            NWNXPInvoke.StackPushFloat(fVolume);
            NWNXPInvoke.StackPushFloat(fSeekOffset);
            NWNXPInvoke.StackPushFloat(fFadeTime);
            NWNXPInvoke.StackPushInteger(bLooping ? 1 : 0);
            NWNXPInvoke.StackPushString(sResRef);
            NWNXPInvoke.StackPushInteger((int)nStreamIdentifier);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1118);
        }

        /// <summary>
        /// Stops the given audio stream.
        /// * fFadeTime is in seconds to gradually fade out the audio instead of stopping directly.
        /// * Only one type of fading can be active at once, for example:
        ///   If you call StartAudioStream() with fFadeInTime = 10.0f, any other audio stream functions with a fade time >0.0f will have no effect
        ///   until StartAudioStream() is done fading.
        /// * Will do nothing if the stream is currently not in use.
        /// </summary>
        public static void StopAudioStream(
            uint oPlayer,
            AudioStreamIdentifierType nStreamIdentifier,
            float fFadeTime = 0.0f)
        {
            NWNXPInvoke.StackPushFloat(fFadeTime);
            NWNXPInvoke.StackPushInteger((int)nStreamIdentifier);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1119);
        }

        /// <summary>
        /// Un/pauses the given audio stream.
        /// * fFadeTime is in seconds to gradually fade the audio out/in instead of pausing/resuming directly.
        /// * Only one type of fading can be active at once, for example:
        ///   If you call StartAudioStream() with fFadeInTime = 10.0f, any other audio stream functions with a fade time >0.0f will have no effect
        ///   until StartAudioStream() is done fading.
        /// * Will do nothing if the stream is currently not in use.
        /// </summary>
        public static void SetAudioStreamPaused(
            uint oPlayer,
            AudioStreamIdentifierType nStreamIdentifier,
            bool bPaused,
            float fFadeTime = 0.0f)
        {
            NWNXPInvoke.StackPushFloat(fFadeTime);
            NWNXPInvoke.StackPushInteger(bPaused ? 1 : 0);
            NWNXPInvoke.StackPushInteger((int)nStreamIdentifier);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1120);
        }

        /// <summary>
        /// Change volume of the audio stream.
        /// * Volume is from 0.0 to 1.0.
        /// * fFadeTime is in seconds to gradually change the volume.
        /// * Only one type of fading can be active at once, for example:
        ///   If you call StartAudioStream() with fFadeInTime = 10.0f, any other audio stream functions with a fade time >0.0f will have no effect
        ///   until StartAudioStream() is done fading.
        /// * Subsequent calls to this function with fFadeTime >0.0f while already fading the volume
        ///   will start the new fade with the previous' fade's progress as starting point.
        /// * Will do nothing if the stream is currently not in use.
        /// </summary>
        public static void SetAudioStreamVolume(
            uint oPlayer,
            AudioStreamIdentifierType nStreamIdentifier,
            float fVolume = 1.0f,
            float fFadeTime = 0.0f)
        {
            NWNXPInvoke.StackPushFloat(fFadeTime);
            NWNXPInvoke.StackPushFloat(fVolume);
            NWNXPInvoke.StackPushInteger((int)nStreamIdentifier);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1121);
        }

        /// <summary>
        /// Seek the audio stream to the given offset.
        /// * When seeking at or beyond the end of a stream, the seek offset will wrap around, even if the file is configured not to loop.
        /// * Will do nothing if the stream is currently not in use.
        /// * Will do nothing if the stream is in ended state (reached end of file and looping is off). In this
        ///   case, you need to restart the stream.
        /// </summary>
        public static void SeekAudioStream(
            uint oPlayer,
            AudioStreamIdentifierType nStreamIdentifier,
            float fSeconds)
        {
            NWNXPInvoke.StackPushFloat(fSeconds);
            NWNXPInvoke.StackPushInteger((int)nStreamIdentifier);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1122);
        }
    }
}
