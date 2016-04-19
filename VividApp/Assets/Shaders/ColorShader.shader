Shader "Unlit/RedGreen"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_SliderValue ("Value from 0 to 1 from UI Slider", Range (0,1)) = 0
		_FilterWeight ("Filter Weight", Range (0,3)) = 0
		_FilterScaling ("Filter Scaling", Range(1,3)) = 1
		_FilterType ("Filter Type", Float) = 0
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

			fixed _FilterWeight;
			fixed _FilterScaling;
			fixed _SliderValue;
			float _FilterType;

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
				_FilterWeight = _SliderValue * 3;
				_FilterScaling = 1 + _SliderValue * 2;


				/*
				** NOTE: NUMBERING CONVENTION 
				** Vision correction / aid:			0-9
				** Corresponding simulation:		10-19, with the second digit corresponding to the above values
				** "Fun" shaders:					20+
				**
				** 1: Deuteranopia correction		11: Deuteranopia simulation
				** 2: Tritanopia					12: Tritanopia simulation (not currently accurate)
				** 3: Higher contrast				13: (not necessary?)
				** 4: Negative						14: (not necessary)
				**
				** 20: Hue Shift (in progress)
				**
				** NOTE: When adding new value here, add to ModeEnum.cs as well
				*/
				if (_FilterType == 1) { /* Deuteranopia correction */
					col.b = ((3 - _FilterWeight) * col.b + _FilterWeight * col.g) / 3;
					col.g = col.g/_FilterScaling;

				} else if (_FilterType == 2) { /* Tritanopia correction */
					// Convert RGB -> CMYK
					fixed r = col.r;
					fixed b = col.b;
					fixed k = 1 - max(r, max(col.g, b));
					fixed y = (1 - b - k) / (1 - k);
					fixed c = (1 - r - k) / (1 - k);

					y = ((3 - _FilterWeight) * y + _FilterWeight * c) / 3;
					c /= _FilterScaling;

					// Convert back
					col.r = (1 - c) * (1 - k);
					col.b = (1 - y) * (1 - k);

				} else if (_FilterType == 3) { /* High Contrast */
					fixed r = col.r;
					fixed g = col.g;
					fixed b = col.b;
					fixed s = _SliderValue / 2;

					r = (r - s) / (1 - 2 * s);
					g = (g - s) / (1 - 2 * s);
					b = (b - s) / (1 - 2 * s);
					col.r = min(1, max(0, r));
					col.g = min(1, max(0, g));
					col.b = min(1, max(0, b));

				} else if (_FilterType == 4) { /* Negative */
					col.r = 1 - col.r;
					col.g = 1 - col.g;
					col.b = 1 - col.b;

				} else if (_FilterType == 11) { /* Deuteranopia simulator */
				 //Verified using simulations on the internet
					fixed s = _SliderValue / 2;
					fixed r = col.r;
					fixed g = col.g;
					col.r = (1 - s) * r + s * g;
					col.g = (1 - s) * g + s * r;

				} else if (_FilterType == 12) { /* Tritanopia simulator? IN PROGRESS */
					fixed s = _SliderValue / 2;
					// Convert RGB -> CMYK
					fixed r = col.r;
					fixed b = col.b;
					fixed k = 1 - max(r, max(col.g, b));
					fixed y = (1 - b - k) / (1 - k);
					fixed c = (1 - r - k) / (1 - k);

					fixed tempy = (1 - s) * y + s * c;
					fixed tempc = (1 - s) * c + s * y;

					// Convert back
					col.r = (1 - tempc) * (1 - k);
					col.b = (1 - tempy) * (1 - k);
				} else if (_FilterType == 20){ /* Hue Shift? IN PROGRESS */
					//Got this code off the internet... Need to fix it, since it's a little wonky
					fixed s = _SliderValue;
					//RGB -> HSL
					fixed r = col.r;
					fixed g = col.g;
					fixed b = col.b;
					fixed maxColor = max(r, max(g, b));
					fixed minColor = min(r, min(g, b));
					fixed l = (minColor + maxColor) / 2;
					fixed sat;
					fixed h;
					if (l < 0.5) sat = (maxColor - minColor) / (maxColor + minColor);
					else sat = (maxColor - minColor) / (2.0 - maxColor - minColor);

					if (r == maxColor) h = (g - b) / (maxColor - minColor);
					else if (g == maxColor) h = 2.0 + (b - r) / (maxColor - minColor);
					else h = 4.0 + (r - g) / (maxColor - minColor);

					h /= 6; //to bring it to a number between 0 and 1
					h += s;
					if (h > 1) h--;

					//HSL->RGB
					fixed temp1;
					fixed temp2;
					fixed tempr;
					fixed tempg;
					fixed tempb;
					if (l < 0.5) temp2 = l * (1 + sat);
					else temp2 = (l + sat) - (l * sat);
					temp1 = 2 * l - temp2;
					tempr = h + 1.0 / 3.0;
					if (tempr > 1) tempr--;
					tempg = h;
					tempb = h - 1.0 / 3.0;
					if (tempb < 0) tempb++;

					//Red
					if (tempr < 1.0 / 6.0) r = temp1 + (temp2 - temp1) * 6.0 * tempr;
					else if (tempr < 0.5) r = temp2;
					else if (tempr < 2.0 / 3.0) r = temp1 + (temp2 - temp1) * ((2.0 / 3.0) - tempr) * 6.0;
					else r = temp1;

					//Green
					if (tempg < 1.0 / 6.0) g = temp1 + (temp2 - temp1) * 6.0 * tempg;
					else if (tempg < 0.5) g = temp2;
					else if (tempg < 2.0 / 3.0) g = temp1 + (temp2 - temp1) * ((2.0 / 3.0) - tempg) * 6.0;
					else g = temp1;

					//Blue
					if (tempb < 1.0 / 6.0) b = temp1 + (temp2 - temp1) * 6.0 * tempb;
					else if (tempb < 0.5) b = temp2;
					else if (tempb < 2.0 / 3.0) b = temp1 + (temp2 - temp1) * ((2.0 / 3.0) - tempb) * 6.0;
					else b = temp1;

					col.r = r;
					col.g = g;
					col.b = b;
				} else if (_FilterType == -1) { /* Blindness simulator? */
					//Shhhh, don't worry about it...
					fixed s = _SliderValue;
					col.r = max(0, col.r - s);
					col.g = max(0, col.g - s);
					col.b = max(0, col.b - s);
				}


                return col;

			}
			ENDCG
		}
	}
}
