Shader "IndieFX/Shaders/FX MotionBlur" {
	Properties {
		_Color ("Color", Color) = (0.0, 0.0, 0.0, 1.0)
		_MainTex ("Base (RGB)", 2D) = "white" { }
	}
	
	CGINCLUDE
	#include "UnityCG.cginc"
	
	sampler2D _MainTex;
	sampler2D _BlurTex;
	float4 _Color;
	uniform float _Threshold;
	
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
		float4 col = tex2D(_MainTex, i.uv) * _Color;
		
		return col;
	}
	ENDCG
	
	SubShader {
		Tags { "Queue" = "Transparent" }
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			Lighting Off
			
			SetTexture [_MainTex] {
				Combine texture
			}
		}
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
}
