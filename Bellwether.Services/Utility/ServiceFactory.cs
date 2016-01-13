﻿using Bellwether.Services.Services;
using Bellwether.Services.Services.IntegrationGameService;
using Bellwether.Services.Services.JokeService;
using Bellwether.Services.Services.ResourceService;
using Bellwether.Services.Services.Version;
using Bellwether.Services.WebServices;

namespace Bellwether.Services.Utility
{
    public static class ServiceFactory
    {
        private static IInitResourceService _initResourceService;
        public static IInitResourceService InitResourceService
            => _initResourceService ?? (_initResourceService = new InitiateResourceService());

        private static IVersionValidateService _versionValidateService;
        public static IVersionValidateService VersionValidateService
            => _versionValidateService ?? (_versionValidateService = new VersionValidateService());

        private static ILanguageService _languageService;
        public static ILanguageService LanguageService => _languageService ?? (_languageService = new LanguageService());

        private static IApplicationResourceService _applicationResourceService;
        public static IApplicationResourceService AppiApplicationResourceService
            => _applicationResourceService ?? (_applicationResourceService = new ApplicationResourceService());

        private static IWebBellwetherLanguageService _webBellwetherLanguageService;
        public static IWebBellwetherLanguageService WebBellwetherLanguageService
            => _webBellwetherLanguageService ?? (_webBellwetherLanguageService = new WebBellwetherLanguageService());

        private static IWebBellwetherVersionService _webBellwetherVersionService;
        public static IWebBellwetherVersionService WebBellwetherVersionService
            => _webBellwetherVersionService ?? (_webBellwetherVersionService = new WebBellwetherVersionService());

        private static IWebBellwetherJokeService _webBellwetherJokeService;
        public static IWebBellwetherJokeService WebBellwetherJokeService
            => _webBellwetherJokeService ?? (_webBellwetherJokeService = new WebBellwetherJokeService());

        private static IJokeCategoryManagementService _jokeCategoryManagementService;
        public static IJokeCategoryManagementService JokeCategoryManagementService
            => _jokeCategoryManagementService ?? (_jokeCategoryManagementService = new JokeCategoryManagementService());

        private static IJokeManagementService _jokeManagementService;
        public static IJokeManagementService JokeManagementService
            => _jokeManagementService ?? (_jokeManagementService = new JokeManagementService());

        private static IGameFeatureManagementService _gameFeatureManagementService;
        public static IGameFeatureManagementService GameFeatureManagementService
            => _gameFeatureManagementService ?? (_gameFeatureManagementService = new GameFeatureManagementService());

        private static IWebBellwetherIntegrationGameService _webBellwetherIntegrationGameService;
        public static IWebBellwetherIntegrationGameService WebBellwetherIntegrationGameService
            =>
                _webBellwetherIntegrationGameService ??
                (_webBellwetherIntegrationGameService = new WebBellwetherIntegrationGameService());
    }
}
