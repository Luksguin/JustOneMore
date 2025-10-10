Shader "Sprites/Outline"
{
    Properties
    {
        _MainTex("Sprite Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)
        _OutlineColor("Outline Color", Color) = (0,0,0,1)
        _OutlineSize("Outline Size", Range(0,0.1)) = 0.03
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
            "Queue"="Transparent"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float4 _Color;
            float4 _OutlineColor;
            float _OutlineSize;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                fixed4 c = tex2D(_MainTex, uv) * _Color;

                float alpha = c.a;
                float outline = 0.0;

                // amostra em 8 direções para gerar o contorno
                float2 offsets[8] = {
                    float2( _OutlineSize, 0),
                    float2(-_OutlineSize, 0),
                    float2(0,  _OutlineSize),
                    float2(0, -_OutlineSize),
                    float2( _OutlineSize,  _OutlineSize),
                    float2(-_OutlineSize,  _OutlineSize),
                    float2( _OutlineSize, -_OutlineSize),
                    float2(-_OutlineSize, -_OutlineSize)
                };

                for (int n = 0; n < 8; n++)
                {
                    outline += tex2D(_MainTex, uv + offsets[n] * _MainTex_TexelSize.xy).a;
                }

                outline = step(0.01, outline);

                float4 result = c;
                result.rgb = lerp(_OutlineColor.rgb, c.rgb, c.a);
                result.a = max(c.a, outline * _OutlineColor.a);

                return result;
            }
            ENDCG
        }
    }
}
