Shader "Unlit/Shader"
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
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			//Vertex shader
			v2f vert (appdata v)
			{
				v.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				return v;
			}
			
			//Fragment shader - This is where we want to modify colors
			fixed4 frag (v2f i) : COLOR
			{
				//Get the color of the texture
				fixed4 col = tex2D(_MainTex, i.uv);

				//Modify the color how we want
				//Colors contain r, g, b, and a values

				/* Transparency + Background Pattern Approach */
				/*
				if (col.b/col.r < 0.8 && col.g/col.r < 0.8) { //Thresholds can be adjusted -- lower = more strict
					col.a = 1 - col.r/(6*(col.r+col.g+col.b)); //The constant in the denominator compresses the alpha range
				}
				*/

				/* Mapping attempt for deuteranomalous type */
				col.b = (col.b + 2 * col.g) / 3;
				col.g = col.g/2;
				return col;
			}
			ENDCG
		}
	}
}
