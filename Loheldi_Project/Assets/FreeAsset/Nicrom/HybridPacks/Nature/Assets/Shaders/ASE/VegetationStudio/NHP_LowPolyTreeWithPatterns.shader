// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Nicrom/NHP/ASE/Vegetation Studio/Low Poly Tree With Patterns"
{
	Properties
	{
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_PatternColor1("Pattern Color 1", Color) = (1,1,1,1)
		_PatternColor2("Pattern Color 2", Color) = (0,0,0,1)
		[NoScaleOffset]_MainTex("Main Texture", 2D) = "white" {}
		[NoScaleOffset]_PatternTex("Pattern Texture", 2D) = "white" {}
		[NoScaleOffset]_NoiseTexture("Noise Texture", 2D) = "white" {}
		_NoiseTextureTilling("Noise Tilling - Static (XY), Animated (ZW)", Vector) = (1,1,1,1)
		_NoisePannerSpeed("Noise Panner Speed", Vector) = (0.1,0.1,0,0)
		_MBDefaultBending("MB Default Bending", Float) = 0
		_MBAmplitude("MB Amplitude", Float) = 1.5
		_MBAmplitudeOffset("MB Amplitude Offset", Float) = 2
		_MBFrequency("MB Frequency", Float) = 1.11
		_MBFrequencyOffset("MB Frequency Offset", Float) = 0
		_MBPhase("MB Phase", Float) = 1
		_MBWindDirBlend("MB Wind Dir Blend", Range( 0 , 1)) = 0
		_MBWindDir("MB Wind Dir", Range( 0 , 360)) = 0
		_MBWindDirOffset("MB Wind Dir Offset", Range( 0 , 180)) = 20
		_MBMaxHeight("MB Max Height", Float) = 10
		[Toggle(_ENABLEVERTICALBENDING_ON)] _EnableVerticalBending("Enable Vertical Bending", Float) = 1
		_DBVerticalAmplitude("DB Vertical Amplitude", Float) = 1
		_DBVerticalAmplitudeOffset("DB Vertical Amplitude Offset", Float) = 1.2
		_DBVerticalFrequency("DB Vertical Frequency", Float) = 1.15
		_DBVerticalPhase("DB Vertical Phase", Float) = 1
		_DBVerticalMaxLength("DB Vertical Max Length", Float) = 2
		[Toggle(_ENABLEHORIZONTALBENDING_ON)] _EnableHorizontalBending("Enable Horizontal Bending", Float) = 1
		_DBHorizontalAmplitude("DB Horizontal Amplitude", Float) = 2
		_DBHorizontalFrequency("DB Horizontal Frequency", Float) = 1.16
		_DBHorizontalPhase("DB Horizontal Phase", Float) = 1
		_DBHorizontalMaxRadius("DB Horizontal Max Radius", Float) = 2
		_UnitScale("Unit Scale", Float) = 20
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma shader_feature_local _ENABLEVERTICALBENDING_ON
		#pragma shader_feature_local _ENABLEHORIZONTALBENDING_ON
		#pragma multi_compile_instancing
		#pragma multi_compile GPU_FRUSTUM_ON __
		#pragma instancing_options procedural:setup
		#pragma instancing_options procedural:setup forwardadd
		#include "VS_indirect.cginc"
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
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
		uniform float _DBVerticalAmplitude;
		uniform float _DBVerticalAmplitudeOffset;
		uniform float _DBVerticalFrequency;
		uniform float _DBVerticalPhase;
		uniform float _UnitScale;
		uniform float _DBVerticalMaxLength;
		uniform float _DBHorizontalAmplitude;
		uniform float _DBHorizontalFrequency;
		uniform float _DBHorizontalPhase;
		uniform float _DBHorizontalMaxRadius;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform float4 _PatternColor2;
		uniform float4 _PatternColor1;
		uniform sampler2D _PatternTex;
		uniform float4 _PatternTex_ST;
		uniform float _Metallic;
		uniform float _Smoothness;


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
			float lerpResult1521 = lerp( _MBWindDir , MBGlobalWindDir , _MBWindDirBlend);
			float MB_WindDirection870 = lerpResult1521;
			float MB_WindDirectionOffset1373 = _MBWindDirOffset;
			float4 transform1501 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float2 appendResult1502 = (float2(transform1501.x , transform1501.z));
			float2 UVs27_g89 = appendResult1502;
			float4 temp_output_24_0_g89 = _NoiseTextureTilling;
			float2 AnimatedNoiseTilling29_g89 = (temp_output_24_0_g89).zw;
			float2 panner7_g89 = ( 0.1 * _Time.y * _NoisePannerSpeed + float2( 0,0 ));
			float4 AnimatedNoise1344 = tex2Dlod( _NoiseTexture, float4( ( ( UVs27_g89 * AnimatedNoiseTilling29_g89 ) + panner7_g89 ), 0, 0.0) );
			float temp_output_11_0_g91 = radians( ( ( MB_WindDirection870 + ( MB_WindDirectionOffset1373 * (-1.0 + ((AnimatedNoise1344).x - 0.0) * (1.0 - -1.0) / (1.0 - 0.0)) ) ) * -1.0 ) );
			float3 appendResult14_g91 = (float3(cos( temp_output_11_0_g91 ) , 0.0 , sin( temp_output_11_0_g91 )));
			float4 transform15_g91 = mul(unity_WorldToObject,float4( appendResult14_g91 , 0.0 ));
			float3 normalizeResult34_g91 = normalize( (transform15_g91).xyz );
			float3 MB_RotationAxis1420 = normalizeResult34_g91;
			float3 RotationAxis56_g92 = MB_RotationAxis1420;
			float MB_Amplitude880 = _MBAmplitude;
			float MB_AmplitudeOffset1356 = _MBAmplitudeOffset;
			float2 StaticNoileTilling28_g89 = (temp_output_24_0_g89).xy;
			float4 StaticNoise1340 = tex2Dlod( _NoiseTexture, float4( ( UVs27_g89 * StaticNoileTilling28_g89 ), 0, 0.0) );
			float4 StaticWorldNoise31_g90 = StaticNoise1340;
			float4 transform8_g90 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float MB_Frequency873 = _MBFrequency;
			float MB_FrequencyOffset1483 = _MBFrequencyOffset;
			float MB_Phase1360 = _MBPhase;
			float MB_DefaultBending877 = _MBDefaultBending;
			float3 ase_vertex3Pos = v.vertex.xyz;
			float MB_MaxHeight1335 = _MBMaxHeight;
			float MB_RotationAngle97 = radians( ( ( ( ( MB_Amplitude880 + ( MB_AmplitudeOffset1356 * (StaticWorldNoise31_g90).x ) ) * sin( ( ( ( transform8_g90.x + transform8_g90.z ) + ( ( _Time.y * ( MB_Frequency873 + ( MB_FrequencyOffset1483 * (StaticWorldNoise31_g90).x ) ) ) + ( ( 2.0 * UNITY_PI ) * 0.0 ) ) ) * MB_Phase1360 ) ) ) + MB_DefaultBending877 ) * ( ase_vertex3Pos.y / MB_MaxHeight1335 ) ) );
			float RotationAngle54_g92 = MB_RotationAngle97;
			float3 PivotPoint60_g92 = float3( 0,0,0 );
			float3 break62_g92 = PivotPoint60_g92;
			float3 appendResult45_g92 = (float3(break62_g92.x , ase_vertex3Pos.y , break62_g92.z));
			float3 rotatedValue30_g92 = RotateAroundAxis( appendResult45_g92, ase_vertex3Pos, RotationAxis56_g92, RotationAngle54_g92 );
			float temp_output_4_0_g79 = radians( ( v.color.b * 360.0 ) );
			float3 appendResult10_g79 = (float3(cos( temp_output_4_0_g79 ) , 0.0 , sin( temp_output_4_0_g79 )));
			float3 DB_RotationAxis757 = appendResult10_g79;
			float DB_VerticalAmplitude887 = _DBVerticalAmplitude;
			float DB_VerticalAmplitudeOffset1423 = _DBVerticalAmplitudeOffset;
			float DB_PhaseShift928 = v.color.a;
			float PhaseShift48_g87 = DB_PhaseShift928;
			float DB_VerticalFrequency883 = _DBVerticalFrequency;
			float Fequency45_g87 = DB_VerticalFrequency883;
			float4 transform2_g87 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float DB_VerticalPhase1384 = _DBVerticalPhase;
			float3 appendResult12_g79 = (float3(0.0 , ( _UnitScale * v.color.g ) , 0.0));
			float3 DB_PivotPosOnYAxis756 = appendResult12_g79;
			float3 PivotPosOnYAxis56_g87 = DB_PivotPosOnYAxis756;
			float DB_VerticalMaxLength1388 = _DBVerticalMaxLength;
			float3 rotatedValue29_g87 = RotateAroundAxis( PivotPosOnYAxis56_g87, ase_vertex3Pos, DB_RotationAxis757, radians( ( ( ( DB_VerticalAmplitude887 + ( DB_VerticalAmplitudeOffset1423 * ( 1.0 - PhaseShift48_g87 ) ) ) * sin( ( ( ( _Time.y * Fequency45_g87 ) - ( ( 2.0 * UNITY_PI ) * PhaseShift48_g87 ) ) + ( ( ( transform2_g87.x + transform2_g87.z ) + ( _Time.y * Fequency45_g87 ) ) * DB_VerticalPhase1384 ) ) ) ) * ( distance( ase_vertex3Pos , PivotPosOnYAxis56_g87 ) / DB_VerticalMaxLength1388 ) ) ) );
			float DetailBendingMask1499 = step( 1.5 , v.texcoord.xy.x );
			float3 DB_VerticalBending1314 = ( ( rotatedValue29_g87 - ase_vertex3Pos ) * DetailBendingMask1499 );
			#ifdef _ENABLEVERTICALBENDING_ON
				float3 staticSwitch960 = DB_VerticalBending1314;
			#else
				float3 staticSwitch960 = float3(0,0,0);
			#endif
			float DB_HorizontalAmplitude1221 = _DBHorizontalAmplitude;
			float DB_HorizontalFrequency1219 = _DBHorizontalFrequency;
			float Frequency41_g88 = DB_HorizontalFrequency1219;
			float4 transform5_g88 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float DB_HorizontalPhase1401 = _DBHorizontalPhase;
			float3 PivotPoint49_g88 = float3( 0,0,0 );
			float3 break52_g88 = PivotPoint49_g88;
			float3 appendResult20_g88 = (float3(break52_g88.x , ase_vertex3Pos.y , break52_g88.z));
			float DB_HorizontalMaxRadius1403 = _DBHorizontalMaxRadius;
			float3 rotatedValue33_g88 = RotateAroundAxis( PivotPoint49_g88, ase_vertex3Pos, float3(0,1,0), radians( ( ( DB_HorizontalAmplitude1221 * sin( ( ( ( _Time.y * Frequency41_g88 ) - ( ( 2.0 * UNITY_PI ) * ( 1.0 - DB_PhaseShift928 ) ) ) + ( ( ( transform5_g88.x + transform5_g88.z ) + ( _Time.y * Frequency41_g88 ) ) * DB_HorizontalPhase1401 ) ) ) ) * ( distance( ase_vertex3Pos , appendResult20_g88 ) / DB_HorizontalMaxRadius1403 ) ) ) );
			float3 DB_HorizontalBending1315 = ( ( rotatedValue33_g88 - ase_vertex3Pos ) * DetailBendingMask1499 );
			#ifdef _ENABLEHORIZONTALBENDING_ON
				float3 staticSwitch1214 = DB_HorizontalBending1315;
			#else
				float3 staticSwitch1214 = float3(0,0,0);
			#endif
			float3 DB_VertexOffset769 = ( staticSwitch960 + staticSwitch1214 );
			float3 rotatedValue34_g92 = RotateAroundAxis( PivotPoint60_g92, ( rotatedValue30_g92 + DB_VertexOffset769 ), RotationAxis56_g92, RotationAngle54_g92 );
			float3 LocalVertexOffset1045 = ( ( rotatedValue34_g92 - ase_vertex3Pos ) * step( 0.01 , ase_vertex3Pos.y ) );
			v.vertex.xyz += LocalVertexOffset1045;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			float2 uv_PatternTex = i.uv_texcoord * _PatternTex_ST.xy + _PatternTex_ST.zw;
			float4 lerpResult1467 = lerp( _PatternColor2 , _PatternColor1 , tex2D( _PatternTex, uv_PatternTex ));
			float TextureMask1456 = step( i.uv_texcoord.y , -1.0 );
			float4 lerpResult1462 = lerp( tex2D( _MainTex, uv_MainTex ) , lerpResult1467 , TextureMask1456);
			float4 Albedo292 = lerpResult1462;
			o.Albedo = Albedo292.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "LowPolyTreeWithPatterns_MaterialInspector"
}
/*ASEBEGIN
Version=18500
2194.286;690.1429;1110;647;7806.464;-186.2614;8.20883;True;False
Node;AmplifyShaderEditor.CommentaryNode;490;-5626.81,3967.678;Inherit;False;888.8715;764.828;;13;1456;1455;1454;1457;756;1499;928;757;1498;1515;989;1496;1497;Vertex Colors and UVs Baked Data;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;1496;-5535.044,4297.787;Inherit;False;Constant;_Float1;Float 1;26;0;Create;True;0;0;False;0;False;1.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;989;-5604.563,4069.742;Float;False;Property;_UnitScale;Unit Scale;30;0;Create;True;0;0;False;0;False;20;20;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;866;-6267.093,2557.093;Inherit;False;1524.165;1150.683;;39;1356;873;1360;1483;1373;870;1335;880;877;1262;952;1334;850;480;1482;300;687;1286;1423;1403;883;1221;1388;1384;1219;1401;887;749;1249;1308;1301;780;792;1288;1220;1218;1520;1519;1521;Material Properties;1,1,1,1;0;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;1497;-5581.19,4380.747;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1308;-5383.727,3330.877;Float;False;Property;_DBHorizontalPhase;DB Horizontal Phase;28;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;1498;-5348.329,4337.952;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;749;-5383.727,2626.877;Float;False;Property;_DBVerticalAmplitude;DB Vertical Amplitude;20;0;Create;True;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1288;-5383.727,2722.876;Float;False;Property;_DBVerticalAmplitudeOffset;DB Vertical Amplitude Offset;21;0;Create;True;0;0;False;0;False;1.2;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1515;-5419.187,4074.058;Inherit;False;VertexColorData - NHP;-1;;79;0242ce46c610b224e91bc03a7bf52b77;0;1;17;FLOAT;0;False;3;FLOAT3;19;FLOAT3;0;FLOAT;18
Node;AmplifyShaderEditor.RangedFloatNode;1249;-5383.727,3426.877;Float;False;Property;_DBHorizontalMaxRadius;DB Horizontal Max Radius;29;0;Create;True;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;792;-5383.727,3010.877;Float;False;Property;_DBVerticalMaxLength;DB Vertical Max Length;24;0;Create;True;0;0;False;0;False;2;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1301;-5383.727,2914.877;Float;False;Property;_DBVerticalPhase;DB Vertical Phase;23;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;780;-5383.727,2819.877;Float;False;Property;_DBVerticalFrequency;DB Vertical Frequency;22;0;Create;True;0;0;False;0;False;1.15;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1218;-5383.727,3233.877;Float;False;Property;_DBHorizontalFrequency;DB Horizontal Frequency;27;0;Create;True;0;0;False;0;False;1.16;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1220;-5383.727,3138.877;Float;False;Property;_DBHorizontalAmplitude;DB Horizontal Amplitude;26;0;Create;True;0;0;False;0;False;2;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;887;-5047.727,2626.877;Float;False;DB_VerticalAmplitude;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1221;-5063.727,3138.877;Float;False;DB_HorizontalAmplitude;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;756;-5017.158,4023.232;Float;False;DB_PivotPosOnYAxis;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;1500;-6017.013,1539.028;Inherit;False;1273.985;763.6335;Comment;8;1344;1340;1511;1518;1504;1502;1506;1501;World Position Noise;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1403;-5051.727,3425.877;Inherit;False;DB_HorizontalMaxRadius;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1384;-5015.727,2914.877;Inherit;False;DB_VerticalPhase;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1219;-5063.727,3234.877;Float;False;DB_HorizontalFrequency;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1499;-5192.694,4331.567;Float;False;DetailBendingMask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;1410;-4478.186,2307.631;Inherit;False;2306.147;1276.802;;25;769;1216;1214;960;1316;1317;959;1215;1314;1315;1405;1394;1408;1399;1406;1392;1381;1400;1383;1380;1391;1409;1382;1395;1393;Detail Bending;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;757;-5013.934,4109.719;Float;False;DB_RotationAxis;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;928;-5014.762,4202.729;Float;False;DB_PhaseShift;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;883;-5047.727,2818.877;Float;False;DB_VerticalFrequency;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1388;-5045.727,3012.877;Inherit;False;DB_VerticalMaxLength;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1423;-5082.727,2721.876;Inherit;False;DB_VerticalAmplitudeOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1401;-5031.727,3330.877;Inherit;False;DB_HorizontalPhase;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1395;-4381.298,3005.98;Inherit;False;1499;DetailBendingMask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1391;-4351.379,2679.246;Inherit;False;928;DB_PhaseShift;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1406;-4355.955,3346.02;Inherit;False;928;DB_PhaseShift;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1393;-4369.923,2838.003;Inherit;False;757;DB_RotationAxis;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1382;-4407.896,2516.63;Inherit;False;883;DB_VerticalFrequency;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1383;-4379.896,2598.63;Inherit;False;1384;DB_VerticalPhase;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1400;-4422.497,3180.976;Inherit;False;1219;DB_HorizontalFrequency;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1392;-4402.923,2756.003;Inherit;False;1388;DB_VerticalMaxLength;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ObjectToWorldTransfNode;1501;-5945.001,1586.97;Inherit;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;1405;-4393.955,3265.02;Inherit;False;1401;DB_HorizontalPhase;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1381;-4441.896,2435.63;Inherit;False;1423;DB_VerticalAmplitudeOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1380;-4405.896,2354.631;Inherit;False;887;DB_VerticalAmplitude;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1399;-4420.497,3097.976;Inherit;False;1221;DB_HorizontalAmplitude;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1408;-4415.782,3424.611;Inherit;False;1403;DB_HorizontalMaxRadius;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1394;-4394.153,2920.871;Inherit;False;756;DB_PivotPosOnYAxis;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1409;-4386.31,3502.079;Inherit;False;1499;DetailBendingMask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;1502;-5738.668,1614.306;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;1504;-5820.254,1775.649;Inherit;True;Property;_NoiseTexture;Noise Texture;6;1;[NoScaleOffset];Create;True;0;0;False;0;False;None;512fa11ad89d84543ad8d6c8d9cb6743;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.RangedFloatNode;1519;-6222.556,3363.691;Inherit;False;Property;_MBWindDirBlend;MB Wind Dir Blend;15;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1520;-6220.556,3283.691;Inherit;False;Global;MBGlobalWindDir;MB Global Wind Dir;28;1;[HideInInspector];Create;True;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;1506;-5814.558,2157.492;Float;False;Property;_NoisePannerSpeed;Noise Panner Speed;8;0;Create;True;0;0;False;0;False;0.1,0.1;0.08,0.1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.FunctionNode;1513;-3972.722,3229.207;Inherit;False;HorizontalBending - NHP;-1;;88;0b16e2546645f904a949bfd32be36037;0;7;44;FLOAT;1;False;39;FLOAT;1;False;43;FLOAT;1;False;40;FLOAT;0;False;46;FLOAT;2;False;47;FLOAT3;0,0,0;False;45;FLOAT;1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;850;-6216.192,3198.146;Float;False;Property;_MBWindDir;MB Wind Dir;16;0;Create;True;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1512;-3972.171,2587.333;Inherit;False;VerticalBending - NHP;-1;;87;41809ea7184502144ad776d88ecd1913;0;9;52;FLOAT;1;False;51;FLOAT;1;False;42;FLOAT;1;False;43;FLOAT;1;False;44;FLOAT;0;False;54;FLOAT;2;False;55;FLOAT3;0,0,0;False;53;FLOAT3;0,0,0;False;58;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector4Node;1518;-5888.433,1976.041;Inherit;False;Property;_NoiseTextureTilling;Noise Tilling - Static (XY), Animated (ZW);7;0;Create;False;0;0;False;0;False;1,1,1,1;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1262;-6228.192,2815.146;Float;False;Property;_MBAmplitudeOffset;MB Amplitude Offset;11;0;Create;True;0;0;False;0;False;2;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;480;-6221.192,2910.146;Float;False;Property;_MBFrequency;MB Frequency;12;0;Create;True;0;0;False;0;False;1.11;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1286;-6214.192,3096.146;Float;False;Property;_MBPhase;MB Phase;14;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1334;-6222.192,3566.146;Inherit;False;Property;_MBMaxHeight;MB Max Height;18;0;Create;True;0;0;False;0;False;10;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;952;-6222.482,3461.336;Float;False;Property;_MBWindDirOffset;MB Wind Dir Offset;17;0;Create;True;0;0;False;0;False;20;0;0;180;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1482;-6221.055,3006.571;Inherit;False;Property;_MBFrequencyOffset;MB Frequency Offset;13;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1314;-3635.776,2580.486;Float;False;DB_VerticalBending;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;300;-6221.192,2722.146;Float;False;Property;_MBAmplitude;MB Amplitude;10;0;Create;True;0;0;False;0;False;1.5;1.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1511;-5510.461,1859.439;Inherit;False;WorldSpaceNoise - NHP;-1;;89;af5fa9ff24e18344ebcc05b64d296c57;0;4;22;FLOAT2;0,0;False;20;SAMPLER2D;;False;24;FLOAT4;1,1,1,1;False;19;FLOAT2;0.1,0.1;False;2;COLOR;0;COLOR;16
Node;AmplifyShaderEditor.RegisterLocalVarNode;1315;-3638.597,3224.547;Float;False;DB_HorizontalBending;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;1521;-5890.41,3261.754;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;687;-6221.192,2627.146;Float;False;Property;_MBDefaultBending;MB Default Bending;9;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;1215;-3206.568,2945.181;Float;False;Constant;_Vector2;Vector 2;27;0;Create;True;0;0;False;0;False;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RegisterLocalVarNode;1483;-5744.157,3003.729;Inherit;False;MB_FrequencyOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1344;-4975.422,1921.677;Inherit;False;AnimatedNoise;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;880;-5708.192,2719.146;Float;False;MB_Amplitude;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;870;-5718.192,3255.595;Float;False;MB_WindDirection;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1340;-4978.033,1821.486;Inherit;False;StaticNoise;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;1068;-1916.261,2304.453;Inherit;False;2171.982;1274.017;;20;1490;1421;1415;1413;97;1420;1486;1488;1375;1369;1367;1371;1370;1362;1376;1366;1484;1365;1378;1045;Main Bending;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1356;-5740.192,2815.146;Inherit;False;MB_AmplitudeOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;877;-5740.192,2623.146;Float;False;MB_DefaultBending;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1316;-3290.229,2842.865;Inherit;False;1314;DB_VerticalBending;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;873;-5708.192,2911.146;Float;False;MB_Frequency;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1335;-5691.192,3564.146;Inherit;False;MB_MaxHeight;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1317;-3328.351,3100.079;Inherit;False;1315;DB_HorizontalBending;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector3Node;959;-3209.762,2686.052;Float;False;Constant;_Vector0;Vector 0;27;0;Create;True;0;0;False;0;False;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RegisterLocalVarNode;1373;-5756.192,3462.146;Inherit;False;MB_WindDirectionOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;489;-3924.754,1147.031;Inherit;False;1741.532;897.0291;;12;1463;292;1462;1467;1460;1459;1458;1465;1466;295;294;515;Albedo;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1360;-5703.192,3097.146;Inherit;False;MB_Phase;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1365;-1754.816,2875.125;Inherit;False;880;MB_Amplitude;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;1214;-2984.18,3020.163;Float;False;Property;_EnableHorizontalBending;Enable Horizontal Bending;25;0;Create;True;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TexturePropertyNode;1458;-3873.645,1809.417;Float;True;Property;_PatternTex;Pattern Texture;5;1;[NoScaleOffset];Create;False;0;0;False;0;False;None;50e8be33b1640034bbc8e6dacf43c1ee;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.RangedFloatNode;1454;-5527.684,4639.243;Inherit;False;Constant;_Float2;Float 2;26;0;Create;True;0;0;False;0;False;-1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;1457;-5579.529,4512.202;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;960;-2977.889,2753.048;Float;False;Property;_EnableVerticalBending;Enable Vertical Bending;19;0;Create;True;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1484;-1791.452,3159.005;Inherit;False;1483;MB_FrequencyOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1378;-1768.083,2616.412;Inherit;False;1344;AnimatedNoise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;1376;-1820.083,2524.412;Inherit;False;1373;MB_WindDirectionOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1362;-1789.816,2778.125;Inherit;False;877;MB_DefaultBending;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1366;-1789.816,2971.125;Inherit;False;1356;MB_AmplitudeOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1371;-1728.798,3450.789;Inherit;False;1340;StaticNoise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;1370;-1759.727,3350.962;Inherit;False;1335;MB_MaxHeight;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1369;-1726.798,3259.789;Inherit;False;1360;MB_Phase;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1375;-1785.083,2425.412;Inherit;False;870;MB_WindDirection;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1367;-1754.816,3067.125;Inherit;False;873;MB_Frequency;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;1455;-5337.966,4591.408;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1216;-2593.533,2875.086;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TexturePropertyNode;515;-3643.76,1209.741;Float;True;Property;_MainTex;Main Texture;4;1;[NoScaleOffset];Create;False;0;0;False;0;False;None;55c6017e9943d5e4a9eb32bcfde5360a;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.TextureCoordinatesNode;1459;-3603.499,1896.285;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;1488;-1433.304,3008.29;Inherit;False;RotationAngle - NHP;-1;;90;87b0b7c0fc8f1424db43b84d20c2e79b;0;9;36;FLOAT;0;False;35;FLOAT;0;False;34;FLOAT;1;False;28;FLOAT;1;False;47;FLOAT;0;False;29;FLOAT;1;False;46;FLOAT;0;False;42;FLOAT;0;False;27;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1486;-1433.083,2507.412;Inherit;False;RotationAxis - NHP;-1;;91;b90648f17dcc4bc449d46e8cf04564ff;0;3;20;FLOAT;0;False;19;FLOAT;0;False;18;FLOAT4;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;1460;-3320.9,1808.536;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;1;[NoScaleOffset];Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;1456;-5168.333,4586.722;Float;False;TextureMask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;1465;-3230.339,1631.972;Inherit;False;Property;_PatternColor1;Pattern Color 1;2;0;Create;True;0;0;False;0;False;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;1466;-3229.366,1455.992;Inherit;False;Property;_PatternColor2;Pattern Color 2;3;0;Create;True;0;0;False;0;False;0,0,0,1;0,0,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;294;-3367.114,1292.109;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;1420;-984.0838,2503.412;Inherit;False;MB_RotationAxis;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;769;-2428.385,2871.099;Float;False;DB_VertexOffset;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;97;-989.2574,3002.308;Float;False;MB_RotationAngle;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;1467;-2945.37,1620.284;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;1415;-732.3167,2870.109;Inherit;False;769;DB_VertexOffset;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1413;-743.3167,2775.109;Inherit;False;97;MB_RotationAngle;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1421;-732.3167,2677.109;Inherit;False;1420;MB_RotationAxis;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1463;-2958.53,1813.717;Inherit;False;1456;TextureMask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;295;-3078.015,1210.859;Inherit;True;Property;_MainTexture;Main Texture;0;1;[NoScaleOffset];Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;1462;-2632.77,1524.546;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;1490;-462.7808,2757.542;Inherit;False;MainBending - NHP;-1;;92;01dba1f3bc33e4b4fa301d2180819576;0;4;55;FLOAT3;0,0,0;False;53;FLOAT;0;False;59;FLOAT3;0,0,0;False;58;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;1265;642.2638,2687.529;Inherit;False;631.4954;512.0168;Comment;5;0;1061;296;1516;1517;Master Node;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;292;-2450.727,1516.703;Float;False;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1045;-31.7477,2756.445;Float;False;LocalVertexOffset;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;1517;705.0869,2935.281;Inherit;False;Property;_Smoothness;Smoothness;1;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1516;705.0869,2848.281;Inherit;False;Property;_Metallic;Metallic;0;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1061;733.1349,3028.227;Inherit;False;1045;LocalVertexOffset;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;296;793.774,2752.494;Inherit;False;292;Albedo;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;1330;-19002.03,10716.32;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;1328;-19355.95,10790.4;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1009.964,2756.547;Float;False;True;-1;2;LowPolyTreeWithPatterns_MaterialInspector;0;0;Standard;Nicrom/NHP/ASE/Vegetation Studio/Low Poly Tree With Patterns;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;5;Pragma;multi_compile_instancing;False;;Custom;Pragma;multi_compile GPU_FRUSTUM_ON __;False;;Custom;Pragma;instancing_options procedural:setup;False;;Custom;Pragma;instancing_options procedural:setup forwardadd;False;;Custom;Include;VS_indirect.cginc;False;;Custom;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;1498;0;1496;0
WireConnection;1498;1;1497;1
WireConnection;1515;17;989;0
WireConnection;887;0;749;0
WireConnection;1221;0;1220;0
WireConnection;756;0;1515;19
WireConnection;1403;0;1249;0
WireConnection;1384;0;1301;0
WireConnection;1219;0;1218;0
WireConnection;1499;0;1498;0
WireConnection;757;0;1515;0
WireConnection;928;0;1515;18
WireConnection;883;0;780;0
WireConnection;1388;0;792;0
WireConnection;1423;0;1288;0
WireConnection;1401;0;1308;0
WireConnection;1502;0;1501;1
WireConnection;1502;1;1501;3
WireConnection;1513;44;1399;0
WireConnection;1513;39;1400;0
WireConnection;1513;43;1405;0
WireConnection;1513;40;1406;0
WireConnection;1513;46;1408;0
WireConnection;1513;45;1409;0
WireConnection;1512;52;1380;0
WireConnection;1512;51;1381;0
WireConnection;1512;42;1382;0
WireConnection;1512;43;1383;0
WireConnection;1512;44;1391;0
WireConnection;1512;54;1392;0
WireConnection;1512;55;1393;0
WireConnection;1512;53;1394;0
WireConnection;1512;58;1395;0
WireConnection;1314;0;1512;0
WireConnection;1511;22;1502;0
WireConnection;1511;20;1504;0
WireConnection;1511;24;1518;0
WireConnection;1511;19;1506;0
WireConnection;1315;0;1513;0
WireConnection;1521;0;850;0
WireConnection;1521;1;1520;0
WireConnection;1521;2;1519;0
WireConnection;1483;0;1482;0
WireConnection;1344;0;1511;16
WireConnection;880;0;300;0
WireConnection;870;0;1521;0
WireConnection;1340;0;1511;0
WireConnection;1356;0;1262;0
WireConnection;877;0;687;0
WireConnection;873;0;480;0
WireConnection;1335;0;1334;0
WireConnection;1373;0;952;0
WireConnection;1360;0;1286;0
WireConnection;1214;1;1215;0
WireConnection;1214;0;1317;0
WireConnection;960;1;959;0
WireConnection;960;0;1316;0
WireConnection;1455;0;1457;2
WireConnection;1455;1;1454;0
WireConnection;1216;0;960;0
WireConnection;1216;1;1214;0
WireConnection;1459;2;1458;0
WireConnection;1488;36;1362;0
WireConnection;1488;35;1365;0
WireConnection;1488;34;1366;0
WireConnection;1488;28;1367;0
WireConnection;1488;47;1484;0
WireConnection;1488;29;1369;0
WireConnection;1488;42;1370;0
WireConnection;1488;27;1371;0
WireConnection;1486;20;1375;0
WireConnection;1486;19;1376;0
WireConnection;1486;18;1378;0
WireConnection;1460;0;1458;0
WireConnection;1460;1;1459;0
WireConnection;1456;0;1455;0
WireConnection;294;2;515;0
WireConnection;1420;0;1486;0
WireConnection;769;0;1216;0
WireConnection;97;0;1488;0
WireConnection;1467;0;1466;0
WireConnection;1467;1;1465;0
WireConnection;1467;2;1460;0
WireConnection;295;0;515;0
WireConnection;295;1;294;0
WireConnection;1462;0;295;0
WireConnection;1462;1;1467;0
WireConnection;1462;2;1463;0
WireConnection;1490;55;1421;0
WireConnection;1490;53;1413;0
WireConnection;1490;58;1415;0
WireConnection;292;0;1462;0
WireConnection;1045;0;1490;0
WireConnection;0;0;296;0
WireConnection;0;3;1516;0
WireConnection;0;4;1517;0
WireConnection;0;11;1061;0
ASEEND*/
//CHKSM=534F3111D0C2995BC902682262A03EB0565F59F5