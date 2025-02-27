using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using System;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;

namespace LimbusLocalizeRUS
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class LCB_CresCorpMod : BasePlugin
    {
        public static ConfigFile CresCorp_Settings;
        public static string ModPath;
        public static string GamePath;
        public const string GUID = "com.CresCorp.LocalizeLimbusCompanyRUS";
        public const string NAME = "LimbusLocalizeRUS";
        public const string VERSION = "0.4.2";
        public const string VERSION_STATE = "";
        public const string AUTHOR = "Base: Bright\nRUS version: Knightey, abcdcode, Disaer";
        public const string LCBRLink = "https://github.com/Crescent-Corporation/LimbusCompanyBusRUS/tree/LC_branch_ORIGINAL";
        public static Action<string, Action> LogFatalError { get; set; }
        public static Action<string> LogInfo { get; set; }
        public static Action<string> LogError { get; set; }
        public static Action<string> LogWarning { get; set; }
        public static void OpenLCBRURL() => Application.OpenURL(LCBRLink);
        public static void OpenGamePath() => Application.OpenURL(GamePath);
        public static bool FauxDevMode = false;
        public override void Load()
        {
            CresCorp_Settings = Config;
            LogInfo = (string log) => { Log.LogInfo(log); Debug.Log(log); };
            LogError = (string log) => { Log.LogError(log); Debug.LogError(log); };
            LogWarning = (string log) => { Log.LogWarning(log); Debug.LogWarning(log); };
            LogFatalError = (string log, Action action) => { Manager.FatalErrorlog += log + "\n"; LogError(log); Manager.FatalErrorAction = action; Manager.CheckModActions(); };
            GamePath = new System.IO.DirectoryInfo(Application.dataPath).Parent.FullName;
            var matchingFiles = System.IO.Directory.EnumerateFiles(GamePath + "\\BepInEx\\plugins", "LimbusCompanyBusRUS_BIE.dll", System.IO.SearchOption.AllDirectories);
            foreach (var filePath in matchingFiles)
            {
                ModPath = System.IO.Path.GetDirectoryName(filePath);
                if (ModPath.Contains("LCBR_Dev"))
                    FauxDevMode = true;
            }
        UpdateChecker.StartAutoUpdate();
            try
            {
                HarmonyLib.Harmony harmony = new(NAME);
                if (Russian_Settings.IsUseRussian.Value)
                {
                    Manager.InitLocalizes(new System.IO.DirectoryInfo(ModPath + "/Localize/RU"));
                    harmony.PatchAll(typeof(Cyrillics));
                    harmony.PatchAll(typeof(ReadmeManager));
                    harmony.PatchAll(typeof(LoadingManager));
                    
                    ApplyPatches(harmony, typeof(SpriteUI));
                    ApplyPatches(harmony, typeof(TextUI));
                    ApplyPatches(harmony, typeof(StoryUI));
                    ApplyPatches(harmony, typeof(CreditsUI));
                    ApplyPatches(harmony, typeof(EventUI));
                    ApplyPatches(harmony, typeof(SeasonUI));
                }
                harmony.PatchAll(typeof(Manager));
                harmony.PatchAll(typeof(Russian_Settings));
                if (!Cyrillics.AddCyrillicFont(ModPath + "/tmpcyrillicfonts"))
                    LogFatalError("You have forgotten to install Font Update Mod. Please, reread README on Github.", OpenLCBRURL);
                LogInfo(AUTHOR);
                LogInfo("Fonts: ");
                for (int i = 0; i < Cyrillics.tmpcyrillicfonts.Count; i++)
                {
                    LogInfo(Cyrillics.GetCyrillicFonts(i).name + " " + i);
                }
                LogInfo("-------------------------\n");
                TMP_FontAsset pretendard = Resources.Load<TMP_FontAsset>("Font/EN/Pretendard/Pretendard-Regular SDF");
                TMP_FontAsset liberation = Resources.Load<TMP_FontAsset>("Fonts & Materials/LiberationSans SDF");
                TMP_FontAsset excel = Resources.Load<TMP_FontAsset>("Font/ExcelsiorSans SDF");
                TMP_FontAsset caveat = Resources.Load<TMP_FontAsset>("Font/EN/cur)Caveat-SemiBold/Caveat-SemiBold SDF");
                LogInfo("Startup at:" + DateTime.Now.ToString());

                excel.fallbackFontAssetTable.Remove(pretendard);
                excel.fallbackFontAssetTable.Remove(liberation);
                excel.fallbackFontAssetTable.Add(Cyrillics.tmpcyrillicfonts[2]);
                caveat.fallbackFontAssetTable.Add(Cyrillics.tmpcyrillicfonts[1]);
            }
            catch (Exception e)
            {
                LogFatalError("Вы нашли редкое окно фатальной ошибки. Передайте нам логи через Гитхаб, пожалуйста.\n\n<b>Нает, если ты это читаешь — ты лох.</b>", () => { CopyLog(); OpenGamePath(); OpenLCBRURL(); });
                LogError(e.ToString());
            }
        }
        public static void ApplyPatches(Harmony harmony, Type patchContainer)
        {
            var methods = patchContainer.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            foreach (var method in methods)
            {
                var harmonyPatchAttributes = method.GetCustomAttributes(typeof(HarmonyPatch), false);
                if (harmonyPatchAttributes.Length > 0)
                {
                    try
                    {
                        ApplyPatch(harmony, method);
                    }
                    catch (Exception ex)
                    {
                        LogWarning($"Failed to apply patch for method {method.Name}: {ex.Message}");
                    }
                }
            }
        }

        private static void ApplyPatch(Harmony harmony, MethodInfo patchMethod)
        {
            var harmonyPatchAttributes = (HarmonyPatch[])patchMethod.GetCustomAttributes(typeof(HarmonyPatch), false);
            foreach (var patchAttribute in harmonyPatchAttributes)
            {
                var targetType = patchAttribute.info.declaringType;
                var targetMethodName = patchAttribute.info.methodName;
                var targetMethod = AccessTools.Method(targetType, targetMethodName);
                var prefix = patchMethod.GetCustomAttributes(typeof(HarmonyPrefix), false).Any() ? new HarmonyMethod(patchMethod) : null;
                var postfix = patchMethod.GetCustomAttributes(typeof(HarmonyPostfix), false).Any() ? new HarmonyMethod(patchMethod) : null;
                if (targetType == null || string.IsNullOrEmpty(targetMethodName))
                {
                    continue;
                }
                if (targetMethod == null)
                {
                    continue;
                }
                try
                {
                    harmony.Patch(targetMethod, prefix, postfix);
                }
                catch (Exception ex)
                {
                    LogError($"Failed to patch {targetMethodName} in {targetType.FullName}: {ex.Message}");
                }
            }
        }
        public static void CopyLog()
        {
            System.IO.File.Copy(GamePath + "/BepInEx/LogOutput.log", GamePath + "/Latest.log", true);
            System.IO.File.Copy(Application.consoleLogPath, GamePath + "/Player.log", true);
        }
    }
}