using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorConverter : MonoBehaviour
{
    public struct CMYK
    {
        public float C;
        public float M;
        public float Y;
        public float K;
    }
    public static CMYK RGB_TO_CMYK (Color rgbcolor)
    {
        //         float R_Inverse = rgbcolor.r / 255;
        //         float G_Inverse = rgbcolor.g / 255;
        //         float B_Inverse = rgbcolor.b / 255;

        float R_Inverse = rgbcolor.r;
        float G_Inverse = rgbcolor.g;
        float B_Inverse = rgbcolor.b; 

        CMYK c;

        c.K = 1 - Mathf.Max(R_Inverse, G_Inverse, B_Inverse);
        c.C = (1 - R_Inverse - c.K) / (1 - c.K);
        c.M = (1 - G_Inverse - c.K) / (1 - c.K);
        c.Y = (1 - B_Inverse - c.K) / (1 - c.K);

        return c;

    }

    public static Color CMYK_TO_RGB(CMYK color)
    {
        Color c = Color.white; ;
        c.r = (1 - color.C) * (1 - color.K);
        c.g =  (1 - color.M) * (1 - color.K);
        c.b =  (1 - color.Y) * (1 - color.K);



        return c;
    }

    public static Color Mix_Color( float mixRatio, Color fg, Color bg)
    {
        Debug.Log("g is : " + fg.g);
      
        Color.RGBToHSV(fg, out float fh, out float fs, out float fv);
        Color.RGBToHSV(bg, out float bh, out float bs, out float bv);

        fg.r = (1 - mixRatio) * fh + mixRatio * bh;
        fg.g = (1 - mixRatio) * fs + mixRatio * bs;
        fg.b = (1 - mixRatio) * fv + mixRatio * bv;
      
        return fg;
    
    }
    

    public static Color MixColor(Color[] colorToMix)
    {



        CMYK[] cmykcolor = new CMYK[colorToMix.Length];
        for (int i = 0; i < colorToMix.Length; i++)
        {
            cmykcolor[i] = RGB_TO_CMYK(colorToMix[i]);
        }

        CMYK mixedCMYK = default;

        for (int i = 0; i < colorToMix.Length; i++)
        {
            mixedCMYK.C += cmykcolor[i].C;
            mixedCMYK.M += cmykcolor[i].M;
            mixedCMYK.Y += cmykcolor[i].Y;
            mixedCMYK.K += cmykcolor[i].K;
        }

        mixedCMYK.C = mixedCMYK.C / cmykcolor.Length;
        mixedCMYK.M = mixedCMYK.M / cmykcolor.Length;
        mixedCMYK.Y = mixedCMYK.Y / cmykcolor.Length;
        mixedCMYK.K = mixedCMYK.K / cmykcolor.Length;

        return CMYK_TO_RGB(mixedCMYK);
        
    }


    public static Color SubtractiveMixColor(Color col1, Color col2, float mixRatio,bool remultiply = false)
    {
        mixRatio = Mathf.Clamp(mixRatio, 0, 1);
        Color mixedColor = col1;

        mixedColor.r = getCHangedValue(col1.r, col2.r, mixRatio);
        mixedColor.g = getCHangedValue(col1.g, col2.g, mixRatio);
        mixedColor.b = getCHangedValue(col1.b, col2.b, mixRatio);

        if (remultiply)
        {
            mixedColor *= 2;
        }
        mixedColor.a = 1;

        Color.RGBToHSV(mixedColor, out float h, out float s, out float v);
        Color.RGBToHSV(col1, out float h1, out float s1, out float v1);
        Color.RGBToHSV(col1, out float h2, out float s2, out float v2);

        Debug.Log("h :" + h);
        Debug.Log("h2 :" + h2);
        Debug.Log("h1 :" + h1);

        //return Color.HSVToRGB(h, (s1 + s2) / 2, (v1 + v2) / 2);

        return mixedColor;
       
        
      

    }

    static float getCHangedValue(float c1, float c2 , float ratio)
    {
        if (c1 * ratio >= c2 * (1-ratio))
        {
            c1 = c1 * ratio - c2 * (1 - ratio);
        }
        else
        {
            c1 = c1 * (1 - ratio) - c2 * ratio;
        }
        return c1;
    }

    public static Color mixRGB_color(Color col1 , Color col2)
    {
        Color mixedCol = Color.black;

        col1 = col1 * col1;
        col2 = col2 * col2;

        mixedCol = col1 + col2;
        mixedCol = mixedCol / 2;
        mixedCol = new Color(Mathf.Sqrt(mixedCol.r), Mathf.Sqrt(mixedCol.g), Mathf.Sqrt(mixedCol.b), Mathf.Sqrt(mixedCol.a));

        return mixedCol;
    }

    public static Color32 mixRGB_color32(Color col1, Color col2)
    {
        Color32 mixedCol = new Color32(0, 0, 0, 0);

        col1 = col1 * col1;
        col2 = col2 * col2;

        mixedCol = col1 + col2;
       // mixedCol = mixedCol / 2;
        mixedCol = new Color(Mathf.Sqrt(mixedCol.r), Mathf.Sqrt(mixedCol.g), Mathf.Sqrt(mixedCol.b), Mathf.Sqrt(mixedCol.a));

        return mixedCol;
    }

    public static Color getMixedColor( Color[] colorstomix)
    {
        Color color = colorstomix[0];

        for (int i = 1; i < colorstomix.Length; i++)
        {
            color = ColorConverter.mixRGB_color(color, colorstomix[i]);
        }

        return color;
    }

}
