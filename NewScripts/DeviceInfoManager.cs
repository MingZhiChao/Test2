using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �豸�����������
/// </summary>
public class DeviceInfoManager : BaseModule
{
    [Tooltip("������֪")]
    [SerializeField]
    public List<BaseObjectParent> m_WholeCognitionDic;

    [SerializeField]
    public List<BaseObjectParent> IncarEquipment;  //�����豸
    [SerializeField]
    public List<BaseObjectParent> OffcarEquipment; //�����豸

    [Tooltip("������֪�豸")]
    [SerializeField]
    public List<Device> m_Devive;
    [SerializeField,Tooltip("ԭ������")]
    public List<SchematicData> m_SchematicData;
    [SerializeField]public List<Transform> LookPoints;


    [SerializeField]List<GameObject> m_Model;
    [SerializeField]private Ground m_Ground;//�����豸����
    private Dictionary<int, ToolItem> DicToolItem;
    public override bool Init()
    {
        //GameEntry.DeviceInfoManager = this;
        DicToolItem = new Dictionary<int, ToolItem>();

        //װ����ֵ
        ToolItem[] toolitems=transform.GetComponentsInChildren<ToolItem>();
        for (int i = 0; i < toolitems.Length; i++)
        {
            ToolItem toolitem = toolitems[i];
            toolitem.Init();
            DicToolItem.Add(toolitem.ID, toolitem);
        }
        return true;
    }

    public void AddToolItem(int id, ToolItem item)//��ӳ���װ��
    {
        if (DicToolItem == null) return;
        if (!DicToolItem.ContainsKey(id) && !DicToolItem.ContainsValue(item)) DicToolItem.Add(id, item);


    }
    public ToolItem GetToolItem(int id)
    {
        if (DicToolItem == null) return null;
        ToolItem toolItem;
        if (DicToolItem.TryGetValue(id, out toolItem))
        {
            return toolItem;
        }
        return toolItem;
    }
    /// <summary>
    /// ������֪�ӽ�װ�� 0,1  ת���  �ܵ繭
    /// </summary>
    /// <param name="index"></param>
    public void TurnToModel(int index)
    {
        if (m_Model == null) return;
        for (int i = 0; i < m_Model.Count; i++)
        {
            m_Model[i].SetActive(false);
        }
        if ((m_Model[index]?.transform.parent.gameObject.activeInHierarchy ?? false) == false)
        {
            m_Model[index]?.transform.parent.gameObject.SetActive(true);
        }
        m_Model[index]?.SetActive(true);

        GameEntry.MainCameraManager.MoveTo(LookPoints[index], 0);
        //m_Ground.gameObject.SetActive(true);
        m_Ground.transform.position = index == 0 ? new Vector3(0, 100.59f, -0.15f): new Vector3(0, 99.003f, -0.15f);
    }
    public Ground Ground
    {
        get {return m_Ground; }
    }
    public override void OnUpdate()
    {
        //throw new System.NotIlementedException();
    }
    public override void Clear()
    {

    }
}
