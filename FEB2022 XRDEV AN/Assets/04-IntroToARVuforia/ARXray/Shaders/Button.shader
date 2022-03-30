//Shader "Custom/Button"
//{
//    SubShader
//    {
//        Tags { "Queue"="Transparent+1" }
//        ZTest Always
//        Pass { Blend SrcAlpha OneMinusSrcAlpha }
//    }
//}

Shader "Custom/Organ Button"
{
   Properties {
      _Color ("Main Color", Color) = (1, 1, 1, 1)
      _MainTex ("Base (RGB)", 2D) = "white"
   }
   SubShader {
      Tags { "Queue"="Transparent" }
      ZTest Off
      Pass {
         Blend SrcAlpha OneMinusSrcAlpha
         SetTexture [_MainTex] {
            constantColor [_Color]
            Combine texture * constant
         }
      }
   }
}