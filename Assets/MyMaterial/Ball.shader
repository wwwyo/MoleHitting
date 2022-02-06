Shader "Custom/Ball"
{
    Properties {
        _MainTex("Albedo",2D) = "white"{}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Geometry-2" }
        LOD 200
        ZWrite On

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
