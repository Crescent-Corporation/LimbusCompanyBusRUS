using HarmonyLib;
using MainUI;
using MainUI.VendingMachine;
using UnityEngine;

namespace LimbusLocalizeRUS
{
    public static class SeasonUI
    {
        [HarmonyPatch(typeof(MainLobbyBannerSlot), nameof(MainLobbyBannerSlot.Update))]
        [HarmonyPostfix]
        private static void MainLobbyUIPanel_Init(MainLobbyBannerSlot __instance)
        {
            //MAIN MENU

            Sprite banner = __instance.img_main.sprite;
            if (banner.name.Contains("banner_battlepass_season5"))
                __instance.img_main.overrideSprite = ReadmeManager.GetReadmePassSprites("Season5_Banner");
        }
        [HarmonyPatch(typeof(VendingMachineBannerSlot), nameof(VendingMachineBannerSlot.SetData))]
        [HarmonyPostfix]
        private static void VendingMachineBannerSlot_Init(BannerSlot<VendingMachineStaticDataList> __instance)
        {
            switch (__instance._id)
            {
                case 0:
                    __instance._base._bannerImage.m_OverrideSprite = ReadmeManager.GetReadmePassSprites("Base_Shop");
                    break;
                case 5:
                    __instance._base._bannerImage.m_OverrideSprite = ReadmeManager.GetReadmePassSprites("Season5_Shop");
                    break;
                case 4:
                    __instance._base._bannerImage.m_OverrideSprite = ReadmeManager.GetReadmePassSprites("Season4_Shop");
                    break;
                case 3:
                    __instance._base._bannerImage.m_OverrideSprite = ReadmeManager.GetReadmePassSprites("Season3_Shop");
                    break;
                case 2:
                    __instance._base._bannerImage.m_OverrideSprite = ReadmeManager.GetReadmePassSprites("Season2_Shop");
                    break;
                case 1:
                    __instance._base._bannerImage.m_OverrideSprite = ReadmeManager.GetReadmePassSprites("Season1_Shop");
                    break;
                case 91:
                    __instance._base._bannerImage.m_OverrideSprite = ReadmeManager.GetReadmePassSprites("Walpurgis_Shop");
                    break;
            }
        }
        [HarmonyPatch(typeof(BattlePassUIPopup), nameof(BattlePassUIPopup.SetupBaseData))]
        [HarmonyPostfix]
        private static void SeasonPass_Init(BattlePassUIPopup __instance)
        {
            __instance.seasonPeriod.text = "(МСК) 06:00 10.10.2024 ~";

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