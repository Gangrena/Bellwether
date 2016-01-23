﻿using System.Collections.ObjectModel;
using Bellwether.Commands;
using Bellwether.Services.Utility;

namespace Bellwether.ViewModels
{
    public class IntegrationGamePageViewModel : ViewModel
    {
        private Models.ViewModels.IntegrationGameViewModel _selectedIntegrationGame;
        public Models.ViewModels.IntegrationGameViewModel SelectedIntegrationGame
        {
            get { return _selectedIntegrationGame; }
            set
            {
                _selectedIntegrationGame = value;
                NotifyPropertyChanged();
            }
        }

        private string _speakerStatus;
        public string SpeakerStatus
        {
            get { return _speakerStatus; }
            set { _speakerStatus = value;NotifyPropertyChanged(); }
        }

        public ObservableCollection<Models.ViewModels.IntegrationGameViewModel> IntegrationGames { get; set; }
        public RelayCommand SpeakCommand { get; set; }

        public IntegrationGamePageViewModel()
        {
            SpeakCommand = new RelayCommand(Speak);
            SpeakerStatus = ServiceFactory.SpeechSyntesizerService.GetSpeakerStatus() ? "STOP": "Speak";
            LoadContent();
            LoadLanguageContent();
        }

        private async void Speak()
        {            
            SpeakerStatus =
                await
                    ServiceFactory.SpeechSyntesizerService.ValidateSpeakerAndSpeak(
                        SelectedIntegrationGame.GameDescription)
                    ? "Stop":"Speak";
        }

        private void LoadContent()
        {
            var integrationGames =
                ServiceExecutor.Execute(() => ServiceFactory.IntegrationGameService.GetIntegrationGames());
            if (integrationGames.IsValid)
            {
                IntegrationGames = new ObservableCollection<Models.ViewModels.IntegrationGameViewModel>(integrationGames.Data);
                SelectedIntegrationGame = IntegrationGames[0];
            }

        }

        private async void LoadLanguageContent()
        {
            var contentKey = new[] { "IntegrationGamesHeader", "GameCategory", "PaceOfPlay", "PreparationFun", "NumberOfPlayer"};
            var contentDictionary = await ServiceFactory.ResourceService.GetLanguageContentForKeys(contentKey);
            TextPageTittle = contentDictionary["IntegrationGamesHeader"];
            TextGameCategory = contentDictionary["GameCategory"];
            TextPaceOfPlay = contentDictionary["PaceOfPlay"];
            TextPreparationFun = contentDictionary["PreparationFun"];
            TextNumberOfPlayer = contentDictionary["NumberOfPlayer"];
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


    }
}