using System;
using UnityEngine;

namespace Nicrom.NHP
{
	public class UnderwaterFog : MonoBehaviour
	{
		private Shader fogShader = null;
		private Material fogMaterial = null;
		private Camera cam;
		private bool waterObjectDetected = false;
		private bool applyUnderwaterEffects = false;
		private float waterLineYAxisWorldPos = 67.8f;
		private float fogFadeState = 1f;

		public float fogFadeStart = 0.2f;
		public float fogFadeSpeed = 0.5f;

		public Color fogColor = Color.cyan;
		[Range(0.001f, 10.0f)]
		public float fogDensity = 1.0f;
		public float fogStartDistance = 0.0f;
		public float fogStartDistanceOffset = 0.0f;
		public float fogEndDistance = 100.0f;

		void Start()
		{
			Init();
		}

		private void Init()
		{
			if (cam == null)
				cam = GetComponent<Camera>();

			if (fogShader == null)
				fogShader = Shader.Find("Hidden/NHP/UnderwaterFog");

			if (fogMaterial == null)
				fogMaterial = new Material(fogShader);

			if (cam.transform.position.y >= waterLineYAxisWorldPos + fogFadeStart)
				fogColor.a = 0f;
		}

		void Update()
		{
			fogFadeState += Time.deltaTime / fogFadeSpeed;
			fogFadeState = Mathf.Clamp(fogFadeState, 0, fogFadeSpeed);

			if (cam.transform.position.y <= (waterLineYAxisWorldPos + fogFadeStart) && waterObjectDetected)
			{
				applyUnderwaterEffects = true;
				fogColor.a = Mathf.Lerp(fogColor.a, 1f, fogFadeState);
			}
			else
			{
				fogColor.a = Mathf.Lerp(fogColor.a, 0f, fogFadeState * 2f);

				if (fogColor.a <= 0.01f)
					applyUnderwaterEffects = false;
			}
		}

		public bool CheckResources()
		{
			if (fogShader == null)
				fogShader = Shader.Find("Hidden/NHP/UnderwaterFog");

			if (fogMaterial == null)
				fogMaterial = new Material(fogShader);

			bool isSupported = true;

			if (!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
				return false;

			cam.depthTextureMode |= DepthTextureMode.Depth;

			return isSupported;
		}

		[ImageEffectOpaque]
		void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (CheckResources() == false || applyUnderwaterEffects == false)
			{
				Graphics.Blit(source, destination);
				return;
			}

			Camera cam = GetComponent<Camera>();
			Transform camtr = cam.transform;
			float camNear = cam.nearClipPlane;
			float camFar = cam.farClipPlane;
			float camFov = cam.fieldOfView;
			float camAspect = cam.aspect;

			Matrix4x4 frustumCorners = Matrix4x4.identity;

			float fovWHalf = camFov * 0.5f;

			Vector3 toRight = camtr.right * camNear * Mathf.Tan(fovWHalf * Mathf.Deg2Rad) * camAspect;
			Vector3 toTop = camtr.up * camNear * Mathf.Tan(fovWHalf * Mathf.Deg2Rad);

			Vector3 topLeft = (camtr.forward * camNear - toRight + toTop);
			float camScale = topLeft.magnitude * camFar / camNear;

			topLeft.Normalize();
			topLeft *= camScale;

			Vector3 topRight = (camtr.forward * camNear + toRight + toTop);
			topRight.Normalize();
			topRight *= camScale;

			Vector3 bottomRight = (camtr.forward * camNear + toRight - toTop);
			bottomRight.Normalize();
			bottomRight *= camScale;

			Vector3 bottomLeft = (camtr.forward * camNear - toRight - toTop);
			bottomLeft.Normalize();
			bottomLeft *= camScale;

			frustumCorners.SetRow(0, topLeft);
			frustumCorners.SetRow(1, topRight);
			frustumCorners.SetRow(2, bottomRight);
			frustumCorners.SetRow(3, bottomLeft);

			var camPos = camtr.position;
			float FdotC = camPos.y - waterLineYAxisWorldPos;
			float paramK = (FdotC <= 0.0f ? 1.0f : 0.0f);

			fogMaterial.SetColor("_Color", fogColor);
			fogMaterial.SetMatrix("_FrustumCornersWS", frustumCorners);
			fogMaterial.SetVector("_CameraWS", camPos);
			fogMaterial.SetVector("_HeightParams", new Vector4(waterLineYAxisWorldPos, FdotC, paramK, fogDensity * 0.5f));
			fogMaterial.SetVector("_DistanceParams", new Vector4(-Mathf.Max(fogStartDistanceOffset, 0.0f), 1.0f, 0, 0));

			var sceneMode = RenderSettings.fogMode;
			var sceneDensity = fogDensity;//RenderSettings.fogDensity;
			var sceneStart = fogStartDistance;
			var sceneEnd = fogEndDistance;
			Vector4 sceneParams;
			bool linear = true;
			float diff = linear ? sceneEnd - sceneStart : 0.0f;
			float invDiff = Mathf.Abs(diff) > 0.0001f ? 1.0f / diff : 0.0f;
			sceneParams.x = sceneDensity * 1.2011224087f; // density / sqrt(ln(2)), used by Exp2 fog mode
			sceneParams.y = sceneDensity * 1.4426950408f; // density / ln(2), used by Exp fog mode
			sceneParams.z = linear ? -invDiff : 0.0f;
			sceneParams.w = linear ? sceneEnd * invDiff : 0.0f;
			fogMaterial.SetVector("_SceneFogParams", sceneParams);
			fogMaterial.SetVector("_SceneFogMode", new Vector4((int)sceneMode, 0, 0, 0));

			CustomGraphicsBlit(source, destination, fogMaterial, 0);
		}

		static void CustomGraphicsBlit(RenderTexture source, RenderTexture dest, Material fxMaterial, int passNr)
		{
			RenderTexture.active = dest;

			fxMaterial.SetTexture("_MainTex", source);

			GL.PushMatrix();
			GL.LoadOrtho();

			fxMaterial.SetPass(passNr);

			GL.Begin(GL.QUADS);

			GL.MultiTexCoord2(0, 0.0f, 0.0f);
			GL.Vertex3(0.0f, 0.0f, 3.0f); // BL

			GL.MultiTexCoord2(0, 1.0f, 0.0f);
			GL.Vertex3(1.0f, 0.0f, 2.0f); // BR

			GL.MultiTexCoord2(0, 1.0f, 1.0f);
			GL.Vertex3(1.0f, 1.0f, 1.0f); // TR

			GL.MultiTexCoord2(0, 0.0f, 1.0f);
			GL.Vertex3(0.0f, 1.0f, 0.0f); // TL

			GL.End();
			GL.PopMatrix();
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
			{
				waterObjectDetected = true;
				waterLineYAxisWorldPos = other.transform.position.y;
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
			{
				waterObjectDetected = false;
			}
		}
	}
}