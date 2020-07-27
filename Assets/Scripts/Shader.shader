Shader "Custom/drawing_shader"
{

	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		[HideInInspector] _DrawingTex("Drawing texture", 2D) = "" {}
	}




		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _DrawingTex;

		struct Input
		{
			float2 uv_MainTex;
			float2 uv_DrawingTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		UNITY_INSTANCING_BUFFER_START(Props)
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf(Input IN, inout SurfaceOutputStandard o) {
			float4 drawData = tex2D(_DrawingTex, IN.uv_DrawingTex);
			float4 mainData = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			fixed4 c = lerp(mainData, drawData, drawData.a);
			c.a = drawData.a + mainData.a;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}

		ENDCG
	}
		FallBack "Diffuse"
}

