Shader "Custom/LerpTexture"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _MainTex2("Albedo (RGB)", 2D) = "white" {}
        _BumpMap("Normalmap",2D) = "bump"{}
        _Metallic("Metallic",Range(0,1)) = 0
        _Smoothness("Smoothness ", Range(0,1)) = 0
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }

            CGPROGRAM
            #pragma surface surf Standard

            sampler2D _MainTex;
            sampler2D _MainTex2;
            sampler2D _BumpMap;
            float _Metallic;
            float _Smoothness;

            struct Input
            {
                float2 uv_MainTex;
                float2 uv_MainTex2;
                float2 uv_BumpMap;
                float4 color:COLOR;

            };
            fixed4 _Color;
            half _LerpValue;

            // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
            // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
            // #pragma instancing_options assumeuniformscaling
            UNITY_INSTANCING_BUFFER_START(Props)
                // put more per-instance properties here
            UNITY_INSTANCING_BUFFER_END(Props)

            void surf(Input IN, inout SurfaceOutputStandard o)
            {
                // Albedo comes from a texture tinted by color
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
                fixed4 d = tex2D(_MainTex2, IN.uv_MainTex2);
                o.Albedo = lerp(c.rgb, d.rgb, IN.color.r);
                o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
                o.Metallic = _Metallic;
                o.Smoothness = _Smoothness;
                o.Alpha = c.a;
            }
            ENDCG
        }
            FallBack "Diffuse"
}