Shader "IndieFX/Shaders/FX BlackAndWhite" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "black" {}
		_Mix ("Mix", Range (0.0, 1.0)) = 1.0
	}
	
	CGINCLUDE
	#include "UnityCG.cginc"
	
	sampler2D _MainTex;
	float _Mix;
	
	struct v2f {
		float4 pos : POSITION;
		float2 uv : TEXCOORD0;
	};
	
	v2f vert (appdata_img v)
	{
		v2f o;
		
		o.pos = mul (UNITY_MATRIX_MVP, v.vertex);

		o.uv = v.texcoord.xy;
		
		return o;
	}
	
	half4 frag (v2f i) : COLOR
	{
		float4 col = tex2D(_MainTex, i.uv);
		
		col.r = (col.r + col.g + col.b) / 3.0;
		col.g = col.r;
		col.b = col.r;
		col.a = _Mix;
		
		return col;
	}
	ENDCG
	
	SubShader {
		Tags { "Queue" = "Overlay" }
		
		Pass {
			Blend SrcAlpha OneMinusSrcAlpha
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			Lighting Off
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma target 3.0
			
			ENDCG
		}
	}
	
	SubShader {
		Tags { "Queue" = "Overlay" }
		
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			Lighting Off
			
			SetTexture [_MainTex] {
				Combine texture
			}
		}
	}
	
	FallBack "Diffuse"
}