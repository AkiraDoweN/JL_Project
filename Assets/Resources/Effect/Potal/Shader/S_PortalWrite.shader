// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "portal4"
{
	Properties
	{
		_za1("za 1", 2D) = "white" {}
		_TextureSample0("Texture Sample 0", 2D) = "gray" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Overlay"  "Queue" = "Overlay+0" "IsEmissive" = "true"  }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha , One One
		
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform sampler2D _za1;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float4 tex2DNode62 = tex2D( _TextureSample0, uv_TextureSample0 );
			float2 panner59 = ( 1.0 * _Time.y * float2( 0,0.57 ) + float2( 0,0 ));
			float2 uv_TexCoord58 = i.uv_texcoord * float2( 1,0.69 ) + panner59;
			float2 uv_TexCoord35 = i.uv_texcoord * float2( 1,1.4 );
			float4 temp_output_50_0 = ( tex2D( _za1, uv_TexCoord58 ) + (uv_TexCoord35).y );
			float4 temp_output_87_0 = ( ( ( ( 0.17 * tex2DNode62 ) + ( ( 1.0 - temp_output_50_0 ) * tex2DNode62 ) ) * float4( float3(1.01,1.1,1.24) , 0.0 ) ) * float4( 0.9292453,1,0.9861922,0.454902 ) );
			o.Emission = temp_output_87_0.rgb;
			o.Alpha = temp_output_87_0.r;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows exclude_path:deferred 

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
884;348;885;663;170.6894;132.7447;1;True;False
Node;AmplifyShaderEditor.PannerNode;59;-1942.913,62.04961;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0.57;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;35;-1512.839,370.2188;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1.4;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;58;-1598.924,93.9022;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,0.69;False;1;FLOAT2;3,0.52;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;57;-1190.168,-19.98263;Inherit;True;Property;_za1;za 1;0;0;Create;True;0;0;False;0;-1;9033b32055fecfc4e8144f6c5a1e84bc;434b8e0502e21324d9107daaf2c37521;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;34;-1259.59,349.2226;Inherit;True;False;True;False;False;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;50;-951.704,288.5728;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;66;-2217.016,281.3944;Inherit;True;Constant;_Float0;Float 0;4;0;Create;True;0;0;False;0;0.17;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;64;-629.7695,320.5694;Inherit;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;62;-2254.085,511.2383;Inherit;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;-1;9033b32055fecfc4e8144f6c5a1e84bc;7ba9aec27d7e2fc458ab09fc66d9d915;True;0;False;gray;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;63;-361.8159,443.4528;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;80;-1941.379,399.5244;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;79;-378.2848,145.0866;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector3Node;85;-131.5921,211.128;Inherit;False;Constant;_Vector0;Vector 0;4;0;Create;True;0;0;False;0;1.01,1.1,1.24;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;84;9.800898,3.197101;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;81;-910.7878,276.3153;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,0.14;False;1;FLOAT2;3,0.52;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;83;-1091.013,279.3975;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.06,0.03;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;56;-254.8892,-317.1421;Inherit;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FloorOpNode;11;-738.2166,-206.4454;Inherit;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;87;301.9713,54.80005;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0.9292453,1,0.9861922,0.454902;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;570.7218,-147.7743;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;portal4;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Overlay;;Overlay;ForwardOnly;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;4;1;False;-1;1;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;58;1;59;0
WireConnection;57;1;58;0
WireConnection;34;0;35;0
WireConnection;50;0;57;0
WireConnection;50;1;34;0
WireConnection;64;0;50;0
WireConnection;63;0;64;0
WireConnection;63;1;62;0
WireConnection;80;0;66;0
WireConnection;80;1;62;0
WireConnection;79;0;80;0
WireConnection;79;1;63;0
WireConnection;84;0;79;0
WireConnection;84;1;85;0
WireConnection;81;1;83;0
WireConnection;56;0;11;0
WireConnection;11;0;50;0
WireConnection;87;0;84;0
WireConnection;0;2;87;0
WireConnection;0;9;87;0
ASEEND*/
//CHKSM=46535DA2FAD9213E98D63752408E604E849D6583