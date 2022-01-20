// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Nicrom/NHP/ASE/Vegetation Studio/Low Poly Reedmace"
{
	Properties
	{
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		[NoScaleOffset][Header(Textures)]_MainTex("Main Texture", 2D) = "white" {}
		[NoScaleOffset]_NoiseTexture("Noise Texture", 2D) = "white" {}
		_NoiseTextureTilling("Noise Tilling - Static (XY), Animated (ZW)", Vector) = (1,1,1,1)
		_NoisePannerSpeed("Noise Panner Speed", Vector) = (0.05,0.03,0,0)
		_MBDefaultBending("MB Default Bending", Float) = 0
		_MBAmplitude("MB Amplitude", Float) = 1.5
		_MBAmplitudeOffset("MB Amplitude Offset", Float) = 2
		_MBFrequency("MB Frequency", Float) = 1.11
		_MBFrequencyOffset("MB Frequency Offset", Float) = 0
		_MBPhase("MB Phase", Float) = 1
		_MBWindDir("MB Wind Dir", Range( 0 , 360)) = 0
		_MBWindDirOffset("MB Wind Dir Offset", Range( 0 , 180)) = 20
		_MBWindDirBlend("MB Wind Dir Blend", Range( 0 , 1)) = 0
		_MBMaxHeight("MB Max Height", Float) = 10
		[Toggle(_ENABLESLOPECORRECTION_ON)] _EnableSlopeCorrection("Enable Slope Correction", Float) = 1
		_SlopeCorrectionMagnitude("Slope Correction Magnitude", Range( 0 , 1)) = 1
		_SlopeCorrectionOffset("Slope Correction Offset", Range( 0 , 1)) = 0
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
		#pragma shader_feature_local _ENABLESLOPECORRECTION_ON
		#pragma multi_compile_instancing
		#pragma multi_compile GPU_FRUSTUM_ON __
		#pragma instancing_options procedural:setup
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
		uniform float _SlopeCorrectionOffset;
		uniform float _SlopeCorrectionMagnitude;
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
			float lerpResult1541 = lerp( _MBWindDir , MBGlobalWindDir , _MBWindDirBlend);
			float MB_WindDirection870 = lerpResult1541;
			float MB_WindDirectionVariation1373 = _MBWindDirOffset;
			float4 transform1_g110 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float3 appendResult1525 = (float3(v.texcoord1.xy.x , 0.0 , v.texcoord1.xy.y));
			float3 MB_LocalPivot1526 = -appendResult1525;
			float4 transform2_g110 = mul(unity_ObjectToWorld,float4( MB_LocalPivot1526 , 0.0 ));
			float2 UVs27_g93 = ( (transform1_g110).xz + (transform2_g110).xz );
			float4 temp_output_24_0_g93 = _NoiseTextureTilling;
			float2 AnimatedNoiseTilling29_g93 = (temp_output_24_0_g93).zw;
			float2 panner7_g93 = ( 0.1 * _Time.y * _NoisePannerSpeed + float2( 0,0 ));
			float4 AnimatedNoise1344 = tex2Dlod( _NoiseTexture, float4( ( ( UVs27_g93 * AnimatedNoiseTilling29_g93 ) + panner7_g93 ), 0, 0.0) );
			float temp_output_11_0_g95 = radians( ( ( MB_WindDirection870 + ( MB_WindDirectionVariation1373 * (-1.0 + ((AnimatedNoise1344).x - 0.0) * (1.0 - -1.0) / (1.0 - 0.0)) ) ) * -1.0 ) );
			float3 appendResult14_g95 = (float3(cos( temp_output_11_0_g95 ) , 0.0 , sin( temp_output_11_0_g95 )));
			float4 transform15_g95 = mul(unity_WorldToObject,float4( appendResult14_g95 , 0.0 ));
			float3 normalizeResult34_g95 = normalize( (transform15_g95).xyz );
			float3 MB_RotationAxis1420 = normalizeResult34_g95;
			float3 RotationAxis56_g96 = MB_RotationAxis1420;
			float MB_Amplitude880 = _MBAmplitude;
			float MB_AmplitudeOffset1356 = _MBAmplitudeOffset;
			float2 StaticNoileTilling28_g93 = (temp_output_24_0_g93).xy;
			float4 StaticNoise1340 = tex2Dlod( _NoiseTexture, float4( ( UVs27_g93 * StaticNoileTilling28_g93 ), 0, 0.0) );
			float4 StaticWorldNoise31_g94 = StaticNoise1340;
			float4 transform8_g94 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float MB_Frequency873 = _MBFrequency;
			float MB_FrequencyOffset1474 = _MBFrequencyOffset;
			float PhaseShift928 = v.color.a;
			float MB_Phase1360 = _MBPhase;
			float MB_DefaultBending877 = _MBDefaultBending;
			float3 ase_vertex3Pos = v.vertex.xyz;
			float MB_MaxHeight1335 = _MBMaxHeight;
			float MB_RotationAngle97 = radians( ( ( ( ( MB_Amplitude880 + ( MB_AmplitudeOffset1356 * (StaticWorldNoise31_g94).x ) ) * sin( ( ( ( transform8_g94.x + transform8_g94.z ) + ( ( _Time.y * ( MB_Frequency873 + ( MB_FrequencyOffset1474 * (StaticWorldNoise31_g94).x ) ) ) + ( ( 2.0 * UNITY_PI ) * PhaseShift928 ) ) ) * MB_Phase1360 ) ) ) + MB_DefaultBending877 ) * ( ase_vertex3Pos.y / MB_MaxHeight1335 ) ) );
			float RotationAngle54_g96 = MB_RotationAngle97;
			float3 PivotPoint60_g96 = MB_LocalPivot1526;
			float3 break62_g96 = PivotPoint60_g96;
			float3 appendResult45_g96 = (float3(break62_g96.x , ase_vertex3Pos.y , break62_g96.z));
			float3 rotatedValue30_g96 = RotateAroundAxis( appendResult45_g96, ase_vertex3Pos, RotationAxis56_g96, RotationAngle54_g96 );
			float3 rotatedValue34_g96 = RotateAroundAxis( PivotPoint60_g96, ( rotatedValue30_g96 + float3( 0,0,0 ) ), RotationAxis56_g96, RotationAngle54_g96 );
			float3 temp_output_1483_0 = ( ( rotatedValue34_g96 - ase_vertex3Pos ) * step( 0.01 , ase_vertex3Pos.y ) );
			float3 MainBending89_g109 = temp_output_1483_0;
			float3 appendResult15_g109 = (float3(0.0 , 1.0 , 0.0));
			float4 transform17_g109 = mul(unity_ObjectToWorld,float4( appendResult15_g109 , 0.0 ));
			float4 break20_g109 = transform17_g109;
			float3 appendResult24_g109 = (float3(-break20_g109.z , 0.0 , break20_g109.x));
			float3 appendResult3_g109 = (float3(0.0 , 1.0 , 0.0));
			float4 transform4_g109 = mul(unity_ObjectToWorld,float4( appendResult3_g109 , 0.0 ));
			float3 lerpResult84_g109 = lerp( float3(0,1,0) , (transform4_g109).xyz , step( 1E-06 , ( abs( transform4_g109.x ) + abs( transform4_g109.z ) ) ));
			float3 normalizeResult7_g109 = normalize( lerpResult84_g109 );
			float dotResult9_g109 = dot( normalizeResult7_g109 , float3(0,1,0) );
			float temp_output_12_0_g109 = acos( dotResult9_g109 );
			float NaNPrevention21_g109 = step( 0.01 , abs( ( temp_output_12_0_g109 * ( 180.0 / UNITY_PI ) ) ) );
			float3 lerpResult26_g109 = lerp( float3(1,0,0) , appendResult24_g109 , NaNPrevention21_g109);
			float4 transform28_g109 = mul(unity_WorldToObject,float4( lerpResult26_g109 , 0.0 ));
			float3 normalizeResult49_g109 = normalize( (transform28_g109).xyz );
			float3 RotationAxis30_g109 = normalizeResult49_g109;
			float SlopeCorrectionOffset1543 = _SlopeCorrectionOffset;
			float SlopeCorrectionMagnitude1531 = _SlopeCorrectionMagnitude;
			float RotationAngle29_g109 = ( saturate( ( (0.0 + ((StaticNoise1340).x - 0.0) * (SlopeCorrectionOffset1543 - 0.0) / (1.0 - 0.0)) + SlopeCorrectionMagnitude1531 ) ) * temp_output_12_0_g109 );
			float3 rotatedValue35_g109 = RotateAroundAxis( MB_LocalPivot1526, ( ase_vertex3Pos + MainBending89_g109 ), RotationAxis30_g109, RotationAngle29_g109 );
			float3 lerpResult52_g109 = lerp( MainBending89_g109 , ( rotatedValue35_g109 - ase_vertex3Pos ) , NaNPrevention21_g109);
			#ifdef _ENABLESLOPECORRECTION_ON
				float3 staticSwitch1535 = lerpResult52_g109;
			#else
				float3 staticSwitch1535 = temp_output_1483_0;
			#endif
			float3 LocalVertexOffset1045 = staticSwitch1535;
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
	CustomEditor "LowPolyReedmace_MaterialInspector"
}
/*ASEBEGIN
Version=18500
2194.286;690.1429;1156;656;5969.341;-1416.542;5.666683;True;False
Node;AmplifyShaderEditor.CommentaryNode;490;-2940.889,3967.001;Inherit;False;889.4651;381.8646;;6;1526;1525;1524;928;1521;1548;Vertex Colors and UVs Baked Data;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1524;-2882.209,4194.662;Inherit;False;1;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;1525;-2625.882,4202.125;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NegateNode;1548;-2434.777,4201.029;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1526;-2254.41,4197.125;Inherit;False;MB_LocalPivot;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;1506;-3580.203,2944.273;Inherit;False;1398.904;766.9824;;8;1546;1340;1344;1519;1510;1538;1537;1547;World Space Noise;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;866;-4728.83,2369.795;Inherit;False;888.7255;1337.645;;25;850;1543;1542;1531;1530;1335;952;1373;1334;1474;880;1360;877;873;1356;870;1541;1540;1539;687;300;1262;480;1473;1286;Material Properties;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;1547;-3537.856,3080.465;Inherit;False;1526;MB_LocalPivot;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;850;-4678.628,3021.549;Float;False;Property;_MBWindDir;MB Wind Dir;12;0;Create;True;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1539;-4679.813,3114.611;Inherit;False;Global;MBGlobalWindDir;MB Global Wind Dir;28;1;[HideInInspector];Create;True;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;1510;-3259.404,3162.387;Inherit;True;Property;_NoiseTexture;Noise Texture;3;1;[NoScaleOffset];Create;True;0;0;False;0;False;None;512fa11ad89d84543ad8d6c8d9cb6743;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.FunctionNode;1546;-3285.447,3084.549;Inherit;False;WorldSpaceUVs - NHP;-1;;110;88a2e8a391a04e241878bdb87d9283a3;0;1;6;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;1537;-3265.792,3548.592;Float;False;Property;_NoisePannerSpeed;Noise Panner Speed;5;0;Create;True;0;0;False;0;False;0.05,0.03;0.08,0.1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector4Node;1538;-3329.792,3370.591;Inherit;False;Property;_NoiseTextureTilling;Noise Tilling - Static (XY), Animated (ZW);4;0;Create;False;0;0;False;0;False;1,1,1,1;1,1,1,1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1540;-4675.813,3199.611;Inherit;False;Property;_MBWindDirBlend;MB Wind Dir Blend;14;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;300;-4682.929,2535.848;Float;False;Property;_MBAmplitude;MB Amplitude;7;0;Create;True;0;0;False;0;False;1.5;1.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1519;-2941.975,3278.449;Inherit;False;WorldSpaceNoise - NHP;-1;;93;af5fa9ff24e18344ebcc05b64d296c57;0;4;22;FLOAT2;0,0;False;20;SAMPLER2D;;False;24;FLOAT4;1,1,1,1;False;19;FLOAT2;0.1,0.1;False;2;COLOR;0;COLOR;16
Node;AmplifyShaderEditor.RangedFloatNode;952;-4668.996,3310.735;Float;False;Property;_MBWindDirOffset;MB Wind Dir Offset;13;0;Create;True;0;0;False;0;False;20;0;0;180;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;480;-4682.929,2727.848;Float;False;Property;_MBFrequency;MB Frequency;9;0;Create;True;0;0;False;0;False;1.11;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1473;-4680.551,2820.459;Inherit;False;Property;_MBFrequencyOffset;MB Frequency Offset;10;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;687;-4682.929,2439.849;Float;False;Property;_MBDefaultBending;MB Default Bending;6;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1262;-4682.929,2631.848;Float;False;Property;_MBAmplitudeOffset;MB Amplitude Offset;8;0;Create;True;0;0;False;0;False;2;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1334;-4668.174,3410.694;Inherit;False;Property;_MBMaxHeight;MB Max Height;15;0;Create;True;0;0;False;0;False;10;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1521;-2885.729,4024.547;Inherit;False;VertexColorData - NHP;-1;;92;0242ce46c610b224e91bc03a7bf52b77;0;1;17;FLOAT;0;False;3;FLOAT3;19;FLOAT3;0;FLOAT;18
Node;AmplifyShaderEditor.LerpOp;1541;-4368.813,3093.611;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1286;-4678.628,2908.549;Float;False;Property;_MBPhase;MB Phase;11;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1340;-2430.839,3239.801;Inherit;False;StaticNoise;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;873;-4108.828,2727.748;Float;False;MB_Frequency;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1360;-4104.528,2908.449;Inherit;False;MB_Phase;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;870;-4122.627,3086.999;Float;False;MB_WindDirection;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1356;-4140.828,2631.748;Inherit;False;MB_AmplitudeOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1344;-2428.892,3355.123;Inherit;False;AnimatedNoise;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1335;-4100.174,3410.694;Inherit;False;MB_MaxHeight;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;928;-2430.785,4065.849;Float;False;PhaseShift;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;877;-4140.828,2439.749;Float;False;MB_DefaultBending;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1373;-4179.995,3310.735;Inherit;False;MB_WindDirectionVariation;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;880;-4108.828,2535.748;Float;False;MB_Amplitude;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;1068;-1918.705,2558.135;Inherit;False;3322.38;1157.592;;27;1528;1371;97;1516;1365;1476;1362;1369;1366;1370;1367;1378;1376;1420;1505;1375;1045;1535;1536;1533;1545;1544;1532;1483;1413;1527;1421;Main Bending;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1474;-4142.552,2820.517;Inherit;False;MB_FrequencyOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1362;-1833.946,2930.135;Inherit;False;877;MB_DefaultBending;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1378;-1808.205,2791.239;Inherit;False;1344;AnimatedNoise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;1375;-1825.788,2627.673;Inherit;False;870;MB_WindDirection;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1476;-1836.218,3288.165;Inherit;False;1474;MB_FrequencyOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1528;-1787.101,3463.718;Inherit;False;928;PhaseShift;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1366;-1838.946,3108.135;Inherit;False;1356;MB_AmplitudeOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1367;-1801.946,3197.135;Inherit;False;873;MB_Frequency;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1376;-1883.289,2710.84;Inherit;False;1373;MB_WindDirectionVariation;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1371;-1784.946,3630.135;Inherit;False;1340;StaticNoise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;1369;-1781.946,3376.135;Inherit;False;1360;MB_Phase;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1370;-1811.875,3545.308;Inherit;False;1335;MB_MaxHeight;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1365;-1801.946,3015.135;Inherit;False;880;MB_Amplitude;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1505;-1437.977,2695.156;Inherit;False;RotationAxis - NHP;-1;;95;b90648f17dcc4bc449d46e8cf04564ff;0;3;20;FLOAT;0;False;19;FLOAT;0;False;18;FLOAT4;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;1516;-1449.794,3168.883;Inherit;False;RotationAngle - NHP;-1;;94;87b0b7c0fc8f1424db43b84d20c2e79b;0;9;36;FLOAT;0;False;35;FLOAT;0;False;34;FLOAT;1;False;28;FLOAT;1;False;47;FLOAT;0;False;29;FLOAT;1;False;46;FLOAT;0;False;42;FLOAT;0;False;27;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1542;-4666.67,3619.679;Inherit;False;Property;_SlopeCorrectionOffset;Slope Correction Offset;18;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1530;-4665.996,3523.735;Inherit;False;Property;_SlopeCorrectionMagnitude;Slope Correction Magnitude;17;0;Create;True;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1420;-969.7913,2692.583;Inherit;False;MB_RotationAxis;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;97;-992.3517,3162.523;Float;False;MB_RotationAngle;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;489;126.2718,1923.215;Inherit;False;1281.093;382.0935;;4;292;295;294;515;Albedo;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1531;-4169.995,3523.735;Inherit;False;SlopeCorrectionMagnitude;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1543;-4143.822,3620.688;Inherit;False;SlopeCorrectionOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1421;-674.1556,2872.381;Inherit;False;1420;MB_RotationAxis;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1413;-687.1556,2961.381;Inherit;False;97;MB_RotationAngle;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1527;-664.6092,3049.037;Inherit;False;1526;MB_LocalPivot;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1544;-126.0897,3283.073;Inherit;False;1543;SlopeCorrectionOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1533;-76.4331,3438.16;Inherit;False;1526;MB_LocalPivot;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TexturePropertyNode;515;215.8411,2031.524;Float;True;Property;_MainTex;Main Texture;2;1;[NoScaleOffset];Create;False;0;0;False;1;Header(Textures);False;None;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.GetLocalVarNode;1532;-157.9282,3200.383;Inherit;False;1531;SlopeCorrectionMagnitude;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1483;-391.049,2947.815;Inherit;False;MainBending - NHP;-1;;96;01dba1f3bc33e4b4fa301d2180819576;0;4;55;FLOAT3;0,0,0;False;53;FLOAT;0;False;59;FLOAT3;0,0,0;False;58;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1545;-60.08958,3363.073;Inherit;False;1340;StaticNoise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;294;492.4873,2113.894;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;1536;202.8029,3241.291;Inherit;False;SlopeCorrection - NHP;-1;;109;af38de3ca0adf3c4ba9b6a3dd482959e;0;5;87;FLOAT3;0,0,0;False;42;FLOAT;1;False;92;FLOAT;0;False;93;FLOAT4;0,0,0,0;False;41;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;295;781.5875,2032.644;Inherit;True;Property;_MainTexture;Main Texture;0;1;[NoScaleOffset];Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;1535;702.172,2947.431;Float;False;Property;_EnableSlopeCorrection;Enable Slope Correction;16;0;Create;True;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;292;1124.579,2033.68;Float;False;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1045;1024.282,2946.147;Float;False;LocalVertexOffset;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;1265;1798.659,2820.852;Inherit;False;631.4954;627.4399;;5;296;1522;1523;1061;0;Master Node;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;296;1913.94,2888.442;Inherit;False;292;Albedo;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;1522;1828.823,2988.11;Inherit;False;Property;_Metallic;Metallic;0;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;1330;-19002.03,10716.32;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1523;1828.823,3075.11;Inherit;False;Property;_Smoothness;Smoothness;1;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;1328;-19355.95,10790.4;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1061;1852.53,3260.43;Inherit;False;1045;LocalVertexOffset;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2172.131,2953.353;Float;False;True;-1;2;LowPolyReedmace_MaterialInspector;0;0;Standard;Nicrom/NHP/ASE/Vegetation Studio/Low Poly Reedmace;False;False;False;False;False;False;True;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;5;Pragma;multi_compile_instancing;False;;Custom;Pragma;multi_compile GPU_FRUSTUM_ON __;False;;Custom;Pragma;instancing_options procedural:setup;False;;Custom;Pragma;instancing_options procedural:setup forwardadd;False;;Custom;Include;VS_indirect.cginc;False;;Custom;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;1525;0;1524;1
WireConnection;1525;2;1524;2
WireConnection;1548;0;1525;0
WireConnection;1526;0;1548;0
WireConnection;1546;6;1547;0
WireConnection;1519;22;1546;0
WireConnection;1519;20;1510;0
WireConnection;1519;24;1538;0
WireConnection;1519;19;1537;0
WireConnection;1541;0;850;0
WireConnection;1541;1;1539;0
WireConnection;1541;2;1540;0
WireConnection;1340;0;1519;0
WireConnection;873;0;480;0
WireConnection;1360;0;1286;0
WireConnection;870;0;1541;0
WireConnection;1356;0;1262;0
WireConnection;1344;0;1519;16
WireConnection;1335;0;1334;0
WireConnection;928;0;1521;18
WireConnection;877;0;687;0
WireConnection;1373;0;952;0
WireConnection;880;0;300;0
WireConnection;1474;0;1473;0
WireConnection;1505;20;1375;0
WireConnection;1505;19;1376;0
WireConnection;1505;18;1378;0
WireConnection;1516;36;1362;0
WireConnection;1516;35;1365;0
WireConnection;1516;34;1366;0
WireConnection;1516;28;1367;0
WireConnection;1516;47;1476;0
WireConnection;1516;29;1369;0
WireConnection;1516;46;1528;0
WireConnection;1516;42;1370;0
WireConnection;1516;27;1371;0
WireConnection;1420;0;1505;0
WireConnection;97;0;1516;0
WireConnection;1531;0;1530;0
WireConnection;1543;0;1542;0
WireConnection;1483;55;1421;0
WireConnection;1483;53;1413;0
WireConnection;1483;59;1527;0
WireConnection;294;2;515;0
WireConnection;1536;87;1483;0
WireConnection;1536;42;1532;0
WireConnection;1536;92;1544;0
WireConnection;1536;93;1545;0
WireConnection;1536;41;1533;0
WireConnection;295;0;515;0
WireConnection;295;1;294;0
WireConnection;1535;1;1483;0
WireConnection;1535;0;1536;0
WireConnection;292;0;295;0
WireConnection;1045;0;1535;0
WireConnection;0;0;296;0
WireConnection;0;3;1522;0
WireConnection;0;4;1523;0
WireConnection;0;11;1061;0
ASEEND*/
//CHKSM=3DBC169FC4FB5B7165903967F7A0974CCD8D9030