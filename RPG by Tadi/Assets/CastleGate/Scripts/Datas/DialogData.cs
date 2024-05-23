
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

            dialog.AddLine(UnitData.LizardMan.Name, "��մ�! ��մ�! ��� ��ʴϱ�?");
            dialog.AddLine(UnitData.LizardMan.Name, "�������� ��մ��� ������ �ʰ� �ֽ��ϴ�.");
            dialog.AddLine(UnitData.LizardMan.Name, "�������� ���°� ���� ������ �ε�..");
            dialog.AddLine(UnitData.LizardMan.Name, "Ȥ�� ��մ��� ã���ֽǼ� �ֳ���?");

            UnitDialogs.Add((SceneList.Village, UnitType.LizardMan), dialog);

            dialog = new Dialog();

            dialog.AddLine(UnitData.TurtleKing.Name, "��翩! �����༭ ������.");
            dialog.AddLine(UnitData.TurtleKing.Name, "Ȥ�� �ٴٿ��� ���⿡ ó������ ���� �θ���.");
            dialog.AddLine(UnitData.TurtleKing.Name, "���� �����ڵ��� �״븦 ���� ���̳�.");
            dialog.AddLine(UnitData.TurtleKing.Name, "�׷� ������ ���!");

            UnitDialogs.Add((SceneList.EsternDionisDungeon, UnitType.TurtleKing), dialog);

            // Dialogs
            dialog = new Dialog();
            
            dialog.AddLine("�������� ��Ҹ�", "�̺���! ���� �� �����ټ� �ְڳ�?");
            dialog.AddLine("�������� ��Ҹ�", "�������� ���� ���̷����� �־�.");
            dialog.AddLine("�������� ��Ҹ�", "�׸� óġ�ϸ� �������� �����ɼ�.");

            Dialogs.Add((SceneList.EsternDionisDungeon, 0), dialog);

        }
    }
}