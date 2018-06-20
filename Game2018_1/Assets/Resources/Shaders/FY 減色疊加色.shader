// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "FY/SubAdd"
{
	Properties
	{
		_MainTex ("減色材質", 2D) = "white" {}
		_Color("變色", Color) = (1, 1, 1, 1)
		_SubRate("減色率", Float) = 1.0
		_Intensity("Intensity", Float) = 1.0
	}
	Category
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}
		
		Cull Off
		Lighting Off
		ZWrite Off
		Fog {Mode Off}
		
		SubShader
		{
			Pass
			{
				Blend One OneMinusSrcAlpha
				
				CGPROGRAM 
				#pragma fragmentoption ARB_precision_hint_fastest
				#pragma multi_compile NO_SCALE
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"
				sampler2D _MainTex;
				half4 _MainTex_ST;
				fixed4 _Color;
				fixed _SubRate;
				float _Intensity;
				
				struct appdata_t
				{
					float4 vertex : POSITION; 
					float4 color : COLOR;
					float2 texcoord : TEXCOORD0;
				};
	
				struct v2f
				{
					float4 vertex : POSITION; 
					float4 color : COLOR;
					float2 texcoord : TEXCOORD0;
				};
				
				v2f vert (appdata_t v)
				{
					v2f o;

					o.vertex = UnityObjectToClipPos(v.vertex);
					o.color = v.color;
					o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
					return o;
				}
				
				float4 frag (v2f i) : COLOR
				{
					float4 c = tex2D(_MainTex, i.texcoord);
					fixed gray = Luminance(c.rgb) * _Color.a * _SubRate * i.color.a;
					c = c * i.color;
					c = float4(c.rgb * _Color.rgb * c.a * _Intensity * _Color.a, gray * 2.0 * _Intensity);
					return c;
				}
				ENDCG
			}
		}
	}
} 