using common;
using common.tag;
using Microsoft.Win32;
using MiniExcelLibs;
using Newtonsoft.Json;
using Panuon.UI.Silver;
using R2R.helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace R2R.protocols
{
    /// <summary>
    /// proto.xaml 的交互逻辑
    /// </summary>
    public partial class protocols : Window
    {
        ObservableCollection<protocol_tag> protocol_Tags { get; set; }
        ObservableCollection<protocol_tag> protocol_Tags_temp ;
        string str_tags_temp;
        bool saved = false;
        public static Dictionary<string, int> devices_changed; // 0:non/ 1:add/ 2:update /3:dele 
        public protocols()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            protocol_Tags = communicationTag.Protos_Tags;
            protocol_Tags_temp = new ObservableCollection<protocol_tag>(protocol_Tags);
            str_tags_temp = JsonConvert.SerializeObject(protocol_Tags_temp);
            tree1.DataContext = protocol_Tags;
            devices_changed = new Dictionary<string, int>();
            foreach (var item in communicationTag.Protos_Tags)
            {
                foreach (var dev in item.Devices)
                {
                    devices_changed.Add(dev.DeviceName, 0);
                }
            }
        }
        private void tree1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView tv = (TreeView)sender;
            var t = tv.SelectedItem;
            MenuItem menuItem1 = dataGridCtMu.Items[1] as MenuItem;
            if (t.GetType() == typeof(Device_tag))
            {

                if ((t as Device_tag).protoType != ProtoType.OPCUA)
                {

                    menuItem1.Visibility = Visibility.Collapsed;
                }
                else
                {
                    menuItem1.Visibility = Visibility.Visible;
                }
                varDataGrid.DataContext = (t as Device_tag).infotags;
                varDataGrid.Tag = t;
                total.Text = (t as Device_tag).infotags.Count.ToString();
            }
            else
            {
                menuItem1.Visibility = Visibility.Collapsed;
            }

        }

        #region  dataGrid  右键菜单  ----------------------------------------------------------------------------------------------------------------------------------------
        private void addvar_Click(object sender, RoutedEventArgs e)
        {
            var source = varDataGrid.Tag as Device_tag;
            varInfo win_var = new varInfo(source, true);
            var re = win_var.ShowDialog();
            if ((bool)re)
            {
                varDataGrid.Items.Refresh();
            }

        }
        private void OPCaddvar_Click(object sender, RoutedEventArgs e)
        {
            var source = varDataGrid.Tag as Device_tag;
            OPCMonitor win_var = new OPCMonitor(source.IPAdress);
            var re = win_var.ShowDialog();
            if ((bool)re)
            {
                var table = win_var.table_added;
                foreach (DataRow row in table.Rows)
                {
                    if (!source.infotags.Any(taginfo => taginfo.address == row[1].ToString()))
                    {
                        var t = new taginfo
                        {
                            protoType = source.protoType,
                            DeviceName = source.DeviceName,
                            name = row[1].ToString(),
                            address = row[1].ToString(),
                            unit = "",
                            defaultvalue = 0,
                            limithigh = 1000,
                            limitlow = 0,
                            alarmhigh = 1000,
                            alarmlow = 0,
                            archive = false,
                            logout = false,
                            description = row[3].ToString(),
                        };
                        switch (row[2].ToString())
                        {
                            case "Boolean":
                                t.tagType = DataType.BOOL;
                                break;
                            case "Byte":
                                t.tagType = DataType.BYTE;
                                break;
                            case "Int16":
                                t.tagType = DataType.INT;
                                break;
                            case "UInt16":
                                t.tagType = DataType.UINT;
                                break;
                            case "Int32":
                                t.tagType = DataType.DINT;
                                break;
                            case "UInt32":
                                t.tagType = DataType.DINT;
                                break;
                            case "Int64":
                                t.tagType = DataType.DINT;
                                break;
                            case "UInt64":
                                t.tagType = DataType.DINT;
                                break;
                            case "Float":
                                t.tagType = DataType.REAL;
                                break;
                            case "Double":
                                t.tagType = DataType.LREAL;
                                break;
                            case "String":
                                t.tagType = DataType.STRING;
                                break;
                            case "DateTime":
                                break;
                        }
                        source.infotags.Add(t);
                    }
                }
                varDataGrid.Items.Refresh();
                #region //OPC tagTypes
                //public static NodeId GetDataTypeId(TypeInfo typeInfo)
                //{
                //    return typeInfo.BuiltInType switch
                //    {
                //        BuiltInType.Boolean => DataTypeIds.Boolean,
                //        BuiltInType.SByte => DataTypeIds.SByte,
                //        BuiltInType.Byte => DataTypeIds.Byte,
                //        BuiltInType.Int16 => DataTypeIds.Int16,
                //        BuiltInType.UInt16 => DataTypeIds.UInt16,
                //        BuiltInType.Int32 => DataTypeIds.Int32,
                //        BuiltInType.UInt32 => DataTypeIds.UInt32,
                //        BuiltInType.Int64 => DataTypeIds.Int64,
                //        BuiltInType.UInt64 => DataTypeIds.UInt64,
                //        BuiltInType.Float => DataTypeIds.Float,
                //        BuiltInType.Double => DataTypeIds.Double,
                //        BuiltInType.String => DataTypeIds.String,
                //        BuiltInType.DateTime => DataTypeIds.DateTime,
                //        BuiltInType.Guid => DataTypeIds.Guid,
                //        BuiltInType.ByteString => DataTypeIds.ByteString,
                //        BuiltInType.XmlElement => DataTypeIds.XmlElement,
                //        BuiltInType.NodeId => DataTypeIds.NodeId,
                //        BuiltInType.ExpandedNodeId => DataTypeIds.ExpandedNodeId,
                //        BuiltInType.StatusCode => DataTypeIds.StatusCode,
                //        BuiltInType.DiagnosticInfo => DataTypeIds.DiagnosticInfo,
                //        BuiltInType.QualifiedName => DataTypeIds.QualifiedName,
                //        BuiltInType.LocalizedText => DataTypeIds.LocalizedText,
                //        BuiltInType.ExtensionObject => DataTypeIds.Structure,
                //        BuiltInType.DataValue => DataTypeIds.DataValue,
                //        BuiltInType.Variant => DataTypeIds.BaseDataType,
                //        BuiltInType.Number => DataTypeIds.Number,
                //        BuiltInType.Integer => DataTypeIds.Integer,
                //        BuiltInType.UInteger => DataTypeIds.UInteger,
                //        BuiltInType.Enumeration => DataTypeIds.Enumeration,
                //        _ => NodeId.Null,
                //    };
                //}
                #endregion
            }
        }
        private void updatevar_Click(object sender, RoutedEventArgs e)
        {
            var oldtag = varDataGrid.SelectedItem as taginfo;
            if (oldtag != null)
            {
                varInfo win_var = new varInfo(oldtag, false);
                var re = win_var.ShowDialog();

                if ((bool)re)
                {
                    varDataGrid.Items.Refresh();
                }
            }


        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var source = varDataGrid.ItemsSource as List<taginfo>;
                devices_changed[source[0].DeviceName] = 2;
                //var tag = varDataGrid.SelectedItem as taginfo;
                var tags = varDataGrid.SelectedItems;
                foreach (taginfo item in tags)
                {
                    if (item != null)
                    {
                        source.Remove(item); 
                    }
                }
                varDataGrid.Items.Refresh();               
            }
            catch
            {

            }
        }

        private void export_Click(object sender, RoutedEventArgs e)
        {
            var source = varDataGrid.ItemsSource as List<taginfo>;
            System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog();
            dlg.Filter = "Excel 文件(*.xls)|*.xls|Excel 文件(*.xlsx)|*.xlsx";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.CreatePrompt = true;
            dlg.Title = "保存为Excel文件";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filePath = dlg.FileName;
                DataTable table = new DataTable();
                try
                {
                    if (source.Count == 0)
                    {
                        table = sqlClientHelper.GetTable(CommandType.Text, sqlClientHelper.sqlString_select + "r2r_tag", null)[0];
                    }
                    else
                    {
                        //table = App.mySQL_to_table(App.sqlString_select_r2r_1, App.connectionString_mySQL);
                        table = sqlClientHelper.GetTable(CommandType.Text, sqlClientHelper.sqlString_select + source[0].DeviceName, null)[0];
                    }

                }
                catch (Exception)
                {
                    MessageBoxX.Show("导出失败，数据库连接失败", "提示");
                }

                try
                {
                    MiniExcel.SaveAs(filePath, table, excelType: ExcelType.XLSX, overwriteFile: true);
                }
                catch (Exception)
                {
                    MessageBoxX.Show("导出失败，无法保存excel文件", "提示");
                }
            }
            dlg.Dispose();

        }
        //导入变量
        private void import_Click(object sender, RoutedEventArgs e)
        {
            var device = tree1.SelectedItem;
            if (device == null)
            {
                MessageBox.Show("请选择相应的设备名称");
                return;
            }
           
            if (device.GetType() == typeof(Device_tag))
            {
                var sdevice = tree1.SelectedItem as Device_tag;
                devices_changed[sdevice.DeviceName] = 2;
            }
            else
            {
                MessageBox.Show("请选择相应的设备名称");
                return;
            }
            List<taginfo> source = varDataGrid.ItemsSource as List<taginfo>;
            List<taginfo> temp = new List<taginfo>();
            //source.ForEach(i => temp.Add(i));
            if (source.Count > 0)
            {
                
                taginfo info = new taginfo();
                foreach (var item in source)
                {
                    communicationTag.Dic_taginfos.TryRemove(item.name, out info);
                }
                //source.Clear();
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Files|*.xls;*.xlsx";  //设置打开文件的后缀类型
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);//打开我的电脑文件夹
            string fileType = ".xls,.xlsx";
            // Show open file dialog box
            Nullable<bool> result = openFileDialog.ShowDialog();
            if (result == true)
            {
                string filePathAndName = openFileDialog.FileName;//文件路径以及文件名
                string fileName = System.IO.Path.GetFileName(filePathAndName); //获取文件名和扩展名
                string fileEx = System.IO.Path.GetExtension(fileName);//获取文件的扩展名
                if (fileType.Contains(fileEx))
                {

                    string errstring = "";
                    FileStream stream = null;
                    try
                    {
                        stream = System.IO.File.OpenRead(filePathAndName);

                    }
                    catch (Exception)
                    {
                        MessageBoxX.Show("导入失败，文件被占用", "提示");
                        return;
                    }
                    try
                    {
                        int done = 0, faild = 0;
                        var table = QueryAsDataTableWithoutEmptyRow(stream, true, null, ExcelType.UNKNOWN);

                        foreach (DataRow row in table.Rows)
                        {
                            if (!DBNull.Value.Equals(row["name"]) && !DBNull.Value.Equals(row["tagType"]) && !DBNull.Value.Equals(row["address"]))
                            {
                                if (!communicationTag.Dic_taginfos.Keys.Contains(row["name"]) && !temp.Exists(obj => obj.name == (string)row["name"]))
                                {
                                    var t = new taginfo
                                    {
                                        protoType = ((Device_tag)device).protoType,
                                        DeviceName = ((Device_tag)device).DeviceName,
                                        name = Convert.ToString(row["name"]),
                                        address = Convert.ToString(row["address"]),
                                        unit = Convert.ToString(row["unit"]),
                                        defaultvalue = Convert.ToDouble(row["defaultvalue"]),
                                        limithigh = Convert.ToDouble(row["limithigh"]),
                                        limitlow = Convert.ToDouble(row["limitlow"]),
                                        alarmhigh = Convert.ToDouble(row["alarmhigh"]),
                                        alarmlow = Convert.ToDouble(row["alarmlow"]),
                                        archive = Convert.ToBoolean(row["archive"]),
                                        logout = Convert.ToBoolean(row["logout"]),
                                        description = Convert.ToString(row["description"])
                                    };
                                    switch (((string)row["tagType"]).ToLower())
                                    {
                                        case "bool":
                                            t.tagType = DataType.BOOL;
                                            break;
                                        case "byte":
                                            t.tagType = DataType.BYTE;
                                            break;
                                        case "word":
                                            t.tagType = DataType.WORD;
                                            break;
                                        case "int":
                                            t.tagType = DataType.INT;
                                            break;
                                        case "uint":
                                            t.tagType = DataType.UINT;
                                            break;
                                        case "dint":
                                            t.tagType = DataType.DINT;
                                            break;
                                        case "real":
                                            t.tagType = DataType.REAL;
                                            break;
                                        case "lreal":
                                            t.tagType = DataType.LREAL;
                                            break;
                                        case "string":
                                            t.tagType = DataType.STRING;
                                            break;
                                        default:
                                            t.tagType = DataType.BOOL;
                                            break;
                                    }
                                    switch (((string)row["alarmType"]).ToLower())
                                    {
                                        case "noalarm":
                                            t.alarmType = AlarmType.NoAlarm;
                                            break;
                                        case "onalarm":
                                            t.alarmType = AlarmType.OnAlarm;
                                            break;
                                        case "offalarm":
                                            t.alarmType = AlarmType.OffAlarm;
                                            break;
                                        case "highalarm":
                                            t.alarmType = AlarmType.HighAlarm;
                                            break;
                                        case "lowalarm":
                                            t.alarmType = AlarmType.LowAlarm;
                                            break;
                                        default:
                                            t.alarmType = AlarmType.NoAlarm;
                                            break;
                                    }
                                    temp.Add(t);
                                   
                                    done++;
                                }
                                else
                                {
                                    faild++;
                                }
                            }
                            else
                            {
                                faild++;
                            }

                        }
                        //source = temp.ToList();
                        //var source1 = varDataGrid.ItemsSource as List<taginfo>;
                        //var ss = communicationTag.Protos_Tags;
                        if (temp.Count > 0)
                        {
                            source.Clear();
                            foreach (var item in temp)
                            {
                                source.Add(item);
                                communicationTag.Dic_taginfos.TryAdd(item.name, item);
                            }
                        }
                        temp.Clear();
                        MessageBoxX.Show("导入完成" + "\n" + "成功条数:" + done + "\n" + "失败条数" + faild, "提示");
                        
                    }
                    catch (Exception)
                    {
                        temp.Clear();
                        source.ForEach(i => communicationTag.Dic_taginfos.TryAdd(i.name, i));
                        MessageBoxX.Show("导入出错", "提示");
                    }
                    
                }
                else
                {
                    MessageBoxX.Show("文件类型不对", "提示");
                }

            }
            varDataGrid.Items.Refresh();
            total.Text = varDataGrid.Items.Count.ToString();
            Operations.addLog("导入PLC变量");//log
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            dataGridCtMu.IsOpen = false;
        }
        private void varDataGrid_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataGridRow dataGridRow = FindVisualParent<DataGridRow>(e.OriginalSource as DependencyObject);
            if (dataGridRow != null)
            {
                updatevar.Visibility = Visibility.Visible;
                delete.Visibility = Visibility.Visible;
            }
            else
            {
                updatevar.Visibility = Visibility.Collapsed;
                delete.Visibility = Visibility.Collapsed;
            }
        }
        #region  search
        search search = null;
        int id = 0;
        int matchindex;
        List<taginfo> matchitems = null;
        private void find_Click(object sender, RoutedEventArgs e)
        {
            var source = varDataGrid.ItemsSource as List<taginfo>;
            if (source.Count > 0)
            {
                if (search == null)
                {
                    search = new search();
                    search.Show();
                    search.Action = (s, button) =>
                    {
                        try
                        {
                            if (s != "")
                            {
                                // Regex方式
                                //Regex reg = new Regex(s, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                //for (int i = 0; i < source.Count; i++)
                                //{
                                //    var tf = source[i];
                                //    matchindex = i;
                                //    if (reg.IsMatch(tf.name) || reg.IsMatch(tf.address) || reg.IsMatch(tf.description))
                                //    {
                                //        varDataGrid.SelectedItems.Clear();
                                //        varDataGrid.SelectedIndex = i;
                                //        varDataGrid.ScrollIntoView(varDataGrid.SelectedItem);
                                //        break;
                                //    }
                                //}

                                matchitems = null;
                                // Use the Select method to find all rows matching the filter.
                                matchitems = source.Where(a => a.name.Contains(s) || a.address.Contains(s) || a.description.Contains(s)).ToList();
                                if (matchitems.Count == 0)
                                {
                                    return;
                                }
                                id = 0;
                            }
                            if (matchitems.Count > 0)
                            {
                                if (!button)
                                {
                                    var index = source.IndexOf(matchitems[id]);
                                    if (index <= source.Count - 1)
                                    {
                                        varDataGrid.SelectedIndex = index;
                                        varDataGrid.ScrollIntoView(varDataGrid.SelectedItem);
                                        id++;
                                        if (id == matchitems.Count)
                                        {
                                            id = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    var index = source.IndexOf(matchitems[matchitems.Count - id]);
                                    if (index <= varDataGrid.Items.Count - 1)
                                    {
                                        varDataGrid.SelectedIndex = index;
                                        varDataGrid.ScrollIntoView(varDataGrid.SelectedItem);
                                        id++;
                                        if (id == matchitems.Count)
                                        {
                                            id = 0;
                                        }
                                    }
                                }

                            }
                        }
                        catch (Exception)
                        {

                        }
                    };
                    search.Show();
                }

            }

        }
        #endregion

        #endregion

        #region tree 右键菜单  -------------------------------------------------------------------------------------------------------------------------------------------------------------
        protocol_tag p;
        Device_tag d;
        int itemOnButton;
        private void addDevice_Click(object sender, RoutedEventArgs e)
        {
            string devname = "";
            switch (p.protoID)
            {
                case ProtoType.ADS:
                    DeviceDialog_default dialog_ADS = new DeviceDialog_default();
                    if (dialog_ADS.ShowDialog() == true)
                    {
                        if (dialog_ADS.deviceName == "")
                        {
                            MessageBox.Show("连接名称不能为空");
                            return;
                        }
                        foreach (var pro in protocol_Tags)
                        {
                            foreach (var dev in pro.Devices)
                            {
                                if (dialog_ADS.deviceName == dev.DeviceName)
                                {
                                    MessageBox.Show("连接已存在" + dialog_ADS.deviceName);
                                    return;
                                }
                            }
                        }
                        Device_tag device = new Device_tag()
                        {
                            protoType = p.protoID,
                            Enable = dialog_ADS.Enabled,
                            DeviceName = dialog_ADS.deviceName,
                            IPAdress = dialog_ADS.IPaddress,
                            TCPPort = dialog_ADS.Port,
                            CycTime = dialog_ADS.Cyctime,
                            Timeout = dialog_ADS.Timeout,
                            infotags = new List<taginfo>()
                        };

                        p.Devices.Add(device);
                        Operations.addLog("添加设备" + dialog_ADS.deviceName);
                        devname = dialog_ADS.deviceName;
                    }
                    break;
                case ProtoType.INOVANCE:
                    DeviceDialog_modbus dialog_inovance = new DeviceDialog_modbus();
                    if (dialog_inovance.ShowDialog() == true)
                    {
                        if (dialog_inovance.deviceName == "")
                        {
                            MessageBox.Show("连接名称不能为空");
                            return;
                        }
                        foreach (var pro in protocol_Tags)
                        {
                            foreach (var dev in pro.Devices)
                            {
                                if (dialog_inovance.deviceName == dev.DeviceName)
                                {
                                    MessageBox.Show("连接已存在" + dialog_inovance.deviceName);
                                    return;
                                }
                            }
                        }
                        Device_tag device = new Device_tag()
                        {
                            protoType = p.protoID,
                            Enable = dialog_inovance.Enabled,
                            DeviceName = dialog_inovance.deviceName,
                            IPAdress = dialog_inovance.IPaddress,
                            TCPPort = dialog_inovance.Port,
                            CycTime = dialog_inovance.Cyctime,
                            Timeout = dialog_inovance.Timeout,
                            CpuType = dialog_inovance.CpuType,
                            NetID = dialog_inovance.NetID,
                            ByteOrder = dialog_inovance.ByteOrder,
                            infotags = new List<taginfo>()
                        };

                        p.Devices.Add(device);
                        Operations.addLog("添加设备" + dialog_inovance.deviceName);
                        devname = dialog_inovance.deviceName;
                    }
                    break;
                case ProtoType.MODBUS:
                    DeviceDialog_modbus dialog_modbus = new DeviceDialog_modbus();
                    dialog_modbus.box_CpuType.Visibility = Visibility.Hidden;
                    dialog_modbus.tex_CpuType.Visibility = Visibility.Hidden;

                    if (dialog_modbus.ShowDialog() == true)
                    {
                        if (dialog_modbus.deviceName == "")
                        {
                            MessageBox.Show("连接名称不能为空");
                            return;
                        }
                        foreach (var pro in protocol_Tags)
                        {
                            foreach (var dev in pro.Devices)
                            {
                                if (dialog_modbus.deviceName == dev.DeviceName)
                                {
                                    MessageBox.Show("连接已存在" + dialog_modbus.deviceName);
                                    return;
                                }
                            }
                        }
                        Device_tag device = new Device_tag()
                        {
                            protoType = p.protoID,
                            Enable = dialog_modbus.Enabled,
                            DeviceName = dialog_modbus.deviceName,
                            IPAdress = dialog_modbus.IPaddress,
                            TCPPort = dialog_modbus.Port,
                            CycTime = dialog_modbus.Cyctime,
                            Timeout = dialog_modbus.Timeout,
                            NetID = dialog_modbus.NetID,
                            ByteOrder = dialog_modbus.ByteOrder,
                            infotags = new List<taginfo>()
                        };

                        p.Devices.Add(device);
                        Operations.addLog("添加设备" + dialog_modbus.deviceName);
                        devname = dialog_modbus.deviceName;
                    }
                    break;
                case ProtoType.OPCUA:
                    DeviceDialog_default dialog_OPC = new DeviceDialog_default();

                    if (dialog_OPC.ShowDialog() == true)
                    {
                        if (dialog_OPC.deviceName == "")
                        {
                            MessageBox.Show("连接名称不能为空");
                            return;
                        }
                        foreach (var pro in protocol_Tags)
                        {
                            foreach (var dev in pro.Devices)
                            {
                                if (dialog_OPC.deviceName == dev.DeviceName)
                                {
                                    MessageBox.Show("连接已存在" + dialog_OPC.deviceName);
                                    return;
                                }
                            }
                        }
                        Device_tag device = new Device_tag()
                        {
                            protoType = p.protoID,
                            Enable = dialog_OPC.Enabled,
                            opcMonitor = dialog_OPC.monitor,
                            DeviceName = dialog_OPC.deviceName,
                            IPAdress = dialog_OPC.IPaddress,
                            TCPPort = dialog_OPC.Port,
                            CycTime = dialog_OPC.Cyctime,
                            Timeout = dialog_OPC.Timeout,
                            infotags = new List<taginfo>()
                        };

                        p.Devices.Add(device);
                        Operations.addLog("添加设备" + dialog_OPC.deviceName);
                        devname = dialog_OPC.deviceName;
                    }
                    break;
                default:
                    break;
            }
            if (devname != "")
            {
                devices_changed.Add(devname, 1);//添加修改标志位
            }

        }
        private void removeDevice_Click(object sender, RoutedEventArgs e)
        {
            if (d != null)
            {
                foreach (var pro in protocol_Tags)
                {
                    foreach (var dev in pro.Devices) //:“集合已修改；可能无法执行枚举操作。”

                    {
                        if (d.DeviceName == dev.DeviceName)
                        {
                            System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show("您确定要删除设备吗？", "提示信息：", System.Windows.MessageBoxButton.OKCancel, System.Windows.MessageBoxImage.Information);
                            if (result == System.Windows.MessageBoxResult.OK)
                            {
                                try
                                {
                                    pro.Devices.Remove(dev);
                                    devices_changed[dev.DeviceName] = 3;
                                    varDataGrid.DataContext = null;
                                    Operations.addLog("删除设备完成" + d.DeviceName);
                                    return;
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("删除设备失败");
                                    Operations.addLog("删除设备失败" + d.DeviceName);
                                    return;
                                }

                            }
                        }
                    }
                }
            }
        }
        private void cancelDevice_Click(object sender, RoutedEventArgs e)
        {
            treeCtMu.IsOpen = false;
        }
        private void editDevice_Click(object sender, RoutedEventArgs e)
        {

            if (d != null)
            {
                if (d.protoType == ProtoType.ADS)
                {
                    DeviceDialog_default dialog_ADS = new DeviceDialog_default();
                    dialog_ADS.Enabled = d.Enable;
                    dialog_ADS.deviceName = d.DeviceName;
                    dialog_ADS.IPaddress = d.IPAdress;
                    dialog_ADS.Port = d.TCPPort;
                    dialog_ADS.Cyctime = d.CycTime;
                    dialog_ADS.Timeout = d.Timeout;

                    if (dialog_ADS.ShowDialog() == true)
                    {
                        if (dialog_ADS.deviceName == "")
                        {
                            MessageBox.Show("连接名称不能为空");
                            return;
                        }
                        //foreach (var pro in protocol_Tags)
                        //{
                        //    foreach (var dev in pro.Devices)
                        //    {
                        //        if (dialog_ADS.deviceName == dev.DeviceName)
                        //        {
                        //            MessageBox.Show("连接已存在" + dialog_ADS.deviceName);
                        //            return;
                        //        }
                        //    }
                        //}
                        try
                        {
                            d.Enable = dialog_ADS.Enabled;
                            d.DeviceName = dialog_ADS.deviceName;
                            d.IPAdress = dialog_ADS.IPaddress;
                            d.TCPPort = dialog_ADS.Port;
                            d.CycTime = dialog_ADS.Cyctime;
                            d.Timeout = dialog_ADS.Timeout;
                        }
                        catch (Exception)
                        {
                            Operations.addLog("编辑设备失败" + dialog_ADS.deviceName);
                            MessageBox.Show("编辑失败");
                        }
                        Operations.addLog("编辑设备完成" + dialog_ADS.deviceName);
                    }
                }
                if (d.protoType == ProtoType.INOVANCE || d.protoType == ProtoType.MODBUS)
                {
                    DeviceDialog_modbus dialog_modbus = new DeviceDialog_modbus();
                    dialog_modbus.Enabled = d.Enable;
                    dialog_modbus.deviceName = d.DeviceName;
                    dialog_modbus.IPaddress = d.IPAdress;
                    dialog_modbus.Port = d.TCPPort;
                    dialog_modbus.Cyctime = d.CycTime;
                    dialog_modbus.Timeout = d.Timeout;
                    dialog_modbus.CpuType = d.CpuType;
                    dialog_modbus.NetID = d.NetID;
                    dialog_modbus.ByteOrder = ByteOrder.ABCD;
                    dialog_modbus.protoType = d.protoType;

                    if (dialog_modbus.ShowDialog() == true)
                    {
                        if (dialog_modbus.deviceName == "")
                        {
                            MessageBox.Show("连接名称不能为空");
                            return;
                        }
                        //foreach (var pro in protocol_Tags)
                        //{
                        //    foreach (var dev in pro.Devices)
                        //    {
                        //        if (dialog_modbus.deviceName == dev.DeviceName)
                        //        {
                        //            MessageBox.Show("连接已存在" + dialog_modbus.deviceName);
                        //            return;
                        //        }
                        //    }
                        //}
                        try
                        {
                            d.Enable = dialog_modbus.Enabled;
                            d.DeviceName = dialog_modbus.deviceName;
                            d.IPAdress = dialog_modbus.IPaddress;
                            d.TCPPort = dialog_modbus.Port;
                            d.CycTime = dialog_modbus.Cyctime;
                            d.Timeout = dialog_modbus.Timeout;
                            d.CpuType = dialog_modbus.CpuType;
                            d.ByteOrder = dialog_modbus.ByteOrder;
                            d.NetID = dialog_modbus.NetID;
                        }
                        catch (Exception)
                        {
                            Operations.addLog("编辑设备失败" + dialog_modbus.deviceName);
                            MessageBox.Show("编辑失败");
                        }
                        Operations.addLog("编辑设备完成" + dialog_modbus.deviceName);
                    }
                }
                if (d.protoType == ProtoType.OPCUA)
                {
                    DeviceDialog_default dialog_opc = new DeviceDialog_default();
                    dialog_opc.Enabled = d.Enable;
                    dialog_opc.monitor = d.opcMonitor;
                    dialog_opc.deviceName = d.DeviceName;
                    dialog_opc.IPaddress = d.IPAdress;
                    dialog_opc.Port = d.TCPPort;
                    dialog_opc.Cyctime = d.CycTime;
                    dialog_opc.Timeout = d.Timeout;

                    if (dialog_opc.ShowDialog() == true)
                    {
                        if (dialog_opc.deviceName == "")
                        {
                            MessageBox.Show("连接名称不能为空");
                            return;
                        }
                        //foreach (var pro in protocol_Tags)
                        //{
                        //    foreach (var dev in pro.Devices)
                        //    {
                        //        if (dialog_ADS.deviceName == dev.DeviceName)
                        //        {
                        //            MessageBox.Show("连接已存在" + dialog_ADS.deviceName);
                        //            return;
                        //        }
                        //    }
                        //}
                        try
                        {
                            d.Enable = dialog_opc.Enabled;
                            d.DeviceName = dialog_opc.deviceName;
                            d.IPAdress = dialog_opc.IPaddress;
                            d.TCPPort = dialog_opc.Port;
                            d.CycTime = dialog_opc.Cyctime;
                            d.Timeout = dialog_opc.Timeout;
                            d.opcMonitor = dialog_opc.monitor;
                        }
                        catch (Exception)
                        {
                            Operations.addLog("编辑设备失败" + dialog_opc.deviceName);
                            MessageBox.Show("编辑失败");
                        }
                        Operations.addLog("编辑设备完成" + dialog_opc.deviceName);
                    }
                }
            }
        }
        #region  右键获取item
        //private void TreeViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    var Item = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
        //    if (Item != null)
        //    {
        //        try
        //        {
        //            d = Item.DataContext as Device_tag;
        //            if (d != null)
        //            {
        //                itemOnButton = 2;//2级菜单
        //                e.Handled = true;
        //                addDevice.Visibility = Visibility.Collapsed;
        //                editDevice.Visibility = Visibility.Visible;
        //                removeDevice.Visibility = Visibility.Visible;
        //                cancelDevice.Visibility = Visibility.Visible;
        //                return;
        //            }
        //        }
        //        catch (Exception)
        //        {

        //        }
        //        try
        //        {
        //            p = Item.DataContext as protocol_tag;
        //            if (p != null)
        //            {
        //                itemOnButton = 1;//1级菜单
        //                e.Handled = true;
        //                addDevice.Visibility = Visibility.Visible;
        //                editDevice.Visibility = Visibility.Collapsed;
        //                removeDevice.Visibility = Visibility.Collapsed;
        //                cancelDevice.Visibility = Visibility.Visible;
        //                return;
        //            }
        //        }
        //        catch (Exception)
        //        {

        //        }
        //    }
        //}

        //static DependencyObject VisualUpwardSearch<T>(DependencyObject source)
        //{
        //    while (source != null && source.GetType() != typeof(T))
        //        source = VisualTreeHelper.GetParent(source);
        //    return source;
        //}
        #endregion

        #region  右键获取item 新
        private void TreeView_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = FindVisualParent<TreeViewItem>(e.OriginalSource as DependencyObject);

            if (treeViewItem != null)
            {
                // 在这里处理鼠标右键点击TreeViewItem的逻辑
                // treeViewItem就是当前点击的TreeViewItem
                try
                {
                    d = treeViewItem.DataContext as Device_tag;
                    if (d != null)
                    {
                        itemOnButton = 2;//2级菜单
                        e.Handled = true;
                        addDevice.Visibility = Visibility.Collapsed;
                        editDevice.Visibility = Visibility.Visible;
                        removeDevice.Visibility = Visibility.Visible;
                        cancelDevice.Visibility = Visibility.Visible;
                        return;
                    }
                }
                catch (Exception)
                {

                }
                try
                {
                    p = treeViewItem.DataContext as protocol_tag;
                    if (p != null)
                    {
                        itemOnButton = 1;//1级菜单
                        e.Handled = true;
                        addDevice.Visibility = Visibility.Visible;
                        editDevice.Visibility = Visibility.Collapsed;
                        removeDevice.Visibility = Visibility.Collapsed;
                        cancelDevice.Visibility = Visibility.Visible;
                        return;
                    }
                }
                catch (Exception)
                {

                }
            }
            else
            {
                addDevice.Visibility = Visibility.Collapsed;
                editDevice.Visibility = Visibility.Collapsed;
                removeDevice.Visibility = Visibility.Collapsed;
                cancelDevice.Visibility = Visibility.Collapsed;
            }
        }
        private static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            var parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null)
                return null;

            T parent = parentObject as T;
            if (parent != null)
                return parent;

            return FindVisualParent<T>(parentObject);
        }
        #endregion
        #endregion

        #region  按钮 -------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void var_save_Click(object sender, RoutedEventArgs e)
        {
            List<string> tableNames = new List<string>();
            int resault = 0;
            try
            {
                SqlDataReader reader = sqlClientHelper.ExecuteReader(sqlClientHelper.connectionString, CommandType.Text, sqlClientHelper.vsqlString_getAll_tablename, null);
                while (reader.Read())
                {
                    tableNames.Add(reader.GetString(0));
                }
                reader.Close();

            }
            catch (Exception)
            {
                Operations.addLog("通讯设置：连接数据库失败，保存未完成");
                MessageBox.Show("连接数据库失败，保存未完成");
                return;
            }

            try
            {
                //保存json
                communicationTag.SaveToFile();
                Operations.addLog("通讯设置：保存json完成");
            }
            catch (Exception)
            {
                Operations.addLog("通讯设置：保存json失败");
                MessageBox.Show("保存通讯组json失败，请检查设置");
                return;
            }

            //保存至sql  判断是否有更改
            //sqlClientHelper.ExecteNonQuery(CommandType.Text, App.sqlString_clear_r2r_1, null);

            int noError = 0;
            try
            {
                foreach (var item in devices_changed)
                {
                    if (item.Value == 1 || item.Value == 2)
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Columns.Add("name", typeof(string));//dt.Columns.Add("列1", typeof(int));
                        dataTable.Columns.Add("tagType", typeof(string));
                        dataTable.Columns.Add("address", typeof(string));
                        dataTable.Columns.Add("unit", typeof(string));
                        dataTable.Columns.Add("defaultvalue", typeof(double));
                        dataTable.Columns.Add("limithigh", typeof(double));
                        dataTable.Columns.Add("limitlow", typeof(double));
                        dataTable.Columns.Add("alarmType", typeof(string));
                        dataTable.Columns.Add("alarmhigh", typeof(double));
                        dataTable.Columns.Add("alarmlow", typeof(double));
                        dataTable.Columns.Add("archive", typeof(bool));
                        dataTable.Columns.Add("logout", typeof(bool));
                        dataTable.Columns.Add("description", typeof(string));
                        dataTable.Columns.Add("protoID", typeof(int));
                        dataTable.Columns.Add("DeviceName", typeof(string));
                        foreach (var proto in communicationTag.Protos_Tags)
                        {
                            foreach (var dev in proto.Devices)
                            {
                                if (dev.DeviceName == item.Key)
                                {
                                    foreach (var tf in dev.infotags)
                                    {
                                        var row = dataTable.NewRow();
                                        row[0] = tf.name;
                                        row[1] = tf.tagType.ToString();
                                        row[2] = tf.address;
                                        row[3] = tf.unit;
                                        row[4] = tf.defaultvalue;
                                        row[5] = tf.limithigh;
                                        row[6] = tf.limitlow;
                                        row[7] = tf.alarmType.ToString();
                                        row[8] = tf.alarmhigh;
                                        row[9] = tf.alarmlow;
                                        row[10] = tf.archive;
                                        row[11] = tf.logout;
                                        row[12] = tf.description;
                                        //row[13] = (int)tf.protoType;
                                        //row[14] = tf.DeviceName;
                                        dataTable.Rows.Add(row);
                                    }
                                }
                            }
                        }

                        try
                        {
                            if (!tableNames.Contains(item.Key))
                            {
                                sqlClientHelper.createNewTable(item.Key);
                            }
                            resault = sqlClientHelper.uptate_From_table(dataTable, item.Key);
                            if (resault != -1)
                            {
                                Operations.addLog("通讯设置：保存至SQL" + item.Key + "完成");
                            }
                            else
                            {
                                noError++;
                            }
                        }
                        catch (Exception)
                        {
                            Operations.addLog("通讯设置：保存至SQL" + item.Key + "失败");
                            noError++;
                        }
                    }
                    if (item.Value == 3)
                    {
                        if (tableNames.Contains(item.Key))
                        {
                            try
                            {
                                sqlClientHelper.ExecteNonQuery(CommandType.Text, "DROP TABLE" + item.Key, null);
                            }
                            catch (Exception)
                            {
                                Operations.addLog("通讯设置：删除设备" + item.Key + "失败");
                                noError++;
                            }
                        }
                    }
                }
                if (noError==0)
                {
                    //保存成功提示重启生效
                    MessageBox.Show("保存完成\n需重启软件以生效");
                }
                else
                {
                    MessageBox.Show("保存SQL失败，请检查设置");
                }
            }
            catch (Exception)
            {
                Operations.addLog("通讯设置：保存失败");
                MessageBox.Show("保存失败，请检查设置");
            }
           

            //清零
            string[] keys = devices_changed.Keys.ToArray();
            for (int i = 0; i < keys.Length; i++)
            {
                devices_changed[keys[i]] = 0;
            }
            saved = true;
            this.Close();
        }

        private void var_cancel_Click(object sender, RoutedEventArgs e)
        {
            //清零
            string[] keys = devices_changed.Keys.ToArray();
            for (int i = 0; i < keys.Length; i++)
            {
                devices_changed[keys[i]] = 0;
            }
            this.Close();
        }
        #endregion

        //miniExcel 读取至datatable并去除空白行
        public DataTable QueryAsDataTableWithoutEmptyRow(Stream stream, bool useHeaderRow, string sheetName, ExcelType excelType)
        {
            if (sheetName == null && excelType != ExcelType.CSV) /*Issue #279*/
                sheetName = stream.GetSheetNames().First();

            var dt = new DataTable(sheetName);
            var first = true;
            var rows = stream.Query(useHeaderRow, sheetName, excelType);
            foreach (IDictionary<string, object> row in rows)
            {
                if (first)
                {
                    foreach (var key in row.Keys)
                    {
                        var column = new DataColumn(key, typeof(object)) { Caption = key };
                        dt.Columns.Add(column);
                    }
                    dt.BeginLoadData();
                    first = false;
                }

                var newRow = dt.NewRow();
                var isNull = true;
                foreach (var key in row.Keys)
                {
                    var _v = row[key];
                    if (_v != null)
                        isNull = false;
                    newRow[key] = _v;
                }

                if (!isNull)
                    dt.Rows.Add(newRow);
            }

            dt.EndLoadData();
            return dt;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!saved)
            {
                var js = JsonConvert.SerializeObject(protocol_Tags);
                if (!js.Equals(str_tags_temp))
                {
                    MessageBoxResult result = MessageBox.Show("变量设置已更改，是否保存更改", "提示信息：", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    if (result == MessageBoxResult.OK)
                    {
                        var_save_Click(null, null);
                    }
                    else
                    {
                        communicationTag.Current.LoadFromFile();
                    }
                }
            }
           
        }

       
    }

    public class TreeViewLineConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double height = (double)values[0];

            TreeViewItem item = values[2] as TreeViewItem;
            ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(item);
            bool isLastOne = ic.ItemContainerGenerator.IndexFromContainer(item) == ic.Items.Count - 1;

            Rectangle rectangle = values[3] as Rectangle;
            if (isLastOne)
            {
                rectangle.VerticalAlignment = VerticalAlignment.Top;
                return 9.0;
            }
            else
            {
                rectangle.VerticalAlignment = VerticalAlignment.Stretch;
                return double.NaN;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
