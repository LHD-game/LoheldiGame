// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Nicrom/NHP/ASE/Terrain First Pass"
{
	Properties
	{
		[HideInInspector]_TerrainHolesTexture("_TerrainHolesTexture", 2D) = "white" {}
		[HideInInspector]_Control("Control", 2D) = "white" {}
		[HideInInspector]_Splat3("Splat3", 2D) = "white" {}
		[HideInInspector]_Splat2("Splat2", 2D) = "white" {}
		[HideInInspector]_Splat1("Splat1", 2D) = "white" {}
		[HideInInspector]_Splat0("Splat0", 2D) = "white" {}
		[HideInInspector]_Normal0("Normal0", 2D) = "white" {}
		[HideInInspector]_Normal1("Normal1", 2D) = "white" {}
		[HideInInspector]_Normal2("Normal2", 2D) = "white" {}
		[HideInInspector]_Normal3("Normal3", 2D) = "white" {}
		[HideInInspector]_Smoothness3("Smoothness3", Range( 0 , 1)) = 1
		[HideInInspector]_Smoothness1("Smoothness1", Range( 0 , 1)) = 1
		[HideInInspector]_Smoothness0("Smoothness0", Range( 0 , 1)) = 1
		[HideInInspector]_Smoothness2("Smoothness2", Range( 0 , 1)) = 1
		[HideInInspector][Gamma]_Metallic0("Metallic0", Range( 0 , 1)) = 0
		[HideInInspector][Gamma]_Metallic2("Metallic2", Range( 0 , 1)) = 0
		[HideInInspector][Gamma]_Metallic3("Metallic3", Range( 0 , 1)) = 0
		[HideInInspector][Gamma]_Metallic1("Metallic1", Range( 0 , 1)) = 0
		_Color0("Color 0", Color) = (1,1,1,1)
		_Color1("Color 1", Color) = (1,1,1,1)
		_Color2("Color 2", Color) = (1,1,1,1)
		_Color3("Color 3", Color) = (1,1,1,1)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry-100" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma multi_compile_fog
		#pragma multi_compile_local __ _ALPHATEST_ON
		#pragma exclude_renderers gles vulkan 
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Control;
		uniform float4 _Control_ST;
		uniform sampler2D _Normal0;
		uniform sampler2D _Splat0;
		uniform float4 _Splat0_ST;
		uniform sampler2D _Normal1;
		uniform sampler2D _Splat1;
		uniform float4 _Splat1_ST;
		uniform sampler2D _Normal2;
		uniform sampler2D _Splat2;
		uniform float4 _Splat2_ST;
		uniform sampler2D _Normal3;
		uniform sampler2D _Splat3;
		uniform float4 _Splat3_ST;
		uniform float _Smoothness0;
		uniform float4 _Color0;
		uniform float _Smoothness1;
		uniform float4 _Color1;
		uniform float _Smoothness2;
		uniform float4 _Color2;
		uniform float _Smoothness3;
		uniform float4 _Color3;
		uniform sampler2D _TerrainHolesTexture;
		SamplerState sampler_TerrainHolesTexture;
		uniform float4 _TerrainHolesTexture_ST;
		uniform float _Metallic0;
		uniform float _Metallic1;
		uniform float _Metallic2;
		uniform float _Metallic3;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float localCalculateTangentsStandard16_g13 = ( 0.0 );
			v.tangent.xyz = cross ( v.normal, float3( 0, 0, 1 ) );
			v.tangent.w = -1;
			v.vertex.xyz += localCalculateTangentsStandard16_g13;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Control = i.uv_texcoord * _Control_ST.xy + _Control_ST.zw;
			float4 tex2DNode5_g13 = tex2D( _Control, uv_Control );
			float dotResult20_g13 = dot( tex2DNode5_g13 , float4(1,1,1,1) );
			float SplatWeight22_g13 = dotResult20_g13;
			float localSplatClip74_g13 = ( SplatWeight22_g13 );
			float SplatWeight74_g13 = SplatWeight22_g13;
			#if !defined(SHADER_API_MOBILE) && defined(TERRAIN_SPLAT_ADDPASS)
				clip(SplatWeight74_g13 == 0.0f ? -1 : 1);
			#endif
			float4 SplatControl26_g13 = ( tex2DNode5_g13 / ( localSplatClip74_g13 + 0.001 ) );
			float2 uv_Splat0 = i.uv_texcoord * _Splat0_ST.xy + _Splat0_ST.zw;
			float2 uv_Splat1 = i.uv_texcoord * _Splat1_ST.xy + _Splat1_ST.zw;
			float2 uv_Splat2 = i.uv_texcoord * _Splat2_ST.xy + _Splat2_ST.zw;
			float2 uv_Splat3 = i.uv_texcoord * _Splat3_ST.xy + _Splat3_ST.zw;
			float4 weightedBlendVar8_g13 = SplatControl26_g13;
			float4 weightedBlend8_g13 = ( weightedBlendVar8_g13.x*tex2D( _Normal0, uv_Splat0 ) + weightedBlendVar8_g13.y*tex2D( _Normal1, uv_Splat1 ) + weightedBlendVar8_g13.z*tex2D( _Normal2, uv_Splat2 ) + weightedBlendVar8_g13.w*tex2D( _Normal3, uv_Splat3 ) );
			o.Normal = UnpackNormal( weightedBlend8_g13 );
			float4 appendResult33_g13 = (float4(1.0 , 1.0 , 1.0 , _Smoothness0));
			float4 Layer1111_g13 = ( ( appendResult33_g13 * tex2D( _Splat0, uv_Splat0 ) ) * _Color0 );
			float4 appendResult36_g13 = (float4(1.0 , 1.0 , 1.0 , _Smoothness1));
			float4 Layer2115_g13 = ( ( appendResult36_g13 * tex2D( _Splat1, uv_Splat1 ) ) * _Color1 );
			float4 appendResult39_g13 = (float4(1.0 , 1.0 , 1.0 , _Smoothness2));
			float4 Layer3118_g13 = ( ( appendResult39_g13 * tex2D( _Splat2, uv_Splat2 ) ) * _Color2 );
			float4 appendResult42_g13 = (float4(1.0 , 1.0 , 1.0 , _Smoothness3));
			float4 Layer4121_g13 = ( ( appendResult42_g13 * tex2D( _Splat3, uv_Splat3 ) ) * _Color3 );
			float4 weightedBlendVar9_g13 = SplatControl26_g13;
			float4 weightedBlend9_g13 = ( weightedBlendVar9_g13.x*Layer1111_g13 + weightedBlendVar9_g13.y*Layer2115_g13 + weightedBlendVar9_g13.z*Layer3118_g13 + weightedBlendVar9_g13.w*Layer4121_g13 );
			float4 MixDiffuse28_g13 = weightedBlend9_g13;
			float4 localClipHoles100_g13 = ( MixDiffuse28_g13 );
			float2 uv_TerrainHolesTexture = i.uv_texcoord * _TerrainHolesTexture_ST.xy + _TerrainHolesTexture_ST.zw;
			float holeClipValue99_g13 = tex2D( _TerrainHolesTexture, uv_TerrainHolesTexture ).r;
			float Hole100_g13 = holeClipValue99_g13;
			#ifdef _ALPHATEST_ON
				clip(Hole100_g13 == 0.0f ? -1 : 1);
			#endif
			o.Albedo = localClipHoles100_g13.xyz;
			float4 appendResult55_g13 = (float4(_Metallic0 , _Metallic1 , _Metallic2 , _Metallic3));
			float dotResult53_g13 = dot( SplatControl26_g13 , appendResult55_g13 );
			o.Metallic = dotResult53_g13;
			o.Smoothness = (MixDiffuse28_g13).w;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
}
/*ASEBEGIN
Version=18500
2394.286;864.1429;1083;565;-1336.137;431.5677;1.6;True;False
Node;AmplifyShaderEditor.CommentaryNode;116;2102.192,-263.2018;Inherit;False;777.1472;505;;2;0;115;Master Node;1,1,1,1;0;0
Node;AmplifyShaderEditor.FunctionNode;115;2159.192,-132.6688;Inherit;False;FourSplatsFirstPassTerrain - NHP;0;;13;9c2877a4c5d621d40a3efaf648c61312;2,102,1,85,0;0;6;FLOAT4;0;FLOAT3;14;FLOAT;56;FLOAT;45;FLOAT;19;FLOAT3;17
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2616.339,-213.2018;Float;False;True;-1;2;;0;0;Standard;Nicrom/NHP/ASE/Terrain First Pass;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;-100;False;Opaque;;Geometry;All;12;d3d9;d3d11_9x;d3d11;glcore;gles3;metal;xbox360;xboxone;ps4;psp2;n3ds;wiiu;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;1;Pragma;multi_compile_fog;False;;Custom;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;115;0
WireConnection;0;1;115;14
WireConnection;0;3;115;56
WireConnection;0;4;115;45
WireConnection;0;11;115;17
ASEEND*/
//CHKSM=D3008AACEC7FB2F762E3CB950CDBEB6A34480B0D