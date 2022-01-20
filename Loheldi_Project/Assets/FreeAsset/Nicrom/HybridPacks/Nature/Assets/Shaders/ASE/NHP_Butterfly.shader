// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Nicrom/NHP/ASE/Butterfly"
{
	Properties
	{
		[NoScaleOffset][Header(Textures)]_ColorMask("Color Mask", 2D) = "white" {}
		_Color1("Color 1", Color) = (0,0,0,1)
		_Color2("Color 2", Color) = (1,0.357633,0,1)
		_Color3("Color 3", Color) = (1,0.1083839,0,1)
		[Space]_Frequency("Frequency", Float) = 0.1
		_Amplitude("Amplitude", Float) = 0.05
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "AlphaTest+0" }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#if defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (defined(SHADER_TARGET_SURFACE_ANALYSIS) && !defined(SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))//ASE Sampler Macros
		#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex.Sample(samplerTex,coord)
		#else//ASE Sampling Macros
		#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex2D(tex,coord)
		#endif//ASE Sampling Macros

		#pragma surface surf Standard keepalpha addshadow fullforwardshadows nolightmap  nodirlightmap vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _Amplitude;
		uniform float _Frequency;
		uniform float4 _Color1;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_ColorMask);
		SamplerState sampler_ColorMask;
		uniform float4 _Color2;
		uniform float4 _Color3;
		uniform float _Cutoff = 0.5;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 appendResult51 = (float3(0.0 , ( ( _Amplitude * sin( ( _Time.y * _Frequency ) ) ) * step( 0.6 , v.texcoord.xy.x ) ) , 0.0));
			float3 LocalVertexOffset17 = appendResult51;
			v.vertex.xyz += LocalVertexOffset17;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_ColorMask1 = i.uv_texcoord;
			float4 tex2DNode1 = SAMPLE_TEXTURE2D( _ColorMask, sampler_ColorMask, uv_ColorMask1 );
			float ColorMask_R25 = tex2DNode1.r;
			float4 lerpResult36 = lerp( float4( 0,0,0,0 ) , _Color1 , ColorMask_R25);
			float ColorMask_G30 = tex2DNode1.g;
			float4 lerpResult35 = lerp( float4( 0,0,0,0 ) , _Color2 , ColorMask_G30);
			float ColorMask_B31 = tex2DNode1.b;
			float4 lerpResult38 = lerp( float4( 0,0,0,0 ) , _Color3 , ColorMask_B31);
			float4 MainTextureColor2 = ( lerpResult36 + lerpResult35 + lerpResult38 );
			o.Albedo = MainTextureColor2.rgb;
			float temp_output_55_0 = 0.0;
			o.Metallic = temp_output_55_0;
			o.Smoothness = temp_output_55_0;
			o.Alpha = 1;
			float Opacity3 = tex2DNode1.a;
			clip( Opacity3 - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
}
/*ASEBEGIN
Version=18500
386.2857;374.2857;1326;647;1755.328;476.0231;2.570016;True;False
Node;AmplifyShaderEditor.CommentaryNode;52;-2174.205,511.8536;Inherit;False;1398.958;632.3766;;12;17;51;44;42;14;15;41;43;11;13;12;10;Vertex Offset;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;53;-2814.478,-252.6543;Inherit;False;758.3452;506.8664;;5;3;30;31;25;1;Color Mask;1,1,1,1;0;0
Node;AmplifyShaderEditor.TimeNode;10;-2139.342,667.8316;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;12;-2090.342,818.8316;Inherit;False;Property;_Frequency;Frequency;4;0;Create;True;0;0;False;1;Space;False;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-2744.478,-78.03712;Inherit;True;Property;_ColorMask;Color Mask;0;1;[NoScaleOffset];Create;True;0;0;False;1;Header(Textures);False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-1899.342,750.8316;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-1780.12,611.0468;Inherit;False;Property;_Amplitude;Amplitude;5;0;Create;True;0;0;False;0;False;0.05;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;25;-2336.562,-156.6544;Inherit;False;ColorMask_R;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;30;-2342.987,-66.15018;Inherit;False;ColorMask_G;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;31;-2340.987,15.84966;Inherit;False;ColorMask_B;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;43;-1731.986,882.4143;Inherit;False;Constant;_Float3;Float 3;7;0;Create;True;0;0;False;0;False;0.6;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;41;-1805.952,970.1671;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SinOpNode;11;-1734.752,751.2906;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;8;-1796.162,-751.3415;Inherit;False;1020.673;1005.265;;11;27;37;38;26;33;29;2;32;35;36;7;Albedo;1,1,1,1;0;0
Node;AmplifyShaderEditor.StepOpNode;42;-1537.986,924.4143;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;27;-1696.101,-71.63398;Inherit;False;Property;_Color3;Color 3;3;0;Create;True;0;0;False;0;False;1,0.1083839,0,1;1,0.1083839,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;29;-1698.366,-492.9402;Inherit;False;25;ColorMask_R;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;37;-1686.109,120.7261;Inherit;False;31;ColorMask_B;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;26;-1695.649,-372.0649;Inherit;False;Property;_Color2;Color 2;2;0;Create;True;0;0;False;0;False;1,0.357633,0,1;1,0.357633,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;7;-1697.629,-655.5555;Inherit;False;Property;_Color1;Color 1;1;0;Create;True;0;0;False;0;False;0,0,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;33;-1693.624,-201.0696;Inherit;False;30;ColorMask_G;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-1541.858,663.3871;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;35;-1419.767,-312.3106;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;36;-1437.055,-594.005;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;-1342.689,804.4222;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;38;-1449.149,0.4699876;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;32;-1180.858,-332.5009;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;51;-1175.896,781.9786;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;54;-377.6145,-0.8588867;Inherit;False;636.7252;509.663;;5;0;5;4;18;55;Master Node;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;17;-1010.536,779.5941;Inherit;False;LocalVertexOffset;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;2;-1028.243,-338.3018;Inherit;False;MainTextureColor;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;3;-2338.247,114.4977;Float;False;Opacity;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;18;-286.004,383.6239;Inherit;False;17;LocalVertexOffset;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;5;-282.6146,58.14111;Inherit;False;2;MainTextureColor;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;4;-240.2272,278.0219;Inherit;False;3;Opacity;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;55;-220.718,151.4803;Inherit;False;Constant;_Float0;Float 0;7;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-16.46079,71.3755;Float;False;True;-1;2;;0;0;Standard;Nicrom/NHP/ASE/Butterfly;False;False;False;False;False;False;True;False;True;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Opaque;;AlphaTest;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;6;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;True;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;13;0;10;2
WireConnection;13;1;12;0
WireConnection;25;0;1;1
WireConnection;30;0;1;2
WireConnection;31;0;1;3
WireConnection;11;0;13;0
WireConnection;42;0;43;0
WireConnection;42;1;41;1
WireConnection;14;0;15;0
WireConnection;14;1;11;0
WireConnection;35;1;26;0
WireConnection;35;2;33;0
WireConnection;36;1;7;0
WireConnection;36;2;29;0
WireConnection;44;0;14;0
WireConnection;44;1;42;0
WireConnection;38;1;27;0
WireConnection;38;2;37;0
WireConnection;32;0;36;0
WireConnection;32;1;35;0
WireConnection;32;2;38;0
WireConnection;51;1;44;0
WireConnection;17;0;51;0
WireConnection;2;0;32;0
WireConnection;3;0;1;4
WireConnection;0;0;5;0
WireConnection;0;3;55;0
WireConnection;0;4;55;0
WireConnection;0;10;4;0
WireConnection;0;11;18;0
ASEEND*/
//CHKSM=3A08328B99F89E4474EE49E26D24A1477F7955D2