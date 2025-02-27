using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LimbusLocalizeRUS
{
    public static class Pretendard_Hardcoded
    {
        public static Material CommonGlow(Material pretendard)
        {
            pretendard.shader = Shader.Find("TextMeshPro/Distance Field");
            pretendard.SetColor("_FaceColor", new Color(1.720795f, 1.720795f, 1.720795f, 1f));
            pretendard.EnableKeyword("UNDERLAY_ON");
            pretendard.SetColor("_UnderlayColor", new Color(1f, 0.3882353f, 0, 1f));
            pretendard.SetFloat("_UnderlayDilate", 0.45f);
            pretendard.SetFloat("_UnderlaySoftness", 0.7f);

            pretendard.name = "Pretendard-Regular SDF CommonGlow Hardcoded";
            return pretendard;
        }
        public static Material CreditLyrics(Material pretendard)
        {
            pretendard.shader = Shader.Find("TextMeshPro/Distance Field");
            pretendard.SetColor("_FaceColor", new Color(0.9215686f, 0.7921569f, 0.6352941f, 1f));
            pretendard.EnableKeyword("UNDERLAY_ON");
            pretendard.SetColor("_UnderlayColor", new Color(0.6313726f, 0.4156863f, 0.2313726f, 1f));
            pretendard.SetFloat("_UnderlayDilate", 0.3f);
            pretendard.SetFloat("_UnderlaySoftness", 0.6f);

            pretendard.name = "Pretendard-Regular SDF CreditLyrics Hardcoded";
            return pretendard;
        }
        public static Material GachaEgoGlow(Material pretendard)
        {
            pretendard.shader = Shader.Find("TextMeshPro/Distance Field");
            pretendard.SetColor("_FaceColor", new Color(0.5f, 0.5f, 0.5f, 0.5686275f));
            pretendard.EnableKeyword("UNDERLAY_ON");
            pretendard.SetColor("_UnderlayColor", new Color(0, 0.9883373f, 0.2276798f, 0.5490196f));
            pretendard.SetFloat("_UnderlayDilate", 0.95f);
            pretendard.SetFloat("_UnderlaySoftness", 0.95f);

            pretendard.name = "Pretendard-Regular SDF Gacha EgoGlow Hardcoded";
            return pretendard;
        }
        public static Material GachaPersonalityGlow(Material pretendard)
        {
            pretendard.shader = Shader.Find("TextMeshPro/Distance Field");
            pretendard.SetColor("_FaceColor", new Color(1.945098f, 1.521569f, 0, 0.6980392f));
            pretendard.EnableKeyword("UNDERLAY_ON");
            pretendard.SetColor("_UnderlayColor", new Color(1.283286f, 0.7070821f, 0, 0.4f));
            pretendard.SetFloat("_UnderlayDilate", 0.95f);
            pretendard.SetFloat("_UnderlaySoftness", 0.95f);

            pretendard.name = "Pretendard-Regular SDF Gacha PersonalityGlow Hardcoded";
            return pretendard;
        }
        public static Material GoldenBranch(Material pretendard)
        {
            pretendard.shader = Shader.Find("TextMeshPro/Distance Field");
            pretendard.EnableKeyword("GLOW_ON");
            pretendard.SetColor("_GlowColor", new Color(0.4862745f, 0.3803922f, 0, 1f));
            pretendard.SetFloat("_GlowOuter", 1f);
            pretendard.SetFloat("_GlowPower", 0.5f);

            pretendard.name = "Pretendard-Regular SDF GoldenBranch Hardcoded";
            return pretendard;
        }
        public static Material Lyrics(Material pretendard)
        {
            pretendard.shader = Shader.Find("TextMeshPro/Distance Field");
            pretendard.EnableKeyword("UNDERLAY_ON");
            pretendard.SetColor("_UnderlayColor", new Color(0, 0, 0, 0.5019608f));
            pretendard.SetFloat("_UnderlayDilate", 0.91f);
            pretendard.EnableKeyword("GLOW_ON");
            pretendard.SetColor("_GlowColor", new Color(1f, 0.6392157f, 0, 0.5019608f));
            pretendard.SetFloat("_GlowOffset", 0.96f);
            pretendard.SetFloat("_GlowInner", 0.05f);
            pretendard.SetFloat("_GlowOuter", 0.06f);
            pretendard.SetFloat("_GlowPower", 0.503f);

            pretendard.name = "Pretendard-Regular SDF Lyrics Hardcoded";
            return pretendard;
        }
        public static Material Double(Material pretendard)
        {
            pretendard.shader = Shader.Find("TextMeshPro/Distance Field");
            pretendard.SetColor("_OutlineColor", new Color(0.003921569f, 0, 0.003921569f, 1f));
            pretendard.SetFloat("_OutlineWidth", 0.2f);
            pretendard.EnableKeyword("UNDERLAY_ON");
            pretendard.SetColor("_UnderlayColor", new Color(0.5f, 0.5f, 0.5f, 1f));
            pretendard.SetFloat("_UnderlayOffsetX", 0.75f);
            pretendard.SetFloat("_UnderlayOffsetY", -0.67f);

            pretendard.name = "Pretendard-Regular SDF Material Double Hardcoded";
            return pretendard;
        }
        public static Material passiveGlow(Material pretendard)
        {
            pretendard.shader = Shader.Find("TextMeshPro/Distance Field");
            pretendard.EnableKeyword("GLOW_ON");
            pretendard.SetColor("_GlowColor", new Color(1f, 0.3960784f, 0.05882353f, 0.5803922f));
            pretendard.SetFloat("_GlowInner", 1f);
            pretendard.SetFloat("_GlowOuter", 0.8f);
            pretendard.SetFloat("_GlowPower", 1f);

            pretendard.name = "Pretendard-Regular SDF Material passiveGlow Hardcoded";
            return pretendard;
        }
        public static Material OutLine(Material pretendard)
        {
            pretendard.shader = Shader.Find("TextMeshPro/Distance Field");
            pretendard.EnableKeyword("UNDERLAY_ON");
            pretendard.SetColor("_UnderlayColor", new Color(0, 0, 0, 1f));
            pretendard.SetFloat("_UnderlayDilate", 0.3f);

            pretendard.name = "Pretendard-Regular SDF OutLine Hardcoded";
            return pretendard;
        }
        public static Material RedDarkGlow(Material pretendard)
        {
            pretendard.shader = Shader.Find("TextMeshPro/Distance Field");
            pretendard.SetColor("_FaceColor", new Color(0.01568628f, 0, 0.003921569f, 1f));
            pretendard.SetColor("_OutlineColor", new Color(0, 0, 0, 0.5803922f));
            pretendard.SetFloat("_OutlineWidth", 0.221f);
            pretendard.EnableKeyword("UNDERLAY_ON");
            pretendard.SetColor("_UnderlayColor", new Color(0, 0, 0, 0.5019608f));
            pretendard.SetFloat("_UnderlayOffsetY", -0.05f);
            pretendard.SetFloat("_UnderlayDilate", 0.71f);
            pretendard.SetFloat("_UnderlaySoftness", 0.06f);
            pretendard.EnableKeyword("BEVEL_ON");
            pretendard.EnableKeyword("GLOW_ON");
            pretendard.SetColor("_GlowColor", new Color(2.4f, 0, 0, 0.5803922f));
            pretendard.SetFloat("_GlowOffset", 1f);
            pretendard.SetFloat("_GlowInner", 0.291f);
            pretendard.SetFloat("_GlowOuter", 0.253f);
            pretendard.SetFloat("_GlowPower", 0.368f);

            pretendard.name = "Pretendard-Regular SDF RedDarkGlow Hardcoded";
            return pretendard;
        }
        public static Material UnderLine(Material pretendard)
        {
            pretendard.shader = Shader.Find("TextMeshPro/Distance Field");
            pretendard.EnableKeyword("UNDERLAY_ON");
            pretendard.SetColor("_UnderlayColor", new Color(0.01568628f, 0, 0.003921569f, 1f));
            pretendard.SetFloat("_UnderlayOffsetX", 1f);
            pretendard.SetFloat("_UnderlayOffsetY", -1f);

            pretendard.name = "Pretendard-Regular SDF UnderLine Hardcoded";
            return pretendard;
        }
        // HIDDEN CODE FOR CYRILLIC_FONT

        ////switch (__instance.fontMaterial.name)
        ////        {
        ////            case "Pretendard-Regular SDF CommonGlow":
        ////                value = Pretendard_Hardcoded.CommonGlow(__instance.m_fontAsset.material);
        ////                break;
        ////            case "Pretendard-Regular SDF CreditLyrics":
        ////                value = Pretendard_Hardcoded.CreditLyrics(__instance.m_fontAsset.material);
        ////                break;
        ////            case "Pretendard-Regular SDF Gacha EgoGlow":
        ////                value = Pretendard_Hardcoded.GachaEgoGlow(__instance.m_fontAsset.material);
        ////                break;
        ////            case "Pretendard-Regular SDF Gacha PersonalityGlow":
        ////                value = Pretendard_Hardcoded.GachaPersonalityGlow(__instance.m_fontAsset.material);
        ////                break;
        ////            case "Pretendard-Regular SDF GoldenBranch":
        ////                value = Pretendard_Hardcoded.GoldenBranch(__instance.m_fontAsset.material);
        ////                break;
        ////            case "Pretendard-Regular SDF Lyrics":
        ////                value = Pretendard_Hardcoded.Lyrics(__instance.m_fontAsset.material);
        ////                break;
        ////            case "Pretendard-Regular SDF Material Double":
        ////                value = Pretendard_Hardcoded.Double(__instance.m_fontAsset.material);
        ////                break;
        ////            case "Pretendard-Regular SDF Material passiveGlow":
        ////                value = Pretendard_Hardcoded.passiveGlow(__instance.m_fontAsset.material);
        ////                break;
        ////            case "Pretendard-Regular SDF OutLine":
        ////                value = Pretendard_Hardcoded.OutLine(__instance.m_fontAsset.material);
        ////                break;
        ////            case "Pretendard-Regular SDF RedDarkGlow":
        ////                value = Pretendard_Hardcoded.RedDarkGlow(__instance.m_fontAsset.material);
        ////                break;
        ////            case "Pretendard-Regular SDF UnderLine":
        ////                value = Pretendard_Hardcoded.UnderLine(__instance.m_fontAsset.material);
        ////                break;
        ////            default:
        ////                value = __instance.m_fontAsset.material;
        ////                break;
        ////        }
}
}
