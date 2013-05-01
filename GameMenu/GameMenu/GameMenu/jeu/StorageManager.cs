﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using System.IO;

namespace Umea_rana
{
    public class StorageManager
    {
        private const string Configuration_filename = "game.config";
        private StorageDevice _storageDevice;

        public StorageManager()
        {
            IAsyncResult result = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);
            _storageDevice = StorageDevice.EndShowSelector(result);
        }
        public GameConfiguration LoadGameConfiguration(GraphicsDeviceManager graphics)
        {
            StorageContainer storage = GetContainer("Setting");

            if (!storage.FileExists(Configuration_filename))
                return new GameConfiguration();
            else
            {
                Stream stream = storage.OpenFile(Configuration_filename, FileMode.Open);
                XmlSerializer serial = new XmlSerializer(typeof(GameConfiguration));
                GameConfiguration config = (GameConfiguration)serial.Deserialize(stream);
                stream.Close();
                storage.Dispose();
                return config;
            }
        }
        public void SaveGameConfiguration(GameConfiguration gameConfig)
        {
            StorageContainer storage = GetContainer("Setting");
            if (storage.FileExists(Configuration_filename))
                storage.DeleteFile(Configuration_filename);

            Stream stream = storage.CreateFile(Configuration_filename);

            XmlSerializer serializer = new XmlSerializer(typeof(GameConfiguration));
            serializer.Serialize(stream, gameConfig);
            stream.Close();
            storage.Dispose();
        }
        private StorageContainer GetContainer(string path)
        {
            IAsyncResult result = _storageDevice.BeginOpenContainer(path, null, null);
            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = _storageDevice.EndOpenContainer(result);
            result.AsyncWaitHandle.Close();

            return container;
        }
    }
}
