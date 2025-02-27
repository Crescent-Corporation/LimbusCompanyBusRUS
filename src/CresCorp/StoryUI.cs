using HarmonyLib;
using StorySystem.InterEffect;
using StorySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using MainUI;
using UtilityUI;
using BattleUI;
using UI.Utility;
using Il2CppSystem.Text.RegularExpressions;

namespace LimbusLocalizeRUS
{
    public static class StoryUI
    {
        #region Story
        [HarmonyPatch(typeof(StoryManager), nameof(StoryManager.Init))]
        [HarmonyPostfix]
        private static void StoryManager_SetData(StoryManager __instance)
        {
            __instance._dialogCon.tmp_name.lineSpacing = -25;
        }
        #endregion

        #region Diary
        [HarmonyPatch(typeof(BookPageContent), nameof(BookPageContent.GetShowTextSequence))]
        [HarmonyPrefix]
        private static void Diary_Book(BookPageContent __instance)
        {
            TMP_FontAsset caveat = Resources.Load<TMP_FontAsset>("Font/EN/cur)Caveat-SemiBold/Caveat-SemiBold SDF");
            caveat.fallbackFontAssetTable.Add(Cyrillics.tmpcyrillicfonts[1]);
            if (__instance.tmp_content != null)
            {
                __instance.tmp_content.m_fontAsset = caveat;
                __instance.tmp_content.m_sharedMaterial = caveat.material;
                __instance.tmp_content.SetAllDirty();
                __instance.tmp_content.fontSize = 46f;
                __instance.tmp_content.lineSpacing = -38f;
            }
            if (__instance.tmp_title != null)
            {
                __instance.tmp_title.m_fontAsset = caveat;
                __instance.tmp_title.m_sharedMaterial = caveat.material;
                __instance.tmp_title.SetAllDirty();
                __instance.tmp_title.fontSize = 54f;
                __instance.tmp_title.lineSpacing = -25f;
                __instance.tmp_title.GetComponentInChildren<RectTransform>().anchoredPosition = new Vector2(__instance.tmp_title.GetComponentInChildren<RectTransform>().anchoredPosition.x, -85f);
            }
            if (__instance.tmp_titleReference != null)
            {
                __instance.tmp_titleReference.m_fontAsset =  caveat;
                __instance.tmp_titleReference.m_sharedMaterial = caveat.material;
                __instance.tmp_titleReference.SetAllDirty();
                __instance.tmp_titleReference.fontSize = 54f;
                __instance.tmp_titleReference.lineSpacing = -25f;
                __instance.tmp_titleReference.GetComponentInChildren<RectTransform>().anchoredPosition = new Vector2(__instance.tmp_titleReference.GetComponentInChildren<RectTransform>().anchoredPosition.x, -85f);
            }
        }
        #endregion

        #region Clear All Cathy Fake Screen
        [HarmonyPatch(typeof(StoryInterEffect_Type1), nameof(StoryInterEffect_Type1.Initialize))]
        [HarmonyPostfix]
        private static void StoryInterEffect_Type1_Init(StoryInterEffect_Type1 __instance)
        {
            //FAKE_TITLE
            Transform title = __instance._title.transform;
            Transform motto = title.Find("[Canvas]/[Image]RedLine/[Image]Phrase");
            Transform logo = title.Find("[Image]Logo");
            SpriteUI.Motto_Changer(null, logo, motto);
            Image donttouch = title.Find("[Canvas]/[Image]TouchToStart").GetComponentInChildren<Image>();
            donttouch.m_OverrideSprite = ReadmeManager.GetReadmeStorySprites("Don't_Start");
            Transform goldenbough = title.Find("[Canvas]/[Text]GoldenBoughSynchronized");
            Transform goldenbough_glow = title.Find("[Canvas]/[Text]GoldenBoughSynchronized/[Text]Glow");
            List<TextMeshProUGUI> goldens_text = new List<TextMeshProUGUI> { goldenbough.GetComponentInChildren<TextMeshProUGUI>(), goldenbough_glow.GetComponentInChildren<TextMeshProUGUI>() };
            foreach (TextMeshProUGUI t in goldens_text)
            {
                t.text = "РЕЗОНАНС С ЗОЛОТОЙ ВЕТВЬЮ";
            }
            goldenbough_glow.GetComponentInChildren<TextMeshProUGUI>(true).alpha = 0.25f;
            //FAKE_LOADING
            Transform loading = __instance._loading.transform;
            TextMeshProUGUI now_l = loading.Find("[Rect]LoadingUI/Text_NowLoading").transform.GetComponentInChildren<TextMeshProUGUI>();
            now_l.text = "ЗАГРУЗКА...";
            TextMeshProUGUI clearing = loading.Find("[Rect]LoadingUI/[Text]ProgressCategory").transform.GetComponentInChildren<TextMeshProUGUI>();
            clearing.text = "ОЧИЩАЕМ МИРЫ ОТ КЭТИ";
            TextMeshProUGUI clearing_glow = loading.Find("[Rect]LoadingUI/[Text]ProgressCategory/[Text]ProgressCategoryGlow").transform.GetComponentInChildren<TextMeshProUGUI>();
            clearing_glow.text = "ОЧИЩАЕМ МИРЫ ОТ КЭТИ";
        }
        #endregion

        #region Heath's Cathy Dialogue Censorship
        [HarmonyPatch(typeof(Util), nameof(Util.GetDlgAfterClearingAllCathy))]
        [HarmonyPrefix]
        private static bool GetDlgAfterClearingAllCathy(string dlgId, string originString, ref string __result)
        {
            if (Russian_Settings.IsUseRussian.Value)
            {
                __result = originString;
                UserDataManager instance = Singleton<UserDataManager>.Instance;
                if (instance == null || instance._unlockCodeData == null || !instance._unlockCodeData.CheckUnlockStatus(106))
                    return false;
                if ("battle_defeat_10707_1".Equals(dlgId))
                    __result = __result.Replace("Кэти", "■■■■");
                else if ("battle_dead_10704_1".Equals(dlgId))
                    __result = __result.Replace("Кэтрин", "■■■■■■");
                return false;
            }
            return true;
        }
        [HarmonyPatch(typeof(StoryPlayData), nameof(StoryPlayData.GetDialogAfterClearingAllCathy))]
        [HarmonyPrefix]
        private static bool GetDialogAfterClearingAllCathy(Scenario curStory, Dialog dialog, ref string __result)
        {
            if (Russian_Settings.IsUseRussian.Value)
            {
                __result = dialog.Content;
                UserDataManager instance = Singleton<UserDataManager>.Instance;
                if ("P10704".Equals(curStory.ID) && instance != null && instance._unlockCodeData != null && instance._unlockCodeData.CheckUnlockStatus(106) && dialog.Id == 3)
                {
                    __result = __result.Replace("Кэти", "■■■■");
                }
                return false;
            }
            return true;
        }
        #endregion

        #region Dante Ability
        [HarmonyPatch(typeof(DanteAbilityUIController), nameof(DanteAbilityUIController.UpdatePopup))]
        [HarmonyPostfix]
        private static void DanteAbilityUI_TitleChanger(DanteAbilityUIController __instance)
        {
            __instance._titleText.characterSpacing = 2;
        }

        [HarmonyPatch(typeof(DanteAbilityUIController), nameof(DanteAbilityUIController.SetInteract))]
        [HarmonyPostfix]
        private static void DanteAbilityUIController_SetData(DanteAbilityUIController __instance)
        {
            foreach (var sin in __instance._showAbilitySlotList)
            {
                if (sin._danteAbilityModel._classInfo._sepira == SEPIRA.HOKMA)
                {
                    sin._danteAbilityModel._classInfo.name = "ЛЕНОСТЬ";
                }
                if (sin._danteAbilityModel._classInfo._sepira == SEPIRA.BINAH)
                {
                    sin._danteAbilityModel._classInfo.name = "ГОРДЫНЯ";
                }
            }
        }

        [HarmonyPatch(typeof(DanteAbilityUIController), nameof(DanteAbilityUIController.SetActivePopup))]
        [HarmonyPostfix]
        private static void DanteAbilityUIController_Sefiroth(DanteAbilityUIController __instance)
        {
            foreach (var slot in __instance._showAbilitySlotList)
            {
                var caution = slot._graphicList[4].GetComponentInChildren<TextMeshProUGUI>();
                caution.text = "ОСТОРОЖНО";
                switch (slot._nameText.text)
                {
                    case "PIGRITIA":
                        slot._nameText.text = "ЛЕНОСТЬ";
                        var hokma = __instance._showAbilitySlotList[0];
                        hokma._nameImage.overrideSprite = ReadmeManager.GetReadmeStorySprites("DanteAb_Hokma_name");
                        var rectTransform = hokma._nameImage.GetComponentInChildren<RectTransform>();
                        rectTransform.sizeDelta = new Vector2(330, 80);
                        rectTransform.localPosition = new Vector2(78, -45);
                        break;
                    case "SUPERBIA":
                        slot._nameText.text = "ГОРДЫНЯ";
                        var binah = __instance._showAbilitySlotList[1];
                        binah._nameImage.overrideSprite = ReadmeManager.GetReadmeStorySprites("DanteAb_Binah_name");
                        break;
                }
            }
        }
        [HarmonyPatch(typeof(DanteAbilitySlot), nameof(DanteAbilitySlot.SetDescActive))]
        [HarmonyPostfix]
        private static void DanteAbilityUIController_Description(DanteAbilitySlot __instance)
        {
            if (__instance._nameText.enabled)
            {
                __instance._rawDescText.color = __instance._nameText.color;
            }
        }
        [HarmonyPatch(typeof(DanteAbilityUseAnim), nameof(DanteAbilityUseAnim.SetData))]
        [HarmonyPostfix]
        private static void DanteAbility_Animation(DanteAbilityUseAnim __instance)
        {
            Image durante = __instance.transform.Find("[Image]Durante").GetComponentInChildren<Image>(true);
            durante.overrideSprite = ReadmeManager.GetReadmeStorySprites("DanteAbility_Durante");
            if (__instance._currentSepira == SEPIRA.HOKMA)
            {
                __instance._danteAbilityNameImage.overrideSprite = ReadmeManager.GetReadmeStorySprites("DanteAb_Hokma");
            }
            else if (__instance._currentSepira == SEPIRA.BINAH)
            {
                __instance._danteAbilityNameImage.overrideSprite = ReadmeManager.GetReadmeStorySprites("DanteAb_Binah");
            }
        }
        [HarmonyPatch(typeof(EnemyHudToggle), nameof(EnemyHudToggle.SetCurrentState))]
        [HarmonyPostfix]
        private static void DanteAbility_KillCount_Enemy(EnemyHudToggle __instance)
        {
            __instance._sinButton.tmp_text.text = "ГРЕХИ";
            __instance._enemyPassiveButton.tmp_text.text = "<size=70%><nobr>ПАССИВКИ</nobr> ВРАГОВ</size>";
            __instance._enemyPassiveButton.tmp_text.lineSpacing = 10;
        }
        #endregion

        #region Stage Map (+Hard Events)
        [HarmonyPatch(typeof(StageProgressUI), nameof(StageProgressUI.Init))]
        [HarmonyPostfix]
        private static void EXClear_CheckAll(StageProgressUI __instance)
        {
            if (__instance.tmp_exClearProgress != null)
                __instance.tmp_exClearProgress.lineSpacing = -30;
            if (__instance.transform.Find("[Rect]ExClearProgressUI/[Image]ClearSign (1)").TryGetComponent<Image>(out Image exStamp))
                exStamp.overrideSprite = ReadmeManager.GetReadmeSprites("EX");
        }
        [HarmonyPatch(typeof(SubchapterGroupDifficultyButtonUI_MiracleDistrict20Renewal), nameof(SubchapterGroupDifficultyButtonUI_MiracleDistrict20Renewal.SetButtonText))]
        [HarmonyPostfix]
        private static void EventMap_HardMod_Toggle(SubchapterGroupDifficultyButtonUI_MiracleDistrict20Renewal __instance)
        {
            if (__instance.tmp_buttonText != null)
                __instance.tmp_buttonText.text = __instance.tmp_buttonText.text.Replace("HARD", "<size=80%><cspace=-2px>УСЛОЖНЁННАЯ</cspace></size>").Replace("NORMAL", "<size=80%>ОБЫЧНАЯ</size>");
        }
        [HarmonyPatch(typeof(SubchapterFeatureScreenFrameUI), nameof(SubchapterFeatureScreenFrameUI.InitImagePosition))]
        [HarmonyPostfix]
        private static void EventMap_HardMode_UpperFrame(SubchapterFeatureScreenFrameUI __instance)
        {
            if (__instance._upperFrameImages != null)
            {
                foreach (Image frame in __instance._upperFrameImages)
                {
                    frame.overrideSprite = ReadmeManager.GetReadmeStorySprites("EventHard_Warning");
                }
            }
            if (__instance._bottomFrameImages != null)
            {
                foreach (Image frame in __instance._bottomFrameImages)
                {
                    frame.overrideSprite = ReadmeManager.GetReadmeStorySprites("EventHard_Warning");
                }
            }
        }
        [HarmonyPatch(typeof(StageIconUISlot), nameof(StageIconUISlot.SetData))]
        [HarmonyPostfix]
        private static void EventMap_HardMode_Warnings(StageIconUISlot __instance)
        {
            Transform illustFrame = __instance.transform.Find("[Rect]ScalePivot/[Rect]Content/[Mask]/[Script]StageNodeWarningUI(Clone)");
            if (illustFrame != null)
                illustFrame.GetComponentInChildren<Image>().overrideSprite = ReadmeManager.GetReadmeStorySprites("EventHard_WarningIllust");
        }
        [HarmonyPatch(typeof(StageInfoDisplayRenewal), nameof(StageInfoDisplayRenewal.SetHardChapterDecoActive))]
        [HarmonyPostfix]
        private static void EventMap_HardMod_WarningSign(StageInfoDisplayRenewal __instance)
        {
            if (__instance.img_hardChapterIcon != null)
                __instance.img_hardChapterIcon.overrideSprite = ReadmeManager.GetReadmeStorySprites("EventHard_WarningSign");
        }
        #endregion

        #region Identity Story Text
        [HarmonyPatch(typeof(StoryTheaterUIPopup), nameof(StoryTheaterUIPopup.OpenStoryEnterPopup))]
        [HarmonyPostfix]
        private static void DescriptionChange(StoryTheaterUIPopup __instance)
        {
            __instance._storyEnterPopup._descText.GetComponentInChildren<TextMeshProLanguageSetter>(true).enabled = false;
            __instance._storyEnterPopup._descText.text = __instance._storyEnterPopup._descText.text.Replace("войти в\n", "прочесть историю, ");
            String story = __instance._storyEnterPopup._descText.text;
            if (story.EndsWith("История?"))
            {
                story = story.Replace("  ", " ");
                string[] parts = story.Split(',');
                string faction = parts[1];
                string sinner = parts[2];
                if (faction.StartsWith(" Та"))
                {
                    faction = " Та, кто держит";
                    sinner = " Фауст";
                }
                else if (faction.StartsWith(" Тот"))
                {
                    faction = " Тот, кому суждено держать";
                    sinner = " Синклер";
                }

                __instance._storyEnterPopup._descText.text = $"Желаете ли прочесть историю из жизни {TextUI.SinnerStory(sinner)} как{Personality_MegaList.Personality_MegaList_Gendered(Personality_MegaList.Personality_MegaList1(faction), TextUI.SinnerStory(sinner))}?";
            }
        }
        #endregion
    }
}