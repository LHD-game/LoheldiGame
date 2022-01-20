// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Nicrom/NHP/ASE/Vegetation Studio/Low Poly Vegetation"
{
	Properties
	{
		[NoScaleOffset]_MainTex("Main Texture", 2D) = "white" {}
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_MBDefaultBending("MB Default Bending", Float) = 0
		_MBAmplitude("MB Amplitude", Float) = 1.5
		_MBAmplitudeOffset("MB Amplitude Offset", Float) = 2
		_MBFrequency("MB Frequency", Float) = 1.11
		_MBFrequencyOffset("MB Frequency Offset", Float) = 0
		_MBPhase("MB Phase", Float) = 1
		_MBWindDir("MB Wind Dir", Range( 0 , 360)) = 0
		_MBWindDirOffset("MB Wind Dir Offset", Range( 0 , 180)) = 20
		_MBWindBlend("MB Wind Blend", Range( 0 , 1)) = 0
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
		[NoScaleOffset]_NoiseTexture("Noise Texture", 2D) = "white" {}
		_NoiseTextureTilling("Noise Tilling - Static (XY), Animated (ZW)", Vector) = (1,1,1,1)
		_NoisePannerSpeed("Noise Panner Speed", Vector) = (0.05,0.03,0,0)
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
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows dithercrossfade vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _MBWindDir;
		uniform float MBGlobalWindDir;
		uniform float _MBWindBlend;
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
			float lerpResult1542 = lerp( _MBWindDir , MBGlobalWindDir , _MBWindBlend);
			float MB_WindDirection870 = lerpResult1542;
			float MB_WindDirectionOffset1373 = _MBWindDirOffset;
			float4 transform1507 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float2 appendResult1506 = (float2(transform1507.x , transform1507.z));
			float2 UVs27_g112 = appendResult1506;
			float4 temp_output_24_0_g112 = _NoiseTextureTilling;
			float2 AnimatedNoiseTilling29_g112 = (temp_output_24_0_g112).zw;
			float2 panner7_g112 = ( 0.1 * _Time.y * _NoisePannerSpeed + float2( 0,0 ));
			float4 AnimatedWorldNoise1344 = tex2Dlod( _NoiseTexture, float4( ( ( UVs27_g112 * AnimatedNoiseTilling29_g112 ) + panner7_g112 ), 0, 0.0) );
			float temp_output_11_0_g116 = radians( ( ( MB_WindDirection870 + ( MB_WindDirectionOffset1373 * (-1.0 + ((AnimatedWorldNoise1344).x - 0.0) * (1.0 - -1.0) / (1.0 - 0.0)) ) ) * -1.0 ) );
			float3 appendResult14_g116 = (float3(cos( temp_output_11_0_g116 ) , 0.0 , sin( temp_output_11_0_g116 )));
			float4 transform15_g116 = mul(unity_WorldToObject,float4( appendResult14_g116 , 0.0 ));
			float3 normalizeResult34_g116 = normalize( (transform15_g116).xyz );
			float3 MB_RotationAxis1420 = normalizeResult34_g116;
			float3 RotationAxis56_g118 = MB_RotationAxis1420;
			float MB_Amplitude880 = _MBAmplitude;
			float MB_AmplitudeOffset1356 = _MBAmplitudeOffset;
			float2 StaticNoileTilling28_g112 = (temp_output_24_0_g112).xy;
			float4 StaticWorldNoise1340 = tex2Dlod( _NoiseTexture, float4( ( UVs27_g112 * StaticNoileTilling28_g112 ), 0, 0.0) );
			float4 StaticWorldNoise31_g117 = StaticWorldNoise1340;
			float4 transform8_g117 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float MB_Frequency873 = _MBFrequency;
			float MB_FrequencyOffset1474 = _MBFrequencyOffset;
			float MB_Phase1360 = _MBPhase;
			float MB_DefaultBending877 = _MBDefaultBending;
			float3 ase_vertex3Pos = v.vertex.xyz;
			float MB_MaxHeight1335 = _MBMaxHeight;
			float MB_RotationAngle97 = radians( ( ( ( ( MB_Amplitude880 + ( MB_AmplitudeOffset1356 * (StaticWorldNoise31_g117).x ) ) * sin( ( ( ( transform8_g117.x + transform8_g117.z ) + ( ( _Time.y * ( MB_Frequency873 + ( MB_FrequencyOffset1474 * (StaticWorldNoise31_g117).x ) ) ) + ( ( 2.0 * UNITY_PI ) * 0.0 ) ) ) * MB_Phase1360 ) ) ) + MB_DefaultBending877 ) * ( ase_vertex3Pos.y / MB_MaxHeight1335 ) ) );
			float RotationAngle54_g118 = MB_RotationAngle97;
			float3 PivotPoint60_g118 = float3( 0,0,0 );
			float3 break62_g118 = PivotPoint60_g118;
			float3 appendResult45_g118 = (float3(break62_g118.x , ase_vertex3Pos.y , break62_g118.z));
			float3 rotatedValue30_g118 = RotateAroundAxis( appendResult45_g118, ase_vertex3Pos, RotationAxis56_g118, RotationAngle54_g118 );
			float temp_output_4_0_g82 = radians( ( v.color.b * 360.0 ) );
			float3 appendResult10_g82 = (float3(cos( temp_output_4_0_g82 ) , 0.0 , sin( temp_output_4_0_g82 )));
			float3 DB_RotationAxis757 = appendResult10_g82;
			float DB_VerticalAmplitude887 = _DBVerticalAmplitude;
			float DB_VerticalAmplitudeOffset1423 = _DBVerticalAmplitudeOffset;
			float DB_PhaseShift928 = v.color.a;
			float PhaseShift48_g97 = DB_PhaseShift928;
			float DB_VerticalFrequency883 = _DBVerticalFrequency;
			float Fequency45_g97 = DB_VerticalFrequency883;
			float4 transform2_g97 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float DB_VerticalPhase1384 = _DBVerticalPhase;
			float3 appendResult12_g82 = (float3(0.0 , ( _UnitScale * v.color.g ) , 0.0));
			float3 DB_PivotPosOnYAxis756 = appendResult12_g82;
			float3 PivotPosOnYAxis56_g97 = DB_PivotPosOnYAxis756;
			float DB_VerticalMaxLength1388 = _DBVerticalMaxLength;
			float3 rotatedValue29_g97 = RotateAroundAxis( PivotPosOnYAxis56_g97, ase_vertex3Pos, DB_RotationAxis757, radians( ( ( ( DB_VerticalAmplitude887 + ( DB_VerticalAmplitudeOffset1423 * ( 1.0 - PhaseShift48_g97 ) ) ) * sin( ( ( ( _Time.y * Fequency45_g97 ) - ( ( 2.0 * UNITY_PI ) * PhaseShift48_g97 ) ) + ( ( ( transform2_g97.x + transform2_g97.z ) + ( _Time.y * Fequency45_g97 ) ) * DB_VerticalPhase1384 ) ) ) ) * ( distance( ase_vertex3Pos , PivotPosOnYAxis56_g97 ) / DB_VerticalMaxLength1388 ) ) ) );
			float VerticalBendingMask1524 = step( 1.0 , v.texcoord.xy.y );
			float3 DB_VerticalMovement1314 = ( ( rotatedValue29_g97 - ase_vertex3Pos ) * VerticalBendingMask1524 );
			#ifdef _ENABLEVERTICALBENDING_ON
				float3 staticSwitch960 = DB_VerticalMovement1314;
			#else
				float3 staticSwitch960 = float3(0,0,0);
			#endif
			float DB_HorizontalAmplitude1221 = _DBHorizontalAmplitude;
			float DB_HorizontalFrequency1219 = _DBHorizontalFrequency;
			float Frequency41_g96 = DB_HorizontalFrequency1219;
			float4 transform5_g96 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float DB_HorizontalPhase1401 = _DBHorizontalPhase;
			float3 PivotPoint49_g96 = float3( 0,0,0 );
			float3 break52_g96 = PivotPoint49_g96;
			float3 appendResult20_g96 = (float3(break52_g96.x , ase_vertex3Pos.y , break52_g96.z));
			float DB_HorizontalMaxRadius1403 = _DBHorizontalMaxRadius;
			float3 rotatedValue33_g96 = RotateAroundAxis( PivotPoint49_g96, ase_vertex3Pos, float3(0,1,0), radians( ( ( DB_HorizontalAmplitude1221 * sin( ( ( ( _Time.y * Frequency41_g96 ) - ( ( 2.0 * UNITY_PI ) * ( 1.0 - DB_PhaseShift928 ) ) ) + ( ( ( transform5_g96.x + transform5_g96.z ) + ( _Time.y * Frequency41_g96 ) ) * DB_HorizontalPhase1401 ) ) ) ) * ( distance( ase_vertex3Pos , appendResult20_g96 ) / DB_HorizontalMaxRadius1403 ) ) ) );
			float HorizontalBendingMask1525 = step( 1.0 , v.texcoord.xy.x );
			float3 DB_SideToSideMovement1315 = ( ( rotatedValue33_g96 - ase_vertex3Pos ) * HorizontalBendingMask1525 );
			#ifdef _ENABLEHORIZONTALBENDING_ON
				float3 staticSwitch1214 = DB_SideToSideMovement1315;
			#else
				float3 staticSwitch1214 = float3(0,0,0);
			#endif
			float3 DB_VertexOffset769 = ( staticSwitch960 + staticSwitch1214 );
			float3 rotatedValue34_g118 = RotateAroundAxis( PivotPoint60_g118, ( rotatedValue30_g118 + DB_VertexOffset769 ), RotationAxis56_g118, RotationAngle54_g118 );
			float3 LocalVertexOffset1045 = ( ( rotatedValue34_g118 - ase_vertex3Pos ) * step( 0.01 , ase_vertex3Pos.y ) );
			v.vertex.xyz += LocalVertexOffset1045;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			float4 Albedo292 = tex2D( _MainTex, uv_MainTex );
			o.Albedo = Albedo292.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "LowPolyVegetation_MaterialInspector"
}
/*ASEBEGIN
Version=18500
2194.286;690.1429;1218;656;8314.45;-681.1864;6.806392;True;False
Node;AmplifyShaderEditor.CommentaryNode;490;-5375.093,3971.594;Inherit;False;885.7786;896.2201;;13;1524;1525;1523;1522;1520;1521;1519;1518;756;757;928;1517;989;Vertex Colors and UVs Baked Data;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;1519;-5162.308,4360.131;Inherit;False;Constant;_Float1;Float 1;26;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;989;-5327.216,4134.123;Float;False;Property;_UnitScale;Unit Scale;27;0;Create;True;0;0;False;0;False;20;20;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;1518;-5208.455,4443.091;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1520;-5152.352,4590.352;Inherit;False;Constant;_Float25;Float 25;26;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;1521;-5211.498,4674.312;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;866;-6022.001,2558.971;Inherit;False;1529.207;1150.107;;39;873;877;1373;880;1356;870;1335;1360;1474;952;687;1473;480;1542;1286;1262;300;1334;1538;1540;850;1221;883;1401;1423;1403;1219;887;1388;1384;780;1220;1218;749;792;1249;1308;1301;1288;Material Properties;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;1249;-5158.635,3432.756;Float;False;Property;_DBHorizontalMaxRadius;DB Horizontal Max Radius;23;0;Create;True;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;749;-5160.635,2633.756;Float;False;Property;_DBVerticalAmplitude;DB Vertical Amplitude;14;0;Create;True;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1301;-5159.635,2919.756;Float;False;Property;_DBVerticalPhase;DB Vertical Phase;17;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1288;-5160.635,2727.755;Float;False;Property;_DBVerticalAmplitudeOffset;DB Vertical Amplitude Offset;15;0;Create;True;0;0;False;0;False;1.2;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;1523;-4979.636,4645.517;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;792;-5157.635,3018.756;Float;False;Property;_DBVerticalMaxLength;DB Vertical Max Length;18;0;Create;True;0;0;False;0;False;2;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1218;-5152.635,3241.756;Float;False;Property;_DBHorizontalFrequency;DB Horizontal Frequency;21;0;Create;True;0;0;False;0;False;1.16;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1308;-5159.635,3336.756;Float;False;Property;_DBHorizontalPhase;DB Horizontal Phase;22;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;780;-5161.635,2825.756;Float;False;Property;_DBVerticalFrequency;DB Vertical Frequency;16;0;Create;True;0;0;False;0;False;1.15;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1517;-5146.711,4138.436;Inherit;False;VertexColorData - NHP;-1;;82;0242ce46c610b224e91bc03a7bf52b77;0;1;17;FLOAT;0;False;3;FLOAT3;19;FLOAT3;0;FLOAT;18
Node;AmplifyShaderEditor.RangedFloatNode;1220;-5158.635,3147.756;Float;False;Property;_DBHorizontalAmplitude;DB Horizontal Amplitude;20;0;Create;True;0;0;False;0;False;2;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;1522;-4975.592,4400.296;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;1343;-5889.719,1538.76;Inherit;False;1402.304;768.1017;;8;1340;1344;1533;1500;1528;1457;1506;1507;World Space Noise;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;1410;-4226.186,2434.631;Inherit;False;2302.06;1276.609;;27;960;1214;1215;959;1317;1316;769;1216;1315;1314;1395;1399;1405;1394;1409;1381;1400;1391;1383;1392;1382;1380;1408;1393;1406;1554;1553;Detail Bending;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1524;-4818.086,4639.133;Float;False;VerticalBendingMask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1219;-4826.635,3238.756;Float;False;DB_HorizontalFrequency;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;928;-4744.829,4238.628;Float;False;DB_PhaseShift;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1221;-4820.635,3144.756;Float;False;DB_HorizontalAmplitude;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;883;-4802.635,2824.756;Float;False;DB_VerticalFrequency;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1423;-4838.635,2727.755;Inherit;False;DB_VerticalAmplitudeOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;887;-4799.635,2632.756;Float;False;DB_VerticalAmplitude;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1388;-4807.635,3018.756;Inherit;False;DB_VerticalMaxLength;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1525;-4819.958,4393.913;Float;False;HorizontalBendingMask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;756;-4744.478,4062.82;Float;False;DB_PivotPosOnYAxis;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1384;-4780.635,2919.756;Inherit;False;DB_VerticalPhase;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1403;-4827.635,3431.756;Inherit;False;DB_HorizontalMaxRadius;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;757;-4744.001,4155.617;Float;False;DB_RotationAxis;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1401;-4795.635,3336.756;Inherit;False;DB_HorizontalPhase;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1382;-4146.896,2643.63;Inherit;False;883;DB_VerticalFrequency;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1395;-4136.298,3135.98;Inherit;False;1524;VerticalBendingMask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1393;-4110.923,2963.003;Inherit;False;757;DB_RotationAxis;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1408;-4160.782,3546.611;Inherit;False;1403;DB_HorizontalMaxRadius;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1380;-4146.896,2484.631;Inherit;False;887;DB_VerticalAmplitude;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1399;-4158.497,3233.976;Inherit;False;1221;DB_HorizontalAmplitude;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1383;-4119.896,2724.63;Inherit;False;1384;DB_VerticalPhase;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1406;-4092.955,3465.02;Inherit;False;928;DB_PhaseShift;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1392;-4147.013,2880.003;Inherit;False;1388;DB_VerticalMaxLength;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ObjectToWorldTransfNode;1507;-5805.668,1593.195;Inherit;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;1391;-4101.379,2803.246;Inherit;False;928;DB_PhaseShift;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1381;-4189.896,2562.63;Inherit;False;1423;DB_VerticalAmplitudeOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1394;-4136.153,3041.871;Inherit;False;756;DB_PivotPosOnYAxis;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1409;-4149.31,3629.079;Inherit;False;1525;HorizontalBendingMask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1405;-4129.955,3388.02;Inherit;False;1401;DB_HorizontalPhase;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1400;-4159.497,3308.976;Inherit;False;1219;DB_HorizontalFrequency;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;1506;-5599.335,1620.531;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;1540;-5972,3361;Inherit;False;Property;_MBWindBlend;MB Wind Blend;11;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;1457;-5684,2161;Float;False;Property;_NoisePannerSpeed;Noise Panner Speed;26;0;Create;True;0;0;False;0;False;0.05,0.03;0.08,0.1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TexturePropertyNode;1500;-5684.122,1782.174;Inherit;True;Property;_NoiseTexture;Noise Texture;24;1;[NoScaleOffset];Create;True;0;0;False;0;False;None;512fa11ad89d84543ad8d6c8d9cb6743;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.FunctionNode;1553;-3730.171,2712.333;Inherit;False;VerticalBending - NHP;-1;;97;41809ea7184502144ad776d88ecd1913;0;9;52;FLOAT;1;False;51;FLOAT;1;False;42;FLOAT;1;False;43;FLOAT;1;False;44;FLOAT;0;False;54;FLOAT;2;False;55;FLOAT3;0,0,0;False;53;FLOAT3;0,0,0;False;58;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector4Node;1528;-5757,1985;Inherit;False;Property;_NoiseTextureTilling;Noise Tilling - Static (XY), Animated (ZW);25;0;Create;False;0;0;False;0;False;1,1,1,1;1,1,1,1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;850;-5980.182,3205.127;Float;False;Property;_MBWindDir;MB Wind Dir;9;0;Create;True;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1554;-3727.852,3366.709;Inherit;False;HorizontalBending - NHP;-1;;96;0b16e2546645f904a949bfd32be36037;0;7;44;FLOAT;1;False;39;FLOAT;1;False;43;FLOAT;1;False;40;FLOAT;0;False;46;FLOAT;2;False;47;FLOAT3;0,0,0;False;45;FLOAT;1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;1538;-5977,3284;Inherit;False;Global;MBGlobalWindDir;MB Global Wind Dir;28;1;[HideInInspector];Create;True;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;480;-5976.1,2917.024;Float;False;Property;_MBFrequency;MB Frequency;6;0;Create;True;0;0;False;0;False;1.11;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1334;-5976.05,3563.617;Inherit;False;Property;_MBMaxHeight;MB Max Height;12;0;Create;True;0;0;False;0;False;10;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1286;-5973.801,3097.725;Float;False;Property;_MBPhase;MB Phase;8;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;300;-5976.1,2725.024;Float;False;Property;_MBAmplitude;MB Amplitude;4;0;Create;True;0;0;False;0;False;1.5;1.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1314;-3383.776,2707.486;Float;False;DB_VerticalMovement;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;1262;-5976.1,2820.024;Float;False;Property;_MBAmplitudeOffset;MB Amplitude Offset;5;0;Create;True;0;0;False;0;False;2;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;952;-5981.34,3464.812;Float;False;Property;_MBWindDirOffset;MB Wind Dir Offset;10;0;Create;True;0;0;False;0;False;20;0;0;180;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1315;-3336.727,3363.049;Float;False;DB_SideToSideMovement;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;687;-5977.1,2629.025;Float;False;Property;_MBDefaultBending;MB Default Bending;3;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1533;-5356.821,1870.278;Inherit;False;WorldSpaceNoise - NHP;-1;;112;af5fa9ff24e18344ebcc05b64d296c57;0;4;22;FLOAT2;0,0;False;20;SAMPLER2D;;False;24;FLOAT4;1,1,1,1;False;19;FLOAT2;0.1,0.1;False;2;COLOR;0;COLOR;16
Node;AmplifyShaderEditor.RangedFloatNode;1473;-5972.718,3008.635;Inherit;False;Property;_MBFrequencyOffset;MB Frequency Offset;7;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;1542;-5661.873,3267.649;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1474;-5498.824,3008.793;Inherit;False;MB_FrequencyOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1335;-5465.05,3556.621;Inherit;False;MB_MaxHeight;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;873;-5467.1,2915.024;Float;False;MB_Frequency;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;1215;-2956.568,3070.181;Float;False;Constant;_Vector2;Vector 2;27;0;Create;True;0;0;False;0;False;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.CommentaryNode;1068;-1662.38,2431.292;Inherit;False;2300.06;1277.088;;20;1045;1483;1421;1415;1413;1420;97;1551;1548;1378;1371;1366;1370;1376;1362;1369;1367;1476;1375;1365;Main Bending;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1356;-5499.1,2819.024;Inherit;False;MB_AmplitudeOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;877;-5499.1,2627.025;Float;False;MB_DefaultBending;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1344;-4835,1930;Inherit;False;AnimatedWorldNoise;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1360;-5456.801,3095.725;Inherit;False;MB_Phase;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1340;-4835,1835;Inherit;False;StaticWorldNoise;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;880;-5467.1,2723.024;Float;False;MB_Amplitude;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;870;-5483.182,3261.577;Float;False;MB_WindDirection;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;959;-2959.762,2811.052;Float;False;Constant;_Vector0;Vector 0;27;0;Create;True;0;0;False;0;False;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.GetLocalVarNode;1316;-3058.229,2966.865;Inherit;False;1314;DB_VerticalMovement;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1373;-5527.05,3461.621;Inherit;False;MB_WindDirectionOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1317;-3078.351,3225.079;Inherit;False;1315;DB_SideToSideMovement;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1370;-1516.55,3461.464;Inherit;False;1335;MB_MaxHeight;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1376;-1555.964,2654.996;Inherit;False;1373;MB_WindDirectionOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1476;-1558.893,3280.321;Inherit;False;1474;MB_FrequencyOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1369;-1491.621,3367.291;Inherit;False;1360;MB_Phase;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;1214;-2734.18,3146.163;Float;False;Property;_EnableHorizontalBending;Enable Horizontal Bending;19;0;Create;True;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1366;-1548.621,3085.291;Inherit;False;1356;MB_AmplitudeOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;960;-2726.889,2875.661;Float;False;Property;_EnableVerticalBending;Enable Vertical Bending;13;0;Create;True;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1371;-1534.021,3544.991;Inherit;False;1340;StaticWorldNoise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;1378;-1535.88,2748.395;Inherit;False;1344;AnimatedWorldNoise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;1362;-1544.621,2894.291;Inherit;False;877;MB_DefaultBending;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1367;-1513.621,3181.291;Inherit;False;873;MB_Frequency;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1375;-1518.463,2556.829;Inherit;False;870;MB_WindDirection;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1365;-1513.621,2989.291;Inherit;False;880;MB_Amplitude;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1216;-2341.533,3002.086;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;1548;-1147.652,2626.313;Inherit;False;RotationAxis - NHP;-1;;116;b90648f17dcc4bc449d46e8cf04564ff;0;3;20;FLOAT;0;False;19;FLOAT;0;False;18;FLOAT4;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;1551;-1158.469,3121.039;Inherit;False;RotationAngle - NHP;-1;;117;87b0b7c0fc8f1424db43b84d20c2e79b;0;9;36;FLOAT;0;False;35;FLOAT;0;False;34;FLOAT;1;False;28;FLOAT;1;False;47;FLOAT;0;False;29;FLOAT;1;False;46;FLOAT;0;False;42;FLOAT;0.1;False;27;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;489;-641.8001,1792.219;Inherit;False;1281.093;382.0935;;4;292;295;294;515;Albedo;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;769;-2176.385,2998.099;Float;False;DB_VertexOffset;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;97;-760.027,3116.68;Float;False;MB_RotationAngle;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1420;-774.467,2621.74;Inherit;False;MB_RotationAxis;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TexturePropertyNode;515;-541.8303,1884.928;Float;True;Property;_MainTex;Main Texture;0;1;[NoScaleOffset];Create;False;0;0;False;0;False;None;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.GetLocalVarNode;1415;-356.0867,3087.638;Inherit;False;769;DB_VertexOffset;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1421;-354.0867,2896.638;Inherit;False;1420;MB_RotationAxis;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1413;-363.0867,2991.638;Inherit;False;97;MB_RotationAngle;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;294;-265.1842,1967.297;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;1483;-75.98019,2972.456;Inherit;False;MainBending - NHP;-1;;118;01dba1f3bc33e4b4fa301d2180819576;0;4;55;FLOAT3;0,0,0;False;53;FLOAT;0;False;59;FLOAT3;0,0,0;False;58;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;295;23.91451,1886.047;Inherit;True;Property;_MainTexture;Main Texture;0;1;[NoScaleOffset];Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;1045;324.3009,2968.485;Float;False;LocalVertexOffset;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;1265;1027.889,2563.993;Inherit;False;634.495;508.0168;;5;0;1495;1061;1496;296;Master Node;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;292;366.9065,1887.084;Float;False;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;1495;1061.29,2686.068;Inherit;False;Property;_Metallic;Metallic;1;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;1330;-19002.03,10716.32;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;1328;-19355.95,10790.4;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1061;1097.76,2896.691;Inherit;False;1045;LocalVertexOffset;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;296;1158.399,2609.958;Inherit;False;292;Albedo;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;1496;1061.29,2773.068;Inherit;False;Property;_Smoothness;Smoothness;2;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1403.957,2624.366;Float;False;True;-1;2;LowPolyVegetation_MaterialInspector;0;0;Standard;Nicrom/NHP/ASE/Vegetation Studio/Low Poly Vegetation;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;5;Pragma;multi_compile_instancing;False;;Custom;Pragma;multi_compile GPU_FRUSTUM_ON __;False;;Custom;Pragma;instancing_options procedural:setup;False;;Custom;Pragma;instancing_options procedural:setup forwardadd;False;;Custom;Include;VS_indirect.cginc;False;;Custom;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;1523;0;1520;0
WireConnection;1523;1;1521;2
WireConnection;1517;17;989;0
WireConnection;1522;0;1519;0
WireConnection;1522;1;1518;1
WireConnection;1524;0;1523;0
WireConnection;1219;0;1218;0
WireConnection;928;0;1517;18
WireConnection;1221;0;1220;0
WireConnection;883;0;780;0
WireConnection;1423;0;1288;0
WireConnection;887;0;749;0
WireConnection;1388;0;792;0
WireConnection;1525;0;1522;0
WireConnection;756;0;1517;19
WireConnection;1384;0;1301;0
WireConnection;1403;0;1249;0
WireConnection;757;0;1517;0
WireConnection;1401;0;1308;0
WireConnection;1506;0;1507;1
WireConnection;1506;1;1507;3
WireConnection;1553;52;1380;0
WireConnection;1553;51;1381;0
WireConnection;1553;42;1382;0
WireConnection;1553;43;1383;0
WireConnection;1553;44;1391;0
WireConnection;1553;54;1392;0
WireConnection;1553;55;1393;0
WireConnection;1553;53;1394;0
WireConnection;1553;58;1395;0
WireConnection;1554;44;1399;0
WireConnection;1554;39;1400;0
WireConnection;1554;43;1405;0
WireConnection;1554;40;1406;0
WireConnection;1554;46;1408;0
WireConnection;1554;45;1409;0
WireConnection;1314;0;1553;0
WireConnection;1315;0;1554;0
WireConnection;1533;22;1506;0
WireConnection;1533;20;1500;0
WireConnection;1533;24;1528;0
WireConnection;1533;19;1457;0
WireConnection;1542;0;850;0
WireConnection;1542;1;1538;0
WireConnection;1542;2;1540;0
WireConnection;1474;0;1473;0
WireConnection;1335;0;1334;0
WireConnection;873;0;480;0
WireConnection;1356;0;1262;0
WireConnection;877;0;687;0
WireConnection;1344;0;1533;16
WireConnection;1360;0;1286;0
WireConnection;1340;0;1533;0
WireConnection;880;0;300;0
WireConnection;870;0;1542;0
WireConnection;1373;0;952;0
WireConnection;1214;1;1215;0
WireConnection;1214;0;1317;0
WireConnection;960;1;959;0
WireConnection;960;0;1316;0
WireConnection;1216;0;960;0
WireConnection;1216;1;1214;0
WireConnection;1548;20;1375;0
WireConnection;1548;19;1376;0
WireConnection;1548;18;1378;0
WireConnection;1551;36;1362;0
WireConnection;1551;35;1365;0
WireConnection;1551;34;1366;0
WireConnection;1551;28;1367;0
WireConnection;1551;47;1476;0
WireConnection;1551;29;1369;0
WireConnection;1551;42;1370;0
WireConnection;1551;27;1371;0
WireConnection;769;0;1216;0
WireConnection;97;0;1551;0
WireConnection;1420;0;1548;0
WireConnection;294;2;515;0
WireConnection;1483;55;1421;0
WireConnection;1483;53;1413;0
WireConnection;1483;58;1415;0
WireConnection;295;0;515;0
WireConnection;295;1;294;0
WireConnection;1045;0;1483;0
WireConnection;292;0;295;0
WireConnection;0;0;296;0
WireConnection;0;3;1495;0
WireConnection;0;4;1496;0
WireConnection;0;11;1061;0
ASEEND*/
//CHKSM=90A4A40489A575DD674E90FDA3AB1C2EE4D6A693