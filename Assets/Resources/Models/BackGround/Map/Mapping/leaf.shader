Shader "Custom/leaf"
{
    Properties
    {
        _Color("MainColor", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Cutoff("Alpha cutoff",float) = 0.5
        _Move("Move",Range(0,0.5)) = 0.3
        _Timeing("Timeing",Range(0,40)) = 30
    }
        SubShader
        {
            Tags { "RenderType" = "TransparentCutout" "Opaque" = "Alphatest" }
            LOD 200

            CGPROGRAM
            #pragma surface surf Standard fullforwardshadows Lambert alphatest:_Cutoff vertex:vert assshadow

            sampler2D _MainTex;
            float _Move;
            float _Timeing;

            void vert(inout appdata_full v)
            {
                v.vertex.x += sin(_Time.x * _Timeing) * _Move * v.color.r;
            }

            struct Input
            {
                float2 uv_MainTex;
            };


            void surf(Input IN, inout SurfaceOutputStandard o)
            {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
                o.Albedo = c.rgb;
                o.Alpha = c.a;
            }
            ENDCG
        }
            FallBack "Legacy Shaders/Transparent/Cutout/VertexLit"
}