using Opc.Ua;
using Opc.Ua.Client;
using OpcUaHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using MessageBox = System.Windows.MessageBox;

namespace R2R.protocols
{
    /// <summary>
    /// OPCMonitor.xaml 的交互逻辑
    /// </summary>
    public partial class OPCMonitor : Window
    {
        public DataTable table_added = new DataTable();

        /// <summary>
        /// Opc客户端的核心类
        /// </summary>
        OpcUaClient m_OpcUaClient = new OpcUaClient();

        public OPCMonitor(string url)
        {
            InitializeComponent();
            tblock1.Text = url;
            BrowseNodesTV.Enabled = false;
            BrowseNodesTV.ImageList = new ImageList();
            BrowseNodesTV.ImageList.Images.Add("Class_489", Properties.Resources.Class_489);
            BrowseNodesTV.ImageList.Images.Add("ClassIcon", Properties.Resources.ClassIcon);
            BrowseNodesTV.ImageList.Images.Add("brackets", Properties.Resources.brackets_Square_16xMD);
            BrowseNodesTV.ImageList.Images.Add("VirtualMachine", Properties.Resources.VirtualMachine);
            BrowseNodesTV.ImageList.Images.Add("Enum_582", Properties.Resources.Enum_582);
            BrowseNodesTV.ImageList.Images.Add("Method_636", Properties.Resources.Method_636);
            BrowseNodesTV.ImageList.Images.Add("Module_648", Properties.Resources.Module_648);
            BrowseNodesTV.ImageList.Images.Add("Loading", Properties.Resources.loading);

        }

        #region   OK  CANCEL  -------------------------------------------------------------------------
        /// <summary>
        /// 将dgv列表数据转换为datatable数据
        /// </summary>
        /// <param name="dgv">当前dgv列表对象</param>
        /// <returns>datatable对象</returns>
        private static DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                System.Data.DataColumn dc = new System.Data.DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }

            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {

            table_added = GetDgvToTable(dataGridView2);
            this.DialogResult = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        #endregion

        #region  CONNECT  ---------------------------------------------------------------------------
        private async void connect_Click(object sender, RoutedEventArgs e)
        {
            m_OpcUaClient.OpcStatusChange += M_OpcUaClient_OpcStatusChange1;
            m_OpcUaClient.ConnectComplete += M_OpcUaClient_ConnectComplete;
            try
            {
                await m_OpcUaClient.ConnectServer(tblock1.Text);
            }
            catch (Exception ex)
            {
                ClientUtils.HandleException("", ex);
            }
        }
        /// <summary>
        /// 连接服务器结束后马上浏览根节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_OpcUaClient_ConnectComplete(object sender, EventArgs e)
        {
            try
            {
                OpcUaClient client = (OpcUaClient)sender;
                if (client.Connected)
                {
                    // populate the browse view.
                    PopulateBranch(ObjectIds.ObjectsFolder, BrowseNodesTV.Nodes);
                    BrowseNodesTV.Enabled = true;
                }
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException("", exception);
            }
        }

        /// <summary>
        /// OPC 客户端的状态变化后的消息提醒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M_OpcUaClient_OpcStatusChange1(object sender, OpcUaStatusEventArgs e)
        {

            if (e.Error)
            {
                state.Background = Brushes.Red;
            }
            else
            {
                state.Background = Brushes.LightGray;
            }

            state.Text = e.ToString();
        }

        /// <summary>
        /// Populates the branch in the tree view.
        /// </summary>
        /// <param name="sourceId">The NodeId of the Node to browse.</param>
        /// <param name="nodes">The node collect to populate.</param>
        private async void PopulateBranch(NodeId sourceId, TreeNodeCollection nodes)
        {
            nodes.Clear();
            nodes.Add(new TreeNode("Browsering...", 7, 7));
            // fetch references from the server.
            TreeNode[] listNode = await Task.Run(() =>
            {
                ReferenceDescriptionCollection references = GetReferenceDescriptionCollection(sourceId);
                List<TreeNode> list = new List<TreeNode>();
                if (references != null)
                {
                    // process results.
                    for (int ii = 0; ii < references.Count; ii++)
                    {
                        ReferenceDescription target = references[ii];
                        TreeNode child = new TreeNode(Utils.Format("{0}", target));

                        child.Tag = target;
                        string key = GetImageKeyFromDescription(target, sourceId);
                        child.ImageKey = key;
                        child.SelectedImageKey = key;

                        // if (target.NodeClass == NodeClass.Object || target.NodeClass == NodeClass.Unspecified || expanded)
                        // {
                        //     child.Nodes.Add(new TreeNode());
                        // }

                        //if (!(bool)checkBox1.IsChecked)
                        //{
                        if (GetReferenceDescriptionCollection((NodeId)target.NodeId).Count > 0)
                        {
                            child.Nodes.Add(new TreeNode());
                        }
                        //}
                        //else
                        //{
                        //   child.Nodes.Add(new TreeNode());
                        //}


                        list.Add(child);
                    }
                }

                return list.ToArray();
            });


            // update the attributes display.
            // DisplayAttributes(sourceId);
            nodes.Clear();
            nodes.AddRange(listNode.ToArray());
        }

        #endregion

        #region  func——common  读取节点 ----------------------------------------------------------------
        //private ReferenceDescriptionCollection GetReferenceDescriptionCollection(NodeId sourceId)
        //{
        //    TaskCompletionSource<ReferenceDescriptionCollection> task = new TaskCompletionSource<ReferenceDescriptionCollection>();

        //    // find all of the components of the node.
        //    BrowseDescription nodeToBrowse1 = new BrowseDescription();

        //    nodeToBrowse1.NodeId = sourceId;
        //    nodeToBrowse1.BrowseDirection = BrowseDirection.Forward;
        //    nodeToBrowse1.ReferenceTypeId = ReferenceTypeIds.Aggregates;
        //    nodeToBrowse1.IncludeSubtypes = true;
        //    nodeToBrowse1.NodeClassMask = (uint)(NodeClass.Object | NodeClass.Variable | NodeClass.Method | NodeClass.ReferenceType | NodeClass.ObjectType | NodeClass.View | NodeClass.VariableType | NodeClass.DataType);
        //    nodeToBrowse1.ResultMask = (uint)BrowseResultMask.All;

        //    // find all nodes organized by the node.
        //    BrowseDescription nodeToBrowse2 = new BrowseDescription();

        //    nodeToBrowse2.NodeId = sourceId;
        //    nodeToBrowse2.BrowseDirection = BrowseDirection.Forward;
        //    nodeToBrowse2.ReferenceTypeId = ReferenceTypeIds.Organizes;
        //    nodeToBrowse2.IncludeSubtypes = true;
        //    nodeToBrowse2.NodeClassMask = (uint)(NodeClass.Object | NodeClass.Variable | NodeClass.Method | NodeClass.View | NodeClass.ReferenceType | NodeClass.ObjectType | NodeClass.VariableType | NodeClass.DataType);
        //    nodeToBrowse2.ResultMask = (uint)BrowseResultMask.All;

        //    BrowseDescriptionCollection nodesToBrowse = new BrowseDescriptionCollection();
        //    nodesToBrowse.Add(nodeToBrowse1);
        //    nodesToBrowse.Add(nodeToBrowse2);

        //    // fetch references from the server.
        //    ReferenceDescriptionCollection references = FormUtils.Browse(m_OpcUaClient.Session, nodesToBrowse, false);
        //    return references;
        //}

        private ReferenceDescriptionCollection GetReferenceDescriptionCollection(NodeId sourceId)
        {
            TaskCompletionSource<ReferenceDescriptionCollection> task = new TaskCompletionSource<ReferenceDescriptionCollection>();

            // 浏览子节点
            Browser browser = new Browser(m_OpcUaClient.Session);
            browser.BrowseDirection = BrowseDirection.Forward;
            browser.ReferenceTypeId = ReferenceTypeIds.HierarchicalReferences;
            browser.IncludeSubtypes = true;
            browser.NodeClassMask = (int)(NodeClass.Object | NodeClass.Variable | NodeClass.Method | NodeClass.ReferenceType | NodeClass.ObjectType | NodeClass.View | NodeClass.VariableType | NodeClass.DataType);
            ReferenceDescriptionCollection references = browser.Browse(sourceId);

            return references;
        }
        private string GetImageKeyFromDescription(ReferenceDescription target, NodeId sourceId)
        {
            if (target.NodeClass == NodeClass.Variable)
            {
                DataValue dataValue = m_OpcUaClient.ReadNode((NodeId)target.NodeId);

                if (dataValue.WrappedValue.TypeInfo != null)
                {
                    if (dataValue.WrappedValue.TypeInfo.ValueRank == ValueRanks.Scalar)
                    {
                        return "Enum_582";
                    }
                    else if (dataValue.WrappedValue.TypeInfo.ValueRank == ValueRanks.OneDimension)
                    {
                        return "brackets";
                    }
                    else if (dataValue.WrappedValue.TypeInfo.ValueRank == ValueRanks.TwoDimensions)
                    {
                        return "Module_648";
                    }
                    else
                    {
                        return "ClassIcon";
                    }
                }
                else
                {
                    return "ClassIcon";
                }
            }
            else if (target.NodeClass == NodeClass.Object)
            {
                if (sourceId == ObjectIds.ObjectsFolder)
                {
                    return "VirtualMachine";
                }
                else
                {
                    return "ClassIcon";
                }
            }
            else if (target.NodeClass == NodeClass.Method)
            {
                return "Method_636";
            }
            else
            {
                return "ClassIcon";
            }
        }

        /// <summary>
        /// 0:NodeClass  1:Value  2:AccessLevel  3:DisplayName  4:Description
        /// </summary>
        /// <param name="nodeIds"></param>
        /// <returns></returns>

        //private DataValue[] ReadOneNodeFiveAttributes(List<NodeId> nodeIds)
        //{
        //    ReadValueIdCollection nodesToRead = new ReadValueIdCollection();
        //    foreach (var nodeId in nodeIds)
        //    {
        //        NodeId sourceId = nodeId;
        //        nodesToRead.Add(new ReadValueId()
        //        {
        //            NodeId = sourceId,
        //            AttributeId = Attributes.NodeClass
        //        });
        //        nodesToRead.Add(new ReadValueId()
        //        {
        //            NodeId = sourceId,
        //            AttributeId = Attributes.Value
        //        });
        //        nodesToRead.Add(new ReadValueId()
        //        {
        //            NodeId = sourceId,
        //            AttributeId = Attributes.AccessLevel
        //        });
        //        nodesToRead.Add(new ReadValueId()
        //        {
        //            NodeId = sourceId,
        //            AttributeId = Attributes.DisplayName
        //        });
        //        nodesToRead.Add(new ReadValueId()
        //        {
        //            NodeId = sourceId,
        //            AttributeId = Attributes.Description
        //        });
        //    }

        //    // read all values.
        //    m_OpcUaClient.Session.Read(
        //        null,
        //        0,
        //        TimestampsToReturn.Neither,
        //        nodesToRead,
        //        out DataValueCollection results,
        //        out DiagnosticInfoCollection diagnosticInfos);

        //    ClientBase.ValidateResponse(results, nodesToRead);
        //    ClientBase.ValidateDiagnosticInfos(diagnosticInfos, nodesToRead);

        //    return results.ToArray();
        //}

        private DataValue[] ReadOneNodeFiveAttributes(List<NodeId> nodeIds)
        {
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();
            foreach (var nodeId in nodeIds)
            {
                NodeId sourceId = nodeId;
                nodesToRead.Add(new ReadValueId()
                {
                    NodeId = sourceId,
                    AttributeId = Attributes.NodeClass
                });
                nodesToRead.Add(new ReadValueId()
                {
                    NodeId = sourceId,
                    AttributeId = Attributes.Value
                });
                nodesToRead.Add(new ReadValueId()
                {
                    NodeId = sourceId,
                    AttributeId = Attributes.AccessLevel
                });
                nodesToRead.Add(new ReadValueId()
                {
                    NodeId = sourceId,
                    AttributeId = Attributes.DisplayName
                });
                nodesToRead.Add(new ReadValueId()
                {
                    NodeId = sourceId,
                    AttributeId = Attributes.Description
                });
            }
            List<ReadValueId> list = nodesToRead.ToList();

            int size = 20;
            // 将含有 110 个元素的列表按照 20 个一组分成若干组
            List<List<ReadValueId>> groupedLists = list
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / size)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();

            DataValueCollection results = new DataValueCollection();
            foreach (var item in groupedLists)
            {
                ReadValueIdCollection ids = new ReadValueIdCollection();
                foreach (var id in item)
                {
                    ids.Add(id);
                }
                // read all values.
                m_OpcUaClient.Session.Read(
                    null,
                    0,
                    TimestampsToReturn.Neither,
                    ids,
                    out DataValueCollection result,
                    out DiagnosticInfoCollection diagnosticInfos);

                ClientBase.ValidateResponse(result, ids);
                ClientBase.ValidateDiagnosticInfos(diagnosticInfos, ids);
                foreach (var res in result)
                {
                    results.Add(res);
                }
            }


            return results.ToArray();
        }


        /// <summary>
        /// 读取一个节点的指定属性
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        private DataValue ReadNoteDataValueAttributes(NodeId nodeId, uint attribute)
        {
            NodeId sourceId = nodeId;
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();


            ReadValueId nodeToRead = new ReadValueId();
            nodeToRead.NodeId = sourceId;
            nodeToRead.AttributeId = attribute;
            nodesToRead.Add(nodeToRead);

            int startOfProperties = nodesToRead.Count;

            // find all of the pror of the node.
            BrowseDescription nodeToBrowse1 = new BrowseDescription();

            nodeToBrowse1.NodeId = sourceId;
            nodeToBrowse1.BrowseDirection = BrowseDirection.Forward;
            nodeToBrowse1.ReferenceTypeId = ReferenceTypeIds.HasProperty;
            nodeToBrowse1.IncludeSubtypes = true;
            nodeToBrowse1.NodeClassMask = 0;
            nodeToBrowse1.ResultMask = (uint)BrowseResultMask.All;

            BrowseDescriptionCollection nodesToBrowse = new BrowseDescriptionCollection();
            nodesToBrowse.Add(nodeToBrowse1);

            // fetch property references from the server.
            ReferenceDescriptionCollection references = FormUtils.Browse(m_OpcUaClient.Session, nodesToBrowse, false);

            if (references == null)
            {
                return null;
            }

            for (int ii = 0; ii < references.Count; ii++)
            {
                // ignore external references.
                if (references[ii].NodeId.IsAbsolute)
                {
                    continue;
                }

                ReadValueId nodeToRead2 = new ReadValueId();
                nodeToRead2.NodeId = (NodeId)references[ii].NodeId;
                nodeToRead2.AttributeId = Attributes.Value;
                nodesToRead.Add(nodeToRead2);
            }

            // read all values.
            DataValueCollection results = null;
            DiagnosticInfoCollection diagnosticInfos = null;

            m_OpcUaClient.Session.Read(
                null,
                0,
                TimestampsToReturn.Neither,
                nodesToRead,
                out results,
                out diagnosticInfos);

            ClientBase.ValidateResponse(results, nodesToRead);
            ClientBase.ValidateDiagnosticInfos(diagnosticInfos, nodesToRead);

            return results[0];
        }

        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            m_OpcUaClient.Disconnect();
        }

        #region tree func ----------------------------------------------------------------------------
        private void BrowseNodesTV_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            try
            {

                // check if node has already been expanded once.
                if (e.Node.Nodes.Count != 1)
                {
                    return;
                }

                if (e.Node.Nodes.Count > 0)
                {
                    if (e.Node.Nodes[0].Text != String.Empty)
                    {
                        return;
                    }
                }

                // get the source for the node.
                ReferenceDescription reference = e.Node.Tag as ReferenceDescription;

                if (reference == null || reference.NodeId.IsAbsolute)
                {
                    e.Cancel = true;
                    return;
                }

                // populate children.
                PopulateBranch((NodeId)reference.NodeId, e.Node.Nodes);
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException("", exception);
            }
        }
        private void BrowseNodesTV_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                RemoveAllSubscript();
                // get the source for the node.
                ReferenceDescription reference = e.Node.Tag as ReferenceDescription;

                if (reference == null || reference.NodeId.IsAbsolute)
                {
                    return;
                }

                // populate children.
                ShowMember((NodeId)reference.NodeId);
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException("", exception);
            }
        }

        #endregion


        #region  datagrid function  ----------------------------------------------------------------------------
        private async void ShowMember(NodeId sourceId)
        {

            textBox_nodeId.Text = sourceId.ToString();

            // dataGrid1.Rows.Clear();
            int index = 0;
            ReferenceDescriptionCollection references;
            try
            {
                references = await Task.Run(() =>
                {
                    return GetReferenceDescriptionCollection(sourceId);
                });
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException("", exception);
                return;
            }


            if (references?.Count > 0)
            {
                // 获取所有要读取的子节点
                List<NodeId> nodeIds = new List<NodeId>();
                for (int ii = 0; ii < references.Count; ii++)
                {
                    ReferenceDescription target = references[ii];
                    nodeIds.Add((NodeId)target.NodeId);
                }

                DateTime dateTimeStart = DateTime.Now;

                // 获取所有的值
                DataValue[] dataValues = await Task.Run(() =>
                {
                    return ReadOneNodeFiveAttributes(nodeIds);
                });

                label_time_spend.Text = (int)(DateTime.Now - dateTimeStart).TotalMilliseconds + " ms";

                // 显示
                for (int jj = 0; jj < dataValues.Length; jj += 5)
                {
                    AddDataGridView1NewRow(dataValues, jj, index++, nodeIds[jj / 5]);
                }

            }
            else
            {
                // 子节点没有数据的情况
                try
                {
                    DateTime dateTimeStart = DateTime.Now;
                    DataValue dataValue = m_OpcUaClient.ReadNode(sourceId);

                    if (dataValue.WrappedValue.TypeInfo?.ValueRank == ValueRanks.OneDimension)
                    {
                        // 数组显示
                        AddDataGridView1ArrayRow(sourceId, out index);
                    }
                    else
                    {
                        // 显示单个数本身
                        label_time_spend.Text = (int)(DateTime.Now - dateTimeStart).TotalMilliseconds + " ms";
                        AddDataGridView1NewRow(ReadOneNodeFiveAttributes(new List<NodeId>() { sourceId }), 0, index++, sourceId);
                    }
                }
                catch (Exception exception)
                {
                    ClientUtils.HandleException("", exception);
                    return;
                }
            }

            ClearDataGridViewRows(index);

        }


        private void AddDataGridView1NewRow(DataValue[] dataValues, int startIndex, int index, NodeId nodeId)
        {
            // int index = dataGridView1.Rows.Add();
            while (index >= dataGridView1.Rows.Count)
            {
                dataGridView1.Rows.Add();
            }
            DataGridViewRow dgvr = dataGridView1.Rows[index];
            dgvr.Tag = nodeId;

            if (dataValues[startIndex].WrappedValue.Value == null) return;
            NodeClass nodeclass = (NodeClass)dataValues[startIndex].WrappedValue.Value;

            dgvr.Cells[1].Value = dataValues[3 + startIndex].WrappedValue.Value; //name
            dgvr.Cells[5].Value = dataValues[4 + startIndex].WrappedValue.Value; //descriptionS
            dgvr.Cells[4].Value = GetDiscriptionFromAccessLevel(dataValues[2 + startIndex]);

            if (nodeclass == NodeClass.Object)
            {
                dgvr.Cells[0].Value = Properties.Resources.ClassIcon;
                dgvr.Cells[2].Value = "";
                dgvr.Cells[3].Value = nodeclass.ToString(); //type
            }
            else if (nodeclass == NodeClass.Method)
            {
                dgvr.Cells[0].Value = Properties.Resources.Method_636;
                dgvr.Cells[2].Value = "";
                dgvr.Cells[3].Value = nodeclass.ToString();
            }
            else if (nodeclass == NodeClass.Variable)
            {
                DataValue dataValue = dataValues[1 + startIndex];

                if (dataValue.WrappedValue.TypeInfo != null)
                {
                    dgvr.Cells[3].Value = dataValue.WrappedValue.TypeInfo.BuiltInType;
                    dgvr.Cells[6].Value = dataValue.StatusCode.ToString();
                    // dgvr.Cells[3].Value = dataValue.Value.GetType().ToString();
                    if (dataValue.WrappedValue.TypeInfo.ValueRank == ValueRanks.Scalar)
                    {
                        dgvr.Cells[2].Value = dataValue.WrappedValue.Value;
                        dgvr.Cells[0].Value = Properties.Resources.Enum_582;
                    }
                    else if (dataValue.WrappedValue.TypeInfo.ValueRank == ValueRanks.OneDimension)
                    {
                        dgvr.Cells[2].Value = dataValue.Value.GetType().ToString();
                        dgvr.Cells[0].Value = Properties.Resources.brackets_Square_16xMD;
                    }
                    else if (dataValue.WrappedValue.TypeInfo.ValueRank == ValueRanks.TwoDimensions)
                    {
                        dgvr.Cells[2].Value = dataValue.Value.GetType().ToString();
                        dgvr.Cells[0].Value = Properties.Resources.Module_648;
                    }
                    else
                    {
                        dgvr.Cells[2].Value = dataValue.Value.GetType().ToString();
                        dgvr.Cells[0].Value = Properties.Resources.ClassIcon;
                    }
                }
                else
                {
                    dgvr.Cells[0].Value = Properties.Resources.ClassIcon;
                    dgvr.Cells[2].Value = dataValue.Value;
                    dgvr.Cells[3].Value = "null";
                }
            }
            else
            {
                dgvr.Cells[2].Value = "";
                dgvr.Cells[0].Value = Properties.Resources.ClassIcon;
                dgvr.Cells[3].Value = nodeclass.ToString();
                dgvr.Cells[6].Value = "";
            }
        }

        private void AddDataGridView1ArrayRow(NodeId nodeId, out int index)
        {

            DateTime dateTimeStart = DateTime.Now;
            DataValue[] dataValues = ReadOneNodeFiveAttributes(new List<NodeId>() { nodeId });
            label_time_spend.Text = (int)(DateTime.Now - dateTimeStart).TotalMilliseconds + " ms";

            DataValue dataValue = dataValues[1];

            if (dataValue.WrappedValue.TypeInfo?.ValueRank == ValueRanks.OneDimension)
            {
                string access = GetDiscriptionFromAccessLevel(dataValues[2]);
                BuiltInType type = dataValue.WrappedValue.TypeInfo.BuiltInType;
                object des = dataValues[4].Value ?? "";
                object dis = dataValues[3].Value ?? type;

                Array array = dataValue.Value as Array;
                int i = 0;
                foreach (object obj in array)
                {
                    while (i >= dataGridView1.Rows.Count)
                    {
                        dataGridView1.Rows.Add();
                    }

                    DataGridViewRow dgvr = dataGridView1.Rows[i];

                    dgvr.Tag = null;

                    dgvr.Cells[0].Value = Properties.Resources.Enum_582;
                    dgvr.Cells[1].Value = $"{dis} [{i++}]";
                    dgvr.Cells[2].Value = obj;
                    dgvr.Cells[3].Value = type;
                    dgvr.Cells[4].Value = access;
                    dgvr.Cells[5].Value = des;
                }
                index = i;
            }
            else
            {
                index = 0;
            }
        }
        private void ClearDataGridViewRows(int index)
        {
            for (int i = dataGridView1.Rows.Count - 1; i >= index; i--)
            {
                if (i >= 0)
                {
                    dataGridView1.Rows.RemoveAt(i);
                }
            }
        }

        #endregion
        private void RemoveAllSubscript()
        {
            m_OpcUaClient?.RemoveAllSubscription();
        }




        private string GetDiscriptionFromAccessLevel(DataValue value)
        {
            if (value.WrappedValue.Value != null)
            {
                switch ((byte)value.WrappedValue.Value)
                {
                    case 0: return "None";
                    case 1: return "CurrentRead";
                    case 2: return "CurrentWrite";
                    case 3: return "CurrentReadOrWrite";
                    case 4: return "HistoryRead";
                    case 8: return "HistoryWrite";
                    case 12: return "HistoryReadOrWrite";
                    case 16: return "SemanticChange";
                    case 32: return "StatusWrite";
                    case 64: return "TimestampWrite";
                    default: return "None";
                }
            }
            else
            {
                return "null";
            }
        }

        #region 订阅刷新块 ------------------------------------------------------------------------
        private async void Subscript_Click(object sender, RoutedEventArgs e)
        {

            if (m_OpcUaClient != null)
            {
                RemoveAllSubscript();
                if (Subscript.Background != Brushes.LimeGreen)
                {
                    Subscript.Background = Brushes.LimeGreen;
                    // 判断当前的选择
                    if (string.IsNullOrEmpty(textBox_nodeId.Text)) return;


                    ReferenceDescriptionCollection references;
                    try
                    {
                        references = await Task.Run(() =>
                        {
                            return GetReferenceDescriptionCollection(new NodeId(textBox_nodeId.Text));
                        });
                    }
                    catch (Exception exception)
                    {
                        ClientUtils.HandleException("", exception);
                        return;
                    }

                    subNodeIds = new List<string>();
                    if (references?.Count > 0)
                    {
                        isSingleValueSub = false;
                        // 获取所有要订阅的子节点
                        for (int ii = 0; ii < references.Count; ii++)
                        {
                            ReferenceDescription target = references[ii];
                            subNodeIds.Add(((NodeId)target.NodeId).ToString());
                        }
                    }
                    else
                    {
                        isSingleValueSub = true;
                        // 子节点没有数据的情况
                        subNodeIds.Add(textBox_nodeId.Text);
                    }

                    m_OpcUaClient.AddSubscription("subTest", subNodeIds.ToArray(), SubCallBack);
                }
                else
                {
                    Subscript.Background = Brushes.LightGray;
                }
            }
        }

        private List<string> subNodeIds = new List<string>();
        private bool isSingleValueSub = false;

        private void SubCallBack(string key, MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs eventArgs)
        {


            MonitoredItemNotification notification = eventArgs.NotificationValue as MonitoredItemNotification;
            string nodeId = monitoredItem.StartNodeId.ToString();

            int index = subNodeIds.IndexOf(nodeId);
            if (index >= 0)
            {
                if (isSingleValueSub)
                {
                    if (notification.Value.WrappedValue.TypeInfo?.ValueRank == ValueRanks.OneDimension)
                    {
                        Array array = notification.Value.WrappedValue.Value as Array;
                        int i = 0;
                        foreach (object obj in array)
                        {
                            DataGridViewRow dgvr = dataGridView1.Rows[i];
                            dgvr.Cells[2].Value = obj;
                            i++;
                        }
                    }
                    else
                    {
                        dataGridView1.Rows[index].Cells[2].Value = notification.Value.WrappedValue.Value;
                    }
                }
                else
                {
                    dataGridView1.Rows[index].Cells[2].Value = notification.Value.WrappedValue.Value;
                }
            }
        }


        #endregion
        #region 点击了表格修改数据--------------------------------------------------------------------------

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value is BuiltInType builtInType)
            {
                dynamic value = null;
                if (dataGridView1.Rows[e.RowIndex].Tag is NodeId nodeId)
                {
                    // 节点
                    try
                    {
                        value = GetValueFromString(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), builtInType);
                    }
                    catch
                    {
                        MessageBox.Show("Invalid Input Value");
                        return;
                    }

                    if (!m_OpcUaClient.WriteNode(nodeId.ToString(), value))
                    {
                        MessageBox.Show("Failed to write value");
                    }
                }
                else
                {
                    // 点击了数组修改
                    IList<string> list = new List<string>();

                    for (int jj = 0; jj < dataGridView1.RowCount; jj++)
                    {
                        list.Add(dataGridView1.Rows[jj].Cells[e.ColumnIndex].Value.ToString());
                    }

                    try
                    {
                        value = GetArrayValueFromString(list, builtInType);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid Input Value: " + ex.Message);
                        return;
                    }

                    if (!m_OpcUaClient.WriteNode(textBox_nodeId.Text, value))
                    {
                        MessageBox.Show("Failed to write value");
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid data type");
            }

            //MessageBox.Show(
            //    "Type:" + dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.GetType().ToString() + Environment.NewLine +
            //    "Value:" + dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
        }

        private dynamic GetValueFromString(string value, BuiltInType builtInType)
        {
            switch (builtInType)
            {
                case BuiltInType.Boolean:
                    {
                        return bool.Parse(value);
                    }
                case BuiltInType.Byte:
                    {
                        return byte.Parse(value);
                    }
                case BuiltInType.DateTime:
                    {
                        return DateTime.Parse(value);
                    }
                case BuiltInType.Double:
                    {
                        return double.Parse(value);
                    }
                case BuiltInType.Float:
                    {
                        return float.Parse(value);
                    }
                case BuiltInType.Guid:
                    {
                        return Guid.Parse(value);
                    }
                case BuiltInType.Int16:
                    {
                        return short.Parse(value);
                    }
                case BuiltInType.Int32:
                    {
                        return int.Parse(value);
                    }
                case BuiltInType.Int64:
                    {
                        return long.Parse(value);
                    }
                case BuiltInType.Integer:
                    {
                        return int.Parse(value);
                    }
                case BuiltInType.LocalizedText:
                    {
                        return value;
                    }
                case BuiltInType.SByte:
                    {
                        return sbyte.Parse(value);
                    }
                case BuiltInType.String:
                    {
                        return value;
                    }
                case BuiltInType.UInt16:
                    {
                        return ushort.Parse(value);
                    }
                case BuiltInType.UInt32:
                    {
                        return uint.Parse(value);
                    }
                case BuiltInType.UInt64:
                    {
                        return ulong.Parse(value);
                    }
                case BuiltInType.UInteger:
                    {
                        return uint.Parse(value);
                    }
                default: throw new Exception("Not supported data type");
            }
        }


        private dynamic GetArrayValueFromString(IList<string> values, BuiltInType builtInType)
        {
            switch (builtInType)
            {
                case BuiltInType.Boolean:
                    {
                        bool[] result = new bool[values.Count];
                        for (int i = 0; i < values.Count; i++)
                        {
                            result[i] = bool.Parse(values[i]);
                        }
                        return result;
                    }
                case BuiltInType.Byte:
                    {
                        byte[] result = new byte[values.Count];
                        for (int i = 0; i < values.Count; i++)
                        {
                            result[i] = byte.Parse(values[i]);
                        }
                        return result;
                    }
                case BuiltInType.DateTime:
                    {
                        DateTime[] result = new DateTime[values.Count];
                        for (int i = 0; i < values.Count; i++)
                        {
                            result[i] = DateTime.Parse(values[i]);
                        }
                        return result;
                    }
                case BuiltInType.Double:
                    {
                        double[] result = new double[values.Count];
                        for (int i = 0; i < values.Count; i++)
                        {
                            result[i] = double.Parse(values[i]);
                        }
                        return result;
                    }
                case BuiltInType.Float:
                    {
                        float[] result = new float[values.Count];
                        for (int i = 0; i < values.Count; i++)
                        {
                            result[i] = float.Parse(values[i]);
                        }
                        return result;
                    }
                case BuiltInType.Guid:
                    {
                        Guid[] result = new Guid[values.Count];
                        for (int i = 0; i < values.Count; i++)
                        {
                            result[i] = Guid.Parse(values[i]);
                        }
                        return result;
                    }
                case BuiltInType.Int16:
                    {
                        short[] result = new short[values.Count];
                        for (int i = 0; i < values.Count; i++)
                        {
                            result[i] = short.Parse(values[i]);
                        }
                        return result;
                    }
                case BuiltInType.Int32:
                    {
                        int[] result = new int[values.Count];
                        for (int i = 0; i < values.Count; i++)
                        {
                            result[i] = int.Parse(values[i]);
                        }
                        return result;
                    }
                case BuiltInType.Int64:
                    {
                        long[] result = new long[values.Count];
                        for (int i = 0; i < values.Count; i++)
                        {
                            result[i] = long.Parse(values[i]);
                        }
                        return result;
                    }
                case BuiltInType.Integer:
                    {
                        int[] result = new int[values.Count];
                        for (int i = 0; i < values.Count; i++)
                        {
                            result[i] = int.Parse(values[i]);
                        }
                        return result;
                    }
                case BuiltInType.LocalizedText:
                    {
                        return values.ToArray();
                    }
                case BuiltInType.SByte:
                    {
                        sbyte[] result = new sbyte[values.Count];
                        for (int i = 0; i < values.Count; i++)
                        {
                            result[i] = sbyte.Parse(values[i]);
                        }
                        return result;
                    }
                case BuiltInType.String:
                    {
                        return values.ToArray();
                    }
                case BuiltInType.UInt16:
                    {
                        ushort[] result = new ushort[values.Count];
                        for (int i = 0; i < values.Count; i++)
                        {
                            result[i] = ushort.Parse(values[i]);
                        }
                        return result;
                    }
                case BuiltInType.UInt32:
                    {
                        uint[] result = new uint[values.Count];
                        for (int i = 0; i < values.Count; i++)
                        {
                            result[i] = uint.Parse(values[i]);
                        }
                        return result;
                    }
                case BuiltInType.UInt64:
                    {
                        ulong[] result = new ulong[values.Count];
                        for (int i = 0; i < values.Count; i++)
                        {
                            result[i] = ulong.Parse(values[i]);
                        }
                        return result;
                    }
                case BuiltInType.UInteger:
                    {
                        uint[] result = new uint[values.Count];
                        for (int i = 0; i < values.Count; i++)
                        {
                            result[i] = uint.Parse(values[i]);
                        }
                        return result;
                    }
                default: throw new Exception("Not supported data type");
            }
        }
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value is BuiltInType builtInType)
            {
                if (
                    builtInType == BuiltInType.Boolean ||
                    builtInType == BuiltInType.Byte ||
                    builtInType == BuiltInType.DateTime ||
                    builtInType == BuiltInType.Double ||
                    builtInType == BuiltInType.Float ||
                    builtInType == BuiltInType.Guid ||
                    builtInType == BuiltInType.Int16 ||
                    builtInType == BuiltInType.Int32 ||
                    builtInType == BuiltInType.Int64 ||
                    builtInType == BuiltInType.Integer ||
                    builtInType == BuiltInType.LocalizedText ||
                    builtInType == BuiltInType.SByte ||
                    builtInType == BuiltInType.String ||
                    builtInType == BuiltInType.UInt16 ||
                    builtInType == BuiltInType.UInt32 ||
                    builtInType == BuiltInType.UInt64 ||
                    builtInType == BuiltInType.UInteger
                    )
                {

                }
                else
                {
                    e.Cancel = true;
                    MessageBox.Show("Not support the Type of modify value!");
                    return;
                }
            }
            else
            {
                e.Cancel = true;
                MessageBox.Show("Not support the Type of modify value!");
                return;
            }


            if (!dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value.ToString().Contains("Write"))
            {
                e.Cancel = true;
                MessageBox.Show("Not support the access of modify value!");
            }
        }

        #endregion
        #region  mine_Added ---------------------------------------------------------------------------------------

        private void dataGridView2_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var contextMenu = FindResource("ContextMenu1") as System.Windows.Controls.ContextMenu;
                DataGridView.HitTestInfo hit = dataGridView1.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.Cell)
                {
                    DataGridViewRow row = dataGridView1.Rows[hit.RowIndex];
                    dataGridView2.Rows[hit.RowIndex].Selected = true;
                    contextMenu.IsOpen = true;
                }
            }
        }

        //是否为最终节点
        private bool IsTerminalNode(TreeNode node)
        {
            return node.Nodes.Count == 0;
        }
        //当前节点属于第几层
        private int GetChildNodeLevel(TreeNode node)
        {
            int level = 0;
            while (node != null)
            {
                level++;
                node = node.Parent;
            }
            return level;
        }

        private void ReadNodes(NodeId sourceId, List<NodeId> nodeIds)
        {
            ReferenceDescriptionCollection references;
            try
            {
                references = GetReferenceDescriptionCollection(sourceId);
                //references = await Task.Run(() =>
                //{
                //    return GetReferenceDescriptionCollection(sourceId);
                //});
            }
            catch (Exception)
            {
                return;
            }
            if (references?.Count > 0)
            {
                foreach (var item in references)
                {
                    if (item.NodeClass == NodeClass.Variable)
                    {
                        ReferenceDescription target = item;
                        nodeIds.Add((NodeId)target.NodeId);
                    }
                    else if (item.NodeClass == Opc.Ua.NodeClass.Object || item.NodeClass == Opc.Ua.NodeClass.ObjectType)
                    {
                        ReadNodes((NodeId)item.NodeId, nodeIds); // 递归获取子节点的Variable
                    }
                }
            }
            else
            {
                nodeIds.Add(sourceId);
            }
        }

        private void AddDataGridView2NewRow(DataValue[] dataValues, int startIndex, NodeId nodeId)
        {

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells["address"].Value.ToString() == nodeId.ToString())
                {
                    MessageBox.Show("已添加该节点");
                    return;
                }
            }

            int rowNo = dataGridView2.Rows.Add();
            DataGridViewRow dgvr = dataGridView2.Rows[rowNo];
            dgvr.Tag = nodeId;

            if (dataValues[startIndex].WrappedValue.Value == null) return;

            NodeClass nodeclass = (NodeClass)dataValues[startIndex].WrappedValue.Value;

            dgvr.Cells[0].Value = dataValues[3 + startIndex].WrappedValue.Value; //name
            dgvr.Cells[1].Value = nodeId.ToString();
            dgvr.Cells[3].Value = dataValues[4 + startIndex].WrappedValue.Value; //description

            if (nodeclass == NodeClass.Object)
            {
                dgvr.Cells[2].Value = nodeclass.ToString(); //type
            }
            else if (nodeclass == NodeClass.Method)
            {
                dgvr.Cells[2].Value = nodeclass.ToString();
            }
            else if (nodeclass == NodeClass.Variable)
            {
                DataValue dataValue = dataValues[1 + startIndex];

                if (dataValue.WrappedValue.TypeInfo != null)
                {
                    dgvr.Cells[2].Value = dataValue.WrappedValue.TypeInfo.BuiltInType;
                }
                else
                {
                    dgvr.Cells[2].Value = "null";
                }
            }
            else
            {
                dgvr.Cells[2].Value = nodeclass.ToString();
            }
        }

        private void AddDataGridView2ArrayRow(NodeId nodeId, out int index)
        {

            DataValue[] dataValues = ReadOneNodeFiveAttributes(new List<NodeId>() { nodeId });
            DataValue dataValue = dataValues[1];

            if (dataValue.WrappedValue.TypeInfo?.ValueRank == ValueRanks.OneDimension)
            {
                BuiltInType type = dataValue.WrappedValue.TypeInfo.BuiltInType; //type
                object des = dataValues[4].Value ?? ""; //description
                object dis = dataValues[3].Value ?? type; //name

                Array array = dataValue.Value as Array;
                int i = 0;
                foreach (object obj in array)
                {
                    int rowNo = dataGridView2.Rows.Add();
                    DataGridViewRow dgvr = dataGridView2.Rows[rowNo];
                    dgvr.Tag = nodeId;

                    dgvr.Cells[0].Value = $"{dis}[{i++}]";
                    dgvr.Cells[1].Value = $"{nodeId}[{i}]";
                    dgvr.Cells[2].Value = type;
                    dgvr.Cells[3].Value = des;
                }
                index = i;
            }
            else
            {
                index = 0;
            }
        }
        //delete
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
            dataGridView2.Rows.Remove(selectedRow); // 从 DataGridView 中移除选中的行
        }
        //add
        private async void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            int index = 0;
            ReferenceDescription reference = BrowseNodesTV.SelectedNode.Tag as ReferenceDescription;

            if (reference == null || reference.NodeId.IsAbsolute)
            {
                return;
            }

            var sourceId = (NodeId)reference.NodeId;

            List<NodeId> nodeIds = new List<NodeId>();

            ReadNodes(sourceId, nodeIds);

            if (nodeIds.Count > 1)
            {
                // 获取所有的值
                DataValue[] dataValues = await Task.Run(() =>
                {
                    return ReadOneNodeFiveAttributes(nodeIds);
                });

                // 显示
                for (int jj = 0, kk = 1; jj < dataValues.Length; jj += 5, kk += 5)
                {
                    AddDataGridView2NewRow(dataValues, jj, nodeIds[jj / 5]);
                }
            }
            else if (nodeIds.Count == 1)
            {
                try
                {
                    DataValue dataValue = m_OpcUaClient.ReadNode(sourceId);

                    if (dataValue.WrappedValue.TypeInfo?.ValueRank == ValueRanks.OneDimension)
                    {
                        // 数组显示
                        AddDataGridView2ArrayRow(sourceId, out index);
                    }
                    else
                    {
                        // 显示单个数本身
                        AddDataGridView2NewRow(ReadOneNodeFiveAttributes(new List<NodeId>() { sourceId }), 0, sourceId);
                    }
                }
                catch (Exception exception)
                {
                    ClientUtils.HandleException("", exception);
                    return;
                }
            }
        }

        private void BrowseNodesTV_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var contextMenu = FindResource("ContextMenu2") as System.Windows.Controls.ContextMenu;
                TreeNode tn = BrowseNodesTV.GetNodeAt(e.X, e.Y);
                if (tn != null)
                {

                    BrowseNodesTV.SelectedNode = tn;

                    //if (IsTerminalNode(BrowseNodesTV.SelectedNode))
                    //{
                    //    tn.ContextMenuStrip = contextMenuStrip1;
                    //}
                    contextMenu.IsOpen = true;
                }
            }
        }
    }
    #endregion
}

