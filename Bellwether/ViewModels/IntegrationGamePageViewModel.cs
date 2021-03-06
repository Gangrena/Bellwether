﻿using System.Collections.ObjectModel;
using Bellwether.Commands;
using Bellwether.Models.ViewModels;
using Bellwether.Services.Utility;

namespace Bellwether.ViewModels
{
    public class IntegrationGamePageViewModel : ViewModel
    {
        private IntegrationGameViewModel _selectedIntegrationGame;
        public IntegrationGameViewModel SelectedIntegrationGame
        {
            get { return _selectedIntegrationGame; }
            set
            {
                _selectedIntegrationGame = value;
                NotifyPropertyChanged();
            }
        }

        private bool _speakerStatus;
        public bool SpeakerStatus
        {
            get { return _speakerStatus; }
            set { _speakerStatus = value;NotifyPropertyChanged(); }
        }

        public ObservableCollection<IntegrationGameViewModel> IntegrationGames { get; set; }
        public RelayCommand ReadCommand { get; set; }

        public IntegrationGamePageViewModel()
        {
            ReadCommand = new RelayCommand(Read);
            IntegrationGames = new ObservableCollection<IntegrationGameViewModel>();
            LoadContent();
            LoadLanguageContent();
            LoadSpeakerStatus();
        }

        public void LoadSpeakerStatus()
        {
            SpeakerStatus = ServiceFactory.SpeechSyntesizerService.GetSpeakerStatus();
        }

        private async void Read()
        {
            if (SelectedIntegrationGame != null)  
            SpeakerStatus =
                await
                    ServiceFactory.SpeechSyntesizerService.ValidateSpeakerAndSpeak(
                        SelectedIntegrationGame.GameDescription);
        }

        private void LoadContent()
        {
            var integrationGames =
                ServiceExecutor.Execute(() => ServiceFactory.IntegrationGameService.GetIntegrationGames());
            if (integrationGames.IsValid)
                IntegrationGames = new ObservableCollection<IntegrationGameViewModel>(integrationGames.Data);
        }

        private async void LoadLanguageContent()
        {
            var contentKey = new[] { "IntegrationGamesHeader", "GameCategory", "PaceOfPlay", "PreparationFun", "NumberOfPlayer", "AvailableGames","Stop","Read"};
            var contentDictionary = await ServiceFactory.ResourceService.GetLanguageContentForKeys(contentKey);
            TextPageTittle = contentDictionary["IntegrationGamesHeader"];
            TextGameCategory = contentDictionary["GameCategory"];
            TextPaceOfPlay = contentDictionary["PaceOfPlay"];
            TextPreparationFun = contentDictionary["PreparationFun"];
            TextNumberOfPlayer = contentDictionary["NumberOfPlayer"];
            TextAvailableGames = contentDictionary["AvailableGames"];
            TextStop = contentDictionary["Stop"];
            TextRead = contentDictionary["Read"];
        }

        private string _textPageTittle;
        public string TextPageTittle { get { return _textPageTittle; } set { _textPageTittle = value; NotifyPropertyChanged(); } }
        private string _textGameCategory;
        public string TextGameCategory { get { return _textGameCategory; } set { _textGameCategory = value; NotifyPropertyChanged(); } }
        private string _textPaceOfPlay;
        public string TextPaceOfPlay
        {
            get { return _textPaceOfPlay; }
            set { _textPaceOfPlay = value; NotifyPropertyChanged(); }
        }
        private string _textNumberOfPlayer;
        public string TextNumberOfPlayer { get { return _textNumberOfPlayer; } set { _textNumberOfPlayer = value;NotifyPropertyChanged(); } }
        private string _textPreparationFun;
        public string TextPreparationFun { get { return _textPreparationFun; } set { _textPreparationFun = value;NotifyPropertyChanged(); } }
        private string _textAvailableGames;
        public string TextAvailableGames { get { return _textAvailableGames; } set { _textAvailableGames = value;NotifyPropertyChanged(); } }        
        private string _textRead;
        public string TextRead { get { return _textRead; } set { _textRead = value; NotifyPropertyChanged(); } }
        private string _textStop;
        public string TextStop { get { return _textStop; } set { _textStop = value; NotifyPropertyChanged(); } }


    }
}
