Shader "Custom/Bloom"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _MaskTex("Albedo", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 200

            CGPROGRAM
            #pragma surface surf Standard fullforwardshadows
            #pragma target 3.0

            sampler2D _MainTex;
            sampler2D _MaskTex;
            fixed4 _Color;

            struct Input
            {
                float2 uv_MainTex;
                float2 uv_MaskTex;
            };

            void surf(Input IN, inout SurfaceOutputStandard o)
            {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
                fixed4 m = tex2D(_MaskTex, IN.uv_MaskTex);
                o.Albedo = c.rgb;
                o.Emission = c.rgb * m.r * _Color;
                o.Alpha = c.a;
            }
            ENDCG
        }
            FallBack "Diffuse"
}