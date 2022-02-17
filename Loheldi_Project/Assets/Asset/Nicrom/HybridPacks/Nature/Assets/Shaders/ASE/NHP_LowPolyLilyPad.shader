// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Nicrom/NHP/ASE/Low Poly Lily Pad"
{
	Properties
	{
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_MainTex("Main Tex", 2D) = "white" {}
		_HorizontalAmplitude("Horizontal Amplitude", Float) = 0.05
		_HorizontalAmplitudeOffset("Horizontal Amplitude Offset", Float) = 0.04
		_HorizontalFrequency("Horizontal Frequency", Float) = 2
		_HorizontalFrequencyOffset("Horizontal Frequency Offset", Float) = 0.5
		_HorizontalPhase("Horizontal Phase", Float) = 1
		_HorizontalWindDir("Horizontal Wind Dir", Range( 0 , 360)) = 0
		_HorizontalWindDirOffset("Horizontal Wind Dir Offset", Range( 0 , 180)) = 20
		_HorizontalWindDirBlend("Horizontal Wind Dir Blend", Range( 0 , 1)) = 0
		[Toggle(_ENABLEROTATION_ON)] _EnableRotation("Enable Rotation", Float) = 1
		_RotationAmplitude("Rotation Amplitude", Float) = 8
		_RotationAmplitudeOffset("Rotation Amplitude Offset", Float) = 3
		_RotationFrequency("Rotation Frequency", Float) = 1.16
		_RotationFrequencyOffset("Rotation Frequency Offset", Float) = 0.5
		_RotationPhase("Rotation Phase", Float) = 1
		[NoScaleOffset]_NoiseTexture("Noise Texture", 2D) = "white" {}
		_NoiseTextureTilling("Noise Tilling - Static (XY), Animated (ZW)", Vector) = (1,1,1,1)
		_NoisePannerSpeed("Noise Panner Speed", Vector) = (0.05,0.03,0,0)
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
		#pragma shader_feature_local _ENABLEROTATION_ON
		#pragma multi_compile_instancing
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows noinstancing nolightmap  vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _HorizontalAmplitude;
		uniform float _HorizontalAmplitudeOffset;
		uniform sampler2D _NoiseTexture;
		uniform float4 _NoiseTextureTilling;
		uniform float _HorizontalFrequency;
		uniform float _HorizontalFrequencyOffset;
		uniform float _HorizontalPhase;
		uniform float _HorizontalWindDir;
		uniform float GlobalWindDir;
		uniform float _HorizontalWindDirBlend;
		uniform float _HorizontalWindDirOffset;
		uniform float2 _NoisePannerSpeed;
		uniform float _RotationAmplitudeOffset;
		uniform float _RotationAmplitude;
		uniform float _RotationFrequency;
		uniform float _RotationFrequencyOffset;
		uniform float _RotationPhase;
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
			float HorizontalAmplitude880 = _HorizontalAmplitude;
			float VerticalAmplitudeOffset1356 = _HorizontalAmplitudeOffset;
			float4 transform1_g63 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float3 appendResult1503 = (float3(v.texcoord1.xy.x , 0.0 , v.texcoord1.xy.y));
			float3 Pivot1504 = -appendResult1503;
			float4 transform2_g63 = mul(unity_ObjectToWorld,float4( Pivot1504 , 0.0 ));
			float2 UVs27_g90 = ( (transform1_g63).xz + (transform2_g63).xz );
			float4 temp_output_24_0_g90 = _NoiseTextureTilling;
			float2 StaticNoileTilling28_g90 = (temp_output_24_0_g90).xy;
			float4 StaticNoise1340 = tex2Dlod( _NoiseTexture, float4( ( UVs27_g90 * StaticNoileTilling28_g90 ), 0, 0.0) );
			float4 transform1607 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float HorizontalFrequency873 = _HorizontalFrequency;
			float HorizontalFrequencyOffset1525 = _HorizontalFrequencyOffset;
			float DB_PhaseShift928 = v.color.a;
			float HorizontalPhase1360 = _HorizontalPhase;
			float lerpResult1703 = lerp( _HorizontalWindDir , GlobalWindDir , _HorizontalWindDirBlend);
			float HorizontalDirection870 = lerpResult1703;
			float HorizontalDirectionOffset1373 = _HorizontalWindDirOffset;
			float2 AnimatedNoiseTilling29_g90 = (temp_output_24_0_g90).zw;
			float2 panner7_g90 = ( 0.1 * _Time.y * _NoisePannerSpeed + float2( 0,0 ));
			float4 AnimatedNoise1344 = tex2Dlod( _NoiseTexture, float4( ( ( UVs27_g90 * AnimatedNoiseTilling29_g90 ) + panner7_g90 ), 0, 0.0) );
			float temp_output_11_0_g91 = radians( ( ( HorizontalDirection870 + ( HorizontalDirectionOffset1373 * (-1.0 + ((AnimatedNoise1344).x - 0.0) * (1.0 - -1.0) / (1.0 - 0.0)) ) ) * -1.0 ) );
			float3 appendResult14_g91 = (float3(cos( temp_output_11_0_g91 ) , 0.0 , sin( temp_output_11_0_g91 )));
			float4 transform15_g91 = mul(unity_WorldToObject,float4( appendResult14_g91 , 0.0 ));
			float3 normalizeResult34_g91 = normalize( (transform15_g91).xyz );
			float3 Direction1420 = normalizeResult34_g91;
			float3 ase_vertex3Pos = v.vertex.xyz;
			float3 HorizontalMovement1680 = ( ( ( ( HorizontalAmplitude880 + ( VerticalAmplitudeOffset1356 * (StaticNoise1340).r ) ) * sin( ( ( ( transform1607.x + transform1607.z ) + ( ( _Time.y * ( HorizontalFrequency873 + ( HorizontalFrequencyOffset1525 * (StaticNoise1340).g ) ) ) + ( ( 2.0 * UNITY_PI ) * DB_PhaseShift928 ) ) ) * HorizontalPhase1360 ) ) ) * Direction1420 ) * step( -0.1 , ase_vertex3Pos.y ) );
			float RotationAmplitudeOffset1684 = _RotationAmplitudeOffset;
			float RotationAmplitude1221 = _RotationAmplitude;
			float RotationFrequency1219 = _RotationFrequency;
			float4 transform1666 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float RotationFrequemcyOffset1686 = _RotationFrequencyOffset;
			float RotationPhase1401 = _RotationPhase;
			float3 rotatedValue1647 = RotateAroundAxis( Pivot1504, ase_vertex3Pos, float3(0,1,0), radians( ( ( ( RotationAmplitudeOffset1684 * (StaticNoise1340).r ) + RotationAmplitude1221 ) * sin( ( ( ( _Time.y * RotationFrequency1219 ) - ( ( 2.0 * UNITY_PI ) * ( 1.0 - DB_PhaseShift928 ) ) ) + ( ( ( transform1666.x + transform1666.z ) + ( _Time.y * ( RotationFrequency1219 + ( RotationFrequemcyOffset1686 * (StaticNoise1340).g ) ) ) ) * RotationPhase1401 ) ) ) ) ) );
			#ifdef _ENABLEROTATION_ON
				float3 staticSwitch1678 = ( rotatedValue1647 - ase_vertex3Pos );
			#else
				float3 staticSwitch1678 = float3(0,0,0);
			#endif
			float3 RotationMovement1672 = staticSwitch1678;
			float3 LocalVertexOffset1045 = ( HorizontalMovement1680 + RotationMovement1672 );
			v.vertex.xyz += LocalVertexOffset1045;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			float4 Albedo1493 = tex2D( _MainTex, uv_MainTex );
			o.Albedo = Albedo1493.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "LowPolyLilyPad_MaterialInspector"
}
/*ASEBEGIN
Version=18500
2194.286;690.1429;1156;656;6137.171;-4035.275;2.672797;True;False
Node;AmplifyShaderEditor.CommentaryNode;490;-5379.032,4227.108;Inherit;False;896.0664;501.0813;;6;928;738;1504;1503;1502;1704;Vertex Colors and UVs Baked Data;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1502;-5320.25,4561.363;Inherit;False;1;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;1503;-5058.921,4566.827;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NegateNode;1704;-4870.769,4566.007;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;1343;-6018.209,2168.092;Inherit;False;1407.986;647.5546;;8;1536;1500;1344;1340;1545;1698;1697;1538;World Space Noise;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1504;-4698.921,4559.827;Inherit;False;Pivot;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1500;-5983.118,2219.627;Inherit;False;1504;Pivot;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector4Node;1697;-5818.059,2506.741;Inherit;False;Property;_NoiseTextureTilling;Noise Tilling - Static (XY), Animated (ZW);18;0;Create;False;0;0;False;0;False;1,1,1,1;1,1,1,1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;1538;-5749.85,2306.892;Inherit;True;Property;_NoiseTexture;Noise Texture;17;1;[NoScaleOffset];Create;True;0;0;False;0;False;None;512fa11ad89d84543ad8d6c8d9cb6743;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.FunctionNode;1536;-5774.082,2223.093;Inherit;False;WorldSpaceUVs - NHP;-1;;63;88a2e8a391a04e241878bdb87d9283a3;0;1;6;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;1698;-5753.059,2679.742;Float;False;Property;_NoisePannerSpeed;Noise Panner Speed;19;0;Create;True;0;0;False;0;False;0.05,0.03;0.08,0.1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.CommentaryNode;866;-6135.91,3065.794;Inherit;False;1521.371;894.004;;26;1685;1524;1686;1218;1525;480;1219;873;1308;1683;1702;1701;1401;850;1684;1703;1286;1220;1262;300;1356;870;1221;1360;1373;880;Material Properties;1,1,1,1;0;0
Node;AmplifyShaderEditor.FunctionNode;1545;-5418.711,2444.107;Inherit;False;WorldSpaceNoise - NHP;-1;;90;af5fa9ff24e18344ebcc05b64d296c57;0;4;22;FLOAT2;0,0;False;20;SAMPLER2D;;False;24;FLOAT4;1,1,1,1;False;19;FLOAT2;0.1,0.1;False;2;COLOR;0;COLOR;16
Node;AmplifyShaderEditor.CommentaryNode;1634;-4352.948,2690.753;Inherit;False;3588.147;1273.3;;40;1689;1687;1690;1688;1663;1668;1691;1655;1666;1648;1654;1652;1665;1638;1637;1670;1649;1653;1664;1692;1694;1658;1635;1693;1671;1695;1660;1644;1696;1636;1643;1641;1657;1661;1639;1647;1677;1650;1678;1672;Rotation Around Local Pivot;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;1685;-5277.209,3441.665;Inherit;False;Property;_RotationFrequencyOffset;Rotation Frequency Offset;15;0;Create;True;0;0;False;0;False;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1340;-4889.351,2403.303;Inherit;False;StaticNoise;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1686;-4945.209,3440.665;Inherit;False;RotationFrequemcyOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1218;-5274.93,3337.358;Float;False;Property;_RotationFrequency;Rotation Frequency;14;0;Create;True;0;0;False;0;False;1.16;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1689;-4315.152,3859.191;Inherit;False;1340;StaticNoise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;1690;-4240.074,3772.153;Inherit;False;1686;RotationFrequemcyOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;1068;-3453.813,1154.218;Inherit;False;2685.381;1277.469;;37;1680;1569;1565;1567;1566;1568;1593;1608;1603;1420;1618;1528;1632;1613;1604;1378;1376;1375;1631;1605;1614;1633;1610;1612;1616;1625;1607;1617;1626;1619;1622;1630;1627;1629;1623;1628;1624;Horizontal Movement;1,1,1,1;0;0
Node;AmplifyShaderEditor.SwizzleNode;1687;-4105.153,3858.191;Inherit;False;FLOAT;1;1;2;3;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1524;-6071.786,3418.967;Inherit;False;Property;_HorizontalFrequencyOffset;Horizontal Frequency Offset;6;0;Create;True;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;738;-5353.238,4285.678;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;1219;-4904.93,3340.358;Float;False;RotationFrequency;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1624;-3433.057,2167.822;Inherit;False;1340;StaticNoise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1688;-3920.155,3808.191;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1663;-4031.424,3683.844;Inherit;False;1219;RotationFrequency;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1525;-5651.889,3418.126;Inherit;False;HorizontalFrequencyOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;928;-4913.641,4374.979;Float;False;DB_PhaseShift;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;480;-6075.884,3320.834;Float;False;Property;_HorizontalFrequency;Horizontal Frequency;5;0;Create;True;0;0;False;0;False;2;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1668;-3888.11,3190.505;Inherit;False;928;DB_PhaseShift;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;873;-5613.884,3318.834;Float;False;HorizontalFrequency;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1308;-5275.93,3541.358;Float;False;Property;_RotationPhase;Rotation Phase;16;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1691;-3739.826,3737.33;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ObjectToWorldTransfNode;1666;-3815.802,3330.4;Inherit;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TimeNode;1655;-3813.09,3519.284;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;1628;-3385.979,2084.784;Inherit;False;1525;HorizontalFrequencyOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SwizzleNode;1623;-3231.056,2168.822;Inherit;False;FLOAT;1;1;2;3;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PiNode;1652;-3647.151,3099.224;Inherit;False;1;0;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1627;-3039.059,2118.822;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;1637;-3675.566,2839.746;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;1401;-4874.93,3540.358;Inherit;False;RotationPhase;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;1654;-3633.58,3195.09;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1638;-3563.62,3609.6;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1665;-3558.6,3370.221;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1683;-5273.209,3231.665;Inherit;False;Property;_RotationAmplitudeOffset;Rotation Amplitude Offset;13;0;Create;True;0;0;False;0;False;3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1629;-3165.98,2004.783;Inherit;False;873;HorizontalFrequency;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1648;-3717.717,2991.956;Inherit;False;1219;RotationFrequency;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;1626;-3003.903,1839.21;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PiNode;1622;-2973.354,2227.082;Inherit;False;1;0;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1630;-3010.744,2308.202;Inherit;False;928;DB_PhaseShift;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1670;-3483.947,3720.252;Inherit;False;1401;RotationPhase;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1701;-6074.14,3688.748;Inherit;False;Global;GlobalWindDir;Global Wind Dir;28;1;[HideInInspector];Create;True;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1619;-2896.45,2050.497;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1220;-5272.285,3130.368;Float;False;Property;_RotationAmplitude;Rotation Amplitude;12;0;Create;True;0;0;False;0;False;8;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1649;-3421.164,3124.224;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1653;-3383.496,3476.975;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1684;-4934.209,3229.665;Inherit;False;RotationAmplitudeOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;850;-6074.486,3609.703;Float;False;Property;_HorizontalWindDir;Horizontal Wind Dir;8;0;Create;True;0;0;False;0;False;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1702;-6073.14,3768.748;Inherit;False;Property;_HorizontalWindDirBlend;Horizontal Wind Dir Blend;10;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1692;-3095.781,2878.606;Inherit;False;1340;StaticNoise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1664;-3411.519,2916.246;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1625;-2759.837,1935.809;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1262;-6074.884,3224.834;Float;False;Property;_HorizontalAmplitudeOffset;Horizontal Amplitude Offset;4;0;Create;True;0;0;False;0;False;0.04;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;1658;-3217.84,3011.875;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ObjectToWorldTransfNode;1607;-2758.894,1721.441;Inherit;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1617;-2753.352,2254.082;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1286;-6072.486,3510.703;Float;False;Property;_HorizontalPhase;Horizontal Phase;7;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1635;-3220.961,3591.762;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;1703;-5769.14,3671.748;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SwizzleNode;1694;-2872.115,2879.555;Inherit;False;FLOAT;0;1;2;3;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1693;-2971.731,2778.728;Inherit;False;1684;RotationAmplitudeOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1221;-4899.285,3128.368;Float;False;RotationAmplitude;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;952;-6071.293,3858.168;Float;False;Property;_HorizontalWindDirOffset;Horizontal Wind Dir Offset;9;0;Create;True;0;0;False;0;False;20;0;0;180;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1360;-5585.486,3507.703;Inherit;False;HorizontalPhase;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1373;-5647.003,3854.976;Inherit;False;HorizontalDirectionOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1610;-2553.076,2074.091;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1356;-5637.884,3221.834;Inherit;False;VerticalAmplitudeOffset;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1660;-2933.465,3282.614;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1695;-2669.171,2828.602;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1612;-2542.181,1758.743;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;300;-6074.884,3130.834;Float;False;Property;_HorizontalAmplitude;Horizontal Amplitude;3;0;Create;True;0;0;False;0;False;0.05;1.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1616;-2569.391,1615.016;Inherit;False;1340;StaticNoise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1344;-4890.191,2514.907;Inherit;False;AnimatedNoise;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;870;-5602.486,3667.153;Float;False;HorizontalDirection;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1671;-2791.78,2993.071;Inherit;False;1221;RotationAmplitude;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1633;-2430.342,2169.137;Inherit;False;1360;HorizontalPhase;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1631;-2442.342,1513.136;Inherit;False;1356;VerticalAmplitudeOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1378;-3350.834,1448.578;Inherit;False;1344;AnimatedNoise;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SwizzleNode;1614;-2342.726,1613.964;Inherit;False;FLOAT;0;1;2;3;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;880;-5611.884,3129.834;Float;False;HorizontalAmplitude;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1605;-2338.956,1900.49;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1376;-3408.834,1357.578;Inherit;False;1373;HorizontalDirectionOffset;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1696;-2503.39,2903.181;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;1644;-2497.763,3273.783;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1375;-3375.835,1260.578;Inherit;False;870;HorizontalDirection;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1528;-3064.106,1340.093;Inherit;False;RotationAxis - NHP;-1;;91;b90648f17dcc4bc449d46e8cf04564ff;0;3;20;FLOAT;0;False;19;FLOAT;0;False;18;FLOAT4;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1632;-2212.341,1426.136;Inherit;False;880;HorizontalAmplitude;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1613;-2139.782,1563.01;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1604;-2138.875,2027.535;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1636;-2298.768,3067.947;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1641;-2133.564,3179.524;Inherit;False;1504;Pivot;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1420;-2674.834,1335.828;Inherit;False;Direction;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SinOpNode;1603;-1947.885,1929.761;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1618;-1928.743,1475.469;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;1661;-2144.538,3268.577;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RadiansOpNode;1657;-2095.241,3078.15;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;1643;-2124.211,2919.361;Float;False;Constant;_Vector0;Vector 0;25;0;Create;True;0;0;False;0;False;0,1,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.PosVertexDataNode;1566;-1751.903,2173.77;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1608;-1718.836,1671.797;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;1639;-1776.823,3244.53;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RotateAboutAxisNode;1647;-1885.347,3088.926;Inherit;False;False;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1593;-1764.635,1957.884;Inherit;False;1420;Direction;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;1568;-1726.948,2088.865;Float;False;Constant;_Float11;Float 11;8;0;Create;True;0;0;False;0;False;-0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1565;-1513.744,1795.022;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StepOpNode;1567;-1492.772,2149.213;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;1677;-1544.532,2982.859;Float;False;Constant;_Vector2;Vector 2;27;0;Create;True;0;0;False;0;False;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleSubtractOpNode;1650;-1524.262,3163.759;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1569;-1253.082,1999.11;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StaticSwitch;1678;-1319.146,3082.841;Float;False;Property;_EnableRotation;Enable Rotation;11;0;Create;True;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1672;-1019.825,3082.279;Inherit;False;RotationMovement;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1680;-1062.693,1994.017;Inherit;False;HorizontalMovement;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;1682;-511.2046,2700.939;Inherit;False;758.2285;254.6436;Comment;4;1674;1681;1673;1045;Local Vertex Offset;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;1681;-486.2089,2751.458;Inherit;False;1680;HorizontalMovement;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1674;-473.5677,2846.896;Inherit;False;1672;RotationMovement;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;489;-514.0952,2056.178;Inherit;False;630.6262;377.7517;;2;1553;1493;Albedo;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;1553;-446.7745,2157.093;Inherit;True;Property;_MainTex;Main Tex;2;0;Create;True;0;0;False;0;False;-1;None;82b66ffd8f6978c44a3d60fd789fe71d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;1673;-189.1746,2790.178;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1493;-88.80232,2157.724;Float;False;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;1265;643.4949,1789.945;Inherit;False;638.3361;641.9899;;5;1700;296;1061;1699;0;Master Node;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1045;-46.9531,2783.239;Float;False;LocalVertexOffset;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;296;750.079,1865.653;Inherit;False;1493;Albedo;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;1699;662.9905,1976.196;Inherit;False;Property;_Metallic;Metallic;0;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1061;696.2244,2220.121;Inherit;False;1045;LocalVertexOffset;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;1700;662.9905,2063.196;Inherit;False;Property;_Smoothness;Smoothness;1;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;1330;-19002.03,10716.32;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1004.352,1928.737;Float;False;True;-1;2;LowPolyLilyPad_MaterialInspector;0;0;Standard;Nicrom/NHP/ASE/Low Poly Lily Pad;False;False;False;False;False;False;True;False;False;False;False;False;False;False;False;False;False;True;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;True;1534;1;Pragma;multi_compile_instancing;False;;Custom;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;1503;0;1502;1
WireConnection;1503;2;1502;2
WireConnection;1704;0;1503;0
WireConnection;1504;0;1704;0
WireConnection;1536;6;1500;0
WireConnection;1545;22;1536;0
WireConnection;1545;20;1538;0
WireConnection;1545;24;1697;0
WireConnection;1545;19;1698;0
WireConnection;1340;0;1545;0
WireConnection;1686;0;1685;0
WireConnection;1687;0;1689;0
WireConnection;1219;0;1218;0
WireConnection;1688;0;1690;0
WireConnection;1688;1;1687;0
WireConnection;1525;0;1524;0
WireConnection;928;0;738;4
WireConnection;873;0;480;0
WireConnection;1691;0;1663;0
WireConnection;1691;1;1688;0
WireConnection;1623;0;1624;0
WireConnection;1627;0;1628;0
WireConnection;1627;1;1623;0
WireConnection;1401;0;1308;0
WireConnection;1654;0;1668;0
WireConnection;1638;0;1655;2
WireConnection;1638;1;1691;0
WireConnection;1665;0;1666;1
WireConnection;1665;1;1666;3
WireConnection;1619;0;1629;0
WireConnection;1619;1;1627;0
WireConnection;1649;0;1652;0
WireConnection;1649;1;1654;0
WireConnection;1653;0;1665;0
WireConnection;1653;1;1638;0
WireConnection;1684;0;1683;0
WireConnection;1664;0;1637;2
WireConnection;1664;1;1648;0
WireConnection;1625;0;1626;2
WireConnection;1625;1;1619;0
WireConnection;1658;0;1664;0
WireConnection;1658;1;1649;0
WireConnection;1617;0;1622;0
WireConnection;1617;1;1630;0
WireConnection;1635;0;1653;0
WireConnection;1635;1;1670;0
WireConnection;1703;0;850;0
WireConnection;1703;1;1701;0
WireConnection;1703;2;1702;0
WireConnection;1694;0;1692;0
WireConnection;1221;0;1220;0
WireConnection;1360;0;1286;0
WireConnection;1373;0;952;0
WireConnection;1610;0;1625;0
WireConnection;1610;1;1617;0
WireConnection;1356;0;1262;0
WireConnection;1660;0;1658;0
WireConnection;1660;1;1635;0
WireConnection;1695;0;1693;0
WireConnection;1695;1;1694;0
WireConnection;1612;0;1607;1
WireConnection;1612;1;1607;3
WireConnection;1344;0;1545;16
WireConnection;870;0;1703;0
WireConnection;1614;0;1616;0
WireConnection;880;0;300;0
WireConnection;1605;0;1612;0
WireConnection;1605;1;1610;0
WireConnection;1696;0;1695;0
WireConnection;1696;1;1671;0
WireConnection;1644;0;1660;0
WireConnection;1528;20;1375;0
WireConnection;1528;19;1376;0
WireConnection;1528;18;1378;0
WireConnection;1613;0;1631;0
WireConnection;1613;1;1614;0
WireConnection;1604;0;1605;0
WireConnection;1604;1;1633;0
WireConnection;1636;0;1696;0
WireConnection;1636;1;1644;0
WireConnection;1420;0;1528;0
WireConnection;1603;0;1604;0
WireConnection;1618;0;1632;0
WireConnection;1618;1;1613;0
WireConnection;1657;0;1636;0
WireConnection;1608;0;1618;0
WireConnection;1608;1;1603;0
WireConnection;1647;0;1643;0
WireConnection;1647;1;1657;0
WireConnection;1647;2;1641;0
WireConnection;1647;3;1661;0
WireConnection;1565;0;1608;0
WireConnection;1565;1;1593;0
WireConnection;1567;0;1568;0
WireConnection;1567;1;1566;2
WireConnection;1650;0;1647;0
WireConnection;1650;1;1639;0
WireConnection;1569;0;1565;0
WireConnection;1569;1;1567;0
WireConnection;1678;1;1677;0
WireConnection;1678;0;1650;0
WireConnection;1672;0;1678;0
WireConnection;1680;0;1569;0
WireConnection;1673;0;1681;0
WireConnection;1673;1;1674;0
WireConnection;1493;0;1553;0
WireConnection;1045;0;1673;0
WireConnection;0;0;296;0
WireConnection;0;3;1699;0
WireConnection;0;4;1700;0
WireConnection;0;11;1061;0
ASEEND*/
//CHKSM=6C6D1FBE359B3909B1AA88CB11002A8C0EA1BD08