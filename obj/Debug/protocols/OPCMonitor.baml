   M S B A M L   `   `   `  ����>  :R2R, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:R2R.protocolsR2R.protocols  5   	   [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms 5   	   F BOpcUaHelper, Version=2.1.3.0, Culture=neutral, PublicKeyToken=nullJ4clr-namespace:OpcUaHelper.Forms;assembly=OpcUaHelperOpcUaHelper.Forms 5   	       R2R.protocols.OPCMonitor   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:R2R.protocols     5   	   Owf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms     5   	   Gforms14clr-namespace:OpcUaHelper.Forms;assembly=OpcUaHelper     5   	   -   5          � Title$  
OPCMonitor��5
   	    � Icon$? 9/Xwindow/Resources/立方体 立体 多面体 方块2.png��6`    � 	Resources 5         ContextMenu1  ContextMenu2��%�   &	        &	 Q     �� 5   
   .@�5      m� -   5        $��delete��5      6'   5      �� 5   
   .@�5      m� -   5        $	��add��5      6$   5      5      .��5      � .+�5   
   �� -   5         ��Name$ OK��5      $��确定��6   $��Right=�6*   $��Bottom=�6F   $��70��6a   
���   � K� ZIndex$ 3��6�   $��
0,0,114,10q�6�   6�   �� 5   
   -   5        $ Cancel��5      $��取消��6!   $��Right=�6.   $��Bottom=�6J   $��70��6e   
���   �$ 3��6�   $��	0,0,24,10q�6�   6�   �� 5   
   $
��Left=�6   $��	30,26,0,0q�6/   $
��Wrap=�6B   $��Address:��6V   $	��Top=�6f   �� 5   
   -   5         ��Name$ tblock1��5      $
��Left=�6#   $��
115,26,0,0q�6>    �� TextWrapping$
 Wrap=�6R   $!��opc.tcp://192.168.1.88:4840��6f   $	��Top=�6�   $	��462��6�   �� 5   
   -   5        $��Connect��5      $
��Left=�6#   $��
587,25,0,0q�6>   $	��Top=�6R   6j   7  1System.Windows.Forms.Integration.WindowsFormsHost  5   
   $
��Left=�6   $	��375��66   $��	30,58,0,0q�6C   $	��Top=�6V   $	��220��6n      Child. 5      "  OpcUaHelper.Forms.TreeViewEx  -   5        BrowseNodesTV��5   �   5      �� 5   
   -	   5         ��Name$ state��5      $
��Left=�6#   $��	30,0,0,10q�6>   $
��Wrap=�6Q   $��state��6e   $��Bottom=�6r   $ 3��6�   �� 5   
   -
   5        $ 	Subscript��5      $��	Subscript��6$   $
��Left=�68   $��
255,59,0,0q�6S   $	��Top=�6g   6   �� 5   
   $
��Left=�6   $��
336,60,0,0q�6/   $
��Wrap=�6C   $��Selected NodeId:��6W   $	��Top=�6o   �� 5   
   -   5        $ textBox_nodeId��5      $��449,60,160,0q�6*   $
 Wrap=�6@   $	��Top=�6T   �� 5    
   -   5        $ label_time_spend��5       $��Right=�6.   $��837,61,120,0q�6J   $
��Wrap=�6`   $	��0ms��6t   $	��Top=�6   �� 5!   
   $
��Left=�6   $��
33,439,0,0q�6/   $
��Wrap=�6C   $��Added NodeIds��6W   $	��Top=�6l     5"   
   $��255,82,20,355q�6   $	��350��62   . 5#      '  !System.Windows.Forms.DataGridView  	   BackgroundColorU QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a#  System.Drawing.ColorConverter$	 White 6   -   5        dataGridView1��"  System.Windows.Forms.Control
   AccessibleRole$
 Default=�5#   N      AllowUserToAddRows .     AllowUserToDeleteRows .     Anchor$
 None=�6�      BorderStyle$ FixedSingle=�6�      CellBorderStyle$ Single=�6�      ClipboardCopyMode$ EnableWithAutoHeaderText=�6�      AutoSizeColumnsMode$
 None=�6     AutoSizeRowsMode$
 None=�6)     ColumnHeadersBorderStyle$ Single=�6A     Dock$
 Fill=�6c     Columns 5$      2  ,System.Windows.Forms.DataGridViewImageColumn  5%      -  'System.Windows.Forms.DataGridViewColumn   Width$ 20��61   4  .System.Windows.Forms.DataGridViewTextBoxColumn  5&         
HeaderText$
 name��63     5'      $ value��63     5(      $
 type��63     5)      $ AccessLevel��63     5*      $ Description��63     5+      $ 
StatusCode��63   5-      5.        5/   
   $��35,459,20,35q�6   . 50        -   5        $	 White 50      dataGridView2��$
 Default=�6M    .   .  $
 None=�6�   $ FixedSingle=�6�   $ Single=�6�   $ EnableWithAutoHeaderText=�6�   $
 None=�6  $
 None=�6(  $ Single=�6@  $
 Fill=�6b   51        52      -   5        name��$
 name��52   A     53         Name$ address��63   $ address��6B     54      -   5        tagType��$ tagType��54   D     55      -   5        Description��$ Description��55   H   57      58      5:      5;      