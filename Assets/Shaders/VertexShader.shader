Shader "Custom/HeightShader" {
    Properties {
       _MainTex ("Main Texture", 2D) = "white" {}
       _HighMountains("High Mountains", Float) = 5.0
       _LowRivers("Low Rivers", Float) = -5.0
       _CenterHeight ("Center Height", Float) = 0.0
       _MaxVariance ("Maximum Variance", Float) = 3.0
       _HighColor ("Hill Color", Color) = (1.0, 1.0, 1.0, 1.0)
       _HighMountainsColor ("High Mountains Color", Color) = (1.0, 1.0, 1.0, 1.0)
       _LowRiversColor ("Low Rivers Color", Color) = (1.0, 1.0, 1.0, 0.5)
       _LowColor ("Low Color", Color) = (0.0, 0.0, 0.0, 1.0)
       _Outline ("Outline", Range(0, 0.15)) = 0.08
    }
    SubShader {
         Tags {"RenderType"="Opaque"}
         LOD 200
         Cull Off
		 Offset 0, -1
		
 		 CGPROGRAM
         #pragma surface surf SimpleSpecular vertex:vert noambient
         #include <UnityCG.cginc>
 		
         float _CenterHeight;
         float _HighMountains;
         float _LowRivers;
         float _MaxVariance;
         float4 _HighColor;
         float4 _HighMountainsColor;
         float4 _LowRiversColor;
         float4 _LowColor;
         sampler2D _MainTex;
         half4 LightingSimpleSpecular (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {
	          half3 h = normalize (lightDir + viewDir);
	
	          half diff = max (0, dot (s.Normal, lightDir));
	
	          float nh = max (0, dot (s.Normal, h));
	          float spec = pow (nh, 48.0);
	
	          half4 c;
	          if(s.Alpha < 0.9)
	          {
	          	c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec) * (atten * 2);
	          }
	          else
	          {
	          	c.rgb = (s.Albedo * _LightColor0.rgb * diff ) * (atten * 2);
	          }
	          c.a = s.Alpha;
	          return c;
	      }
         struct Input{
          float2 uv_MainTex;
          float4 color : COLOR;
         };
 
         void vert(inout appdata_full v){
          // Convert to world position
          float4 worldPos = mul(_Object2World, v.vertex);
          float diff = worldPos.y - _CenterHeight;
          float cFactor = saturate(diff/(2*_MaxVariance) + 0.5);
          if(diff > _HighMountains) 
          { 
          	 v.color = lerp(_HighColor, _HighMountainsColor, cFactor);
          }
          if(diff < _HighMountains)
          {
          	 v.color = lerp(_LowColor, _HighColor, cFactor);
          }
          if(diff < _LowRivers) 
          {
          	v.color = lerp(_LowRiversColor, _LowColor, cFactor);
          } 
         	v.color = v.color * 1.2;
         } 
 
         void surf(Input IN, inout SurfaceOutput o){ 
               o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * IN.color;
               o.Alpha = IN.color.a;
         }
 
         ENDCG
         Pass {
		 
		    Cull Front
		    Lighting Off
		 	ZWrite On
		    CGPROGRAM
		    #pragma vertex vert
		    #pragma fragment frag
		 
		    #include "UnityCG.cginc"
		    struct a2v
		    {
		        float4 vertex : POSITION;
		        float3 normal : NORMAL;
		    }; 
		 
		    struct v2f
		    {
		        float4 pos : POSITION;
		    };
		 
		    float _Outline;
		 
		    v2f vert (a2v v)
		    {
		        v2f o;
		        o.pos = mul( UNITY_MATRIX_MVP, v.vertex + (float4(v.normal,0) * _Outline));
		        return o;
		    }
		 
		    float4 frag (v2f IN) : COLOR
		    {
		        return float4(0);
		    }
		 
		    ENDCG
		 
		}
       }
    
    FallBack "VertexLit"
}