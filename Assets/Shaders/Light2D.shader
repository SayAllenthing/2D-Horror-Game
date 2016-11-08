// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/Light2D" {
Properties {	
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_Source ("Source", vector) = (0,0,0,0)
	_LoY ("Opaque Y", float) = 0
	_HiY ("Distance", float) = 1
}

SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	Blend SrcAlpha OneMinusSrcAlpha
    ZWrite Off


CGPROGRAM
#pragma surface surf Lambert alpha:blend vertex:myvert

sampler2D _MainTex;
float4 _Source;
half _HiY;
half _LoY;

struct Input {
	float2 uv_MainTex;
	float alpha;
};

void myvert (inout appdata_full v, out Input data) {
       // convert the vertex to world space: 
       float4 worldV = mul (unity_ObjectToWorld, v.vertex);
       // calculate alpha according to the world Y coordinate:

       float len = length(worldV.xy - _Source.xy);

       data.alpha = 1 - saturate((len - _LoY) / (_HiY - _LoY));
       data.uv_MainTex = worldV.xy;
   }

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
				
	o.Albedo = float3(0,0,0);
	o.Alpha = IN.alpha;	
}
ENDCG
}

Fallback "Legacy Shaders/Transparent/VertexLit"
}