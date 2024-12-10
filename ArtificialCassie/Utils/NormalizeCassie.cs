namespace ArtificialCassie.Utils
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text.Json;

    public static class NormalizeCassie
    {
        private static readonly Dictionary<string, string> replacements;

        static NormalizeCassie()
        {
            // Load replacements from the embedded JSON file
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "ArtificialCassie.Resources.Replacements.json"; // Adjust to your namespace and file path

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream ?? throw new FileNotFoundException("Embedded resource not found.")))
            {
                var json = reader.ReadToEnd();
                replacements = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
            }
        }

        public static string Normalize(string text)
        {
            // Apply replacements to the input text
            foreach (var pair in replacements)
            {
                text = text.Replace(pair.Key, pair.Value);
            }
            return text;
        }
    }
}
