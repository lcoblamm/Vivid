Shader "Unlit/RedGreen"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_SliderValue ("Value from 0 to 1 from UI Slider", Range (0,1)) = 0
		_GreenWeight ("Green content in blue component", Range (0,3)) = 0
		_GreenScaling ("Green Scaling", Range(1,3)) = 1
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

			fixed _GreenWeight;
			fixed _GreenScaling;
			fixed _SliderValue;

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
				v2f ret;
				ret.vertex = v.vertex;
				ret.uv = v.uv;
				return ret;
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
				_GreenWeight = _SliderValue * 3;
				_GreenScaling = 1 + _SliderValue * 2;
				col.b = ((3 - _GreenWeight) * col.b + _GreenWeight * col.g) / 3;
				col.g = col.g/_GreenScaling;
				return col;


			}
			ENDCG
		}
	}
}
