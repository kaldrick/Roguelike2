Shader "Vertex Colored Alpha Surf Shader" {

    Properties {

        _MainTex ("Base (RGB)", 2D) = "white" {}

    }

    SubShader {

        Tags { "Queue"="AlphaTest" "IgnoreProjector"="False" "RenderType"="Opaque" }

        LOD 300

        
 
        CGPROGRAM

        #pragma surface surf Lambert alpha vertex:vert fullforwardshadows approxview
		#include "AutoLight.cginc"
       
        sampler2D _MainTex;

  
		struct v2f { 
			V2F_SHADOW_CASTER; 
			float2 uv : TEXCOORD1;
			
		};

        struct Input {

            float2 uv_MainTex;

            float4 color: Color; // Vertex color

        };

 		v2f vert (inout appdata_full v) { 
			v2f o; 
			return o; 
		} 

        void surf (Input IN, inout SurfaceOutput o) {

            half4 c = tex2D (_MainTex, IN.uv_MainTex);

            o.Albedo = c.rgb * IN.color.rgb ; // vertex RGB

            o.Alpha = c.a * IN.color.a ; // vertex Alpha

        }

        ENDCG

    } 

    FallBack "Diffuse"

}