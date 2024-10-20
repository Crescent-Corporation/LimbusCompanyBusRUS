﻿using HarmonyLib;
using MainUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using MainUI.Gacha;
using BattleUI.Typo;
using UtilityUI;
using Il2CppSystem;
using CustomScriptableObject;

namespace LimbusLocalizeRUS
{
    public static class LCBR_EventUI
    {
        // EVENT DATES
        public static void Event_Dates()
        {
            
        }

        #region Base Things
        [HarmonyPatch(typeof(BattleResultUIRewardSlot), nameof(BattleResultUIRewardSlot.SetRewardState))]
        [HarmonyPostfix]
        private static void ExchangeEffectSprite(BattleResultUIRewardSlot __instance)
        {
            __instance._effectTag.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["UserInfo_Effect"];
        }
        #endregion

        #region New Manager Banner
        [HarmonyPatch(typeof(BannerSlot<GachaBannerSlot>), nameof(BannerSlot<GachaBannerSlot>.SetData))]
        [HarmonyPostfix]
        private static void GachaBannerSlot_SetData(BannerSlot<GachaBannerSlot> __instance)
        {
            if (__instance._name == "gacha_3_illust")
            {
                __instance._base._bannerImage.sprite = LCBR_ReadmeManager.ReadmeEventSprites["NewManagerGacha_Banner"];
            }
        }
        [HarmonyPatch(typeof(GachaUIPanel), nameof(GachaUIPanel.SetGachaInfoPanel))]
        [HarmonyPostfix]
        private static void GachaUIPanel_SetData(GachaUIPanel __instance)
        {
            Sprite safe = __instance.img_displayCharacterCG.sprite;
            if (__instance._lastSettingId == 3)
            {
                __instance.img_displayCharacterCG.overrideSprite = LCBR_ReadmeManager.ReadmeSprites["NewManagerGacha"];
                __instance._currentGachaTitleImage.sprite = LCBR_ReadmeManager.ReadmeSprites["NewManagerGacha_Typo"];
            }
            else
            {
                __instance.img_displayCharacterCG.overrideSprite = safe;
            }
        }
        [HarmonyPatch(typeof(ChanceCounter), nameof(ChanceCounter.SetData))]
        [HarmonyPostfix]
        private static void ChanceCounter_SetData(ChanceCounter __instance)
        {
            __instance.tmp_number_of_times.text = getRaza(__instance.tmp_number.text);
        }
        public static string getRaza(string numStr)
        {
            if (!int.TryParse(numStr, out int num))
            {
                return "HAHAHA";
            }

            int lastDigit = num % 10;
            int secondLastDigit = (num / 10) % 10;

            if (lastDigit == 1 && secondLastDigit != 1)
            {
                return "Раз";
            }
            else if (lastDigit >= 2 && lastDigit <= 4 && secondLastDigit != 1)
            {
                return "Раза";
            }
            else
            {
                return "Раз";
            }
        }
        #endregion

        #region 7th Anniversary
        [HarmonyPatch(typeof(SeventhAnniversaryEventPopup), nameof(SeventhAnniversaryEventPopup.InitEventStataicData))]
        [HarmonyPostfix]
        private static void ProjectMoon7AnnivCG_Init(SeventhAnniversaryEventPopup __instance)
        {
            Transform anniversaryBG = __instance.transform.Find("GameObject/[Image]Bg");
            if (anniversaryBG != null)
            {
                anniversaryBG.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["7thAnniversary"];
            }
        }
        [HarmonyPatch(typeof(SeventhAnniversaryEventPopup), nameof(SeventhAnniversaryEventPopup.InitLocalizeText))]
        [HarmonyPostfix]
        private static void ProjectMoon7AnnivText_Init(SeventhAnniversaryEventPopup __instance)
        {
            Transform anniversaryDate = __instance.transform.Find("GameObject/[Text]EventDate");
            if (anniversaryDate != null)
            {
                anniversaryDate.GetComponentInChildren<TextMeshProUGUI>(true).m_text = "18:00 17.11.2023(ПТ) - 17:59 24.11.2023(ПТ) (МСК)";
                anniversaryDate.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
                anniversaryDate.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[2].material;
            }
        }
        #endregion

        #region Miracle in District 20
        [HarmonyPatch(typeof(MiracleEventUIPanel), nameof(MiracleEventUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void MiracleEventUI_Init(MiracleEventUIPanel __instance)
        {
            Transform miracleLoading = __instance.transform.Find("[Rect]IntroObjs/[Image]EventTitle");
            Transform miracleLogo = __instance.transform.Find("[Rect]UIObjs/[Image]TitleLogo");
            Transform miracleDate = __instance.transform.Find("[Rect]UIObjs/[Image]TitleLogo/[Rect]EventPeriod/tmp_period");
            Transform miracleStory = __instance.transform.Find("[Rect]UIObjs/[Button]StoryEventUI");
            Transform miracleStoryMO = __instance.transform.Find("[Rect]UIObjs/[Button]StoryEventUI/[Image]StageButtonMouseover");
            Transform miracleStage = __instance.transform.Find("[Rect]UIObjs/[Button]StageEventUI");
            Transform miracleStageMO = __instance.transform.Find("[Rect]UIObjs/[Button]StageEventUI/[Image]StageButtonMouseover");
            if (miracleLoading != null)
                miracleLoading.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["Miracle20_Logo"];
            if (miracleLogo != null)
                miracleLogo.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["Miracle20_Logo"];
            if (miracleDate != null)
            {
                miracleDate.GetComponentInChildren<TextMeshProUGUI>(true).text = "06:00 28.12.2023(ЧТ) - 04:00 25.01.2024(ЧТ) (МСК)";
                miracleDate.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
                miracleDate.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[2].material;
            }
            if (miracleStory != null)
                miracleStory.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["Miracle20_Story"];
            if (miracleStoryMO != null)
                miracleStoryMO.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["Miracle20_Story_Mouseover"];
            if (miracleStage != null)
                miracleStage.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["Miracle20_Stage"];
            if (miracleStageMO != null)
                miracleStageMO.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["Miracle20_Stage_Mouseover"];
        }
        [HarmonyPatch(typeof(MiracleEventMultipleBanner), nameof(MiracleEventMultipleBanner.Init))]
        [HarmonyPostfix]
        private static void MiracleBanners_Init(MiracleEventMultipleBanner __instance)
        {
            Transform mainBanner = __instance.transform.Find("[Button]MainBanner");
            if (mainBanner != null)
                mainBanner.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["Miracle20_EventBanner"];
            Transform subBanner = __instance.transform.Find("[Button]RewardBanner");
            if (subBanner != null)
                subBanner.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["Miracle20_ExchangeBanner"];
        }
        [HarmonyPatch(typeof(MiracleEventRewardUIPanel), nameof(MiracleEventRewardUIPanel.InitEventStataicData))]
        [HarmonyPostfix]
        private static void MiracleRewardUI_Init(MiracleEventRewardUIPanel __instance)
        {
            Transform miracleBG = __instance.transform.Find("img_background");
            Transform miracleLogo = __instance.transform.Find("EventDescriptionPanel/EventLocalizeLogo");
            if (miracleBG != null)
                miracleBG.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["Miracle20_ExchangeBG"];
            if (miracleLogo != null)
                miracleLogo.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["Miracle20_Logo"];
        }
        
        [HarmonyPatch(typeof(MiracleEventRewardUIPanel), nameof(MiracleEventRewardUIPanel.InitDateText))]
        [HarmonyPostfix]
        private static void MiracleRewardDate_Init (MiracleEventRewardUIPanel __instance)
        {
            Transform miracleDate = __instance.transform.Find("EventDescriptionPanel/tmp_eventPeriod");

            if (miracleDate != null)
            {
                miracleDate.GetComponentInChildren<TextMeshProUGUI>(true).text = "06:00 28.12.2023(ЧТ) - 04:00 01.02.2024(ЧТ) (МСК)";
                miracleDate.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
                miracleDate.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[2].material;
            }

        }
        [HarmonyPatch(typeof(MiracleEventRewardButton), nameof(MiracleEventRewardButton.SetData))]
        [HarmonyPostfix]
        private static void MiracleLock_Init(MiracleEventRewardButton __instance)
        {
            __instance.img_lock.sprite = LCBR_ReadmeManager.ReadmeEventSprites["Miracle20_Lock"];
            __instance.img_complete.sprite = LCBR_ReadmeManager.ReadmeEventSprites["Miracle20_Get"];
        }
        #endregion

        #region 2nd Walpurgisnacht
        [HarmonyPatch(typeof(DawnOfGreenEventRewardBanner), nameof(DawnOfGreenEventRewardBanner.Init))]
        [HarmonyPostfix]
        private static void RewardBanner_Init (DawnOfGreenEventRewardBanner __instance)
        {
            GameObject secondWalpurgisMission = GameObject.Find("[Canvas]RatioMainUI/[Rect]PresenterRoot/[UIPresenter]StageUIPresenter(Clone)/[Rect]Active/[Script]PartAndChapterSelectionUIPanel/[Rect]Active/[Rect]Left/[Script]StageLeftBanners/[Script]StageEventBanner_Multiple_DawnOfGreen(Clone)/[Button]SecondBanner");
            if (secondWalpurgisMission != null)
                secondWalpurgisMission.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["WN2_Mission_Banner"];
        }
        [HarmonyPatch(typeof(DawnOfGreenEventUIPanel), nameof(DawnOfGreenEventUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void DawnOfGreen_Init(DawnOfGreenEventUIPanel __instance)
        {
            GameObject secondWalpurgisDate = GameObject.Find("[Canvas]RatioMainUI/[Rect]PanelRoot/[UIPanel]DawnOfGreen_MainEvent(Clone)/[Text]Date");
            if (secondWalpurgisDate != null)
            {
                secondWalpurgisDate.GetComponentInChildren<TextMeshProUGUI>(true).text = "06:00 11.01.2024(ЧТ) - 04:00 25.01.2024(ЧТ) (МСК)";
                secondWalpurgisDate.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[0];
                secondWalpurgisDate.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[0].material;
            }
        }
        [HarmonyPatch(typeof(DawnOfGreenEventRewardUIPanel), nameof(DawnOfGreenEventRewardUIPanel.SetData))]
        [HarmonyPostfix]
        private static void RewardBackground_Init(DawnOfGreenEventRewardUIPanel __instance)
        {
            GameObject secondWalpurgisBG = GameObject.Find("[Canvas]RatioMainUI/[Rect]PopupRoot/[UIPopup]DawnOfGreen_Reward(Clone)/[Image]Background");
            if (secondWalpurgisBG != null)
                secondWalpurgisBG.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["WN2_BG"];
            GameObject secondWalpurgisDesc = GameObject.Find("[Canvas]RatioMainUI/[Rect]PopupRoot/[UIPopup]DawnOfGreen_Reward(Clone)/EventDescriptionPanel");
            if (secondWalpurgisDesc != null)
                secondWalpurgisDesc.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["WN2_Desc"];
            GameObject secondWalpurgisMissionDate = GameObject.Find("[Canvas]RatioMainUI/[Rect]PopupRoot/[UIPopup]DawnOfGreen_Reward(Clone)/EventDescriptionPanel/[Text]EventPeriod");
            if (secondWalpurgisMissionDate != null)
            {
                secondWalpurgisMissionDate.GetComponentInChildren<TextMeshProUGUI>(true).text = "06:00 11.01.2024(ЧТ) - 04:00 1.02.2024(ЧТ) (МСК)";
                secondWalpurgisMissionDate.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
                secondWalpurgisMissionDate.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[2].material;
            }
        }
        [HarmonyPatch(typeof(DawnOfGreenEventRewardButton), nameof(DawnOfGreenEventRewardButton.SetData))]
        [HarmonyPostfix]
        private static void RewardClear_Init(DawnOfGreenEventRewardButton __instance)
        {
            __instance._completeImage.sprite = LCBR_ReadmeManager.ReadmeEventSprites["WN2_Clear"];
        }
        #endregion

        #region 1st Anniversary of LCB
        [HarmonyPatch(typeof(Limbus1stAnniveAttendanceUIPopup), nameof(Limbus1stAnniveAttendanceUIPopup.SetData))]
        [HarmonyPostfix]
        private static void Limbus1stAnniversaryPeriod(Limbus1stAnniveAttendanceUIPopup __instance)
        {
            __instance.tmp_eventDate.text = "06:00 22.02.2024(ЧТ) - 04:00 21.03.2024(ЧТ) (МСК)";
            __instance.tmp_eventDate.font = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
            __instance.tmp_eventDate.fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[2].material;
        }
        [HarmonyPatch(typeof(Limbus1stAnnivRewardSign), nameof(Limbus1stAnnivRewardSign.SetData))]
        [HarmonyPostfix]
        private static void Limbus1stAnniversaryTexts(Limbus1stAnnivRewardSign __instance)
        {
            __instance.tmp_rewardName.text = __instance.tmp_rewardName.text.Replace("Анжела Комментатор&\n1st Birthday", "<size=52><cspace=-2px>Получите комментатора\nАнжелу и билет 1-ой Годовщины!</cspace></size>");
            Transform day = __instance.transform.Find("[Image]DailyPanel/[Text]DayText");
            if (day != null)
            {
                string daytext = day.GetComponentInChildren<TextMeshProUGUI>(true).text;
                if (daytext.Contains("Daily"))
                    daytext = "Ежедневно";
                else if (daytext.EndsWith("Days"))
                    daytext = daytext.Replace(" Days", "-й день");
            }
        }
        [HarmonyPatch(typeof(Limbus1stAnnivRewardButton), nameof(Limbus1stAnnivRewardButton.SetData))]
        [HarmonyPostfix]
        private static void Limbus1stAnniversaryComplete(Limbus1stAnnivRewardButton __instance)
        {
            Transform complete = __instance.transform.Find("[Image]Complete");
            if (complete != null)
            {
                complete.GetComponentInChildren<Image>(true).overrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["1stLCBAnniversary_Complete"];
            }
        }
        [HarmonyPatch(typeof(Limbus1stAnnivEventUIPopup), nameof(Limbus1stAnnivEventUIPopup.InitDateText))]
        [HarmonyPostfix]
        private static void Limbus1stAnniversaryPopUp(Limbus1stAnnivEventUIPopup __instance)
        {
            Color yellowish = new Color(1.0f, 0.506f, 0, 0.502f);
            Color yellow = new Color(0.97f, 0.76f, 0, 1.0f);
            __instance.tmp_eventTitle.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(11);
            __instance.tmp_eventTitle.color = yellow;
            __instance.tmp_eventTitle.fontMaterial.SetColor("_GlowColor", yellowish);
            __instance.tmp_eventTitle.fontMaterial.SetFloat("_GlowInner", 0.2f);
            __instance.tmp_eventTitle.fontMaterial.SetFloat("_GlowOuter", 0.4f);
            __instance.tmp_eventTitle.fontMaterial.SetFloat("_GlowPower", 3);
            __instance.tmp_eventDate.text = "18:00 26.02.2024(ЧТ) - 04:00 21.03.2024(ЧТ) (МСК)";
            __instance.tmp_eventDate.font = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
            __instance.tmp_eventDate.fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[2].material;

        }
        #endregion

        #region Yield My Flesh
        [HarmonyPatch(typeof(YCGDMainEventBanner), nameof(YCGDMainEventBanner.Init))]
        [HarmonyPostfix]
        private static void YCGD_MainBanner(YCGDMainEventBanner __instance)
        {
            __instance._bannerImage.sprite = LCBR_ReadmeManager.ReadmeEventSprites["YCGD_EventBanner"];
        }
        [HarmonyPatch(typeof(YCGDSubEventBanner), nameof(YCGDSubEventBanner.Init))]
        [HarmonyPostfix]
        private static void YCGD_SubBanner(YCGDSubEventBanner __instance)
        {
            __instance._bannerImage.sprite = LCBR_ReadmeManager.ReadmeEventSprites["YCGD_ExchangeBanner"];
        }
        #endregion

        #region 3rd Walpurgisnacht
        [HarmonyPatch(typeof(Walpu3SubEventBanner), nameof(Walpu3SubEventBanner.Init))]
        [HarmonyPostfix]
        private static void MissionButton(Walpu3SubEventBanner __instance)
        {
            __instance._bannerImage.overrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["WN3_Mission_Banner"];
            //    GameObject thirdWalpurgisMission = GameObject.Find("[Canvas]RatioMainUI/[Rect]PresenterRoot/[UIPresenter]StageUIPresenter(Clone)/[Rect]Active/[Script]PartAndChapterSelectionUIPanel/[Rect]Active/[Rect]Left/[Script]StageLeftBanners/[Script]StageEventBanner_Sub_Walpu3(Clone)/[Mask]BannerImage/[Image]BannerImage (1)");
            //    if (thirdWalpurgisMission != null)
            //        thirdWalpurgisMission.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["WN3_Mission_Banner"];
        }
        [HarmonyPatch(typeof(Walpu3EventUIPanel), nameof(Walpu3EventUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void ThirdWalpurgisDate(Walpu3EventUIPanel __instance)
        {
            GameObject thirdWalpurgisDate = GameObject.Find("[Canvas]RatioMainUI/[Rect]PanelRoot/[UIPanel]Walpu3_MainEvent(Clone)/[Image]DateBox/[Text]Date");
            if (thirdWalpurgisDate != null)
            {
                thirdWalpurgisDate.GetComponentInChildren<TextMeshProUGUI>(true).text = "06:00 02.05.2024 (ЧТ) - 04:00 16.05.2024 (ЧТ) (МСК)";
                thirdWalpurgisDate.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[0];
                thirdWalpurgisDate.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[0].material;
                __instance._logoImage.GetComponentInChildren<Image>(true).overrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["WN3_Logo"];
            }
        }
        [HarmonyPatch(typeof(Walpu3EventRewardPopup), nameof(Walpu3EventRewardPopup.Initialize))]
        [HarmonyPostfix]
        private static void ThirdWalpurgisReward(Walpu3EventUIPanel __instance)
        { 
            GameObject thirdWalpurgisDate = GameObject.Find("[Canvas]RatioMainUI/[Rect]PopupRoot/[UIPopup]Walpu3_RewardEvent(Clone)/EventDescriptionPanel/[Text]EventPeriod");
            if (thirdWalpurgisDate != null)
            {
                thirdWalpurgisDate.GetComponentInChildren<TextMeshProUGUI>(true).text = "06:00 02.05.2024 (ЧТ) - 04:00 24.05.2024 (ЧТ) (МСК)";
                thirdWalpurgisDate.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[0];
                thirdWalpurgisDate.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[0].material;
            }
            GameObject logoImage = GameObject.Find("[Canvas]RatioMainUI/[Rect]PopupRoot/[UIPopup]Walpu3_RewardEvent(Clone)/EventDescriptionPanel/[Image]LocalizeLogo");
            if (logoImage != null)
                logoImage.GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["WN3_Logo"];
            GameObject namePopup = GameObject.Find("[Canvas]RatioMainUI/[Rect]PopupRoot/[UIPopup]Walpu3_RewardEvent(Clone)/[Image]PopupNameTag/[Text]PopupName");
            if (namePopup != null)
            {
                namePopup.GetComponentInChildren<UITextDataLoader>(true).enabled = false;
                namePopup.GetComponentInChildren<TextMeshProUGUI>(true).text = "Задания";
            }
        }
        [HarmonyPatch(typeof(Walpu3EventRewardButton), nameof(Walpu3EventRewardButton.SetData))]
        [HarmonyPostfix]
        private static void ThirdWalpuClear_Init(Walpu3EventRewardButton __instance)
        {
            __instance.cg_check.transform.Find("[Image]Complete").GetComponentInChildren<Image>(true).sprite = LCBR_ReadmeManager.ReadmeEventSprites["WN3_Clear"];
        }
        [HarmonyPatch(typeof(ActTypoLORBattleResultUI), nameof(ActTypoLORBattleResultUI.Open))]
        [HarmonyPostfix]
        private static void LoR_Finisher(ActTypoLORBattleResultUI __instance)
        {
            __instance._stageResultText.font = LCB_Cyrillic_Font.GetCyrillicFonts(4);
            __instance._stageResultText.fontMaterial = LCB_Cyrillic_Font.GetCyrillicFonts(4).material;
            __instance._stageResultAlphaText.font = LCB_Cyrillic_Font.GetCyrillicFonts(4);
            __instance._stageResultAlphaText.fontMaterial = LCB_Cyrillic_Font.GetCyrillicFonts(4).material;
            if (__instance._isWin)
                __instance._resultTypoImage.overrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["WP3_Victory"];
            else
                __instance._resultTypoImage.overrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["WP3_Defeat"];
        }
        #endregion

        #region Timekilling Time
        [HarmonyPatch(typeof(TKTMainEventBanner), nameof(TKTMainEventBanner.Init))]
        [HarmonyPostfix]
        private static void TKT_MainBanner(TKTMainEventBanner __instance)
        {
            __instance._bannerImage.overrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["TimeKillingTime_EventBanner"];
        }
        [HarmonyPatch(typeof(TKTSubEventBanner), nameof(TKTSubEventBanner.Init))]
        [HarmonyPostfix]
        private static void TKT_SubBanner(TKTSubEventBanner __instance)
        {
            __instance._bannerImage.overrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["TimeKillingTime_ExchangeBanner"];
        }

        // It seems that you have to replace this part each time an event starts.
        [HarmonyPatch(typeof(TKTEventUIPanel), nameof(TKTEventUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void TKT_MainEvent(TKTEventUIPanel __instance)
        {
            var base_intro = __instance._produceObj.transform.Find("Canvas/Title_EN");
            Image base1 = base_intro.Find("[Image] Title_EN").GetComponent<Image>();
            Image base2 = base_intro.Find("[Image] Title02_EN").GetComponent<Image>();
            Image base3 = base_intro.Find("[Image] Title03_EN").GetComponent<Image>();
            base1.m_OverrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["TKT_Intro1"]; //MainUI_TimeKillingTime_10_2
            base2.m_OverrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["TKT_Intro2"]; //MainUI_TimeKillingTime_10_7
            base3.m_OverrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["TKT_Intro3"]; //MainUI_TimeKillingTime_10_8
            base2.GetComponentInChildren<RectTransform>().anchoredPosition = new Vector2(-15, 42);
            base3.GetComponentInChildren<RectTransform>().anchoredPosition = new Vector2(-8, -50);
            base2.GetComponentInChildren<RectTransform>().sizeDelta = new Vector2(66, 66);
            base3.GetComponentInChildren<RectTransform>().sizeDelta = new Vector2(64, 64);

            Transform logo = __instance.transform.Find("[Rect]UIObjs/[Image]TitleLogo");
            if (logo != null)
                logo.GetComponentInChildren<Image>(true).overrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["TimeKillingTime_Logo"];
            Transform date = __instance.transform.Find("[Rect]UIObjs/[Image]TitleLogo/[Rect]EventPeriod/tmp_period");
            if (date != null)
            {
                date.GetComponentInChildren<TextMeshProUGUI>(true).text = "06:00 13.06.2024(ЧТ) - 04:00 11.07.2024(ЧТ) (МСК)";
                date.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
                date.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[2].material;
            }
            __instance._storytheaterBtn_tkt.tmp_buttonText.lineSpacing = -30;
        }
        [HarmonyPatch(typeof(TKTRewardUIPopup), nameof(TKTRewardUIPopup.Initialize))]
        [HarmonyPostfix]
        private static void TKT_RewardUI(TKTRewardUIPopup __instance)
        {
            __instance.img_logo.overrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["TimeKillingTime_Logo"];
            __instance.tmp_eventDate.text = "06:00 13.06.2024(ЧТ) - 04:00 18.07.2024(ЧТ) (МСК)";
            __instance.tmp_eventDate.font = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
            __instance.tmp_eventDate.fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[2].material;
        }
        #endregion

        #region Refraction Railway Line 4
        [HarmonyPatch(typeof(StageInfoWaveButtonListUI), nameof(StageInfoWaveButtonListUI.SetData))]
        [HarmonyPostfix]
        private static void StageInfo_WaveButton(StageInfoWaveButtonListUI __instance)
        {
            foreach (var wave in __instance._waveButtons)
            {
                wave.tmp_buttonText.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
                wave.tmp_buttonText.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(2);
                wave.tmp_buttonText.fontSize = 30;
                var myN = Int32.Parse(wave.tmp_buttonText.text.Substring(5));
                wave.tmp_buttonText.text = $"{myN}{GetOrdinal(myN)} ВОЛНА";
            }
        }
        public static string GetOrdinal(int number)
        {
            int lD = number % 10;
            int secondLD = (number / 10) % 10;
            string ending;
            if (secondLD == 1)
            {
                ending = "-АЯ";
            }
            else
            {
                switch (lD)
                {
                    case 0:
                        ending = "-АЯ";
                        break;
                    case 3:
                        ending = "-ЬЯ";
                        break;
                    default:
                        ending = "-АЯ";
                        break;
                }
            }
            return ending;
        }
        #endregion

        #region Murder on the Warp Express
        [HarmonyPatch(typeof(MOWEMainEventBanner), nameof(MOWEMainEventBanner.Init))]
        [HarmonyPostfix]
        private static void MOWE_MainBanner(MOWEMainEventBanner __instance)
        {
            __instance._bannerImage.overrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["MOWE_EventBanner"];
        }
        [HarmonyPatch(typeof(MOWESubEventBanner), nameof(MOWESubEventBanner.Init))]
        [HarmonyPostfix]
        private static void MOWE_SubBanner(MOWESubEventBanner __instance)
        {
            __instance._bannerImage.overrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["MOWE_ExchangeBanner"];
        }

        [HarmonyPatch(typeof(MOWEEventUIPanel), nameof(MOWEEventUIPanel.Open))]
        [HarmonyPostfix]
        private static void MOWE_MainEvent(MOWEEventUIPanel __instance)
        {
            var intro = __instance.transform.Find("MOWE_introgroup");
            Image text = intro.Find("[Image]Typo").GetComponent<Image>();
            text.m_OverrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["MOWE_Intro"];

            foreach (var logo in __instance._logoImages)
            {
                if (logo.sprite.name == "MainUI_MOWE_07_26" || logo.sprite.name == "MainUI_MOWE_07_25" || logo.sprite.name == "MainUI_MOWE_07_24")
                {
                    logo.sprite = LCBR_ReadmeManager.ReadmeEventSprites["MOWE_Logo_Blood"];
                }
                else
                {
                    logo.sprite = LCBR_ReadmeManager.ReadmeEventSprites["MOWE_Logo"];
                }
            }
            Transform date = __instance.transform.Find("[Rect]UIObjs/[Rect]Title/[Image]TitleLogo/tmp_period");
            if (date != null)
            {
                date.GetComponentInChildren<TextMeshProUGUI>(true).text = "06:00 08.08.2024(ЧТ) - 04:00 05.09.2024(ЧТ) (МСК)";
                date.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
                date.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[2].material;
            }
            __instance.btn_theater.tmp_buttonText.lineSpacing = -30;
        }

        [HarmonyPatch(typeof(MOWERewardUIPopup), nameof(MOWERewardUIPopup.SetData))]
        [HarmonyPostfix]
        private static void MOWE_RewardUI(MOWERewardUIPopup __instance)
        {
            __instance.img_logo.overrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["MOWE_Logo"];
            __instance.tmp_eventDate.text = "06:00 08.08.2024(ЧТ) - 04:00 12.09.2024(ЧТ) (МСК)";
            __instance.tmp_eventDate.font = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
            __instance.tmp_eventDate.fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[2].material;
        }
        [HarmonyPatch(typeof(MOWERewardButton),nameof(MOWERewardButton.SetData))]
        [HarmonyPostfix]
        private static void MOWE_ExchangeButton(MOWERewardButton __instance)
        {
            GameObject MOWE = GameObject.Find("[Canvas]RatioMainUI/[Rect]PopupRoot/[UIPopup]MOWE_Reward(Clone)/EventDescriptionPanel/[Image]ItemCounterPanel/tmp_label_itemCounter");
            MOWE.GetComponentInChildren<TextMeshProLanguageSetter>(true).enabled = false;
            MOWE.GetComponentInChildren<UITextDataLoader>(true).enabled = false;
            MOWE.GetComponentInChildren<TextMeshProUGUI>(true).m_text = "Наборов на руках:";
            MOWE.GetComponentInChildren<TextMeshProUGUI>(true).text = "Наборов на руках:";

            __instance.transform.Find("Image").GetComponentInChildren<Image>(true).overrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["MOWE_Exchange"];
        }
        #endregion
    }
}
