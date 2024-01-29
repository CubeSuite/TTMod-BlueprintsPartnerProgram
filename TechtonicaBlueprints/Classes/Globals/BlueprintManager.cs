using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechtonicaBlueprints.Classes;

namespace TechtonicaBlueprints
{
    public static class BlueprintManager
    {
        // Objects & Variables
        private static Dictionary<int, Blueprint> blueprints = new Dictionary<int, Blueprint>();

        // Public Functions

        public static void addBlueprint(Blueprint blueprint, bool shouldSave = true) {
            if(blueprint.id <= 0) blueprint.id = getNewBlueprintID();
            blueprints.Add(blueprint.id, blueprint);
            if (shouldSave) saveData();
        }

        public static void updateBlueprint(Blueprint blueprint) {
            if (doesBlueprintExist(blueprint.id)) {
                blueprints[blueprint.id] = blueprint;
                saveData();
            }
        }

        public static bool doesBlueprintExist(int id) {
            return blueprints.ContainsKey(id);
        }

        public static Blueprint getBlueprint(int id) {
            if (doesBlueprintExist(id)) return blueprints[id];
            else return null;
        }

        public static List<Blueprint> getAllBlueprints() {
            return blueprints.Values.ToList();
        }

        public static void deleteBlueprint(Blueprint blueprint) {
            if (!doesBlueprintExist(blueprint.id) || !BookManager.doesBookExist(blueprint.parentID)) return;
            
            BlueprintBook parent = BookManager.getBook(blueprint.parentID);
            parent.removeBlueprint(blueprint.id);
            blueprints.Remove(blueprint.id);
        }

        // Private Functions

        private static int getNewBlueprintID() {
            if (blueprints.Count == 0) return 0;
            else return blueprints.Keys.Max() + 1;
        }

        // Data Functions

        public static void saveData() {
            string json = JsonConvert.SerializeObject(getAllBlueprints());
            File.WriteAllText(ProgramData.Paths.blueprintsFile, json);
        }

        public static void loadData() {
            if (!File.Exists(ProgramData.Paths.blueprintsFile)) return;
            string json = File.ReadAllText(ProgramData.Paths.blueprintsFile);
            List<Blueprint> blueprintsFromFile = JsonConvert.DeserializeObject<List<Blueprint>>(json);
            foreach(Blueprint blueprint in blueprintsFromFile) {
                addBlueprint(blueprint, false);
            }
        }

        #region Overloads

        // Public Functions

        public static bool doesBlueprintExist(Blueprint blueprint) {
            return doesBlueprintExist(blueprint.id);
        }
        
        public static void deleteBlueprint(int id) {
            deleteBlueprint(getBlueprint(id));
        }

        // Private Functions

        #endregion
    }
}
