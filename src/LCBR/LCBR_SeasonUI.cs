using HarmonyLib;
using MainUI;
using MainUI.VendingMachine;
using System;
using System.Runtime.InteropServices;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UI;
using TMPro;
using static UI.Utility.InfoModels;
using static UI.Utility.TMProStringMatcher;

namespace LimbusLocalizeRUS
{
    public static class LCBR_SeasonUI
    {
        [HarmonyPatch(typeof(MainLobbyBannerSlot), nameof(MainLobbyBannerSlot.Update))]
        [HarmonyPostfix]
        private static void MainLobbyUIPanel_Init(MainLobbyBannerSlot __instance)
        {
            //MAIN MENU

            Sprite banner = __instance.img_main.sprite;
            if (banner.name.Contains("banner_battlepass_season4"))
                __instance.img_main.overrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["Season4_Banner"];

            //GameObject banner = GameObject.Find("[Canvas]RatioMainUI/[Rect]PresenterRoot/[UIPresenter]LobbyUIPresenter(Clone)/[Rect]Active/[UIPanel]MainLobbyUIPanel/[Rect]Banner/[Rect]RightBanners/[Script]FirstBanner/[Mask]BannerImageMask/[Image]BannerImage");
            //if (banner.GetComponentInChildren<Image>(true).sprite.name.Contains("banner_battlepass_season4_en") || banner.GetComponentInChildren<Image>(true).sprite.name.Contains("banner_battlepass_season4_kr") || banner.GetComponentInChildren<Image>(true).sprite.name.Contains("banner_battlepass_season4_jp"))
            //    banner.GetComponentInChildren<Image>(true).m_OverrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["Season4_Banner"];
        }
        [HarmonyPatch(typeof(VendingMachineBannerSlot), nameof(VendingMachineBannerSlot.SetData))]
        [HarmonyPostfix]
        private static void VendingMachineBannerSlot_Init(BannerSlot<VendingMachineStaticDataList> __instance)
        {
            if (__instance._id == 0)
            {
                __instance._base._bannerImage.m_OverrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["Base_Shop"];
            }
            else if (__instance._id == 4)
            {
                __instance._base._bannerImage.m_OverrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["Season4_Shop"];
            }
            else if (__instance._id == 3)
            {
                __instance._base._bannerImage.m_OverrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["Season3_Shop"];
            }
            else if (__instance._id == 2)
            {
                __instance._base._bannerImage.m_OverrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["Season2_Shop"];
            }
            else if (__instance._id == 1)
            {
                __instance._base._bannerImage.m_OverrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["Season1_Shop"];
            }
            else if (__instance._id == 91)
            {
                __instance._base._bannerImage.m_OverrideSprite = LCBR_ReadmeManager.ReadmeEventSprites["Walpurgis_Shop"];
            }
        }
        [HarmonyPatch(typeof(BattlePassUIPopup), nameof(BattlePassUIPopup.SetupBaseData))]
        [HarmonyPostfix]
        private static void SeasonPass_Init(BattlePassUIPopup __instance)
        {
            __instance.seasonPeriod.font = LCB_Cyrillic_Font.tmpcyrillicfonts[1];
            __instance.seasonPeriod.fontMaterial = LCB_Cyrillic_Font.tmpcyrillicfonts[1].material;
            __instance.seasonPeriod.text = "(МСК) 06:00 28.03.2024 ~";

            //FLAGS
            __instance.seasonPeriod.m_isRebuildingLayout = false;
            __instance.seasonPeriod.ignoreVisibility = true;
            __instance.seasonPeriod.isOverlay = false;
            __instance.seasonPeriod.m_ignoreCulling = true;
            __instance.seasonPeriod.isOverlay = false;
            __instance.seasonPeriod.m_isOverlay = false;
            __instance.seasonPeriod.m_isParsingText = true;
            __instance.seasonPeriod.m_RaycastTarget = false;
            __instance.seasonPeriod.raycastTarget = false;
        }
    }
}
