using FMODUnity;
using HarmonyLib;
using LimbusLocalizeRUS;
using StorySystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace LimbusCompanyBusRUS
{
    public class Voicelines : MonoBehaviour
    {
        public static Action<string> LogInfo = LCB_CresCorpMod.LogInfo;
        public static Action<string> LogError = LCB_CresCorpMod.LogError;
        public static List<string> localizedBanks = new();
        public static List<AudioClip> voiceline = new();
        public static List<string> voicelinenames = new();
        //public static bool AddRU1Voicelines(string path)
        //{
        //    if (!File.Exists(path))
        //        return false;

        //    bool __result = false;
        //    var allAssets = AssetBundle.LoadFromFile(path).LoadAllAssets();
        //    foreach (var asset in allAssets)
        //    {
        //        var tryCastFontAsset = asset.TryCast<AudioClip>();
        //        if (!tryCastFontAsset) continue;
        //        DontDestroyOnLoad(tryCastFontAsset);
        //        tryCastFontAsset.hideFlags |= HideFlags.HideAndDontSave;
        //        voiceline.Add(tryCastFontAsset);
        //        voicelinenames.Add(tryCastFontAsset.name);
        //        __result = true;
        //    }
        //    return __result;
        //}
        //public static bool LoadAudioClipsFromBundleAsyncWithoutCoroutine(string path)
        //{
        //    if (!File.Exists(path))
        //        return false;
        //    bool __result = false;
        //    var allAssets = AssetBundle.LoadFromFile(path).LoadAllAssetsAsync<AudioClip>();
        //    foreach (var asset in allAssets.allAssets)
        //    {
        //        var tryCastFontAsset = asset.TryCast<AudioClip>();
        //        if (!tryCastFontAsset) continue;
        //        DontDestroyOnLoad(tryCastFontAsset);
        //        tryCastFontAsset.hideFlags |= HideFlags.HideAndDontSave;
        //        voiceline.Add(tryCastFontAsset);
        //        voicelinenames.Add(tryCastFontAsset.name);
        //        __result = true;
        //    }
        //    return __result;
        //}
        public static bool AddVoicelines(string path)
        {
            if (!File.Exists(path))
                return false;
            bool __result = false;
            var allAssets = AssetBundle.LoadFromFile(path).LoadAllAssets();
            foreach (var asset in allAssets)
            {
                var tryCastFontAsset = asset.TryCast<AudioClip>();
                if (!tryCastFontAsset) continue;
                DontDestroyOnLoad(tryCastFontAsset);
                tryCastFontAsset.hideFlags |= HideFlags.HideAndDontSave;
                voiceline.Add(tryCastFontAsset);
                voicelinenames.Add(tryCastFontAsset.name);
                __result = true;
            }
            return __result;
        }
        public static AudioClip GetRUVoiceline(int idx)
        {
            int Count = voiceline.Count - 1;
            if (Count < idx)
                idx = Count;
            return voiceline[idx];
        }
        public static AudioClip FindByName(string clipName)
        {
            return voiceline.FirstOrDefault(clip => clip.name == clipName);
        }

        //StorySoundController.CallVoice ==== {code} - 1D101A-02 || {storyID} - 1D101A              || {name} - 1D101A-02
        //                                    {code} - S401V-465 || {storyID} - S410B


        //PlayStoryVoice_Custom ==== {name} - 1D101A-02 || {storyID} - 1D101A || event:/Voice_Story/{storyID}/{name}
        //PlayStoryVoice_Custom ==== string bankPath = $"{LCB_CresCorpMod.ModPath}/Localize/RU_Voices/{storyID}.bank";
        //public static AudioSource audioSource;
        [HarmonyPatch(typeof(StorySoundController), nameof(StorySoundController.CallVoice))]
        [HarmonyPostfix]
        public static void StorySoundController2(StorySoundController __instance, ref string code, ref string storyID)
        {
            string name = Regex.Replace(code, "\\s", "").Split(':', StringSplitOptions.None)[0];
            if (code.Contains(storyID[..2]))
            {
                AudioSource audioSource = __instance.gameObject.GetComponent<AudioSource>();
                if(audioSource == null)
                    audioSource = __instance.gameObject.AddComponent<AudioSource>();
                PlayStoryVoice_Custom(name, storyID, audioSource);
                return;
            }
            PlayStoryVoice_Custom(name, null, null);
        }
        public static void PlayStoryVoice_Custom(string name, string storyID, AudioSource audioSource)
        {
            VoiceGenerator.StopVoiceSound();
            if (storyID != null)
            {
                if (localizedBanks.Contains(storyID))
                {
                    //C:\Program Files (x86)\Steam\steamapps\common\Limbus Company\BepInEx\plugins\LCBR\Localize\Voicelines\1D101A\1D101A-02.wav
                    //string path = $"file://{LCB_CresCorpMod.ModPath}\\Localize\\Voicelines\\{storyID}\\{name}.wav";

                    audioSource.clip = FindByName(name);
                    audioSource.Play();
                    //VoiceGenerator.SetMainVoice(VoiceGenerator.CreateVoiceInstance("event:/Voice_Story_3DT/" + storyID + "/" + name, false), -1);
                }
                else
                {
                    VoiceGenerator.SetMainVoice(VoiceGenerator.CreateVoiceInstance(VoiceGenerator.VOICE_STORY_EVENT_PATH + storyID + "/" + name, false), -1);
                }
                return;
            }
            VoiceGenerator.SetMainVoice(VoiceGenerator.CreateVoiceInstance(VoiceGenerator.VOICE_EVENT_PATH + "Default/" + name, false), -1);
        }
        //public static void SetMainVoice_Custom(EventInstance instance, int charid)
        //{
        //    if (!instance.isValid())
        //    {
        //        return;
        //    }
        //    if (VoiceGenerator.mainVoice != null)
        //    {
        //        VoiceGenerator.mainVoice.instance_Voice.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //        VoiceGenerator.mainVoice.instance_Voice.release();
        //    }
        //    VoiceEventClass voiceEventClass = new VoiceEventClass();
        //    voiceEventClass.instance_Voice = instance;
        //    voiceEventClass.callback_eventEnd = VoiceGenerator._studioEndCallback;
        //    voiceEventClass.instance_Voice.setCallback(voiceEventClass.callback_eventEnd, (EVENT_CALLBACK_TYPE)4294967295U);
        //    voiceEventClass.instance_Voice.setParameterByName("Volume_Multi", 1f, false);
        //    if (Camera.main != null)
        //    {
        //        voiceEventClass.instance_Voice.set3DAttributes(Camera.main.transform.position.To3DAttributes());
        //    }
        //    voiceEventClass.personalityID = charid;
        //    voiceEventClass.length = SingletonBehavior<SoundManager>.Instance.GetEventLength(instance);
        //    VoiceGenerator.PlayVoiceEvent(voiceEventClass);
        //    VoiceGenerator.mainVoice = voiceEventClass;
        //}
        //public static EventInstance CreateVoiceInstance_Custom(string path, bool isSpecial = false)
        //{
        //    if (RuntimeManager.StudioSystem.getEvent(path, out _) == RESULT.OK)
        //    {
        //        EventInstance result = RuntimeManager.CreateInstance(path);
        //        if (isSpecial)
        //        {
        //            result.setParameterByName("isSpecial", 1f, false);
        //        }
        //        return result;
        //    }
        //    return default;
        //}




        [HarmonyPatch(typeof(RuntimeManager), nameof(RuntimeManager.LoadBankFromFile))]
        [HarmonyPostfix]
        public static void RuntimeManager1_Debug(RuntimeManager __instance, ref TextAsset asset, ref bool loadSamples)
        {
            LCB_CresCorpMod.LogInfo($"2_LoadBankFromFile: {asset.name} : {loadSamples}");
        }
        [HarmonyPatch(typeof(RuntimeManager), nameof(RuntimeManager.LoadBankFromFileDirect))]
        [HarmonyPostfix]
        public static void RuntimeManager1_Debug(RuntimeManager __instance, ref string bankPath, ref bool loadSamples)
        {
            LCB_CresCorpMod.LogInfo($"2_LoadBankFromFileDirect: {bankPath} : {loadSamples}");
        }
        [HarmonyPatch(typeof(RuntimeManager), nameof(RuntimeManager.LoadBanks))]
        [HarmonyPostfix]
        public static void RuntimeManager2_Debug(RuntimeManager __instance, ref FMODUnity.Settings fmodSettings)
        {
            LCB_CresCorpMod.LogInfo($"2_LoadBanks: {fmodSettings.BanksToLoad.Count} : {fmodSettings.Banks.Count} : {fmodSettings.MasterBanks.Count}");
        }
        [HarmonyPatch(typeof(SoundManager), nameof(SoundManager.LoadBank))]
        [HarmonyPostfix]
        public static void SoundManager_Debug(SoundManager __instance, ref List<string> bankids)
        {
            LCB_CresCorpMod.LogInfo($"2_SoundManager: {bankids.Count}");
        }
        [HarmonyPatch(typeof(RuntimeManager), nameof(FMODUnity.RuntimeManager.UnloadBank))]
        [HarmonyPostfix]
        public static void SoundManager222133_Debug(RuntimeManager __instance, ref string bankName)
        {
            LCB_CresCorpMod.LogInfo($"2_UnloadBank: {bankName}");
        }
    }
}
