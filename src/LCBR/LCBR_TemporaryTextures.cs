﻿using BattleUI;
using BattleUI.Information;
using HarmonyLib;
using MainUI;
using MainUI.VendingMachine;
using TMPro;
using UnityEngine;
using MainUI.Gacha;
using BattleUI.Typo;
using BattleUI.BattleUnit;
using System.Collections.Generic;
using UtilityUI;
using MainUI.BattleResult;
using Il2CppSystem.Security.Cryptography;

namespace LimbusLocalizeRUS
{
    internal class LCBR_TemporaryTextures
    {
        #region MainMenu
        public static void getBurnTS(List<Transform> transforms)
        {
            foreach (Transform t in transforms)
            {
                getBurnT(t);
            }
        }
        public static void getBurnT(Transform t)
        {
            Color yellowish = new Color(1.0f, 0.306f, 0, 0.502f);
            t.GetComponentInChildren<TextMeshProUGUI>().m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(11);
            t.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.EnableKeyword("GLOW_ON");
            t.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.SetColor("_GlowColor", yellowish);
            t.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.SetFloat("_GlowInner", (float)0.6);
            t.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.SetFloat("_GlowPower", 0.8f);
            t.GetComponentInChildren<TextMeshProUGUI>().characterSpacing = 2;
        }
        [HarmonyPatch(typeof(ChapterSelectionUIPanel), nameof(ChapterSelectionUIPanel.SetDataOpen))]
        [HarmonyPostfix]
        private static void ChapterSelectionUIPanel_Init(ChapterSelectionUIPanel __instance)
        {
            List<Transform> transforms = new List<Transform>
            {
                __instance._rewardDungeonBanner.transform.Find("[Rect]Items/[Text]Entrance"),
                __instance._rewardDungeonBanner.transform.Find("[Rect]Items/[Text]Entrance/[Text]Entrance (1)"),
                __instance._mirrorDungeonBanner.transform.Find("[Rect]Items/[Text]Entrance"),
                __instance._mirrorDungeonBanner.transform.Find("[Rect]Items/[Text]Entrance/[Text]Entrance (1)"),
                __instance._railwayDungeonBanner.transform.Find("[Rect]Items/[Text]Entrance"),
                __instance._railwayDungeonBanner.transform.Find("[Rect]Items/[Text]Entrance/[Text]Entrance (1)")
            };
            getBurnTS(transforms);

            Transform railway_line = __instance._railwayDungeonBanner.transform.Find("[Rect]Items/[Image]ImageBackground/[Image]Image/[Text]RailTextDeco");
            Color railway = railway_line.GetComponentInChildren<TextMeshProUGUI>(true).color;
            railway_line.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(11);
            railway_line.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetColor("_GlowColor", railway);
            railway_line.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetColor("_UnderlayColor", ColorSchemes["charcoal"]);
            railway_line.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.SetFloat("_GlowInner", 0.4f);
            railway_line.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.SetFloat("_GlowOuter", 0.4f);
            railway_line.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.SetFloat("_GlowPower", 0.8f);
            railway_line.GetComponentInChildren<TextMeshProUGUI>(true).characterSpacing = 3;
        }
        private static Material glowingBurn = new Material(LCB_Cyrillic_Font.GetCyrillicMats(11));
        [HarmonyPatch(typeof(RailwayDungeonUIPanel), nameof(RailwayDungeonUIPanel.SetDataOpen))]
        [HarmonyPostfix]
        private static void RailwayDungeonUIPanel_Init(RailwayDungeonUIPanel __instance)
        {
            List<Transform> transforms = new List<Transform>
            {
                __instance._upperUI.transform.Find("[Text]Title_Highlight"),
                __instance._upperUI.transform.Find("[Text]Title_Highlight/[Text]Title_Highlight")
            };
            getBurnTS(transforms);
        }
        [HarmonyPatch(typeof(GetNewCardTimelineScriptBase), nameof(GetNewCardTimelineScriptBase.Init))]
        [HarmonyPostfix]
        private static void GetNewCardTimelineScriptBase_Init(GetNewCardTimelineScriptBase __instance)
        {
            Transform subtitle = __instance._subtitle.transform.Find("TextRect/TextLayout/[LocalizeText]Subtitle");
            subtitle.GetComponentInChildren<TextMeshProUGUI>().m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(15);
        }
        [HarmonyPatch(typeof(GachaUIPanel), nameof(GachaUIPanel.SetGachaInfoPanel))]
        [HarmonyPostfix]
        private static void Gacha_Init(GachaUIPanel __instance)
        {
            Transform yisang = __instance.transform.Find("[Rect]CurrentGachaPage/ReasonPointAnchor/ReasonPointIndicator/tmp_number_of_extract");
            yisang.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(17);
            yisang.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.EnableKeyword("UNDERLAY_ON");
            yisang.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetColor("_UnderlayColor", ColorSchemes["charcoal"]);
            yisang.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetFloat("_UnderlayOffsetX", 5);
            yisang.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetFloat("_UnderlayOffsetY", -5);
            yisang.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetFloat("_UnderlayDilate", 3);
            yisang.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetFloat("_UnderlaySoftness", 0);
            Transform exchange = __instance.transform.Find("[Rect]CurrentGachaPage/ReasonPointAnchor/ReasonPointIndicator/tmp_exchange");
            exchange.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(17);
            exchange.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.EnableKeyword("UNDERLAY_ON");
            exchange.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetColor("_UnderlayColor", ColorSchemes["charcoal"]);
            exchange.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetFloat("_UnderlayOffsetX", 5);
            exchange.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetFloat("_UnderlayOffsetY", -5);
            exchange.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetFloat("_UnderlayDilate", 3);
            exchange.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetFloat("_UnderlaySoftness", 0);
        }
        [HarmonyPatch(typeof(StageEventBanner), nameof(StageEventBanner.Init))]
        [HarmonyPostfix]
        private static void EventOpen_Init(StageEventBanner __instance)
        {
            Color yellowish = new Color(1.0f, 0.306f, 0, 0.502f);
            __instance._eventStateUI.tmp_eventState.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(9);
            __instance._eventStateUI.tmp_eventState.color = Color.yellow;
            __instance._eventStateUI.tmp_eventState.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.EnableKeyword("GLOW_ON");
            __instance._eventStateUI.tmp_eventState.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.SetColor("_GlowColor", yellowish);
            __instance._eventStateUI.tmp_eventState.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.SetFloat("_GlowInner", (float)0.6);
            __instance._eventStateUI.tmp_eventState.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.SetFloat("_GlowPower", 3);
        }



        public static void getBurnEventT(Transform t)
        {
            Color yellowish = new Color(1.0f, 0.306f, 0, 0.502f);
            t.GetComponentInChildren<TextMeshProUGUI>().m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(9);
            t.GetComponentInChildren<TextMeshProUGUI>().color = Color.yellow;
            t.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.EnableKeyword("GLOW_ON");
            t.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.SetColor("_GlowColor", yellowish);
            t.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.SetFloat("_GlowInner", (float)0.6);
            t.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.SetFloat("_GlowPower", 3);
            t.GetComponentInChildren<TextMeshProUGUI>().characterSpacing = 2;
        }
        [HarmonyPatch(typeof(EventStateUI), nameof(EventStateUI.SetBannerText))]
        [HarmonyPostfix]
        private static void EventStateUI_Init(EventStateUI __instance)
        {
            getBurnEventT(__instance.tmp_eventState.transform);
        }
        [HarmonyPatch(typeof(SubChapterScrollViewItem), nameof(SubChapterScrollViewItem.SetData))]
        [HarmonyPostfix]
        private static void SubChapterScrollViewItem_Init(SubChapterScrollViewItem __instance)
        {
            getBurnEventT(__instance.tmp_eventState.transform);
        }



        [HarmonyPatch(typeof(StageStoryNodeSelectUI), nameof(StageStoryNodeSelectUI.OnStorySelect))]
        [HarmonyPostfix]
        private static void StoryNodeUI_Init(StageStoryNodeSelectUI __instance)
        {
            Transform episode = __instance.transform.Find("[Rect]Desc/[Image]Background/[Image]Panel/[Rect]TItleNodeStage/[Text]Episode");
            episode.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            episode.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.GetCyrillicMats(3);
            episode.GetComponentInChildren<TextMeshProUGUI>(true).text = "ЭПИЗОД";
        }
        #endregion

        #region Enemy Info
        [HarmonyPatch(typeof(UnitInformationEnemyNameTag), nameof(UnitInformationEnemyNameTag.SetData))]
        [HarmonyPostfix]
        private static void EnemyNameTag(UnitInformationEnemyNameTag __instance)
        {
            Color bronze = new Color(0.64f, 0.24f, 0.07f, 1.0f);
            Color bloody_red = new Color(0.51f, 0.05f, 0.02f, 1.0f);
            __instance.tmp_name.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(12);
            __instance.tmp_name.fontMaterial.SetColor("_UnderlayColor", bloody_red);
            __instance.tmp_name.fontMaterial.SetColor("_FaceColor", bronze);
        }
        [HarmonyPatch(typeof(UnitInfoPersonalityNameTag), nameof(UnitInfoPersonalityNameTag.SetData))]
        [HarmonyPostfix]
        private static void PersonalityNameTag(UnitInfoPersonalityNameTag __instance)
        {
            Color milky = new Color(0.92156863f, 0.7921569f, 0.63529414f, 1.0f);
            Color bronze = new Color(0.64f, 0.24f, 0.07f, 1.0f);
            __instance.tmp_name.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(12);
            
            if (__instance.img_RankUI.isActiveAndEnabled == true)
            {
                __instance.tmp_name.color = milky;
                __instance.tmp_name.fontMaterial.SetColor("_UnderlayColor", ColorSchemes["charcoal"]);
            }
            else if (__instance.img_egoRankUI.isActiveAndEnabled == true)
            {
                __instance.tmp_name.color = ColorSchemes["charcoal"];
                __instance.tmp_name.fontMaterial.SetColor("_UnderlayColor", bronze);
            }
        }
        [HarmonyPatch(typeof(UnitInformationAbnormalityNameTag), nameof(UnitInformationAbnormalityNameTag.SetData))]
        [HarmonyPostfix]
        private static void AbnormalityNameTag(UnitInformationAbnormalityNameTag __instance)
        {
            Color bronze = new Color(0.64f, 0.24f, 0.07f, 1.0f);
            Color bloody_red = new Color(0.51f, 0.05f, 0.02f, 1.0f);
            __instance.tmp_name.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(12);
            __instance.tmp_name.fontMaterial.SetColor("_UnderlayColor", bloody_red);
            __instance.tmp_name.fontMaterial.SetColor("_FaceColor", bronze);
        }
        #endregion

        #region Sinner UI
        [HarmonyPatch(typeof(UnitInformationPersonalitySkillTypeButton), nameof(UnitInformationPersonalitySkillTypeButton.SetSelectedState))]
        [HarmonyPostfix]
        private static void SkillType_Materials(UnitInformationPersonalitySkillTypeButton __instance)
        {
            __instance.tmp_skilType.GetComponentInChildren<TextMeshProLanguageSetter>(true).enabled = false;

            if (__instance.IsFoucused == true)
            {
                Color yellowish = new Color(1.0f, 0.306f, 0, 0.502f);
                __instance.tmp_skilType.color = Color.yellow;
                __instance.tmp_skilType.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(11);
                __instance.tmp_skilType.fontMaterial.EnableKeyword("GLOW_ON");
                __instance.tmp_skilType.fontMaterial.SetColor("_GlowColor", yellowish);
                __instance.tmp_skilType.fontMaterial.SetFloat("_GlowInner", (float)0.6);
                __instance.tmp_skilType.fontMaterial.SetFloat("_GlowPower", 0.8f);
                __instance.tmp_skilType.characterSpacing = 3;
            }
            else if (__instance._isEntered == true)
            {
                Color yellowish = new Color(1.0f, 0.306f, 0, 0.502f);
                __instance.tmp_skilType.color = Color.yellow;
                __instance.tmp_skilType.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(11);
                __instance.tmp_skilType.fontMaterial.EnableKeyword("GLOW_ON");
                __instance.tmp_skilType.fontMaterial.SetColor("_GlowColor", yellowish);
                __instance.tmp_skilType.fontMaterial.SetFloat("_GlowInner", (float)0.6);
                __instance.tmp_skilType.fontMaterial.SetFloat("_GlowPower", 0.8f);
                __instance.tmp_skilType.characterSpacing = 3;
            }
            else
            {
                Color milky = new Color(0.92156863f, 0.7921569f, 0.63529414f, 1.0f);
                __instance.tmp_skilType.color = milky;
                __instance.tmp_skilType.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(12);
                __instance.tmp_skilType.fontMaterial.DisableKeyword("GLOW_ON");
                __instance.tmp_skilType.characterSpacing = 2;
            }
        }
        #endregion

        #region Details
        [HarmonyPatch(typeof(VendingMachineUIPanel), nameof(VendingMachineUIPanel.ClickBannerEvent))]
        [HarmonyPostfix]
        private static void VendingMachineUI_Init(VendingMachineUIPanel __instance)
        {
            __instance.tmp_notice_sold_out.GetComponentInChildren<TextMeshProLanguageSetter>().enabled = false;
            Color yellowish = new Color(1.0f, 0.306f, 0, 0.502f);
            //ActiveRect/VendingMachineUIPanel/
            Transform sold_out = __instance.transform.Find("GoodsStoreAreaMaster/GoodsStorePanelGroup/BackPanel/Main/SoldOut/[LocalizeText]WaitRestock");
            if (sold_out != null)
            {
                sold_out.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(11);
                sold_out.GetComponentInChildren<TextMeshProUGUI>(true).fontSharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(11);
                sold_out.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetColor("_GlowColor", yellowish);
                sold_out.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetFloat("_GlowInner", (float)0.6);
                sold_out.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetFloat("_GlowPower", 3);
                __instance.tmp_notice_sold_out.m_currentMaterial = LCB_Cyrillic_Font.GetCyrillicMats(11);
                __instance.tmp_notice_sold_out.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(11);
                __instance.tmp_notice_sold_out.fontSharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(11);
                __instance.tmp_notice_sold_out.fontMaterial.EnableKeyword("GLOW_ON");
                __instance.tmp_notice_sold_out.fontMaterial.SetColor("_GlowColor", yellowish);
                __instance.tmp_notice_sold_out.fontMaterial.SetFloat("_GlowInner", (float)0.1);
                __instance.tmp_notice_sold_out.fontMaterial.SetFloat("_GlowPower", 0.8f);
                __instance.tmp_notice_sold_out.characterSpacing = 2;

            }
        }
        [HarmonyPatch(typeof(GacksungLevelUpCompletionPopup), nameof(GacksungLevelUpCompletionPopup.UpdateAcquiredContentsLayout))]
        [HarmonyPostfix]
        private static void GacksungLevelUpCompletionPopup_Init(GacksungLevelUpCompletionPopup __instance)
        {
            //Update
            Color yellowish = new Color(1.0f, 0.443f, 0, 0.275f);
            Color blueish = new Color(0.451f, 0.620f, 0.710f, 0.175f);
            Color tier_default = new Color(255, 255, 0, 145);
            Color pm_yellow = new Color(0.969f, 0.765f, 0, 1.0f);
            __instance.tmp_contentTitle.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(3);
            __instance.tmp_contentTitle.transform.GetComponentInChildren<TextMeshProUGUI>().m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(11);
            __instance.tmp_contentTitle.transform.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.EnableKeyword("GLOW_ON");
            __instance.tmp_contentTitle.transform.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.SetFloat("_GlowInner", 0.6f);
            __instance.tmp_contentTitle.transform.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.SetFloat("_GlowOuter", 0.4f);
            __instance.tmp_contentTitle.transform.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.SetFloat("_GlowPower", 3);
            __instance.tmp_contentTitle.transform.GetComponentInChildren<TextMeshProUGUI>().characterSpacing = 2;
            __instance.tmp_contentTitle.characterSpacing = 2;
            if (__instance.img_upperChain.activeSprite.name.StartsWith("MainUI_PersonalityList_9_7") || __instance.tmp_egoDeco_upper.faceColor == tier_default)
            {
                __instance.tmp_contentTitle.color = pm_yellow;
                __instance.tmp_contentTitle.fontMaterial.SetColor("_GlowColor", yellowish);
            }
            else
            {
                __instance.tmp_contentTitle.color = Color.white;
                __instance.tmp_contentTitle.fontMaterial.SetColor("_GlowColor", blueish);
            }
        }
        [HarmonyPatch(typeof(PlayerLevelUpUIPopup), nameof(PlayerLevelUpUIPopup.OpenAndSetup))]
        [HarmonyPostfix]
        private static void NewManagerLevel_Init(PlayerLevelUpUIPopup __instance)
        {
            Color yellowish = new Color(1.0f, 0.306f, 0, 0.275f);
            Color pm_yellow = new Color(0.969f, 0.765f, 0, 1.0f);
            __instance.tmp_user_level_up_notice.fontSharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(11);
            __instance.tmp_user_level_up_notice.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(11);
            __instance.tmp_user_level_up_notice.color = pm_yellow;
            __instance.tmp_user_level_up_notice.characterSpacing = 2;
            __instance.tmp_user_level_up_notice.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.EnableKeyword("GLOW_ON");
            __instance.tmp_user_level_up_notice.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.SetColor("_GlowColor", yellowish);
            __instance.tmp_user_level_up_notice.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.SetFloat("_GlowInner", 0.6f);
            __instance.tmp_user_level_up_notice.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.SetFloat("_GlowPower", 3);
        }
        [HarmonyPatch(typeof(PersonalityLevelLimitReleaseUIPopup), nameof(PersonalityLevelLimitReleaseUIPopup.Initialize))]
        [HarmonyPostfix]
        private static void NewPersonalityLevels_Init(PersonalityLevelLimitReleaseUIPopup __instance)
        {
            Color yellowish = new Color(1.0f, 0.306f, 0, 0.275f);
            Color pm_yellow = new Color(0.969f, 0.765f, 0, 1.0f);
            __instance.tmp_mainTitle.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(3);
            __instance.tmp_mainTitle.fontSharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(11);
            __instance.tmp_mainTitle.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(11);
            __instance.tmp_mainTitle.color = pm_yellow;
            __instance.tmp_mainTitle.characterSpacing = 2;
            __instance.tmp_mainTitle.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.EnableKeyword("GLOW_ON");
            __instance.tmp_mainTitle.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.SetColor("_GlowColor", yellowish);
            __instance.tmp_mainTitle.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.SetFloat("_GlowInner", 0.6f);
            __instance.tmp_mainTitle.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.SetFloat("_GlowPower", 3);
        }
        [HarmonyPatch(typeof(UITextDataLoader), nameof(UITextDataLoader.SetText))]
        [HarmonyPostfix]
        private static void ChangeSinnerDungeonStay(UITextDataLoader __instance)
        {
            Color yellowish = new Color(1.0f, 0.506f, 0, 0.502f);
            Color yellow = new Color(0.97f, 0.76f, 0, 1.0f);
            if (__instance.txt_tmpro.text.Contains("грешника"))
            {
                __instance.txt_tmpro.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(11);
                __instance.txt_tmpro.fontMaterial.SetColor("_GlowColor", yellowish);
                __instance.txt_tmpro.color = yellow;
                __instance.txt_tmpro.fontMaterial.SetFloat("_GlowInner", 0.4f);
                __instance.txt_tmpro.fontMaterial.SetFloat("_GlowOuter", 0.4f);
                __instance.txt_tmpro.fontMaterial.SetFloat("_GlowPower", 0.8f);
            }
            else if (__instance.txt_tmpro.text.EndsWith("участников"))
            {
                __instance.txt_tmpro.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(11);
                __instance.txt_tmpro.fontMaterial.SetColor("_GlowColor", ColorSchemes["charcoal"]);
                __instance.txt_tmpro.color = Color.gray;
                __instance.txt_tmpro.fontMaterial.SetFloat("_GlowInner", 0.4f);
                __instance.txt_tmpro.fontMaterial.SetFloat("_GlowOuter", 0.4f);
                __instance.txt_tmpro.fontMaterial.SetFloat("_GlowPower", 0.4f);
            }
        }
        [HarmonyPatch(typeof(Formation_PortraitEventSlotUI), nameof(Formation_PortraitEventSlotUI.SetDefault))]
        [HarmonyPostfix]
        private static void ChangeSinner(Formation_PortraitEventSlotUI __instance)
        {
            Color yellowish = new Color(1.0f, 0.506f, 0, 0.502f);
            Color yellow = new Color(0.97f, 0.76f, 0, 1.0f);
            Transform just_work = __instance.transform.Find("[Image]HoverImage/[Text]Selected");
            just_work.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(11);
            just_work.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetColor("_GlowColor", yellowish);
            just_work.GetComponentInChildren<TextMeshProUGUI>(true).color = yellow;
            just_work.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetFloat("_GlowInner", 0.4f);
            just_work.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetFloat("_GlowOuter", 0.4f);
            just_work.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetFloat("_GlowPower", 0.8f);
            if (just_work.GetComponentInChildren<TextMeshProUGUI>(true).text.EndsWith("участников"))
            {
                just_work.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(11);
                just_work.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetColor("_GlowColor", ColorSchemes["charcoal"]);
                just_work.GetComponentInChildren<TextMeshProUGUI>(true).color = Color.gray;
                just_work.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetFloat("_GlowInner", 0.4f);
                just_work.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetFloat("_GlowOuter", 0.4f);
                just_work.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetFloat("_GlowPower", 0.4f);
            }
        }
        [HarmonyPatch(typeof(ActReceivedGoldenBough), nameof(ActReceivedGoldenBough.Init))]
        [HarmonyPostfix]
        private static void GoldenBoughDone(ActReceivedGoldenBough __instance)
        {
            __instance.tmp_received_the_golden_bough.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            __instance.tmp_received_the_golden_bough.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(3);
        }
        #endregion

        #region Dialogues
        //Лист
        public static Dictionary<string, Color> ColorSchemes = new Dictionary<string, Color>
        {
            {"yisang", new Color(0.831f, 0.882f, 0.909f, 1.0f)},
            {"faust", new Color(1.0f, 0.694f, 0.705f, 1.0f)},
            {"donquixote", new Color(1.0f, 0.937f, 0.137f, 1.0f)},
            {"ryoshu", new Color(0.811f, 0, 0, 1.0f)},
            {"meursault", new Color(0.352f, 0411f, 0.701f, 1.0f)},
            {"honglu", new Color(0.356f, 1.0f, 0.870f, 1.0f)},
            {"heathcliff", new Color(0.549f, 0.388f, 0.760f, 1.0f)},
            {"ishmael", new Color(1.0f, 0.584f, 0, 1.0f)},
            {"rodya", new Color(0.572f, 0.219f, 0.219f, 1.0f)},
            {"sinclair",new Color(0.545f, 0.611f, 0.082f, 1.0f)},
            {"outis", new Color(0.415f, 0.6f, 0.454f, 1.0f)},
            {"gregor", new Color(0.631f, 0.349f, 0.117f, 1.0f)},

            {"wrath", new Color(1.0f, 0.294f, 0.2f, 1.0f)},
            {"lust", new Color(1.0f, 0.396f, 0.101f, 1.0f)},
            {"sloth", new Color(1.0f, 0.729f, 0.341f, 1.0f)},
            {"gluttony", new Color(0.796f, 0.913f, 0, 1.0f)},
            {"gloom", new Color(0.247f, 0.870f, 1.0f, 1.0f)},
            {"pride", new Color(0, 0.388f, 0.737f, 1.0f)},
            {"envy", new Color(0.760f, 0.266f, 1.0f, 1.0f)},

            {"whiteish", new Color(0.9f, 0.9f, 0.9f, 1.0f)},
            {"charcoal", new Color (0.016f, 0.016f, 0.016f, 0.91f)},
            {"crimson", new Color(0.666f, 0.001f, 0, 0.99f)},
            {"violet", new Color(0.577f, 0.0118f, 0.502f, 0.75f)}
        };


        [HarmonyPatch(typeof(MainLobbyPersonalityCard), nameof(MainLobbyPersonalityCard.ActiveDialog))]
        [HarmonyPostfix]
        private static void Lobby_Init(MainLobbyPersonalityCard __instance)
        {
            __instance.tmpro_dialog.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(15);
            __instance.tmpro_dialog.fontMaterial.SetFloat("_GlowInner", (float)0.4);
            __instance.tmpro_dialog.fontMaterial.SetFloat("_GlowOuter", (float)0.75);
            __instance.tmpro_dialog.fontMaterial.SetFloat("_GlowPower", 0.2f);
            __instance.tmpro_dialog.ForceMeshUpdate();
            TextMeshProUGUI long_click = __instance._personalityAcquisitionAnimOwnerObj.GetComponentInChildren<TextMeshProUGUI>(true);
            Color glow = new Color(__instance.tmpro_dialog.color.r, __instance.tmpro_dialog.color.g, __instance.tmpro_dialog.color.b, 0.4f);
            Color glow_longclick = new Color(long_click.color.r, long_click.color.g, long_click.color.b, 0.4f);
            __instance.tmpro_dialog.m_sharedMaterial.SetColor("_GlowColor", glow);
            long_click.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(15);
            long_click.fontMaterial.SetColor("_GlowColor", glow_longclick);
            long_click.fontMaterial.SetFloat("_GlowInner", (float)0.4);
            long_click.fontMaterial.SetFloat("_GlowOuter", (float)0.75);
            long_click.fontMaterial.SetFloat("_GlowPower", 0.2f);
        }
        [HarmonyPatch(typeof(UserInfoCard), nameof(UserInfoCard.SetDataMainLobby))]
        [HarmonyPostfix]
        private static void Lobby_Card(UserInfoCard __instance)
        {
            __instance._idCopyBtn.GetComponentInChildren<TextMeshProUGUI>(true).m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(3);
            __instance._idCopyBtn.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(12);

            __instance.tmp_introduction.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(4);
            __instance.tmp_introduction.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(17);

            //Add Level and number Labels' changer later.
        }
        [HarmonyPatch(typeof(TierUpEffectUIPanel), nameof(TierUpEffectUIPanel.SetupAndOpen))]
        [HarmonyPostfix]
        private static void TierUp_Init(TierUpEffectUIPanel __instance)
        {
            Color glow = new Color(__instance._subtitle.tmp_punchLine.color.r, __instance._subtitle.tmp_punchLine.color.g, __instance._subtitle.tmp_punchLine.color.b, 0.25f);
            __instance._subtitle.tmp_punchLine.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(15);
            __instance._subtitle.tmp_punchLine.m_sharedMaterial.SetColor("_GlowColor", glow);
            __instance._subtitle.tmp_punchLine.m_sharedMaterial.SetFloat("_GlowPower", 3);
        }
        [HarmonyPatch(typeof(PersonalityStoryPersonalityUI), nameof(PersonalityStoryPersonalityUI.OpenPersonalityVoiceTab))]
        [HarmonyPostfix]
        private static void PersonalityStoryPersonalityUI_Init(PersonalityStoryPersonalityUI __instance)
        {
            Color glow = new Color(__instance._voiceText.color.r, __instance._voiceText.color.g, __instance._voiceText.color.b, 0.4f);
            __instance._voiceText.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(15);
            __instance._voiceText.fontMaterial.EnableKeyword("UNDELAY_ON");
            __instance._voiceText.fontMaterial.SetColor("_GlowColor", glow);
            __instance._voiceText.fontMaterial.SetFloat("_GlowPower", 0.2f);
            __instance._voiceText.GetComponentInChildren<TextMeshProLanguageSetter>().enabled = false;
        }
        [HarmonyPatch(typeof(BattleDialogUI), nameof(BattleDialogUI.Init))]
        [HarmonyPostfix]
        private static void EGO_Init(BattleDialogUI __instance)
        {
            __instance.tmp_dialog.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(16);
        }
        [HarmonyPatch(typeof(BattleResultUIPanel), nameof(BattleResultUIPanel.SetStatusUI))]
        [HarmonyPostfix]
        private static void BattleResult_Init(BattleResultUIPanel __instance)
        {
            Color glow = new Color(__instance.tmp_dialog.color.r, __instance.tmp_dialog.color.g, __instance.tmp_dialog.color.b, 0.5f);
            __instance.tmp_dialog.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(15);
            __instance.tmp_dialog.fontMaterial.SetColor("_GlowColor", glow);
            __instance.tmp_dialog.fontMaterial.SetFloat("_GlowPower", 3);

            Color yellowish = new Color(1.0f, 0.306f, 0, 0.251f);
            Color reddish = new Color(0.686f, 0.003f, 0.003f, 0.251f);
            __instance.tmp_result.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(11);
            __instance.tmp_result.fontMaterial.EnableKeyword("GLOW_ON");

            __instance.tmp_result.fontMaterial.SetFloat("_GlowInner", 0.6f);
            __instance.tmp_result.fontMaterial.SetFloat("_GlowPower", 3);
            __instance.tmp_result.characterSpacing = 3;

            if (__instance.tmp_result.text.Contains("Победа"))
                __instance.tmp_result.fontMaterial.SetColor("_GlowColor", yellowish);
            else
                __instance.tmp_result.fontMaterial.SetColor("_GlowColor", reddish);
        }
        [HarmonyPatch(typeof(GachaGetNewCardTimelineManager), nameof(GachaGetNewCardTimelineManager.Init))]
        [HarmonyPostfix]
        private static void GachaGetNewCardTimelineManager_Init(GachaGetNewCardTimelineManager __instance)
        {
            List<TextMeshProUGUI> subtitles = new List<TextMeshProUGUI> { __instance.rank3Timeline._subtitle.tmp_punchLine, __instance.rank2Timeline._subtitle.tmp_punchLine, __instance.rank1Timeline._subtitle.tmp_punchLine, __instance.egoTimeline._subtitle.tmp_punchLine };
            Color glow_punchline_r3 = new Color(__instance.rank3Timeline._subtitle.tmp_punchLine.color.r, __instance.rank3Timeline._subtitle.tmp_punchLine.color.g, __instance.rank3Timeline._subtitle.tmp_punchLine.color.b, 0.2f);
            Color glow_punchline_r2 = new Color(__instance.rank2Timeline._subtitle.tmp_punchLine.color.r, __instance.rank2Timeline._subtitle.tmp_punchLine.color.g, __instance.rank2Timeline._subtitle.tmp_punchLine.color.b, 0.2f);
            Color glow_punchline_r1 = new Color(__instance.rank1Timeline._subtitle.tmp_punchLine.color.r, __instance.rank1Timeline._subtitle.tmp_punchLine.color.g, __instance.rank1Timeline._subtitle.tmp_punchLine.color.b, 0.2f);
            Color glow_punchline_ego = new Color(__instance.egoTimeline._subtitle.tmp_punchLine.color.r, __instance.egoTimeline._subtitle.tmp_punchLine.color.g, __instance.egoTimeline._subtitle.tmp_punchLine.color.b, 0.2f);
            foreach (var subtitle in subtitles)
            {
                subtitle.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(16);
                subtitle.fontMaterial.SetFloat("_GlowPower", 3);
                subtitle.fontMaterial.SetFloat("_UnderlayDilate", 0.95f);
            }
            __instance.rank3Timeline._subtitle.tmp_punchLine.fontMaterial.SetColor("_GlowColor", glow_punchline_r3);
            __instance.rank2Timeline._subtitle.tmp_punchLine.fontMaterial.SetColor("_GlowColor", glow_punchline_r2);
            __instance.rank1Timeline._subtitle.tmp_punchLine.fontMaterial.SetColor("_GlowColor", glow_punchline_r1);
            __instance.egoTimeline._subtitle.tmp_punchLine.fontMaterial.SetColor("_GlowColor", glow_punchline_ego);
        }
        [HarmonyPatch(typeof(GachaGetNewCardTimelineManager), nameof(GachaGetNewCardTimelineManager.Init))]
        [HarmonyPostfix]
        private static void GachaGetNewCardTimelineManager2_Init(GachaGetNewCardTimelineManager __instance)
        {
            List<TextMeshProUGUI> punchlines = new List<TextMeshProUGUI> { __instance.rank3Timeline._punchline.tmp_punchLine, __instance.rank2Timeline._punchline.tmp_punchLine, __instance.rank1Timeline._punchline.tmp_punchLine, __instance.egoTimeline._punchline.tmp_punchLine };
            Color glow_punchline_r3 = new Color(__instance.rank3Timeline._punchline.tmp_punchLine.color.r, __instance.rank3Timeline._punchline.tmp_punchLine.color.g, __instance.rank3Timeline._punchline.tmp_punchLine.color.b, 0.2f);
            Color glow_punchline_r2 = new Color(__instance.rank2Timeline._punchline.tmp_punchLine.color.r, __instance.rank2Timeline._punchline.tmp_punchLine.color.g, __instance.rank2Timeline._punchline.tmp_punchLine.color.b, 0.2f);
            Color glow_punchline_r1 = new Color(__instance.rank1Timeline._punchline.tmp_punchLine.color.r, __instance.rank1Timeline._punchline.tmp_punchLine.color.g, __instance.rank1Timeline._punchline.tmp_punchLine.color.b, 0.2f);
            Color glow_punchline_ego = new Color(__instance.egoTimeline._punchline.tmp_punchLine.color.r, __instance.egoTimeline._punchline.tmp_punchLine.color.g, __instance.egoTimeline._punchline.tmp_punchLine.color.b, 0.2f);
            foreach (var punchline in punchlines)
            {
                punchline.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(16);
                punchline.fontMaterial.SetFloat("_GlowPower", 3);
                punchline.fontMaterial.SetFloat("_UnderlayDilate", 0.95f);
            }
            __instance.rank3Timeline._punchline.tmp_punchLine.fontMaterial.SetColor("_GlowColor", glow_punchline_r3);
            __instance.rank2Timeline._punchline.tmp_punchLine.fontMaterial.SetColor("_GlowColor", glow_punchline_r2);
            __instance.rank1Timeline._punchline.tmp_punchLine.fontMaterial.SetColor("_GlowColor", glow_punchline_r1);
            __instance.egoTimeline._punchline.tmp_punchLine.fontMaterial.SetColor("_GlowColor", glow_punchline_ego);
        }
        [HarmonyPatch(typeof(SalvageGachaCard), nameof(SalvageGachaCard.SetData))]
        [HarmonyPostfix]
        private static void AnnouncerText_Material(SalvageGachaCard __instance)
        {
            Color textColor = __instance.tmp_WA_Subtitle.color;
            __instance.tmp_WA_Subtitle.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(16);
            __instance.tmp_WA_Subtitle.fontMaterial.SetColor("_GlowColor", textColor);
        }
        #endregion

        #region Battle
        [HarmonyPatch(typeof(BattleTotalDamageTypoSlot), nameof(BattleTotalDamageTypoSlot.Open))]
        [HarmonyPostfix]
        private static void BattleTotalDamageTypoSlot_Init(BattleTotalDamageTypoSlot __instance)
        {
            GameObject ego_t = GameObject.Find("[Script]BattleUIRoot/[Script]BattleSkillViewUIController/[Canvas]Canvas/[Script]SkillViewCanvas/[Rect]NameBox_EGO/[Image]EgoNameBg/[Rect]TotalDamageSlot/[Tmpro]Total");
            List<TextMeshProUGUI> vsego = new List<TextMeshProUGUI> { __instance.tmp_total, ego_t.GetComponentInChildren<TextMeshProUGUI>() };
            foreach (var v in vsego)
            {
                v.text = "<size=40>ВСЕГО</size>";
                v.font = LCB_Cyrillic_Font.tmpcyrillicfonts[1];
                v.fontMaterial = LCB_Cyrillic_Font.GetCyrillicMats(6);
            }
            __instance.tmp_total.fontMaterial.SetColor("_OutlineColor", __instance._atrOutlineColor);
            __instance.tmp_total.fontMaterial.SetColor("_UnderlayColor", __instance._atrUnderlayColor);
            __instance.tmp_total.fontMaterial.SetFloat("_UnderlayDilate", 0.58f);
            __instance.tmp_total.fontMaterial.EnableKeyword("UNDERLAY_ON");
            __instance.tmp_total.fontMaterial.EnableKeyword("OUTLINE_ON");
        }
        #endregion

        #region Experimental properties
        [HarmonyPatch(typeof(TextMeshProMaterialSetter), nameof(TextMeshProMaterialSetter.WriteMaterialProperty))]
        [HarmonyPrefix]
        public static bool WriteMaterialProperty(TextMeshProMaterialSetter __instance)
        {
            if (!__instance._text.font.name.StartsWith("Pretendard-Regular") && !__instance._text.font.name.StartsWith("Mikodacs") || !LCB_Cyrillic_Font.GetCyrillicFonts(__instance._text.font.name, out _) && !LCB_Cyrillic_Font.IsCyrillicFont(__instance._text.font))
                return true;
            Color underlay = __instance._text.fontMaterial.GetColor("_UnderlayColor");

            if (__instance._text.font.name.StartsWith("Pretendard-Regular"))
                __instance._text.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(15);

            Color underlayColor = __instance.underlayColor;

            if (__instance._text.font.name.StartsWith("Pretendard-Regular"))
            {
                if (__instance.underlayOn && __instance._fontMaterialInstance.HasProperty(ShaderUtilities.ID_UnderlayColor))
                {
                    underlayColor = __instance.underlayHdrColorOn ? __instance.underlayHdrColor : underlayColor;
                    if (underlayColor.r > 0f || underlayColor.g > 0f || underlayColor.b > 0f)
                        __instance._text.color = underlayColor;
                }

                if (__instance.Text.text.Contains("</color>"))
                {
                    Color glowColor = new Color(__instance.faceColor.r, __instance.faceColor.g, __instance.faceColor.b, 0.4f);
                    __instance.Text.fontMaterial.SetColor("_UnderlayColor", underlayColor);
                    __instance.Text.fontMaterial.SetColor("_GlowColor", glowColor);
                    __instance.Text.fontMaterial.SetFloat("_GlowPower", 0.2f);
                    __instance.Text.color = __instance.faceColor;
                    return false;
                }
                else if (__instance.underlayHdrColorOn == false)
                {
                    __instance._text.fontMaterial.SetColor("_UnderlayColor", underlayColor);
                    __instance._text.fontMaterial.SetColor("_GlowColor", underlayColor);
                    __instance._text.fontMaterial.SetFloat("_GlowPower", 0.2f);
                    __instance._text.color = ColorSchemes["whiteish"];
                    return false;
                }
                else
                {
                    Color glowColor = new Color(__instance.faceColor.r, __instance.faceColor.g, __instance.faceColor.b, 0.4f);
                    __instance._text.fontMaterial.SetColor("_UnderlayColor", underlayColor);
                    __instance._text.fontMaterial.SetColor("_GlowColor", glowColor);
                    __instance._text.fontMaterial.SetFloat("_GlowPower", 0.2f);
                    __instance._text.color = ColorSchemes["charcoal"];
                    return false;
                }
            }
            return false;
        }

        [HarmonyPatch(typeof(OutterGradiantEffectController), nameof(OutterGradiantEffectController.SetDialog))]
        [HarmonyPostfix]
        public static void BattleDialogue_TMPLanguageSetter_Off(OutterGradiantEffectController __instance)
        {
            TextMeshProUGUI battleDialogue = __instance._dialogText;
            Color textColor = __instance.GetComponentInChildren<TextMeshProMaterialSetter>().faceColor;
            Color underlayColor = __instance._dialogTextMatSetter.underlayColor;
            Color glowColor = new Color(__instance._dialogTextMatSetter.faceColor.r, __instance._dialogTextMatSetter.faceColor.g, __instance._dialogTextMatSetter.faceColor.b, 0.4f);
            if (__instance._dialogText.text.EndsWith("</color>"))
            {
                __instance.GetComponentInChildren<TextMeshProLanguageSetter>(true).enabled = false;
                BattleDialogueChanger(__instance._dialogText, textColor, underlayColor, glowColor);
            }
        }

        public static bool BattleDialogueChanger(TextMeshProUGUI dialogue, Color textColor, Color underlayColor, Color glowColor)
        {
            dialogue.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(15); 
            dialogue.color = textColor;
            dialogue.fontMaterial.SetColor("_UnderlayColor", underlayColor);
            dialogue.fontMaterial.SetColor("_GlowColor", glowColor);
            dialogue.fontMaterial.SetFloat("_GlowPower", 0.2f);
            return false;
        }

        [HarmonyPatch(typeof(BattleLyricsContoller), nameof(BattleLyricsContoller.Init))]
        [HarmonyPostfix]
        private static void BattleLyricsMat1(BattleLyricsContoller __instance)
        {
            Color textColor = new Color(1.0f, 0.636f, 0, 0.5f);
            __instance.tmp.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(3);
            __instance.tmp.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(10);
            __instance.tmp.fontMaterial.EnableKeyword("GLOW_ON");
            __instance.tmp.fontMaterial.SetColor("_GlowColor", textColor);
            __instance.tmp.fontMaterial.SetFloat("_GlowInner", 1f);
            __instance.tmp.fontMaterial.SetFloat("_GlowOffset", 0.08f);
            __instance.tmp.fontMaterial.SetFloat("_GlowOuter", 0.4f);
            __instance.tmp.fontMaterial.SetFloat("_GlowPower", 0.6f);
            __instance.tmp.fontMaterial.SetColor("_UnderlayColor", textColor);
            if (__instance._curText.Contains("</color>"))
            {
                __instance.tmp.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(3);
                __instance.tmp.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(9);
                __instance.tmp.fontMaterial.EnableKeyword("GLOW_ON");
                __instance.tmp.fontMaterial.SetFloat("_GlowInner", 0.15f);
                __instance.tmp.fontMaterial.SetFloat("_GlowOuter", 0.5f);
                __instance.tmp.fontMaterial.EnableKeyword("UNDERLAY_ON");
                __instance.tmp.fontMaterial.SetFloat("_UnderlaySoftness", 0);
                __instance.tmp.fontMaterial.SetFloat("_UnderlayOffsetX", 0);
                __instance.tmp.fontMaterial.SetFloat("_UnderlayOffsetY", 0);
                if (__instance._curText.StartsWith("<color=#242423>"))
                {
                    __instance.tmp.fontMaterial.SetColor("_GlowColor", ColorSchemes["crimson"]);
                    __instance.tmp.fontMaterial.SetColor("_UnderlayColor", ColorSchemes["crimson"]);
                }
                else if (__instance._curText.StartsWith("<color=#930380>") || __instance._curText.StartsWith("<color=#783294>"))
                {
                    __instance.tmp.fontMaterial.SetColor("_GlowColor", ColorSchemes["heathcliff"]);
                    __instance.tmp.fontMaterial.SetColor("_UnderlayColor", ColorSchemes["heathcliff"]);
                }
            }
        }
        [HarmonyPatch(typeof(BattleLyricsContoller), nameof(BattleLyricsContoller.CompleteText))]
        [HarmonyPostfix]
        private static void BattleLyricsMatC(BattleLyricsContoller __instance)
        {
            Color textColor = new Color(1.0f, 0.636f, 0, 0.5f);
            __instance.tmp.fontMaterial.EnableKeyword("GLOW_ON");
            __instance.tmp.fontMaterial.SetColor("_GlowColor", Color.white);
            __instance.tmp.fontMaterial.SetFloat("_GlowPower", 1f);
            __instance.tmp.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(3);
            __instance.tmp.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(10);
            __instance._curFontInfo.fontMaterial.SetColor("_UnderlayColor", textColor);
            __instance._curFontInfo.fontMaterial.SetFloat("_UnderlayDilate", 0.06f);
            __instance._curFontInfo.fontMaterial.SetFloat("_UnderlaySoftness", 0);
            __instance._curFontInfo.fontMaterial.SetFloat("_UnderlayOffsetX", 0);
            __instance._curFontInfo.fontMaterial.SetFloat("_UnderlayOffsetY", 0);

            if (__instance._curText.EndsWith("</color>"))
            {
                __instance.tmp.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(3);
                __instance.tmp.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(9);
                __instance.tmp.fontMaterial.EnableKeyword("GLOW_ON");
                __instance.tmp.fontMaterial.SetFloat("_GlowInner", 0.15f);
                __instance.tmp.fontMaterial.SetFloat("_GlowOuter", 0.5f);
                __instance.tmp.fontMaterial.SetFloat("_GlowPower", 1f);
                __instance.tmp.fontMaterial.EnableKeyword("UNDERLAY_ON");
                if (__instance._curText.StartsWith("<color=#242423>"))
                {
                    __instance.tmp.fontMaterial.SetColor("_GlowColor", ColorSchemes["crimson"]);
                }
                else if (__instance._curText.StartsWith("<color=#930380>"))
                {
                    __instance.tmp.fontMaterial.SetColor("_GlowColor", ColorSchemes["heathcliff"]);
                }
            }
        }
        #endregion
    }
}
