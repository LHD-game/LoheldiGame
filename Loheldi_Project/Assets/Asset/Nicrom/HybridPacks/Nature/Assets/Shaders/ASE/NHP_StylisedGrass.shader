// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Nicrom/NHP/ASE/Stylised Grass"
{
	Properties
	{
		[NoScaleOffset]_MainTex("Main Tex", 2D) = "white" {}
		_Color1("Color 1", Color) = (0.3393185,0.490566,0.09255961,1)
		_Color2("Color 2", Color) = (0.719914,0.8207547,0.3639195,1)
		_ColorBlendStart("Color Blend Start", Range( 0 , 1)) = 0
		_ColorBlendEnd("Color Blend End", Range( 0 , 1)) = 1
		_AlphaCutoff("Alpha Cutoff", Range( 0 , 1)) = 0.5
		_MBDefaultBending("MB Default Bending", Float) = 0
		_MBAmplitude("MB Amplitude", Float) = 1.5
		_MBAmplitudeOffset("MB Amplitude Offset", Float) = 2
		_MBFrequency("MB Frequency", Float) = 1.11
		_MBFrequencyOffset("MB Frequency Offset", Float) = 0
		_MBWindDir("MB Wind Dir", Range( 0 , 360)) = 0
		_MBWindDirOffset("MB Wind Dir Offset", Range( 0 , 180)) = 20
		_MBWindDirBlend("MB Wind Dir Blend", Range( 0 , 1)) = 0
		_MBPhase("MB Phase", Float) = 3
		_MBMaxHeight("MB Max Height", Float) = 1
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
		uniform float _SlopeCorrectionOffset;
		uniform float _SlopeCorrectionMagnitude;
		uniform sampler2D _MainTex;
		SamplerState sampler_MainTex;
		uniform float4 _Color2;
		uniform float4 _Color1;
		uniform float _ColorBlendStart;
		uniform float _ColorBlendEnd;
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
			float lerpResult1653 = lerp( _MBWindDir , MBGlobalWindDir , _MBWindDirBlend);
			float MB_WindDirection870 = lerpResult1653;
			float MB_WindDirectionOffset1373 = _MBWindDirOffset;
			float4 transform1_g71 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float3 appendResult1503 = (float3(v.texcoord1.xy.x , 0.0 , v.texcoord1.xy.y));
			float3 Pivot1504 = -appendResult1503;
			float4 transform2_g71 = mul(unity_ObjectToWorld,float4( Pivot1504 , 0.0 ));
			float2 UVs27_g103 = ( (transform1_g71).xz + (transform2_g71).xz );
			float4 temp_output_24_0_g103 = _NoiseTextureTilling;
			float2 AnimatedNoiseTilling29_g103 = (temp_output_24_0_g103).zw;
			float2 panner7_g103 = ( 0.1 * _Time.y * _NoisePannerSpeed + float2( 0,0 ));
			float4 AnimatedWorldSpaceNoise1344 = tex2Dlod( _NoiseTexture, float4( ( ( UVs27_g103 * AnimatedNoiseTilling29_g103 ) + panner7_g103 ), 0, 0.0) );
			float temp_output_11_0_g121 = radians( ( ( MB_WindDirection870 + ( MB_WindDirectionOffset1373 * (-1.0 + ((AnimatedWorldSpaceNoise1344).x - 0.0) * (1.0 - -1.0) / (1.0 - 0.0)) ) ) * -1.0 ) );
			float3 appendResult14_g121 = (float3(cos( temp_output_11_0_g121 ) , 0.0 , sin( temp_output_11_0_g121 )));
			float4 transform15_g121 = mul(unity_WorldToObject,float4( appendResult14_g121 , 0.0 ));
			float3 normalizeResult34_g121 = normalize( (transform15_g121).xyz );
			float3 MB_RotationAxis1420 = normalizeResult34_g121;
			float MB_Amplitude880 = _MBAmplitude;
			float MB_AmplitudeOffset1356 = _MBAmplitudeOffset;
			float2 StaticNoileTilling28_g103 = (temp_output_24_0_g103).xy;
			float4 StaticWorldSpaceNoise1340 = tex2Dlod( _NoiseTexture, float4( ( UVs27_g103 * StaticNoileTilling28_g103 ), 0, 0.0) );
			float4 StaticWorldNoise31_g122 = StaticWorldSpaceNoise1340;
			float4 transform8_g122 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float MB_Frequency873 = _MBFrequency;
			float MB_FrequencyOffset1525 = _MBFrequencyOffset;
			float DB_PhaseShift928 = v.color.a;
			float MB_Phase1360 = _MBPhase;
			float MB_DefaultBending877 = _MBDefaultBending;
			float3 ase_vertex3Pos = v.vertex.xyz;
			float MB_MaxHeight1335 = _MBMaxHeight;
			float MB_RotationAngle97 = radians( ( ( ( ( MB_Amplitude880 + ( MB_AmplitudeOffset1356 * (StaticWorldNoise31_g122).x ) ) * sin( ( ( ( transform8_g122.x + transform8_g122.z ) + ( ( _Time.y * ( MB_Frequency873 + ( MB_FrequencyOffset1525 * (StaticWorldNoise31_g122).x ) ) ) + ( ( 2.0 * UNITY_PI ) * DB_PhaseShift928 ) ) ) * MB_Phase1360 ) ) ) + MB_DefaultBending877 ) * ( ase_vertex3Pos.y / MB_MaxHeight1335 ) ) );
			float3 appendResult1539 = (float3(ase_vertex3Pos.x , 0.0 , ase_vertex3Pos.z));
			float3 rotatedValue1536 = RotateAroundAxis( appendResult1539, ase_vertex3Pos, MB_RotationAxis1420, MB_RotationAngle97 );
			float3 temp_output_1545_0 = ( ( rotatedValue1536 - ase_vertex3Pos ) * step( 0.01 , ase_vertex3Pos.y ) );
			float3 MainBending89_g172 = temp_output_1545_0;
			float3 appendResult15_g172 = (float3(0.0 , 1.0 , 0.0));
			float4 transform17_g172 = mul(unity_ObjectToWorld,float4( appendResult15_g172 , 0.0 ));
			float4 break20_g172 = transform17_g172;
			float3 appendResult24_g172 = (float3(-break20_g172.z , 0.0 , break20_g172.x));
			float3 appendResult3_g172 = (float3(0.0 , 1.0 , 0.0));
			float4 transform4_g172 = mul(unity_ObjectToWorld,float4( appendResult3_g172 , 0.0 ));
			float3 lerpResult84_g172 = lerp( float3(0,1,0) , (transform4_g172).xyz , step( 1E-06 , ( abs( transform4_g172.x ) + abs( transform4_g172.z ) ) ));
			float3 normalizeResult7_g172 = normalize( lerpResult84_g172 );
			float dotResult9_g172 = dot( normalizeResult7_g172 , float3(0,1,0) );
			float temp_output_12_0_g172 = acos( dotResult9_g172 );
			float NaNPrevention21_g172 = step( 0.01 , abs( ( temp_output_12_0_g172 * ( 180.0 / UNITY_PI ) ) ) );
			float3 lerpResult26_g172 = lerp( float3(1,0,0) , appendResult24_g172 , NaNPrevention21_g172);
			float4 transform28_g172 = mul(unity_WorldToObject,float4( lerpResult26_g172 , 0.0 ));
			float3 normalizeResult49_g172 = normalize( (transform28_g172).xyz );
			float3 RotationAxis30_g172 = normalizeResult49_g172;
			float SlopeCorrectionOffset1642 = _SlopeCorrectionOffset;
			float SlopeCorrectionMagnitude1570 = _SlopeCorrectionMagnitude;
			float RotationAngle29_g172 = ( saturate( ( (0.0 + ((StaticWorldSpaceNoise1340).x - 0.0) * (SlopeCorrectionOffset1642 - 0.0) / (1.0 - 0.0)) + SlopeCorrectionMagnitude1570 ) ) * temp_output_12_0_g172 );
			float3 rotatedValue35_g172 = RotateAroundAxis( Pivot1504, ( ase_vertex3Pos + MainBending89_g172 ), RotationAxis30_g172, RotationAngle29_g172 );
			float3 lerpResult52_g172 = lerp( MainBending89_g172 , ( rotatedValue35_g172 - ase_vertex3Pos ) , NaNPrevention21_g172);
			#ifdef _ENABLESLOPECORRECTION_ON
				float3 staticSwitch1565 = lerpResult52_g172;
			#else
				float3 staticSwitch1565 = temp_output_1545_0;
			#endif
			float3 LocalVertexOffset1045 = staticSwitch1565;
			v.vertex.xyz += LocalVertexOffset1045;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_MainTex1481 = i.uv_texcoord;
			float4 tex2DNode1481 = tex2D( _MainTex, uv_MainTex1481 );
			float MainTextureColor1487 = tex2DNode1481.r;
			float smoothstepResult1550 = smoothstep( _ColorBlendStart , _ColorBlendEnd , i.uv_texcoord.y);
			float4 lerpResult1480 = lerp( _Color2 , _Color1 , smoothstepResult1550);
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
	CustomEditor "StylisedGrass_MaterialInspector"
}
/*ASEBEGIN
Version=18500
2194.286;690.1429;1156;516;4942.316;-4079.051;1.469193;True;False
Node;AmplifyShaderEditor.CommentaryNode;490;-4485.178,3842.801;Inherit;False;898.0664;507.0813;;6;928;738;1504;1503;1502;1654;Vertex Colors and UVs Baked Data;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1502;-4443.396,4160.054;Inherit;False;1;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;1503;-4177.067,4168.518;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NegateNode;1654;-3999.736,4164.199;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;1343;-5250.086,2944.493;Inherit;False;1538.849;638.5145;;8;1649;1556;1340;1344;1574;1645;1560;1500;World Position Noise;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1504;-3833.567,4160.418;Inherit;False;Pivot;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1500;-5189.595,2984.071;Inherit;False;1504;Pivot;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;866;-6399.086,2178.779;Inherit;False;894.3172;1406.288;;26;1534;1570;1642;1569;1641;1335;1334;1373;952;1360;870;877;1356;1525;873;880;1262;1286;1524;480;1653;687;300;1650;1651;1652;Material Properties;1,1,1,1;0;0
Node;AmplifyShaderEditor.FunctionNode;1560;-4955.422,2989.588;Inherit;False;WorldSpaceUVs - NHP;-1;;71;88a2e8a391a04e241878bdb87d9283a3;0;1;6;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;1651;-6352.832,2817.725;Float;False;Property;_MBWindDir;MB Wind Dir;11;0;Create;False;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1650;-6353.587,2898.093;Inherit;False;Global;MBGlobalWindDir;MB Global Wind Dir;28;1;[HideInInspector];Create;True;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1652;-6351.228,2980.915;Inherit;False;Property;_MBWindDirBlend;MB Wind Dir Blend;13;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;1556;-4923.387,3076.077;Inherit;True;Property;_NoiseTexture;Noise Texture;19;1;[NoScaleOffset];Create;True;0;0;False;0;False;None;512fa11ad89d84543ad8d6c8d9cb6743;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.Vector4Node;1645;-4991.832,3272.741;Inherit;False;Property;_NoiseTextureTilling;Noise Tilling - Static (XY), Animated (ZW);20;0;Create;False;0;0;False;0;False;1,1,1,1;1,1,1,1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;1649;-4927.279,3454.543;Float;False;Property;_NoisePannerSpeed;Noise Panner Speed;21;0;Create;True;0;0;False;0;False;0.05,0.03;0.08,0.1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;1334;-6353.02,3172.413;Inherit;False;Property;_MBMaxHeight;MB Max Height;15;0;Create;True;0;0;False;0;False;1;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;738;-4448.384,3905.37;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;687;-6353.188,2248.833;Float;False;Property;_MBDefaultBending;MB Default Bending;6;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1262;-6353.188,2440.832;Float;False;Property;_MBAmplitudeOffset;MB Amplitude Offset;8;0;Create;True;0;0;False;0;False;2;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1574;-4589.422,3192.682;Inherit;False;WorldSpaceNoise - NHP;-1;;103;af5fa9ff24e18344ebcc05b64d296c57;0;4;22;FLOAT2;0,0;False;20;SAMPLER2D;;False;24;FLOAT4;1,1,1,1;False;19;FLOAT2;0.1,0.1;False;2;COLOR;0;COLOR;16
Node;AmplifyShaderEditor.RangedFloatNode;1524;-6351.089,2630.966;Inherit;False;Property;_MBFrequencyOffset;MB Frequency Offset;10;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;300;-6353.188,2344.832;Float;False;Property;_MBAmplitude;MB Amplitude;7;0;Create;True;0;0;False;0;False;1.5;1.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;480;-6353.188,2536.832;Float;False;Property;_MBFrequency;MB Frequency;9;0;Create;True;0;0;False;0;False;1.11;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;1653;-6034.522,2880.247;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1286;-6349.79,2727.702;Float;False;Property;_MBPhase;MB Phase;14;0;Create;True;0;0;False;0;False;3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;952;-6350.31,3085.15;Float;False;Property;_MBWindDirOffset;MB Wind Dir Offset;12;0;Create;True;0;0;False;0;False;20;0;0;180;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1356;-5867.188,2440.832;Inherit;False;MB_AmplitudeOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;928;-3995.768,3993.253;Float;False;DB_PhaseShift;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;870;-5851.79,2874.152;Float;False;MB_WindDirection;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;1068;-3450.039,2307.786;Inherit;False;3576.668;1272.024;;35;1045;1565;1545;1648;1572;1562;1643;1644;1544;1546;1542;1536;1541;1543;1539;1413;1421;1540;1538;1420;97;1586;1585;1378;1512;1366;1375;1370;1369;1526;1365;1376;1362;1367;1371;Main Bending;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1373;-5894.02,3082.959;Inherit;False;MB_WindDirectionOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1335;-5832.964,3169.412;Inherit;False;MB_MaxHeight;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1340;-4027.563,3144.099;Inherit;False;StaticWorldSpaceNoise;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;880;-5835.188,2344.832;Float;False;MB_Amplitude;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1525;-5869.192,2631.124;Inherit;False;MB_FrequencyOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;873;-5835.188,2536.832;Float;False;MB_Frequency;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;877;-5867.188,2248.833;Float;False;MB_DefaultBending;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1360;-5831.79,2727.702;Inherit;False;MB_Phase;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1344;-4027.007,3256.267;Inherit;False;AnimatedWorldSpaceNoise;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;1370;-3289.468,3369.12;Inherit;False;1335;MB_MaxHeight;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1375;-3300.806,2375.234;Inherit;False;870;MB_WindDirection;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1512;-3296.005,3277.568;Inherit;False;928;DB_PhaseShift;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1526;-3320.468,3085.416;Inherit;False;1525;MB_FrequencyOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1376;-3339.476,2468.353;Inherit;False;1373;MB_WindDirectionOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1365;-3282.54,2795.947;Inherit;False;880;MB_Amplitude;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1362;-3316.864,2699.947;Inherit;False;877;MB_DefaultBending;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1378;-3359.333,2570.273;Inherit;False;1344;AnimatedWorldSpaceNoise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;1371;-3344.529,3461.943;Inherit;False;1340;StaticWorldSpaceNoise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;1366;-3321.54,2894.947;Inherit;False;1356;MB_AmplitudeOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1367;-3284.54,2991.947;Inherit;False;873;MB_Frequency;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1369;-3259.54,3186.947;Inherit;False;1360;MB_Phase;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1586;-2962.23,2999.21;Inherit;False;RotationAngle - NHP;-1;;122;87b0b7c0fc8f1424db43b84d20c2e79b;0;9;36;FLOAT;0;False;35;FLOAT;0;False;34;FLOAT;1;False;28;FLOAT;1;False;47;FLOAT;0;False;29;FLOAT;1;False;46;FLOAT;0;False;42;FLOAT;0.1;False;27;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1585;-2982.117,2452.388;Inherit;False;RotationAxis - NHP;-1;;121;b90648f17dcc4bc449d46e8cf04564ff;0;3;20;FLOAT;0;False;19;FLOAT;0;False;18;FLOAT4;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;97;-2519.946,2993.336;Float;False;MB_RotationAngle;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;1538;-2266.959,2698.509;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;489;-1404.902,1152.3;Inherit;False;1524.726;891.1869;;15;1494;1493;1491;1489;1490;1487;1488;1480;1481;1476;1477;1550;1548;1549;1547;Albedo;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1420;-2619.547,2447.874;Inherit;False;MB_RotationAxis;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1413;-2131.588,2625.069;Inherit;False;97;MB_RotationAngle;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;1539;-2033.958,2721.509;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1421;-2118.588,2533.069;Inherit;False;1420;MB_RotationAxis;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1547;-1294.205,1480.932;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1549;-1334.433,1716.105;Inherit;False;Property;_ColorBlendEnd;Color Blend End;4;0;Create;True;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;1540;-2089.958,2852.509;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1548;-1334.703,1619.052;Inherit;False;Property;_ColorBlendStart;Color Blend Start;3;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotateAboutAxisNode;1536;-1819.482,2660.204;Inherit;False;False;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;1569;-6355.173,3266.013;Inherit;False;Property;_SlopeCorrectionMagnitude;Slope Correction Magnitude;17;0;Create;True;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;1477;-1049.371,1413.076;Inherit;False;Property;_Color1;Color 1;1;0;Create;True;0;0;False;0;False;0.3393185,0.490566,0.09255961,1;0.5754717,0.3435538,0.1465824,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1641;-6354.025,3354.562;Inherit;False;Property;_SlopeCorrectionOffset;Slope Correction Offset;18;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1542;-1677.803,2989.766;Float;False;Constant;_Float11;Float 11;8;0;Create;True;0;0;False;0;False;0.01;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;1550;-975.4325,1600.105;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;1476;-1053.57,1232.292;Inherit;False;Property;_Color2;Color 2;2;0;Create;True;0;0;False;0;False;0.719914,0.8207547,0.3639195,1;0.009433985,0.4014694,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PosVertexDataNode;1541;-1708.758,3074.671;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PosVertexDataNode;1543;-1710.445,2816.452;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;1570;-5910.173,3265.013;Inherit;False;SlopeCorrectionMagnitude;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;1546;-1456.777,2733.261;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;1480;-760.8494,1392.762;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1642;-5883.025,3354.562;Inherit;False;SlopeCorrectionOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1481;-1365.69,1843.528;Inherit;True;Property;_MainTex;Main Tex;0;1;[NoScaleOffset];Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;1544;-1438.628,3041.114;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1488;-561.6697,1386.595;Inherit;False;Color;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1487;-1014.167,1845.01;Inherit;False;MainTextureColor;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1562;-1145.919,3266.502;Inherit;False;1504;Pivot;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1643;-1225.919,3106.502;Inherit;False;1642;SlopeCorrectionOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1644;-1241.919,3186.502;Inherit;False;1340;StaticWorldSpaceNoise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1545;-1186.056,2877.184;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1572;-1257.919,3026.502;Inherit;False;1570;SlopeCorrectionMagnitude;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1648;-889.9186,3058.502;Inherit;False;SlopeCorrection - NHP;-1;;172;af38de3ca0adf3c4ba9b6a3dd482959e;0;5;87;FLOAT3;0,0,0;False;42;FLOAT;1;False;92;FLOAT;0;False;93;FLOAT4;0,0,0,0;False;41;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1490;-605.9545,1665.131;Inherit;False;1487;MainTextureColor;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1489;-557.7126,1762.656;Inherit;False;1488;Color;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1491;-339.3347,1706.322;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;1565;-450.904,2875.348;Float;False;Property;_EnableSlopeCorrection;Enable Slope Correction;16;0;Create;True;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1045;-105.4578,2874.823;Float;False;LocalVertexOffset;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1493;-176.7338,1700.468;Float;False;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1494;-1010.189,1944.06;Float;False;Opacity;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;1265;515.5881,2044.887;Inherit;False;631.4954;512.0168;;4;0;1061;296;1508;Master Node;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;1061;552.4447,2423.432;Inherit;False;1045;LocalVertexOffset;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;296;599.0982,2107.852;Inherit;False;1493;Albedo;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;1328;-19355.95,10790.4;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1534;-6128.229,3484.561;Inherit;False;Property;_AlphaCutoff;Alpha Cutoff;5;0;Create;True;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1508;604.6362,2305.683;Inherit;False;1494;Opacity;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;1330;-19002.03,10716.32;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;883.2871,2113.905;Float;False;True;-1;2;StylisedGrass_MaterialInspector;0;0;Standard;Nicrom/NHP/ASE/Stylised Grass;False;False;False;False;False;False;True;False;False;False;False;False;True;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Opaque;;AlphaTest;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;True;1534;1;Pragma;multi_compile_instancing;False;;Custom;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;1503;0;1502;1
WireConnection;1503;2;1502;2
WireConnection;1654;0;1503;0
WireConnection;1504;0;1654;0
WireConnection;1560;6;1500;0
WireConnection;1574;22;1560;0
WireConnection;1574;20;1556;0
WireConnection;1574;24;1645;0
WireConnection;1574;19;1649;0
WireConnection;1653;0;1651;0
WireConnection;1653;1;1650;0
WireConnection;1653;2;1652;0
WireConnection;1356;0;1262;0
WireConnection;928;0;738;4
WireConnection;870;0;1653;0
WireConnection;1373;0;952;0
WireConnection;1335;0;1334;0
WireConnection;1340;0;1574;0
WireConnection;880;0;300;0
WireConnection;1525;0;1524;0
WireConnection;873;0;480;0
WireConnection;877;0;687;0
WireConnection;1360;0;1286;0
WireConnection;1344;0;1574;16
WireConnection;1586;36;1362;0
WireConnection;1586;35;1365;0
WireConnection;1586;34;1366;0
WireConnection;1586;28;1367;0
WireConnection;1586;47;1526;0
WireConnection;1586;29;1369;0
WireConnection;1586;46;1512;0
WireConnection;1586;42;1370;0
WireConnection;1586;27;1371;0
WireConnection;1585;20;1375;0
WireConnection;1585;19;1376;0
WireConnection;1585;18;1378;0
WireConnection;97;0;1586;0
WireConnection;1420;0;1585;0
WireConnection;1539;0;1538;1
WireConnection;1539;2;1538;3
WireConnection;1536;0;1421;0
WireConnection;1536;1;1413;0
WireConnection;1536;2;1539;0
WireConnection;1536;3;1540;0
WireConnection;1550;0;1547;2
WireConnection;1550;1;1548;0
WireConnection;1550;2;1549;0
WireConnection;1570;0;1569;0
WireConnection;1546;0;1536;0
WireConnection;1546;1;1543;0
WireConnection;1480;0;1476;0
WireConnection;1480;1;1477;0
WireConnection;1480;2;1550;0
WireConnection;1642;0;1641;0
WireConnection;1544;0;1542;0
WireConnection;1544;1;1541;2
WireConnection;1488;0;1480;0
WireConnection;1487;0;1481;1
WireConnection;1545;0;1546;0
WireConnection;1545;1;1544;0
WireConnection;1648;87;1545;0
WireConnection;1648;42;1572;0
WireConnection;1648;92;1643;0
WireConnection;1648;93;1644;0
WireConnection;1648;41;1562;0
WireConnection;1491;0;1490;0
WireConnection;1491;1;1489;0
WireConnection;1565;1;1545;0
WireConnection;1565;0;1648;0
WireConnection;1045;0;1565;0
WireConnection;1493;0;1491;0
WireConnection;1494;0;1481;4
WireConnection;0;0;296;0
WireConnection;0;10;1508;0
WireConnection;0;11;1061;0
ASEEND*/
//CHKSM=D6BD1521180A268F45544C110812CE7F9F65163C