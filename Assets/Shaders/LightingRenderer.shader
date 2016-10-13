Shader "Custom/Lighting Renderer" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_Darkness ("Darkness", float) = 0.9
}

SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 200

CGPROGRAM
#pragma surface surf Lambert alpha:blend

sampler2D _MainTex;
fixed4 _Color;
float _Darkness;

struct Input {
	float2 uv_MainTex;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex);

	float diff = length(c.rgb - float3(1,1,1));

	o.Albedo = _Color.rgb * clamp(diff, 0, 0.4);
	o.Alpha = clamp(_Darkness - (diff), 0.5, _Darkness);
}
ENDCG
}

Fallback "Legacy Shaders/Transparent/VertexLit"
}
