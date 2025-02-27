using HarmonyLib;
using BattleUI;
using BattleUI.Typo;
using MainUI;
using MainUI.VendingMachine;
using UnityEngine;
using UnityEngine.UI;
using StorySystem;
using BattleUI.BattleUnit;
using MainUI.Gacha;
using Dungeon.Shop;
using ChoiceEvent;
using BattleUI.Information;
using System;
using BattleUI.EvilStock;
using UIComponent.Character.Icon;
using TMPro;
using MainUI.BattlePass;

namespace LimbusLocalizeRUS
{
    public static class SpriteUI
    {
        #region Clashing
        [HarmonyPatch(typeof(ParryingTypoUI), nameof(ParryingTypoUI.SetParryingTypoData))]
        [HarmonyPrefix]
        private static void ParryingTypoUI_Init(ParryingTypoUI __instance)
        {
            __instance.img_parryingTypo.overrideSprite = ReadmeManager.GetReadmeSprites("ParryThisYouFilthyWinrater");
            __instance.img_parryingTypo.rectTransform.sizeDelta = new Vector2(540, 645);
            __instance.img_parryingTypo.rectTransform.anchoredPosition = new Vector2(260, -40);
        }
        #endregion

        #region Login
        [HarmonyPatch(typeof(LoginSceneManager), nameof(LoginSceneManager.OnInitLoginInfoManagerEnd))]
        [HarmonyPostfix]
        private static void LoginSceneManager_Init(LoginSceneManager __instance)
        {
            Transform catchphrase = __instance._canvas.transform.Find("[Image]Catchphrase");
            Transform logo = __instance._canvas.transform.Find("[Image]Logo");
            if (catchphrase.GetComponentInChildren<Image>(true).sprite.name == "season_catchphrase")
            {
                catchphrase.GetComponentInChildren<Image>(true).sprite = ReadmeManager.GetReadmeSprites("Catchphrase");
            }
            __instance.img_touchToStart.sprite = ReadmeManager.GetReadmeSprites("Start");
            Transform motto = __instance.transform.Find("[Canvas]/[Image]RedLine/[Image]Phrase");
            Motto_Changer(catchphrase, logo, motto);
        }

        public static void Motto_Changer(Transform catchphrase, Transform logo, Transform motto)
        {
            DateTime event_end = new DateTime(2025, 2, 27, 2, 59, 0).ToLocalTime();

            DateTime startup = DateTime.Now;
            if (motto != null)
            {
                if (catchphrase.gameObject.active == true)
                        motto.GetComponentInChildren<Image>(true).sprite = ReadmeManager.GetReadmeSprites("Motto_Season");
                else if (logo.gameObject.active == true)
                {
                    if (DateTime.Compare(startup, event_end) < 0)
                        motto.GetComponentInChildren<Image>(true).sprite = ReadmeManager.GetReadmeSprites("Motto_Event");
                    else
                        motto.GetComponentInChildren<Image>(true).sprite = ReadmeManager.GetReadmeSprites("Motto_Default");
                }
            }
        }
        #endregion

        #region Main Menu
        [HarmonyPatch(typeof(LowerControlUIPanel), nameof(LowerControlUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void LunacyTag_Init(LowerControlUIPanel __instance)
        {
            Transform lunacyTag = __instance.transform.Find("[Rect]Pivot/[Rect]UserInfoUI/[Rect]Info/[Button]CurrencyInfo/[Image]CashTag");
            if (lunacyTag != null)
            {
                lunacyTag.GetComponentInChildren<Image>(true).sprite = ReadmeManager.GetReadmeSprites("LunacyTag");
            }
        }
        [HarmonyPatch(typeof(MirrorDungeonBanner), nameof(MirrorDungeonBanner.Initialize))]
        [HarmonyPostfix]
        private static void MirrorDungeon_Banner_Init(MirrorDungeonBanner __instance)
        {
            Transform banner = __instance._hsv.transform.Find("[Rect]Items/[Image]ImageBackground/[Image]Image");
            if (banner.GetComponentInChildren<Image>(true).sprite.name.StartsWith("MirrorDungeon5"))
            {
                banner.GetComponentInChildren<Image>(true).overrideSprite = ReadmeManager.GetReadmeSprites("MirrorDungeon_Banner");
            }
        }

        [HarmonyPatch(typeof(ElementsSummary), nameof(ElementsSummary.SetHighlight))]
        [HarmonyPostfix]
        private static void ElementsSummary_RU(ElementsSummary __instance)
        {
            __instance._effectTag.sprite = ReadmeManager.GetReadmeSprites("UserInfo_Effect");
        }
        [HarmonyPatch(typeof(UserInfoBanner), nameof(UserInfoBanner.SetDataForUserChangePopup))]
        [HarmonyPostfix]
        private static void UserInfoBanner_Effect(UserInfoBanner __instance)
        {
            __instance._bannerUseEffectTag.sprite = ReadmeManager.GetReadmeSprites("UserInfo_Effect");
            __instance._subBannerEffectTag.sprite = ReadmeManager.GetReadmeSprites("UserInfo_Effect");
        }

        [HarmonyPatch(typeof(StageProgressRewardButton), nameof(StageProgressRewardButton.Init))]
        [HarmonyPostfix]
        private static void StageProgressRewardButton_RU(StageProgressRewardButton __instance)
        {
            __instance.img_clearSign.overrideSprite = ReadmeManager.GetReadmeSprites("ClearStamp");
        }

        [HarmonyPatch(typeof(PersonalityStoryPersonalityStorySlot), nameof(PersonalityStoryPersonalityStorySlot.SetData))]
        [HarmonyPostfix]
        private static void PersonalityStoryPersonalityStorySlot_RU(PersonalityStoryPersonalityStorySlot __instance)
        {
            __instance._clearObj.GetComponentInChildren<Image>().overrideSprite = ReadmeManager.GetReadmeSprites("ClearStamp");
        }
        #endregion

        #region Battle Pass
        [HarmonyPatch(typeof(BattlePassUIMissionSlot), nameof(BattlePassUIMissionSlot.SetData))]
        [HarmonyPostfix]
        private static void BattlePass_Mission_ClearStamp(BattlePassUIMissionSlot __instance)
        {
            if (__instance.clearImage != null)
                __instance.clearImage.overrideSprite = ReadmeManager.GetReadmeSprites("ClearStamp");
        }
        [HarmonyPatch(typeof(BattlePassUIRewardSlot), nameof(BattlePassUIRewardSlot.SetData))]
        [HarmonyPostfix]
        private static void BattlePass_ClearStamps(BattlePassUIRewardSlot __instance)
        {
            if (__instance._ticketTop.img_clear != null)
                __instance._ticketTop.img_clear.overrideSprite = ReadmeManager.GetReadmePassSprites("ClearStamp_FreePass");
            if (__instance._ticketBottom.img_clear != null)
                __instance._ticketBottom.img_clear.overrideSprite = ReadmeManager.GetReadmePassSprites("ClearStamp_LimbusPass");
        }
        #endregion

        #region Friends
        [HarmonyPatch(typeof(UserInfoTicketItem), nameof(UserInfoTicketItem.SetData))]
        [HarmonyPostfix]
        private static void TicketInfoPopup_EffectSprite(UserInfoTicketItem __instance)
        {
            __instance._useEffectTagImage.overrideSprite = ReadmeManager.GetReadmeSprites("UserInfo_Effect");
            __instance._subTicketUseEffectTagImage.overrideSprite = ReadmeManager.GetReadmeSprites("UserInfo_Effect");
        }
        #endregion

        #region Vending Machine
        [HarmonyPatch(typeof(VendingMachineUIPanel), nameof(VendingMachineUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void VendingMachineUIPanel_Init(VendingMachineUIPanel __instance)
        {
            Transform soldOut = __instance.transform.Find("GoodsStoreAreaMaster/GoodsStorePanelGroup/BackPanel/Main/SoldOut");
            if (soldOut != null)
            {
                soldOut.GetComponentInChildren<Image>(true).sprite = ReadmeManager.GetReadmeSprites("SoldOut");
            }
        }
        [HarmonyPatch(typeof(ElementListScrollItem), nameof(ElementListScrollItem.SetRedDot))]
        [HarmonyPostfix]
        private static void VendingMachine_NewRedDot(ElementListScrollItem __instance)
        {
            __instance._redDot.GetComponentInChildren<Image>(true).overrideSprite = ReadmeManager.GetReadmeSprites("New");
        }
        #endregion

        #region Formation UI
        [HarmonyPatch(typeof(FormationPersonalityUI), nameof(FormationPersonalityUI.Initialize))]
        [HarmonyPostfix]
        private static void FormationPersonalityUI_Init(FormationPersonalityUI __instance)
        {
            __instance.img_support.sprite = ReadmeManager.GetReadmeSprites("SupportTag");
            __instance._redDot.GetComponentInChildren<Image>(true).sprite = ReadmeManager.GetReadmeSprites("New");
        }
        [HarmonyPatch(typeof(FormationPersonalityUI_Label), nameof(FormationPersonalityUI_Label.Reload))]
        [HarmonyPostfix]
        private static void FormationPersonalityUI_Label_Init(FormationPersonalityUI_Label __instance)
        {
            switch (__instance._model._status)
            {
                case FormationPersonalityUI_LabelTypes.Participated:
                    __instance.img_label.overrideSprite = ReadmeManager.GetReadmeSprites("InParty");
                    break;
                case FormationPersonalityUI_LabelTypes.Baton:
                    __instance.img_label.overrideSprite = ReadmeManager.GetReadmeSprites("Backup_Label");
                    __instance.img_label.GetComponentInChildren<RectTransform>().sizeDelta = new Vector2(6, 0);
                    break;
            }
        }
        [HarmonyPatch(typeof(FormationSwitchablePersonalityUIScrollViewItem), nameof(FormationSwitchablePersonalityUIScrollViewItem.Initialize))]
        [HarmonyPostfix]
        private static void FormationSwitchablePersonalityUIScrollViewItem_Init(FormationSwitchablePersonalityUIScrollViewItem __instance)
        {
            Transform img_isParticipaged = __instance._participatedObject.transform.parent.parent.parent.Find("[Image]ParticipateSlotUI");
            img_isParticipaged.GetComponentInChildren<Image>(true).sprite = ReadmeManager.GetReadmeSprites("InParty");
            __instance._newAcquiredRedDot.GetComponentInChildren<Image>(true).sprite = ReadmeManager.GetReadmeSprites("New");
        }
        [HarmonyPatch(typeof(FormationSwitchableSupporterPersonalityUIScrollViewItem), nameof(FormationSwitchableSupporterPersonalityUIScrollViewItem.SetData))]
        [HarmonyPostfix]
        private static void YobenBoben(FormationSwitchableSupporterPersonalityUIScrollViewItem __instance)
        {
            __instance._selectedFrame.sprite = ReadmeManager.GetReadmeSprites("InParty");
        }
        [HarmonyPatch(typeof(PersonalityUILabelScriptable), nameof(PersonalityUILabelScriptable.Convert))]
        [HarmonyPostfix]
        private static void ParticipationLabel_Scriptable(PersonalityUILabelScriptable __instance)
        {
            //__instance._participatedLabelSprite = ReadmeManager.GetReadmeSprites("InParty");
            __instance._batonSprite = ReadmeManager.GetReadmeSprites("Backup_Label");
        }
        //[HarmonyPatch(typeof(FormationPersonalityUI_Label),  nameof(FormationPersonalityUI_Label.SetData))]
        //[HarmonyPostfix]
        //private static void SelectedLabel_Testing(FormationPersonalityUI_Label __instance)
        //{
        //    if (__instance.img_label.name == "New_MainUI_Formation_1_2")
        //        __instance.img_label.overrideSprite = ReadmeManager.GetReadmeSprites("InParty");
        //}
        [HarmonyPatch(typeof(FormationEgoSlot), nameof(FormationEgoSlot.SetData))]
        [HarmonyPostfix]
        private static void FormationEgoSlot_Init(FormationEgoSlot __instance)
        {
            __instance._redDot.GetComponentInChildren<Image>(true).overrideSprite = ReadmeManager.GetReadmeSprites("New");
        }
        [HarmonyPatch(typeof(FormationSwitchablePersonalityUIPanel), nameof(FormationSwitchablePersonalityUIPanel.SetDataOpen))]
        [HarmonyPostfix]
        private static void FormationSwitchablePersonalityUIPanel_Init(FormationSwitchablePersonalityUIPanel __instance)
        {
            Transform newPersonality = __instance.transform.Find("[Script]RightPanel/[Script]FormationEgoList/[Script]RedDot");
            __instance._egoRedDot.GetComponentInChildren<Image>(true).overrideSprite = ReadmeManager.GetReadmeSprites("New");
            __instance._personalityRedDot.GetComponentInChildren<Image>(true).overrideSprite = ReadmeManager.GetReadmeSprites("New");
            if (newPersonality != null)
                newPersonality.GetComponentInChildren<Image>(true).overrideSprite = ReadmeManager.GetReadmeSprites("New");
        }
        [HarmonyPatch(typeof(FormationSwitchableEgoUIScrollViewItem), nameof(FormationSwitchableEgoUIScrollViewItem.SetData))]
        [HarmonyPostfix]
        private static void RedDotAgain_Init(FormationSwitchableEgoUIScrollViewItem __instance)
        {
            __instance._newAcquiredRedDot.GetComponentInChildren<Image>(true).sprite = ReadmeManager.GetReadmeSprites("New");
        }
        [HarmonyPatch(typeof(UnitInformationPersonalitySkillTypeButton), nameof(UnitInformationPersonalitySkillTypeButton.SetData))]
        [HarmonyPostfix]
        private static void FormationUI_3rdSkill_Lock(UnitInformationPersonalitySkillTypeButton __instance)
        {
            __instance.img_lock.GetComponent<RectTransform>().anchoredPosition = new Vector2(-40, -25);
        }
        #endregion

        #region Support Formation UI
        [HarmonyPatch(typeof(FormationSwitchablePersonalityUIPanel), nameof(FormationSwitchablePersonalityUIPanel.SetDataOpen))]
        [HarmonyPostfix]
        private static void Support_Init(FormationSwitchablePersonalityUIPanel __instance)
        {
            // SUPPORT TAG
            Transform support_tag = __instance.transform.Find("[Script]RightPanel/[Script]FormationEgoList/[Image]SupportTag");
            if (support_tag != null)
                support_tag.GetComponentInChildren<Image>(true).sprite = ReadmeManager.GetReadmeSprites("SupportTag");
        }
        #endregion

        #region Mirror Dungeon
        [HarmonyPatch(typeof(MirrorDungeonThemeCardUI), nameof(MirrorDungeonThemeCardUI.Reload))]
        [HarmonyPostfix]
        private static void MirrorDungeonThemeCard43UI_SomeSettings(MirrorDungeonThemeCardUI __instance)
        {
            var redDot = __instance.transform.Find("[Script]RedDot");
            var lvlMd = __instance.transform.Find("[Rect]Cards/[Rect]Additional/rect_level/[Image]Icon");
            if (redDot != null)
                redDot.GetComponentInChildren<Image>().overrideSprite = ReadmeManager.GetReadmeSprites("New");
            if (lvlMd != null)
                lvlMd.GetComponentInChildren<Image>().overrideSprite = ReadmeManager.GetReadmeSprites("LvlMD");
        }
        [HarmonyPatch(typeof(MirrorDungeonSelectThemeUIPanel), nameof(MirrorDungeonSelectThemeUIPanel.ReloadData))]
        [HarmonyPostfix]
        private static void MirrorDungeonSelectThemeUIPane42l_SomeSettings(MirrorDungeonSelectThemeUIPanel __instance, ref MirrorDungeonSelectThemeUIPanel.Model data, ref bool isPlayAnimation, ref bool isRetainViewDetail, ref bool isDifficultyChanged)
        {
            switch (__instance.img_difficulty.sprite.name)
            {
                case "MainUI_MirrorDungeon5_6_1":
                    __instance.img_difficulty.overrideSprite = ReadmeManager.GetReadmeSprites("HardMD");
                    break;
                case "MainUI_MirrorDungeon5_6_3":
                    __instance.img_difficulty.overrideSprite = ReadmeManager.GetReadmeSprites("NormalMD");
                    break;
            }
            switch (__instance.img_clearBonus.sprite.name)
            {
                case "MainUI_MirrorDungeon5_6_17":
                    __instance.img_clearBonus.overrideSprite = ReadmeManager.GetReadmeSprites("MD5HB");
                    break;
                case "MainUI_MirrorDungeon5_6_7":
                    __instance.img_clearBonus.overrideSprite = ReadmeManager.GetReadmeSprites("MD5B");
                    break;
            }
            switch (__instance.img_difficultyToggle.overrideSprite.name)
            {
                case "MainUI_MirrorDungeon5_6_10":
                    __instance.img_difficultyToggle.overrideSprite = ReadmeManager.GetReadmeSprites("MDT10");
                    break;
                case "MainUI_MirrorDungeon5_6_13":
                    __instance.img_difficultyToggle.overrideSprite = ReadmeManager.GetReadmeSprites("MDT13");
                    break;
            }
            if (__instance.img_difficultyToggleHighlight != null)
            {
                __instance.img_difficultyToggleHighlight.overrideSprite = ReadmeManager.GetReadmeSprites("MDT11");
            }
        }
        [HarmonyPatch(typeof(AbnormalityStatUI), nameof(AbnormalityStatUI.SetAbnormalityGuideUIActive))]
        [HarmonyPostfix]
        private static void BottomStat_Init(AbnormalityStatUI __instance)
        {
            Transform new_info = __instance.transform.Find("[Rect]FixedScalePivot/[Text]UnitName/[Image]Icon");
            if (new_info != null)
                new_info.GetComponentInChildren<Image>(true).sprite = ReadmeManager.GetReadmeSprites("NewInfo");
        }
        [HarmonyPatch(typeof(MirrorDungeonShopItemSlot), nameof(MirrorDungeonShopItemSlot.SetData))]
        [HarmonyPostfix]
        private static void MirrorDungeonShopItemSlot_Init(MirrorDungeonShopItemSlot __instance)
        {
            __instance._soldOutTitleObject.GetComponentInChildren<UnityEngine.UI.Image>().sprite = ReadmeManager.GetReadmeSprites("Mirror_SoldOut");
            __instance._soldOutTitleObject.transform.Find("[Text]SoldOut").GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            __instance.tmp_lack.fontSizeMax = 25;
            if (__instance.tmp_title.text.Contains(" Дар") || __instance.tmp_title.text.Contains("Вылечить"))
                __instance.tmp_title.fontSizeMax = 28;
        }
        [HarmonyPatch(typeof(ChoiceEventController), nameof(ChoiceEventController.InitCallbacks))]
        [HarmonyPostfix]
        private static void ChoiceEventSkip(ChoiceEventController __instance)
        {
            __instance._backgroundUI.img_battleBG.transform.Find("[Image]ScenarioPanel/[Script]ScenarioField/[Rect]StorySkipButton/[Button]StorySkipButton/[Image]SkipIcon (1)").GetComponentInChildren<Image>(true).sprite = ReadmeManager.GetReadmeSprites("Skip_Choice");
            __instance._choiceSectionUI.btn_skip.GetComponentInChildren<Image>(true).sprite = ReadmeManager.GetReadmeSprites("Skip_Choice");
        }
        [HarmonyPatch(typeof(UIButtonWithOverlayImg), nameof(UIButtonWithOverlayImg.SetDefault))]
        [HarmonyPostfix]
        private static void ChoiceEvent_EgoGiftButton(UIButtonWithOverlayImg __instance)
        {
            if (__instance.gameObject.name == "[Button]ViewEgoGift")
                __instance.gameObject.GetComponentInChildren<Image>(true).sprite = ReadmeManager.GetReadmeSprites("EgoGift_MirrorButton");
        }
        [HarmonyPatch(typeof(MirrorDungeonFloorRewardList), nameof(MirrorDungeonFloorRewardList.SetData))]
        [HarmonyPostfix]
        private static void DungeonClearLogo(MirrorDungeonFloorRewardList __instance)
        {
            foreach (var rewardItem in __instance._rewardItemList)
            {
                rewardItem.img_clearLogo.sprite = ReadmeManager.GetReadmeSprites("DungeonClearLogo");
                rewardItem.tmp_floor.text = rewardItem.tmp_floor.text.Replace("[HARD]", "「УСЛОЖ.」");
                rewardItem.tmp_floor.text = rewardItem.tmp_floor.text.Replace("[NORMAL]", "「ОБЫЧ.」");
            }
        }
        [HarmonyPatch(typeof(MirrorDungeonIconManagerInBattleScene), nameof(MirrorDungeonIconManagerInBattleScene.SetActiveMirrorDungeonEgoGiftButton))]
        [HarmonyPostfix]
        private static void MirrorGiftBox(MirrorDungeonIconManagerInBattleScene __instance)
        {
            __instance.btn_egoGiftPopup.GetComponentInChildren<Image>(true).sprite = ReadmeManager.GetReadmeSprites("EgoGift_MirrorButton");
        }
        [HarmonyPatch(typeof(MirrorDungeonFinishedPanel), nameof(MirrorDungeonFinishedPanel.Initialize))]
        [HarmonyPostfix]
        private static void MirrorClear(MirrorDungeonFinishedPanel __instance)
        {
            foreach (var clear_logo in __instance._progressPanel._iconList._rewardItemList)
            {
                clear_logo.img_clearLogo.sprite = ReadmeManager.GetReadmeSprites("DungeonClearLogo");
            }
        }
        [HarmonyPatch(typeof(MirrorDungeonRewardPopup_Season4), nameof(MirrorDungeonRewardPopup_Season4.SetDataOpenEvent))]
        [HarmonyPostfix]
        private static void LastClearPlease(MirrorDungeonRewardPopup_Season4 __instance)
        {
            __instance._progressUI.img_clearLogo.sprite = ReadmeManager.GetReadmeSprites("DungeonClearLogo");
            __instance._progressUI.tmp_floor.text = __instance._progressUI.tmp_floor.text.Replace("[HARD]", "「УСЛОЖ.」");
            __instance._progressUI.tmp_floor.text = __instance._progressUI.tmp_floor.text.Replace("[NORMAL]", "「ОБЫЧ.」");
        }
        [HarmonyPatch(typeof(MirrorDungeonThemeListItemUI), nameof(MirrorDungeonThemeListItemUI.Reload))]
        [HarmonyPostfix]
        private static void MirrorDungeon_Cleared_FloorDifficulty(MirrorDungeonThemeListItemUI __instance)
        {
            __instance._difficultyText.text = __instance._difficultyText.text.Replace("HARD", "УСЛОЖНЁННЫЙ").Replace("NORMAL", "ОБЫЧНЫЙ");
        }
        [HarmonyPatch(typeof(MirrorDungeonFinishedPanel_Progress), nameof(MirrorDungeonFinishedPanel_Progress.SetData))]
        [HarmonyPostfix]
        private static void MirrorDungeon_Cleared_LastFloor(MirrorDungeonFinishedPanel_Progress __instance)
        {
            __instance.transform.Find("[Text]Title").GetComponentInChildren<TextMeshProUGUI>().lineSpacing = -30;
            __instance.tmp_floor.text = __instance.tmp_floor.text.Replace("[HARD]", "「УСЛОЖ.」").Replace("[NORMAL]", "「ОБЫЧ.」");
        }
        [HarmonyPatch(typeof(MirrorDungeonSelectedEnemyBuffIconAndNameScrollUIItem), nameof(MirrorDungeonSelectedEnemyBuffIconAndNameScrollUIItem.SetData))]
        [HarmonyPostfix]
        private static void MirrorDungeon_Cleared_DebuffNames(MirrorDungeonSelectedEnemyBuffIconAndNameScrollUIItem __instance)
        {
            __instance._iconAndNameUI.tmp_name.lineSpacing = -30;
        }
        [HarmonyPatch(typeof(MirrorDungeonSelectThemeUIPanel), nameof(MirrorDungeonSelectThemeUIPanel.Reload))]
        [HarmonyPostfix]
        private static void ThemeUIPanel_ClearBonus(MirrorDungeonSelectThemeUIPanel __instance)
        {
            Image clear_bonus = __instance._clearBonus.GetComponentInChildren<Image>(true);
            if (clear_bonus.sprite.name.Contains("MirrorDungeon4"))
            {
                clear_bonus.overrideSprite = ReadmeManager.GetReadmeSprites("MirrorDungeon_FloorBonus");
            }
        }
        [HarmonyPatch(typeof(MirrorDungeonRewardPopup_Season4), nameof(MirrorDungeonRewardPopup_Season4.SetPopup))]
        [HarmonyPostfix]
        private static void MirrorDungeonRewardPopup_Season423_Name(MirrorDungeonRewardPopup_Season4 __instance)
        {
            foreach (var haha in __instance._chanceButtonList)
            {
                haha.img_off.overrideSprite = ReadmeManager.GetReadmeSprites("OFF_Bonus");
                haha.img_on.overrideSprite = ReadmeManager.GetReadmeSprites("ON_Bonus");
            }
        }
        #endregion

        #region Battle UI
        [HarmonyPatch(typeof(BattleUIRoot), nameof(BattleUIRoot.Init))]
        [HarmonyPostfix]
        private static void BattleUI_Init(BattleUIRoot __instance)
        {
            Transform turnUI = __instance.transform.Find("[Canvas]PerspectiveUI/SafeArea/[Script]WaveUI/[Rect]Pivot/[Image]TurnPanel");
            Transform start = __instance.transform.Find("[Canvas,Script]BattleUIController/SafeArea/[Script]NewOperationController/[Rect]ActiveControl/[Rect]Pivot/[Rect]ActionableSlotList/[Layout]SinActionSlotsGrid/[EventTrigger]EndButton/[Image]RightLeg/[Rect]StartUI/[Rect]Pivot/[Image]Start");
            if (turnUI != null)
            {
                turnUI.GetComponentInChildren<Image>(true).sprite = ReadmeManager.GetReadmeSprites("TurnUI");
                turnUI.GetComponentInChildren<Image>(true).m_Sprite = ReadmeManager.GetReadmeSprites("TurnUI");
                turnUI.GetComponentInChildren<Image>(true).overrideSprite = ReadmeManager.GetReadmeSprites("TurnUI");
            }
            if (start != null)
            {
                start.GetComponentInChildren<Image>(true).sprite = ReadmeManager.GetReadmeSprites("StartBattle");
                start.GetComponentInChildren<Image>(true).m_Sprite = ReadmeManager.GetReadmeSprites("StartBattle");
                start.GetComponentInChildren<Image>(true).overrideSprite = ReadmeManager.GetReadmeSprites("StartBattle");
            }
        }
        [HarmonyPatch(typeof(WaveUI), nameof(WaveUI.SetWaveWaveUIDisplay))]
        [HarmonyPostfix]
        private static void WaveUI_DynamicLabel(WaveUI __instance)
        {
            var wavePanel = __instance._wavePanelImage.sprite;
            switch (wavePanel.name)
            {
                case "BattleUI_UpperLeft_1_23":
                    __instance._wavePanelImage.sprite = ReadmeManager.GetReadmeSprites("WaveUI_Yellow");
                    break;
                case "BattleUI_UpperLeft_1_40":
                    __instance._wavePanelImage.sprite = ReadmeManager.GetReadmeSprites("EnemyUI");
                    break;
            }
        }
        [HarmonyPatch(typeof(ActTypoTurnUI), nameof(ActTypoTurnUI.Open))]
        [HarmonyPostfix]
        private static void PreBattleUI_Init(ActTypoTurnUI __instance)
        {
            Image turn = __instance.transform.Find("[Image]Turn").GetComponentInChildren<Image>(true);
            if (turn.enabled)
            {
                turn.overrideSprite = ReadmeManager.GetReadmeSprites("Turn");
            }
        }
        [HarmonyPatch(typeof(UnitInformationAbnormalityNameTag), nameof(UnitInformationAbnormalityNameTag.UpdateLayout))]
        [HarmonyPostfix]
        private static void RisksNameTag(UnitInformationAbnormalityNameTag __instance)
        {
            if (__instance.img_abClassType.sprite.name.EndsWith("27") || __instance.img_abClassType.sprite.name.EndsWith("28") || __instance.img_abClassType.sprite.name.EndsWith("31"))
                __instance.img_abClassType.overrideSprite = ReadmeManager.GetReadmeSprites("Risk_Unindentified");
        }
        [HarmonyPatch(typeof(AbnormalityStatUI), nameof(AbnormalityStatUI.CheckAbUnlockInformation))]
        [HarmonyPostfix]
        private static void Risks(AbnormalityStatUI __instance)
        {
            if (__instance.img_level.sprite.name.EndsWith("27") || __instance.img_level.sprite.name.EndsWith("28") || __instance.img_level.sprite.name.EndsWith("31"))
                __instance.img_level.overrideSprite = ReadmeManager.GetReadmeSprites("Risk_Unindentified");
        }
        [HarmonyPatch(typeof(DanteAbilityButton), nameof(DanteAbilityButton.DanteAbilityButtonPropertyUpdate))]
        [HarmonyPostfix]
        private static void DanteAbility(DanteAbilityButton __instance)
        {
            if (__instance._dnateAbilityBtnImage.sprite.name.EndsWith('0'))
                __instance._dnateAbilityBtnImage.overrideSprite = ReadmeManager.GetReadmeSprites("DanteAbility_Active");
            else
                __instance._dnateAbilityBtnImage.overrideSprite = ReadmeManager.GetReadmeSprites("DanteAbility_Inactive");
        }
        [HarmonyPatch(typeof(EvilStockController), nameof(EvilStockController.UpdateEnemyUIState))]
        [HarmonyPostfix]
        private static void EnemySins(EvilStockController __instance)
        {
            if (__instance._enemyEvilstockUI != null)
            {
                Image enemy_sins = __instance._enemyEvilstockUI.transform.Find("[Image]enemyEvilstockBackground/[Image]EnemyTag").GetComponentInChildren<Image>(true);
                enemy_sins.overrideSprite = ReadmeManager.GetReadmeSprites("Battle_EnemySins");
            }
        }
        [HarmonyPatch(typeof(PassiveUIManager), nameof(PassiveUIManager.SetData))]
        [HarmonyPostfix]
        private static void EnemyPassives(PassiveUIManager __instance)
        {
            if (!__instance._isPlayerUI)
            {
                Image enemy_passives = __instance.transform.Find("[Rect]Pivot/[Rect]BattleUnitPassive/[Image]Background/[Image]EnemyTag").GetComponentInChildren<Image>(true);
                enemy_passives.overrideSprite = ReadmeManager.GetReadmeSprites("Battle_EnemyPassives");
            }
        }
        #endregion

        #region EGO
        [HarmonyPatch(typeof(BattleSkillViewUIInfo), nameof(BattleSkillViewUIInfo.Init))]
        [HarmonyPostfix]
        private static void EGO_Perspective_Warnings(BattleSkillViewUIInfo __instance)
        {
            __instance.textFrame_Awaken = ReadmeManager.GetReadmeSprites("EGO_Awaken");
            __instance.textFrame_Erode = ReadmeManager.GetReadmeSprites("EGO_Erode");
        }
        #endregion

        #region Skip Button
        [HarmonyPatch(typeof(GachaResultUI), nameof(GachaResultUI.Initialize))]
        [HarmonyPostfix]
        private static void SkipGacha_Init(GachaResultUI __instance)
        {
            Transform skip_gacha = __instance.transform.Find("[Rect]GetNewCardRoot/[Button]Skip");
            if (skip_gacha != null)
            {
                skip_gacha.GetComponentInChildren<Image>(true).sprite = ReadmeManager.GetReadmeSprites("Skip");
            }
        }
        #endregion

        #region Auto Button
        [HarmonyPatch(typeof(StoryManager), nameof(StoryManager.Init))]
        [HarmonyPostfix]
        private static void AutoButton_Init(StoryManager __instance)
        {
            Transform autoButton = __instance._nonPostProcessRectTransform.transform.Find("[Rect]Buttons/[Rect]MenuObject/[Button]Auto");
            autoButton.GetComponentInChildren<Image>(true).sprite = ReadmeManager.GetReadmeSprites("AutoButton");
            autoButton.GetComponentInChildren<StoryUIButton>(true)._buttonImageList[0] = ReadmeManager.GetReadmeSprites("AutoButton");
            autoButton.GetComponentInChildren<StoryUIButton>(true)._buttonImageList[1] = ReadmeManager.GetReadmeSprites("AutoButton_Enabled");
            autoButton.GetComponentInChildren<StoryUIButton>(true)._buttonImageList[2] = ReadmeManager.GetReadmeSprites("TextButton");
        }
        #endregion

        #region BattleEnding
        [HarmonyPatch(typeof(ActBattleEndTypoUI), nameof(ActBattleEndTypoUI.Open))]
        [HarmonyPostfix]
        private static void ActBattleEndTypoUI_Init(ActBattleEndTypoUI __instance)
        {
            Transform Def = __instance._defeatGroup.transform.Find("[Image]Typo_Defeat");
            Transform Win = __instance._victoryGroup.transform.Find("[Image]Typo_Victory");
            Def.GetComponentInChildren<Image>().overrideSprite = ReadmeManager.GetReadmeSprites("Defeat_Text");
            Win.GetComponentInChildren<Image>().overrideSprite = ReadmeManager.GetReadmeSprites("Victory_Text");
        }
        [HarmonyPatch(typeof(BattleResultUIPanel), nameof(BattleResultUIPanel.SetStatusUI))]
        [HarmonyPostfix]
        private static void BattleResultUIPanel_Init(BattleResultUIPanel __instance)
        {
            if (__instance.img_ResultMark.sprite.name == "MainUI_BattleResult_1_20")
            {
                __instance.img_ResultMark.overrideSprite = ReadmeManager.GetReadmeSprites("Defeat");
            }
            else
            {
                __instance.img_ResultMark.overrideSprite = ReadmeManager.GetReadmeSprites("Victory");
            }
            __instance.img_exclear.overrideSprite = ReadmeManager.GetReadmeSprites("EX");
        }
        #endregion

        #region GachaResult
        [HarmonyPatch(typeof(GachaCardUI), nameof(GachaCardUI.OnDisable))]
        [HarmonyPostfix]
        private static void GachaCardUI_SetData(GachaCardUI __instance)
        {
            __instance.img_newMark.overrideSprite = ReadmeManager.GetReadmeSprites("NewGacha");
        }
        [HarmonyPatch(typeof(ElementsSummary), nameof(ElementsSummary.SetDefault))]
        [HarmonyPostfix]
        private static void ElementsSummary_Init(ElementsSummary __instance)
        {
            if (__instance._redDot != null)
            {
                __instance._redDot.GetComponentInChildren<Image>().overrideSprite = ReadmeManager.GetReadmeSprites("New");
            }
        }
        #endregion

        #region Overclock
        [HarmonyPatch(typeof(ActTypoOverClockUI), nameof(ActTypoOverClockUI.Open))]
        [HarmonyPostfix]
        private static void ActTypoOverClockUI_Init(ActTypoOverClockUI __instance)
        {
            __instance.tmp_content.text = "РАЗГОН";
        }
        [HarmonyPatch(typeof(BattleSkillViewUIOverClock), nameof(BattleSkillViewUIOverClock.SetActiveOverClock))]
        [HarmonyPostfix]
        private static void BattleSkillViewUIOverClock_SetData(BattleSkillViewUIOverClock __instance)
        {
            __instance._image_OverClock.m_OverrideSprite = ReadmeManager.GetReadmeSprites("Overclock");
        }
        #endregion

        #region Critical
        [HarmonyPatch(typeof(BattleUnitDamageTypoUISlot), nameof(BattleUnitDamageTypoUISlot.Update))]
        [HarmonyPostfix]
        private static void BattleUnitDamageTypoUISlot_Init(BattleUnitDamageTypoUISlot __instance)
        {
            var crit = __instance.img_resist.sprite.name;
            switch (__instance.img_resist.sprite.name)
            {
                case string name when name.Contains("Amber"):
                    __instance.img_Critical.overrideSprite = ReadmeManager.GetReadmeSprites("CritAmber");
                    break;
                case string name when name.Contains("Azure"):
                    __instance.img_Critical.overrideSprite = ReadmeManager.GetReadmeSprites("CritAzure");
                    break;
                case string name when name.Contains("Crimson"):
                    __instance.img_Critical.overrideSprite = ReadmeManager.GetReadmeSprites("CritCrimson");
                    break;
                case string name when name.Contains("Indigo"):
                    __instance.img_Critical.overrideSprite = ReadmeManager.GetReadmeSprites("CritIndigo");
                    break;
                case string name when name.Contains("Scarlet"):
                    __instance.img_Critical.overrideSprite = ReadmeManager.GetReadmeSprites("CritScarlet");
                    break;
                case string name when name.Contains("Shamrock"):
                    __instance.img_Critical.overrideSprite = ReadmeManager.GetReadmeSprites("CritShamrock");
                    break;
                case string name when name.Contains("Violet"):
                    __instance.img_Critical.overrideSprite = ReadmeManager.GetReadmeSprites("CritViolet");
                    break;
            }
        }
        #endregion
    }
}
