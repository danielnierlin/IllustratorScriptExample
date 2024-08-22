
using Illustrator;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting Illustrator...");

        var application = new Application
        {
            UserInteractionLevel = AiUserInteractionLevel.aiDontDisplayAlerts
        };

        Console.WriteLine("Version: " + application.Version);

        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("Iteration " + (i + 1));

            Console.WriteLine("Opening file...");
            var document = application.Open(GetFilePath("MD215_NG-6318-1.ai"));

            // If we don't duplicate the Layer on run the script on the original Layer "MD_2_BEMASSUNG" it works fine.
            // 
            Console.WriteLine("Duplicating layer");
            var sourceLayer = GetLayer(document, "MD_2_BEMASSUNG");

            Duplicate(document, sourceLayer, "MD_2_BEMASSUNG_NEW");

            // System.Threading.Thread.Sleep(5000); // Doesn't help

            Console.WriteLine("Running JavaScript...");

            application.DoJavaScriptFile(GetFilePath("replaceDecimalSeparator.js"));

            Console.WriteLine("Closing file...");

            application.Documents[1].Close(AiSaveOptions.aiDoNotSaveChanges);
        }

        Console.ReadKey();
    }

    private static string GetFilePath(string fileName)
    {
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            fileName);
    }

    private static Layer GetLayer(Document document, string name)
    {
        for (int i = 1; i <= document.Layers.Count; i++)
        {
            if (document.Layers[i].Name == name)
            {
                return document.Layers[i];
            }
;
        }

        return null;
    }

    private static Layer Duplicate(Document document,
        Layer source,
        string newLayerName)
    {
        var newLayer = document.Layers.Add();
        newLayer.Name = newLayerName;

        for (int i = 1; i <= source.GroupItems.Count; i++)
        {
            source.GroupItems[i].Duplicate(newLayer);

        }

        return newLayer;
    }
}


