using System.Collections.ObjectModel;

namespace GlobalHotKeyDemo
{
    /// <summary>
    /// 快捷键设置管理器
    /// </summary>
    public class HotKeySettingsManager
    {
        private static HotKeySettingsManager m_Instance;
        /// <summary>
        /// 单例实例
        /// </summary>
        public static HotKeySettingsManager Instance
        {
            get { return m_Instance ?? (m_Instance = new HotKeySettingsManager()); }
        }

        /// <summary>
        /// 加载默认快捷键
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<HotKeyModel> LoadDefaultHotKey()
        {
            var hotKeyList = new ObservableCollection<HotKeyModel>();
            hotKeyList.Add(new HotKeyModel { Name = EHotKeySetting.全屏.ToString(), IsUsable = true, IsSelectCtrl = true, IsSelectAlt = false, IsSelectShift = false, SelectKey = EKey.Q });
            hotKeyList.Add(new HotKeyModel { Name = EHotKeySetting.最小化.ToString(), IsUsable = true, IsSelectCtrl = true, IsSelectAlt = false, IsSelectShift = false, SelectKey = EKey.M });
            hotKeyList.Add(new HotKeyModel { Name = EHotKeySetting.最大化.ToString(), IsUsable = true, IsSelectCtrl = true, IsSelectAlt = false, IsSelectShift = false, SelectKey = EKey.Space });
            hotKeyList.Add(new HotKeyModel { Name = EHotKeySetting.截图.ToString(), IsUsable = true, IsSelectCtrl = true, IsSelectAlt = false, IsSelectShift = false, SelectKey = EKey.J });
            hotKeyList.Add(new HotKeyModel { Name = EHotKeySetting.退出程序.ToString(), IsUsable = true, IsSelectCtrl = true, IsSelectAlt = false, IsSelectShift = false, SelectKey = EKey.E });
            hotKeyList.Add(new HotKeyModel { Name = EHotKeySetting.变量监控.ToString(), IsUsable = true, IsSelectCtrl = true, IsSelectAlt = false, IsSelectShift = false, SelectKey = EKey.R });
            hotKeyList.Add(new HotKeyModel { Name = EHotKeySetting.备用1.ToString(), IsUsable = true, IsSelectCtrl = true, IsSelectAlt = false, IsSelectShift = false, SelectKey = EKey.F1 });
            hotKeyList.Add(new HotKeyModel { Name = EHotKeySetting.备用2.ToString(), IsUsable = true, IsSelectCtrl = true, IsSelectAlt = false, IsSelectShift = false, SelectKey = EKey.F2 });
            hotKeyList.Add(new HotKeyModel { Name = EHotKeySetting.备用3.ToString(), IsUsable = true, IsSelectCtrl = true, IsSelectAlt = false, IsSelectShift = false, SelectKey = EKey.F3 });
            hotKeyList.Add(new HotKeyModel { Name = EHotKeySetting.备用4.ToString(), IsUsable = true, IsSelectCtrl = true, IsSelectAlt = false, IsSelectShift = false, SelectKey = EKey.F4 });
            hotKeyList.Add(new HotKeyModel { Name = EHotKeySetting.备用5.ToString(), IsUsable = true, IsSelectCtrl = true, IsSelectAlt = false, IsSelectShift = false, SelectKey = EKey.F5 });

            return hotKeyList;
        }

        /// <summary>
        /// 通知注册系统快捷键委托
        /// </summary>
        /// <param name="hotKeyModelList"></param>
        public delegate bool RegisterGlobalHotKeyHandler(ObservableCollection<HotKeyModel> hotKeyModelList);
        public event RegisterGlobalHotKeyHandler RegisterGlobalHotKeyEvent;
        public bool RegisterGlobalHotKey(ObservableCollection<HotKeyModel> hotKeyModelList)
        {
            if (RegisterGlobalHotKeyEvent != null)
            {
                return RegisterGlobalHotKeyEvent(hotKeyModelList);
            }
            return false;
        }

    }
}
