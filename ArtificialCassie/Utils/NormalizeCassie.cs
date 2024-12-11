namespace ArtificialCassie.Utils
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public static class NormalizeCassie
    {
        private static Dictionary<string, string> replacements;

        static NormalizeCassie()
        {
            // Initialize the replacements dictionary
            replacements = new Dictionary<string, string>();
            _ = LoadReplacementsAsync(); // Load replacements asynchronously
        }

        private static async Task LoadReplacementsAsync()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "ArtificialCassie.Resources.Replacements.json";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream ?? throw new FileNotFoundException("Embedded resource not found.")))
            {
                var json = await reader.ReadToEndAsync();
                replacements = JsonConvert.DeserializeObject<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
            }
        }

        public static async Task<string> NormalizeAsync(string text)
        {
            if (text.StartsWith("#"))
            {
                text = text.Substring(1); //if theres a # at the first index remove it since we used it to initiate this process
            }

            // Ensure replacements are loaded (necessary if Normalize is called immediately after startup)
            while (replacements == null || replacements.Count == 0)
            {
                await Task.Delay(10);
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
