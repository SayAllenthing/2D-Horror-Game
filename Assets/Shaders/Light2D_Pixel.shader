// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/Light2D_pixel"
{
	Properties
	{
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
		_Source ("Source", vector) = (0,0,0,0)
		_LoY ("Opaque Y", float) = -10
		_HiY ("Distance", float) = 5
		_Color ("Main Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		Blend SrcAlpha OneMinusSrcAlpha
    	ZWrite Off
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			uniform float4 _MainTex_ST;

			sampler2D _MainTex;
			float4 _Source;
			half _HiY;
			half _LoY;
			fixed4 _Color;

			struct VertexOutput
			{
				float4 posWorld : TEXCOORD0;
				float4 pos : SV_POSITION;
			};
			
			VertexOutput vert (appdata_base v)
			{
				VertexOutput o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.posWorld = mul(unity_ObjectToWorld, v.vertex);
				return o;
			}
			
			fixed4 frag (VertexOutput i) : COLOR
			{
				// sample the texture
				fixed4 col;

				float len = length(i.posWorld - _Source.xy);
			   	float alpha = 1 - saturate((len - _LoY) / (_HiY - _LoY));

				col = float4(_Color.r,_Color.g,_Color.b, alpha);

				return col;
			}
			ENDCG
		}
	}
}
