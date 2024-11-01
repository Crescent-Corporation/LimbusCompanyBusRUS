using BattleUI;
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
            t.GetComponentInChildren<TextMeshProUGUI>().m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(8);
            t.GetComponentInChildren<TextMeshProUGUI>().characterSpacing = 3;
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
            railway_line.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.SetFloat("_GlowOuter", 0.02f);
            railway_line.GetComponentInChildren<TextMeshProUGUI>().fontMaterial.SetFloat("_GlowPower", 0.25f);
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



        public static void getBurnEventT(Transform t)
        {
            t.GetComponentInChildren<TextMeshProUGUI>().m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(8);
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
        [HarmonyPatch(typeof(UnitInformationPersonalitySkillTypeButton), nameof(UnitInformationPersonalitySkillTypeButton.SetSkillTextHighlight))]
        [HarmonyPostfix]
        private static void SkillType_Materials(UnitInformationPersonalitySkillTypeButton __instance)
        {
            __instance.tmp_skilType.GetComponentInChildren<TextMeshProLanguageSetter>(true).enabled = false;
            __instance.img_lock.GetComponentInChildren<RectTransform>(true).anchoredPosition = new Vector2(-45, -25);
            if (__instance.IsFoucused == true && __instance.img_lock.enabled == false)
            {
                Color yellowish = new Color(1.0f, 0.306f, 0, 0.502f);
                __instance.tmp_skilType.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(8);
                __instance.tmp_skilType.characterSpacing = 3;
            }
            else if (__instance._isEntered == true && __instance.img_lock.enabled == false)
            {
                Color yellowish = new Color(1.0f, 0.306f, 0, 0.502f);
                __instance.tmp_skilType.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(8);
                __instance.tmp_skilType.characterSpacing = 3;
            }
            else if (__instance.img_lock.enabled == true)
            {
                __instance.tmp_skilType.color = Color.gray;
                __instance.tmp_skilType.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(12);
                __instance.tmp_skilType.characterSpacing = 2;
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
            Color yellowish = new Color(3.9533f, 1.097f, 0, 1);
            //ActiveRect/VendingMachineUIPanel/
            Transform sold_out = __instance.transform.Find("GoodsStoreAreaMaster/GoodsStorePanelGroup/BackPanel/Main/SoldOut/[LocalizeText]WaitRestock");
            if (sold_out != null)
            {
                __instance.tmp_notice_sold_out.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(8);
                __instance.tmp_notice_sold_out.characterSpacing = 3;
            }
        }
        [HarmonyPatch(typeof(GacksungLevelUpCompletionPopup), nameof(GacksungLevelUpCompletionPopup.UpdateAcquiredContentsLayout))]
        [HarmonyPostfix]
        private static void GacksungLevelUpCompletionPopup_Init(GacksungLevelUpCompletionPopup __instance)
        {
            //Update
            Color yellowish = new Color(3.9533f, 1.097f, 0, 1);
            Color blueish = new Color(0.451f, 0.620f, 0.710f, 1);
            Color tier_default = new Color(255, 255, 0, 145);
            Color pm_yellow = new Color(0.969f, 0.765f, 0, 1.0f);
            __instance.tmp_contentTitle.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(3);
            __instance.tmp_contentTitle.transform.GetComponentInChildren<TextMeshProUGUI>().m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(8);
            __instance.tmp_contentTitle.transform.GetComponentInChildren<TextMeshProUGUI>().characterSpacing = 2;
            __instance.tmp_contentTitle.characterSpacing = 2;
            if (__instance.img_upperChain.activeSprite.name.StartsWith("MainUI_PersonalityList_9_7") || __instance.tmp_egoDeco_upper.faceColor == tier_default)
            {
                __instance.tmp_contentTitle.color = Color.white;
            }
            else
            {
                __instance.tmp_contentTitle.faceColor = Color.white;
                __instance.tmp_contentTitle.m_sharedMaterial.SetColor("_GlowColor", blueish);
                __instance.tmp_contentTitle.m_sharedMaterial.SetColor("_UnderlayColor", blueish);
            }
        }
        [HarmonyPatch(typeof(PlayerLevelUpUIPopup), nameof(PlayerLevelUpUIPopup.OpenAndSetup))]
        [HarmonyPostfix]
        private static void NewManagerLevel_Init(PlayerLevelUpUIPopup __instance)
        {
            Color yellowish = new Color(3.9533f, 1.097f, 0, 1);
            Color pm_yellow = new Color(0.969f, 0.765f, 0, 1.0f);
            __instance.tmp_user_level_up_notice.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(8);
            __instance.tmp_user_level_up_notice.characterSpacing = 2;
        }
        [HarmonyPatch(typeof(PersonalityLevelLimitReleaseUIPopup), nameof(PersonalityLevelLimitReleaseUIPopup.Initialize))]
        [HarmonyPostfix]
        private static void NewPersonalityLevels_Init(PersonalityLevelLimitReleaseUIPopup __instance)
        {
            Color yellowish = new Color(3.9533f, 1.097f, 0, 1);
            Color pm_yellow = new Color(0.969f, 0.765f, 0, 1.0f);
            __instance.tmp_mainTitle.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(3);
            __instance.tmp_mainTitle.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(8);
            __instance.tmp_mainTitle.characterSpacing = 3;
        }
        [HarmonyPatch(typeof(Formation_PortraitEventSlotUI), nameof(Formation_PortraitEventSlotUI.OnPointerEnter))]
        [HarmonyPostfix]
        private static void ChangeSinner(Formation_PortraitEventSlotUI __instance)
        {
            //TextMeshProUGUI just_work = __instance.transform.Find("[Image]HoverImage/[Text]Selected").GetComponentInChildren<TextMeshProUGUI>();
            __instance.tmp_text.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(3);
            __instance.tmp_text.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(8);
            Color foryou = new Color(3.9533f, 1.097f, 0, 0.05f);
            Color forme = new Color(0.5411765f, 0.5411765f, 0.5411765f, 1f);
            Color forus = __instance.tmp_text.m_sharedMaterial.GetColor("_FaceColor");
            __instance.tmp_text.lineSpacing = -30;
            __instance.tmp_text.characterSpacing = 3;
            __instance.tmp_text.fontMaterial.EnableKeyword(ShaderUtilities.Keyword_Glow);
            __instance.tmp_text.fontMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 0.45f);
            __instance.tmp_text.fontMaterial.SetFloat(ShaderUtilities.ID_GlowOuter, 0.02f);
            
            if (__instance.tmp_text.text.EndsWith("участников"))
            {
                __instance.tmp_text.fontMaterial.SetColor(ShaderUtilities.ID_FaceColor, forme);
                __instance.tmp_text.fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, ColorSchemes["charcoal"]);
            }
            else
            {
                __instance.tmp_text.fontMaterial.SetColor(ShaderUtilities.ID_FaceColor, forus);
                __instance.tmp_text.fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, foryou);
            }
        }
        [HarmonyPatch(typeof(Formation_SelectParticipatedEventSlotUI), nameof(Formation_SelectParticipatedEventSlotUI.UpdateData))]
        [HarmonyPostfix]
        private static void ChangeSinner_Dungeon(Formation_SelectParticipatedEventSlotUI __instance)
        {
            //TextMeshProUGUI just_work = __instance.transform.Find("[Image]HoverImage/[Text]Selected").GetComponentInChildren<TextMeshProUGUI>();
            __instance.tmp_text.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(3);
            __instance.tmp_text.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(8);
            Color foryou = new Color(3.9533f, 1.097f, 0, 0.05f);
            Color forme = new Color(0.5411765f, 0.5411765f, 0.5411765f, 1);
            Color forus = __instance.tmp_text.m_sharedMaterial.GetColor("_FaceColor");
            __instance._materialSetter._text.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(8);
            __instance._materialSetter._text.lineSpacing = -30;
            __instance._materialSetter._text. characterSpacing = 3;
            __instance._materialSetter._text.fontMaterial.EnableKeyword(ShaderUtilities.Keyword_Glow);
            __instance._materialSetter._text.fontMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 0.45f);
            __instance._materialSetter._text.fontMaterial.SetFloat(ShaderUtilities.ID_GlowOuter, 0.02f);
            if (__instance._materialSetter._text.text.EndsWith("участников"))
            {
                __instance._materialSetter._text.fontMaterial.SetColor(ShaderUtilities.ID_FaceColor, forme);
                __instance._materialSetter._text.fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, ColorSchemes["charcoal"]);
            }
            else
            {
                __instance._materialSetter._text.fontMaterial.SetColor(ShaderUtilities.ID_FaceColor, forus);
                __instance._materialSetter._text.fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, foryou);
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


        [HarmonyPatch(typeof(MainLobbyPersonalityCard), nameof(MainLobbyPersonalityCard.StartDialog))]
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
        }
        [HarmonyPatch(typeof(PersonalityStoryPersonalityUI), nameof(PersonalityStoryPersonalityUI.OpenPersonalityVoiceTab))]
        [HarmonyPostfix]
        private static void PersonalityStoryPersonalityUI_Init(PersonalityStoryPersonalityUI __instance)
        {
            Color glow = new Color(__instance._voiceText.color.r, __instance._voiceText.color.g, __instance._voiceText.color.b, 0.4f);
            __instance._voiceText.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(15);
            __instance._voiceText.fontMaterial.EnableKeyword("UNDELAY_ON");
            __instance._voiceText.fontMaterial.SetColor("_GlowColor", glow);
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

            Color yellowish = new Color(1.0f, 0.306f, 0, 0.251f);
            __instance.tmp_result.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(8);
            __instance.tmp_result.lineSpacing = -30;

            __instance.tmp_result.characterSpacing = 3;

            if (__instance.tmp_result.text.Contains("Победа"))
                __instance.tmp_result.fontMaterial.SetColor("_GlowColor", yellowish);
            else
                __instance.tmp_result.fontMaterial.DisableKeyword("GLOW_ON");
        }
        [HarmonyPatch(typeof(GachaGetNewCardTimelineManager), nameof(GachaGetNewCardTimelineManager.Init))]
        [HarmonyPostfix]
        private static void GachaGetNewCardTimelineManager_Init(GachaGetNewCardTimelineManager __instance)
        {
            List<GetNewCardTimelineScriptBase> subtitles = new List<GetNewCardTimelineScriptBase> { __instance.rank3Timeline, __instance.rank2Timeline, __instance.rank1Timeline, __instance.egoTimeline };
            List<TextMeshProUGUI> subtitles_txt = new List<TextMeshProUGUI>();
            foreach (var subtitle in subtitles)
            {
                if (subtitle._subtitle != null)
                {
                    subtitles_txt.Add(subtitle._subtitle.tmp_punchLine);
                }
                if (subtitle._punchline != null)
                {
                    subtitles_txt.Add(subtitle._punchline.tmp_punchLine);
                }
            }
            foreach (var subtitle in subtitles_txt)
            {
                subtitle.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(15);
                Color test = new Color(subtitle.color.r, subtitle.color.g, subtitle.color.b, 0.2f);
                subtitle.fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, test);
            }
        }
        [HarmonyPatch(typeof(SalvageGachaCard), nameof(SalvageGachaCard.SetData))]
        [HarmonyPostfix]
        private static void AnnouncerText_Material(SalvageGachaCard __instance)
        {
            Color textColor = __instance.tmp_WA_Subtitle.color;
            __instance.tmp_WA_Subtitle.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(15);
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
        [HarmonyPatch(typeof(TargetDetailSkillInfoController), nameof(TargetDetailSkillInfoController.SetUISetting))]
        [HarmonyPostfix]
        private static void Targeting(TargetDetailSkillInfoController __instance)
        {
            __instance._winRateTypo._textMeshPro.lineSpacing = -30;
        }
        #endregion

        #region Experimental properties
        [HarmonyPatch(typeof(BattleSkillViewUIInfo), nameof(BattleSkillViewUIInfo.Update))]
        [HarmonyPrefix]
        private static void BattleSkillViewUIInfo_Init(BattleSkillViewUIInfo __instance)
        {
            Color underlayColor = __instance._materialSetter.underlayColor;
            __instance._materialSetter._text.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(15);

            if (__instance._materialSetter.underlayOn && __instance._materialSetter._fontMaterialInstance.HasProperty(ShaderUtilities.ID_UnderlayColor))
            {
                underlayColor = __instance._materialSetter.underlayHdrColorOn ? __instance._materialSetter.underlayHdrColor : underlayColor;
                if (underlayColor.r > 0f || underlayColor.g > 0f || underlayColor.b > 0f)
                    __instance._materialSetter._text.color = underlayColor;
            }
            __instance._materialSetter._text.fontMaterial.SetColor(ShaderUtilities.ID_UnderlayColor, underlayColor);
            __instance._materialSetter._text.fontMaterial.DisableKeyword(ShaderUtilities.Keyword_Glow);

            if (__instance._materialSetter.underlayHdrColorOn == false)
            {
                __instance._materialSetter._text.color = ColorSchemes["whiteish"];
            }
            else
            {
                __instance._materialSetter._text.color = ColorSchemes["charcoal"];
            }
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

        [HarmonyPatch(typeof(BattleLyricsContoller), nameof(BattleLyricsContoller.SetJitterHeight))]
        [HarmonyPostfix]
        private static void BattleLyricsMat1(BattleLyricsContoller __instance)
        {
            __instance.tmp.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(3);
            __instance.tmp.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(10);
            if (__instance._curText.Contains("</color>"))
            {
                __instance.tmp.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(3);
                __instance.tmp.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(9);
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
            __instance.tmp.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(3);
            __instance.tmp.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(10);

            if (__instance._curText.EndsWith("</color>"))
            {
                __instance.tmp.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(3);
                __instance.tmp.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(9);
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
