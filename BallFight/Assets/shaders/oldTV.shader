// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Screen/OldTV" {
	Properties
	{
		//原图
		_MainTex("MainTex", 2D) = "white" {}

	//划痕
	 _ScratchesTex("ScratchesTex", 2D) = "white" {}
	 _ScratchesXSpeed("ScratchesXSpeed", float) = 100
	 _ScratchesYSpeed("ScratchesYSpeed", float) = 100
		 //灰尘
		 _DustTex("DustTex", 2D) = "white" {}
		 _DustXSpeed("_DustXSpeed", float) = 100
		 _DustYSpeed("_DustYSpeed", float) = 100

		 _RandomValue("RandomValue", float) = 1.0
		 _EffectAmount("Old Film Effect Amount", Range(0, 1)) = 1
	}

		SubShader
		 {
			 // No culling or depth
			 Cull Off ZWrite Off ZTest Always
			 Tags{"RenderType" = "Transparent"}
			 Pass 
			 {
			     Blend SrcAlpha OneMinusSrcAlpha
			     CGPROGRAM
			     #pragma vertex vert
			     #pragma fragment frag

			     #include "UnityCG.cginc"

			     sampler2D _MainTex;
			     float4 _MainTex_ST;

			     sampler2D _ScratchesTex;
			     sampler2D _DustTex;

			     float _EffectAmount;
			     float _RandomValue;

			     float _ScratchesXSpeed;
			     float _ScratchesYSpeed;
			     float _DustXSpeed;
			     float _DustYSpeed;

				 fixed4 _SepiaColor;

				 struct v2f 
				 {
					 float4 pos : SV_POSITION;
					 float2 uv : TEXCOORD0;
				 };

				 v2f vert(appdata_base v)
				 {
					 v2f o;
					 o.pos = UnityObjectToClipPos(v.vertex);
					 o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
					 return o;
				 }


			      fixed4 frag(v2f i) : COLOR
			     {
					half2 scratchesUV = half2(i.uv.x + (_RandomValue * _SinTime.z * _ScratchesXSpeed),i.uv.y + (_RandomValue * _Time.x * _ScratchesYSpeed));
                    fixed4 scratchesTex = tex2D(_ScratchesTex, scratchesUV);

                    half2 dustUV = half2(i.uv.x + (_RandomValue * _SinTime.z * _DustXSpeed),
                    i.uv.y + (_Time.x * _DustYSpeed*_SinTime.z));
                    fixed4 dustTex = tex2D(_DustTex, dustUV);

					fixed4 finalColor = tex2D(_MainTex, i.uv);
					fixed3 constantWhite = fixed3(1, 1, 1);
					finalColor.rgb *= scratchesTex;
					finalColor.rgb *= lerp(dustTex, constantWhite, (_RandomValue * _SinTime.z));
					//finalColor = lerp(_MainTex, finalColor, _EffectAmount);
					return finalColor;
			      }

			  ENDCG
			 }
		 }
}
