Shader "project/WireFrameRendering"
{
    SubShader 
    { 
        Pass 
        {  
            Blend SrcAlpha OneMinusSrcAlpha 
        } 
    } 
}