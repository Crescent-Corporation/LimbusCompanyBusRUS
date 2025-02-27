using HarmonyLib;
using LocalSave;
using MainUI;
using BepInEx.Configuration;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace LimbusLocalizeRUS
{
    public static class Russian_Settings
    {
        public static ConfigEntry<bool> IsUseRussian = LCB_CresCorpMod.CresCorp_Settings.Bind("CresCorp Settings", "IsUseRussian", true, "По умолчанию 「true」, 「false」 отключает русификатор.");
        static bool _isuserussian;
        static Toggle _russianCheckMark;
        [HarmonyPatch(typeof(SettingsPanelGame), nameof(SettingsPanelGame.InitLanguage))]
        [HarmonyPrefix]
        private static bool InitLanguage(SettingsPanelGame __instance, LocalGameOptionData option)
        {
            if (!_isuserussian)
            {
                Toggle original = __instance._languageToggles[0];
                var parent = original.transform.parent;
                var languageToggle = Object.Instantiate(original, parent);
                var rutmp = languageToggle.GetComponentInChildren<TextMeshProUGUI>(true);
                rutmp.fontSizeMax = 39;
                rutmp.text = "Русский";
                if (!_isuserussian)
                    rutmp.text = "Russian";
                _russianCheckMark = languageToggle;
                parent.localPosition =
                    new Vector3(parent.localPosition.x - 306f, parent.localPosition.y, parent.localPosition.z);
                while (__instance._languageToggles.Count > 3)
                    __instance._languageToggles.RemoveAt(__instance._languageToggles.Count - 1);
                __instance._languageToggles.Add(languageToggle);
            }

            foreach (var tg in __instance._languageToggles)
            {
                tg.onValueChanged.RemoveAllListeners();

                tg.onValueChanged.AddListener((Action<bool>)OnValueChanged);
                tg.SetIsOnWithoutNotify(false);
                continue;

                void OnValueChanged(bool isOn)
                {
                    if (!isOn) return;
                    __instance.OnClickLanguageToggleEx(__instance._languageToggles.IndexOf(tg));
                }
            }

            var language = option.GetLanguage();
            if (_isuserussian = IsUseRussian.Value)
                _russianCheckMark.SetIsOnWithoutNotify(true);
            else
                switch (language)
                {
                    case LOCALIZE_LANGUAGE.KR:
                        __instance._languageToggles[0].SetIsOnWithoutNotify(true);
                        break;
                    case LOCALIZE_LANGUAGE.EN:
                        __instance._languageToggles[1].SetIsOnWithoutNotify(true);
                        break;
                    case LOCALIZE_LANGUAGE.JP:
                        __instance._languageToggles[2].SetIsOnWithoutNotify(true);
                        break;
                }

            __instance._lang = language;
            return false;
        }
        [HarmonyPatch(typeof(SettingsPanelGame), nameof(SettingsPanelGame.ApplySetting))]
        [HarmonyPostfix]
        private static void ApplySetting() => IsUseRussian.Value = _isuserussian;
        private static void OnClickLanguageToggleEx(this SettingsPanelGame __instance, int tgIdx)
        {
            if (tgIdx == 3)
            {
                _isuserussian = true;
                return;
            }
            _isuserussian = false;
            if (tgIdx == 0)
                __instance._lang = LOCALIZE_LANGUAGE.KR;
            else if (tgIdx == 1)
                __instance._lang = LOCALIZE_LANGUAGE.EN;
            else if (tgIdx == 2)
                __instance._lang = LOCALIZE_LANGUAGE.JP;
        }
        [HarmonyPatch(typeof(DateUtil), nameof(DateUtil.TimeZoneOffset), MethodType.Getter)]
        [HarmonyPrefix]
        public static bool TimeZoneOffset(ref int __result)
        {
            if (IsUseRussian.Value)
            {
                __result = 3;
                return false;
            }
            return true;
        }
        [HarmonyPatch(typeof(DateUtil), nameof(DateUtil.TimeZoneString), MethodType.Getter)]
        [HarmonyPrefix]
        public static bool TimeZoneString(ref string __result)
        {
            if (IsUseRussian.Value)
            {
                __result = "MSK";
                return false;
            }
            return true;
        }
        [HarmonyPatch(typeof(PriceText), nameof(PriceText.SetPrice))]
        [HarmonyPrefix]
        private static bool SetPrice(PriceText __instance, IAPProductStaticData productStaticData)
        {
            if (IsUseRussian.Value)
            {
                __instance.tmp_unit.text = "РУБ";
                int priceTier = StaticDataManager.Instance._iapProductStaticDataList.GetDataByProductID(productStaticData.productId).priceTier;
                int usd_cent = StaticDataManager.Instance._iapPriceTierStaticDataList.list.Find((Func<IAPPriceTierStaticData, bool>)((Price) =>
                {
                    return Price.PriceTier == priceTier;
                })).usd_cent;
                __instance.tmp_price.text = (usd_cent + 1).ToString();
                return false;
            }
            return true;
        }
    }
}