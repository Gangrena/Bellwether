﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;

namespace Bellwether.Repositories.Repositories
{
    public interface IResourceRepository
    {
        Task<string> GetValueForKey(string key);
        Task SaveValueForKey(string key, string value);
        Task<Dictionary<string, string>> GetSelectedKeysValues(IEnumerable<string> keys);
        Task SaveValuesAndKays(Dictionary<string, string> resources);
        Task SaveSelectedValues(Dictionary<string, string> resources);
        Task<Dictionary<string, string>> GetAll();
    }
    public class ResourceRepository:IResourceRepository
    {
        private readonly string _fileName;
        private readonly string _localResourceFolderName;
        private readonly StorageFolder _localFolder;
        private StorageFile _localFile;

        public ResourceRepository(string fileName,string resourcesFolderName,StorageFolder localFolder)
        {
            _localFolder = localFolder;
            _fileName = fileName;
            _localResourceFolderName = resourcesFolderName;
        }
        private async Task Init()
        {
            try
            {
                var dataFolder = await _localFolder.TryGetItemAsync(_localResourceFolderName) as StorageFolder;
                if (dataFolder != null) _localFile = await dataFolder.TryGetItemAsync(_fileName) as StorageFile;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
            
        }
        public async Task<string> GetValueForKey(string key)
        {
            await Init();
            string content = await FileIO.ReadTextAsync(_localFile);
            dynamic jsonObj = JsonConvert.DeserializeObject(content);
            Dispose();
            return jsonObj[key];

        }

        public async Task SaveValueForKey(string key, string value)
        {
            await Init();
            string content = await
                FileIO.ReadTextAsync(_localFile);
            dynamic jsonObj = JsonConvert.DeserializeObject(content);
            jsonObj[key] = value;
            FileIO.WriteTextAsync(_localFile, JsonConvert.SerializeObject(jsonObj));
            Dispose();
        }
        public async Task<Dictionary<string, string>> GetSelectedKeysValues(IEnumerable<string> keys)
        {
            await Init();
            string content = await
               FileIO.ReadTextAsync(_localFile);
            Dictionary<string, string> localResource = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
            Dictionary<string, string> scenario = new Dictionary<string, string>();
            keys.ToList().ForEach(key =>
            {
                var searchItem = localResource.FirstOrDefault(z => z.Key == key);
                if (searchItem.Key != null)
                    scenario.Add(searchItem.Key, searchItem.Value);
            });
            Dispose();
            return scenario;
        }
        public async Task SaveValuesAndKays(Dictionary<string, string> resources)
        {
            try
            {
                await Init();
                await FileIO.WriteTextAsync(_localFile, JsonConvert.SerializeObject(resources));
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
            Dispose();
        }

        public async Task SaveSelectedValues(Dictionary<string, string> resources)
        {
            await Init();
            string content = await
            FileIO.ReadTextAsync(_localFile);
            var localResource = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
            resources.ToList().ForEach(x =>
            {
                localResource[x.Key] = x.Value;
            });
            await FileIO.WriteTextAsync(_localFile, JsonConvert.SerializeObject(localResource));
            Dispose();
        }

        public async Task<Dictionary<string, string>> GetAll()
        {
            await Init();
            string content = await
            FileIO.ReadTextAsync(_localFile);
            var localResource = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
            Dispose();
            return localResource;
        }

        private void Dispose()
        {
            _localFile = null;
        }
    }
}