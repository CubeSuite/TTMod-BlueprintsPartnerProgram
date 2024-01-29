using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechtonicaBlueprints.Classes;

namespace TechtonicaBlueprints
{
    public static class ProgramData
    {
        public static bool isDebugBuild {
            get {
                #if DEBUG
                    return true;
                #else
                    return false;
                #endif
            }
        }
        public static int currentBookID;
        public static BlueprintBook rootBook = new BlueprintBook() {
            id = 0,
            parentId = -1,
            name = "All Blueprints"
        };

        public static class Paths
        {
            public static string dataFolder {
                get {
                    if (!isDebugBuild) {
                        return $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\TechtonicaBlueprintsData";
                    }
                    else {
                        return $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\TechtonicaBlueprintsData";
                        //return $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\TechtonicaBlueprintsDataDebug";
                    }
                }
            }
            public static string blueprintsFile = $"{dataFolder}\\Blueprints.json";
            public static string booksFile = $"{dataFolder}\\Books.json";
            public static string currentBlueprintFile = $"{dataFolder}\\CurrentBlueprint.json";
            public static string showFile = $"{dataFolder}\\Show.txt";
            public static string hideFile = $"{dataFolder}\\Hide.txt";

            private static List<string> folders = new List<string>() { dataFolder };

            public static void createFolderStructure() {
                foreach(string folder in folders) {
                    Directory.CreateDirectory(folder);
                }
            }
        }
    }
}
