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
using BattleUI.UIRoot;
using BattleUI.Operation;
using System;
using Dungeon.UI;
using Dungeon.UI.EgoGift;
using BattleUI.BattleUnit.SkillInfoUI;
using Dungeon.Shop;
using BattleStatistics;
using Microsoft.VisualBasic;
using BepInEx.Logging;
using System.Net.Mail;

namespace LimbusLocalizeRUS
{
    internal class LCBR_TextUI
    {
        #region Login
        [HarmonyPatch(typeof(LoginSceneManager), nameof(LoginSceneManager.SetLoginInfo))]
        [HarmonyPostfix]
        private static void Client_Init(LoginSceneManager __instance)
        {
            __instance.btn_allCacheClear.GetComponentInChildren<TextMeshProUGUI>(true).lineSpacing = -60;
            __instance.tmp_loginAccount.font = LCB_Cyrillic_Font.tmpcyrillicfonts[4];
            __instance.tmp_loginAccount.fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[4].material;
        }
        [HarmonyPatch(typeof(NetworkingUI), "Initialize")]
        [HarmonyPostfix]
        private static void NetworkingUI_Init(NetworkingUI __instance)
        {
            Transform connection = __instance.transform.Find("connecting_background/tmp_connecting");
            if (connection != null)
            {
                connection.GetComponentInChildren<TextMeshProUGUI>(true).text = "ПОДКЛЮЧЕНИЕ";
                connection.GetComponentInChildren<RectTransform>(true).anchoredPosition = new Vector2(10, 0);
                connection.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.GetCyrillicFonts(0);
                connection.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.GetCyrillicFonts(0).material;
                connection.GetComponentInChildren<TextMeshProUGUI>(true).fontSize = 77f;
            }
        }
        [HarmonyPatch(typeof(UpdateMovieScreen), nameof(UpdateMovieScreen.SetDownLoadProgress))]
        [HarmonyPostfix]
        private static void UpdateMovieScreen_Init(UpdateMovieScreen __instance)
        {
            if (__instance._loadingCategoryText != null)
            {
                GameObject now_l = GameObject.Find("[Canvas]DownloadScreen/UpdateMovieScreen/[Rect]LoadingUI/Text_NowLoading");
                now_l.GetComponentInChildren<TextMeshProUGUI>(true).text = "ЗАГРУЗКА...";
                now_l.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[1];
                now_l.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[1].material;
                __instance._loadingCategoryText.GetComponentInChildren<TextMeshProUGUI>(true).name = "ОБНОВЛЕНИЕ";
                __instance._loadingCategoryText.GetComponentInChildren<TextMeshProUGUI>(true).text = __instance._loadingCategoryText.GetComponentInChildren<TextMeshProUGUI>(true).text.Replace("Downloading", "ЗАГРУЗКА");
                __instance._loadingCategoryText.GetComponentInChildren<TextMeshProUGUI>(true).text = __instance._loadingCategoryText.GetComponentInChildren<TextMeshProUGUI>(true).text.Replace("UPDATE", "ОБНОВЛЕНИЯ");
                __instance._loadingCategoryText.GetComponentInChildren<TextMeshProUGUI>(true).text = __instance._loadingCategoryText.GetComponentInChildren<TextMeshProUGUI>(true).text.Replace("Sound", "ЗВУКОВ");
                __instance._loadingCategoryText.GetComponentInChildren<TextMeshProUGUI>(true).text = __instance._loadingCategoryText.GetComponentInChildren<TextMeshProUGUI>(true).text.Replace("Sprite", "СПРАЙТОВ");
                __instance._loadingCategoryText.GetComponentInChildren<TextMeshProUGUI>(true).text = __instance._loadingCategoryText.GetComponentInChildren<TextMeshProUGUI>(true).text.Replace("Retry ЗВГРУЗКА", "ПОПЫТКА ЗАГРУЗКИ");
                __instance._loadingCategoryText.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[1];
                __instance._loadingCategoryText.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[1].material;
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
                level.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.GetCyrillicFonts(0);
                level.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.GetCyrillicFonts(0).material;
                No.GetComponentInChildren<TextMeshProUGUI>(true).text = "№";
                No.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.GetCyrillicFonts(0);
                No.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.GetCyrillicFonts(0).material;
            }
        }

        [HarmonyPatch(typeof(UserInfoCard), nameof(UserInfoCard.SetDataMainLobby))]
        [HarmonyPostfix]
        private static void Lobby_LevelID(UserInfoCard __instance)
        {
            TextMeshProUGUI lv = __instance.transform.Find("[Rect]AspectRatio/[Canvas]Info/[Text]LevelLabel").GetComponentInChildren<TextMeshProUGUI>(true);
            lv.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            lv.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(2);
            lv.text = "УР";

            TextMeshProUGUI no = __instance.transform.Find("[Rect]AspectRatio/[Canvas]Info/[Text]IdNumberLabel").GetComponentInChildren<TextMeshProUGUI>(true);
            no.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            no.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(2);
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
            Color velvet_red = new Color(1.0f, 0.339f, 0.339f, 0.2f);
            Color reddish = new Color(0.686f, 0.003f, 0.003f, 1.0f);
            Transform district4 = __instance.transform.Find("[Rect]Active/[Script]PartAndChapterSelectionUIPanel/[Rect]Active/[Rect]Right/[Rect]Pivot/[Rect]StoryMap/[Mask]StoryMap/[Rect]ZoomPivot/[Image]MapBG/[Script]D_4/[Rect]TextData/[Tmpro]Area");
            Transform district10 = __instance.transform.Find("[Rect]Active/[Script]PartAndChapterSelectionUIPanel/[Rect]Active/[Rect]Right/[Rect]Pivot/[Rect]StoryMap/[Mask]StoryMap/[Rect]ZoomPivot/[Image]MapBG/[Script]J_10/[Rect]TextData/[Tmpro]Area");
            Transform district11 = __instance.transform.Find("[Rect]Active/[Script]PartAndChapterSelectionUIPanel/[Rect]Active/[Rect]Right/[Rect]Pivot/[Rect]StoryMap/[Mask]StoryMap/[Rect]ZoomPivot/[Image]MapBG/[Script]K_11/[Rect]TextData/[Tmpro]Area");
            Transform district21 = __instance.transform.Find("[Rect]Active/[Script]PartAndChapterSelectionUIPanel/[Rect]Active/[Rect]Right/[Rect]Pivot/[Rect]StoryMap/[Mask]StoryMap/[Rect]ZoomPivot/[Image]MapBG/[Script]U_21/[Rect]TextData/[Tmpro]Area");
            Transform district20 = __instance.transform.Find("[Rect]Active/[Script]PartAndChapterSelectionUIPanel/[Rect]Active/[Rect]Right/[Rect]Pivot/[Rect]StoryMap/[Mask]StoryMap/[Rect]ZoomPivot/[Image]MapBG/[Script]T_20/[Rect]TextData/[Tmpro]Area");
            Transform district16 = __instance.transform.Find("[Rect]Active/[Script]PartAndChapterSelectionUIPanel/[Rect]Active/[Rect]Right/[Rect]Pivot/[Rect]StoryMap/[Mask]StoryMap/[Rect]ZoomPivot/[Image]MapBG/[Script]P_16/[Rect]TextData/[Tmpro]Area");
            if (district4 != null)
            {
                district4.GetComponentInChildren<TextMeshProUGUI>(true).text = "4-й Район";
                district4.GetComponentInChildren<TextMeshProUGUI>(true).color = reddish;
                district4.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.GetCyrillicFonts(0);
                district4.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(1);
                district4.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetColor("_GlowColor", velvet_red);
            }
            if (district10 != null)
            {
                district10.GetComponentInChildren<TextMeshProUGUI>(true).text = "10-й Район";
                district10.GetComponentInChildren<TextMeshProUGUI>(true).color = reddish;
                district10.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.GetCyrillicFonts(0);
                district10.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(1);
                district10.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetColor("_GlowColor", velvet_red);
            }
            if (district11 != null)
            {
                district11.GetComponentInChildren<TextMeshProUGUI>(true).text = "11-й Район";
                district11.GetComponentInChildren<TextMeshProUGUI>(true).color = reddish;
                district11.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.GetCyrillicFonts(0);
                district11.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(1);
                district11.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetColor("_GlowColor", velvet_red);
            }
            if (district21 != null)
            {
                district21.GetComponentInChildren<TextMeshProUGUI>(true).text = "21-й Район";
                district21.GetComponentInChildren<TextMeshProUGUI>(true).color = reddish;
                district21.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.GetCyrillicFonts(0);
                district21.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(1);
                district21.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetColor("_GlowColor", velvet_red);
            }
            if (district20 != null)
            {
                district20.GetComponentInChildren<TextMeshProUGUI>(true).text = "20-й Район";
                district20.GetComponentInChildren<TextMeshProUGUI>(true).color = reddish;
                district20.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.GetCyrillicFonts(0);
                district20.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(1);
                district20.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetColor("_GlowColor", velvet_red);
            }
            if (district16 != null)
            {
                district16.GetComponentInChildren<TextMeshProUGUI>(true).text = "16-й Район";
                district16.GetComponentInChildren<TextMeshProUGUI>(true).color = reddish;
                district16.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.GetCyrillicFonts(0);
                district16.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(1);
                district16.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.SetColor("_GlowColor", velvet_red);
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
                level.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
                level.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[2].material;
            }
        }
        [HarmonyPatch(typeof(SubChapterScrollViewItem), nameof(SubChapterScrollViewItem.SetData))]
        [HarmonyPostfix]
        private static void SubChapterScrollViewItem_Init(SubChapterScrollViewItem __instance)
        {
            __instance.tmp_pageForBebaskai.text = __instance.tmp_pageForBebaskai.text.Replace("MINI", "МИНИ");
            __instance.tmp_pageForBebaskai.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            __instance.tmp_pageForBebaskai.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(2);

            // Small date at the chapter banner
            __instance.tmp_timeline.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            __instance.tmp_timeline.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(2);
            string area_timeline = __instance.tmp_timeline.text;
            __instance.tmp_timeline.text = timeline(area_timeline);
        }
        [HarmonyPatch(typeof(SubChapterScrollViewItem), nameof(SubChapterScrollViewItem.SetDefault))]
        [HarmonyPostfix]
        private static void SubChapterScrollViewItem_Pages(SubChapterScrollViewItem __instance)
        {
            __instance.tmp_page.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            __instance.tmp_page.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(2);
            __instance.tmp_page.GetComponentInChildren<TextMeshProLanguageSetter>().enabled = false;
        }
        [HarmonyPatch(typeof(ChapterSelectionUIPanel), nameof(ChapterSelectionUIPanel.StartMoveToRegion))]
        [HarmonyPostfix]
        private static void SubChapterTimeline_Data(ChapterSelectionUIPanel __instance)
        {
            List<TextMeshProUGUI> mapParts = new List<TextMeshProUGUI> { __instance.tmp_timeline, __instance.tmp_area, __instance.tmp_company };
            foreach (TextMeshProUGUI mapPart in mapParts)
            {
                mapPart.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
                mapPart.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(3);
            }
            
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

        private static string numberEnding(int number)
        {
            int lastDigit = number % 10;
            int lastTwoDigits = number % 100;

            if (lastTwoDigits >= 11 && lastTwoDigits <= 19)
            {
                return "-ый";
            }
            else if (lastDigit == 2 || lastTwoDigits == 40)
            {
                return "-ой";
            }
            else if (lastDigit == 3)
            {
                return "-ий";
            }
            else if (lastDigit >= 6 && lastDigit <= 8)
            {
                return "-ой";
            }
            else
            {
                return "-ый";
            }
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
            episode_main.GetComponentInChildren<TextMeshProUGUI>(true).m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(2);
            episode_main.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(7);

            Transform enter = __instance.transform.Find("[Rect]Desc/[Image]Background/[Image]Panel/[Button]EnterStory/[Text]Enter");
            enter.GetComponentInChildren<TextMeshProLanguageSetter>(true).enabled = false;
            enter.GetComponentInChildren<TextMeshProUGUI>(true).text = "Прочесть";
            if (enter.GetComponentInChildren<TextMeshProUGUI>(true).color == Color.black)
                enter.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial.DisableKeyword("UNDERLAY_ON");
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
                string numEpi = numberEnding(number);
                __instance._leftStoryNumberText.text = $"{number}{numEpi} {episode}";
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
                string unnumEpi = numberEnding(unnumber);
                __instance._unSelectStoryText.text = $"{unnumber}{unnumEpi}\n{unepisode}";

                __instance._unSelectStoryText.fontSize = 46;
                __instance._unSelectStoryText.lineSpacing = -30;
            }

            if (__instance._selectStoryText != null)
            {
                string[] parts = __instance._selectStoryText.text.Split(' ');
                int number = int.Parse(parts[0]);
                string episode = parts[1];
                string numEpi = numberEnding(number);
                __instance._selectStoryText.text = $"{number}{numEpi}\n{episode}";

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
                episode.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
                episode.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(3);
                episode.text = "ЭПИЗОД";
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
            ego_label.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(2);
            ego_label.text = "ЭГО";
        }
        #endregion

        #region Settings
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
                item.tmp_level.font = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
                item.tmp_level.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(6);
                Transform label = item.tmp_level.transform.parent.parent.Find("[Text]StageLabel");
                label.GetComponentInChildren<TextMeshProUGUI>().text = "ЭТАП";
                label.GetComponentInChildren<TextMeshProUGUI>().font = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
                label.GetComponentInChildren<TextMeshProUGUI>().fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[2].material;
            }
        }

        [HarmonyPatch(typeof(ThreadDungeonSelectStageButton), nameof(ThreadDungeonSelectStageButton.SetData))]
        [HarmonyPostfix]
        private static void ThreadDungeonSelectStageButton_Init(ThreadDungeonSelectStageButton __instance)
        {
            __instance.tmp_level.text = __instance.tmp_level.text.Replace("Lv", "Ур.");
            __instance.tmp_level.font = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
            __instance.tmp_level.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(6);
            __instance.tmp_level.fontSize = 60;
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
        #endregion

        #region Mirror Dungeons
        [HarmonyPatch(typeof(MirrorDungeonEntranceScrollUIPanel), nameof(MirrorDungeonEntranceScrollUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void MirrorDungeonDoors_Init(MirrorDungeonEntranceScrollUIPanel __instance)
        {
            Transform dungeon = __instance.transform.Find("[Script]EntranceItemsScrollView/[Rect]ViewPort/[Layout]Items/[Script]MirrorDungeonEntranceItemView_Single/[Rect]Selectable/[Tmpro]DungeonLabel");
            if (dungeon != null)
            {
                dungeon.GetComponentInChildren<TextMeshProUGUI>(true).text = "<cspace=-2px>Подземелье</cspace>";
                dungeon.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.GetCyrillicFonts(0);
                dungeon.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.GetCyrillicFonts(0).material;
            }
            Transform dungeon_hard = __instance.transform.Find("[Script]EntranceItemsScrollView/[Rect]ViewPort/[Layout]Items/[Script]MirrorDungeonEntranceItemView_Single (1)/[Rect]Selectable/[Tmpro]DungeonLabel");
            if (dungeon_hard != null)
            {
                dungeon_hard.GetComponentInChildren<TextMeshProUGUI>(true).text = "<cspace=-2px>Подземелье</cspace>";
                dungeon_hard.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.GetCyrillicFonts(0);
                dungeon_hard.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.GetCyrillicFonts(0).material;
            }
            Transform mirror = __instance.transform.Find("[Script]EntranceItemsScrollView/[Rect]ViewPort/[Layout]Items/[Script]MirrorDungeonEntranceItemView_Single/[Rect]Selectable/[Button]Stage Frame/Text (TMP)");
            if (mirror != null)
            {
                mirror.GetComponentInChildren<TextMeshProUGUI>(true).text = "Симуляция";
                mirror.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.GetCyrillicFonts(0);
                mirror.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.GetCyrillicFonts(0).material;
            }
            Transform mirror_hard = __instance.transform.Find("[Script]EntranceItemsScrollView/[Rect]ViewPort/[Layout]Items/[Script]MirrorDungeonEntranceItemView_Single (1)/[Rect]Selectable/[Button]Stage Frame/Text (TMP)");
            if (mirror_hard != null)
            {
                mirror_hard.GetComponentInChildren<TextMeshProUGUI>(true).text = "Ритурнель";
                mirror_hard.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.GetCyrillicFonts(0);
                mirror_hard.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.GetCyrillicFonts(0).material;
            }
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
        [HarmonyPatch(typeof(VendingMachineUIPanel), "Initialize")]
        [HarmonyPostfix]
        private static void VendingMachineUIPanel_Init(VendingMachineUIPanel __instance)
        {
            Transform identity_dispence = __instance.transform.Find("PageButtonArea/BackPanel/Btn_TypePersonality/Tmp_personality");
            Transform comment = __instance.transform.Find("GoodsStoreAreaMaster/PageButtonArea/BackPanel/Btn_TypeAnnouncer/Tmp_Announcer");
            Transform ego_dispence = __instance.transform.Find("GoodsStoreAreaMaster/PageButtonArea/BackPanel/Btn_TypeEGO/Tmp_EGO");
            if (identity_dispence != null)
            {
                identity_dispence.GetComponentInChildren<TextMeshProUGUI>(true).text = "<cspace=-2px><size = 70%>Идентичности</size></cspace>";
            }
            if (ego_dispence != null)
            {
                ego_dispence.GetComponentInChildren<TextMeshProUGUI>(true).text = "ЭГО";
            }
            if (comment != null)
            {
                comment.GetComponentInChildren<TextMeshProUGUI>(true).text = "<cspace=-1px><size=80%>Комментаторы</size></cspace>";
            }
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
                ego_mainui_2.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[3];
                ego_mainui_2.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[3].material;
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
                ego.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[3];
                ego.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[3].material;
                ego_hover.GetComponentInChildren<TextMeshProUGUI>(true).text = "ЭГО";
                ego_hover.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[3];
                ego_hover.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[3].material;
                ego_hover.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(8);
            }
            // TEAMS
            GameObject teams = GameObject.Find("[Canvas]RatioMainUI/[Rect]PanelRoot/[UIPanel]PersonalityFormationUIPanel(Clone)/[Rect]LeftObjects/[Rect]DeckSettings/[Rect]Contents/[Text]DeckTitle");
            if (teams != null)
            {
                teams.GetComponentInChildren<TextMeshProUGUI>(true).name = "КОМАНДЫ";
                teams.GetComponentInChildren<UITextDataLoader>(true).enabled = false;
                teams.GetComponentInChildren<TextMeshProUGUI>(true).text = "КОМАНДЫ";
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
                guard.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[3];
                guard.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[3].material;
            }
        }
        [HarmonyPatch(typeof(PersonalityDetailButton), nameof(PersonalityDetailButton.SetUIMode))]
        [HarmonyPostfix]
        private static void FormationSecondChance_Init(PersonalityDetailButton __instance)
        {
            __instance._egoText._text.font = LCB_Cyrillic_Font.tmpcyrillicfonts[3];
            __instance._egoText._text.m_fontAsset = LCB_Cyrillic_Font.tmpcyrillicfonts[3];
            __instance._egoText._text.m_currentFontAsset = LCB_Cyrillic_Font.tmpcyrillicfonts[3];
            __instance._egoText._text.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(8);
            __instance.tmp_egoHighlight.font = LCB_Cyrillic_Font.tmpcyrillicfonts[3];
            __instance.tmp_egoHighlight.m_fontAsset = LCB_Cyrillic_Font.tmpcyrillicfonts[3];
            __instance.tmp_egoHighlight.m_currentFontAsset = LCB_Cyrillic_Font.tmpcyrillicfonts[3];
            __instance.tmp_egoHighlight.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(8);
            __instance._skillText._text.fontStyle = FontStyles.Normal | FontStyles.SmallCaps;
            __instance.tmp_skillHighlight.fontStyle = FontStyles.Normal | FontStyles.SmallCaps;
            __instance._skillText._text.text = "Навыки";
            __instance.tmp_skillHighlight.text = "Навыки";
            __instance.tmp_skillHighlight.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(8);
        }
        [HarmonyPatch(typeof(PersonalityDetailButton), nameof(PersonalityDetailButton.Initialize))]
        [HarmonyPostfix]
        private static void FormationUwa_Init(PersonalityDetailButton __instance)
        {
            __instance._skillText._text.text = "Навыки";
            __instance.tmp_skillHighlight.text = "Навыки";
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
            //SetUpperClickActive, Initialize, SetUpperUIMode, SetData, 
            Color charcoal = new Color(0.016f, 0.016f, 0.016f, 0.91f);

            __instance._textInfo.txt_level.text = __instance._textInfo.txt_level.text.Replace("Lv.", "Ур.");
            __instance._textInfo.txt_level.font = LCB_Cyrillic_Font.GetCyrillicFonts(2);
            __instance._textInfo.txt_level.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(6);

            __instance._textInfo.txt_level.m_sharedMaterial.SetColor("_UnderlayColor", charcoal);

            __instance._additionalInfo.transform.Find("[Script]Abstain/[Text]Label").GetComponentInChildren<TextMeshProLanguageSetter>().enabled = false;

            TextMeshProUGUI abstain = __instance._additionalInfo.transform.Find("[Script]Abstain/[Text]Label").GetComponentInChildren<TextMeshProUGUI>(true);
            abstain.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            abstain.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(2);
            abstain.fontSize = 44;

            var sinnerName = __instance.transform.name.Substring(25);
            __instance._upperPortraitEventUI.tmp_text.text = Declension_sinners(sinnerName, __instance._upperPortraitEventUI.tmp_text.text);
            __instance._upperPortraitEventUI.tmp_text.lineSpacing = -30;
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
            __instance._materialSetter._text.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(8);

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
            __instance.tmp_text.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            __instance.tmp_text.fontSharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(1);

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

        [HarmonyPatch(typeof(FormationUIDeckToggle),nameof(FormationUIDeckToggle.UpdateScrollAnimation))]
        [HarmonyPostfix]
        private static void TeamName(FormationUIDeckToggle __instance)
        {
            __instance.tmp_title.text = __instance.tmp_title.text.Replace("#", "№");
        }

        [HarmonyPatch(typeof(MirrorDungeonShopItemSlotManager), nameof(MirrorDungeonShopItemSlotManager.SetDataOpen))]
        [HarmonyPostfix]
        private static void MirrorDungeonShopItemSlotManager_Init(MirrorDungeonShopItemSlotManager __instance)
        {
            if (__instance._changeSkillItem._item != null)
            {
                var sinner = __instance._changeSkillItem.tmp_title.text.Substring(21);
                __instance._changeSkillItem.tmp_title.text = "Изменение навыка для " + NameChanger(sinner);
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
        [HarmonyPatch(typeof(UnitinfoUnitStatusContent), nameof(UnitinfoUnitStatusContent.Init))]
        [HarmonyPostfix]
        private static void UnitinfoUnitStatusContent_Init(UnitinfoUnitStatusContent __instance)
        {
            TextMeshProUGUI lvlb = __instance._levelUI.transform.Find("Tmp_LV_sign").GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI lvlp = __instance._levelExpGuageUI.transform.Find("Tmp_LV_sign").GetComponentInChildren<TextMeshProUGUI>();
            List<TextMeshProUGUI> lvl_l = new List<TextMeshProUGUI> { lvlb, lvlp };
            foreach (TextMeshProUGUI lvl in lvl_l)
            {
                lvl.text = lvl.text.Replace("LV", "УР");
                lvl.font = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
                lvl.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(6);
            }
        }
        [HarmonyPatch(typeof(FormationSwitchablePersonalityUIScrollViewItem), nameof(FormationSwitchablePersonalityUIScrollViewItem.SetData))]
        [HarmonyPostfix]
        private static void FormationSwitchablePersonalityUIScrollViewItemLevel_Init(FormationSwitchablePersonalityUIScrollViewItem __instance)
        {
            __instance.txt_level.text = __instance.txt_level.text.Replace("Lv", "Ур.");
            __instance.txt_level.font = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
            __instance.txt_level.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(6);
            if (__instance._participatedObject != null)
            {
                TextMeshProUGUI participated = __instance._participatedObject.transform.Find("[Text]Label").GetComponentInChildren<TextMeshProUGUI>();
                participated.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
                participated.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(3);
                participated.fontSize = 56;
            }
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
            lvl_l.m_fontAsset = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
            lvl_l.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(6);
        }
        private static void NumberLabel(TextMeshProUGUI num_l)
        {
            num_l.text = "№";
            num_l.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            num_l.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(3);
        }
        [HarmonyPatch(typeof(PersonalityLevelUpExpUI), nameof(PersonalityLevelUpExpUI.Initialize))]
        [HarmonyPostfix]
        private static void LevelUpPopUpChange(PersonalityLevelUpExpUI __instance)
        {
            TextMeshProUGUI lvl = __instance.transform.FindChild("[TMPro]LevelLabel").GetComponentInChildren<TextMeshProUGUI>(true);
            LevelLabel(lvl);
            TextMeshProUGUI exp = __instance.transform.FindChild("[TMPro]EXP Label").GetComponentInChildren<TextMeshProUGUI>(true);
            exp.text = "ОПЫТ";
            exp.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(2);
            exp.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(6);
        }

        [HarmonyPatch(typeof(StageInfoDisplayRenewal), nameof(StageInfoDisplayRenewal.SetChapterUIActive))]
        [HarmonyPostfix]
        private static void StageInfo_Level(StageInfoDisplayRenewal __instance)
        {
            TextMeshProUGUI level = __instance.tmp_recommendLevel;

            level.GetComponentInChildren<RectTransform>(true).anchoredPosition = new Vector2(5, -20);

            level.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            level.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(3);

            string[] parts = level.text.Split('.');
            int lv_number = int.Parse(parts[1]);
            __instance.tmp_recommendLevel.text = $"<size=82>УР.{lv_number}</size>";
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
                max_level.GetComponentInChildren<TextMeshProUGUI>(true).text = "MAKC.";
                max_thread.GetComponentInChildren<TextMeshProUGUI>(true).text = "MAKC.";
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
                skill.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[3];
                skill.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[3].material;
            }
            if (__instance.tmp_skillTier.text == "DEFENSE")
            {
                __instance.tmp_skillTier.text = "ЗАЩИТА";
                __instance.tmp_skillTier.GetComponent<TextMeshProUGUI>().enabled = true;
                __instance.tmp_skillTier.GetComponentInChildren<TextMeshProUGUI>(true).enabled = true;
            }
            __instance.tmp_skillTier.font = LCB_Cyrillic_Font.tmpcyrillicfonts[1];
            __instance.tmp_skillTier.fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[1].material;
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

                __instance.tmp_skilType.text = $"{num}{skillNumEnding(num)} Атака";
            }
        }
        public static string skillNumEnding(int num)
        {
            int lastDigit = num % 10;

            if (lastDigit == 3)
            {
                return "-ья";
            }
            else
            {
                return "-ая";
            }
        }
        [HarmonyPatch(typeof(UnitInfoBreakSectionTooltipUI), nameof(UnitInfoBreakSectionTooltipUI.SetDataAndOpen))]
        [HarmonyPostfix]
        private static void UnitInfoBreakSections(UnitInfoBreakSectionTooltipUI __instance)
        {
            __instance.tmp_tooltipContent.font = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            __instance.tmp_tooltipContent.fontSize = 35f;
        }
        #endregion

        #region Battle UI
        [HarmonyPatch(typeof(UpperSkillInfoUIStateSettingButton), nameof(UpperSkillInfoUIStateSettingButton.SetCurrentState))]
        [HarmonyPostfix]
        private static void BattleUI_Init(UpperSkillInfoUIStateSettingButton __instance)
        {
            List<TextMeshProUGUI> buttons = new List<TextMeshProUGUI>() { __instance._minBtn.tmp_text, __instance._midBtn.tmp_text, __instance._maxBtn.tmp_text, __instance._currentStateText };
            foreach (TextMeshProUGUI button in buttons)
            {
                button.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
                button.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(3);
            }

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
                __instance.tmp_content.font = LCB_Cyrillic_Font.tmpcyrillicfonts[3];
                __instance.tmp_content.fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[3].material;
            }
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
        [HarmonyPatch(typeof(AbnormalityStatUI), nameof(AbnormalityStatUI.UpdateBottomUIScale))]
        [HarmonyPostfix]
        private static void AbnormalityStatUI_Init(AbnormalityStatUI __instance)
        {
            __instance.tmp_unitName.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            __instance.tmp_unitName.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(2);
            __instance.tmp_code.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(3);
            __instance.tmp_code.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicFonts(3).material;
        }

        [HarmonyPatch(typeof(TmproCharacterTypoTextEffect), nameof(TmproCharacterTypoTextEffect.SetTiltAndPosText))]
        [HarmonyPostfix]
        private static void PanicStateUI_Open(TmproCharacterTypoTextEffect __instance)
        {
            Color textColor = __instance.tmp.color;
            __instance.tmp.text = __instance.tmp.text.Replace("PANIC", "СРЫВ");
            __instance.tmp.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(3);
            __instance.tmp.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicFonts(3).material;
            __instance.tmp.fontMaterial.EnableKeyword("BEVEL_ON");
            __instance.tmp.fontMaterial.EnableKeyword("UNDERLAY_ON");
            __instance.tmp.fontMaterial.EnableKeyword("GLOW_ON");
            __instance.tmp.fontMaterial.SetColor("_GlowColor", textColor);
        }

        [HarmonyPatch(typeof(BattleChoiceSelectionOmenUI), nameof(BattleChoiceSelectionOmenUI.SetActiveOff))]
        [HarmonyPostfix]
        private static void BattleChoiceSelectionOmenUI_Init(BattleChoiceSelectionOmenUI __instance)
        {
            __instance.tmp_desc.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            __instance.tmp_desc.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(3);
        }
        
        [HarmonyPatch(typeof(NewOperationController), nameof(NewOperationController.SetState))]
        [HarmonyPostfix]
        private static void AutoButtons(NewOperationController __instance)
        {
            Transform WinRate = __instance.transform.Find("[Rect]ActiveControl/[Rect]Pivot/[Rect]ActionableSlotList/[Layout]SinActionSlotsGrid/[Rect]AutoSelectButton/[Rect]Pivot/[Toggle]WinRate/Background/Text (TMP)");
            WinRate.GetComponentInChildren<TextMeshProUGUI>(true).fontSize = 34;
            WinRate.GetComponentInChildren<TextMeshProUGUI>(true).lineSpacing = -20;
        }

        [HarmonyPatch(typeof(SkillAndCoinUI), nameof(SkillAndCoinUI.InitData))]
        [HarmonyPostfix]
        private static void SkillName(SkillAndCoinUI __instance)
        {
            __instance.tmp_skillName.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(12);
            __instance.tmp_skillName.fontMaterial.SetFloat("_UnderlayOffsetX", 0.8f);
            __instance.tmp_skillName.fontMaterial.SetFloat("_UnderlayOffsetY", -0.8f);
            if (__instance.tmp_skillName.text.StartsWith("Этика"))
                __instance.tmp_skillName.text = __instance.tmp_skillName.text.Replace("Этика", "<size=60%><cspace=-2px>Этика");
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
            __instance.tmp_skillTier.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            __instance.tmp_skillTier.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicFonts(0).material;
        }
        [HarmonyPatch(typeof(SkillLackTypoUI), nameof(SkillLackTypoUI.SetData))]
        [HarmonyPostfix]
        private static void LackOfSkill(SkillLackTypoUI __instance)
        {
            __instance._lackUIText.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(3);
            __instance._lackUIText.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(13);

            __instance._lackUIText.fontMaterial.EnableKeyword("UNDERLAY_ON");
            __instance._lackUIText.fontMaterial.SetColor("_UnderlayColor", new Color(0.01568628f, 0, 0.003921569f, 1f));
            __instance._lackUIText.fontMaterial.SetFloat("_UnderlayOffsetX", 0.4f);
            __instance._lackUIText.fontMaterial.SetFloat("_UnderlayOffsetY", -0.4f);
            __instance._lackUIText.fontMaterial.SetFloat("_UnderlayDilate", 0.1f);
            __instance._lackUIText.fontMaterial.SetFloat("_UnderlaySoftness", 0.2f);
            __instance._lackUIText.fontMaterial.EnableKeyword("GLOW_ON");
            __instance._lackUIText.fontMaterial.SetColor("_GlowColor", __instance._lackUIText.color);
            __instance._lackUIText.fontMaterial.SetFloat("_GlowInner", 0.05f);
            __instance._lackUIText.fontMaterial.SetFloat("_GlowOuter", 0.125f);
            __instance._lackUIText.fontMaterial.SetFloat("_GlowPower", 0.145f);

            __instance._lackUIText.text = __instance._lackUIText.text.Replace("Патроныов", "Патронов").Replace("Живущие и Почившиеов", "Бабочек Живущих и Почивших");

            System.Random thatsIt = new System.Random();
            int deliveryMeme = 1;
            for (int r = 0; r<2; r++)
            {
                if (__instance._lackUIText.enabled)
                {
                    deliveryMeme = thatsIt.Next(1, 21);
                }
                LCB_LCBRMod.LogInfo("Roll 1d20:" + deliveryMeme);
                if (__instance._lackUIText.text.Contains("КУРЬЕР") && deliveryMeme == 20)
                    __instance._lackUIText.text = "! РОДЯ ДАЁТ ПО СЪЁБАМ !";
            }
        }
        [HarmonyPatch(typeof(AbnormalityUnitConditionText), nameof(AbnormalityUnitConditionText.SetConditionText))]
        [HarmonyPostfix]
        private static void AbnormalityPartTexts(AbnormalityUnitConditionText __instance)
        {
            __instance.tmp_condition.lineSpacing = -30;
        }
        #endregion

        #region Battle Result UI
        [HarmonyPatch(typeof(BattleResultUIPanel), nameof(BattleResultUIPanel.SetStatusUI))]
        [HarmonyPostfix]
        private static void BattleResult_Init(BattleResultUIPanel __instance)
        {
            Transform managerLV = __instance.transform.Find("[Rect]Right/[Rect]Frames/rect_titleGroup/[Script]UserLevel/[Tmpro]LvValue/[Tmpro]Lv");
            if (managerLV != null)
            {
                managerLV.GetComponentInChildren<TextMeshProUGUI>(true).text = managerLV.GetComponentInChildren<TextMeshProUGUI>(true).text.Replace("LV.", "УР.");
                managerLV.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
                managerLV.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(6);
            }
            Transform exp = __instance.transform.Find("[Rect]Right/[Rect]Frames/rect_titleGroup/[Script]UserLevel/[Rect]ExpTexts/[Tmpro]ExpTitle");
            if (exp != null)
            {
                exp.GetComponentInChildren<TextMeshProUGUI>(true).text = exp.GetComponentInChildren<TextMeshProUGUI>(true).text.Replace("EXP", "ОПЫТ");
                exp.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
                exp.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[2].material;
                exp.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(6);
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
            Lv.GetComponentInChildren<TextMeshProUGUI>().m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(2);
            Lv.GetComponentInChildren<TextMeshProUGUI>().m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(6);
        }
        [HarmonyPatch(typeof(BattleResultPersonalityUI), nameof(BattleResultPersonalityUI.PlayGageAnimation))]
        [HarmonyPostfix]
        private static void SinnerLvlUI(BattleResultPersonalityUI __instance)
        {
            Color yellowish = new Color(1.0f, 0.306f, 0, 0.502f);
            Transform sinnerLV = __instance.tmp_level_text.transform.Find("tmp_level_text");
            if (sinnerLV != null)
            {
                sinnerLV.GetComponentInChildren<TextMeshProUGUI>(true).text = sinnerLV.GetComponentInChildren<TextMeshProUGUI>(true).text.Replace("LV.", "NV.");
                sinnerLV.GetComponentInChildren<TextMeshProUGUI>(true).m_fontAsset = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
                sinnerLV.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(6);
            }
            Transform sinnerLV_glow = __instance.tmp_level_text.transform.Find("tmp_level_text/tmp_effect_levelText");
            if (sinnerLV_glow != null)
            {
                sinnerLV_glow.GetComponentInChildren<TextMeshProUGUI>(true).text = sinnerLV_glow.GetComponentInChildren<TextMeshProUGUI>(true).text.Replace("LV.", "NV.");
                sinnerLV_glow.GetComponentInChildren<TextMeshProUGUI>(true).m_fontAsset = LCB_Cyrillic_Font.tmpcyrillicfonts[2];
                sinnerLV_glow.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(5);
                sinnerLV_glow.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial.EnableKeyword("GLOW_ON");
                sinnerLV_glow.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial.SetColor("_GlowColor", yellowish);
                sinnerLV_glow.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial.SetFloat("_GlowInner", 0.15f);
                sinnerLV_glow.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial.SetFloat("_GlowPower", 0.3f);
            }
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
                __instance._startTypo.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.GetCyrillicFonts(2);
                __instance._startTypo.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.GetCyrillicFonts(2).material;
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
                sinclair.GetComponentInChildren<TextMeshProUGUI>(true).name = "Выбран";
            }
            Transform rodya = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (rodya != null)
            {
                rodya.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбрана";
                rodya.GetComponentInChildren<TextMeshProUGUI>(true).name = "Выбрана";
            }
            Transform yisang = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (yisang != null)
            {
                yisang.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбран";
                yisang.GetComponentInChildren<TextMeshProUGUI>(true).name = "Выбран";
            }
            Transform yuri = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (yuri != null)
            {

                yuri.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбрана";
                yuri.GetComponentInChildren<TextMeshProUGUI>(true).name = "Выбрана";
            }
            Transform effiesod = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (effiesod != null)
            {
                effiesod.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбраны";
                effiesod.GetComponentInChildren<TextMeshProUGUI>(true).name = "Выбраны";
            }
            Transform ishmael = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (ishmael != null)
            {
                ishmael.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбрана";
                ishmael.GetComponentInChildren<TextMeshProUGUI>(true).name = "Выбрана";
            }
            Transform malkuth = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (malkuth != null)
            {
                malkuth.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбрана";
                malkuth.GetComponentInChildren<TextMeshProUGUI>(true).name = "Выбрана";
            }
            Transform pierrejack = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (pierrejack != null)
            {
                pierrejack.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбраны";
                pierrejack.GetComponentInChildren<TextMeshProUGUI>(true).name = "Выбраны";
            }
            Transform angela_my_beloved = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (angela_my_beloved != null)
            {
                angela_my_beloved.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбрана";
                angela_my_beloved.GetComponentInChildren<TextMeshProUGUI>(true).name = "Выбрана";
            }
            Transform nelly = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (nelly != null)
            {
                nelly.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбрана";
                nelly.GetComponentInChildren<TextMeshProUGUI>(true).name = "Выбрана";
            }
            Transform heathcliff = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (heathcliff != null)
            {
                heathcliff.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбран";
                heathcliff.GetComponentInChildren<TextMeshProUGUI>(true).name = "Выбран";
            }
            Transform samjo = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (samjo != null)
            {
                samjo.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбран";
                samjo.GetComponentInChildren<TextMeshProUGUI>(true).name = "Выбран";
            }
            Transform yesod = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (yesod != null)
            {
                yesod.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбран";
                yesod.GetComponentInChildren<TextMeshProUGUI>(true).name = "Выбран";
            }
            Transform molars = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (molars != null)
            {
                molars.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбраны";
                molars.GetComponentInChildren<TextMeshProUGUI>(true).name = "Выбраны";
            }
            Transform hod = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (hod != null)
            {
                hod.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбрана";
                hod.GetComponentInChildren<TextMeshProUGUI>(true).name = "Выбрана";
            }
            Transform dawn_offise = __instance.transform.Find("[Scroll]AnnouncerScrollView/Scroll View/Viewport/Content/Layout/[Script]BattleAnnouncerSlot(Clone)/[Image]SelectedTag/[Text]Selected");
            if (dawn_offise != null)
            {
                dawn_offise.GetComponentInChildren<TextMeshProUGUI>(true).text = "Выбраны";
                dawn_offise.GetComponentInChildren<TextMeshProUGUI>(true).name = "Выбраны";
            }
        }
        [HarmonyPatch(typeof(FormationBattleAnnouncerSelectionScrollViewItem), nameof(FormationBattleAnnouncerSelectionScrollViewItem.SetData))]
        [HarmonyPostfix]
        private static void FormationBattleAnnouncerSelectionScrollViewItem_Init(FormationBattleAnnouncerSelectionScrollViewItem __instance)
        {
            __instance.cg_selectedTag.GetComponentInChildren<TextMeshProUGUI>().font = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            __instance.cg_selectedTag.GetComponentInChildren<TextMeshProUGUI>().fontMaterial = LCB_Cyrillic_Font.GetCyrillicFonts(0).material;
        }
        [HarmonyPatch(typeof(FormationUIPanel), nameof(FormationUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void FixedAnnouncer_Init(FormationUIPanel __instance)
        {
            GameObject fixedAnnouncer = GameObject.Find("[Canvas]RatioMainUI/[Rect]PanelRoot/[UIPanel]PersonalityFormationUIPanel(Clone)/[Rect]LeftObjects/[Script]FormationBattleAnnouncer/[Rect]Contents/[Rect]LeftSide/[Rect]FixedLabel/[Image]FixedLabel/[Text]Fixed");
            if (fixedAnnouncer != null)
            {
                fixedAnnouncer.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[4];
                fixedAnnouncer.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[4].material;
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
            __instance.tmp_season.font = LCB_Cyrillic_Font.tmpcyrillicfonts[1];
            __instance.tmp_season.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(2);
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
            __instance.tmp_season.font = LCB_Cyrillic_Font.tmpcyrillicfonts[1];
            __instance.tmp_season.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(2);
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
        [HarmonyPatch(typeof(BattlePassLeftText), nameof(BattlePassLeftText.Initailize))]
        [HarmonyPostfix]
        private static void BattlePass_Label(BattlePassLeftText __instance)
        {
            __instance.tmp_limbusPassElse.GetComponentInChildren<TextMeshProLanguageSetter>().enabled = false;
            __instance.tmp_limbusPassElse.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(12);
            __instance.tmp_limbusPassKR.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(12);
        }
        public static void BebasForPass(List<Transform> transforms)
        {
            foreach (Transform t in transforms)
            {
                t.GetComponentInChildren<TextMeshProUGUI>(true).m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
                t.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(2);
            }
        }
        [HarmonyPatch(typeof(BattlePassUIPopup), nameof(BattlePassUIPopup.localizeHelper.Initialize))]
        [HarmonyPostfix]
        private static void BattlePass_Init(BattlePassUIPopup __instance)
        {
            Transform limbus_pass = __instance.transform.Find("[Rect]Right/[Rect]Ticket/[Button]TicketImage_BuyNotYet/[Rect]Texts/[Text]LIMBUSPASS");
            Transform limbus_pass_bought = __instance.transform.Find("[Rect]Right/[Rect]Ticket/[Image]LimTicketImage_YesIHave/[LocalizeText]Useing/[Text]LimbusPass");
            Transform battle_pass = __instance.transform.Find("[Rect]Right/[Rect]Ticket/[Button]TicketImage_BuyNotYet/[Rect]Texts/[LocalizeText]Buying");
            Transform battle_pass_bought = __instance.transform.Find("[Rect]Right/[Rect]Ticket/[Image]LimTicketImage_YesIHave/[LocalizeText]Useing");
            Transform package = __instance.transform.Find("[Rect]Right/[Rect]Package/[Text]Title");
            Transform package_popUp = __instance.transform.Find("BattlePassPurchaseLimbusOrPackagePopup/PanelGroup/PopupBase/Container/[Rect]Contents/[Button]PurchasePackage/[Text]Title");
            Transform until_pass = __instance.transform.Find("[Rect]Right/[Text]UntilSeason");
            limbus_pass.GetComponentInChildren<TextMeshProUGUI>(true).text = "<cspace=-2px>ЛИМБУС ПАСС</cspace>";
            limbus_pass_bought.GetComponentInChildren<TextMeshProUGUI>(true).text = "<cspace=-2px>ЛИМБУС ПАСС</cspace>";
            package.GetComponentInChildren<TextMeshProLanguageSetter>(true).enabled = false;
            battle_pass.GetComponentInChildren<TextMeshProLanguageSetter>(true).enabled = false;
            List<Transform> transforms = new List<Transform> { limbus_pass, limbus_pass_bought, battle_pass, battle_pass_bought, package, package_popUp, until_pass, __instance.tmp_be_in_use.transform, __instance.limbusPassPopup.tmp_description.transform };
            BebasForPass(transforms);
        }
        [HarmonyPatch(typeof(BattlePassUIPopup), nameof(BattlePassUIPopup.SetRemainText))]
        [HarmonyPostfix]
        private static void BattleSeason_Init(BattlePassUIPopup __instance)
        {
            Transform until_pass = __instance.transform.Find("[Rect]Right/[Text]UntilSeason");
            until_pass.GetComponentInChildren<TextMeshProUGUI>(true).m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            until_pass.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(2);
        }
        [HarmonyPatch(typeof(BattlePassPurchasedModal), nameof(BattlePassPurchasedModal.SetCurrentSeasonOpen))]
        [HarmonyPostfix]
        private static void Activation_Init(BattlePassPurchasedModal __instance)
        {
            Transform limbuspass_active = __instance.transform.Find("MainPanel/[Text]Activation");
            if (limbuspass_active != null)
            {
                limbuspass_active.GetComponentInChildren<TextMeshProUGUI>(true).m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
                limbuspass_active.GetComponentInChildren<TextMeshProUGUI>(true).m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(2);
            }
        }
        [HarmonyPatch(typeof(BattlePassPurchaseLimbusOrPackagePopup), nameof(BattlePassPurchaseLimbusOrPackagePopup.SetDataOpen))]
        [HarmonyPostfix]
        private static void PurchasePass(BattlePassPurchaseLimbusOrPackagePopup __instance)
        {
            TextMeshProUGUI special_pack = __instance._purchasePackagePopup.transform.Find("[Text]Title").GetComponentInChildren<TextMeshProUGUI>(true);
            special_pack.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(0);
            special_pack.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(2);
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
                episode.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[1];
                episode.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[1].material;
            }
            __instance._conditionComingSoonText.GetComponent<TextMeshProUGUI>().text = "Скоро...";
            __instance._conditionComingSoonText.GetComponent<TextMeshProUGUI>().font = LCB_Cyrillic_Font.tmpcyrillicfonts[1];
            __instance._conditionComingSoonText.GetComponent<TextMeshProUGUI>().fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[1].material;
        }
        [HarmonyPatch(typeof(OtherStorySlot), nameof(OtherStorySlot.SetData))]
        [HarmonyPostfix]
        private static void OtherStorySlot_Init(OtherStorySlot __instance)
        {
            Transform coming_soon = __instance.transform.Find("[Button]OtherStorySlot/[Rect]LockObject/[Text]LockComingSoon");
            if (coming_soon != null)
            {
                coming_soon.GetComponent<TextMeshProUGUI>().text = "Скоро";
                coming_soon.GetComponent<TextMeshProUGUI>().font = LCB_Cyrillic_Font.tmpcyrillicfonts[1];
                coming_soon.GetComponent<TextMeshProUGUI>().fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[1].material;
            }
            Transform episode = __instance.transform.Find("[Button]OtherStorySlot/[Rect]UnLockObject/[Text]EPISODE");
            if (episode != null)
            {
                episode.GetComponentInChildren<TextMeshProUGUI>(true).text = "ЭПИЗОД";
                episode.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[1];
                episode.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[1].material;
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
                episode.GetComponentInChildren<TextMeshProUGUI>(true).font = LCB_Cyrillic_Font.tmpcyrillicfonts[1];
                episode.GetComponentInChildren<TextMeshProUGUI>(true).fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[1].material;
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

            __instance.tmp_button.text = $"{timeplace} {cantoNumber}{cantoNumEnding(cantoNumber)} {canto}";
        }

        public static string cantoNumEnding(int number)
        {
            int lastDigit = number % 10;
            int lastTwoDigits = number % 100;

            if (lastDigit == 3 && lastTwoDigits != 13)
            {
                return "-ей";
            }
            else
            {
                return "-ой";
            }
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
                __instance.tmp_timeOfLimit.m_fontAsset = LCB_Cyrillic_Font.GetCyrillicFonts(2);
                __instance.tmp_timeOfLimit.m_sharedMaterial = LCB_Cyrillic_Font.GetCyrillicMats(6);
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
    }
}