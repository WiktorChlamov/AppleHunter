using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
namespace Game
{
    public class Data : MonoBehaviour
    {
        [Serializable]
        private struct SavingData
        {
            public int hitRecord, score, stageRecord, lang_id,adsCount;
            public List<string> iD;
            public string selectedKnife;
            public bool sound, bloom;
        }
        private static int hitRecord, currentHits, stageRecord, currentStage = 1, score, lang_id,adsCount;
        private static List<String> byedKnifes = new List<string>();
        private static string selectedKnife =string.Empty;
        private static bool sound = true, bloom = false;
        private static string SavePath => $"{Application.persistentDataPath}/save.txt";
        public static int Score { get => score; set => score = value; }
        public static int HitRecord { get => hitRecord; set => hitRecord = value; }
        public static int CurrentHits { get => currentHits; set => currentHits = value; }
        public static int StageRecord { get => stageRecord; set => stageRecord = value; }
        public static int CurrentStage { get => currentStage; set => currentStage = value; }
        public static List<string> ByedKnifes { get => byedKnifes; set => byedKnifes = value; }
        public static string SelectedKnife { get => selectedKnife; set => selectedKnife = value; }
        public static int Lang_id { get => lang_id; set => lang_id = value; }
        public static bool Sound { get => sound; set => sound = value; }
        public static int AdsCount { get => adsCount; set => adsCount = value; }
        public static bool Bloom { get => bloom; set => bloom = value; }

        public static void Save()
        {
            SaveFile(CaptureData());
        }
        public static void Load()
        {
            var state = LoadFile();
            RestoreState(state);
        }
        private static void RestoreState(SavingData state)
        {
            bloom = state.bloom;
            adsCount = state.adsCount;
            sound = state.sound;
            SelectedKnife = state.selectedKnife;
            ByedKnifes = state.iD ;
            score = state.score;
            hitRecord = state.hitRecord;
            stageRecord = state.stageRecord;
            lang_id = state.lang_id;
        }
        private static SavingData LoadFile()
        {
            if (!File.Exists(SavePath))
            {   
                ByedKnifes.Add(Materials.materials.UniqueKnife.Id);
                return new SavingData() {iD = ByedKnifes, sound = true };
            }
            using (FileStream stream = File.Open(SavePath, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                return (SavingData)formatter.Deserialize(stream);
            }
        }
        private static SavingData CaptureData()
        {
            return new SavingData
            {   bloom = bloom,
                adsCount = adsCount,
                sound = sound,
                lang_id = lang_id,
                selectedKnife = SelectedKnife,
                iD = ByedKnifes,
                score = score,
                hitRecord = hitRecord >= currentHits ? hitRecord : currentHits,
                stageRecord = stageRecord >= currentStage ? stageRecord : currentStage
            };
        }
        private static void SaveFile(object state)
        {
            using (var stream = File.Open(SavePath, FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }
    }
}