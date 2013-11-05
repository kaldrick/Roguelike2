Shader "IndieFX/Shaders/FX Blur" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" { }
	}
	
	CGINCLUDE
	#include "UnityCG.cginc"
	
	sampler2D _MainTex;
	float4 _MainTex_TexelSize;
	
	struct v2f_m {
		float4 pos : POSITION;
		float2 uv[2] : TEXCOORD0;
	};
	
	v2f_m vertV (appdata_img v)
	{
		v2f_m o;
		
		o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
		float2 uv = v.texcoord.xy;
		
		float2 right = float2(_MainTex_TexelSize.x, 0.0);
			
		o.uv[0].xy = float2(_MainTex_TexelSize.x, 0.0);
		o.uv[1].xy = uv;
		
		return o;
	}
	
	v2f_m vertH (appdata_img v)
	{
		v2f_m o;
		
		o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
		float2 uv = v.texcoord.xy;
		
		float2 up = float2(0.0, _MainTex_TexelSize.y);
			
		o.uv[0].xy = float2(0.0, _MainTex_TexelSize.y);
		o.uv[1].xy = uv;
		
		return o;
	}
	
	half4 fragV (v2f_m i) : COLOR
	{
		
		float4 col = tex2D(_MainTex, i.uv[1]);
		int radius = 10;
		int count = 0;
		float4 newCol;
		newCol.rgb = (0, 0, 0);
		for (int pix = 0; pix < radius; pix ++) {
			
			float2 uvOff = i.uv[0] * pix;
			float4 tmpCol = tex2D(_MainTex, i.uv[1] + uvOff);
			if (i.uv[1].x + uvOff.x <= 1.0) {
				newCol += tmpCol;
				count ++;
			}
			
			tmpCol = tex2D(_MainTex, i.uv[1] - uvOff);
			if (i.uv[1].x - uvOff.x >= 0.0) {
				newCol += tmpCol;
				count ++;
			}
			
		}
		
		newCol = (newCol + col) / (count + 1);
		
		return newCol;
	}
	
	half4 fragH (v2f_m i) : COLOR
	{
		
		float4 col = tex2D(_MainTex, i.uv[1]);
		int radius = 10;
		int count = 0;
		float4 newCol;
		newCol.rgb = (0, 0, 0);
		for (int pix = 0; pix < radius; pix ++) {
			
			float2 uvOff = i.uv[0] * pix;
			float4 tmpCol = tex2D(_MainTex, i.uv[1] + uvOff);
			if (i.uv[1].y + uvOff.y <= 1.0) {
				newCol += tmpCol;
				count ++;
			}
			
			tmpCol = tex2D(_MainTex, i.uv[1] - uvOff);
			if (i.uv[1].y - uvOff.y >= 0.0) {
				newCol += tmpCol;
				count ++;
			}
			
		}
		
		newCol = (newCol + col) / (count + 1);
		
		return newCol;
	}
	ENDCG
	
	SubShader {
		Tags { "Queue"="Overlay" "RenderType"="Overlay"}
		
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			Lighting Off
			
			CGPROGRAM
			#pragma vertex vertH
			#pragma fragment fragH
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma target 3.0
			
			ENDCG
		}
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			Lighting Off
			
			CGPROGRAM
			#pragma vertex vertV
			#pragma fragment fragV
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma target 3.0
			
			ENDCG
		}
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			Lighting Off
			
			CGPROGRAM
			#pragma vertex vertH
			#pragma fragment fragH
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma target 3.0
			
			ENDCG
		}
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			Lighting Off
			
			CGPROGRAM
			#pragma vertex vertV
			#pragma fragment fragV
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma target 3.0
			
			ENDCG
		}
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			Lighting Off
			
			CGPROGRAM
			#pragma vertex vertH
			#pragma fragment fragH
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma target 3.0
			
			ENDCG
		}
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			Lighting Off
			
			CGPROGRAM
			#pragma vertex vertV
			#pragma fragment fragV
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma target 3.0
			
			ENDCG
		}
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			Lighting Off
			
			CGPROGRAM
			#pragma vertex vertH
			#pragma fragment fragH
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma target 3.0
			
			ENDCG
		}
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			Lighting Off
			
			CGPROGRAM
			#pragma vertex vertV
			#pragma fragment fragV
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma target 3.0
			
			ENDCG
		}
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			Lighting Off
			
			CGPROGRAM
			#pragma vertex vertH
			#pragma fragment fragH
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma target 3.0
			
			ENDCG
		}
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			Lighting Off
			
			CGPROGRAM
			#pragma vertex vertV
			#pragma fragment fragV
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma target 3.0
			
			ENDCG
		}
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			Lighting Off
			
			CGPROGRAM
			#pragma vertex vertH
			#pragma fragment fragH
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma target 3.0
			
			ENDCG
		}
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			Lighting Off
			
			CGPROGRAM
			#pragma vertex vertV
			#pragma fragment fragV
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma target 3.0
			
			ENDCG
		}
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			Lighting Off
			
			CGPROGRAM
			#pragma vertex vertH
			#pragma fragment fragH
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma target 3.0
			
			ENDCG
		}
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			Lighting Off
			
			CGPROGRAM
			#pragma vertex vertV
			#pragma fragment fragV
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma target 3.0
			
			ENDCG
		}
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			Lighting Off
			
			CGPROGRAM
			#pragma vertex vertH
			#pragma fragment fragH
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma target 3.0
			
			ENDCG
		}
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			Lighting Off
			
			CGPROGRAM
			#pragma vertex vertV
			#pragma fragment fragV
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma target 3.0
			
			ENDCG
		}
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			Lighting Off
			
			CGPROGRAM
			#pragma vertex vertH
			#pragma fragment fragH
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma target 3.0
			
			ENDCG
		}
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			Lighting Off
			
			CGPROGRAM
			#pragma vertex vertV
			#pragma fragment fragV
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma target 3.0
			
			ENDCG
		}
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			Lighting Off
			
			CGPROGRAM
			#pragma vertex vertH
			#pragma fragment fragH
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma target 3.0
			
			ENDCG
		}
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			Lighting Off
			
			CGPROGRAM
			#pragma vertex vertV
			#pragma fragment fragV
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
	Fallback off
}
