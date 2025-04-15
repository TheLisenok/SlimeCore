Shader "Custom/RealTimePixelateScreenAligned"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PixelSize ("Pixel Size", Float) = 10.0
        _ScreenParams ("Screen Params", Vector) = (1,1,1,1) // Передаётся из кода
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float2 screenUV : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _PixelSize;
            float4 _ScreenParams; // (Screen Width, Screen Height, 1/Screen Width, 1/Screen Height)

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                // Преобразуем clip-space координаты в нормализованные экранные UV
                o.screenUV = o.vertex.xy / o.vertex.w * 0.5 + 0.5;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Делаем пикселизацию относительно экрана
                float2 pixelatedUV = i.screenUV * _ScreenParams.xy * _PixelSize;
                pixelatedUV = floor(pixelatedUV) / _PixelSize;

                // Выбираем цвет из текстуры
                fixed4 col = tex2D(_MainTex, pixelatedUV);
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
