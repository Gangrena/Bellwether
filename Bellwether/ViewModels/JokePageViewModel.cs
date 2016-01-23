﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Bellwether.Commands;
using Bellwether.Models.Models;
using Bellwether.Models.ViewModels;
using Bellwether.Services.Utility;

namespace Bellwether.ViewModels
{
    public class JokePageViewModel:ViewModel
    {
        public ObservableCollection<JokeViewModel> Jokes { get; set; }
        private JokeViewModel _selectedJoke;
        public JokeViewModel SelectedJoke
        {
            get { return _selectedJoke; }
            set
            {
                _selectedJoke = value;
                NotifyPropertyChanged();
            }
        }
        private string _speakerStatus;
        public string SpeakerStatus
        {
            get { return _speakerStatus; }
            set { _speakerStatus = value; NotifyPropertyChanged(); }
        }
        private RelayCommand _selectedJokeCommand;
        public RelayCommand SelectedJokeCommand
        {
            get
            {
                return this._selectedJokeCommand ?? (this._selectedJokeCommand = new RelayCommand(() =>
                {

                }));
            }
        }
        public RelayCommand SpeakCommand { get; set; }
        public JokePageViewModel()
        {
            Jokes = new ObservableCollection<JokeViewModel>();
            SpeakCommand = new RelayCommand(Speak);
            LoadContent();
        }
        public async void Speak()
        {
            SpeakerStatus =
                await
                    ServiceFactory.SpeechSyntesizerService.ValidateSpeakerAndSpeak(
                        SelectedJoke.JokeContent)
                    ? "Stop" : "Speak";
        }
        private void LoadContent()
        {
            var jokes = ServiceExecutor.Execute(() => ServiceFactory.JokeService.GetJokes());
            jokes.Data.ToList().ForEach(x =>
            {
                Jokes.Add(x);
            });
        }
    }
}