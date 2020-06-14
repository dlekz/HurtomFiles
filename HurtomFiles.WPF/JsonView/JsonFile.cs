using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;

namespace HurtomFiles.WPF
{
    public class JsonFile
    {
        private readonly string workingDirectory = Environment.CurrentDirectory;
        private readonly string fileName = "";

        public string Article { set; get; } = "";

        public JsonFile(string fileName)
        {
            this.fileName = fileName + ".json";

            if (!File.Exists(FullPath))
                File.Create(FullPath);
        }

        public string FullPath =>
            workingDirectory + "\\" + fileName;

        public JToken[] ReadArticle(string article = "") 
        {
            try
            {
                string jText = File.ReadAllText(this.FullPath);
                JObject jObject = JObject.Parse(jText);
                var jArticle = (article == "") ? Article : article;

                return jObject[jArticle].Children().ToArray();
            }
            catch (Exception ex)
            {
                return new JToken[0];
            }
        }

        public void WriteArticle(object[] objects, string article = "") 
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Formatting = Formatting.Indented;
            var jArticle = (article == "") ? Article : article;

            using (StreamWriter sWriter = new StreamWriter(this.FullPath))
            {
                sWriter.Write("{\"" + jArticle + "\":[");

                for (int i = 0; i < objects.Length; i++)
                {
                    if (i != 0) sWriter.Write(",");
                    sWriter.Write(Environment.NewLine);
                    serializer.Serialize(sWriter, objects[i]);
                }

                sWriter.Write("]}");
            };
        }

        public void Clear() 
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Formatting = Formatting.Indented;

            using (StreamWriter sWriter = new StreamWriter(this.FullPath))
            {
                sWriter.Write("");
            };
        }
    }
}
