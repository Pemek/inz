using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NAudio;
using NAudio.Wave;
using System.ComponentModel;

namespace mvvm.Model.NAudio
{
    public class ConstructionSoundPlayer
    {
        private readonly BackgroundWorker playMusicWorker = new BackgroundWorker();

        private BlockAlignReductionStream stream = null;
        private DirectSoundOut outSound = null;
        private string path= null;

        public ConstructionSoundPlayer(string soundName)
        {
            try
            {
                path = "sounds\\" + soundName + ".mp3";

                WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader("sounds\\" + soundName + ".mp3"));
                stream = new BlockAlignReductionStream(pcm);

                outSound = new DirectSoundOut();
                outSound.Init(stream);
            }

            catch { };

            playMusicWorker.DoWork += playMusicWorker_DoWork;
        }

        public void playSound()
        {
            if(!playMusicWorker.IsBusy)
                playMusicWorker.RunWorkerAsync();
            //try
            //{
            //    stream.Position = 0;
            //    outSound.Play();
            //}
            //catch { }
        }

        private void playMusicWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //WaveStream pcmTmp = WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(path));
            //BlockAlignReductionStream streamTmp = new BlockAlignReductionStream(pcmTmp);

            //DirectSoundOut outTmp = new DirectSoundOut();
            //outTmp.Init(streamTmp);
            //outTmp.Play();


            //BlockAlignReductionStream s = new BlockAlignReductionStream(stream);
            //s.Position = 0;
            //DirectSoundOut o = new DirectSoundOut();
            //o.Init(s);
            if (stream != null)
            {
                if (stream.Position == stream.Length || stream.Position == 0 || stream.Length / 6 == stream.Position)
                {
                    stream.Position = 0;
                    outSound.Play();
                }
            }
        }
    }
}
