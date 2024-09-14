using UnityEditor;

public static class CreateScriptTemplates
{
    [MenuItem("Assets/Create/DOTS Code/ISystem", priority = 40)]
    public static void CreateISystem()
    {
        string templatePath = "Assets/Editor/ISystem.cs.txt";
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, "System.cs");
    }

    [MenuItem("Assets/Create/DOTS Code/Component", priority = 40)]
    public static void CreateComponent()
    {
        string templatePath = "Assets/Editor/Component.cs.txt";
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, "Component.cs");
    }

    [MenuItem("Assets/Create/DOTS Code/Authoring", priority = 40)]
    public static void CreateAuthoring()
    {
        string templatePath = "Assets/Editor/Authoring.cs.txt";
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, "Authoring.cs");
    }
}
