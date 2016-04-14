Shader "Unlit/HighContrast"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Blend SrcAlpha OneMinusSrcAlpha //This allows for transparency of the image

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			//Vertex shader
			v2f vert (appdata v)
			{
				v.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				v2f ret;
				ret.vertex = v.vertex;
				ret.uv = v.uv;
				return ret;
			}
			
			//Fragment shader
			fixed4 frag (v2f i) : SV_Target
			{
				//Get the color of the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed r = col.r;
				fixed g = col.g;
				fixed b = col.b;
				if (0.299 * r + 0.587 * g + 0.114 * b > 0.3) {
					col.r = sqrt(r);
					col.g = sqrt(g);
					col.b = sqrt(b);
				}
				else {
					col.r = pow(r, 2);
					col.g = pow(g, 2);
					col.b = pow(b, 2);
				}
				return col;
			}
			ENDCG
		}
	}
}
