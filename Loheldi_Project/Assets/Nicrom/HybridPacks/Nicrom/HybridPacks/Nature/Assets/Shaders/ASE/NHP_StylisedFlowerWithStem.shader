// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Nicrom/NHP/ASE/Stylised Flower With Stem"
{
	Properties
	{
		_FlowerColor1("Flower Color 1", Color) = (0.7843137,0.454902,0.1411765,1)
		_FlowerColor2("Flower Color 2", Color) = (0.8980392,0.9529412,1,1)
		_ColorBlendStart("Color Blend Start", Range( 0 , 1)) = 0.1
		_ColorBlendEnd("Color Blend End", Range( 0 , 1)) = 0.15
		_StemColor("Stem Color", Color) = (0.3960784,0.5647059,0.1019608,1)
		[NoScaleOffset]_MainTex("Flower Texture", 2D) = "white" {}
		[NoScaleOffset]_StemTexture("Stem Texture", 2D) = "white" {}
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
		_MBMaxHeight("MB Max Height", Float) = 1
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
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows nolightmap  dithercrossfade vertex:vertexDataFunc 
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
		uniform sampler2D _StemTexture;
		uniform float4 _FlowerColor1;
		uniform float4 _FlowerColor2;
		uniform float _ColorBlendStart;
		uniform float _ColorBlendEnd;
		uniform float4 _StemColor;
		SamplerState sampler_MainTex;
		SamplerState sampler_StemTexture;
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
			float lerpResult1811 = lerp( _MBWindDir , MBGlobalWindDir , _MBWindDirBlend);
			float MB_WindDirection870 = lerpResult1811;
			float MB_WindDirectionOffset1373 = _MBWindDirOffset;
			float4 transform1_g99 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float3 appendResult1503 = (float3(v.texcoord1.xy.x , 0.0 , v.texcoord1.xy.y));
			float3 LocalPivot1504 = -appendResult1503;
			float4 transform2_g99 = mul(unity_ObjectToWorld,float4( LocalPivot1504 , 0.0 ));
			float2 UVs27_g114 = ( (transform1_g99).xz + (transform2_g99).xz );
			float4 temp_output_24_0_g114 = _NoiseTextureTilling;
			float2 AnimatedNoiseTilling29_g114 = (temp_output_24_0_g114).zw;
			float2 panner7_g114 = ( 0.1 * _Time.y * _NoisePannerSpeed + float2( 0,0 ));
			float4 AnimatedNoise1344 = tex2Dlod( _NoiseTexture, float4( ( ( UVs27_g114 * AnimatedNoiseTilling29_g114 ) + panner7_g114 ), 0, 0.0) );
			float temp_output_11_0_g130 = radians( ( ( MB_WindDirection870 + ( MB_WindDirectionOffset1373 * (-1.0 + ((AnimatedNoise1344).x - 0.0) * (1.0 - -1.0) / (1.0 - 0.0)) ) ) * -1.0 ) );
			float3 appendResult14_g130 = (float3(cos( temp_output_11_0_g130 ) , 0.0 , sin( temp_output_11_0_g130 )));
			float4 transform15_g130 = mul(unity_WorldToObject,float4( appendResult14_g130 , 0.0 ));
			float3 normalizeResult34_g130 = normalize( (transform15_g130).xyz );
			float3 MB_RotationAxis1420 = normalizeResult34_g130;
			float3 RotationAxis56_g131 = MB_RotationAxis1420;
			float MB_Amplitude880 = _MBAmplitude;
			float MB_AmplitudeOffset1356 = _MBAmplitudeOffset;
			float2 StaticNoileTilling28_g114 = (temp_output_24_0_g114).xy;
			float4 StaticNoise1340 = tex2Dlod( _NoiseTexture, float4( ( UVs27_g114 * StaticNoileTilling28_g114 ), 0, 0.0) );
			float4 StaticWorldNoise31_g129 = StaticNoise1340;
			float4 transform8_g129 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float MB_Frequency873 = _MBFrequency;
			float MB_FrequencyOffset1525 = _MBFrequencyOffset;
			float DB_PhaseShift928 = v.color.a;
			float MB_Phase1360 = _MBPhase;
			float MB_DefaultBending877 = _MBDefaultBending;
			float3 ase_vertex3Pos = v.vertex.xyz;
			float MB_MaxHeight1335 = _MBMaxHeight;
			float MB_RotationAngle97 = radians( ( ( ( ( MB_Amplitude880 + ( MB_AmplitudeOffset1356 * (StaticWorldNoise31_g129).x ) ) * sin( ( ( ( transform8_g129.x + transform8_g129.z ) + ( ( _Time.y * ( MB_Frequency873 + ( MB_FrequencyOffset1525 * (StaticWorldNoise31_g129).x ) ) ) + ( ( 2.0 * UNITY_PI ) * DB_PhaseShift928 ) ) ) * MB_Phase1360 ) ) ) + MB_DefaultBending877 ) * ( ase_vertex3Pos.y / MB_MaxHeight1335 ) ) );
			float RotationAngle54_g131 = MB_RotationAngle97;
			float3 PivotPoint60_g131 = LocalPivot1504;
			float3 break62_g131 = PivotPoint60_g131;
			float3 appendResult45_g131 = (float3(break62_g131.x , ase_vertex3Pos.y , break62_g131.z));
			float3 rotatedValue30_g131 = RotateAroundAxis( appendResult45_g131, ase_vertex3Pos, RotationAxis56_g131, RotationAngle54_g131 );
			float DB_HorizontalAmplitude1221 = _DBHorizontalAmplitude;
			float DB_HorizontalFrequency1219 = _DBHorizontalFrequency;
			float Frequency41_g115 = DB_HorizontalFrequency1219;
			float4 transform5_g115 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float DB_HorizontalPhase1401 = _DBHorizontalPhase;
			float3 PivotPoint49_g115 = LocalPivot1504;
			float3 break52_g115 = PivotPoint49_g115;
			float3 appendResult20_g115 = (float3(break52_g115.x , ase_vertex3Pos.y , break52_g115.z));
			float DB_HorizontalMaxRadius1403 = _DBHorizontalMaxRadius;
			float3 rotatedValue33_g115 = RotateAroundAxis( PivotPoint49_g115, ase_vertex3Pos, float3(0,1,0), radians( ( ( DB_HorizontalAmplitude1221 * sin( ( ( ( _Time.y * Frequency41_g115 ) - ( ( 2.0 * UNITY_PI ) * ( 1.0 - DB_PhaseShift928 ) ) ) + ( ( ( transform5_g115.x + transform5_g115.z ) + ( _Time.y * Frequency41_g115 ) ) * DB_HorizontalPhase1401 ) ) ) ) * ( distance( ase_vertex3Pos , appendResult20_g115 ) / DB_HorizontalMaxRadius1403 ) ) ) );
			#ifdef _ENABLEHORIZONTALBENDING_ON
				float3 staticSwitch1214 = ( ( rotatedValue33_g115 - ase_vertex3Pos ) * 1.0 );
			#else
				float3 staticSwitch1214 = float3(0,0,0);
			#endif
			float3 DB_VertexOffset769 = staticSwitch1214;
			float3 rotatedValue34_g131 = RotateAroundAxis( PivotPoint60_g131, ( rotatedValue30_g131 + DB_VertexOffset769 ), RotationAxis56_g131, RotationAngle54_g131 );
			float3 temp_output_1533_0 = ( ( rotatedValue34_g131 - ase_vertex3Pos ) * step( 0.01 , ase_vertex3Pos.y ) );
			float3 MainBending89_g133 = temp_output_1533_0;
			float3 appendResult15_g133 = (float3(0.0 , 1.0 , 0.0));
			float4 transform17_g133 = mul(unity_ObjectToWorld,float4( appendResult15_g133 , 0.0 ));
			float4 break20_g133 = transform17_g133;
			float3 appendResult24_g133 = (float3(-break20_g133.z , 0.0 , break20_g133.x));
			float3 appendResult3_g133 = (float3(0.0 , 1.0 , 0.0));
			float4 transform4_g133 = mul(unity_ObjectToWorld,float4( appendResult3_g133 , 0.0 ));
			float3 lerpResult84_g133 = lerp( float3(0,1,0) , (transform4_g133).xyz , step( 1E-06 , ( abs( transform4_g133.x ) + abs( transform4_g133.z ) ) ));
			float3 normalizeResult7_g133 = normalize( lerpResult84_g133 );
			float dotResult9_g133 = dot( normalizeResult7_g133 , float3(0,1,0) );
			float temp_output_12_0_g133 = acos( dotResult9_g133 );
			float NaNPrevention21_g133 = step( 0.01 , abs( ( temp_output_12_0_g133 * ( 180.0 / UNITY_PI ) ) ) );
			float3 lerpResult26_g133 = lerp( float3(1,0,0) , appendResult24_g133 , NaNPrevention21_g133);
			float4 transform28_g133 = mul(unity_WorldToObject,float4( lerpResult26_g133 , 0.0 ));
			float3 normalizeResult49_g133 = normalize( (transform28_g133).xyz );
			float3 RotationAxis30_g133 = normalizeResult49_g133;
			float SlopeCorrectionOffset1805 = _SlopeCorrectionOffset;
			float SlopeCorrectionMagnitude1775 = _SlopeCorrectionMagnitude;
			float RotationAngle29_g133 = ( saturate( ( (0.0 + ((StaticNoise1340).x - 0.0) * (SlopeCorrectionOffset1805 - 0.0) / (1.0 - 0.0)) + SlopeCorrectionMagnitude1775 ) ) * temp_output_12_0_g133 );
			float3 rotatedValue35_g133 = RotateAroundAxis( LocalPivot1504, ( ase_vertex3Pos + MainBending89_g133 ), RotationAxis30_g133, RotationAngle29_g133 );
			float3 lerpResult52_g133 = lerp( MainBending89_g133 , ( rotatedValue35_g133 - ase_vertex3Pos ) , NaNPrevention21_g133);
			#ifdef _ENABLESLOPECORRECTION_ON
				float3 staticSwitch1781 = lerpResult52_g133;
			#else
				float3 staticSwitch1781 = temp_output_1533_0;
			#endif
			float3 LocalVertexOffset1045 = staticSwitch1781;
			v.vertex.xyz += LocalVertexOffset1045;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_MainTex1481 = i.uv_texcoord;
			float4 tex2DNode1481 = tex2D( _MainTex, uv_MainTex1481 );
			float2 uv_StemTexture1482 = i.uv_texcoord;
			float4 tex2DNode1482 = tex2D( _StemTexture, uv_StemTexture1482 );
			float TextureMask1458 = step( 1.5 , i.uv_texcoord.x );
			float4 lerpResult1486 = lerp( tex2DNode1481 , tex2DNode1482 , TextureMask1458);
			float4 TextureColor1487 = lerpResult1486;
			float DistanceToCenter1466 = distance( float2( 0.5,0.5 ) , i.uv_texcoord );
			float ColorBlendStart1506 = _ColorBlendStart;
			float ColorBlendEnd1812 = _ColorBlendEnd;
			float4 lerpResult1480 = lerp( _FlowerColor1 , _FlowerColor2 , ( saturate( ( ( DistanceToCenter1466 - ColorBlendStart1506 ) / ColorBlendEnd1812 ) ) * step( ColorBlendStart1506 , DistanceToCenter1466 ) ));
			float4 lerpResult1485 = lerp( lerpResult1480 , _StemColor , TextureMask1458);
			float4 Color1488 = lerpResult1485;
			float4 Albedo1493 = ( TextureColor1487 * Color1488 );
			o.Albedo = Albedo1493.rgb;
			float temp_output_1816_0 = 0.0;
			o.Metallic = temp_output_1816_0;
			o.Smoothness = temp_output_1816_0;
			o.Alpha = 1;
			float lerpResult1492 = lerp( tex2DNode1481.a , tex2DNode1482.a , TextureMask1458);
			float Opacity1494 = lerpResult1492;
			clip( Opacity1494 - _AlphaCutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "StylisedFlowerWithStem_MaterialInspector"
}
/*ASEBEGIN
Version=18500
2194.286;690.1429;1156;516;2498.465;-4309.778;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;490;-2426.842,3970.215;Inherit;False;889.0664;633.0813;;10;1458;1457;1455;1456;928;738;1504;1503;1502;1817;Vertex Colors and UVs Baked Data;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1502;-2398.06,4442.469;Inherit;False;1;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;489;-376.9557,770.6545;Inherit;False;1907.837;1399.909;;32;1477;1488;1485;1484;1479;1480;1478;1476;1475;1474;1472;1471;1473;1469;1813;1467;1468;1466;1494;1487;1493;1491;1492;1490;1489;1486;1481;1482;1483;1465;1464;1463;Albedo;1,1,1,1;0;0
Node;AmplifyShaderEditor.DynamicAppendNode;1503;-2141.731,4449.933;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector2Node;1463;-309.8395,846.4044;Inherit;False;Constant;_Vector33;Vector 33;20;0;Create;True;0;0;False;0;False;0.5,0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.NegateNode;1817;-1946.465,4448.778;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;866;-4856.785,2299.094;Inherit;False;1526.371;1405.004;;38;870;1811;1810;1809;1373;952;1805;1804;1335;1334;1506;1505;1534;1775;1774;880;877;873;1360;1356;1525;1262;1524;850;300;1286;480;687;1403;1221;1219;1401;1218;1308;1220;1249;1470;1812;Material Properties;1,1,1,1;0;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;1464;-327.9157,974.8167;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DistanceOpNode;1465;-66.73778,910.1024;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1504;-1770.731,4442.933;Inherit;False;LocalPivot;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;1505;-4807.063,2352.738;Inherit;False;Property;_ColorBlendStart;Color Blend Start;2;0;Create;True;0;0;False;0;False;0.1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;1343;-3074.098,2178.515;Inherit;False;1405.583;634.9444;;8;1500;1536;1340;1344;1784;1815;1538;1814;World Space Noise;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;1500;-2988.906,2216.792;Inherit;False;1504;LocalPivot;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;1470;-4803.965,2453.833;Inherit;False;Property;_ColorBlendEnd;Color Blend End;3;0;Create;True;0;0;False;0;False;0.15;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1506;-4286.782,2351.879;Inherit;False;ColorBlendStart;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1466;117.5732,903.9054;Inherit;False;DistanceToCenter;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1468;-180.3753,1225.116;Inherit;False;1506;ColorBlendStart;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1467;-189.3753,1146.116;Inherit;False;1466;DistanceToCenter;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;1815;-2824.401,2509.249;Inherit;False;Property;_NoiseTextureTilling;Noise Tilling - Static (XY), Animated (ZW);27;0;Create;False;0;0;False;0;False;1,1,1,1;1,1,1,1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;1536;-2774.618,2221.383;Inherit;False;WorldSpaceUVs - NHP;-1;;99;88a2e8a391a04e241878bdb87d9283a3;0;1;6;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;1814;-2754.401,2682.25;Float;False;Property;_NoisePannerSpeed;Noise Panner Speed;28;0;Create;True;0;0;False;0;False;0.05,0.03;0.08,0.1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;1810;-4809.595,3300.19;Inherit;False;Property;_MBWindDirBlend;MB Wind Dir Blend;16;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1308;-4030.775,2579.569;Float;False;Property;_DBHorizontalPhase;DB Horizontal Phase;21;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;1538;-2755.761,2310.993;Inherit;True;Property;_NoiseTexture;Noise Texture;26;1;[NoScaleOffset];Create;True;0;0;False;0;False;None;512fa11ad89d84543ad8d6c8d9cb6743;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.RegisterLocalVarNode;1812;-4284.864,2453.152;Inherit;False;ColorBlendEnd;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;738;-2387.048,4032.784;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1218;-4032.775,2483.569;Float;False;Property;_DBHorizontalFrequency;DB Horizontal Frequency;20;0;Create;True;0;0;False;0;False;1.16;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1809;-4807.595,3212.19;Inherit;False;Global;MBGlobalWindDir;MB Global Wind Dir;28;1;[HideInInspector];Create;True;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;850;-4810.689,3122.742;Float;False;Property;_MBWindDir;MB Wind Dir;14;0;Create;True;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1249;-4032.775,2675.569;Float;False;Property;_DBHorizontalMaxRadius;DB Horizontal Max Radius;22;0;Create;True;0;0;False;0;False;0.05;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1220;-4032.775,2386.569;Float;False;Property;_DBHorizontalAmplitude;DB Horizontal Amplitude;19;0;Create;True;0;0;False;0;False;2;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1524;-4811.99,2929.007;Inherit;False;Property;_MBFrequencyOffset;MB Frequency Offset;12;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;1469;66.62472,1194.116;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1784;-2455.037,2439.188;Inherit;False;WorldSpaceNoise - NHP;-1;;114;af5fa9ff24e18344ebcc05b64d296c57;0;4;22;FLOAT2;0,0;False;20;SAMPLER2D;;False;24;FLOAT4;1,1,1,1;False;19;FLOAT2;0.1,0.1;False;2;COLOR;0;COLOR;16
Node;AmplifyShaderEditor.CommentaryNode;1410;-3067.739,3072.701;Inherit;False;1399.019;639.176;;10;769;1214;1215;1535;1405;1406;1399;1520;1408;1400;Detail Bending;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;480;-4814.087,2834.873;Float;False;Property;_MBFrequency;MB Frequency;11;0;Create;True;0;0;False;0;False;1.11;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;952;-4805.979,3394.934;Float;False;Property;_MBWindDirOffset;MB Wind Dir Offset;15;0;Create;True;0;0;False;0;False;20;0;0;180;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;687;-4814.087,2546.874;Float;False;Property;_MBDefaultBending;MB Default Bending;8;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1813;12.72603,1302.437;Inherit;False;1812;ColorBlendEnd;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1334;-4808.824,3517.175;Inherit;False;Property;_MBMaxHeight;MB Max Height;17;0;Create;True;0;0;False;0;False;1;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1286;-4810.689,3025.742;Float;False;Property;_MBPhase;MB Phase;13;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1401;-3645.776,2580.569;Inherit;False;DB_HorizontalPhase;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1262;-4814.087,2738.873;Float;False;Property;_MBAmplitudeOffset;MB Amplitude Offset;10;0;Create;True;0;0;False;0;False;2;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1219;-3673.776,2486.569;Float;False;DB_HorizontalFrequency;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;928;-1961.451,4118.086;Float;False;DB_PhaseShift;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1403;-3691.776,2671.569;Inherit;False;DB_HorizontalMaxRadius;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1221;-3675.776,2386.569;Float;False;DB_HorizontalAmplitude;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;300;-4814.087,2642.873;Float;False;Property;_MBAmplitude;MB Amplitude;9;0;Create;True;0;0;False;0;False;1.5;1.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;1811;-4496.122,3191.33;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1360;-4292.113,3025.742;Inherit;False;MB_Phase;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1408;-2999.21,3517.783;Inherit;False;1403;DB_HorizontalMaxRadius;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;873;-4295.511,2834.873;Float;False;MB_Frequency;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1472;178.6248,1402.117;Inherit;False;1506;ColorBlendStart;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1344;-1920.92,2511.391;Inherit;False;AnimatedNoise;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;1473;258.6248,1242.116;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;1068;-1405.837,2435.614;Inherit;False;2939.069;1275.755;;28;1045;1781;1808;1807;1779;1806;1780;1533;1415;1421;1413;1507;1420;97;1785;1786;1366;1370;1512;1375;1367;1369;1376;1362;1378;1526;1365;1371;Main Bending;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;1471;146.6248,1482.117;Inherit;False;1466;DistanceToCenter;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1520;-2902.462,3604.302;Inherit;False;1504;LocalPivot;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;880;-4287.511,2642.873;Float;False;MB_Amplitude;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1399;-3002.925,3200.148;Inherit;False;1221;DB_HorizontalAmplitude;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1405;-2974.383,3354.192;Inherit;False;1401;DB_HorizontalPhase;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1406;-2936.383,3433.192;Inherit;False;928;DB_PhaseShift;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;870;-4321.689,3187.193;Float;False;MB_WindDirection;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;1456;-2363.843,4294.766;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1455;-2317.697,4212.805;Inherit;False;Constant;_Float2;Float 2;26;0;Create;True;0;0;False;0;False;1.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1400;-3003.925,3275.148;Inherit;False;1219;DB_HorizontalFrequency;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;877;-4318.511,2546.874;Float;False;MB_DefaultBending;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1335;-4289.527,3514.187;Inherit;False;MB_MaxHeight;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1356;-4327.511,2738.873;Inherit;False;MB_AmplitudeOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1525;-4329.516,2929.165;Inherit;False;MB_FrequencyOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1373;-4343.689,3393.742;Inherit;False;MB_WindDirectionOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1340;-1921.077,2393.786;Inherit;False;StaticNoise;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;1512;-1316.048,3441.396;Inherit;False;928;DB_PhaseShift;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1378;-1310.849,2689.062;Inherit;False;1344;AnimatedNoise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector3Node;1215;-2494.941,3184.301;Float;False;Constant;_Vector2;Vector 2;27;0;Create;True;0;0;False;0;False;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.StepOpNode;1457;-2130.982,4252.969;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1362;-1336.906,2862.775;Inherit;False;877;MB_DefaultBending;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1526;-1340.51,3248.244;Inherit;False;1525;MB_FrequencyOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1369;-1279.582,3349.775;Inherit;False;1360;MB_Phase;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1371;-1294.582,3627.775;Inherit;False;1340;StaticNoise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;1535;-2595.002,3342.698;Inherit;False;HorizontalBending - NHP;-1;;115;0b16e2546645f904a949bfd32be36037;0;7;44;FLOAT;1;False;39;FLOAT;1;False;43;FLOAT;1;False;40;FLOAT;0;False;46;FLOAT;2;False;47;FLOAT3;0,0,0;False;45;FLOAT;1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1370;-1312.51,3539.948;Inherit;False;1335;MB_MaxHeight;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1367;-1304.582,3154.775;Inherit;False;873;MB_Frequency;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1375;-1329.849,2494.062;Inherit;False;870;MB_WindDirection;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;1474;418.6248,1434.117;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;1475;402.6248,1242.116;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1366;-1341.582,3057.775;Inherit;False;1356;MB_AmplitudeOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1376;-1368.849,2594.062;Inherit;False;1373;MB_WindDirectionOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1365;-1302.582,2958.775;Inherit;False;880;MB_Amplitude;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;1477;521.9259,892.152;Inherit;False;Property;_FlowerColor1;Flower Color 1;0;0;Create;True;0;0;False;0;False;0.7843137,0.454902,0.1411765,1;0.5754717,0.3435538,0.1465824,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1478;594.6249,1338.117;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1458;-1958.008,4248.586;Float;False;TextureMask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;1476;551.7269,1072.368;Inherit;False;Property;_FlowerColor2;Flower Color 2;1;0;Create;True;0;0;False;0;False;0.8980392,0.9529412,1,1;0.009433985,0.4014694,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;1786;-982.2729,3162.038;Inherit;False;RotationAngle - NHP;-1;;129;87b0b7c0fc8f1424db43b84d20c2e79b;0;9;36;FLOAT;0;False;35;FLOAT;0;False;34;FLOAT;1;False;28;FLOAT;1;False;47;FLOAT;0;False;29;FLOAT;1;False;46;FLOAT;0;False;42;FLOAT;0.1;False;27;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;1214;-2265.668,3263.249;Float;False;Property;_EnableHorizontalBending;Enable Horizontal Bending ;18;0;Create;True;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;1785;-987.121,2579.576;Inherit;False;RotationAxis - NHP;-1;;130;b90648f17dcc4bc449d46e8cf04564ff;0;3;20;FLOAT;0;False;19;FLOAT;0;False;18;FLOAT4;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1420;-543.849,2575.062;Inherit;False;MB_RotationAxis;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;769;-1900.229,3264.676;Float;False;DB_VertexOffset;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;1804;-4034.173,2886.452;Inherit;False;Property;_SlopeCorrectionOffset;Slope Correction Offset;25;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1774;-4030.819,2787.221;Inherit;False;Property;_SlopeCorrectionMagnitude;Slope Correction Magnitude;24;0;Create;True;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;97;-537.988,3159.164;Float;False;MB_RotationAngle;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;1480;858.4489,1070.839;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1482;-331.9526,1969.693;Inherit;True;Property;_StemTexture;Stem Texture;6;1;[NoScaleOffset];Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;1483;-234.4138,1872.09;Inherit;False;1458;TextureMask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;1479;790.1998,1326.214;Float;False;Property;_StemColor;Stem Color;4;0;Create;True;0;0;False;0;False;0.3960784,0.5647059,0.1019608,1;0.8301887,0.5461104,0.3015307,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;1484;809.6268,1516.508;Inherit;False;1458;TextureMask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1481;-337.8076,1668.115;Inherit;True;Property;_MainTex;Flower Texture;5;1;[NoScaleOffset];Create;False;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;1486;118.5862,1735.09;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;1415;-239.4421,3006.921;Inherit;False;769;DB_VertexOffset;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1805;-3662.326,2885.461;Inherit;False;SlopeCorrectionOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1775;-3690.997,2787.021;Inherit;False;SlopeCorrectionMagnitude;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;1485;1124.392,1278.601;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;1413;-246.4321,2833.488;Inherit;False;97;MB_RotationAngle;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1421;-235.4321,2729.488;Inherit;False;1420;MB_RotationAxis;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1507;-199.0092,2912.417;Inherit;False;1504;LocalPivot;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1806;168.9883,3096.126;Inherit;False;1805;SlopeCorrectionOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1488;1316.935,1272.132;Inherit;False;Color;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;1779;241.8991,3287.028;Inherit;False;1504;LocalPivot;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1780;139.4043,3006.25;Inherit;False;1775;SlopeCorrectionMagnitude;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1533;64.104,2854.921;Inherit;False;MainBending - NHP;-1;;131;01dba1f3bc33e4b4fa301d2180819576;0;4;55;FLOAT3;0,0,0;False;53;FLOAT;0;False;59;FLOAT3;0,0,0;False;58;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1487;327.5022,1731.593;Inherit;False;TextureColor;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;1807;230.9883,3193.126;Inherit;False;1340;StaticNoise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;1490;823.9894,1838.812;Inherit;False;1487;TextureColor;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;1808;507.5047,3057.542;Inherit;False;SlopeCorrection - NHP;-1;;133;af38de3ca0adf3c4ba9b6a3dd482959e;0;5;87;FLOAT3;0,0,0;False;42;FLOAT;1;False;92;FLOAT;0;False;93;FLOAT4;0,0,0,0;False;41;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1489;851.2313,1937.337;Inherit;False;1488;Color;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1491;1069.609,1881.003;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;1492;107.5862,1977.09;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;1781;936.2124,2848.605;Float;False;Property;_EnableSlopeCorrection;Enable Slope Correction;23;0;Create;True;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1493;1234.211,1877.149;Float;False;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1045;1295.557,2849.422;Float;False;LocalVertexOffset;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;1265;1923.979,2049.843;Inherit;False;631.4954;512.0168;;4;0;1508;1574;296;Master Node;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1494;322.1372,1971.11;Float;False;Opacity;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1816;2105.049,2220.133;Inherit;False;Constant;_Float3;Float 3;30;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;1330;-19002.03,10716.32;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;296;2053.489,2106.808;Inherit;False;1493;Albedo;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;1328;-19355.95,10790.4;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1534;-3836.964,3010.247;Inherit;False;Property;_AlphaCutoff;Alpha Cutoff;7;0;Create;True;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1508;2068.027,2295.639;Inherit;False;1494;Opacity;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1574;2016.391,2403.527;Inherit;False;1045;LocalVertexOffset;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2291.679,2118.861;Float;False;True;-1;2;StylisedFlowerWithStem_MaterialInspector;0;0;Standard;Nicrom/NHP/ASE/Stylised Flower With Stem;False;False;False;False;False;False;True;False;False;False;False;False;True;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Opaque;;AlphaTest;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;True;1534;1;Pragma;multi_compile_instancing;False;;Custom;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;1503;0;1502;1
WireConnection;1503;2;1502;2
WireConnection;1817;0;1503;0
WireConnection;1465;0;1463;0
WireConnection;1465;1;1464;0
WireConnection;1504;0;1817;0
WireConnection;1506;0;1505;0
WireConnection;1466;0;1465;0
WireConnection;1536;6;1500;0
WireConnection;1812;0;1470;0
WireConnection;1469;0;1467;0
WireConnection;1469;1;1468;0
WireConnection;1784;22;1536;0
WireConnection;1784;20;1538;0
WireConnection;1784;24;1815;0
WireConnection;1784;19;1814;0
WireConnection;1401;0;1308;0
WireConnection;1219;0;1218;0
WireConnection;928;0;738;4
WireConnection;1403;0;1249;0
WireConnection;1221;0;1220;0
WireConnection;1811;0;850;0
WireConnection;1811;1;1809;0
WireConnection;1811;2;1810;0
WireConnection;1360;0;1286;0
WireConnection;873;0;480;0
WireConnection;1344;0;1784;16
WireConnection;1473;0;1469;0
WireConnection;1473;1;1813;0
WireConnection;880;0;300;0
WireConnection;870;0;1811;0
WireConnection;877;0;687;0
WireConnection;1335;0;1334;0
WireConnection;1356;0;1262;0
WireConnection;1525;0;1524;0
WireConnection;1373;0;952;0
WireConnection;1340;0;1784;0
WireConnection;1457;0;1455;0
WireConnection;1457;1;1456;1
WireConnection;1535;44;1399;0
WireConnection;1535;39;1400;0
WireConnection;1535;43;1405;0
WireConnection;1535;40;1406;0
WireConnection;1535;46;1408;0
WireConnection;1535;47;1520;0
WireConnection;1474;0;1472;0
WireConnection;1474;1;1471;0
WireConnection;1475;0;1473;0
WireConnection;1478;0;1475;0
WireConnection;1478;1;1474;0
WireConnection;1458;0;1457;0
WireConnection;1786;36;1362;0
WireConnection;1786;35;1365;0
WireConnection;1786;34;1366;0
WireConnection;1786;28;1367;0
WireConnection;1786;47;1526;0
WireConnection;1786;29;1369;0
WireConnection;1786;46;1512;0
WireConnection;1786;42;1370;0
WireConnection;1786;27;1371;0
WireConnection;1214;1;1215;0
WireConnection;1214;0;1535;0
WireConnection;1785;20;1375;0
WireConnection;1785;19;1376;0
WireConnection;1785;18;1378;0
WireConnection;1420;0;1785;0
WireConnection;769;0;1214;0
WireConnection;97;0;1786;0
WireConnection;1480;0;1477;0
WireConnection;1480;1;1476;0
WireConnection;1480;2;1478;0
WireConnection;1486;0;1481;0
WireConnection;1486;1;1482;0
WireConnection;1486;2;1483;0
WireConnection;1805;0;1804;0
WireConnection;1775;0;1774;0
WireConnection;1485;0;1480;0
WireConnection;1485;1;1479;0
WireConnection;1485;2;1484;0
WireConnection;1488;0;1485;0
WireConnection;1533;55;1421;0
WireConnection;1533;53;1413;0
WireConnection;1533;59;1507;0
WireConnection;1533;58;1415;0
WireConnection;1487;0;1486;0
WireConnection;1808;87;1533;0
WireConnection;1808;42;1780;0
WireConnection;1808;92;1806;0
WireConnection;1808;93;1807;0
WireConnection;1808;41;1779;0
WireConnection;1491;0;1490;0
WireConnection;1491;1;1489;0
WireConnection;1492;0;1481;4
WireConnection;1492;1;1482;4
WireConnection;1492;2;1483;0
WireConnection;1781;1;1533;0
WireConnection;1781;0;1808;0
WireConnection;1493;0;1491;0
WireConnection;1045;0;1781;0
WireConnection;1494;0;1492;0
WireConnection;0;0;296;0
WireConnection;0;3;1816;0
WireConnection;0;4;1816;0
WireConnection;0;10;1508;0
WireConnection;0;11;1574;0
ASEEND*/
//CHKSM=506F1064857D6AAA4AA5E9815E784B72E1237524