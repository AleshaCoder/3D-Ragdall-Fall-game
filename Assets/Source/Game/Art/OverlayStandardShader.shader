Shader "Custom/OverlayStandardShader"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0.0, 1.0)) = 0.5
        _Metallic("Metallic", Range(0.0, 1.0)) = 0.0
    }
        SubShader
        {
            Tags { "Queue" = "Overlay" } // Объект рендерится после всех других
            LOD 200

            Pass
            {
                ZTest Always // Отключаем тест глубины, объект рендерится поверх всего
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
                    float4 pos : SV_POSITION;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                fixed4 _Color;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // Получаем цвет текстуры
                    fixed4 texColor = tex2D(_MainTex, i.uv) * _Color;
                    return texColor;
                }
                ENDCG
            }
        }
            FallBack "Diffuse"
}
