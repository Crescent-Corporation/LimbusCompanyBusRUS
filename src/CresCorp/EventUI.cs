using HarmonyLib;
using MainUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using MainUI.Gacha;
using BattleUI.Typo;
using UtilityUI;
using Il2CppSystem;
using CustomScriptableObject;
using Microsoft.Extensions.Logging;
using LimbusCompany.Events.LCBCheckup;

namespace LimbusLocalizeRUS
{
    public static class EventUI
    {
        #region Base Things
        [HarmonyPatch(typeof(BattleResultUIRewardSlot), nameof(BattleResultUIRewardSlot.SetRewardState))]
        [HarmonyPostfix]
        private static void ExchangeEffectSprite(BattleResultUIRewardSlot __instance)
        {
            __instance._effectTag.overrideSprite = ReadmeManager.GetReadmeSprites("UserInfo_Effect");
        }
        public static int bannerCount;
        [HarmonyPatch(typeof(StageEventBannerManager), nameof(StageEventBannerManager.Init))]
        [HarmonyPostfix]
        private static void StageEventBannerManager_Label(StageEventBannerManager __instance, ref MainEventDataManager stageEventManager)
        {
            bannerCount = __instance._currentActiveEventCount;
        }
        [HarmonyPatch(typeof(EventStateUI), nameof(EventStateUI.SetBannerText))]
        [HarmonyPostfix]
        private static void EventOpen_42Label(EventStateUI __instance, ref EventStateUI.BANNER_STATE bannerTextType)
        {
            if (bannerCount == 1)
                __instance.tmp_eventState.text = "Ивент открыт!";
        }
        #endregion

        #region New Manager Banner
        [HarmonyPatch(typeof(BannerSlot<GachaBannerSlot>), nameof(BannerSlot<GachaBannerSlot>.SetData))]
        [HarmonyPostfix]
        private static void GachaBannerSlot_SetData(BannerSlot<GachaBannerSlot> __instance)
        {
            if (__instance._name == "gacha_3_illust")
            {
                __instance._base._bannerImage.sprite = ReadmeManager.GetReadmeEventSprites("NewManagerGacha_Banner");
            }
        }
        [HarmonyPatch(typeof(GachaUIPanel), nameof(GachaUIPanel.SetGachaInfoPanel))]
        [HarmonyPostfix]
        private static void GachaUIPanel_SetData(GachaUIPanel __instance)
        {
            Sprite safe = __instance.img_displayCharacterCG.sprite;
            if (__instance._lastSettingId == 3)
            {
                __instance.img_displayCharacterCG.overrideSprite = ReadmeManager.GetReadmeSprites("NewManagerGacha");
                __instance._currentGachaTitleImage.sprite = ReadmeManager.GetReadmeSprites("NewManagerGacha_Typo");
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

        #region Refraction Railway Line 4
        [HarmonyPatch(typeof(StageInfoWaveButtonListUI), nameof(StageInfoWaveButtonListUI.SetData))]
        [HarmonyPostfix]
        private static void StageInfo_WaveButton(StageInfoWaveButtonListUI __instance)
        {
            foreach (var wave in __instance._waveButtons)
            {
                wave.tmp_buttonText.fontSize = 30;
                var myN = Int32.Parse(wave.tmp_buttonText.text.Substring(5));
                wave.tmp_buttonText.text = $"{myN}-Я ВОЛНА";
            }
        }
        #endregion

        #region Miracle in District 20
        [HarmonyPatch(typeof(MiracleEventUIPanel), nameof(MiracleEventUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void MiracleEventUI_Init(MiracleEventUIPanel __instance)
        {
            if (__instance._subchapterId != 9105)
                return;
            __instance.btn_theater.img_overlayHOver.sprite = ReadmeManager.GetReadmeEventSprites("Miracle20_Story_Mouseover");
            __instance.btn_stage.img_overlayHOver.sprite = ReadmeManager.GetReadmeEventSprites("Miracle20_Stage_Mouseover");
            foreach (var logo in __instance._logoimages)
            {
                logo.sprite = ReadmeManager.GetReadmeEventSprites("Miracle20_Logo");
            }
            __instance.tmp_eventDate.text = "06:00 26.12.2024(ЧТ) - 04:00 30.01.2025(ЧТ) (МСК)";
            var miracleStory = __instance.transform.Find("[Rect]UIObjs/[Button]StoryEventUI").GetComponentInChildren<Image>();
            var miracleStage = __instance.transform.Find("[Rect]UIObjs/[Button]StageEventUI").GetComponentInChildren<Image>();
            if (miracleStory != null)
                miracleStory.sprite = ReadmeManager.GetReadmeEventSprites("Miracle20_Story");
            if (miracleStage != null)
                miracleStage.sprite = ReadmeManager.GetReadmeEventSprites("Miracle20_Stage");
        }
        [HarmonyPatch(typeof(MiracleEventRewardUIPanel), nameof(MiracleEventRewardUIPanel.InitEventStataicData))]
        [HarmonyPostfix]
        private static void MiracleRewardUI_Init(MiracleEventRewardUIPanel __instance)
        {
            Transform miracleBG = __instance.transform.Find("img_background");
            Transform miracleLogo = __instance.transform.Find("EventDescriptionPanel/EventLocalizeLogo");
            if (miracleBG != null)
                miracleBG.GetComponentInChildren<Image>(true).sprite = ReadmeManager.GetReadmeEventSprites("Miracle20_ExchangeBG");
            if (miracleLogo != null)
                miracleLogo.GetComponentInChildren<Image>(true).sprite = ReadmeManager.GetReadmeEventSprites("Miracle20_Logo");
        }

        [HarmonyPatch(typeof(MiracleEventRewardUIPanel), nameof(MiracleEventRewardUIPanel.InitDateText))]
        [HarmonyPostfix]
        private static void MiracleRewardDate_Init(MiracleEventRewardUIPanel __instance)
        {
            if (__instance._eventTitleId.Contains("miracle"))
                __instance.tmp_eventDate.text = "06:00 26.12.2024(ЧТ) - 04:00 30.01.2025(ЧТ) (МСК)";
        }
        [HarmonyPatch(typeof(MiracleEventRewardButton), nameof(MiracleEventRewardButton.SetData))]
        [HarmonyPostfix]
        private static void MiracleLock_Init(MiracleEventRewardButton __instance)
        {
            __instance.img_lock.sprite = ReadmeManager.GetReadmeEventSprites("Miracle20_Lock");
            __instance.img_complete.sprite = ReadmeManager.GetReadmeEventSprites("Miracle20_Get");
        }
        #endregion

        #region Walpurgisnacht 5
        //[HarmonyPatch(typeof(Walpu5EventUIPanel), nameof(Walpu5EventUIPanel.Open))]
        //[HarmonyPostfix]
        //private static void Walpu5EventUIPanel_Open(Walpu5EventUIPanel __instance, UIPresenter uiPresenter)
        //{
        //    __instance.img_todaysBook.sprite = ReadmeManager.GetReadmeEventSprites("bookoftheday");
        //    __instance._logoImage.sprite = ReadmeManager.GetReadmeEventSprites("Walpu5Logo");
        //    __instance._dateText.text = "06:00 01.09.2024 (ЧТ) - 04:00 23.01.2024 (ЧТ) (МСК)";
        //}
        //[HarmonyPatch(typeof(Walpu3EventRewardButton), nameof(Walpu3EventRewardButton.SetData))]
        //[HarmonyPostfix]
        //private static void ThirdWalpuClear_Init(Walpu3EventRewardButton __instance)
        //{
        //    __instance.cg_check.transform.Find("[Image]Complete").GetComponentInChildren<Image>(true).sprite = ReadmeManager.GetReadmeEventSprites("WN3_Clear");
        //}
        //[HarmonyPatch(typeof(WalpuEventRewardPopup_LOR), nameof(WalpuEventRewardPopup_LOR.InitEventStataicData))]
        //[HarmonyPostfix]
        //private static void FifthWalpurgisReward(WalpuEventRewardPopup_LOR __instance)
        //{
        //    __instance.img_logo.sprite = ReadmeManager.GetReadmeEventSprites("Walpu5Logo");
        //}
        //[HarmonyPatch(typeof(WalpuEventRewardPopup_LOR), nameof(WalpuEventRewardPopup_LOR.InitDateText))]
        //[HarmonyPostfix]
        //private static void WalpuEventRewardPopup_LOR_Date(WalpuEventRewardPopup_LOR __instance)
        //{
        //    if (__instance._mainEventId.Contains("Walpu5"))
        //        __instance.tmp_eventDate.text = "06:00 01.09.2024 (ЧТ) - 04:00 23.01.2024 (ЧТ) (МСК)";
        //}
        //[HarmonyPatch(typeof(ActTypoLORBattleResultUI), nameof(ActTypoLORBattleResultUI.Open))]
        //[HarmonyPostfix]
        //private static void LoR_Finisher(ActTypoLORBattleResultUI __instance)
        //{
        //    if (__instance._isWin)
        //        __instance._resultTypoImage.overrideSprite = ReadmeManager.GetReadmeEventSprites("WP3_Victory");
        //    else
        //        __instance._resultTypoImage.overrideSprite = ReadmeManager.GetReadmeEventSprites("WP3_Defeat");
        //}
        #endregion

        #region LCB CheckUp
        [HarmonyPatch(typeof(LCBCheckupEventUIPanel), nameof(LCBCheckupEventUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void LCBCheckupEventUIPanel_Set(LCBCheckupEventUIPanel __instance)
        {
            if (__instance._subchapterId != 9114)
                return;
            __instance.tmp_eventDate.text = "06:00 23.01.2025 (ЧТ) - 04:00 20.02.2025 (ЧТ) (МСК)";
            foreach (var logo in __instance._logoImages)
            {
                logo.sprite = ReadmeManager.GetReadmeEventSprites("RegularCU");
            }
        }
        [HarmonyPatch(typeof(LCBCheckupRewardUIPopup), nameof(LCBCheckupRewardUIPopup.InitEventStataicData))]
        [HarmonyPostfix]
        private static void LCBCheckupRewardUIPopup_Logo(LCBCheckupRewardUIPopup __instance)
        {
            __instance.img_logo.sprite = ReadmeManager.GetReadmeEventSprites("RegularCU");
            GameObject LCB_CheckUp = GameObject.Find("[Canvas]RatioMainUI/[Rect]PopupRoot/[UIPopup]LCBCheckup_Reward(Clone)/EventDescriptionPanel/[Image]ItemCounterPanel/tmp_label_itemCounter");
            LCB_CheckUp.GetComponentInChildren<TextMeshProLanguageSetter>(true).enabled = false;
            LCB_CheckUp.GetComponentInChildren<UITextDataLoader>(true).enabled = false;
            LCB_CheckUp.GetComponentInChildren<TextMeshProUGUI>(true).m_text = "Бумаг на руках:";
            LCB_CheckUp.GetComponentInChildren<TextMeshProUGUI>(true).text = "Бумаг на руках:";
        }
        [HarmonyPatch(typeof(LCBCheckupRewardUIPopup), nameof(LCBCheckupRewardUIPopup.InitDateText))]
        [HarmonyPostfix]
        private static void LCBCheckupRewardUIPopup_Date(LCBCheckupRewardUIPopup __instance)
        {
            if (__instance._eventDescId.Contains("lcb_checkup"))
                __instance.tmp_eventDate.text = "06:00 23.01.2025 (ЧТ) - 04:00 20.02.2025 (ЧТ) (МСК)";
        }
        #endregion

        #region Multiple Banners
        [HarmonyPatch(typeof(LCBCheckupEventMultipleBanner), nameof(LCBCheckupEventMultipleBanner.Init))]
        [HarmonyPostfix]
        private static void LCBCheckupEventMultipleBanner_Fix(LCBCheckupEventMultipleBanner __instance, ref MainEventData eventData)
        {
            var main = __instance._bannerDict["LCBCheckup"]._bannerImage.sprite;
            switch (main.name)
            {
                case string when main.name.Contains("07_24"):
                    __instance._bannerDict["LCBCheckup"]._bannerImage.sprite = ReadmeManager.GetReadmeEventSprites("RegularCU_Mini");
                    break;
                case string when main.name.Contains("07_15"):
                    __instance._bannerDict["LCBCheckup"]._bannerImage.sprite = ReadmeManager.GetReadmeEventSprites("RegularCU_Big");
                    break;
            }
            if (__instance._bannerDict["LCBCheckupReward"]._bannerImage == null)
                return;
            var reward = __instance._bannerDict["LCBCheckupReward"]._bannerImage.sprite;
            switch (reward.name)
            {
                case string when reward.name.Contains("07_21"):
                    __instance._bannerDict["LCBCheckupReward"]._bannerImage.sprite = ReadmeManager.GetReadmeEventSprites("RegularCU_RMini");
                    break;
                case string when reward.name.Contains("07_18"):
                    __instance._bannerDict["LCBCheckupReward"]._bannerImage.sprite = ReadmeManager.GetReadmeEventSprites("RegularCU_RBig");
                    break;
            }
        }
        [HarmonyPatch(typeof(StageEventBanner_Multiple), nameof(StageEventBanner_Multiple.Init))]
        [HarmonyPostfix]
        private static void StageEventBanner_Multiple_Fix(StageEventBanner_Multiple __instance, ref MainEventData eventData)
        {
            if (__instance._bannerDict.ContainsKey("YCGD"))
            {
                __instance._bannerDict["YCGD"]._bannerImage.sprite = ReadmeManager.GetReadmeEventSprites("YCGD_MBBG");
                __instance._bannerDict["YCGDReward"]._bannerImage.sprite = ReadmeManager.GetReadmeEventSprites("YCGD_REBG");
            }
        }
        [HarmonyPatch(typeof(MiracleEventMultipleBanner), nameof(MiracleEventMultipleBanner.Init))]
        [HarmonyPostfix]
        private static void MiracleEventMultipleBanner_Fix(MiracleEventMultipleBanner __instance, ref MainEventData eventData)
        {
            __instance._bannerDict["MiracleOfDistrict20"]._bannerImage.sprite = ReadmeManager.GetReadmeEventSprites("Miracle20_EventBannerLil");
        }
        #endregion

        #region YCGD BokGak
        [HarmonyPatch(typeof(YCGDEventUIPanel), nameof(YCGDEventUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void YCGDEventUIPanel_Set(YCGDEventUIPanel __instance)
        {
            if (__instance._subchapterId != 9107)
                return;
            __instance.tmp_eventDate.text = "06:00 06.02.2025 (ЧТ) - 04:00 06.03.2025 (ЧТ) (МСК)";
            foreach (var logo in __instance._logoimages)
            {
                logo.sprite = ReadmeManager.GetReadmeEventSprites("YCGD_LogoBokGak");
            }
        }
        [HarmonyPatch(typeof(YCGDRewardUIPopup), nameof(YCGDRewardUIPopup.InitEventStataicData))]
        [HarmonyPostfix]
        private static void LCBCheckupRewardUIPopup_Logo(YCGDRewardUIPopup __instance)
        {
            __instance.img_logo.sprite = ReadmeManager.GetReadmeEventSprites("YCGD_LogoBokGak");
        }
        [HarmonyPatch(typeof(YCGDRewardUIPopup), nameof(YCGDRewardUIPopup.InitDateText))]
        [HarmonyPostfix]
        private static void LCBCheckupRewardUIPopup_Date(YCGDRewardUIPopup __instance)
        {
            if (__instance._eventDescId.Contains("ycgd_reward_popup_desc"))
                __instance.tmp_eventDate.text = "06:00 06.02.2025 (ЧТ) - 04:00 06.03.2025 (ЧТ) (МСК)";
        }
        #endregion

        #region 2nd Anniversary
        [HarmonyPatch(typeof(LimbusAnniversaryEventUIPopup), nameof(LimbusAnniversaryEventUIPopup.SetData))]
        [HarmonyPostfix]
        private static void LCB_Anniversary (LimbusAnniversaryEventUIPopup __instance)
        {
            if (__instance != null)
            {
                __instance.transform.Find("img_background").GetComponentInChildren<Image>().overrideSprite = ReadmeManager.GetReadmeEventSprites("LCB_Anniversary2nd_Background");
                Image anniv = __instance.transform.Find("[image]Panel/[Image]typo").GetComponent<Image>();
                anniv.overrideSprite = ReadmeManager.GetReadmeEventSprites("LCB_Anniversary2nd_Label");
                __instance.tmp_eventDate.text = "18:00 26.02.2025 (ЧТ) - 04:00 27.03.2025 (ЧТ) (МСК)";
            }
        }
        [HarmonyPatch(typeof(LimbusAnniversaryAttendanceUIPopup_2ndAnniversary), nameof(LimbusAnniversaryAttendanceUIPopup_2ndAnniversary.SetData))]
        [HarmonyPostfix]
        private static void Anniversary_Attendance(LimbusAnniversaryAttendanceUIPopup_2ndAnniversary __instance)
        {
            __instance.tmp_eventDate.text = "06:00 27.02.2025 (ЧТ) - 04:00 27.03.2025 (ЧТ) (МСК)";
            Image annivBG =__instance.transform.Find("[image]Panel").GetComponentInChildren<Image>();
            annivBG.overrideSprite = ReadmeManager.GetReadmeEventSprites("LCB_Anniversary2nd_AttendanceBG");
        }
        [HarmonyPatch(typeof(LimbusAnniversaryRewardButton), nameof(LimbusAnniversaryRewardButton.SetData))]
        [HarmonyPostfix]
        private static void Limbus1stAnniversaryComplete(LimbusAnniversaryRewardButton __instance)
        {
            Transform complete = __instance.transform.Find("[Image]Complete");
            if (complete != null)
            {
                complete.GetComponentInChildren<Image>(true).overrideSprite = ReadmeManager.GetReadmeEventSprites("1stLCBAnniversary_Complete");
            }
        }

        [HarmonyPatch(typeof(LimbusAnniversaryRewardSign), nameof(LimbusAnniversaryRewardSign.SetData))]
        [HarmonyPostfix]
        private static void Limbus1stAnniversaryTexts(LimbusAnniversaryRewardSign __instance)
        {
            if (__instance.tmp_rewardName.text.StartsWith("Вергилий Комментатор"))
                __instance.tmp_rewardName.text = "Получите записи комментатора Вергилия, особую плашку на 2-ю годовщину компании «Лимбус», а также особый декор билета с анимацией!";
            else if (__instance.tmp_rewardName.text.StartsWith("2nd Anniversary"))
                __instance.tmp_rewardName.text = "Вас ждёт декор визитки-билета на 2-ю годовщину!";
            else
                __instance.tmp_rewardName.text = "Плашка 2-й годовщины «Лимбуса» и 1 Именной билет идентичности Сезона 1, получите и распишитесь, менеджер.";
        }
            #endregion
        }
}
