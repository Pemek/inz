using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using mvvm.Model.NAudio;
using System.Threading;

namespace mvvm.Model
{
    public class Instruments : INotifyPropertyChanged
    {
        /// <summary>
        /// aktualnie wykorzystywana mapa
        /// </summary>
        private Map myMap;
        public Map MyMap
        {
            get { return myMap; }
            set
            {
                myMap = value;
                OnPropertyChanged("MyMap");
            }
        }

        /// <summary>
        /// lista dzwiekow mapy
        /// </summary>
        private List<ConstructionSoundPlayer> soundsList;
        public List<ConstructionSoundPlayer> SoundsList
        {
            get { return soundsList; }
            set
            {
                soundsList = value;
                OnPropertyChanged("SoundsList");
            }
        }

        private readonly BackgroundWorker playMusicWorker = new BackgroundWorker();

        public Instruments(Map m)
        {
            MyMap = m;
            SoundsList = new List<ConstructionSoundPlayer>();
            foreach (var potentialSound in MyMap.ConstructionList)
            {
                SoundsList.Add(new ConstructionSoundPlayer(potentialSound.StreamName));
            }

            playMusicWorker.DoWork += playMusicWorker_DoWork;
            playMusicWorker.WorkerSupportsCancellation = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// rozpoczecie odtwarzania muzyki
        /// </summary>
        public void playMusic()
        {
            playMusicWorker.RunWorkerAsync();
        }

        private void playMusicWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            while (true)
            {
                if (playMusicWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                for (int i = 0; i < MyMap.ConstructionList.Count; i++)
                {
                    if (Calculations.calculateDistance(MyMap.ConstructionList[i].X, MyMap.ConstructionList[i].Y, MyMap.UserX, MyMap.UserY) < 20)
                    {
                        SoundsList[i].playSound();
                    }
                    Thread.Sleep(10);
                }
            }
        }
        /// <summary>
        /// zakonczenie oddtwarzanie muzyki
        /// </summary>
        public void stopMusic()
        {
            playMusicWorker.CancelAsync();
        }
    }
}
