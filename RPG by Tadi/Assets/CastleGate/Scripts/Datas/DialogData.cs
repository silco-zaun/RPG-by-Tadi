
using System.Collections.Generic;
using Tadi.Datas.Scene;

namespace Tadi.Datas.Unit
{
    public static class DialogData
    {
        public static Dictionary<(SceneList, int), Dialog> Dialogs { get; private set; } = new Dictionary<(SceneList, int), Dialog>();
        public static Dictionary<(SceneList, UnitType), Dialog> UnitDialogs { get; private set; } = new Dictionary<(SceneList, UnitType), Dialog>();

        static DialogData()
        {
            Init();
        }

        public static void Init()
        {
            // UnitDialogs
            Dialog dialog = new Dialog();

            dialog.AddLine(UnitData.LizardMan.Name, "용왕님! 용왕님! 어디 계십니까?");
            dialog.AddLine(UnitData.LizardMan.Name, "얼마전부터 용왕님이 보이지 않고 있습니다.");
            dialog.AddLine(UnitData.LizardMan.Name, "동쪽으로 가는걸 본게 마지막 인데..");
            dialog.AddLine(UnitData.LizardMan.Name, "혹시 용왕님을 찾아주실수 있나요?");

            UnitDialogs.Add((SceneList.Village, UnitType.LizardMan), dialog);

            dialog = new Dialog();

            dialog.AddLine(UnitData.TurtleKing.Name, "용사여! 구해줘서 고맙네.");
            dialog.AddLine(UnitData.TurtleKing.Name, "혹시 바다에서 위기에 처했을때 나를 부르게.");
            dialog.AddLine(UnitData.TurtleKing.Name, "나의 추종자들이 그대를 도울 것이네.");
            dialog.AddLine(UnitData.TurtleKing.Name, "그럼 무운을 비네!");

            UnitDialogs.Add((SceneList.EsternDionisDungeon, UnitType.TurtleKing), dialog);

            // Dialogs
            dialog = new Dialog();
            
            dialog.AddLine("누군가의 목소리", "이보게! 나를 좀 구해줄수 있겠나?");
            dialog.AddLine("누군가의 목소리", "안쪽으로 들어가면 스켈레톤이 있어.");
            dialog.AddLine("누군가의 목소리", "그를 처치하면 감옥문이 열릴걸세.");

            Dialogs.Add((SceneList.EsternDionisDungeon, 0), dialog);

        }
    }
}
