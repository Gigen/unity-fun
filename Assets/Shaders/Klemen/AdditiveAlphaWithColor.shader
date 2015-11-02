Shader "Klemen/AdditiveAlphaWithColor" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_Texture ("RefractionTexture", 2D) = "white" {}
	}
	SubShader {
		Tags { "Queue" = "Transparent" }
		GrabPass { }
		Pass {
			Blend One One
			CGPROGRAM
			#pragma vertex vert
	        #pragma fragment frag

			float4 _Color;
			sampler2D _Texture;

			struct vertexInput {
	            float4 vertex : POSITION;
	            float2 texcoord : TEXCOORD0;
	        };

	        struct vertexOutput {
	            float4 pos : SV_POSITION;
	            float2 tex : TEXCOORD0;
	        };

			 vertexOutput vert (vertexInput v)
	        {
	            vertexOutput o;
	            
	            o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
	            o.tex = v.texcoord;
	            
	            return o;
	        }
	         
	        fixed4 frag (vertexOutput i) : COLOR
	        {
	        	return tex2D(_Texture, i.tex).a * _Color;
	        }
			
			ENDCG
		}
	} 
}
