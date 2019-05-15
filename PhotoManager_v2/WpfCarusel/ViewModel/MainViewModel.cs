using System.Collections.ObjectModel;
using System.ComponentModel;
using WpfCarouselDemo.Model;

namespace WpfCarouselDemo.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            RadioStationsDAB = new ObservableCollection<RadioStation>
            {
                new RadioStation() { Name = "BBC Radio 1", ShortName = "BBC R1", ImageSource = "/Resources/BBCRadio1.jpg", Text = "Clara Amfo: Christine and the Queens are in the Live Lounge!" },
                new RadioStation() { Name = "BBC Radio 1 Extra", ShortName = "BBC R1 Extra", ImageSource = "/Resources/BBCRadio1Extra.jpg", Text = "Ace: Swedish-Gambian R&B singer Seinabo Say." },
                new RadioStation() { Name = "BBC Radio 2", ShortName = "BBC R2", ImageSource = "/Resources/BBCRadio2.jpg", Text = "Ken Bruce: Roger Daltrey chooses the Tracks of My Years." },
                new RadioStation() { Name = "BBC Radio 3", ShortName = "BBC R3", ImageSource = "/Resources/BBCRadio3.jpg", Text = "The London Philharmonic play Motorhead." },
                new RadioStation() { Name = "BBC Radio 4", ShortName = "BBC R4", ImageSource = "/Resources/BBCRadio4.jpg", Text = "Melvin Bragg talks to some clever people." },
                new RadioStation() { Name = "BBC Radio 4 Extra", ShortName = "BBC R4 Extra", ImageSource = "/Resources/BBCRadio4Extra.jpg", Text = "Around the Horne." },
                new RadioStation() { Name = "BBC Radio 5 Live", ShortName = "BBC R5 Live", ImageSource = "/Resources/BBCRadio5Live.jpg", Text = "The Emma Barnett Show: cutting edge political debate." },
                new RadioStation() { Name = "BBC Radio 5 Live Sports", ShortName = "BBC R5 Live Sports", ImageSource = "/Resources/BBCRadio5LiveSports.jpg", Text = "Football chat with Steve Crossman and guests Leon Osman and Simon Grayson." },
                new RadioStation() { Name = "BBC Radio 6 Music", ShortName = "BBC R6 Music", ImageSource = "/Resources/BBCRadio6Music.jpg", Text = "Some music. What did you expect?" },
                new RadioStation() { Name = "BBC Radio Asian Network", ShortName = "BBC R Asian", ImageSource = "/Resources/BBCRadioAsianNetwork.jpg", Text = "Noreen Khan: Nadia Ali sits in." },
                new RadioStation() { Name = "Heart", ShortName = "Heart", ImageSource = "/Resources/Heart.png", Text = "Push the Button by Sugababes" },
                new RadioStation() { Name = "Jazz FM", ShortName = "Jazz FM", ImageSource = "/Resources/JazzFM.jpg", Text = "Kenneth Clarke presents A History Of Jazz" },
                new RadioStation() { Name = "Capital FM", ShortName = "Capital FM", ImageSource = "/Resources/CapitalFM.png", Text = "Bassman: Hear all the hottest tunes and freshest music news with The Bassman" }
            };

            SelectedRadioStationDAB = RadioStationsDAB[0];
        }

        private ObservableCollection<RadioStation> _radioStationsDAB;
        public ObservableCollection<RadioStation> RadioStationsDAB
        {
            get => _radioStationsDAB;
            set
            {
                _radioStationsDAB = value;
                NotifyPropertyChanged("RadioStationsDAB");
            }
        }

        private RadioStation _selectedRadioStationDAB;
        public RadioStation SelectedRadioStationDAB
        {
            get => _selectedRadioStationDAB;
            set
            {
                _selectedRadioStationDAB = value;
                NotifyPropertyChanged("SelectedRadioStationDAB");
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged
    }
}
