Shader "Custom/FlashLight" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_Darkness ("Darkness", float) = 0.9
}

SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 200

CGPROGRAM
#pragma surface surf Lambert alpha:fade

sampler2D _MainTex;
fixed4 _Color;
float _Darkness;

struct Input {
	float2 uv_MainTex;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex);

	if(c.a > 0.5)
	{
		o.Albedo = _Color.rgb;
		o.Alpha = 0.2;
	}
	else
	{		
		o.Albedo = float3(0,0,0);
		o.Alpha = _Darkness;
	}




}
ENDCG
}

Fallback "Legacy Shaders/Transparent/VertexLit"
}
