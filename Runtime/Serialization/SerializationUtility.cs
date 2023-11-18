using System.IO;
using UnityEngine;

namespace Foundation {
    public static class SerializationUtility {
        public static Model Read<Model>(string url) where Model : IJsonSerializable {
            if (!File.Exists(url)) {
                throw new System.Exception($"File doesn't exist at {url}");
            }

            string text = File.ReadAllText(url);
            return JsonUtility.FromJson<Model>(text);
        }

        public static void Write<Model>(this Model model, string url) where Model : IJsonSerializable {
            string text = JsonUtility.ToJson(model, model.PrettyPrint);
            File.WriteAllText(url, text);
        }
        // public static Model Read<Model>() where Model : IJsonSerializable, IFileSerializable
        //     => Read<Model>(Model.FileURL);
        //
        // public static void Write<Model>(this Model model) where Model : IJsonSerializable, IFileSerializable
        //     => model.Write(Model.FileURL);
    }
}