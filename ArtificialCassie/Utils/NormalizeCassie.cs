namespace ArtificialCassie.Utils
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using Newtonsoft.Json;

    public static class NormalizeCassie
    {
        private static Dictionary<string, string> replacements;

        public static string Normalize(string text)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "ArtificialCassie.Resources.Replacements.json";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream ?? throw new FileNotFoundException("Embedded resource not found.")))
            {
                var json = reader.ReadToEnd();
                replacements = JsonConvert.DeserializeObject<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
            }

            // Apply replacements to the input text
            foreach (var pair in replacements)
            {
                text = text.Replace(pair.Key, pair.Value);
            }
            return text;
        }
    }
}
