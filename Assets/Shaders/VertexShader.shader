
Shader "Custom/VertexShader" {
	Properties {
		_PeakTex ("PeakTexture", 2D) = "white" {}
		_LevelTex ("LevelTexture", 2D) = "white" {}
		_WaterTex ("WaterTexture", 2D) = "white" {}
   		_PeakColor ("PeakColor", Color) = (1,0,1,1)
	    _PeakLevel ("PeakLevel", Float) = 200
	    _Level3Color ("Level3Color", Color) = (1,0,0,1)
	    _Level3 ("Level3", Float) = 198
	    _Level2Color ("Level2Color", Color) = (0,0,1,1)
	    _Level2 ("Level2", Float) = 197.5
	    _Level1Color ("Level1Color", Color) = (1,1,0,1)
	    _WaterLevel ("WaterLevel", Float) = 197
	    _WaterColor ("WaterColor", Color) = (0,1,1,1)
	    _Slope ("Slope Fader", Range (0,1)) = 0.25
	    _CentrePoint ("Centre", Vector) = (0, 0, 0, 0)
	}
	SubShader {
	    Tags { "RenderType" = "Opaque" }
	    Fog { Mode Off }
	    Tags { "LightMode" = "Always" }
	    CGPROGRAM
	    #pragma surface surf Lambert vertex:vert
	    #pragma target 3.0
	    
	    struct Input {
	    	float2 uv_PeakTex;
	    	float2 uv_LevelTex;
	    	float2 uv_WaterTex;
	        float3 customColor;
	        float3 worldPos;
	        float3 viewDir;
	    };
	    void vert (inout appdata_full v, out Input o){
	    	UNITY_INITIALIZE_OUTPUT(Input,o);
	        o.customColor = abs(v.normal);
	    }
	    float _PeakLevel;
	    float4 _PeakColor;
	    float _Level3;
	    float4 _Level3Color;
	    float _Level2;
	    float4 _Level2Color;
	    float _Level1;
	    float4 _Level1Color;
	    float _Slope;
	    float _WaterLevel;
	    float4 _WaterColor;
	    float4 _CentrePoint;
	    float _worldPos;
		float4 _RimColor;
	    float _RimPower;
	    sampler2D _PeakTex;
	    sampler2D _LevelTex;
	    sampler2D _WaterTex;	    
	    void surf (Input IN, inout SurfaceOutput o) {
	    	if (distance(_CentrePoint.y, IN.worldPos.y) >= _PeakLevel)
	    	{
	    		o.Albedo = tex2D (_PeakTex, IN.uv_PeakTex).rgb;
	            o.Albedo.rgb *= _PeakColor;
	        }
            if (distance(_CentrePoint.y, IN.worldPos.y) <= _PeakLevel)
            {
            	o.Albedo = tex2D (_PeakTex, IN.uv_PeakTex).rgb;
	            o.Albedo *= lerp(_Level3Color, _PeakColor, (distance(_CentrePoint.y, IN.worldPos.y) - _Level3)/(_PeakLevel - _Level3));
	        }    
	        if (distance(_CentrePoint.y, IN.worldPos.y) <= _Level3)
	        {
	         	o.Albedo = tex2D (_LevelTex, IN.uv_LevelTex).rgb;
	            o.Albedo *= lerp(_Level2Color, _Level3Color, (distance(_CentrePoint.y, IN.worldPos.y) - _Level2)/(_Level3 - _Level2));
	        }
	        if (distance(_CentrePoint.y, IN.worldPos.y) <= _Level2)
	        {
	        	o.Albedo = tex2D (_LevelTex, IN.uv_LevelTex).rgb;
	            o.Albedo *= lerp(_WaterColor, _Level1Color, (distance(_CentrePoint.y, IN.worldPos.y) - _WaterLevel)/(_Level2 - _WaterLevel));
	        }
	        if (distance(_CentrePoint.y, IN.worldPos.y) <= _WaterLevel)
	        {
	        	o.Albedo = tex2D (_WaterTex, IN.uv_WaterTex).rgb;
	            o.Albedo *= _WaterColor;
	        }
	        o.Albedo *= saturate(IN.customColor + _Slope);      
	    }
	    ENDCG
	}
	SubShader{
		Tags { "RenderType" = "Opaque" }
		CGPROGRAM
		#pragma surface surf BlinnPhong

	      struct Input {
	          float2 uv_MainTex;
	      };
	      sampler2D _MainTex;
	      void surf (Input IN, inout SurfaceOutput o) {
	          o.Specular = 1;
	      }
      ENDCG
	}
	Fallback "Diffuse"
}