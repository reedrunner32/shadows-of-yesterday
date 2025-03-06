Shader "Custom/CRTShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PixelSize ("Pixel Size", Float) = 8
        _ScanlineIntensity ("Scanline Intensity", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _PixelSize;
            float _ScanlineIntensity;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 pixelSize = _PixelSize * _MainTex_TexelSize.xy;
                float2 uv = floor(i.uv / pixelSize) * pixelSize;
                fixed4 col = tex2D(_MainTex, uv);

                // Add scanlines effect
                float scanline = sin(i.uv.y * _ScreenParams.y * 3.1415) * 0.5 + 0.5;
                scanline = lerp(1, scanline, _ScanlineIntensity);
                col.rgb *= scanline;

                return col;
            }
            ENDCG
        }
    }
}