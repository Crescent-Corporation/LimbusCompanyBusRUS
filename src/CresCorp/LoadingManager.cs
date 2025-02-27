using BepInEx.Configuration;
using HarmonyLib;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using TMPro;
using System;
using Random = UnityEngine.Random;

namespace LimbusLocalizeRUS
{
    public static class LoadingManager
    {
        static List<string> LoadingTexts = new();
        static string Angela;
        public static ConfigEntry<bool> RandomLoadText = LCB_CresCorpMod.CresCorp_Settings.Bind("CresCorp Settings", "RandomLoadText", true, "Решает, будут ли появляться случайные фразы загрузки или же базовая версия ( true | false )");
        static LoadingManager() => InitLoadingTexts();
        public static void InitLoadingTexts()
        {
            LoadingTexts = File.ReadAllLines(LCB_CresCorpMod.ModPath + "/Localize/Readme/LoadingTexts.md").ToList();
            for (int i = 0; i < LoadingTexts.Count; i++)
            {
                string LoadingText = LoadingTexts[i];
                LoadingTexts[i] = LoadingText.Remove(0, 2);
            }
            Angela = LoadingTexts[0];
            LoadingTexts.RemoveAt(0);
        }
        public static T SelectOne<T>(List<T> list)
            => list.Count == 0 ? default : list[Random.Range(0, list.Count)];
        [HarmonyPatch(typeof(LoadingSceneManager), nameof(LoadingSceneManager.Start))]
        [HarmonyPostfix]
        private static void LSM_Start(LoadingSceneManager __instance)
        {
            if (!RandomLoadText.Value)
                return;
            UserDataManager instance = Singleton<UserDataManager>.Instance;
            __instance._loadingText.fontSize = 58;

            TMP_FontAsset caveat = Resources.Load<TMP_FontAsset>("Font/EN/cur)Caveat-SemiBold/Caveat-SemiBold SDF");

            caveat.fallbackFontAssetTable.Add(Cyrillics.tmpcyrillicfonts[1]);
            caveat.SetDirty();

            TMP_FontAsset bebas = Resources.Load<TMP_FontAsset>("Font/BebasKai SDF");

            bebas.fallbackFontAssetTable.Add(Cyrillics.tmpcyrillicfonts[0]);
            bebas.SetDirty();

            __instance._loadingText.m_fontAsset.SetDirty();
            int random = Random.Range(0, 100);
            if (random < 25)
            {
                __instance._loadingText.text = "<bounce f=0.5>Загрузка...</bounce>";
            }
            else if (random < 50)
            {
                __instance._loadingText.text = $"<bounce f=0.5>{Angela}</bounce>";
            }
            else
            {
                __instance._loadingText.text = $"<bounce f=0.5>{SelectOne(LoadingTexts)}</bounce>";
            }
            if (instance._unlockCodeData.CheckUnlockStatus(106))
                __instance._loadingText.text = __instance._loadingText.text.Replace("Кэти", "■■■■");
            if (__instance._loadingText.text.Contains("Дорогой дневник"))
            {
                __instance._loadingText.m_fontAsset = caveat;
                __instance._loadingText.m_sharedMaterial = caveat.material;
            }
            if (TimeOnly.FromDateTime(DateTime.Now) > TimeOnly.Parse("00:00") && TimeOnly.FromDateTime(DateTime.Now) < TimeOnly.Parse("06:00"))
            {
                if (random > 50)
                {
                    __instance._loadingText.text = "Уже поздно, вам пора отдыхать!";
                    __instance._loadingText.m_fontAsset = caveat;
                    __instance._loadingText.m_sharedMaterial = caveat.material;
                    __instance._loadingText.fontSize = 68;
                }
            }
            __instance._loadingText.SetAllDirty();
        }
    }
}
