Shader "Darjo/Tire" {
	Properties {
		_ThreadTex ("Thread", 2D) = "white" {}
		_SideTex ("Side", 2D) = "white" {}
		_TireColor ("TireColor",Color) = (1,1,1,1)
		_ThreadIntensity ("ThreadIntensity",Float) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		
		Pass {
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"
				
				struct appdata {
		            float4 vertex : POSITION;
		            float2 texcoord1 : TEXCOORD0;
		            float2 texcoord2 : TEXCOORD1;
		        };
		        
				struct v2f {
					float4 pos : SV_POSITION;
					float2 texcoord1 : TEXCOORD0;
					float2 texcoord2 : TEXCOORD1;
				};
			
				v2f vert(appdata v) {
					v2f o;
					o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
					o.texcoord1 = v.texcoord1;
					o.texcoord2 = v.texcoord2;
					return o;
				}
			
				sampler2D _ThreadTex;
				sampler2D _SideTex;
				half4 _TireColor;
				half _ThreadIntensity;
			
				float4 frag(v2f i) : COLOR {
					return lerp(1,tex2D(_ThreadTex,i.texcoord1).r,_ThreadIntensity) * tex2D(_SideTex,i.texcoord2) * _TireColor;
				}
			ENDCG
		}
	}
}