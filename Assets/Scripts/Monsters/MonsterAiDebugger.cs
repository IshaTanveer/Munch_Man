using System.Collections.Generic;
using UnityEngine;

public class MonsterAiDebugger : MonoBehaviour
{
    MonsterController monsterController;
    List<Vector2> ProjectedPaths = new();
    [SerializeField] GameObject pathPrefab;
    List<GameObject> VisualizedPaths = new();
    void Start()
    {
        monsterController = GetComponent<MonsterController>();
    }
    void Update()
    {
        CleanUp();
        UpdatePathProjections();
        updatePathVisualization();
    }
    void CleanUp()
    {
        ProjectedPaths.Clear();
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
            currentTarget.currentTargetpos = MonsterAINavigation.GetNextInterMediateTarget(currentTarget.currentDirection, currentTarget.currentTargetpos, finalTargetpos).resultPos;
            ProjectedPaths.Add(currentTarget.currentTargetpos);
            count--;
        }
    }
    void updatePathVisualization()
    {
        foreach (Vector2 path in ProjectedPaths)
        {
            GameObject VisualizedPath = Instantiate(pathPrefab, path, Quaternion.identity);
            VisualizedPaths.Add(VisualizedPath);
        }
    }
}
