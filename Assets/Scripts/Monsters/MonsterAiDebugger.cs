using System.Collections.Generic;
using UnityEngine;

public class MonsterAiDebugger : MonoBehaviour
{
    MonsterController monsterController;
    List<(Vector2 dir, Vector2 path)> Paths = new();
    [SerializeField] GameObject pathPrefab;
    List<GameObject> VisualizedPaths = new();
    Transform debugParent;
    void Start()
    {
        monsterController = GetComponent<MonsterController>();
        debugParent = GameObject.FindGameObjectWithTag("Debug").transform;
    }
    void Update()
    {
        CleanUp();
        UpdatePathProjections();
        updatePathVisualization();
    }
    void CleanUp()
    {
        Paths.Clear();
        foreach (GameObject path in VisualizedPaths)
        {
            Destroy(path);
        }
        VisualizedPaths.Clear();
    }
    void UpdatePathProjections()
    {
        Vector2 finalTargetpos = monsterController.FinalTarget;
        (Vector2 currentDirection, Vector2 currentTargetpos) currentTarget = (monsterController.CurrentDirection, monsterController.CurrentTarget);

        int count = 100;
        while (finalTargetpos != currentTarget.currentTargetpos && count > 0)
        {
            currentTarget = MonsterAINavigation.GetNextInterMediateTarget(currentTarget.currentDirection, currentTarget.currentTargetpos, finalTargetpos);
            Paths.Add(currentTarget);
            count--;
        }
    }
    void updatePathVisualization()
    {
        foreach ((Vector2 dir, Vector2 pos) path in Paths)
        {
            GameObject VisualizedPath = Instantiate(pathPrefab, path.pos, Quaternion.identity);
            VisualizedPath.transform.parent = debugParent;
            VisualizedPaths.Add(VisualizedPath);
            VisualizedPath.GetComponent<PathVisuallizerSetup>().SetUp(monsterController.MonsterConf.MonsterColor, path.dir);
        }
    }
}
