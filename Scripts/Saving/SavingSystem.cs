using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
namespace Game.Saving
{
    public partial class SavingSystem : Node
    {
        public static SavingSystem instance;
        internal static SaveableManager manager;

        #region SAVING
        public void Save(string saveFile = "game")
        {
            Dictionary<string, SaveWrapper> state = LoadFile(saveFile);
            CaptureState(state);
            SaveFile(saveFile, state);
        }

        private void CaptureState(Dictionary<string, SaveWrapper> state)
        {
            foreach (SaveableEntity saveable in manager.GetSaveables())
            {
                ISaveData data = saveable.CaptureState();
                if (data == null)
                {
                    GD.PrintErr("No saveable data given");
                }
                else GD.Print(data.ToString());
                Type dataType = data.GetType();
                state[saveable.GetUniqueIdentifier()] = new SaveWrapper
                {
                    Type = dataType.AssemblyQualifiedName,
                    Data = JsonSerializer.SerializeToElement(data, dataType, new JsonSerializerOptions { WriteIndented = true })
                };
            }
        }

        private void SaveFile(string saveFile, Dictionary<string, SaveWrapper> state)
        {
            string path = GetPathFromSaveFile(saveFile);

            string data = JsonSerializer.Serialize(state, new JsonSerializerOptions
            {
                WriteIndented = true,
                IncludeFields = true
            });

            File.WriteAllText(path, data);
        }
        #endregion

        #region LOADING
        public void Load(string saveFile = "game")
        {
            RestoreState(LoadFile(saveFile));
        }

        private Dictionary<string, SaveWrapper> LoadFile(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);

            if (!File.Exists(path)) { return new Dictionary<string, SaveWrapper>(); }

            var jsonString = File.ReadAllText(path);
            var rawData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonString);
            Dictionary<string, SaveWrapper> data = new();
            foreach (KeyValuePair<string, JsonElement> kv in rawData)
            {
                data.Add(kv.Key, JsonSerializer.Deserialize<SaveWrapper>(kv.Value));
            }
            return data;
        }

        private void RestoreState(Dictionary<string, SaveWrapper> state)
        {
            foreach (SaveableEntity saveable in manager.GetSaveables())
            {
                string id = saveable.GetUniqueIdentifier();
                if (state.ContainsKey(id))
                {
                    SaveWrapper wrapper = state[id];
                    Type t = Type.GetType(wrapper.Type);
                    saveable.RestoreState((ISaveData)wrapper.Data.Deserialize(t));
                }

            }
        }
        #endregion


        public void Delete(string defaultSaveFile = "game")
        {
            File.Delete(GetPathFromSaveFile(defaultSaveFile));
        }


        private string GetPathFromSaveFile(string saveFile)
        {
            string path = "res://Saves/" + saveFile + ".save";
            return ProjectSettings.GlobalizePath(path);

        }

        public override void _Ready()
        {
            if (instance == null)
            {
                instance = this;
            }
            else QueueFree();

            manager = GetParent<SaveableManager>();
        }

        public override void _Process(double delta)
        {
            if (Input.IsActionJustPressed("ui_page_up")) { Load(); }
            if (Input.IsActionJustPressed("ui_page_down")) { Save(); }
            if (Input.IsActionJustPressed("ui_text_delete")) { Delete(); }
        }
    }
    [Serializable]
    public class SaveWrapper
    {
        public string Type { get; set; }
        public JsonElement Data { get; set; }
    }

    public interface SaveableManager
    {
        public List<SaveableEntity> GetSaveables();
        public void AddSaveable(SaveableEntity entity);
        public void RemoveSaveable(SaveableEntity entity);
    }
}

