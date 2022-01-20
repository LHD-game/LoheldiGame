// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Nicrom/NHP/ASE/Vegetation Studio/Stylised Flower Without Stem"
{
	Properties
	{
		_FlowerColor1("Flower Color 1", Color) = (0.7843137,0.454902,0.1411765,1)
		_FlowerColor2("Flower Color 2", Color) = (0.8980392,0.9529412,1,1)
		_ColorBlendStart("Color Blend Start", Range( 0 , 1)) = 0.1
		_ColorBlendEnd("Color Blend End", Range( 0 , 1)) = 0.15
		[NoScaleOffset][Header(Textures)]_MainTex("Flower Texture", 2D) = "white" {}
		_AlphaCutoff("Alpha Cutoff", Range( 0 , 1)) = 0.5
		_MBDefaultBending("MB Default Bending", Float) = 0
		_MBAmplitude("MB Amplitude", Float) = 1.5
		_MBAmplitudeOffset("MB Amplitude Offset", Float) = 2
		_MBFrequency("MB Frequency", Float) = 1.11
		_MBFrequencyOffset("MB Frequency Offset", Float) = 0
		_MBPhase("MB Phase", Float) = 1
		_MBWindDir("MB Wind Dir", Range( 0 , 360)) = 0
		_MBWindDirOffset("MB Wind Dir Offset", Range( 0 , 180)) = 20
		_MBWindDirBlend("MB Wind Dir Blend", Range( 0 , 1)) = 0
		_MBMaxHeight("MB Max Height", Float) = 0.5
		[Toggle(_ENABLEHORIZONTALBENDING_ON)] _EnableHorizontalBending("Enable Horizontal Bending ", Float) = 1
		_DBHorizontalAmplitude("DB Horizontal Amplitude", Float) = 2
		_DBHorizontalFrequency("DB Horizontal Frequency", Float) = 1.16
		_DBHorizontalPhase("DB Horizontal Phase", Float) = 1
		_DBHorizontalMaxRadius("DB Horizontal Max Radius", Float) = 0.05
		[Toggle(_ENABLESLOPECORRECTION_ON)] _EnableSlopeCorrection("Enable Slope Correction", Float) = 1
		_SlopeCorrectionMagnitude("Slope Correction Magnitude", Range( 0 , 1)) = 1
		_SlopeCorrectionOffset("Slope Correction Offset", Range( 0 , 1)) = 0
		[NoScaleOffset]_NoiseTexture("Noise Texture", 2D) = "white" {}
		_NoiseTextureTilling("Noise Tilling - Static (XY), Animated (ZW)", Vector) = (1,1,1,1)
		_NoisePannerSpeed("Noise Panner Speed", Vector) = (0.05,0.03,0,0)
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
		#pragma shader_feature_local _ENABLESLOPECORRECTION_ON
		#pragma shader_feature_local _ENABLEHORIZONTALBENDING_ON
		#pragma multi_compile_instancing
		#pragma multi_compile GPU_FRUSTUM_ON __
		#pragma instancing_options procedural:setupScale
		#pragma instancing_options procedural:setup forwardadd
		#include "VS_indirect.cginc"
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows nolightmap  vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _MBWindDir;
		uniform float MBGlobalWindDir;
		uniform float _MBWindDirBlend;
		uniform float _MBWindDirOffset;
		uniform sampler2D _NoiseTexture;
		uniform float4 _NoiseTextureTilling;
		uniform float2 _NoisePannerSpeed;
		uniform float _MBAmplitude;
		uniform float _MBAmplitudeOffset;
		uniform float _MBFrequency;
		uniform float _MBFrequencyOffset;
		uniform float _MBPhase;
		uniform float _MBDefaultBending;
		uniform float _MBMaxHeight;
		uniform float _DBHorizontalAmplitude;
		uniform float _DBHorizontalFrequency;
		uniform float _DBHorizontalPhase;
		uniform float _DBHorizontalMaxRadius;
		uniform float _SlopeCorrectionOffset;
		uniform float _SlopeCorrectionMagnitude;
		uniform sampler2D _MainTex;
		uniform float4 _FlowerColor1;
		uniform float4 _FlowerColor2;
		uniform float _ColorBlendStart;
		uniform float _ColorBlendEnd;
		SamplerState sampler_MainTex;
		uniform float _AlphaCutoff;


		float3 RotateAroundAxis( float3 center, float3 original, float3 u, float angle )
		{
			original -= center;
			float C = cos( angle );
			float S = sin( angle );
			float t = 1 - C;
			float m00 = t * u.x * u.x + C;
			float m01 = t * u.x * u.y - S * u.z;
			float m02 = t * u.x * u.z + S * u.y;
			float m10 = t * u.x * u.y + S * u.z;
			float m11 = t * u.y * u.y + C;
			float m12 = t * u.y * u.z - S * u.x;
			float m20 = t * u.x * u.z - S * u.y;
			float m21 = t * u.y * u.z + S * u.x;
			float m22 = t * u.z * u.z + C;
			float3x3 finalMatrix = float3x3( m00, m01, m02, m10, m11, m12, m20, m21, m22 );
			return mul( finalMatrix, original ) + center;
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float lerpResult1574 = lerp( _MBWindDir , MBGlobalWindDir , _MBWindDirBlend);
			float MB_WindDirection870 = lerpResult1574;
			float MB_WindDirectiionOffset1373 = _MBWindDirOffset;
			float4 transform1_g65 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float3 appendResult1503 = (float3(v.texcoord1.xy.x , 0.0 , v.texcoord1.xy.y));
			float3 LocalPivot1504 = -appendResult1503;
			float4 transform2_g65 = mul(unity_ObjectToWorld,float4( LocalPivot1504 , 0.0 ));
			float2 UVs27_g75 = ( (transform1_g65).xz + (transform2_g65).xz );
			float4 temp_output_24_0_g75 = _NoiseTextureTilling;
			float2 AnimatedNoiseTilling29_g75 = (temp_output_24_0_g75).zw;
			float2 panner7_g75 = ( 0.1 * _Time.y * _NoisePannerSpeed + float2( 0,0 ));
			float4 AnimatedNoise1344 = tex2Dlod( _NoiseTexture, float4( ( ( UVs27_g75 * AnimatedNoiseTilling29_g75 ) + panner7_g75 ), 0, 0.0) );
			float temp_output_11_0_g86 = radians( ( ( MB_WindDirection870 + ( MB_WindDirectiionOffset1373 * (-1.0 + ((AnimatedNoise1344).x - 0.0) * (1.0 - -1.0) / (1.0 - 0.0)) ) ) * -1.0 ) );
			float3 appendResult14_g86 = (float3(cos( temp_output_11_0_g86 ) , 0.0 , sin( temp_output_11_0_g86 )));
			float4 transform15_g86 = mul(unity_WorldToObject,float4( appendResult14_g86 , 0.0 ));
			float3 normalizeResult34_g86 = normalize( (transform15_g86).xyz );
			float3 MB_RotationAxis1420 = normalizeResult34_g86;
			float3 RotationAxis56_g107 = MB_RotationAxis1420;
			float MB_Amplitude880 = _MBAmplitude;
			float MB_AmplitudeOffset1356 = _MBAmplitudeOffset;
			float2 StaticNoileTilling28_g75 = (temp_output_24_0_g75).xy;
			float4 StaticNoise1340 = tex2Dlod( _NoiseTexture, float4( ( UVs27_g75 * StaticNoileTilling28_g75 ), 0, 0.0) );
			float4 StaticWorldNoise31_g85 = StaticNoise1340;
			float4 transform8_g85 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float MB_Frequency873 = _MBFrequency;
			float MB_FrequencyOffset1525 = _MBFrequencyOffset;
			float DB_PhaseShift928 = v.color.a;
			float MB_Phase1360 = _MBPhase;
			float MB_DefaultBending877 = _MBDefaultBending;
			float3 ase_vertex3Pos = v.vertex.xyz;
			float MB_MaxHeight1335 = _MBMaxHeight;
			float MB_RotationAngle97 = radians( ( ( ( ( MB_Amplitude880 + ( MB_AmplitudeOffset1356 * (StaticWorldNoise31_g85).x ) ) * sin( ( ( ( transform8_g85.x + transform8_g85.z ) + ( ( _Time.y * ( MB_Frequency873 + ( MB_FrequencyOffset1525 * (StaticWorldNoise31_g85).x ) ) ) + ( ( 2.0 * UNITY_PI ) * DB_PhaseShift928 ) ) ) * MB_Phase1360 ) ) ) + MB_DefaultBending877 ) * ( ase_vertex3Pos.y / MB_MaxHeight1335 ) ) );
			float RotationAngle54_g107 = MB_RotationAngle97;
			float3 PivotPoint60_g107 = LocalPivot1504;
			float3 break62_g107 = PivotPoint60_g107;
			float3 appendResult45_g107 = (float3(break62_g107.x , ase_vertex3Pos.y , break62_g107.z));
			float3 rotatedValue30_g107 = RotateAroundAxis( appendResult45_g107, ase_vertex3Pos, RotationAxis56_g107, RotationAngle54_g107 );
			float DB_HorizontalAmplitude1221 = _DBHorizontalAmplitude;
			float DB_HorizontalFrequency1219 = _DBHorizontalFrequency;
			float Frequency41_g76 = DB_HorizontalFrequency1219;
			float4 transform5_g76 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float DB_HorizontalPhase1401 = _DBHorizontalPhase;
			float3 PivotPoint49_g76 = LocalPivot1504;
			float3 break52_g76 = PivotPoint49_g76;
			float3 appendResult20_g76 = (float3(break52_g76.x , ase_vertex3Pos.y , break52_g76.z));
			float DB_HorizontalMaxRadius1403 = _DBHorizontalMaxRadius;
			float3 rotatedValue33_g76 = RotateAroundAxis( PivotPoint49_g76, ase_vertex3Pos, float3(0,1,0), radians( ( ( DB_HorizontalAmplitude1221 * sin( ( ( ( _Time.y * Frequency41_g76 ) - ( ( 2.0 * UNITY_PI ) * ( 1.0 - DB_PhaseShift928 ) ) ) + ( ( ( transform5_g76.x + transform5_g76.z ) + ( _Time.y * Frequency41_g76 ) ) * DB_HorizontalPhase1401 ) ) ) ) * ( distance( ase_vertex3Pos , appendResult20_g76 ) / DB_HorizontalMaxRadius1403 ) ) ) );
			#ifdef _ENABLEHORIZONTALBENDING_ON
				float3 staticSwitch1214 = ( ( rotatedValue33_g76 - ase_vertex3Pos ) * 1.0 );
			#else
				float3 staticSwitch1214 = float3(0,0,0);
			#endif
			float3 DB_VertexOffset769 = staticSwitch1214;
			float3 rotatedValue34_g107 = RotateAroundAxis( PivotPoint60_g107, ( rotatedValue30_g107 + DB_VertexOffset769 ), RotationAxis56_g107, RotationAngle54_g107 );
			float3 temp_output_1533_0 = ( ( rotatedValue34_g107 - ase_vertex3Pos ) * step( 0.01 , ase_vertex3Pos.y ) );
			float3 MainBending89_g108 = temp_output_1533_0;
			float3 appendResult15_g108 = (float3(0.0 , 1.0 , 0.0));
			float4 transform17_g108 = mul(unity_ObjectToWorld,float4( appendResult15_g108 , 0.0 ));
			float4 break20_g108 = transform17_g108;
			float3 appendResult24_g108 = (float3(-break20_g108.z , 0.0 , break20_g108.x));
			float3 appendResult3_g108 = (float3(0.0 , 1.0 , 0.0));
			float4 transform4_g108 = mul(unity_ObjectToWorld,float4( appendResult3_g108 , 0.0 ));
			float3 lerpResult84_g108 = lerp( float3(0,1,0) , (transform4_g108).xyz , step( 1E-06 , ( abs( transform4_g108.x ) + abs( transform4_g108.z ) ) ));
			float3 normalizeResult7_g108 = normalize( lerpResult84_g108 );
			float dotResult9_g108 = dot( normalizeResult7_g108 , float3(0,1,0) );
			float temp_output_12_0_g108 = acos( dotResult9_g108 );
			float NaNPrevention21_g108 = step( 0.01 , abs( ( temp_output_12_0_g108 * ( 180.0 / UNITY_PI ) ) ) );
			float3 lerpResult26_g108 = lerp( float3(1,0,0) , appendResult24_g108 , NaNPrevention21_g108);
			float4 transform28_g108 = mul(unity_WorldToObject,float4( lerpResult26_g108 , 0.0 ));
			float3 normalizeResult49_g108 = normalize( (transform28_g108).xyz );
			float3 RotationAxis30_g108 = normalizeResult49_g108;
			float SlopeCorrectionOffset1578 = _SlopeCorrectionOffset;
			float SlopeCorrectionMagnitude1563 = _SlopeCorrectionMagnitude;
			float RotationAngle29_g108 = ( saturate( ( (0.0 + ((StaticNoise1340).x - 0.0) * (SlopeCorrectionOffset1578 - 0.0) / (1.0 - 0.0)) + SlopeCorrectionMagnitude1563 ) ) * temp_output_12_0_g108 );
			float3 rotatedValue35_g108 = RotateAroundAxis( LocalPivot1504, ( ase_vertex3Pos + MainBending89_g108 ), RotationAxis30_g108, RotationAngle29_g108 );
			float3 lerpResult52_g108 = lerp( MainBending89_g108 , ( rotatedValue35_g108 - ase_vertex3Pos ) , NaNPrevention21_g108);
			#ifdef _ENABLESLOPECORRECTION_ON
				float3 staticSwitch1569 = lerpResult52_g108;
			#else
				float3 staticSwitch1569 = temp_output_1533_0;
			#endif
			float3 LocalVertexOffset1045 = staticSwitch1569;
			v.vertex.xyz += LocalVertexOffset1045;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_MainTex1481 = i.uv_texcoord;
			float4 tex2DNode1481 = tex2D( _MainTex, uv_MainTex1481 );
			float4 MainTextureColor1487 = tex2DNode1481;
			float DistanceToCenter1466 = distance( float2( 0.5,0.5 ) , i.uv_texcoord );
			float ColorBlendStart1506 = _ColorBlendStart;
			float ColorBlendEnd1575 = _ColorBlendEnd;
			float4 lerpResult1480 = lerp( _FlowerColor1 , _FlowerColor2 , ( saturate( ( ( DistanceToCenter1466 - ColorBlendStart1506 ) / ColorBlendEnd1575 ) ) * step( ColorBlendStart1506 , DistanceToCenter1466 ) ));
			float4 Color1488 = lerpResult1480;
			float4 Albedo1493 = ( MainTextureColor1487 * Color1488 );
			o.Albedo = Albedo1493.rgb;
			o.Alpha = 1;
			float Opacity1494 = tex2DNode1481.a;
			clip( Opacity1494 - _AlphaCutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "StylisedFlowerWithoutStem_MaterialInspector"
}
/*ASEBEGIN
Version=18500
2194.286;690.1429;1156;656;3473.815;-1826.48;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;490;-2556.941,3841.677;Inherit;False;889.0664;379.0813;;6;928;738;1504;1503;1502;1583;Vertex Colors and UVs Baked Data;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1502;-2499.159,4091.931;Inherit;False;1;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;1503;-2242.83,4099.395;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NegateNode;1583;-2066.573,4099.493;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;489;-507.5929,1013.92;Inherit;False;1913.986;1029.289;;25;1477;1488;1480;1476;1478;1475;1474;1471;1472;1473;1576;1469;1468;1467;1493;1494;1491;1490;1489;1487;1481;1466;1465;1463;1464;Albedo;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;866;-4987.905,2305.739;Inherit;False;1531.371;1279.004;;38;1562;870;1574;1573;1572;1373;952;1335;1334;1506;1505;1534;1563;873;880;877;1360;1356;1525;1262;687;1524;850;480;1286;300;1403;1219;1221;1401;1220;1218;1308;1249;1470;1575;1577;1578;Material Properties;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;1463;-462.4769,1068.67;Inherit;False;Constant;_Vector33;Vector 33;20;0;Create;True;0;0;False;0;False;0.5,0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RegisterLocalVarNode;1504;-1892.83,4096.395;Inherit;False;LocalPivot;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;1343;-3196.445,2048.899;Inherit;False;1404.17;640.3209;;8;1340;1344;1551;1579;1580;1546;1500;1541;World Space Noise;1,1,1,1;0;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;1464;-480.5525,1197.082;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;1500;-3115.457,2099.243;Inherit;False;1504;LocalPivot;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DistanceOpNode;1465;-219.3748,1132.368;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1505;-4955.815,2367.933;Inherit;False;Property;_ColorBlendStart;Color Blend Start;2;0;Create;True;0;0;False;0;False;0.1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1506;-4424.629,2366.096;Inherit;False;ColorBlendStart;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1249;-4152.895,2686.214;Float;False;Property;_DBHorizontalMaxRadius;DB Horizontal Max Radius;20;0;Create;True;0;0;False;0;False;0.05;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;850;-4950.607,3133.662;Float;False;Property;_MBWindDir;MB Wind Dir;12;0;Create;True;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1466;-35.06372,1126.171;Inherit;False;DistanceToCenter;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1220;-4163.895,2394.214;Float;False;Property;_DBHorizontalAmplitude;DB Horizontal Amplitude;17;0;Create;True;0;0;False;0;False;2;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1218;-4152.895,2494.214;Float;False;Property;_DBHorizontalFrequency;DB Horizontal Frequency;18;0;Create;True;0;0;False;0;False;1.16;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1573;-4947.652,3300.641;Inherit;False;Property;_MBWindDirBlend;MB Wind Dir Blend;14;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1541;-2875.294,2106.036;Inherit;False;WorldSpaceUVs - NHP;-1;;65;88a2e8a391a04e241878bdb87d9283a3;0;1;6;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.VertexColorNode;738;-2475.147,3895.246;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1470;-4953.9,2453.023;Inherit;False;Property;_ColorBlendEnd;Color Blend End;3;0;Create;True;0;0;False;0;False;0.15;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1308;-4151.895,2590.214;Float;False;Property;_DBHorizontalPhase;DB Horizontal Phase;19;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;1580;-2843.546,2566.78;Float;False;Property;_NoisePannerSpeed;Noise Panner Speed;26;0;Create;True;0;0;False;0;False;0.05,0.03;0.08,0.1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;1572;-4951.652,3215.641;Inherit;False;Global;MBGlobalWindDir;MB Global Wind Dir;28;1;[HideInInspector];Create;True;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;1579;-2915.546,2388.779;Inherit;False;Property;_NoiseTextureTilling;Noise Tilling - Static (XY), Animated (ZW);25;0;Create;False;0;0;False;0;False;1,1,1,1;1,1,1,1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;1546;-2845.371,2194.969;Inherit;True;Property;_NoiseTexture;Noise Texture;24;1;[NoScaleOffset];Create;True;0;0;False;0;False;None;512fa11ad89d84543ad8d6c8d9cb6743;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.RangedFloatNode;1262;-4954.004,2749.792;Float;False;Property;_MBAmplitudeOffset;MB Amplitude Offset;8;0;Create;True;0;0;False;0;False;2;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;928;-2055.531,3986.129;Float;False;DB_PhaseShift;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;952;-4948.952,3403.741;Float;False;Property;_MBWindDirOffset;MB Wind Dir Offset;13;0;Create;True;0;0;False;0;False;20;0;0;180;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;1410;-3194.2,2941.038;Inherit;False;1400.125;641.3857;;10;769;1214;1215;1553;1408;1406;1520;1399;1405;1400;Detail Bending;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;1334;-4947.97,3505.01;Inherit;False;Property;_MBMaxHeight;MB Max Height;15;0;Create;True;0;0;False;0;False;0.5;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1286;-4950.607,3036.662;Float;False;Property;_MBPhase;MB Phase;11;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;480;-4954.004,2845.792;Float;False;Property;_MBFrequency;MB Frequency;9;0;Create;True;0;0;False;0;False;1.11;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;1574;-4624.652,3199.641;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1403;-3772.895,2688.214;Inherit;False;DB_HorizontalMaxRadius;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1401;-3751.895,2588.214;Inherit;False;DB_HorizontalPhase;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1524;-4951.908,2939.926;Inherit;False;Property;_MBFrequencyOffset;MB Frequency Offset;10;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1221;-3783.895,2396.214;Float;False;DB_HorizontalAmplitude;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;300;-4954.004,2653.792;Float;False;Property;_MBAmplitude;MB Amplitude;7;0;Create;True;0;0;False;0;False;1.5;1.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;687;-4953.004,2558.793;Float;False;Property;_MBDefaultBending;MB Default Bending;6;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1219;-3783.895,2492.214;Float;False;DB_HorizontalFrequency;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1468;-65.76256,1396.199;Inherit;False;1506;ColorBlendStart;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1551;-2544.793,2310.421;Inherit;False;WorldSpaceNoise - NHP;-1;;75;af5fa9ff24e18344ebcc05b64d296c57;0;4;22;FLOAT2;0,0;False;20;SAMPLER2D;;False;24;FLOAT4;1,1,1,1;False;19;FLOAT2;0.1,0.1;False;2;COLOR;0;COLOR;16
Node;AmplifyShaderEditor.RegisterLocalVarNode;1575;-4421.826,2450.759;Inherit;False;ColorBlendEnd;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1467;-79.76256,1316.199;Inherit;False;1466;DistanceToCenter;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;877;-4454.761,2558.679;Float;False;MB_DefaultBending;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;880;-4422.761,2654.678;Float;False;MB_Amplitude;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;870;-4439.607,3194.112;Float;False;MB_WindDirection;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1399;-3123.441,3067.708;Inherit;False;1221;DB_HorizontalAmplitude;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;1068;-1532.357,2304.458;Inherit;False;2939.361;1277.706;;28;1045;1569;1571;1566;1565;1582;1581;1533;1413;1507;1415;1421;97;1420;1552;1543;1526;1378;1371;1367;1369;1376;1365;1512;1375;1362;1370;1366;Main Bending;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1525;-4456.766,2940.97;Inherit;False;MB_FrequencyOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1576;102.3116,1476.936;Inherit;False;1575;ColorBlendEnd;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1340;-2023.114,2263.048;Inherit;False;StaticNoise;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1356;-4454.761,2750.678;Inherit;False;MB_AmplitudeOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;873;-4422.761,2846.678;Float;False;MB_Frequency;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1520;-3030.978,3450.861;Inherit;False;1504;LocalPivot;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1335;-4422.292,3505.897;Inherit;False;MB_MaxHeight;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1405;-3094.899,3221.751;Inherit;False;1401;DB_HorizontalPhase;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1400;-3124.441,3142.708;Inherit;False;1219;DB_HorizontalFrequency;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;1469;171.2374,1347.199;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1344;-2018.805,2378.301;Inherit;False;AnimatedNoise;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;1408;-3137.726,3374.343;Inherit;False;1403;DB_HorizontalMaxRadius;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1360;-4419.363,3037.549;Inherit;False;MB_Phase;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1406;-3063.899,3297.751;Inherit;False;928;DB_PhaseShift;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1373;-4482.077,3404.324;Inherit;False;MB_WindDirectiionOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;1473;350.2371,1412.199;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1370;-1447.03,3389.792;Inherit;False;1335;MB_MaxHeight;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1375;-1454.369,2376.906;Inherit;False;870;MB_WindDirection;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1362;-1467.426,2723.619;Inherit;False;877;MB_DefaultBending;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1376;-1494.369,2464.906;Inherit;False;1373;MB_WindDirectiionOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1378;-1433.369,2553.906;Inherit;False;1344;AnimatedNoise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;1526;-1471.03,3109.088;Inherit;False;1525;MB_FrequencyOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1371;-1426.102,3477.619;Inherit;False;1340;StaticNoise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;1512;-1444.568,3303.24;Inherit;False;928;DB_PhaseShift;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1471;253.2374,1651.199;Inherit;False;1466;DistanceToCenter;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1472;270.2373,1572.199;Inherit;False;1506;ColorBlendStart;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1365;-1433.102,2819.619;Inherit;False;880;MB_Amplitude;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1553;-2747.811,3194.349;Inherit;False;HorizontalBending - NHP;-1;;76;0b16e2546645f904a949bfd32be36037;0;7;44;FLOAT;1;False;39;FLOAT;1;False;43;FLOAT;1;False;40;FLOAT;0;False;46;FLOAT;2;False;47;FLOAT3;0,0,0;False;45;FLOAT;1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector3Node;1215;-2651.532,3037.867;Float;False;Constant;_Vector2;Vector 2;27;0;Create;True;0;0;False;0;False;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.GetLocalVarNode;1367;-1435.102,3015.619;Inherit;False;873;MB_Frequency;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1369;-1410.102,3210.619;Inherit;False;1360;MB_Phase;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1366;-1472.102,2918.619;Inherit;False;1356;MB_AmplitudeOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1543;-1117.641,2440.42;Inherit;False;RotationAxis - NHP;-1;;86;b90648f17dcc4bc449d46e8cf04564ff;0;3;20;FLOAT;0;False;19;FLOAT;0;False;18;FLOAT4;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SaturateNode;1475;494.2367,1412.199;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;1474;510.2367,1604.199;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1552;-1112.793,3022.883;Inherit;False;RotationAngle - NHP;-1;;85;87b0b7c0fc8f1424db43b84d20c2e79b;0;9;36;FLOAT;0;False;35;FLOAT;0;False;34;FLOAT;1;False;28;FLOAT;1;False;47;FLOAT;0;False;29;FLOAT;1;False;46;FLOAT;0;False;42;FLOAT;0;False;27;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;1214;-2429.143,3112.848;Float;False;Property;_EnableHorizontalBending;Enable Horizontal Bending ;16;0;Create;True;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;1562;-4150.713,2790.191;Inherit;False;Property;_SlopeCorrectionMagnitude;Slope Correction Magnitude;22;0;Create;False;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1478;746.2366,1513.199;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1577;-4146.098,2885.627;Inherit;False;Property;_SlopeCorrectionOffset;Slope Correction Offset;23;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;769;-2067.457,3111.94;Float;False;DB_VertexOffset;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;1476;672.3386,1315.451;Inherit;False;Property;_FlowerColor2;Flower Color 2;1;0;Create;True;0;0;False;0;False;0.8980392,0.9529412,1,1;0.009433985,0.4014694,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;97;-668.509,3020.009;Float;False;MB_RotationAngle;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1420;-674.37,2435.906;Inherit;False;MB_RotationAxis;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;1477;644.5375,1137.234;Inherit;False;Property;_FlowerColor1;Flower Color 1;0;0;Create;True;0;0;False;0;False;0.7843137,0.454902,0.1411765,1;0.5754717,0.3435538,0.1465824,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;1421;-318.9654,2571.713;Inherit;False;1420;MB_RotationAxis;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;1481;-462.4074,1794.486;Inherit;True;Property;_MainTex;Flower Texture;4;1;[NoScaleOffset];Create;False;0;0;False;1;Header(Textures);False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;1507;-267.5425,2768.643;Inherit;False;1504;LocalPivot;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1413;-329.9654,2675.713;Inherit;False;97;MB_RotationAngle;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1563;-3795.713,2789.191;Inherit;False;SlopeCorrectionMagnitude;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1578;-3763.251,2884.635;Inherit;False;SlopeCorrectionOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;1480;963.0616,1294.921;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;1415;-322.9654,2869.713;Inherit;False;769;DB_VertexOffset;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1488;1162.241,1288.754;Inherit;False;Color;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;1565;47.19562,2852.526;Inherit;False;1563;SlopeCorrectionMagnitude;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1582;144.9758,3023.422;Inherit;False;1340;StaticNoise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1487;-94.88531,1792.968;Inherit;False;MainTextureColor;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;1533;-28.90881,2694.302;Inherit;False;MainBending - NHP;-1;;107;01dba1f3bc33e4b4fa301d2180819576;0;4;55;FLOAT3;0,0,0;False;53;FLOAT;0;False;59;FLOAT3;0,0,0;False;58;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1581;78.97589,2938.422;Inherit;False;1578;SlopeCorrectionOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1566;154.4902,3110.377;Inherit;False;1504;LocalPivot;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1489;795.9364,1915.144;Inherit;False;1488;Color;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;1490;733.6945,1815.619;Inherit;False;1487;MainTextureColor;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;1571;399.9267,2894.434;Inherit;False;SlopeCorrection - NHP;-1;;108;af38de3ca0adf3c4ba9b6a3dd482959e;0;5;87;FLOAT3;0,0,0;False;42;FLOAT;1;False;92;FLOAT;0;False;93;FLOAT4;0,0,0,0;False;41;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StaticSwitch;1569;810.7398,2687.917;Float;False;Property;_EnableSlopeCorrection;Enable Slope Correction;21;0;Create;True;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1491;1000.315,1856.81;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1493;1162.918,1850.956;Float;False;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1494;-70.90729,1886.019;Float;False;Opacity;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1045;1162.65,2688.311;Float;False;LocalVertexOffset;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;1265;1794.37,2047.332;Inherit;False;631.4954;512.0168;;4;0;1061;296;1508;Master Node;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;1508;1911.419,2321.128;Inherit;False;1494;Opacity;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;296;1898.882,2109.296;Inherit;False;1493;Albedo;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;1061;1854.228,2424.877;Inherit;False;1045;LocalVertexOffset;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;1534;-3963.828,3024.006;Inherit;False;Property;_AlphaCutoff;Alpha Cutoff;5;0;Create;True;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;1330;-19002.03,10716.32;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;1328;-19355.95,10790.4;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2162.071,2116.35;Float;False;True;-1;2;StylisedFlowerWithoutStem_MaterialInspector;0;0;Standard;Nicrom/NHP/ASE/Vegetation Studio/Stylised Flower Without Stem;False;False;False;False;False;False;True;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Opaque;;AlphaTest;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;True;1534;5;Pragma;multi_compile_instancing;False;;Custom;Pragma;multi_compile GPU_FRUSTUM_ON __;False;;Custom;Pragma;instancing_options procedural:setupScale;False;;Custom;Pragma;instancing_options procedural:setup forwardadd;False;;Custom;Include;VS_indirect.cginc;False;;Custom;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;1503;0;1502;1
WireConnection;1503;2;1502;2
WireConnection;1583;0;1503;0
WireConnection;1504;0;1583;0
WireConnection;1465;0;1463;0
WireConnection;1465;1;1464;0
WireConnection;1506;0;1505;0
WireConnection;1466;0;1465;0
WireConnection;1541;6;1500;0
WireConnection;928;0;738;4
WireConnection;1574;0;850;0
WireConnection;1574;1;1572;0
WireConnection;1574;2;1573;0
WireConnection;1403;0;1249;0
WireConnection;1401;0;1308;0
WireConnection;1221;0;1220;0
WireConnection;1219;0;1218;0
WireConnection;1551;22;1541;0
WireConnection;1551;20;1546;0
WireConnection;1551;24;1579;0
WireConnection;1551;19;1580;0
WireConnection;1575;0;1470;0
WireConnection;877;0;687;0
WireConnection;880;0;300;0
WireConnection;870;0;1574;0
WireConnection;1525;0;1524;0
WireConnection;1340;0;1551;0
WireConnection;1356;0;1262;0
WireConnection;873;0;480;0
WireConnection;1335;0;1334;0
WireConnection;1469;0;1467;0
WireConnection;1469;1;1468;0
WireConnection;1344;0;1551;16
WireConnection;1360;0;1286;0
WireConnection;1373;0;952;0
WireConnection;1473;0;1469;0
WireConnection;1473;1;1576;0
WireConnection;1553;44;1399;0
WireConnection;1553;39;1400;0
WireConnection;1553;43;1405;0
WireConnection;1553;40;1406;0
WireConnection;1553;46;1408;0
WireConnection;1553;47;1520;0
WireConnection;1543;20;1375;0
WireConnection;1543;19;1376;0
WireConnection;1543;18;1378;0
WireConnection;1475;0;1473;0
WireConnection;1474;0;1472;0
WireConnection;1474;1;1471;0
WireConnection;1552;36;1362;0
WireConnection;1552;35;1365;0
WireConnection;1552;34;1366;0
WireConnection;1552;28;1367;0
WireConnection;1552;47;1526;0
WireConnection;1552;29;1369;0
WireConnection;1552;46;1512;0
WireConnection;1552;42;1370;0
WireConnection;1552;27;1371;0
WireConnection;1214;1;1215;0
WireConnection;1214;0;1553;0
WireConnection;1478;0;1475;0
WireConnection;1478;1;1474;0
WireConnection;769;0;1214;0
WireConnection;97;0;1552;0
WireConnection;1420;0;1543;0
WireConnection;1563;0;1562;0
WireConnection;1578;0;1577;0
WireConnection;1480;0;1477;0
WireConnection;1480;1;1476;0
WireConnection;1480;2;1478;0
WireConnection;1488;0;1480;0
WireConnection;1487;0;1481;0
WireConnection;1533;55;1421;0
WireConnection;1533;53;1413;0
WireConnection;1533;59;1507;0
WireConnection;1533;58;1415;0
WireConnection;1571;87;1533;0
WireConnection;1571;42;1565;0
WireConnection;1571;92;1581;0
WireConnection;1571;93;1582;0
WireConnection;1571;41;1566;0
WireConnection;1569;1;1533;0
WireConnection;1569;0;1571;0
WireConnection;1491;0;1490;0
WireConnection;1491;1;1489;0
WireConnection;1493;0;1491;0
WireConnection;1494;0;1481;4
WireConnection;1045;0;1569;0
WireConnection;0;0;296;0
WireConnection;0;10;1508;0
WireConnection;0;11;1061;0
ASEEND*/
//CHKSM=07C28BF823B812F8F26523CFC82FD6682E6D4B6A