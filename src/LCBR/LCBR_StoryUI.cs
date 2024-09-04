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

namespace LimbusLocalizeRUS
{
    public static class LCBR_StoryUI
    {
        #region Story
        [HarmonyPatch(typeof(StoryManager), nameof(StoryManager.Init))]
        [HarmonyPostfix]
        private static void StoryManager_SetData(StoryManager __instance)
        {
            __instance._dialogCon.tmp_name.lineSpacing = -25;
        }
        #endregion

        #region Introduction
        [HarmonyPatch(typeof(StoryIntroduceCharacterDescription), nameof(StoryIntroduceCharacterDescription.SetData))]
        [HarmonyPostfix]
        private static void IntroductionDescription(StoryIntroduceCharacterDescription __instance)
        {
            foreach (TextMeshProUGUI desc in __instance._textList)
            {
                Color glow = new Color(desc.color.r, desc.color.g, desc.color.b, 0.64f);
                Color underlay = new Color(desc.color.r - 0.3f, desc.color.g - 0.3f, desc.color.b - 0.3f, 1.0f);
                desc.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(15);
                desc.m_sharedMaterial.SetFloat("_GlowPower", 0.4f);
                desc.m_sharedMaterial.SetColor("_GlowColor", glow);
                desc.m_sharedMaterial.SetColor("_UnderlayColor", underlay);
            }
        }
        #endregion

        #region Walpurgis 2 Kill Count
        [HarmonyPatch(typeof(KillCountUI), nameof(KillCountUI.Init))]
        [HarmonyPostfix]
        private static void KillCount(KillCountUI __instance)
        {
            __instance._testTitleText.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(3);
            __instance._testTitleText.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(12);

            __instance._testkillCountNameText.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(4);
            __instance._testkillCountNameText.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(17);
        }
        #endregion

        #region Diary
        [HarmonyPatch(typeof(StoryNotePage), nameof(StoryNotePage.SetData))]
        [HarmonyPostfix]
        private static void Diary_Init(StoryNotePage __instance)
        {
            __instance.tmp_title.GetComponentInChildren<TextMeshProLanguageSetter>(true).enabled = false;
            __instance.tmp_title.font = LCB_Cyrillic_Font.GetCyrillicFonts(1);
            __instance.tmp_title.fontMaterial = LCB_Cyrillic_Font.GetCyrillicFonts(1).material;

            __instance.tmp_content.GetComponentInChildren<TextMeshProLanguageSetter>(true).enabled = false;
            __instance.tmp_content.font = LCB_Cyrillic_Font.GetCyrillicFonts(1);
            __instance.tmp_content.fontMaterial = LCB_Cyrillic_Font.GetCyrillicFonts(1).material;
        }
        public static void Handwriting(Transform transform)
        {
            transform.GetComponentInChildren<TextMeshProLanguageSetter>(true).enabled = false;
            transform.GetComponentInChildren<TextMeshProUGUI>(true).m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(1);
            transform.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicFonts(1).material;
            transform.GetComponentInChildren<TextMeshProUGUI>(true).lineSpacing = -20;
            if (transform.GetComponentInChildren<TextMeshProUGUI>(true).fontSize == 30)
                transform.GetComponentInChildren<TextMeshProUGUI>(true).fontSize = 46;
        }
        public static void HandwritingStroke(List<Transform> diary)
        {
            foreach (Transform stroke in diary)
            {
                Handwriting(stroke);
            }
        }
        [HarmonyPatch(typeof(StoryManager), nameof(StoryManager.Init))]
        [HarmonyPostfix]
        private static void DiaryHandwriting(StoryManager __instance)
        {
            List<Transform> le_diary = new List<Transform>()
            {
                __instance._noteEffect._diary.currentLeft.contentParent.transform.Find("[Rect]Content/[Text]Title"),
                __instance._noteEffect._diary.currentLeft.contentParent.transform.Find("[Rect]Content/[Text]Text"),
                __instance._noteEffect._diary.currentRight.contentParent.transform.Find("[Rect]Content/[Text]Title"),
                __instance._noteEffect._diary.currentRight.contentParent.transform.Find("[Rect]Content/[Text]Text")
            };
            foreach (Transform stroke in le_diary)
            {
                HandwritingStroke(le_diary);
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
            LCBR_SpriteUI.Motto_Changer(null, logo, motto);
            Image donttouch = title.Find("[Canvas]/[Image]TouchToStart").GetComponentInChildren<Image>();
            donttouch.m_OverrideSprite = LCBR_ReadmeManager.ReadmeStorySprites["Don't_Start"];
            Transform goldenbough = title.Find("[Canvas]/[Text]GoldenBoughSynchronized");
            Transform goldenbough_glow = title.Find("[Canvas]/[Text]GoldenBoughSynchronized/[Text]Glow");
            List<Transform> goldens = new List<Transform> { goldenbough, goldenbough_glow };
            List<TextMeshProUGUI> goldens_text = new List<TextMeshProUGUI> { goldenbough.GetComponentInChildren<TextMeshProUGUI>(), goldenbough_glow.GetComponentInChildren<TextMeshProUGUI>() };
            foreach (TextMeshProUGUI t in goldens_text)
            {
                t.text = "РЕЗОНАНС С ЗОЛОТОЙ ВЕТВЬЮ";
                t.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
                t.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(1);
            }
            goldenbough_glow.GetComponentInChildren<TextMeshProUGUI>(true).alpha = 0.25f;
            LCBR_TemporaryTextures.getBurnTS(goldens);
            //FAKE_LOADING
            Transform loading = __instance._loading.transform;
            TextMeshProUGUI now_l = loading.Find("[Rect]LoadingUI/Text_NowLoading").transform.GetComponentInChildren<TextMeshProUGUI>();
            now_l.text = "ЗАГРУЗКА...";
            now_l.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            now_l.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(3);
            TextMeshProUGUI clearing = loading.Find("[Rect]LoadingUI/[Text]ProgressCategory").transform.GetComponentInChildren<TextMeshProUGUI>();
            clearing.text = "ОЧИЩАЕМ МИРЫ ОТ КЭТИ";
            clearing.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            clearing.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(3);
            TextMeshProUGUI clearing_glow = loading.Find("[Rect]LoadingUI/[Text]ProgressCategory/[Text]ProgressCategoryGlow").transform.GetComponentInChildren<TextMeshProUGUI>();
            clearing_glow.text = "ОЧИЩАЕМ МИРЫ ОТ КЭТИ";
            clearing_glow.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            clearing_glow.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(3);
        }
        #endregion

        #region Heath's Cathy Dialogue Censorship
        [HarmonyPatch(typeof(Util), nameof(Util.GetDlgAfterClearingAllCathy))]
        [HarmonyPrefix]
        private static bool GetDlgAfterClearingAllCathy(string dlgId, string originString, ref string __result)
        {
            if (LCBR_Russian_Settings.IsUseRussian.Value)
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
            if (LCBR_Russian_Settings.IsUseRussian.Value)
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
            Color yellowish = new Color(1.0f, 0.306f, 0, 0.502f);
            
            TextMeshProUGUI title = __instance._titleText;
            title.color = Color.yellow;
            title.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(11);
            title.fontMaterial.EnableKeyword("GLOW_ON");
            title.fontMaterial.SetColor("_GlowColor", yellowish);
            title.fontMaterial.SetFloat("_GlowInner", (float)0.6);
            title.fontMaterial.SetFloat("_GlowPower", 0.8f);
            title.characterSpacing = 2;
        }

        [HarmonyPatch(typeof(DanteAbilityUIController), nameof(DanteAbilityUIController.SetInteract))]
        [HarmonyPostfix]
        private static void DanteAbilityUIController_SetData(DanteAbilityUIController __instance)
        {
            foreach (var sin in __instance._danteAbilitySlotList)
            {
                if (sin._danteAbilityModel._classInfo._sepira == SEPIRA.HOKMA)
                {
                    sin._danteAbilityModel._classInfo.name = "ЛЕНОСТЬ";
                }
            }
        }

        [HarmonyPatch(typeof(DanteAbilitySlot), nameof(DanteAbilitySlot.SetData))]
        [HarmonyPostfix]
        private static void DanteAbility_Sefiroth(DanteAbilitySlot __instance)
        {
            __instance._nameText.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            __instance._nameText.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(1);
            __instance._nameText.text = "ЛЕНОСТЬ";

            TextMeshProUGUI caution = __instance.transform.Find("[Image]AbilityDesc/[Text]Caution").GetComponentInChildren<TextMeshProUGUI>(true);
            caution.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            caution.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(2);
            caution.text = "ОСТОРОЖНО";
        }
        [HarmonyPatch(typeof(DanteAbilityUseAnim), nameof(DanteAbilityUseAnim.SetData))]
        [HarmonyPostfix]
        private static void DanteAbility_Animation(DanteAbilityUseAnim __instance)
        {
            Image durante = __instance.transform.Find("[Image]Durante").GetComponentInChildren<Image>(true);
            durante.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["DanteAbility_Durante"];
            if (__instance._currentSepira == SEPIRA.HOKMA)
            {
                __instance._danteAbilityNameText.text = "ЛЕНОСТЬ";
                __instance._danteAbilityNameText.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
                __instance._danteAbilityNameText.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(0);
                __instance._danteAbilityNameText.fontMaterial.SetColor("_GlowColor", __instance._danteAbilityNameText.color);
                __instance._danteAbilityNameText.fontMaterial.SetFloat("_GlowPower", 0.1f);
            }
        }
        [HarmonyPatch(typeof(EnemyHudToggle), nameof(EnemyHudToggle.SetCurrentState))]
        [HarmonyPostfix]
        private static void DanteAbility_KillCount_Enemy(EnemyHudToggle __instance)
        {
            __instance._sinButton.tmp_text.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            __instance._sinButton.tmp_text.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(2);
            __instance._sinButton.tmp_text.text = "ГРЕХИ";

            __instance._enemyPassiveButton.tmp_text.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            __instance._enemyPassiveButton.tmp_text.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(2);
            __instance._enemyPassiveButton.tmp_text.text = "<size=70%><nobr>ПАССИВКИ</nobr> ВРАГОВ</size>";
            __instance._enemyPassiveButton.tmp_text.lineSpacing = 10;
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

                __instance._storyEnterPopup._descText.text = $"Желаете ли прочесть историю из жизни {LCBR_TextUI.SinnerStory(sinner)} как{LCBR_Personality_MegaList.Personality_MegaList_Gendered(LCBR_Personality_MegaList.Personality_MegaList(faction), LCBR_TextUI.SinnerStory(sinner))}?";
            }
        }
        #endregion
    }
}
