using BattleUI;
using BattleUI.Information;
using HarmonyLib;
using MainUI;
using MainUI.VendingMachine;
using TMPro;
using UnityEngine;
using BattleUI.EvilStock;
using Dungeon.Map.UI;
using BattleUI.BattleUnit;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MainUI.BattleResult;
using UtilityUI;
using BattleUI.Operation;
using System;
using Dungeon.Shop;
using EGOGift;
using UnitInformation;
using UnityEngine.Playables;

namespace LimbusLocalizeRUS
{
    public static class TextUI
    {
        #region Login
        [HarmonyPatch(typeof(LoginSceneManager), nameof(LoginSceneManager.SetLoginInfo))]
        [HarmonyPostfix]
        private static void StartMenu_ClearAllCache_LineSpace(LoginSceneManager __instance)
        {
            __instance.btn_allCacheClear.GetComponentInChildren<TextMeshProUGUI>(true).lineSpacing = -60;
        }
        [HarmonyPatch(typeof(NetworkingUI), "Initialize")]
        [HarmonyPostfix]
        private static void Connecting_Label(NetworkingUI __instance)
        {
            Transform connection = __instance.transform.Find("connecting_background/tmp_connecting");
            if (connection != null)
            {
                connection.GetComponentInChildren<TextMeshProUGUI>(true).text = "ПОДКЛЮЧЕНИЕ";
                connection.GetComponentInChildren<TextMeshProUGUI>(true).font = Cyrillics.GetCyrillicFonts(0);
                connection.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = Cyrillics.GetCyrillicFonts(0).material;
                connection.GetComponentInChildren<RectTransform>(true).anchoredPosition = new Vector2(10, 0);
                connection.GetComponentInChildren<TextMeshProUGUI>(true).fontSize = 77f;
            }
        }
        [HarmonyPatch(typeof(UpdateMovieScreen), nameof(UpdateMovieScreen.SetDownLoadProgress))]
        [HarmonyPostfix]
        private static void Update_Loading(UpdateMovieScreen __instance)
        {
            TMP_FontAsset fontAsset = Resources.Load<TMP_FontAsset>("Font/BebasKai SDF");
            fontAsset.fallbackFontAssetTable.Add(Cyrillics.tmpcyrillicfonts[0]);
            if (__instance._loadingCategoryText != null)
            {
                TextMeshProUGUI now_l = GameObject.Find("[Canvas]DownloadScreen/UpdateMovieScreen/[Rect]LoadingUI/Text_NowLoading").GetComponentInChildren<TextMeshProUGUI>();
                now_l.text = "ЗАГРУЗКА...";
                now_l.m_fontAsset = fontAsset;
                now_l.fontMaterial = fontAsset.material;
                var self = __instance._loadingCategoryText;
                self.name = "ОБНОВЛЕНИЕ";
                self.text = self.text.Replace("Downloading", "ЗАГРУЗКА");
                self.text = self.text.Replace("UPDATE", "ОБНОВЛЕНИЯ");
                self.text = self.text.Replace("Sound", "ЗВУКОВ");
                self.text = self.text.Replace("Sprite", "СПРАЙТОВ");
                self.text = self.text.Replace("Retry ЗАГРУЗКА", "ПОПЫТКА ЗАГРУЗКИ");
                self.m_fontAsset = fontAsset;
                self.fontMaterial = fontAsset.material;
            }
        }
        #endregion

        #region Main Menu
        [HarmonyPatch(typeof(LowerControlUIPanel), nameof(LowerControlUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void ControlPanel_Init(LowerControlUIPanel __instance)
        {
            Transform level = __instance.transform.Find("[Rect]Pivot/[Rect]UserInfoUI/[Rect]Info/[Rect]UserInfo/[Tmpro]Lv");
            Transform No = __instance.transform.Find("[Rect]Pivot/[Rect]UserInfoUI/[Rect]Info/[Rect]UserInfo/[Tmpro]No");
            if (level != null)
            {
                level.GetComponentInChildren<TextMeshProUGUI>(true).text = "УР";
                No.GetComponentInChildren<TextMeshProUGUI>(true).text = "№";
            }
        }

        [HarmonyPatch(typeof(UserInfoCard), nameof(UserInfoCard.SetDataMainLobby))]
        [HarmonyPostfix]
        private static void Lobby_LevelID(UserInfoCard __instance)
        {
            TextMeshProUGUI lv = __instance.transform.Find("[Rect]AspectRatio/[Canvas]Info/[Text]LevelLabel").GetComponentInChildren<TextMeshProUGUI>(true);
            lv.text = "УР";

            TextMeshProUGUI no = __instance.transform.Find("[Rect]AspectRatio/[Canvas]Info/[Text]IdNumberLabel").GetComponentInChildren<TextMeshProUGUI>(true);
            no.text = "№";
        }

        [HarmonyPatch(typeof(NoticeUIPopup), nameof(NoticeUIPopup.Initialize))]
        [HarmonyPostfix]
        private static void NoticeNews(NoticeUIPopup __instance)
        {
            __instance.btn_systemNotice.GetComponentInChildren<TextMeshProUGUI>(true).lineSpacing = -30;
            __instance.btn_eventNotice.GetComponentInChildren<TextMeshProUGUI>(true).lineSpacing = -30;
        }
        [HarmonyPatch(typeof(StageUIPresenter), nameof(StageUIPresenter.Initialize))]
        [HarmonyPostfix]
        private static void StageUIPresenter_Init(StageUIPresenter __instance)
        {
            Transform district4 = __instance.transform.Find("[Rect]Active/[Script]PartAndChapterSelectionUIPanel/[Rect]Active/[Rect]Right/[Rect]Pivot/[Rect]StoryMap/[Mask]StoryMap/[Rect]ZoomPivot/[Image]MapBG/[Script]D_4/[Rect]TextData/[Tmpro]Area");
            Transform district10 = __instance.transform.Find("[Rect]Active/[Script]PartAndChapterSelectionUIPanel/[Rect]Active/[Rect]Right/[Rect]Pivot/[Rect]StoryMap/[Mask]StoryMap/[Rect]ZoomPivot/[Image]MapBG/[Script]J_10/[Rect]TextData/[Tmpro]Area");
            Transform district11 = __instance.transform.Find("[Rect]Active/[Script]PartAndChapterSelectionUIPanel/[Rect]Active/[Rect]Right/[Rect]Pivot/[Rect]StoryMap/[Mask]StoryMap/[Rect]ZoomPivot/[Image]MapBG/[Script]K_11/[Rect]TextData/[Tmpro]Area");
            Transform district21 = __instance.transform.Find("[Rect]Active/[Script]PartAndChapterSelectionUIPanel/[Rect]Active/[Rect]Right/[Rect]Pivot/[Rect]StoryMap/[Mask]StoryMap/[Rect]ZoomPivot/[Image]MapBG/[Script]U_21/[Rect]TextData/[Tmpro]Area");
            Transform district20 = __instance.transform.Find("[Rect]Active/[Script]PartAndChapterSelectionUIPanel/[Rect]Active/[Rect]Right/[Rect]Pivot/[Rect]StoryMap/[Mask]StoryMap/[Rect]ZoomPivot/[Image]MapBG/[Script]T_20/[Rect]TextData/[Tmpro]Area");
            Transform district16 = __instance.transform.Find("[Rect]Active/[Script]PartAndChapterSelectionUIPanel/[Rect]Active/[Rect]Right/[Rect]Pivot/[Rect]StoryMap/[Mask]StoryMap/[Rect]ZoomPivot/[Image]MapBG/[Script]P_16/[Rect]TextData/[Tmpro]Area");
            if (district4 != null)
            {
                district4.GetComponentInChildren<TextMeshProUGUI>(true).text = "4-й Район";
            }
            if (district10 != null)
            {
                district10.GetComponentInChildren<TextMeshProUGUI>(true).text = "10-й Район";
            }
            if (district11 != null)
            {
                district11.GetComponentInChildren<TextMeshProUGUI>(true).text = "11-й Район";
            }
            if (district21 != null)
            {
                district21.GetComponentInChildren<TextMeshProUGUI>(true).text = "21-й Район";
            }
            if (district20 != null)
            {
                district20.GetComponentInChildren<TextMeshProUGUI>(true).text = "20-й Район";
            }
            if (district16 != null)
            {
                district16.GetComponentInChildren<TextMeshProUGUI>(true).text = "16-й Район";
            }
        }
        [HarmonyPatch(typeof(StageInfoUI), nameof(StageInfoUI.SetDataOpen))]
        [HarmonyPostfix]
        private static void StageInfoUI_Init(StageInfoUI __instance)
        {
            Transform level = __instance.transform.Find("[UIPanel]StageInfoUIRenewal/[Rect]Pivot/[Rect]StageInfoStatus/[Script]ExclearCondition/[Tmpro]Desc (1)/[Image]RecommentLevelTitleFrame/[Tmpro]Lv");
            if (level != null)
            {
                level.GetComponentInChildren<TextMeshProUGUI>(true).text = level.GetComponentInChildren<TextMeshProUGUI>(true).text.Replace("Lv", "Ур");
            }
        }
        [HarmonyPatch(typeof(SubChapterScrollViewItem), nameof(SubChapterScrollViewItem.SetData))]
        [HarmonyPostfix]
        private static void SubChapterScrollViewItem_Init(SubChapterScrollViewItem __instance)
        {
            __instance.tmp_pageForBebaskai.text = __instance.tmp_pageForBebaskai.text.Replace("MINI", "МИНИ");
            string area_timeline = __instance.tmp_timeline.text;
            __instance.tmp_timeline.text = timeline(area_timeline);
        }
        [HarmonyPatch(typeof(ChapterSelectionUIPanel), nameof(ChapterSelectionUIPanel.StartMoveToRegion))]
        [HarmonyPostfix]
        private static void SubChapterTimeline_Data(ChapterSelectionUIPanel __instance)
        {
            string area_timeline = __instance.tmp_timeline.text;
            __instance.tmp_timeline.text = timeline(area_timeline);
        }

        private static string timeline(string timeline)
        {
            if (timeline.Contains('-'))
            {
                string[] parts = timeline.Split('-');
                int year = int.Parse(parts[0]);
                int month = int.Parse(parts[1]);
                timeline = $"{year}/{month}";
                return timeline;
            }
            else if (timeline.Contains('/'))
                return timeline;
            else
                return "[ДАННЫЕ УДАЛЕНЫ]";
        }

        [HarmonyPatch(typeof(StageStoryNodeSelectUI), nameof(StageStoryNodeSelectUI.Init))]
        [HarmonyPostfix]
        private static void NodeSelectUI(StageStoryNodeSelectUI __instance)
        {
            Transform episode_right = __instance.transform.Find("[Rect]Banner/[Image]PageTitle/[Text]PageTitle");
            episode_right.GetComponentInChildren<TextMeshProLanguageSetter>(true).enabled = false;
            episode_right.GetComponentInChildren<TextMeshProUGUI>(true).fontSize = 48;

            Transform episode_main = __instance.transform.Find("[Rect]Desc/[Image]Background/[Image]Panel/[Text]Episode");
            episode_main.GetComponentInChildren<TextMeshProUGUI>(true).text = "ЭПИЗОД";

            Transform enter = __instance.transform.Find("[Rect]Desc/[Image]Background/[Image]Panel/[Button]EnterStory/[Text]Enter");
            enter.GetComponentInChildren<TextMeshProLanguageSetter>(true).enabled = false;
            enter.GetComponentInChildren<TextMeshProUGUI>(true).text = "Прочесть";
        }

        [HarmonyPatch(typeof(StageStoryNodeSelectUI), nameof(StageStoryNodeSelectUI.OnStorySelect))]
        [HarmonyPostfix]
        private static void NodeSelectUI_RightCorner(StageStoryNodeSelectUI __instance)
        {
            if (__instance._leftStoryNumberText.text != null)
            {
                string[] parts = __instance._leftStoryNumberText.text.Split(' ');
                int number = int.Parse(parts[0]);
                string episode = parts[1];
                __instance._leftStoryNumberText.text = $"{number}-й {episode}";
            }
        }

        [HarmonyPatch(typeof(StorytheaterSelectNodeBase), nameof(StorytheaterSelectNodeBase.SetData))]
        [HarmonyPostfix]
        private static void NodeSelectUIBottom(StorytheaterSelectNodeBase __instance)
        {
            if (__instance._unSelectStoryText != null)
            {
                string[] unparts = __instance._unSelectStoryText.text.Split(' ');
                int unnumber = int.Parse(unparts[0]);
                string unepisode = unparts[1];
                __instance._unSelectStoryText.text = $"{unnumber}-й\n{unepisode}";

                __instance._unSelectStoryText.fontSize = 46;
                __instance._unSelectStoryText.lineSpacing = -30;
            }

            if (__instance._selectStoryText != null)
            {
                string[] parts = __instance._selectStoryText.text.Split(' ');
                int number = int.Parse(parts[0]);
                string episode = parts[1];
                __instance._selectStoryText.text = $"{number}-й\n{episode}";

                __instance._selectStoryText.fontSize = 46;
                __instance._selectStoryText.lineSpacing = -30;
            }
        }

        private static string getTimerD(int days)
        {
            int lastDigit = days % 10;
            int lastTwoDigits = days % 100;

            if (lastTwoDigits >= 11 && lastTwoDigits <= 19)
            {
                return "дней";
            }
            else if (lastDigit == 1)
            {
                return "день";
            }
            else if (lastDigit >= 2 && lastDigit <= 4)
            {
                return "дня";
            }
            else
            {
                return "дней";
            }
        }

        private static string getTimerH(int hours)
        {
            int lastDigit = hours % 10;
            int lastTwoDigits = hours % 100;

            if (lastTwoDigits >= 11 && lastTwoDigits <= 19)
            {
                return "часов";
            }
            else if (lastDigit == 1)
            {
                return "час";
            }
            else if (lastDigit >= 2 && lastDigit <= 4)
            {
                return "часа";
            }
            else
            {
                return "часов";
            }
        }

        private static string getTimerM(int minutes)
        {
            int lastDigit = minutes % 10;
            int lastTwoDigits = minutes % 100;
            if (lastTwoDigits >= 11 && lastTwoDigits <= 19)
            {
                return "минут";
            }
            else if (lastDigit == 1)
            {
                return "минута";
            }
            else if (lastDigit >= 2 && lastTwoDigits <= 4)
            {
                return "минуты";
            }
            else
            {
                return "минут";
            }
        }
        [HarmonyPatch(typeof(EventTimerUI), nameof(EventTimerUI.UpdateRemainEventTime))]
        [HarmonyPostfix]
        private static void EventTimerUI_Init(EventTimerUI __instance)
        {
            __instance.tmp_remainingTime.name = "EVENT!";
            string pattern = @"(\d+ дней)(\d+ часов)";
            Match match = Regex.Match(__instance.tmp_remainingTime.text, pattern);
            if (match.Success)
            {
                int days = int.Parse(match.Groups[1].Value.Split(' ')[0]);
                int hours = int.Parse(match.Groups[2].Value.Split(' ')[0]);
                string dayWord = getTimerD(days);
                string hourWord = getTimerH(hours);
                __instance.tmp_remainingTime.text = Regex.Replace(__instance.tmp_remainingTime.text, pattern, days + " " + dayWord + " " + hours + " " + hourWord);
            }
            string lastPattern = @"(\d+ часов)(\d+ минут)";
            Match lastMatch = Regex.Match(__instance.tmp_remainingTime.text, lastPattern);
            if (lastMatch.Success)
            {
                int hours = int.Parse(lastMatch.Groups[1].Value.Split(' ')[0]);
                int minutes = int.Parse(lastMatch.Groups[2].Value.Split(' ')[0]);
                string hourWord = getTimerH(hours);
                string minuteWord = getTimerM(minutes);
                __instance.tmp_remainingTime.text = Regex.Replace(__instance.tmp_remainingTime.text, lastPattern, hours + " " + hourWord + " " + minutes + " " + minuteWord);
            }
            __instance.tmp_remainingTime.name = "EVENT!";
            string dayPattern = @"(\d+ дней)";
            Match dayMatch = Regex.Match(__instance.tmp_remainingTime.text, dayPattern);
            if (dayMatch.Success)
            {
                int days = int.Parse(dayMatch.Groups[1].Value.Split(' ')[0]);
                string dayWord = getTimerD(days);
                __instance.tmp_remainingTime.text = Regex.Replace(__instance.tmp_remainingTime.text, dayPattern, days + " " + dayWord);
            }
            __instance.tmp_remainingTime.name = "EVENT!";
            string hourPattern = @"(\d+ часов)";
            Match hourMatch = Regex.Match(__instance.tmp_remainingTime.text, hourPattern);
            if (hourMatch.Success)
            {
                int hours = int.Parse(hourMatch.Groups[1].Value.Split(' ')[0]);
                string hourWord = getTimerH(hours);
                __instance.tmp_remainingTime.text = Regex.Replace(__instance.tmp_remainingTime.text, hourPattern, hours + " " + hourWord);
            }
            __instance.tmp_remainingTime.name = "EVENT!";
            string minutePattern = @"(\d+ минут)";
            Match minuteMatch = Regex.Match(__instance.tmp_remainingTime.text, minutePattern);
            if (minuteMatch.Success)
            {
                int minutes = int.Parse(minuteMatch.Groups[1].Value.Split(' ')[0]);
                string minuteWord = getTimerM(minutes);
                __instance.tmp_remainingTime.text = Regex.Replace(__instance.tmp_remainingTime.text, minutePattern, minutes + " " + minuteWord);
            }
        }
        [HarmonyPatch(typeof(RewatchingStageStoryButton), nameof(RewatchingStageStoryButton.SetData))]
        [HarmonyPostfix]
        private static void Rewatch_EpisodeLabel(RewatchingStageStoryButton __instance)
        {
            TextMeshProUGUI episode = __instance.transform.Find("[LayoutGroup]Episode/[Text]Episode").GetComponentInChildren<TextMeshProUGUI>(true);
            if (episode != null)
            {
                episode.text = "ЭПИЗОД";
            }
        }
        [HarmonyPatch(typeof(UnitInformationPopupButton), nameof(UnitInformationPopupButton.UpdateContentText))]
        [HarmonyPostfix]
        private static void Uptie_Label_OneLined(UnitInformationPopupButton __instance)
        {
            if (__instance.tmp_content != null)
            {
                __instance.tmp_content.text = __instance.tmp_content.text.Replace("Усиление ", "Усиление\n");
                __instance.tmp_content.fontSizeMax = 30;
            }
        }
        #endregion

        #region Friends
        [HarmonyPatch(typeof(UserInfoUIPopup), nameof(UserInfoUIPopup.Open))]
        [HarmonyPostfix]
        private static void TicketInfoPopup(UserInfoUIPopup __instance)
        {
            Transform ego = __instance._userinfoTicketCustomPopup._egoBackgroundBtn.transform.Find("[Text]EGO");
            Transform bg = __instance._userinfoTicketCustomPopup._egoBackgroundBtn.transform.Find("[Text]BG");
            ego.GetComponentInChildren<TextMeshProUGUI>(true).text = "<size=40>Фон\nдля</size>";
            ego.GetComponentInChildren<TextMeshProUGUI>(true).lineSpacing = -20;
            bg.GetComponentInChildren<UITextDataLoader>(true).enabled = false;
            bg.GetComponentInChildren<TextMeshProUGUI>(true).text = "<size=70>ЭГО</size>";
        }
        [HarmonyPatch(typeof(UserInfoTicketItem), nameof(UserInfoTicketItem.SetTag))]
        [HarmonyPostfix]
        private static void TicketChangePopup(UserInfoTicketItem __instance)
        {
            __instance._using_choosingText.text = "Выбран";
            __instance._using_choosingText.lineSpacing = -20;
        }
        [HarmonyPatch(typeof(UserInfoRepChangePopup), nameof(UserInfoRepChangePopup.SetData))]
        [HarmonyPostfix]
        private static void UserInfo_EgoLabelChanger(UserInfoRepChangePopup __instance)
        {
            TextMeshProUGUI ego_label = __instance._egoBtn.transform.Find("[Text]EGO").GetComponentInChildren<TextMeshProUGUI>(true);
            ego_label.text = "ЭГО";
        }
        #endregion

        #region Settings
        [HarmonyPatch(typeof(MirrorDungeonProgressPopup), nameof(MirrorDungeonProgressPopup.Open))]
        [HarmonyPostfix]
        private static void MirrorDungeonProgressPopup_Ini34t(MirrorDungeonProgressPopup __instance)
        {
            __instance.btn_continue.GetComponentInChildren<UIPopupButtonLight>().tmp_text.fontSizeMax = 28;
            __instance.btn_gameSetting.GetComponentInChildren<UIPopupButtonLight>().tmp_text.fontSizeMax = 28;
            __instance.btn_giveUp.GetComponentInChildren<UIPopupButtonLight>().tmp_text.fontSizeMax = 28;
            __instance.btn_returnToMain.GetComponentInChildren<UIPopupButtonLight>().tmp_text.fontSizeMax = 28;
        }

        [HarmonyPatch(typeof(SettingsPopup), nameof(SettingsPopup.Initialize))]
        [HarmonyPostfix]
        private static void SettingsSound_Init(SettingsPopup __instance)
        {
            Transform bgm = __instance.transform.Find("PanelGroup/PopupBase/Container/[Rect]Contents/Viewport/Content/[Script]SettingsSound/SettingsSoundBGM/[Text]Label");
            if (bgm != null)
                bgm.GetComponentInChildren<TextMeshProUGUI>(true).text = "Фоновая музыка";
        }
        #endregion

        #region Luxcavation
        [HarmonyPatch(typeof(ExpDungeonUIPanel), nameof(ExpDungeonUIPanel.SetDataOpen))]
        [HarmonyPostfix]
        private static void ExpDungeonItem_Init(ExpDungeonUIPanel __instance)
        {
            foreach (ExpDungeonItem item in __instance._dungeonItemList)
            {
                item.tmp_title.fontStyle = FontStyles.Normal | FontStyles.SmallCaps;
                item.tmp_level.text = item.tmp_level.text.Replace("Lv", "Ур.");
                Transform label = item.tmp_level.transform.parent.parent.Find("[Text]StageLabel");
                label.GetComponentInChildren<TextMeshProUGUI>().text = "ЭТАП";
            }
        }

        [HarmonyPatch(typeof(ThreadDungeonSelectStageButton), nameof(ThreadDungeonSelectStageButton.SetData))]
        [HarmonyPostfix]
        private static void ThreadDungeonSelectStageButton_Init(ThreadDungeonSelectStageButton __instance)
        {
            __instance.tmp_level.text = __instance.tmp_level.text.Replace("Lv", "Ур.");
            __instance.tmp_level.fontSize = 60;
        }

        [HarmonyPatch(typeof(DungeonRewardPreviewUI), nameof(DungeonRewardPreviewUI.SetData))]
        [HarmonyPostfix]
        private static void ThreadDungeonSelectStageButton_Init(DungeonRewardPreviewUI __instance)
        {
            __instance.transform.Find("[Text]Label").GetComponentInChildren<TextMeshProUGUI>().fontSize = 43;
        }

        [HarmonyPatch(typeof(RemainTimeText), nameof(RemainTimeText.SetRemainNextDay))]
        [HarmonyPostfix]
        private static void ThreadDungeonRemainingTime(RemainTimeText __instance)
        {
            if (__instance.tmp_timer.text.Contains("Часов:"))
            {
                __instance.tmp_timer.text = __instance.tmp_timer.text.Replace("Часов:", "");
                string[] parts = __instance.tmp_timer.text.Split(' ');
                int number = int.Parse(parts[0]);
                string leftWord = "";
                if (getTimerH(number).EndsWith("час"))
                    leftWord = "Остался";
                else leftWord = "Осталось";
                __instance.tmp_timer.text = $"{leftWord} {number} {getTimerH(number)}";
            }
            if (__instance.tmp_timer.text.Contains("Минут:"))
            {
                __instance.tmp_timer.text = __instance.tmp_timer.text.Replace("Минут:", "");
                string[] parts = __instance.tmp_timer.text.Split(' ');
                int number = int.Parse(parts[0]);
                string leftWord = "";
                if (getTimerM(number).EndsWith("минута"))
                    leftWord = "Осталась";
                else leftWord = "Осталось";
                __instance.tmp_timer.text = $"{leftWord} {number} {getTimerM(number)}";
            }
        }
        [HarmonyPatch(typeof(ExpDungeonItemTab), nameof(ExpDungeonItemTab.SetData))]
        [HarmonyPostfix]
        private static void ExpDungeonItemTab_Init(ExpDungeonItemTab __instance)
        {
            switch (__instance.tmp_main.text)
            {
                case string when __instance.tmp_main.text.StartsWith("MON"):
                    __instance.tmp_main.text = "<line-height=100%>ПН\nВТ";
                    break;
                case string when __instance.tmp_main.text.StartsWith("WED"):
                    __instance.tmp_main.text = "<line-height=100%>СР\nЧТ";
                    break;
                case string when __instance.tmp_main.text.StartsWith("FRI"):
                    __instance.tmp_main.text = "<line-height=100%>ПТ\nСБ";
                    break;
            }
            return;
        }
        #endregion

        #region Mirror Dungeons
        [HarmonyPatch(typeof(MirrorDungeonThemeListUIPopup), nameof(MirrorDungeonThemeListUIPopup.OpenAnimationEndEvent))]
        [HarmonyPrefix]
        private static void MirrorDungeonTheme_DifficultyLabels(MirrorDungeonThemeListUIPopup __instance)
        {
            __instance.tmp_normalDifficulty.text = "Обычное подземелье";
            __instance.tmp_hardDifficulty.text = "Усложнённое подземелье";
        }
        [HarmonyPatch(typeof(MirrorDungeonEntranceUIObject),  nameof(MirrorDungeonEntranceUIObject.UpdateUI))]
        [HarmonyPostfix]
        private static void MirrorDungeon_DungeonSmallLabel(MirrorDungeonEntranceUIObject __instance)
        {
            TextMeshProUGUI dungeon = __instance._onObject.transform.Find("[Tmpro]DungeonLabel").GetComponentInChildren<TextMeshProUGUI>();
            if (dungeon != null)
            {
                dungeon.text = "ПОДЗЕМЕЛЬЕ";
            }
        }
        [HarmonyPatch(typeof(EgoGiftTooltip), nameof(EgoGiftTooltip.SetUpDataAndOpen))]
        [HarmonyPostfix]
        private static void EgoGift_ToolTipLabel(EgoGiftTooltip __instance)
        {
            TextMeshProUGUI tooltip = __instance.transform.Find("TitleLabelRect/tmp_title").GetComponentInChildren<TextMeshProUGUI>(true);
            if (tooltip != null)
            {
                tooltip.text = "ЭГО Дар";
            }
        }
        [HarmonyPatch(typeof(StageInfoDisplayRenewal), nameof(StageInfoDisplayRenewal.SetData))]
        [HarmonyPostfix]
        private static void StageInfo_Name(StageInfoDisplayRenewal __instance)
        {
            __instance.tmp_title.characterSpacing = 2;
            __instance.tmp_title.lineSpacing = -20;
        }
        [HarmonyPatch(typeof(MirrorDungeonRewardPopup_Season4), nameof(MirrorDungeonRewardPopup_Season4.SetPopup))]
        [HarmonyPostfix]
        private static void MirrorDungeonRewardPopup_Season42_Name(MirrorDungeonRewardPopup_Season4 __instance)
        {
            __instance.tmp_userExp.transform.Find("[Text]UserExpLabel").GetComponentInChildren<TextMeshProUGUI>().text = "ОПЫТ";
        }
        [HarmonyPatch(typeof(EgoGiftCombineResultIndicator), nameof(EgoGiftCombineResultIndicator.Init))]
        [HarmonyPostfix]
        private static void EgoGiftCombineResultIndicator_Res(EgoGiftCombineResultIndicator __instance)
        {
            __instance.tmp_label.text = "Ожидаемый\nрезультат";
            __instance.tmp_label.GetComponentInChildren<RectTransform>().anchoredPosition = new Vector2(0, 15);
        }
        #endregion

        #region Choice UI
        [HarmonyPatch(typeof(PersonalityChoiceEventJudgementTitleUI), nameof(PersonalityChoiceEventJudgementTitleUI.SetEventJudgementTitle))]
        [HarmonyPostfix]
        private static void SinAffinity_UI(PersonalityChoiceEventJudgementTitleUI __instance)
        {
            __instance.tmp_requiredAttributes.lineSpacing = -30;
            __instance.tmp_judgementComparisonType.lineSpacing = -30;

            __instance.tmp_requiredAttributes.text = __instance.tmp_requiredAttributes.text.Replace("/", " / ").Replace("< / ", "</");
        }
        #endregion

        #region Vending Machine
        [HarmonyPatch(typeof(VendingMachineUIPanel), nameof(VendingMachineUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void VendingMachineUIPanel_Init(VendingMachineUIPanel __instance)
        {
            __instance.tmp_announcerGroupPage.fontSizeMax = 30;
            __instance.tmp_announcerGroupPage.fontSizeMin = 30;
            __instance.tmp_eventGroupPage.fontSizeMax = 38;
            __instance.tmp_gachaGroupPage.fontSizeMax = 38;
            __instance.tmp_passGroupPage.fontSizeMax = 38;
            __instance.tmp_personalityGroupPage.fontSizeMax = 38;
            __instance.btn_egoGroupPage.transform.Find("Tmp_EGO").GetComponentInChildren<TextMeshProUGUI>().text = "ЭГО";
        }
        [HarmonyPatch(typeof(VendingMachineUIPersonalityGoodsSlot), nameof(VendingMachineUIPersonalityGoodsSlot.SetData))]
        [HarmonyPostfix]
        private static void VendingMachineUIPersonalityGoodsSlot_Init(VendingMachineUIPersonalityGoodsSlot __instance)
        {
            if (__instance._purchaseImpossibleLable.tmp_main == null)
                return;
            string[] parts = __instance._purchaseImpossibleLable.tmp_main.text.Split(' ');
            string[] parts_edit = parts[5].Split('.');
            if (Int32.Parse(parts_edit[1]) < 10)
                parts_edit[1] = $"0{parts_edit[1]}";
            __instance._purchaseImpossibleLable.tmp_main.text = $"Можно приобрести за эгосколки после 06:00 {parts_edit[0]}.{parts_edit[1]} (МСК).";
        }
        [HarmonyPatch(typeof(VendingMachineUIEgoGoodsSlot), nameof(VendingMachineUIEgoGoodsSlot.SetData))]
        [HarmonyPostfix]
        private static void VendingMachineUIEgoGoodsSlot_Init(VendingMachineUIEgoGoodsSlot __instance)
        {
            if (__instance._purchaseImpossibleLable.tmp_main == null)
                return;
            string[] parts = __instance._purchaseImpossibleLable.tmp_main.text.Split(' ');
            string[] parts_edit = parts[5].Split('.');
            if (Int32.Parse(parts_edit[1]) < 10)
                parts_edit[1] = $"0{parts_edit[1]}";
            __instance._purchaseImpossibleLable.tmp_main.text = $"Можно приобрести за эгосколки после 06:00 {parts_edit[0]}.{parts_edit[1]} (МСК).";
        }
        #endregion

        #region Formation UI
        [HarmonyPatch(typeof(FormationSwitchablePersonalityUIPanel), nameof(FormationSwitchablePersonalityUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void FormationSwitchablePersonalityUIPanel_Init(FormationSwitchablePersonalityUIPanel __instance)
        {
            Transform id_mainui = __instance.transform.Find("[Script]RightPanel/[Script]FormationEgoList/[Text]Personality_Label");
            Transform ego_mainui_1 = __instance.transform.Find("[Script]RightPanel/[Script]FormationEgoList/[Text]Ego_Label");
            Transform ego_mainui_2 = __instance.transform.Find("[Script]ListTabRoot/[Layout]Tabs/[Toggle]Ego/[Text]E.G.O");
            if (ego_mainui_1 != null)
            {
                ego_mainui_1.GetComponentInChildren<TextMeshProUGUI>(true).text = "ЭГО";
                ego_mainui_2.GetComponentInChildren<TextMeshProUGUI>(true).text = "ЭГО";
            }
            if (id_mainui != null)
            {
                id_mainui.GetComponentInChildren<UITextDataLoader>().enabled = false;
                id_mainui.GetComponentInChildren<TextMeshProUGUI>(true).richText = false;
                id_mainui.GetComponentInChildren<TextMeshProUGUI>(true).autoSizeTextContainer = true;
                id_mainui.GetComponentInChildren<TextMeshProUGUI>(true).text = "Идентичность";
            }
        }
        [HarmonyPatch(typeof(UnitInformationTabButton), nameof(UnitInformationTabButton.SetData))]
        [HarmonyPostfix]
        private static void Skills(UnitInformationTabButton __instance)
        {
            __instance.tmp_tabName.text = __instance.tmp_tabName.text.Replace("Атака", "Навыки");
        }
        [HarmonyPatch(typeof(FormationUIPanel), nameof(FormationUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void FormationUIPanel_Init(FormationUIPanel __instance)
        {
            //MAIN UI HOVER EGO TEXT
            Transform ego = __instance.transform.Find("[Rect]MainPanel/[Rect]Contents/[Rect]Personalities/PersonalityDetail/[Button]Main/[Rect]Select/[Button]EGO/[Text]EGO");
            Transform ego_hover = __instance.transform.Find("[Rect]MainPanel/[Rect]Contents/[Rect]Personalities/PersonalityDetail/[Button]Main/[Rect]Select/[Button]EGO/[Text]EGO/[Text]EGO_Highlight");
            if (ego != null)
            {
                ego.GetComponentInChildren<TextMeshProUGUI>(true).text = "ЭГО";
                ego_hover.GetComponentInChildren<TextMeshProUGUI>(true).text = "ЭГО";
            }
            // TEAMS
            GameObject teams = GameObject.Find("[Canvas]RatioMainUI/[Rect]PanelRoot/[UIPanel]PersonalityFormationUIPanel(Clone)/[Rect]LeftObjects/[Rect]DeckSettings/[Rect]Contents/[Text]DeckTitle");
            if (teams != null)
            {
                teams.GetComponentInChildren<TextMeshProUGUI>(true).name = "КОМАНДЫ";
                teams.GetComponentInChildren<UITextDataLoader>(true).enabled = false;
                teams.GetComponentInChildren<TextMeshProUGUI>(true).text = "КОМАНДЫ";
            }
            //_waitParticipate
        }
        [HarmonyPatch(typeof(FormationUIPanel), nameof(FormationUIPanel.UpdateCurrentFormation))]
        [HarmonyPostfix]
        private static void FormationUIPanel_Update(FormationUIPanel __instance)
        {
            var backup = __instance._waitParticipate.tmp_text.transform.parent.Find("").GetComponentInChildren<TextMeshProUGUI>();
            if (backup != null)
            {
                backup.m_fontAsset = __instance._waitParticipate.tmp_text.font;
                backup.fontMaterial = __instance._waitParticipate.tmp_text.fontMaterial;
                backup.m_fontMaterial = __instance._waitParticipate.tmp_text.fontMaterial;
                backup.fontSize = 45;
                backup.text = "В запасе";
                backup.GetComponentInChildren<RectTransform>().anchoredPosition = new Vector2(65, -5);
            }
        }
        [HarmonyPatch(typeof(FormationUIPanel), nameof(FormationUIPanel.SetDataOpen))]
        [HarmonyPostfix]
        private static void TeamNameChanger(FormationUIPanel __instance)
        {
            //MAIN UI # TO № CHANGER
            TextMeshProUGUI team_name = __instance.transform.Find("[Rect]MainPanel/ChangeFormationNameTitle/[Image]Mask/TilteText").GetComponentInChildren<TextMeshProUGUI>(true);
            if (team_name != null)
            {
                team_name.text = team_name.text.Replace('#', '№');
            }
        }
        [HarmonyPatch(typeof(PersonalitySlotSkillInfoList), nameof(PersonalitySlotSkillInfoList.SetData))]
        [HarmonyPostfix]
        private static void PersonalitySlotSkillInfoList_Init(PersonalitySlotSkillInfoList __instance)
        {
            Transform guard = __instance._guardItem.transform.Find("Text (TMP)");
            if (guard != null)
            {
                guard.GetComponentInChildren<TextMeshProUGUI>(true).text = "ЗАЩ.";
            }
        }
        [HarmonyPatch(typeof(PersonalityDetailButton), nameof(PersonalityDetailButton.SetUIMode))]
        [HarmonyPostfix]
        private static void FormationSecondChance_Init(PersonalityDetailButton __instance)
        {
            __instance._skillText._text.fontStyle = FontStyles.Normal | FontStyles.SmallCaps;
            __instance.tmp_skillHighlight.fontStyle = FontStyles.Normal | FontStyles.SmallCaps;
            __instance._skillText._text.text = "Навыки";
            __instance.tmp_skillHighlight.text = "Навыки";

            __instance._egoText._text.text = "ЭГО";
            __instance.tmp_egoHighlight.text = "ЭГО";
        }
        [HarmonyPatch(typeof(PersonalityDetailButton), nameof(PersonalityDetailButton._Initialize_b__22_3))]
        [HarmonyPostfix]
        private static void FormationUwa_Init(PersonalityDetailButton __instance)
        {
            __instance._skillText._text.text = "Навыки";
            __instance.tmp_skillHighlight.text = "Навыки";

            __instance._egoText._text.text = "ЭГО";
            __instance.tmp_egoHighlight.text = "ЭГО";
        }
        [HarmonyPatch(typeof(FormationUIDeckToggle), nameof(FormationUIDeckToggle.SetText))]
        [HarmonyPostfix]
        private static void FormationUIDeckToggle_Init(FormationUIDeckToggle __instance)
        {
            __instance.tmp_title.text = __instance.tmp_title.text.Replace("#", "№");
        }
        [HarmonyPatch(typeof(FormationPersonalityUI), nameof(FormationPersonalityUI.SetNonClickable))]
        [HarmonyPostfix]
        private static void FormationLabel_Init(FormationPersonalityUI __instance)
        {
            __instance._textInfo.txt_level.text = __instance._textInfo.txt_level.text.Replace("Lv.", "Ур.");
            var sinnerName = __instance.transform.name.Substring(25);
            __instance._upperPortraitEventUI.tmp_text.text = Declension_sinners(sinnerName, __instance._upperPortraitEventUI.tmp_text.text);
            __instance._upperPortraitEventUI.tmp_text.lineSpacing = -30;
            if (__instance._textInfo.txt_groupName.text.StartsWith("Корректир"))
            {
                __instance._textInfo.txt_groupName.fontSize = 30;
            }
        }
        [HarmonyPatch(typeof(UnitInformationSkillContent), nameof(UnitInformationSkillContent.SetData))]
        [HarmonyPostfix]
        private static void FormationLabel_SkillWeight(UnitInformationSkillContent __instance)
        {
            __instance.tmp_skillTargetNum.GetComponentInChildren<RectTransform>().anchoredPosition = new Vector2(17, -4);
            TextMeshProUGUI weight =__instance.transform.Find("[Rect]SkillInfoUpper/[Text]TargetNumLabel").GetComponent<TextMeshProUGUI>();
            if (weight != null)
            {
                weight.rectTransform.sizeDelta = new Vector2(280, 60);
                weight.m_fontSizeBase = 80f;
                weight.lineSpacing = 18f;
            }
        }
        [HarmonyPatch(typeof(EGOSelectUISkillInfo), nameof(EGOSelectUISkillInfo.SetData))]
        [HarmonyPostfix]
        private static void EGOSelect_SkillWeight(EGOSelectUISkillInfo __instance)
        {
            __instance._egoSkillTargetNumText.GetComponentInChildren<RectTransform>().anchoredPosition = new Vector2 (8, -20);
            __instance._egoSkillTargetNumText.enableWordWrapping = true;
            __instance._egoSkillTargetNumText.fontSize = 30f;
            TextMeshProUGUI weight = __instance.transform.Find("Scroll View/Viewport/Content/[Rect]Skill_CoinInfo/[Rect]SkillData/[Text]TargetNumLabel").GetComponent<TextMeshProUGUI> ();
            if (weight != null)
            {
                weight.rectTransform.anchoredPosition = new Vector2(-100, -50);
                weight.rectTransform.sizeDelta = new Vector2(180, 70);
                weight.m_fontSizeBase = 100f;
                weight.lineSpacing = 20f;
            }
        }
        [HarmonyPatch(typeof(Formation_UpperEventSlotUI), nameof(Formation_UpperEventSlotUI.SetData))]
        [HarmonyPostfix]
        private static void FormationLabel_Upper(Formation_UpperEventSlotUI __instance)
        {
            var sinnerName = __instance.transform.parent.name.Substring(25);
            __instance.tmp_text.text = Declension_sinners(sinnerName, __instance.tmp_text.text);
            __instance.tmp_text.lineSpacing = -30;
        }

        [HarmonyPatch(typeof(Formation_SelectParticipatedEventSlotUI), nameof(Formation_SelectParticipatedEventSlotUI.SetIsFull))]
        [HarmonyPostfix]
        private static void Formation_SelectParticipatedEventSlotUI_Abstain(Formation_SelectParticipatedEventSlotUI __instance)
        {
            var sinnerName = __instance.transform.parent.name.Substring(25);
            __instance._text.enabled = false;
            __instance._materialSetter._text.text = Declension_sinners(sinnerName, __instance._materialSetter._text.text);
        }
        static string Declension_sinners(string sinner, string replacing)
        {
            switch (sinner)
            {
                case "Faust" or "Donqui" or "Ryoshu" or "Ishmael" or "Rodion" or "Outis":
                    return replacing.Replace("ка", "цу");
            }
            return replacing;
        }

        [HarmonyPatch(typeof(FormationPersonalityUI_Label), nameof(FormationPersonalityUI_Label.Reload))]
        [HarmonyPostfix]
        private static void FormationLabel(FormationPersonalityUI_Label __instance)
        {
            String sinner = __instance.tmp_text.transform.parent.parent.parent.parent.name.Substring(25);

            if (__instance.tmp_text.text.Contains("Необходим"))
            {
                switch (sinner)
                {
                    case "Faust" or "Donqui" or "Ryoshu" or "Ishmael" or "Rodion" or "Outis":
                        __instance.tmp_text.text = "<voffset=-0.25em><size=56><cspace=-1px>Необходима</cspace></size></voffset>";
                        break;
                }
            }

            if (__instance.tmp_text.text.Contains("Ключевой"))
            {
                switch (sinner)
                {
                    case "Faust" or "Donqui" or "Ryoshu" or "Ishmael" or "Rodion" or "Outis":
                        __instance.tmp_text.text = "<voffset=-0.25em><cspace=-1px>Ключевая</cspace></voffset>";
                        break;
                }
            }

            if (__instance.tmp_text.text.Contains("Недоступен"))
            {
                switch (sinner)
                {
                    case "Faust" or "Donqui" or "Ryoshu" or "Ishmael" or "Rodion" or "Outis":
                        __instance.tmp_text.text = "<voffset=-0.25em><cspace=-1px>Недоступна</cspace></voffset>";
                        break;
                }
            }

            __instance.tmp_text.text = __instance.tmp_text.text.Replace("CHANGED", "<voffset=-0.25em><cspace=-1px>ЗАМЕНА</cspace></voffset>");
        }

        [HarmonyPatch(typeof(FormationUIDeckToggle), nameof(FormationUIDeckToggle.UpdateScrollAnimation))]
        [HarmonyPostfix]
        private static void TeamName(FormationUIDeckToggle __instance)
        {
            __instance.tmp_title.text = __instance.tmp_title.text.Replace("#", "№");
        }

        [HarmonyPatch(typeof(MirrorDungeonShopItemSlotManager), nameof(MirrorDungeonShopItemSlotManager.SetDataOpen))]
        [HarmonyPostfix]
        private static void MirrorDungeonShopItemSlotManager_Init(MirrorDungeonShopItemSlotManager __instance)
        {
            if (__instance._changeSkillItemList != null)
            {
                var sinner = __instance._changeSkillItemList[0].tmp_title.text.Substring(21);
                __instance._changeSkillItemList[0].tmp_title.text = "Изменение навыка для " + NameChanger(sinner);
            }
        }
        [HarmonyPatch(typeof(MirrorDungeonShopSkillChangePopupUI), nameof(MirrorDungeonShopSkillChangePopupUI.SetDataAndOpen))]
        [HarmonyPostfix]
        private static void MirrorDungeonShopSkillChangePopupUI_SetData(MirrorDungeonShopSkillChangePopupUI __instance)
        {
            var sinner = __instance.tmp_popupLabel.text.Substring(21);
            __instance.tmp_popupLabel.text = "Изменение навыка для " + NameChanger(sinner);
        }
        public static string NameChanger(string sinnerName)
        {
            switch (sinnerName)
            {
                case "И Сан":
                    return "И Сана";
                case "Хитклиф":
                    return "Хитклифа";
                case "Родя":
                    return "Роди";
                case "Грегор":
                    return "Грегора";
                case "Синклер":
                    return "Синклера";
            }
            return sinnerName;
        }
        #endregion

        #region LV
        [HarmonyPatch(typeof(PlayerLevelUpUIPopup), nameof(PlayerLevelUpUIPopup.Initialize))]
        [HarmonyPostfix]
        private static void PlayerLevelUpUIPopup_Init(PlayerLevelUpUIPopup __instance)
        {
            List<TextMeshProUGUI> lvls = new List<TextMeshProUGUI> { __instance.tmp_level_after, __instance.tmp_level_before };
            foreach (var lvl in lvls)
            {
                lvl.transform.parent.Find("tmp_LV").GetComponentInChildren<TextMeshProUGUI>().text = "УР";
            }
        }
        [HarmonyPatch(typeof(UnitInformationLevelUI), nameof(UnitInformationLevelUI.SetLevelText))]
        [HarmonyPostfix]
        private static void UnitInformationLevelUI_Init(UnitInformationLevelUI __instance, ref string levelText)
        {
            __instance.rect_levelTagText.GetComponentInChildren<TextMeshProUGUI>().text = "УР";
        }
        [HarmonyPatch(typeof(PersonalityLevelUpUIPopup), nameof(PersonalityLevelUpUIPopup.SetData))]
        [HarmonyPostfix]
        private static void PersonalityLevelUpUIPopup_Init(PersonalityLevelUpUIPopup __instance)
        {
            __instance._expUI.tmp_expCurrent.transform.parent.Find("[TMPro]EXP Label").GetComponentInChildren<TextMeshProUGUI>().text = "ОПЫТ";
            __instance._expUI.tmp_levelAfter.transform.parent.parent.parent.Find("[TMPro]LevelLabel").GetComponentInChildren<TextMeshProUGUI>().text = "УР";
            __instance._ingradientsUI.transform.Find("[Image]Status (LabelBackground)/[TMPro]Status (Label)").GetComponentInChildren<TextMeshProUGUI>().fontSizeMax = 28;
        }
        [HarmonyPatch(typeof(ExpGaugeUI), nameof(ExpGaugeUI.SetLevelText))]
        [HarmonyPostfix]
        private static void ExpGaugeUI_Init(ExpGaugeUI __instance)
        {
            if (__instance.tmp_levelText.transform.parent.Find("Tmp_LV_sign") == null)
                return;
            __instance.tmp_levelText.transform.parent.Find("Tmp_LV_sign").GetComponentInChildren<TextMeshProUGUI>().text = "УР";
        }
        [HarmonyPatch(typeof(FormationSwitchablePersonalityUIScrollViewItem), nameof(FormationSwitchablePersonalityUIScrollViewItem.SetData))]
        [HarmonyPostfix]
        private static void FormationSwitchablePersonalityUIScrollViewItemLevel_Init(FormationSwitchablePersonalityUIScrollViewItem __instance)
        {
            __instance.txt_level.text = __instance.txt_level.text.Replace("Lv", "Ур.");
            if (__instance._participatedObject != null)
            {
                TextMeshProUGUI participated = __instance._participatedObject.transform.Find("[Text]Label").GetComponentInChildren<TextMeshProUGUI>();
                participated.fontSize = 56;
            }
        }
        [HarmonyPatch(typeof(FormationSwitchableSupporterPersonalityUIScrollViewItem), nameof(FormationSwitchableSupporterPersonalityUIScrollViewItem.SetData))]
        [HarmonyPostfix]
        private static void FormationSwitchableSupporterPersonalityUIScrollViewItem_Init(FormationSwitchableSupporterPersonalityUIScrollViewItem __instance)
        {
            __instance.txt_level.text = __instance.txt_level.text.Replace("Lv", "Ур.");
        }
        [HarmonyPatch(typeof(UserInfoCard), nameof(UserInfoCard.SetData))]
        [HarmonyPostfix]
        private static void UserInfoCard_Init(UserInfoCard __instance)
        {
            TextMeshProUGUI lvl = __instance.tmp_level.transform.parent.Find("[Text]LevelLabel").GetComponentInChildren<TextMeshProUGUI>();
            LevelLabel(lvl);
            TextMeshProUGUI num = __instance.tmp_level.transform.parent.Find("[Text]IdNumberLabel").GetComponentInChildren<TextMeshProUGUI>();
            NumberLabel(num);
        }
        [HarmonyPatch(typeof(UserInfoFriednsSlot), nameof(UserInfoFriednsSlot.SetData))]
        [HarmonyPostfix]
        private static void UserInfoFriednsSlot_Init(UserInfoFriednsSlot __instance)
        {
            TextMeshProUGUI lvl = __instance._friendCard.tmp_level.transform.parent.Find("[Text]LevelLabel").GetComponentInChildren<TextMeshProUGUI>();
            LevelLabel(lvl);
            TextMeshProUGUI num = __instance._friendCard.tmp_level.transform.parent.Find("[Text]IdNumberLabel").GetComponentInChildren<TextMeshProUGUI>();
            NumberLabel(num);
            num.fontSize = 35;
        }
        [HarmonyPatch(typeof(UserInfoFriendsInfoPopup), nameof(UserInfoFriendsInfoPopup.SetData))]
        [HarmonyPostfix]
        private static void UserInfoFriendsInfoPopup_Init(UserInfoFriendsInfoPopup __instance)
        {
            TextMeshProUGUI lvl = __instance._friendsManager._friendCard.tmp_level.transform.parent.Find("[Text]LevelLabel").GetComponentInChildren<TextMeshProUGUI>();
            LevelLabel(lvl);
            TextMeshProUGUI num = __instance._friendsManager._friendCard.tmp_level.transform.parent.Find("[Text]IdNumberLabel").GetComponentInChildren<TextMeshProUGUI>();
            NumberLabel(num);
        }
        private static void LevelLabel(TextMeshProUGUI lvl_l)
        {
            lvl_l.text = lvl_l.text.Replace("LV", "УР");
        }
        private static void NumberLabel(TextMeshProUGUI num_l)
        {
            num_l.text = "№";
        }
        [HarmonyPatch(typeof(PersonalityLevelUpExpUI), nameof(PersonalityLevelUpExpUI.Initialize))]
        [HarmonyPostfix]
        private static void LevelUpPopUpChange(PersonalityLevelUpExpUI __instance)
        {
            TextMeshProUGUI lvl = __instance.transform.FindChild("[TMPro]LevelLabel").GetComponentInChildren<TextMeshProUGUI>(true);
            LevelLabel(lvl);
            TextMeshProUGUI exp = __instance.transform.FindChild("[TMPro]EXP Label").GetComponentInChildren<TextMeshProUGUI>(true);
            exp.text = "ОПЫТ";
        }

        [HarmonyPatch(typeof(StageInfoDisplayRenewal), nameof(StageInfoDisplayRenewal.SetChapterUIActive))]
        [HarmonyPostfix]
        private static void StageInfo_Level(StageInfoDisplayRenewal __instance)
        {
            if (__instance.tmp_recommendLevel != null)
            {
                TextMeshProUGUI level = __instance.tmp_recommendLevel;

                level.GetComponentInChildren<RectTransform>(true).anchoredPosition = new Vector2(5, -20);

                level.text = level.text.Replace("LV.", "УР.") + "\n";
            }
        }
        #endregion

        #region Sinner UI
        [HarmonyPatch(typeof(UnitInformationController), nameof(UnitInformationController.Init))]
        [HarmonyPostfix]
        private static void UnitInformationController_Init(UnitInformationController __instance)
        {
            Transform max_level = __instance.transform.Find("[Script]UnitInformationController_Renewal/[Canvas]AboveSpine/[Rect]UnitStatusContent/[Button]PersonaliyLevelUpButton/[Text]MAXContent");
            Transform max_thread = __instance.transform.Find("[Script]UnitInformationController_Renewal/[Canvas]AboveSpine/[Rect]UnitStatusContent/[Button]GacksungLevelUpButton/[Text]MAXContent");
            if (max_level != null)
            {
                max_level.GetComponentInChildren<TextMeshProUGUI>(true).text = "МАКС.";
                max_thread.GetComponentInChildren<TextMeshProUGUI>(true).text = "МАКС.";
            }
        }
        [HarmonyPatch(typeof(UnitInformationSkillSlot), nameof(UnitInformationSkillSlot.SetData))]
        [HarmonyPostfix]
        private static void UnitInformationSkillSlot_Init(UnitInformationSkillSlot __instance)
        {
            GameObject skill = GameObject.Find("[Canvas]RatioMainUI/[Rect]PanelRoot/[Script]UnitInformationController(Clone)/[Script]UnitInformationController_Renewal/[Canvas]AboveSpine/[Script]TabContentManager/[Layout]UnitInfoTabList/[Button]UnitInfoTab (1)/[Text]UnitInfoTabName");
            if (skill != null)
            {
                skill.GetComponentInChildren<TextMeshProUGUI>(true).text = "Навыки";
            }
            if (__instance.tmp_skillTier.text == "DEFENSE")
            {
                __instance.tmp_skillTier.text = "ЗАЩИТА";
                __instance.tmp_skillTier.GetComponent<TextMeshProUGUI>().enabled = true;
                __instance.tmp_skillTier.GetComponentInChildren<TextMeshProUGUI>(true).enabled = true;
            }
        }
        [HarmonyPatch(typeof(UnitInformationTabButton), nameof(UnitInformationTabButton.SetData))]
        [HarmonyPostfix]
        private static void UnitInformationTabButton_Init(UnitInformationTabButton __instance)
        {
            if (__instance.tmp_tabName.text == "E.G.O")
            {
                __instance.tmp_tabName.text = "ЭГО";
            }
        }
        [HarmonyPatch(typeof(UnitInformationPersonalitySkillTypeButton), nameof(UnitInformationPersonalitySkillTypeButton.SetData))]
        [HarmonyPostfix]
        private static void SkillTypes_Names(UnitInformationPersonalitySkillTypeButton __instance)
        {
            String skills = __instance.tmp_skilType.text;
            if (skills.StartsWith("Атака"))
            {
                string[] parts = skills.Split(' ');
                int num = int.Parse(parts[1]);

                __instance.tmp_skilType.text = $"{num}-я Атака";
            }
        }
        [HarmonyPatch(typeof(UnitInfoBreakSectionTooltipUI), nameof(UnitInfoBreakSectionTooltipUI.SetDataAndOpen))]
        [HarmonyPostfix]
        private static void UnitInfoBreakSections(UnitInfoBreakSectionTooltipUI __instance)
        {
            __instance.tmp_tooltipContent.fontSize = 35f;
        }
        #endregion

        #region Battle UI
        [HarmonyPatch(typeof(UpperSkillInfoUIStateSettingButton), nameof(UpperSkillInfoUIStateSettingButton.SetCurrentState))]
        [HarmonyPostfix]
        private static void BattleUI_Init(UpperSkillInfoUIStateSettingButton __instance)
        {
            __instance._currentStateText.text = __instance._currentStateText.text.Replace("MAX", "<size=50><cspace=-3px>МАКС</cspace></size>").Replace("MID", "<size=50>CР</size>").Replace("MIN", "<size=50>МИН</size>");

            __instance._minBtn.tmp_text.text = "<size=50>МИН</size>";
            __instance._midBtn.tmp_text.text = "<size=50>СР</size>";
            __instance._maxBtn.tmp_text.text = "<size=50><cspace=-3px>МАКС</cspace></size>";
        }
        [HarmonyPatch(typeof(ActTypoWaveStartUI), nameof(ActTypoWaveStartUI.Open))]
        [HarmonyPostfix]
        private static void ActTypoWaveStartUI_Init(ActTypoWaveStartUI __instance)
        {
            if (__instance.tmp_content.text.Contains("WAVE"))
            {
                __instance.tmp_content.text = __instance.tmp_content.text.Replace("WAVE", "ВОЛНА");
            }
        }
        [HarmonyPatch(typeof(MentalBreakStateTypoUI), nameof(MentalBreakStateTypoUI.RevealTypo_Panic))]
        [HarmonyPostfix]
        private static void PanicStateUI_42Open(MentalBreakStateTypoUI __instance)
        {
            if (__instance._titlePanicTypoEffect != null)
                __instance._titlePanicTypoEffect.tmp.text = __instance._titlePanicTypoEffect.tmp.text.Replace("PANIC", "СРЫВ");
        }
        [HarmonyPatch(typeof(TargetDetailSkillInfoController), nameof(TargetDetailSkillInfoController.SetSkillUpperData))]
        [HarmonyPostfix]
        private static void ParryingData_WinRate(TargetDetailSkillInfoController __instance)
        {
            __instance._winRateTypo._textMeshPro.lineSpacing = -30;
        }
        [HarmonyPatch(typeof(UnitInformationTabButton), nameof(UnitInformationTabButton.Init))]
        [HarmonyPostfix]
        private static void EnemyUnitInfo_Init(UnitInformationTabButton __instance)
        {
            __instance.tmp_tabName.text = __instance.tmp_tabName.text.Replace("Атака", "Навыки");
        }
        [HarmonyPatch(typeof(NewOperationController), nameof(NewOperationController.SetState))]
        [HarmonyPostfix]
        private static void AutoButtons(NewOperationController __instance)
        {
            Transform WinRate = __instance.transform.Find("[Rect]ActiveControl/[Rect]Pivot/[Rect]ActionableSlotList/[Layout]SinActionSlotsGrid/[Rect]AutoSelectButton/[Rect]Pivot/[Toggle]WinRate/Background/Text (TMP)");
            WinRate.GetComponentInChildren<TextMeshProUGUI>(true).fontSize = 34;
            WinRate.GetComponentInChildren<TextMeshProUGUI>(true).lineSpacing = -20;
        }

        [HarmonyPatch(typeof(UnitInfoNameTagAbResearchLevelUI), nameof(UnitInfoNameTagAbResearchLevelUI.Init))]
        [HarmonyPostfix]
        private static void AbnormalityResearch(UnitInfoNameTagAbResearchLevelUI __instance)
        {
            __instance.tmp_storyButton.lineSpacing = -20;
        }
        [HarmonyPatch(typeof(UnitInformationSkillSlot), nameof(UnitInformationSkillSlot.UpdateLayout))]
        [HarmonyPostfix]
        private static void SkillDefence(UnitInformationSkillSlot __instance)
        {
            __instance.tmp_skillTier.text = __instance.tmp_skillTier.text.Replace("DEFENSE", "ЗАЩИТА");
        }

        public static string DevyatSinners(OperationSkillSlot __instance)
        {
            string sinner;
            switch (__instance.SkillID)
            {
                case 1091004:
                    sinner = "Родя";
                    break;
                case 1101104:
                    sinner = "Синклер";
                    break;
                default:
                    sinner = "Ноунейм какой-то";
                    break;
            }
            return sinner;
        }

        [HarmonyPatch(typeof(SkillLackTypoUI), nameof(SkillLackTypoUI.SetData))]
        [HarmonyPostfix]
        private static void LackOfSkill(SkillLackTypoUI __instance)
        {
            __instance._lackUIText.text = __instance._lackUIText.text.Replace("Патроныов", "Патронов").Replace("Живущие и Почившиеов", "Бабочек Живущих и Почивших");

            System.Random thatsIt = new System.Random();
            int deliveryMeme = 1;
            OperationSkillSlot skillSlot = __instance.gameObject.GetComponentInParent<OperationSkillSlot>(true);
            string sinner = DevyatSinners(skillSlot);
            for (int r = 0; r < 2; r++)
            {
                if (__instance._lackUIText.enabled)
                {
                    deliveryMeme = thatsIt.Next(1, 21);
                }
                if (__instance._lackUIText.text.Contains("КУРЬЕР") && deliveryMeme == 20)
                {
                    switch (sinner)
                    {
                        case "Родя":
                            __instance._lackUIText.text = "! РОДЯ ДАЁТ ПО СЪЁБАМ !";
                            break;
                        case "Синклер":
                            __instance._lackUIText.text = "! СИНКЛЕР ДАЁТ ПО СЪЁБАМ !";
                            break;
                        default:
                            __instance._lackUIText.text = "! КУРЬЕР ДЕВЯТКИ ЩА СЪЕБЁТСЯ !";
                            break;
                    }
                }
            }
        }
        [HarmonyPatch(typeof(AbnormalityUnitConditionText), nameof(AbnormalityUnitConditionText.SetConditionText))]
        [HarmonyPostfix]
        private static void AbnormalityPart_ConditionTexts(AbnormalityUnitConditionText __instance)
        {
            if (__instance.tmp_condition != null)
            {
                __instance.tmp_condition.lineSpacing = -30;
                __instance.tmp_condition.GetComponent<RectTransform>().anchoredPosition = new Vector2(16, 2.75f);
            }
        }
        [HarmonyPatch(typeof(UnitInfoNameTagAbResearchLevelUI), nameof(UnitInfoNameTagAbResearchLevelUI.SetData))]
        [HarmonyPostfix]
        private static void Enemy_ResearchLevel(UnitInfoNameTagAbResearchLevelUI __instance)
        {
            if (__instance.tmp_researchLevel != null)
            {
                __instance.tmp_researchLevel.lineSpacing = -30;
                __instance.tmp_researchLevel.GetComponent<RectTransform>().anchoredPosition = new Vector2(97.95f, -20);
            }
        }

        [HarmonyPatch(typeof(BattleTotalDamageTypoSlot), nameof(BattleTotalDamageTypoSlot.Open))]
        [HarmonyPostfix]
        private static void BattleTotalDamageTypoSlot_Init(BattleTotalDamageTypoSlot __instance)
        {
            __instance.tmp_total.text = "ВСЕГО";
            __instance.tmp_total.color = __instance._originTextColor;
            __instance.tmp_damage.color = __instance._originTextColor;
            __instance.tmp_total.fontMaterial.SetColor(ShaderUtilities.ID_OutlineColor, __instance._atrOutlineColor);
            __instance.tmp_total.fontMaterial.SetColor(ShaderUtilities.ID_UnderlayColor, __instance._atrUnderlayColor);
        }
        [HarmonyPatch(typeof(TargetDetailSkillInfoController), nameof(TargetDetailSkillInfoController.SetUISetting))]
        [HarmonyPostfix]
        private static void Targeting(TargetDetailSkillInfoController __instance)
        {
            if (__instance._winRateTypo != null)
                __instance._winRateTypo._textMeshPro.lineSpacing = -30;
        }
        [HarmonyPatch(typeof(TargettingSkillInfo_Base), nameof(TargettingSkillInfo_Base.Init))]
        [HarmonyPrefix]
        private static void Targeting_SkillName(TargettingSkillInfo_Base __instance)
        {
            if (__instance != null)
            {
                __instance._tmp_skillName.GetComponentInChildren<TextMeshProLanguageSetter>(true).enabled = false;
                __instance._tmp_skillName.lineSpacing = -20;
                __instance._tmp_Lv.transform.parent.Find("[Text]LV").GetComponentInChildren<TextMeshProUGUI>().text = "<size=130%>У</size>р";
                __instance._tmp_skillName.GetComponentInChildren<RectTransform>().sizeDelta = new Vector2(350, 86);
            }
        }
        #endregion

        #region Battle Result UI
        [HarmonyPatch(typeof(BattleResultUIPanel), nameof(BattleResultUIPanel.SetStatusUI))]
        [HarmonyPostfix]
        private static void BattleResult_Init(BattleResultUIPanel __instance)
        {
            __instance.tmp_result.lineSpacing = -30;
            __instance.tmp_result.characterSpacing = 3;

            Transform managerLV = __instance.transform.Find("[Rect]Right/[Rect]Frames/rect_titleGroup/[Script]UserLevel/[Tmpro]LvValue/[Tmpro]Lv");
            if (managerLV != null)
            {
                managerLV.GetComponentInChildren<TextMeshProUGUI>(true).text = managerLV.GetComponentInChildren<TextMeshProUGUI>(true).text.Replace("LV.", "УР.");
            }
            Transform exp = __instance.transform.Find("[Rect]Right/[Rect]Frames/rect_titleGroup/[Script]UserLevel/[Rect]ExpTexts/[Tmpro]ExpTitle");
            if (exp != null)
            {
                exp.GetComponentInChildren<TextMeshProUGUI>(true).text = exp.GetComponentInChildren<TextMeshProUGUI>(true).text.Replace("EXP", "ОПЫТ");
                exp.GetComponentInChildren<TextMeshProUGUI>(true).fontSize = 40f;
                exp.GetComponentInChildren<TextMeshProUGUI>(true).characterSpacing = -2;
            }
        }
        [HarmonyPatch(typeof(BattleResultPersonalityExpGaugeUI), nameof(BattleResultPersonalityExpGaugeUI.SetLevelText))]
        [HarmonyPostfix]
        private static void BattleResultPersonalityExpGaugeUI_Init(BattleResultPersonalityExpGaugeUI __instance)
        {
            Transform Lv = __instance.tmp_levelText.transform.Find("tmp_level_text");
            Lv.GetComponentInChildren<TextMeshProUGUI>().text = "УР.";
        }

        [HarmonyPatch(typeof(BattleResultHintUI), nameof(BattleResultHintUI.SetData))]
        [HarmonyPostfix]
        private static void BattleResultHintUI_Fix(BattleResultHintUI __instance)
        {
            __instance.tmp_content.fontSizeMax = 35;
        }
        #endregion

        #region Dungeon
        [HarmonyPatch(typeof(NodeUI), nameof(NodeUI.UpdateData))]
        [HarmonyPostfix]
        private static void Nodes(NodeUI __instance)
        {
            if (__instance._startTypo != null)
            {
                __instance._startTypo.GetComponentInChildren<TextMeshProUGUI>(true).text = "НАЧАЛО";
            }
        }
        #endregion

        #region Announcers
        [HarmonyPatch(typeof(AnnouncerSelectionUI), nameof(AnnouncerSelectionUI.UpdateBattleAnnouncer))]
        [HarmonyPostfix]
        private static void AnnouncerSelectionUI_Init(AnnouncerSelectionUI __instance)
        {
            Transform dante = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot/[Image]SelectedTag/[Text]Selected");
            if (dante != null)
            {
                dante.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбран";
            }
            Transform gregor = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot (1)/[Image]SelectedTag/[Text]Selected");
            if (gregor != null)
            {
                gregor.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбран";
            }
            Transform charon = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot (2)/[Image]SelectedTag/[Text]Selected");
            if (charon != null)
            {
                charon.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбрана";
            }
            Transform sinclair = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (sinclair != null)
            {
                sinclair.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбран";
                sinclair.GetComponentInChildren<TextMeshProUGUI>(true).name = "Синклер";
            }
            Transform rodya = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (rodya != null)
            {
                rodya.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбрана";
                rodya.GetComponentInChildren<TextMeshProUGUI>(true).name = "Родя";
            }
            Transform yisang = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (yisang != null)
            {
                yisang.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбран";
                yisang.GetComponentInChildren<TextMeshProUGUI>(true).name = "ИСан";
            }
            Transform yuri = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (yuri != null)
            {

                yuri.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбрана";
                yuri.GetComponentInChildren<TextMeshProUGUI>(true).name = "Юри";
            }
            Transform effiesod = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (effiesod != null)
            {
                effiesod.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбраны";
                effiesod.GetComponentInChildren<TextMeshProUGUI>(true).name = "ЭпиСод";
            }
            Transform ishmael = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (ishmael != null)
            {
                ishmael.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбрана";
                ishmael.GetComponentInChildren<TextMeshProUGUI>(true).name = "Изя";
            }
            Transform malkuth = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (malkuth != null)
            {
                malkuth.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбрана";
                malkuth.GetComponentInChildren<TextMeshProUGUI>(true).name = "Малкьют";
            }
            Transform pierrejack = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (pierrejack != null)
            {
                pierrejack.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбраны";
                pierrejack.GetComponentInChildren<TextMeshProUGUI>(true).name = "ПьерДжек";
            }
            Transform angela_my_beloved = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (angela_my_beloved != null)
            {
                angela_my_beloved.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбрана";
                angela_my_beloved.GetComponentInChildren<TextMeshProUGUI>(true).name = "Анжелааа";
            }
            Transform nelly = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (nelly != null)
            {
                nelly.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбрана";
                nelly.GetComponentInChildren<TextMeshProUGUI>(true).name = "Нелли";
            }
            Transform heathcliff = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (heathcliff != null)
            {
                heathcliff.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбран";
                heathcliff.GetComponentInChildren<TextMeshProUGUI>(true).name = "Хитклиф";
            }
            Transform samjo = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (samjo != null)
            {
                samjo.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбран";
                samjo.GetComponentInChildren<TextMeshProUGUI>(true).name = "Самджо";
            }
            Transform yesod = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (yesod != null)
            {
                yesod.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбран";
                yesod.GetComponentInChildren<TextMeshProUGUI>(true).name = "Йесод";
            }
            Transform molars = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (molars != null)
            {
                molars.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбраны";
                molars.GetComponentInChildren<TextMeshProUGUI>(true).name = "Коренные";
            }
            Transform hod = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (hod != null)
            {
                hod.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбрана";
                hod.GetComponentInChildren<TextMeshProUGUI>(true).name = "Ход";
            }
            Transform dawn_offise = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (dawn_offise != null)
            {
                dawn_offise.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбраны";
                dawn_offise.GetComponentInChildren<TextMeshProUGUI>(true).name = "Рассвет";
            }
            Transform sancho = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (sancho != null)
            {
                sancho.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбрана";
                sancho.GetComponentInChildren<TextMeshProUGUI>(true).name = "Санчо";
            }
            Transform don_quixote = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (don_quixote != null)
            {
                don_quixote.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбрана";
                don_quixote.GetComponentInChildren<TextMeshProUGUI>(true).name = "ДонКихот";
            }
            Transform laMancha_bloodfiends = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (laMancha_bloodfiends != null)
            {
                laMancha_bloodfiends.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбраны";
                laMancha_bloodfiends.GetComponentInChildren<TextMeshProUGUI>(true).name = "Кровосиси";
            }
            Transform om_trignome = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (om_trignome != null)
            {
                om_trignome.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбраны";
                om_trignome.GetComponentInChildren<TextMeshProUGUI>(true).name = "Гномы";
            }
            Transform netzach = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (netzach != null)
            {
                netzach.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбран";
                netzach.GetComponentInChildren<TextMeshProUGUI>(true).name = "Нецах";
            }
            Transform fullstopoffice = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (fullstopoffice != null)
            {
                fullstopoffice.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбраны";
                fullstopoffice.GetComponentInChildren<TextMeshProUGUI>(true).name = "Точечники";
            }
            Transform aengdu = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (aengdu != null)
            {
                aengdu.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбрана";
                aengdu.GetComponentInChildren<TextMeshProUGUI>(true).name = "Энду";
            }
            Transform jun = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (jun != null)
            {
                jun.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбран";
                jun.GetComponentInChildren<TextMeshProUGUI>(true).name = "ДзюнОфБэкстритс";
            }
        }
        #endregion

        #region SeasonTag
        [HarmonyPatch(typeof(UnitInformationSeasonTagUI), nameof(UnitInformationSeasonTagUI.SetSeasonDataWithTitle))]
        [HarmonyPostfix]
        private static void UnitInformationSeasonTagUI_Init(UnitInformationSeasonTagUI __instance)
        {
            string text = __instance.tmp_season.text.Replace("SEASON", "СЕЗОН");
            __instance.tmp_season.text = text.Replace("WALPURGISNACHT -", "<cspace=-2px>ВАЛЬПУРГИЕВА НОЧЬ -</cspace>");
            __instance.tmp_season.text = __instance.tmp_season.text.Replace("I", "<cspace=-2px>I</cspace>");
            float seasonSize = __instance.tmp_season.fontSize;
            switch (seasonSize)
            {
                case 65f:
                {
                    __instance.tmp_season.fontSize = 60f;
                    break;
                }
            }
        }
        [HarmonyPatch(typeof(UnitInformationSeasonTagUI), nameof(UnitInformationSeasonTagUI.SetSeasonData))]
        [HarmonyPostfix]
        private static void UnitInformationSeasonTagUI_SetSeasonData(UnitInformationSeasonTagUI __instance)
        {
            string text = __instance.tmp_season.text.Replace("SEASON", "СЕЗОН");
            __instance.tmp_season.text = text.Replace("WALPURGISNACHT -", "<cspace=-4px>ВАЛЬПУРГИЕВА НОЧЬ -</cspace>");
            __instance.tmp_season.text = __instance.tmp_season.text.Replace("I", "<cspace=-4px>I</cspace>");
            float seasonSize = __instance.tmp_season.fontSize;
            switch (seasonSize)
            {
                case 65f:
                    {
                        __instance.tmp_season.fontSize = 60f;
                        break;
                    }
            }
        }
        #endregion

        #region BattlePass
        [HarmonyPatch(typeof(BattlePassUIPopup), nameof(BattlePassUIPopup.localizeHelper.Initialize))]
        [HarmonyPostfix]
        private static void BattlePass_Init(BattlePassUIPopup __instance)
        {
            TextMeshProUGUI battlePassBtn = __instance.passInfoButton.GetComponentInChildren<TextMeshProUGUI>();
            battlePassBtn.lineSpacing = -30;
            Transform limbus_pass = __instance.transform.Find("[Rect]Right/[Rect]Ticket/[Button]TicketImage_BuyNotYet/[Rect]Texts/[Text]LIMBUSPASS");
            Transform limbus_pass_bought = __instance.transform.Find("[Rect]Right/[Rect]Ticket/[Image]LimTicketImage_YesIHave/[LocalizeText]Useing/[Text]LimbusPass");
            Transform battle_pass = __instance.transform.Find("[Rect]Right/[Rect]Ticket/[Button]TicketImage_BuyNotYet/[Rect]Texts/[LocalizeText]Buying");
            Transform battle_pass_bought = __instance.transform.Find("[Rect]Right/[Rect]Ticket/[Image]LimTicketImage_YesIHave/[LocalizeText]Useing");
            Transform package = __instance.transform.Find("[Rect]Right/[Rect]Package/[Text]Title");
            limbus_pass.GetComponentInChildren<TextMeshProUGUI>(true).text = "<cspace=-2px>ПРОПУСК «ЛИМБУСА»</cspace>";
            limbus_pass_bought.GetComponentInChildren<TextMeshProUGUI>(true).text = "<size=75%><cspace=-2px>ПРОПУСК «ЛИМБУСА» ПРОБИТ</cspace></size>";
            limbus_pass_bought.GetComponentInChildren<TextMeshProUGUI>(true).enableWordWrapping = false;
            limbus_pass_bought.GetComponentInChildren<RectTransform>(true).anchoredPosition = new Vector2(-90f, -37.5f);
            battle_pass_bought.GetComponentInChildren<TextMeshProUGUI>(true).text = "";
            package.GetComponentInChildren<TextMeshProLanguageSetter>(true).enabled = false;
            battle_pass.GetComponentInChildren<TextMeshProLanguageSetter>(true).enabled = false;
        }
        #endregion

        #region BattlePass Timer
        [HarmonyPatch(typeof(BattlePassUIPopup), nameof(BattlePassUIPopup.SetRemainText))]
        [HarmonyPostfix]
        private static void BattlePassT_Init(BattlePassUIPopup __instance)
        {
            if (__instance.tmp_remainDate.text.Contains("Дней:"))
            {
                __instance.tmp_remainDate.text = __instance.tmp_remainDate.text.Replace("Дней:", "");
                string[] parts = __instance.tmp_remainDate.text.Split(' ');
                int number = int.Parse(parts[0]);
                string leftWord = "";
                if (getTimerD(number).EndsWith("день"))
                    leftWord = "Остался";
                else leftWord = "Осталось";
                __instance.tmp_remainDate.text = $"{leftWord} {number} {getTimerD(number)}";
            }
            else if (__instance.tmp_remainDate.text.Contains("Часов:"))
            {
                __instance.tmp_remainDate.text = __instance.tmp_remainDate.text.Replace("Часов:", "");
                string[] parts = __instance.tmp_remainDate.text.Split(' ');
                int number = int.Parse(parts[0]);
                string leftWord = "";
                if (getTimerH(number).EndsWith("час"))
                    leftWord = "Остался";
                else leftWord = "Осталось";
                __instance.tmp_remainDate.text = $"{leftWord} {number} {getTimerH(number)}";
            }
            else if (__instance.tmp_remainDate.text.Contains("Минут:"))
            {
                __instance.tmp_remainDate.text = __instance.tmp_remainDate.text.Replace("Минут:", "");
                string[] parts = __instance.tmp_remainDate.text.Split(' ');
                int number = int.Parse(parts[0]);
                string leftWord = "";
                if (getTimerM(number).EndsWith("минута"))
                    leftWord = "Осталась";
                else leftWord = "Осталось";
                __instance.tmp_remainDate.text = $"{leftWord} {number} {getTimerM(number)}";
            }
            else if (__instance.tmp_remainDate.text.Contains("Днейчасов:"))
            {
                __instance.tmp_remainDate.text = __instance.tmp_remainDate.text.Replace("Днейчасов:", "");
                string[] parts = __instance.tmp_remainDate.text.Split(' ');
                int number1 = int.Parse(parts[0]);
                int number2 = int.Parse(parts[1]);
                string leftWord = "";
                if (getTimerD(number1).EndsWith("день"))
                    leftWord = "Остался";
                else leftWord = "Осталось";
                __instance.tmp_remainDate.text = $"{leftWord} {number1} {getTimerD(number1)} и {number2} {getTimerH(number2)}";
            }
        }
        #endregion

        #region Story UI
        [HarmonyPatch(typeof(MainStorySlot), nameof(MainStorySlot.SetData))]
        [HarmonyPostfix]
        private static void MainStorySlot_Init(MainStorySlot __instance)
        {
            GameObject episode = GameObject.Find("[Canvas]RatioMainUI/[Rect]PanelRoot/[UIPanel]StoryUIPanel(Clone)/[Rect]MainStoryUI/[Rect]MainStory/Scroll View/Viewport/Content/[Rect]MainStorySlot(Clone)/[Rect]Panel/[Rect]Title/[Text]Chapter");
            if (episode != null)
            {
                episode.GetComponentInChildren<TextMeshProUGUI>(true).name = "ЭПИЗОД";
                episode.GetComponentInChildren<TextMeshProUGUI>(true).text = "ЭПИЗОД";
            }
            __instance._conditionComingSoonText.GetComponent<TextMeshProUGUI>().text = "Скоро...";
        }
        [HarmonyPatch(typeof(OtherStorySlot), nameof(OtherStorySlot.SetData))]
        [HarmonyPostfix]
        private static void OtherStorySlot_Init(OtherStorySlot __instance)
        {
            Transform coming_soon = __instance.transform.Find("[Button]OtherStorySlot/[Rect]LockObject/[Text]LockComingSoon");
            if (coming_soon != null)
            {
                coming_soon.GetComponent<TextMeshProUGUI>().text = "Скоро";
            }
            Transform episode = __instance.transform.Find("[Button]OtherStorySlot/[Rect]UnLockObject/[Text]EPISODE");
            if (episode != null)
            {
                episode.GetComponentInChildren<TextMeshProUGUI>(true).text = "ЭПИЗОД";
            }
        }
        [HarmonyPatch(typeof(OtherStorySlot), nameof(OtherStorySlot.SetData_Event))]
        [HarmonyPostfix]
        private static void OSS_Init(OtherStorySlot __instance)
        {
            Transform episode = __instance.transform.Find("[Button]OtherStorySlot/[Rect]UnLockObject/[Text]EPISODE");
            if (episode != null)
            {
                episode.GetComponentInChildren<TextMeshProUGUI>(true).text = "ЭПИЗОД";
            }
        }
        [HarmonyPatch(typeof(MirrorStoryNodeSelectUI), nameof(MirrorStoryNodeSelectUI.OnStorySelect))]
        [HarmonyPostfix]
        private static void StoryNode_Title(MirrorStoryNodeSelectUI __instance)
        {
            __instance._rightStoryNameText.characterSpacing = 1;

            string[] parts = __instance._rightStoryNameText.text.Split(',');
            string faction = parts[0];
            string sinner = parts[1];
            string story = parts[2];
            string afterwards = null;

            if (story.StartsWith(" (Послесловие)"))
            {
                story = parts[3];
                afterwards = " (Послесловие)";
            }
            __instance._rightStoryNameText.text = $"{faction} {sinner}, {story}{afterwards}".Replace("  ", " ");

        }

        public static string SinnerStory(string sinnerName)
        {
            if (sinnerName == " И Сан")
                return "И Сана";
            else if (sinnerName == " Фауст")
                return "Фауст";
            else if (sinnerName == " Дон Кихот")
                return "Дон Кихот";
            else if (sinnerName == " Рёшу")
                return "Рёшу";
            else if (sinnerName == " Мерсо")
                return "Мерсо";
            else if (sinnerName == " Хон Лу")
                return "Хон Лу";
            else if (sinnerName == " Хитклиф")
                return "Хитклифа";
            else if (sinnerName == " Измаил")
                return "Измаил";
            else if (sinnerName == " Родя")
                return "Роди";
            else if (sinnerName == " Синклер")
                return "Синклера";
            else if (sinnerName == " Отис")
                return "Отис";
            else if (sinnerName == " Грегор")
                return "Грегора";
            else
                return "[ДАННЫЕ УДАЛЕНЫ]";

        }

        [HarmonyPatch(typeof(PersonalityStoryUI), nameof(PersonalityStoryUI.Open))]
        [HarmonyPostfix]
        private static void PersonalityStory(PersonalityStoryUI __instance)
        {
            __instance.PersonalityUI._personalityDetailObject.GetComponentInChildren<TextMeshProLanguageSetter>(true).enabled = false;
        }
        [HarmonyPatch(typeof(PersonalityStoryPersonalityStorySlot), nameof(PersonalityStoryPersonalityStorySlot.SetData))]
        [HarmonyPostfix]
        private static void StoryName(PersonalityStoryPersonalityStorySlot __instance)
        {
            
            String story = __instance._storyTitleText.text;
            if (__instance._storyTitleText.text.EndsWith("История"))
            {
                __instance._storyTitleText.text = __instance._storyTitleText.text.Replace("  ", " ");
                string[] parts = story.Split(',');
                string faction = parts[0];
                string sinner = parts[1];
                string storyword = "История";
                string afterward = "(Послесловие)";
                if (faction.StartsWith("Та"))
                {
                    faction = "Та, кто держит";
                    sinner = " Фауст";
                }
                else if (faction.StartsWith("Тот"))
                {
                    faction = "Тот, кому суждено держать";
                    sinner = " Синклер";
                }

                if (__instance._storyTitleText.text.Contains("(Послесловие)"))
                    __instance._storyTitleText.text = $"{faction}: {storyword} {SinnerStory(sinner)} {afterward}";
                else
                    __instance._storyTitleText.text = $"{faction}: {storyword} {SinnerStory(sinner)}";
            }
        }

        [HarmonyPatch(typeof(PersonalityStoryVoiceSectionButton), nameof(PersonalityStoryVoiceSectionButton.Init))]
        [HarmonyPostfix]
        private static void ChooseDialogueSet_Button(PersonalityStoryVoiceSectionButton __instance)
        {
            String buttons = __instance.tmp_button.text;
            string[] parts = buttons.Split(' ');
            string timeplace = parts[0];
            string number = parts[1];
            string canto = parts[2];
            string[] numeral = number.Split('-');
            int cantoNumber = int.Parse(numeral[0]);

            __instance.tmp_button.text = $"{timeplace} {cantoNumber}-й {canto}";
        }

        #endregion

        #region Gacha
        [HarmonyPatch(typeof(GachaUIPanel), nameof(GachaUIPanel.SetGachaInfoPanel))]
        [HarmonyPostfix]
        private static void GachaDateChanger(GachaUIPanel __instance)
        {
            if (__instance.GachaInfo.BannerInfo.EndDate == null)
            {
                __instance._scheduleRoot.SetActive(false);
            }
            else
            {
                var newMskTIME = __instance.GachaInfo.bannerInfo.EndDate.ToString("HH:mm", false);
                __instance.tmp_dateOfLimit.text = __instance.GachaInfo.BannerInfo.EndDate.ToString("dd.MM.yyyy", false);
                __instance.tmp_timeOfLimit.text = $"{newMskTIME} (МСК)";
            }
        }
        #endregion

        #region Buffs
        [HarmonyPatch(typeof(TypoText), nameof(TypoText.SetEnable))]
        [HarmonyPostfix]
        private static void TypoText_Init(TypoText __instance)
        {
            __instance._text.text = __instance._text.text.Replace("Дыхание счётчик", "Счётчик Дыхания");
            __instance._text.text = __instance._text.text.Replace("Заряд счётчик", "Счётчик Заряда");
            __instance._text.text = __instance._text.text.Replace("Кровотечение счётчик", "Счётчик Кровотечения");
            __instance._text.text = __instance._text.text.Replace("Огонь счётчик", "Счётчик Огня");
            __instance._text.text = __instance._text.text.Replace("Разрыв счётчик", "Счётчик Разрывов");
            __instance._text.text = __instance._text.text.Replace("Тремор счётчик", "Счётчик Тремора");
            __instance._text.text = __instance._text.text.Replace("Утопание счётчик", "Счётчик Утопания");
            __instance._text.text = __instance._text.text.Replace("Lack of Патроны", "Патронов нет!");
        }
        #endregion

        #region EGO
        [HarmonyPatch(typeof(BattleSkillViewUIInfo), nameof(BattleSkillViewUIInfo.Init))]
        [HarmonyPostfix]
        private static void EGO_Name_Lines(BattleSkillViewUIInfo __instance)
        {
            __instance._abnormalNameText.lineSpacing = -20;
            __instance._egoSkillNameText.lineSpacing = -20;
        }
        #endregion

        #region Dante Notes
        [HarmonyPatch(typeof(StoryTheaterDanteNoteDescSlot), nameof(StoryTheaterDanteNoteDescSlot.SetData))]
        [HarmonyPostfix]
        private static void StoryTheaterDanteNoteDescSlot_Clear(StoryTheaterDanteNoteDescSlot __instance)
        {
            __instance._descText.text = Regex.Replace(__instance._descText.text, @"<line-height=.*?>", "");
        }
        #endregion

        #region Mental BreakDown
        [HarmonyPatch(typeof(UnitInfoMentalTabContent), nameof(UnitInfoMentalTabContent.SetData))]
        [HarmonyPostfix]
        private static void UnitInfoMentalTabContent_Clear(UnitInfoMentalTabContent __instance)
        {
            __instance.tmp_addConditionDesc.text = Regex.Replace(__instance.tmp_addConditionDesc.text, @"<line-height=.*?>", "");
            __instance.tmp_addConditionDesc.fontSize = 35;
        }
        #endregion
    }
}