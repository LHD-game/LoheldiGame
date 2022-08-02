using UnityEditor;
using UnityEngine;

namespace UnityEditor {
    public class Water_MaterialInspector : ShaderGUI {

        MaterialProperty metallic = null;
        MaterialProperty smoothness = null;

        MaterialProperty shallowWater = null;
        MaterialProperty deepWater = null;
        MaterialProperty waterDepth = null;
        MaterialProperty edgeBlend = null;

        MaterialProperty normalMap = null;
        MaterialProperty normalMapTiling = null;
        MaterialProperty normalMapPanningSpeed = null;
        MaterialProperty normalMap1Strength = null;
        MaterialProperty normalMap2Strength = null;

        MaterialProperty distortionToggle = null;
        MaterialProperty distortionTexture = null;
        MaterialProperty distortionTexTiling = null;
        MaterialProperty distortionTexPanningSpeed = null;
        MaterialProperty distortion = null;

        MaterialProperty foamToggle = null;
        MaterialProperty foamSpeed = null;
        MaterialProperty foamWidth = null;
        MaterialProperty foamDepth = null;
        MaterialProperty foamDepthOffset = null;


        MaterialEditor matEditor;

        public void FindProperties(MaterialProperty[] mProps)
        {
            shallowWater                        = FindProperty("_ShallowWater", mProps);
            deepWater                           = FindProperty("_DeepWater", mProps);
            waterDepth                          = FindProperty("_WaterDepth", mProps);
            edgeBlend                           = FindProperty("_EdgeBlend", mProps);

            metallic                            = FindProperty("_Metallic", mProps);
            smoothness                          = FindProperty("_Smoothness", mProps);

            normalMap                           = FindProperty("_NormalMap", mProps);
            normalMapTiling                     = FindProperty("_TilingMap1XYMap2ZW", mProps);
            normalMapPanningSpeed               = FindProperty("_SpeedMap1XYMap2ZW", mProps);
            normalMap1Strength                  = FindProperty("_Map1Strength", mProps);
            normalMap2Strength                  = FindProperty("_Map2Strength", mProps);

            distortionToggle                    = FindProperty("_EnableDistortion", mProps);
            distortionTexture                   = FindProperty("_DistortionTexture", mProps);
            distortionTexTiling                 = FindProperty("_DistTilingMap1XYMap2ZW", mProps);
            distortionTexPanningSpeed           = FindProperty("_DistSpeedMap1XYMap2ZW", mProps);
            distortion                          = FindProperty("_Distortion", mProps);

            foamToggle                          = FindProperty("_EnableFoam", mProps);
            foamSpeed                           = FindProperty("_FoamSpeed", mProps);
            foamWidth                           = FindProperty("_FoamWidth", mProps);
            foamDepth                           = FindProperty("_FoamDepth", mProps);
            foamDepthOffset                     = FindProperty("_FoamDepthOffset", mProps);

        }

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] mProps)
        {
            matEditor = materialEditor;
            Material material = materialEditor.target as Material;

            FindProperties(mProps);
            ShaderPropertiesGUI(material);
        }

        public void ShaderPropertiesGUI(Material material)
        {
            EditorGUI.BeginChangeCheck();
            {
                EditorGUIUtility.fieldWidth = 64f;

                EditorGUILayout.Separator();
                InspectorBox(10, () =>
                {
                    EditorGUILayout.LabelField(new GUIContent("Color"), EditorStyles.boldLabel);

                    GUILayout.Space(5);
                    matEditor.ShaderProperty(shallowWater, new GUIContent("Shallow Water"));
                    matEditor.ShaderProperty(deepWater, new GUIContent("Deep Water"));

                    GUILayout.Space(5);
                    matEditor.ShaderProperty(waterDepth, new GUIContent("Water Depth"));
                    matEditor.ShaderProperty(edgeBlend, new GUIContent("Edge Blend"));

                    GUILayout.Space(5);
                    matEditor.ShaderProperty(metallic, "Metallic");
                    matEditor.ShaderProperty(smoothness, "Smoothness");
                });

                EditorGUILayout.Separator();
                InspectorBox(10, () =>
                {
                    EditorGUILayout.LabelField(new GUIContent("Normals"), EditorStyles.boldLabel);

                    GUILayout.Space(5);
                    matEditor.TexturePropertySingleLine(new GUIContent("Normal Map"), normalMap);
                    matEditor.ShaderProperty(normalMapTiling, new GUIContent("Tiling Map1 (XY), Map2 (ZW)"));
                    matEditor.ShaderProperty(normalMapPanningSpeed, new GUIContent("Speed Map1 (XY), Map2 (ZW)"));
                    matEditor.ShaderProperty(normalMap1Strength, new GUIContent("Map1 Strength"));
                    matEditor.ShaderProperty(normalMap2Strength, new GUIContent("Map2 Strength"));

                });

                EditorGUILayout.Separator();
                InspectorBox(10, () =>
                {
                    EditorGUILayout.LabelField(new GUIContent("Distortion"), EditorStyles.boldLabel);
                    matEditor.ShaderProperty(distortionToggle, new GUIContent("Enable"));

                    if (distortionToggle.floatValue == 1)
                    {
                        GUILayout.Space(5);
                        matEditor.TexturePropertySingleLine(new GUIContent("Distortion Map"), distortionTexture);
                        matEditor.ShaderProperty(distortionTexTiling, new GUIContent("Tiling Map1 (XY), Map2 (ZW)"));
                        matEditor.ShaderProperty(distortionTexPanningSpeed, new GUIContent("Speed Map1 (XY), Map2 (ZW)"));
                        matEditor.ShaderProperty(distortion, new GUIContent("Distortion"));
                    }
                });

                EditorGUILayout.Separator();
                InspectorBox(10, () =>
                {
                    EditorGUILayout.LabelField(new GUIContent("Foam"), EditorStyles.boldLabel);
                    matEditor.ShaderProperty(foamToggle, new GUIContent("Enable"));

                    if (foamToggle.floatValue == 1)
                    {
                        GUILayout.Space(5);
                        matEditor.ShaderProperty(foamSpeed, new GUIContent("Foam Speed"));
                        matEditor.ShaderProperty(foamWidth, new GUIContent("Foam Width"));
                        matEditor.ShaderProperty(foamDepth, new GUIContent("Foam Depth"));
                        matEditor.ShaderProperty(foamDepthOffset, new GUIContent("Foam Depth Offset"));
                    }
                });


                EditorGUILayout.Separator();
                InspectorBox(10, () =>
                {
                    GUILayout.Label("Advanced Options", EditorStyles.boldLabel);

                    matEditor.EnableInstancingField();
                });
            }
        }

        public void InspectorBox(int aBorder, System.Action inside)
        {
            Rect r = EditorGUILayout.BeginHorizontal();

            GUI.Box(r, GUIContent.none);
            GUILayout.Space(aBorder);
            EditorGUILayout.BeginVertical();
            GUILayout.Space(aBorder);
            inside();
            GUILayout.Space(aBorder);
            EditorGUILayout.EndVertical();
            GUILayout.Space(aBorder);
            EditorGUILayout.EndHorizontal();
        }
    }
}
