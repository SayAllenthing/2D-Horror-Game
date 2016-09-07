Shader "Custom/TextureAlphaShader" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 200
        Lighting OFF
       
        CGPROGRAM
        #pragma surface surf Unlit alpha noambient novertexlights nodirlightmap nolightmap noforwardadd
 
        half4 LightingUnlit (SurfaceOutput s, half3 dir, half atten) {
            fixed4 c;
            c.rgb = s.Albedo;
            c.a = s.Alpha;
            return c;
        }
 
        sampler2D _MainTex;
 
        struct Input {
            float2 uv_MainTex;
        };
 
        void surf (Input IN, inout SurfaceOutput o) {
            half4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = 0;//c.rgb;


            o.Alpha = IN.uv_MainTex.x;
           
        }
        ENDCG
    }
    Fallback "Diffuse"
}