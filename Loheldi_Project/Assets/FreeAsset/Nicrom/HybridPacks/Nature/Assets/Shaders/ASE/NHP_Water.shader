// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Nicrom/NHP/ASE/Water"
{
	Properties
	{
		[HDR]_ShallowWater("Shallow Water", Color) = (0.6862745,0.7843137,0.8196079,1)
		[HDR]_DeepWater("Deep Water", Color) = (0.3607843,0.5137255,0.6313726,1)
		_WaterDepth("Water Depth", Float) = 1
		_EdgeBlend("Edge Blend", Float) = -0.04
		_Metallic("Metallic", Range( 0 , 1)) = 1
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		[NoScaleOffset]_NormalMap("Normal Map", 2D) = "bump" {}
		_TilingMap1XYMap2ZW("Tiling Map1 (XY), Map2 (ZW)", Vector) = (0.1,0.1,0.1,0.1)
		_SpeedMap1XYMap2ZW("Speed Map1 (XY), Map2 (ZW)", Vector) = (8,6,-10,8)
		_Map1Strength("Map1 Strength", Float) = 0.3
		_Map2Strength("Map2 Strength", Float) = 0.2
		[NoScaleOffset]_DistortionTexture("Distortion Texture", 2D) = "bump" {}
		_DistTilingMap1XYMap2ZW("Dist Tiling Map1 (XY), Map2 (ZW)", Vector) = (1,1,1,1)
		_DistSpeedMap1XYMap2ZW("Dist Speed Map1 (XY), Map2 (ZW)", Vector) = (0.2,-0.3,-0.1,0.25)
		_Distortion("Distortion", Float) = 0.3
		[Toggle(_ENABLEFOAM_ON)] _EnableFoam("Enable Foam", Float) = 1
		_FoamSpeed("Foam Speed", Float) = 2
		_FoamWidth("Foam Width", Float) = 19
		_FoamDepth("Foam Depth", Float) = 2.45
		_FoamDepthOffset("Foam Depth Offset", Float) = 0.3
		[Toggle(_ENABLEDISTORTION_ON)] _EnableDistortion("EnableDistortion", Float) = 1
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		GrabPass{ }
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma shader_feature_local _ENABLEDISTORTION_ON
		#pragma shader_feature_local _ENABLEFOAM_ON
		#if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex);
		#else
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex)
		#endif
		#pragma surface surf Standard alpha:fade keepalpha nolightmap  
		struct Input
		{
			float3 worldPos;
			float4 screenPos;
			half ASEVFace : VFACE;
		};

		uniform sampler2D _NormalMap;
		uniform float4 _TilingMap1XYMap2ZW;
		uniform float4 _SpeedMap1XYMap2ZW;
		uniform float _Map1Strength;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _WaterDepth;
		uniform float _Map2Strength;
		ASE_DECLARE_SCREENSPACE_TEXTURE( _GrabTexture )
		uniform sampler2D _DistortionTexture;
		uniform float4 _DistTilingMap1XYMap2ZW;
		uniform float4 _DistSpeedMap1XYMap2ZW;
		uniform float _Distortion;
		uniform float _EdgeBlend;
		uniform float4 _DeepWater;
		uniform float4 _ShallowWater;
		uniform float _FoamWidth;
		uniform float _FoamSpeed;
		uniform float _FoamDepthOffset;
		uniform float _FoamDepth;
		uniform float _Metallic;
		uniform float _Smoothness;


		inline float4 ASE_ComputeGrabScreenPos( float4 pos )
		{
			#if UNITY_UV_STARTS_AT_TOP
			float scale = -1.0;
			#else
			float scale = 1.0;
			#endif
			float4 o = pos;
			o.y = pos.w * 0.5f;
			o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
			return o;
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldPos = i.worldPos;
			float2 temp_output_642_0 = (ase_worldPos).xz;
			float2 appendResult644 = (float2(_TilingMap1XYMap2ZW.x , _TilingMap1XYMap2ZW.y));
			float2 appendResult647 = (float2(_SpeedMap1XYMap2ZW.x , _SpeedMap1XYMap2ZW.y));
			float2 UV133 = ( ( temp_output_642_0 * appendResult644 ) + ( appendResult647 * _Time.x ) );
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float WaterDepth431 = _WaterDepth;
			float screenDepth184 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth184 = abs( ( screenDepth184 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( WaterDepth431 ) );
			float switchResult190 = (((i.ASEVFace>0)?(( ase_worldViewDir.y * distanceDepth184 )):(( distanceDepth184 * -ase_worldViewDir.y ))));
			float DepthFade77 = saturate( switchResult190 );
			float2 appendResult645 = (float2(_TilingMap1XYMap2ZW.z , _TilingMap1XYMap2ZW.w));
			float2 appendResult648 = (float2(_SpeedMap1XYMap2ZW.z , _SpeedMap1XYMap2ZW.w));
			float2 UV243 = ( ( temp_output_642_0 * appendResult645 ) + ( _Time.x * appendResult648 ) );
			float3 Normals10 = BlendNormals( UnpackScaleNormal( tex2D( _NormalMap, -UV133 ), ( _Map1Strength * DepthFade77 ) ) , UnpackScaleNormal( tex2D( _NormalMap, -UV243 ), ( _Map2Strength * DepthFade77 ) ) );
			o.Normal = Normals10;
			float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( ase_screenPos );
			float4 ase_grabScreenPosNorm = ase_grabScreenPos / ase_grabScreenPos.w;
			float2 appendResult419 = (float2(ase_grabScreenPosNorm.r , ase_grabScreenPosNorm.g));
			float2 appendResult7 = (float2(ase_grabScreenPosNorm.r , ase_grabScreenPosNorm.g));
			float2 temp_output_655_0 = (ase_worldPos).xz;
			float screenDepth205 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth205 = abs( ( screenDepth205 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _EdgeBlend ) );
			float switchResult208 = (((i.ASEVFace>0)?(( ase_worldViewDir.y * distanceDepth205 )):(( distanceDepth205 * -ase_worldViewDir.y ))));
			float EdgeOpacity210 = saturate( switchResult208 );
			float2 DistortionUVs589 = ( ( ( 0.5 * ( (UnpackNormal( tex2D( _DistortionTexture, ( ( temp_output_655_0 * (_DistTilingMap1XYMap2ZW).xy ) + ( (_DistSpeedMap1XYMap2ZW).xy * _Time.y ) ) ) )).xy + (UnpackNormal( tex2D( _DistortionTexture, ( ( temp_output_655_0 * (_DistTilingMap1XYMap2ZW).zz ) + ( _Time.y * (_DistSpeedMap1XYMap2ZW).zw ) ) ) )).xy ) ) * 0.1 * _Distortion ) * EdgeOpacity210 );
			#ifdef _ENABLEDISTORTION_ON
				float2 staticSwitch694 = ( appendResult7 - DistortionUVs589 );
			#else
				float2 staticSwitch694 = appendResult7;
			#endif
			float2 ScreenUVsWithDistortion17 = staticSwitch694;
			float eyeDepth411 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, float4( ScreenUVsWithDistortion17, 0.0 , 0.0 ).xy ));
			float2 lerpResult424 = lerp( appendResult419 , ScreenUVsWithDistortion17 , step( ase_screenPos.w , eyeDepth411 ));
			float2 RefractionsUVs420 = lerpResult424;
			float4 screenColor155 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_GrabTexture,RefractionsUVs420);
			float4 Refraction379 = screenColor155;
			float eyeDepth426 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, float4( RefractionsUVs420, 0.0 , 0.0 ).xy ));
			float DepthOfDist435 = saturate( ( WaterDepth431 / abs( ( eyeDepth426 - ase_screenPos.w ) ) ) );
			float4 lerpResult152 = lerp( _DeepWater , _ShallowWater , DepthOfDist435);
			float4 DepthColor373 = lerpResult152;
			float4 lerpResult401 = lerp( Refraction379 , DepthColor373 , (DepthColor373).a);
			float screenDepth344 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth344 = abs( ( screenDepth344 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( ( ( sin( ( _Time.y * _FoamSpeed ) ) * _FoamDepthOffset ) + _FoamDepth ) ) );
			float switchResult348 = (((i.ASEVFace>0)?(( ase_worldViewDir.y * distanceDepth344 )):(( distanceDepth344 * -ase_worldViewDir.y ))));
			float FoamLine297 = ( 1.0 - saturate( ( _FoamWidth * saturate( switchResult348 ) ) ) );
			#ifdef _ENABLEFOAM_ON
				float staticSwitch312 = FoamLine297;
			#else
				float staticSwitch312 = 0.0;
			#endif
			float4 lerpResult596 = lerp( Refraction379 , saturate( ( lerpResult401 + staticSwitch312 ) ) , EdgeOpacity210);
			float4 FinalColor157 = lerpResult596;
			o.Emission = FinalColor157.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			o.Alpha = EdgeOpacity210;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "Water_MaterialInspector"
}
/*ASEBEGIN
Version=18500
386.2857;374.2857;1326;647;10152.26;1602.857;9.927353;True;False
Node;AmplifyShaderEditor.CommentaryNode;580;-2300.669,765.8984;Inherit;False;2686.524;1024.847;;28;589;595;594;586;587;588;671;672;670;581;669;659;566;577;668;663;576;667;579;658;578;666;660;665;655;656;657;664;Distortion;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector4Node;657;-2172.442,1116.628;Inherit;False;Property;_DistTilingMap1XYMap2ZW;Dist Tiling Map1 (XY), Map2 (ZW);12;0;Create;True;0;0;False;0;False;1,1,1,1;0.2,0.2,0.4,0.4;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;664;-2225.406,1400.2;Inherit;False;Property;_DistSpeedMap1XYMap2ZW;Dist Speed Map1 (XY), Map2 (ZW);13;0;Create;True;0;0;False;0;False;0.2,-0.3,-0.1,0.25;0.1,-0.1,-0.2,0.1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldPosInputsNode;656;-2074.191,936.6353;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SwizzleNode;665;-1808.594,1315.719;Inherit;False;FLOAT2;0;1;2;3;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SwizzleNode;658;-1842.117,1085.767;Inherit;False;FLOAT2;0;1;2;3;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SwizzleNode;666;-1814.328,1571.365;Inherit;False;FLOAT2;2;3;2;3;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TimeNode;578;-1857.443,1413.05;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SwizzleNode;660;-1837.822,1207.774;Inherit;False;FLOAT2;2;2;2;3;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;79;-3836.725,-897.4833;Inherit;False;1657.258;1402.116;;30;77;191;190;185;187;435;189;186;184;436;188;430;434;433;429;431;426;428;78;427;210;209;208;207;206;204;203;205;202;201;Depth and Edge Blend;1,1,1,1;0;0
Node;AmplifyShaderEditor.SwizzleNode;655;-1837.59,930.1487;Inherit;False;FLOAT2;0;2;2;3;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;202;-3699.302,-134.3485;Inherit;False;Property;_EdgeBlend;Edge Blend;3;0;Create;True;0;0;False;0;False;-0.04;-0.09;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;201;-3610.105,-36.28735;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;576;-1593.533,987.5596;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;663;-1597.401,1148.121;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;667;-1588.804,1499.462;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;579;-1590.995,1345.81;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;577;-1319.063,1026.063;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NegateNode;204;-3389.095,-4.941395;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;668;-1329.805,1457.883;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DepthFade;205;-3502.969,-152.4863;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;203;-3442.478,-327.7327;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;206;-3198.571,-236.2477;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;207;-3199.473,-94.68322;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;659;-1167.683,1427.116;Inherit;True;Property;_TextureSample1;Texture Sample 1;11;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;True;Instance;566;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;566;-1169.905,995.4453;Inherit;True;Property;_DistortionTexture;Distortion Texture;11;1;[NoScaleOffset];Create;True;0;0;False;0;False;-1;None;f245fb356e6caa846aaf27ada7c2d5aa;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SwizzleNode;581;-830.3309,996.6409;Inherit;False;FLOAT2;0;1;2;3;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SwizzleNode;669;-808.4917,1427.616;Inherit;False;FLOAT2;0;1;2;3;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SwitchByFaceNode;208;-3020.371,-182.1096;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;209;-2791.241,-183.9207;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;672;-600.8776,1115.656;Inherit;False;Constant;_Float2;Float 2;24;0;Create;True;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;670;-562.0397,1232.13;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;671;-420.7457,1180.836;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;587;-436.3546,1295.383;Inherit;False;Constant;_Float8;Float 8;33;0;Create;True;0;0;False;0;False;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;588;-440.9485,1378.398;Inherit;False;Property;_Distortion;Distortion;14;0;Create;True;0;0;False;0;False;0.3;0.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;210;-2608.916,-190.5239;Inherit;False;EdgeOpacity;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;586;-220.6653,1279.283;Inherit;False;3;3;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;594;-272.5893,1422.76;Inherit;False;210;EdgeOpacity;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;12;-1021.417,-901.2461;Inherit;False;1398.786;379.4877;;6;17;694;591;6;7;4;Screen UVs With Distortion;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;595;-40.5887,1337.76;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;589;155.1821,1331.749;Inherit;False;DistortionUVs;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GrabScreenPosition;4;-982.2758,-844.0997;Inherit;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;7;-694.7333,-816.1997;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;591;-760.3867,-671.0944;Inherit;False;589;DistortionUVs;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;6;-498.5373,-735.6583;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StaticSwitch;694;-278.384,-822.4406;Inherit;False;Property;_EnableDistortion;EnableDistortion;20;0;Create;True;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;17;32.01684,-825.3582;Inherit;False;ScreenUVsWithDistortion;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;496;-900.7973,-264.3154;Inherit;False;1282.885;770.2797;Comment;12;379;155;421;420;424;419;417;423;414;411;418;412;Refraction;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;412;-852.7549,211.3831;Inherit;False;17;ScreenUVsWithDistortion;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GrabScreenPosition;418;-586.3085,-216.2154;Inherit;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenDepthNode;411;-537.7745,212.423;Inherit;False;0;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;414;-531.8907,35.39168;Float;False;1;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;311;-2301.911,2048.436;Inherit;False;2678.793;636.4862;;22;297;296;294;295;292;300;349;348;346;347;345;343;344;342;351;309;341;304;308;307;306;305;Foam;1,1,1,1;0;0
Node;AmplifyShaderEditor.DynamicAppendNode;419;-301.308,-187.2154;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;417;-440.0559,-44.1822;Inherit;False;17;ScreenUVsWithDistortion;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StepOpNode;423;-294.056,154.777;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;306;-2190.914,2363.035;Inherit;False;Property;_FoamSpeed;Foam Speed;16;0;Create;True;0;0;False;0;False;2;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;305;-2219.914,2210.035;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;424;-74.65604,-65.3231;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;307;-1991.913,2276.036;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;420;96.63594,-69.283;Inherit;False;RefractionsUVs;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;427;-3775.575,196.166;Inherit;False;420;RefractionsUVs;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;308;-1915.913,2400.035;Inherit;False;Property;_FoamDepthOffset;Foam Depth Offset;19;0;Create;True;0;0;False;0;False;0.3;0.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;304;-1839.913,2276.036;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;309;-1679.914,2316.035;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;78;-3789.78,-644.3219;Inherit;False;Property;_WaterDepth;Water Depth;2;0;Create;True;0;0;False;0;False;1;1.15;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;341;-1713.309,2495.117;Inherit;False;Property;_FoamDepth;Foam Depth;18;0;Create;True;0;0;False;0;False;2.45;2.45;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;428;-3513.269,287.8766;Float;False;1;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenDepthNode;426;-3524.282,196.289;Inherit;False;0;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;342;-1422.858,2504.744;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleSubtractOpNode;429;-3274.408,286.1125;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;351;-1506.44,2400.697;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;431;-3593.914,-645.2111;Inherit;False;WaterDepth;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;434;-3099.08,284.4487;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;433;-3177.08,184.4487;Inherit;False;431;WaterDepth;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;345;-1258.913,2205.933;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DepthFade;344;-1320.406,2381.18;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode;343;-1203.532,2517.724;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;430;-2942.408,227.1128;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;347;-1015.91,2438.983;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;346;-1016.008,2301.42;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SwitchByFaceNode;348;-836.8073,2351.557;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;436;-2785.346,228.7717;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;11;-5372.547,769.8353;Inherit;False;2811.435;1019.11;;31;50;89;90;563;46;48;2;3;10;560;564;91;45;92;646;639;653;654;651;643;652;650;642;644;648;647;649;645;641;43;33;Normals;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;300;-622.8085,2240.116;Inherit;False;Property;_FoamWidth;Foam Width;17;0;Create;True;0;0;False;0;False;19;23;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;498;-1929.597,5.550147;Inherit;False;770.6675;505.226;Comment;5;152;373;146;143;144;Depth Color;1,1,1,1;0;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;188;-3452.97,-544.0356;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RegisterLocalVarNode;435;-2614.026,223.2268;Inherit;False;DepthOfDist;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;349;-598.6774,2349.746;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;646;-5313.405,1423.374;Inherit;False;Property;_SpeedMap1XYMap2ZW;Speed Map1 (XY), Map2 (ZW);8;0;Create;True;0;0;False;0;False;8,6,-10,8;1.5,1,-1.2,1.5;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;639;-5287.804,1046.638;Inherit;False;Property;_TilingMap1XYMap2ZW;Tiling Map1 (XY), Map2 (ZW);7;0;Create;True;0;0;False;0;False;0.1,0.1,0.1,0.1;0.28,0.28,0.2,0.2;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;146;-1833.189,395.776;Inherit;False;435;DepthOfDist;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;143;-1878.312,226.6081;Inherit;False;Property;_ShallowWater;Shallow Water;0;1;[HDR];Create;True;0;0;False;0;False;0.6862745,0.7843137,0.8196079,1;0.3686274,0.7330523,0.7450981,0.4823529;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;144;-1879.598,55.55003;Inherit;False;Property;_DeepWater;Deep Water;1;1;[HDR];Create;True;0;0;False;0;False;0.3607843,0.5137255,0.6313726,1;0,0.4517202,0.737255,0.8392157;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldPosInputsNode;641;-5108.376,882.5635;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;292;-417.6246,2278.264;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode;189;-3250.961,-497.6907;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;184;-3356.836,-660.234;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;186;-3296.342,-835.4813;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.GetLocalVarNode;421;-370.1578,315.0353;Inherit;False;420;RefractionsUVs;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;187;-3053.339,-602.432;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;295;-248.6669,2164.572;Inherit;False;Constant;_Float0;Float 0;21;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;649;-4957.659,1460.715;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;294;-248.6669,2280.572;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;647;-4894.362,1331.285;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;644;-4877.709,1029.496;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SwizzleNode;642;-4892.775,876.0772;Inherit;False;FLOAT2;0;2;2;3;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;645;-4874.459,1147.591;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;648;-4893.362,1636.285;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;152;-1569.257,206.0361;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;185;-3052.436,-743.994;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;373;-1369.431,206.2581;Inherit;False;DepthColor;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;296;-53.66693,2203.572;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;651;-4670.092,1545.017;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;652;-4665.125,1075.134;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;643;-4663.482,940.7701;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;142;641.5489,775.5737;Inherit;False;1545.085;510.5074;;14;692;157;596;408;597;298;402;401;392;400;502;312;299;315;Final Water Color;1,1,1,1;0;0
Node;AmplifyShaderEditor.ScreenColorNode;155;-126.882,315.8194;Inherit;False;Global;_GrabScreen0;Grab Screen 0;0;0;Create;True;0;0;False;0;False;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;650;-4663.638,1381.604;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SwitchByFaceNode;190;-2874.237,-689.8571;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;191;-2636.106,-691.6682;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;402;688.1456,1026.399;Inherit;False;373;DepthColor;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;297;131.6831,2204.367;Inherit;False;FoamLine;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;379;83.52495,318.4964;Inherit;False;Refraction;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;654;-4410.65,1380.727;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;653;-4409.15,1101.324;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;43;-4228.993,1373.198;Inherit;False;UV2;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;392;877.9462,929.6987;Inherit;False;373;DepthColor;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;33;-4235.014,1098.987;Inherit;False;UV1;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;315;898.2469,1120.391;Inherit;False;Constant;_Float1;Float 1;24;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;77;-2454.781,-692.351;Inherit;False;DepthFade;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;299;863.2358,1206.121;Inherit;False;297;FoamLine;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;400;877.9462,833.6989;Inherit;False;379;Refraction;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SwizzleNode;502;912.4725,1027.567;Inherit;False;FLOAT;3;1;2;3;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;312;1066.905,1144.076;Inherit;False;Property;_EnableFoam;Enable Foam;15;0;Create;True;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;46;-3802.273,1347.949;Inherit;False;43;UV2;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;401;1142.946,912.6986;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;92;-3912.015,1245.767;Inherit;False;77;DepthFade;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;45;-3841.423,1048.035;Inherit;False;33;UV1;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;48;-3906.771,1165.924;Inherit;False;Property;_Map1Strength;Map1 Strength;9;0;Create;True;0;0;False;0;False;0.3;0.8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;50;-3825.747,1424.074;Inherit;False;Property;_Map2Strength;Map2 Strength;10;0;Create;True;0;0;False;0;False;0.2;0.6;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;90;-3833.903,1505.369;Inherit;False;77;DepthFade;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode;564;-3643.993,1069.233;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NegateNode;563;-3611.495,1354.511;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;91;-3673.692,1197.278;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;89;-3614.619,1458.717;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;298;1379.528,1027.06;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;408;1526.182,1027.449;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;2;-3440.573,1089.895;Inherit;True;Property;_NormalMap;Normal Map;6;1;[NoScaleOffset];Create;True;0;0;False;0;False;-1;None;f245fb356e6caa846aaf27ada7c2d5aa;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;692;1489.639,918.2155;Inherit;False;379;Refraction;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;597;1460.799,1125.995;Inherit;False;210;EdgeOpacity;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;3;-3436.724,1363.988;Inherit;True;Property;_TextureSample0;Texture Sample 0;6;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;True;Instance;2;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;596;1746.932,969.3342;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendNormalsNode;560;-3079.958,1233.263;Inherit;False;0;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;10;-2844.893,1227.726;Inherit;False;Normals;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;359;2573.932,711.441;Inherit;False;755.0811;634.8221;;6;212;194;690;193;0;100;Master Node;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;157;1950.792,964.1696;Inherit;False;FinalColor;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;212;2686.899,1144.987;Inherit;False;210;EdgeOpacity;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;193;2698.218,783.5743;Inherit;False;10;Normals;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;690;2609.979,964.026;Inherit;False;Property;_Metallic;Metallic;4;0;Create;True;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;194;2602.219,1057.726;Inherit;False;Property;_Smoothness;Smoothness;5;0;Create;True;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;100;2683.956,873.6589;Inherit;False;157;FinalColor;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;3000.807,838.1094;Float;False;True;-1;2;Water_MaterialInspector;0;0;Standard;Nicrom/NHP/ASE/Water;False;False;False;False;False;False;True;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;1;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;665;0;664;0
WireConnection;658;0;657;0
WireConnection;666;0;664;0
WireConnection;660;0;657;0
WireConnection;655;0;656;0
WireConnection;576;0;655;0
WireConnection;576;1;658;0
WireConnection;663;0;655;0
WireConnection;663;1;660;0
WireConnection;667;0;578;2
WireConnection;667;1;666;0
WireConnection;579;0;665;0
WireConnection;579;1;578;2
WireConnection;577;0;576;0
WireConnection;577;1;579;0
WireConnection;204;0;201;2
WireConnection;668;0;663;0
WireConnection;668;1;667;0
WireConnection;205;0;202;0
WireConnection;206;0;203;2
WireConnection;206;1;205;0
WireConnection;207;0;205;0
WireConnection;207;1;204;0
WireConnection;659;1;668;0
WireConnection;566;1;577;0
WireConnection;581;0;566;0
WireConnection;669;0;659;0
WireConnection;208;0;206;0
WireConnection;208;1;207;0
WireConnection;209;0;208;0
WireConnection;670;0;581;0
WireConnection;670;1;669;0
WireConnection;671;0;672;0
WireConnection;671;1;670;0
WireConnection;210;0;209;0
WireConnection;586;0;671;0
WireConnection;586;1;587;0
WireConnection;586;2;588;0
WireConnection;595;0;586;0
WireConnection;595;1;594;0
WireConnection;589;0;595;0
WireConnection;7;0;4;1
WireConnection;7;1;4;2
WireConnection;6;0;7;0
WireConnection;6;1;591;0
WireConnection;694;1;7;0
WireConnection;694;0;6;0
WireConnection;17;0;694;0
WireConnection;411;0;412;0
WireConnection;419;0;418;1
WireConnection;419;1;418;2
WireConnection;423;0;414;4
WireConnection;423;1;411;0
WireConnection;424;0;419;0
WireConnection;424;1;417;0
WireConnection;424;2;423;0
WireConnection;307;0;305;2
WireConnection;307;1;306;0
WireConnection;420;0;424;0
WireConnection;304;0;307;0
WireConnection;309;0;304;0
WireConnection;309;1;308;0
WireConnection;426;0;427;0
WireConnection;429;0;426;0
WireConnection;429;1;428;4
WireConnection;351;0;309;0
WireConnection;351;1;341;0
WireConnection;431;0;78;0
WireConnection;434;0;429;0
WireConnection;344;0;351;0
WireConnection;343;0;342;2
WireConnection;430;0;433;0
WireConnection;430;1;434;0
WireConnection;347;0;344;0
WireConnection;347;1;343;0
WireConnection;346;0;345;2
WireConnection;346;1;344;0
WireConnection;348;0;346;0
WireConnection;348;1;347;0
WireConnection;436;0;430;0
WireConnection;435;0;436;0
WireConnection;349;0;348;0
WireConnection;292;0;300;0
WireConnection;292;1;349;0
WireConnection;189;0;188;2
WireConnection;184;0;431;0
WireConnection;187;0;184;0
WireConnection;187;1;189;0
WireConnection;294;0;292;0
WireConnection;647;0;646;1
WireConnection;647;1;646;2
WireConnection;644;0;639;1
WireConnection;644;1;639;2
WireConnection;642;0;641;0
WireConnection;645;0;639;3
WireConnection;645;1;639;4
WireConnection;648;0;646;3
WireConnection;648;1;646;4
WireConnection;152;0;144;0
WireConnection;152;1;143;0
WireConnection;152;2;146;0
WireConnection;185;0;186;2
WireConnection;185;1;184;0
WireConnection;373;0;152;0
WireConnection;296;0;295;0
WireConnection;296;1;294;0
WireConnection;651;0;649;1
WireConnection;651;1;648;0
WireConnection;652;0;642;0
WireConnection;652;1;645;0
WireConnection;643;0;642;0
WireConnection;643;1;644;0
WireConnection;155;0;421;0
WireConnection;650;0;647;0
WireConnection;650;1;649;1
WireConnection;190;0;185;0
WireConnection;190;1;187;0
WireConnection;191;0;190;0
WireConnection;297;0;296;0
WireConnection;379;0;155;0
WireConnection;654;0;652;0
WireConnection;654;1;651;0
WireConnection;653;0;643;0
WireConnection;653;1;650;0
WireConnection;43;0;654;0
WireConnection;33;0;653;0
WireConnection;77;0;191;0
WireConnection;502;0;402;0
WireConnection;312;1;315;0
WireConnection;312;0;299;0
WireConnection;401;0;400;0
WireConnection;401;1;392;0
WireConnection;401;2;502;0
WireConnection;564;0;45;0
WireConnection;563;0;46;0
WireConnection;91;0;48;0
WireConnection;91;1;92;0
WireConnection;89;0;50;0
WireConnection;89;1;90;0
WireConnection;298;0;401;0
WireConnection;298;1;312;0
WireConnection;408;0;298;0
WireConnection;2;1;564;0
WireConnection;2;5;91;0
WireConnection;3;1;563;0
WireConnection;3;5;89;0
WireConnection;596;0;692;0
WireConnection;596;1;408;0
WireConnection;596;2;597;0
WireConnection;560;0;2;0
WireConnection;560;1;3;0
WireConnection;10;0;560;0
WireConnection;157;0;596;0
WireConnection;0;1;193;0
WireConnection;0;2;100;0
WireConnection;0;3;690;0
WireConnection;0;4;194;0
WireConnection;0;9;212;0
ASEEND*/
//CHKSM=E5B3E8C60146F17C0BB46C28FAC496F3F643EEEF