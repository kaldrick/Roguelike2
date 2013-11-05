Shader "IndieFX/Shaders/FX ColorBalance" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "black" {}
		_Lift ("Lift (RBG)", Color) = (1.0, 1.0, 1.0, 1.0)
		_LiftB ("Bright", Range (0.0, 2.0)) = 1.0
		_Gamma ("Gamma (RBG)", Color) = (1.0, 1.0, 1.0, 1.0)
		_GammaB ("Bright", Range (0.0, 2.0)) = 1.0
		_Gain ("Gain (RBG)", Color) = (1.0, 1.0, 1.0, 1.0)
		_GainB ("Bright", Range (0.0, 2.0)) = 1.0
	}
	
	CGINCLUDE
	#include "UnityCG.cginc"
	
	sampler2D _MainTex;
	fixed4 _Lift;
	float _LiftB;
	fixed4 _Gamma;
	float _GammaB;
	fixed4 _Gain;
	float _GainB;
	
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
		
		_Lift = _Lift * _LiftB;
		_Gamma = _Gamma * _GammaB;
		_Gain = _Gain * _GainB;
		
		col.rgb = pow(_Gain.rgb * (col.rgb + (_Lift.rgb - 1.0) * (1.0 - col.rgb)), 1.0 / _Gamma.rgb);
		
		return col;
	}
	ENDCG
	
	SubShader {
		Pass {
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
	
	FallBack "Diffuse"
}