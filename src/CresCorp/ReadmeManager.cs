using HarmonyLib;
using Il2CppSystem.Collections.Generic;
using LocalSave;
using MainUI;
using MainUI.NoticeUI;
using Server;
using SimpleJSON;
using System;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UObject = UnityEngine.Object;

namespace LimbusLocalizeRUS
{
    public static class ReadmeManager
    {
        public static NoticeUIPopup NoticeUIInstance;
        public static RedDotWriggler _redDot_Notice;
        public static List<Notice> ReadmeList = new();
        public static Dictionary<string, Sprite> ReadmeButton = new();
        public static Dictionary<string, Sprite> ReadmeSprites = new();
        public static Dictionary<string, Sprite> ReadmeEventSprites = new();
        public static Dictionary<string, Sprite> ReadmeStorySprites = new();
        public static Dictionary<string, Sprite> ReadmePassSprites = new();
        public static System.Collections.Generic.Dictionary<string, Action> ReadmeActions = new();
        static ReadmeManager()
        {
            InitReadmeList();
            InitReadmeButton();
            InitReadmeSprites();
            InitReadmeEventSprites();
            InitReadmeStorySprites();
            InitReadmePassSprites();
        }
        public static void UIInitialize()
        {
            var close = Close;
            NoticeUIInstance._popupPanel.closeEvent.AddListener(close);
            NoticeUIInstance._arrowScroll.Initialize();
            NoticeUIInstance._titleViewManager.Initialized();
            NoticeUIInstance._contentViewManager.Initialized();
            NoticeUIInstance.btn_back._onClick.AddListener(close);
            var eventNoticeOnClick = NoticeUIInstance.EventTapClickEvent;
            var systemNoticeOnClick = NoticeUIInstance.SystemTapClickEvent;
            NoticeUIInstance.btn_eventNotice._onClick.AddListener(eventNoticeOnClick);
            NoticeUIInstance.btn_systemNotice._onClick.AddListener(systemNoticeOnClick);
            NoticeUIInstance.btn_systemNotice.GetComponentInChildren<UITextDataLoader>(true).enabled = false;
            NoticeUIInstance.btn_systemNotice.GetComponentInChildren<TextMeshProUGUI>(true).text = "Новости\nобновлений";
            NoticeUIInstance.btn_systemNotice.GetComponentInChildren<TextMeshProUGUI>(true).lineSpacing = -30;
            NoticeUIInstance.btn_eventNotice.GetComponentInChildren<UITextDataLoader>(true).enabled = false;
            NoticeUIInstance.btn_eventNotice.GetComponentInChildren<TextMeshProUGUI>(true).text = "Основные\nновости";
            NoticeUIInstance.btn_eventNotice.GetComponentInChildren<TextMeshProUGUI>().lineSpacing = -30;
        }
        public static void Open()
        {
            NoticeUIInstance.Open();
            NoticeUIInstance._popupPanel.Open();
            var notices = ReadmeList;
            NoticeUIInstance._systemNotices = notices.FindAll((Func<Notice, bool>)Findsys);
            NoticeUIInstance._eventNotices = notices.FindAll((Func<Notice, bool>)Findeve);
            NoticeUIInstance.EventTapClickEvent();
            NoticeUIInstance.btn_eventNotice.Cast<UISelectedButton>().SetSelected(true);
            return;

            bool Findsys(Notice x)
            {
                return x.noticeType == NOTICE_TYPE.System;
            }

            bool Findeve(Notice x)
            {
                return x.noticeType == NOTICE_TYPE.Event;
            }
        }

        public static void InitReadmeButton()
        {
            ReadmeButton = new Dictionary<string, Sprite>();
            {
                foreach (FileInfo fileInfo in new DirectoryInfo(LCB_CresCorpMod.ModPath + "/Localize/Readme").GetFiles().Where(f => f.Extension == ".jpg" || f.Extension == ".png"))
                {
                    Texture2D texture2D = new(2, 2);
                    ImageConversion.LoadImage(texture2D, File.ReadAllBytes(fileInfo.FullName));
                    Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileInfo.FullName);
                    texture2D.name = fileNameWithoutExtension;
                    sprite.name = fileNameWithoutExtension;
                    UObject.DontDestroyOnLoad(sprite);
                    sprite.hideFlags |= HideFlags.HideAndDontSave;
                    ReadmeButton[fileNameWithoutExtension] = sprite;
                }
            }
        }
        public static void InitReadmeSprites()
        {
            ReadmeSprites = new Dictionary<string, Sprite>();

            foreach (FileInfo fileInfo in new DirectoryInfo(LCB_CresCorpMod.ModPath + "/Localize/Readme/Sprites").GetFiles().Where(f => f.Extension == ".jpg" || f.Extension == ".png"))
            {
                Texture2D texture2D = new(2, 2);
                ImageConversion.LoadImage(texture2D, File.ReadAllBytes(fileInfo.FullName));
                Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileInfo.FullName);
                texture2D.name = fileNameWithoutExtension;
                sprite.name = fileNameWithoutExtension;
                UObject.DontDestroyOnLoad(sprite);
                sprite.hideFlags |= HideFlags.HideAndDontSave;
                ReadmeSprites[fileNameWithoutExtension] = sprite;
            }
        }
        public static Sprite GetReadmeSprites(string spriteName)
        {
            if (ReadmeSprites.TryGetValue(spriteName, out Sprite sprite))
            {
                return sprite;
            }
            else
            {
                LCB_CresCorpMod.LogInfo($"Sprite with name '{spriteName}' not found! (ReadmeSprites)");
                return null;
            }
        }
        public static void InitReadmeEventSprites()
        {
            ReadmeEventSprites = new Dictionary<string, Sprite>();

            foreach (FileInfo fileInfo in new DirectoryInfo(LCB_CresCorpMod.ModPath + "/Localize/Readme/Sprites/Event").GetFiles().Where(f => f.Extension == ".jpg" || f.Extension == ".png"))
            {
                Texture2D texture2D = new(2, 2);
                ImageConversion.LoadImage(texture2D, File.ReadAllBytes(fileInfo.FullName));
                Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileInfo.FullName);
                texture2D.name = fileNameWithoutExtension;
                sprite.name = fileNameWithoutExtension;
                UObject.DontDestroyOnLoad(sprite);
                sprite.hideFlags |= HideFlags.HideAndDontSave;
                ReadmeEventSprites[fileNameWithoutExtension] = sprite;
            }
        }
        public static Sprite GetReadmeEventSprites(string spriteName)
        {
            if (ReadmeEventSprites.TryGetValue(spriteName, out Sprite sprite))
            {
                return sprite;
            }
            else
            {
                LCB_CresCorpMod.LogInfo($"Sprite with name '{spriteName}' not found! (ReadmeEventSprites)");
                return null;
            }
        }
        public static void InitReadmeStorySprites()
        {
            ReadmeStorySprites = new Dictionary<string, Sprite>();

            foreach (FileInfo fileInfo in new DirectoryInfo(LCB_CresCorpMod.ModPath + "/Localize/Readme/Sprites/Story").GetFiles().Where(f => f.Extension == ".jpg" || f.Extension == ".png"))
            {
                Texture2D texture2D = new(2, 2);
                ImageConversion.LoadImage(texture2D, File.ReadAllBytes(fileInfo.FullName));
                Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileInfo.FullName);
                texture2D.name = fileNameWithoutExtension;
                sprite.name = fileNameWithoutExtension;
                UObject.DontDestroyOnLoad(sprite);
                sprite.hideFlags |= HideFlags.HideAndDontSave;
                ReadmeStorySprites[fileNameWithoutExtension] = sprite;
            }
        }
        public static Sprite GetReadmeStorySprites(string spriteName)
        {
            if (ReadmeStorySprites.TryGetValue(spriteName, out Sprite sprite))
            {
                return sprite;
            }
            else
            {
                LCB_CresCorpMod.LogInfo($"Sprite with name '{spriteName}' not found! (ReadmeStorySprites)");
                return null;
            }
        }
        public static void InitReadmePassSprites()
        {
            ReadmePassSprites = new Dictionary<string, Sprite>();

            foreach (FileInfo fileInfo in new DirectoryInfo(LCB_CresCorpMod.ModPath + "/Localize/Readme/Sprites/BattlePass").GetFiles().Where(f => f.Extension == ".jpg" || f.Extension == ".png"))
            {
                Texture2D texture2D = new(2, 2);
                ImageConversion.LoadImage(texture2D, File.ReadAllBytes(fileInfo.FullName));
                Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileInfo.FullName);
                texture2D.name = fileNameWithoutExtension;
                sprite.name = fileNameWithoutExtension;
                UObject.DontDestroyOnLoad(sprite);
                sprite.hideFlags |= HideFlags.HideAndDontSave;
                ReadmePassSprites[fileNameWithoutExtension] = sprite;
            }
        }
        public static Sprite GetReadmePassSprites(string spriteName)
        {
            if (ReadmePassSprites.TryGetValue(spriteName, out Sprite sprite))
            {
                return sprite;
            }
            else
            {
                LCB_CresCorpMod.LogInfo($"Sprite with name '{spriteName}' not found! (ReadmePassSprites)");
                return null;
            }
        }
        public static void InitReadmeList()
        {
            ReadmeList.Clear();
            foreach (var notices in JSONNode.Parse(File.ReadAllText(LCB_CresCorpMod.ModPath + "/Localize/Readme/Readme.json"))[0].AsArray.m_List)
                ReadmeList.Add(HandleDynamicType(notices.ToString()));
        }
        public static Notice HandleDynamicType(string jsonPayload)
        {
            var noticetype = typeof(NoticeSynchronousDataList).GetProperty("noticeFormats")!.PropertyType.GetGenericArguments()[0];
            var deserializedObject = typeof(JsonUtility).GetMethod("FromJson", new[] { typeof(string) })
                ?.MakeGenericMethod(noticetype).Invoke(null, new object[] { jsonPayload });
            return Activator.CreateInstance(typeof(Notice), deserializedObject, LOCALIZE_LANGUAGE.KR) as Notice;
        }

        public static void Close()
        {
            UserLocalSaveDataRoot.Instance.NoticeRedDotSaveModel.Save();
            NoticeUIInstance._popupPanel.Close();
            UpdateNoticeRedDot();
        }

        public static void UpdateNoticeRedDot()
        {
            _redDot_Notice?.gameObject.SetActive(IsValidRedDot());
        }
        public static bool IsValidRedDot()
        {
            var i = 0;
            var count = ReadmeList.Count;
            while (i < count)
            {
                var readme = ReadmeList[i];
                if (!readme.StartDate.isFuture && !readme.EndDate.isPast &&
                    !UserLocalSaveDataRoot.Instance.NoticeRedDotSaveModel.TryCheckId(readme.ID)) return true;
                i++;
            }

            return false;
        }
        #region Новости
        [HarmonyPatch(typeof(UserLocalNoticeRedDotModel), nameof(UserLocalNoticeRedDotModel.InitNoticeList))]
        [HarmonyPrefix]
        private static bool InitNoticeList(UserLocalNoticeRedDotModel __instance, List<int> severNoticeList)
        {
            //UpdateChecker.CheckReadmeUpdate();
            if (__instance.idList.RemoveAll((Func<int, bool>)Func) > 0)
                __instance.isChanged = true;
            __instance.Save();
            UpdateNoticeRedDot();
            return false;

            bool Func(int id)
            {
                return !severNoticeList.Contains(id) && ReadmeList.FindAll((Func<Notice, bool>)Func2).Count == 0;

                bool Func2(Notice notice)
                {
                    return notice.ID == id;
                }
            }
        }
        [HarmonyPatch(typeof(NoticeUIPopup), nameof(NoticeUIPopup.Initialize))]
        [HarmonyPostfix]
        private static void NoticeUIPopupInitialize(NoticeUIPopup __instance)
        {
            if (NoticeUIInstance) return;
            var noticeUIPopupInstance = UObject.Instantiate(__instance, __instance.transform.parent);
            NoticeUIInstance = noticeUIPopupInstance;
            UIInitialize();
        }
        [HarmonyPatch(typeof(MainLobbyUIPanel), nameof(MainLobbyUIPanel.Initialize))]
        [HarmonyPostfix]
        private static void MainLobbyUIPanelInitialize(MainLobbyUIPanel __instance)
        {
            var uiButtonInstance = UObject.Instantiate(__instance.button_notice, __instance.button_notice.transform.parent)
                .Cast<MainLobbyRightUpperUIButton>();
            _redDot_Notice = uiButtonInstance.gameObject.GetComponentInChildren<RedDotWriggler>(true);
            UpdateNoticeRedDot();
            uiButtonInstance._onClick.RemoveAllListeners();
            var onClick = Open;
            uiButtonInstance._onClick.AddListener(onClick);
            uiButtonInstance.transform.SetSiblingIndex(1);
            var spriteSetting = ScriptableObject.CreateInstance<ButtonSprites>();
            spriteSetting._enabled = ReadmeButton["Crescent_Button"];
            spriteSetting._hover = ReadmeButton["Crescent_Button_Hover"];
            uiButtonInstance.spriteSetting = spriteSetting;
            var transform = __instance.button_notice.transform.parent;
            var layoutGroup = transform.GetComponent<HorizontalLayoutGroup>();
            layoutGroup.childScaleHeight = true;
            layoutGroup.childScaleWidth = true;
            for (var i = 0; i < transform.childCount; i++) transform.GetChild(i).localScale = new Vector3(0.77f, 0.77f, 1f);
        }
        [HarmonyPatch(typeof(NoticeUIContentImage), nameof(NoticeUIContentImage.SetData))]
        [HarmonyPrefix]
        private static bool ImageSetData(NoticeUIContentImage __instance, string formatValue)
        {
            if (!formatValue.StartsWith("Readme_")) return true;
            var image = ReadmeSprites[formatValue];
            __instance.gameObject.SetActive(true);
            __instance.SetImage(image);
            return false;
        }
        [HarmonyPatch(typeof(NoticeUIContentHyperLink), nameof(NoticeUIContentHyperLink.OnPointerClick))]
        [HarmonyPrefix]
        private static bool HyperLinkOnPointerClick(NoticeUIContentHyperLink __instance)
        {
            var url = __instance.tmp_main.text;
            if (url.StartsWith("<link"))
            {
                var startIndex = url.IndexOf('=');
                if (startIndex != -1)
                {
                    var endIndex = url.IndexOf('>', startIndex + 1);
                    if (endIndex != -1) url = url.Substring(startIndex + 1, endIndex - startIndex - 1);
                }

                if (url.StartsWith("Action_"))
                {
                    ReadmeActions[url]?.Invoke();
                    return false;
                }
            }

            Application.OpenURL(url);
            return false;
        }
        #endregion
    }
}
