using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NAudio;
using NAudio.Wave;

namespace mvvm.Model.NAudio
{
    static public class SoundPlayer
    {
        private static BlockAlignReductionStream streamUp = null;
        private static BlockAlignReductionStream streamDown = null;
        private static BlockAlignReductionStream streamLeft = null;
        private static BlockAlignReductionStream streamRight = null;

        private static DirectSoundOut outLeft = null;
        private static DirectSoundOut outRight = null;
        private static DirectSoundOut outUp = null;
        private static DirectSoundOut outDown = null;

        static SoundPlayer()
        {
            try
            {
                //left init
                WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader("..\\..\\sounds\\left.mp3"));
                streamLeft = new BlockAlignReductionStream(pcm);

                outLeft = new DirectSoundOut();
                outLeft.Init(streamLeft);
                
                //right init
                pcm = WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader("..\\..\\sounds\\right.mp3"));
                streamRight = new BlockAlignReductionStream(pcm);

                outRight = new DirectSoundOut();
                outRight.Init(streamRight);

                //up init
                pcm = WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader("..\\..\\sounds\\up.mp3"));
                streamUp = new BlockAlignReductionStream(pcm);

                outUp = new DirectSoundOut();
                outUp.Init(streamUp);

                //down init
                pcm = WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader("..\\..\\sounds\\down.mp3"));
                streamDown = new BlockAlignReductionStream(pcm);

                outDown = new DirectSoundOut();
                outDown.Init(streamDown);
            }
            catch
            { 
            }


        }

        static public void playLeft()
        {
            try
            {
                streamLeft.Position = 0;
                outLeft.Play();
            }
            catch
            {
            }
        }

        static public void playRight()
        {
            try
            {
                streamRight.Position = 0;
                outRight.Play();
            }
            catch
            {
            }
        }

        static public void playUp()
        {
            try
            {
                streamUp.Position = 0;
                outUp.Play();
            }
            catch
            {
            }
        }

        static public void playDown()
        {
            try
            {
                streamDown.Position = 0;
                outDown.Play();
            }
            catch
            {
            }
        }

        /// <summary>
        /// zwraca satream z pliku muzycznego
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static public BlockAlignReductionStream getStream(string path)
        {
            WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(path));
            return new BlockAlignReductionStream(pcm);
        }

        /// <summary>
        /// odtworzenie dzwieku z zadanego streamu
        /// </summary>
        /// <param name="stream"></param>
        static public void playFromStream(BlockAlignReductionStream stream)
        {
            stream.Position = 0;
            DirectSoundOut tmp = new DirectSoundOut();
            tmp.Init(stream);
        }
    }
}
