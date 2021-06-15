Shader "Effect/fisheye"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Factor_x("x轴方向畸变", Range(0, 0.15)) = 0
        _Factor_y("y 轴方向畸变", Range(0, 0.15)) = 0
        _Scale("畸变范围", Float) = 1
        [Toggle] _UseTex("使用原始贴图", Int) = 0
    }
        SubShader
        {
            // No culling or depth
            Cull Off ZWrite Off ZTest Always

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                // shader_feature 可能在真机上会失效
                #pragma multi_compile _USETEX_ON

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
                };

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }


                sampler2D _MainTex;
                float _Factor_x;
                float _Factor_y;
                float _Scale;

                inline fixed2 CalEyeUV(fixed2 uv)
                {
                    float2 uv0 = (uv - 0.5) * 2.0;
                    float2 uv1;
                    uv1.x = (1 - uv0.y * uv0.y) * _Factor_y * (uv0.x);
                    uv1.y = (1 - uv0.x * uv0.x) * _Factor_x * (uv0.y);
                    return(uv - uv1);
                }


                fixed4 frag(v2f i) : SV_Target
                {
                    #if _USETEX_ON 
                        float2 eyeUV = CalEyeUV(i.uv);
                        fixed4 col = tex2D(_MainTex, eyeUV);
                    #else
                        fixed4 col = tex2D(_MainTex, i.uv);
                    #endif
                    return col;
                }
                ENDCG
            }
        }
}
