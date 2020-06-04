// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Portal2"
{
	Properties
	{
		_portal4("portal4", 2D) = "white" {}
		_za1("za 1", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _za1;
		uniform sampler2D _portal4;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 panner39 = ( 1.0 * _Time.y * float2( 0,0.62 ) + float2( 0,0 ));
			float2 uv_TexCoord38 = i.uv_texcoord * float2( 1,0.48 ) + panner39;
			float4 tex2DNode36 = tex2D( _za1, uv_TexCoord38 );
			float2 uv_TexCoord12 = i.uv_texcoord * float2( 1,0.46 ) + float2( 0,0.3 );
			float4 tex2DNode35 = tex2D( _portal4, uv_TexCoord12 );
			float4 temp_output_88_0 = ( ( ( ( tex2DNode36 * float4( 1,0.9388742,0.7688679,0.8705882 ) ) + float4( 0.2627451,0.2627451,0.2627451,0 ) ) * ( tex2DNode35 * float4( float3(1.96,1.55,0.97) , 0.0 ) ) ) * float4( 0.8584906,0.8584906,0.8584906,0.8862745 ) );
			o.Emission = temp_output_88_0.rgb;
			o.Alpha = temp_output_88_0.r;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows exclude_path:deferred 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17900
109;475;885;488;1123.06;550.9312;3.664428;True;False
Node;AmplifyShaderEditor.PannerNode;39;-1346.019,-388.0816;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0.62;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;64;-884.4872,137.0069;Inherit;False;Constant;_Vector2;Vector 2;6;0;Create;True;0;0;False;0;0,0.3;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;25;-989.787,9.281371;Inherit;False;Constant;_Vector1;Vector 1;4;0;Create;True;0;0;False;0;1,0.46;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;38;-1167.796,-391.1638;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,0.48;False;1;FLOAT2;3,0.52;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;12;-752.2631,-6.598482;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;36;-969.0875,-433.7222;Inherit;True;Property;_za1;za 1;4;0;Create;True;0;0;False;0;-1;9033b32055fecfc4e8144f6c5a1e84bc;9033b32055fecfc4e8144f6c5a1e84bc;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;35;-316.9698,60.70209;Inherit;True;Property;_portal4;portal4;2;0;Create;True;0;0;False;0;-1;4645a3bb2877b0949acc4488ec4a2d35;4645a3bb2877b0949acc4488ec4a2d35;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;49;-188.4861,-247.1792;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;1,0.9388742,0.7688679,0.8705882;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector3Node;45;-180.0577,323.1434;Inherit;False;Constant;_Vector0;Vector 0;7;0;Create;True;0;0;False;0;1.96,1.55,0.97;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleAddOpNode;87;63.23146,-225.3835;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0.2627451,0.2627451,0.2627451,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;46;168.7695,125.9672;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;50;529.7117,-236.1618;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;73;-1001.17,1284.886;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;22;-480.5635,463.3925;Inherit;True;Property;_portal3;portal3;6;0;Create;True;0;0;False;0;-1;03650d0b7e7f11c4db071ade716583a8;03650d0b7e7f11c4db071ade716583a8;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;76;-427.7513,1141.4;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;71;-1990.845,1395.838;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;78;-39.66544,999.5109;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;69;-2266.483,1277.708;Inherit;True;Constant;_Float0;Float 0;4;0;Create;True;0;0;False;0;0.17;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;80;-1140.479,1275.711;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.06,0.03;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;75;-411.2824,1439.767;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FloorOpNode;82;-787.6831,789.8684;Inherit;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;55;108.6506,382.968;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1.75;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;89;833.745,585.0414;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;74;-679.236,1316.883;Inherit;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;79;-960.2543,1272.629;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,0.14;False;1;FLOAT2;3,0.52;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;-187.4552,-537.6549;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TimeNode;5;-1185.96,480.1946;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;66;-1562.305,1366.532;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1.4;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;72;-1239.634,976.3312;Inherit;True;Property;_TextureSample1;Texture Sample 1;0;0;Create;True;0;0;False;0;-1;9033b32055fecfc4e8144f6c5a1e84bc;434b8e0502e21324d9107daaf2c37521;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;6;-1185.294,174.7572;Inherit;True;Property;_speed;speed;1;0;Create;True;0;0;False;0;0,0.2;0,0.2;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.ComponentMaskNode;68;-1309.056,1345.536;Inherit;True;False;True;False;False;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;52;386.168,319.9384;Inherit;True;False;True;False;False;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;65;-1992.379,1058.363;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0.57;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;61;556.1887,253.8055;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;40;-1200.823,-636.4739;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,0.14;False;1;FLOAT2;3,0.52;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;70;-2303.552,1507.552;Inherit;True;Property;_TextureSample0;Texture Sample 0;3;0;Create;True;0;0;False;0;-1;9033b32055fecfc4e8144f6c5a1e84bc;7ba9aec27d7e2fc458ab09fc66d9d915;True;0;False;gray;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;83;252.5049,1051.114;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0.9292453,1,0.9861922,0.454902;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;58;399.136,108.942;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;67;-1648.39,1090.216;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,0.69;False;1;FLOAT2;3,0.52;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-663.1837,-566.3609;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector3Node;77;-181.0585,1207.442;Inherit;False;Constant;_Vector3;Vector 3;4;0;Create;True;0;0;False;0;1.01,1.1,1.24;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;37;-989.3135,-641.7178;Inherit;True;Property;_dksk;dksk;5;0;Create;True;0;0;False;0;-1;ef7c45949e7d49a48ba784620667e4ca;ef7c45949e7d49a48ba784620667e4ca;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;88;745.959,79.37001;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0.8584906,0.8584906,0.8584906,0.8862745;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;81;-304.3557,679.1718;Inherit;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PannerNode;41;-1381.048,-633.3917;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.06,0.03;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-981.8011,314.3369;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;60;381.4494,40.05679;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0.22;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1121.461,-15.95075;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Portal2;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;ForwardOnly;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;2;5;False;-1;10;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;38;1;39;0
WireConnection;12;0;25;0
WireConnection;12;1;64;0
WireConnection;36;1;38;0
WireConnection;35;1;12;0
WireConnection;49;0;36;0
WireConnection;87;0;49;0
WireConnection;46;0;35;0
WireConnection;46;1;45;0
WireConnection;50;0;87;0
WireConnection;50;1;46;0
WireConnection;73;0;72;0
WireConnection;73;1;68;0
WireConnection;22;1;12;0
WireConnection;76;0;71;0
WireConnection;76;1;75;0
WireConnection;71;0;69;0
WireConnection;71;1;70;0
WireConnection;78;0;76;0
WireConnection;78;1;77;0
WireConnection;75;0;74;0
WireConnection;75;1;70;0
WireConnection;82;0;73;0
WireConnection;74;0;73;0
WireConnection;79;1;80;0
WireConnection;44;0;42;0
WireConnection;72;1;67;0
WireConnection;68;0;66;0
WireConnection;52;0;55;0
WireConnection;40;1;41;0
WireConnection;83;0;78;0
WireConnection;67;1;65;0
WireConnection;42;0;37;0
WireConnection;42;1;36;0
WireConnection;37;1;40;0
WireConnection;88;0;50;0
WireConnection;81;0;82;0
WireConnection;13;0;6;0
WireConnection;13;1;5;2
WireConnection;60;0;35;4
WireConnection;0;2;88;0
WireConnection;0;9;88;0
ASEEND*/
//CHKSM=7F2FA04CA52C1F8B4F0C9E5DDCD4C3FB1746B41D