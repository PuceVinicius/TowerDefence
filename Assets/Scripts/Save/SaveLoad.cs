using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Boilerplate.SaveCommons;
using UnityEngine;

namespace Boilerplate.Save
{
    public static class SaveLoad
    {
        public static void Save(PlayerSave playerSave)
        {
            var savePath = Application.persistentDataPath + SaveConsts.SaveName;

            var stream = new FileStream(savePath, FileMode.OpenOrCreate);
            var formatter = new BinaryFormatter();

            formatter.Serialize(stream, playerSave);
            stream.Close();
        }

        public static PlayerSave Load()
        {
            var savePath = Application.persistentDataPath + SaveConsts.SaveName;

            if (!File.Exists(savePath))
                return null;

            var stream = new FileStream(savePath, FileMode.Open);
            var formatter = new BinaryFormatter();

            var data = formatter.Deserialize(stream) as PlayerSave;
            stream.Close();

            return data;
        }
    }
}