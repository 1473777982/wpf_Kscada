在项目开发的过程，通常会遇到一些表格数据的绑定，因为没有WPF的开发经验所以一边摸索一边开发，所幸WPF的上手难度不大，开发过程较为顺利。不过在使用DataGrid的时候还是遇到了一点阻绊遇。所以在这里讲一下这个DataGrid应该怎么用，以及要注意的事情。

DataGrid是个非常实用的控件，可以用来展示及获取较为复杂的数据结构。

要在WPF下使用DataGrid并绑定数据，大致操作如下：

1、在资源视图xml文件中添加DataGrid，并设置绑定，如下。

< Window x: Class = "Wpfdemo.Window1"

        xmlns = "http://schemas.microsoft.com/winfx/2006/xaml/presentation"

        xmlns: x = "http://schemas.microsoft.com/winfx/2006/xaml"

        xmlns: d = "http://schemas.microsoft.com/expression/blend/2008"

        xmlns: mc = "http://schemas.openxmlformats.org/markup-compatibility/2006"

        xmlns: local = "clr-namespace:Wpfdemo"

        mc: Ignorable = "d"

        Title = "Window1" Height = "300" Width = "300" >
    

        < Grid >
    

            < DataGrid AutoGenerateColumns = "False" HorizontalAlignment = "Stretch" Margin = "5,5" Name = "dataGrid1" VerticalAlignment = "Stretch" ItemsSource = "{Binding}" HorizontalGridLinesBrush = "Gainsboro" VerticalGridLinesBrush = "Gainsboro" >
                   

                               < DataGrid.Columns >
                   

                                   < DataGridTextColumn Header = " ID " Binding = "{Binding ID}" ></ DataGridTextColumn >
                      

                                      < DataGridTextColumn Header = "姓 名" Width = "100" Binding = "{Binding Name}" ></ DataGridTextColumn >
                          

                                          < DataGridTextColumn Header = "电 话" Width = "100" Binding = "{Binding PhoneNumber}" ></ DataGridTextColumn >
                              

                                              < DataGridTextColumn Header = "住 址" Width = "100" Binding = "{Binding Address}" ></ DataGridTextColumn >
                                  

                                              </ DataGrid.Columns >
                                  

                                          </ DataGrid >
                                  

                                      </ Grid >
                                  

                                  </ Window >

                                  在初始化代码中设置DataGrid绑定到的对象。
 {

    public Window1()

    {

        InitializeComponent();

        DataTable dt = new System.Data.DataTable();

        dt.Columns.Add("ID", typeof(int));

        dt.Columns.Add("Name", typeof(string));

        dt.Columns.Add("PhoneNumber", typeof(string));

        dt.Columns.Add("Address", typeof(string));

        DataRow row = dt.NewRow();

        row["ID"] = 1;

        row["Name"] = "张三";

        row["PhoneNumber"] = "239456";

        row["Address"] = "北京";

        dt.Rows.Add(row);

        row = dt.NewRow();

        row["ID"] = 2;

        row["Name"] = "李四";

        row["PhoneNumber"] = "982089*5";

        row["Address"] = "广东";

        dt.Rows.Add(row);

        //dataGrid1.DataContext = dt;

        dataGrid1.ItemsSource = dt.DefaultView;

        //设置网格线

        dataGrid1.GridLinesVisibility = DataGridGridLinesVisibility.All;

    }
————————————————
版权声明：本文为CSDN博主「--Rainman」的原创文章，遵循CC 4.0 BY - SA版权协议，转载请附上原文出处链接及本声明。
原文链接：https://blog.csdn.net/weixin_44541250/article/details/112909506






    #region  //DataGrid  样式
    < Page.Resources >
        < Style TargetType = "DataGrid" >
 
             < !--网格线颜色-- >
 
             < Setter Property = "CanUserResizeColumns" Value = "True" />
    
                < Setter Property = "Background" Value = "#FFD2DCE6" />
       
                   < Setter Property = "BorderBrush" Value = "#FFD2DCE6" />
          
                      < Setter Property = "HorizontalGridLinesBrush" >
           
                           < Setter.Value >
           
                               < SolidColorBrush Color = "#FFD2DCE6" />
            
                            </ Setter.Value >
            
                        </ Setter >
            
                        < Setter Property = "VerticalGridLinesBrush" >
             
                             < Setter.Value >
             
                                 < SolidColorBrush Color = "#FFD2DCE6" />
              
                              </ Setter.Value >
              
                          </ Setter >
              
                      </ Style >
              

                      < Style TargetType = "DataGridColumnHeader" >
               
                           < Setter Property = "SnapsToDevicePixels" Value = "True" />
                  
                              < Setter Property = "MinWidth" Value = "0" />
                     
                                 < Setter Property = "MinHeight" Value = "28" />
                        
                                    < Setter Property = "Foreground" Value = "#323433" />
                           
                                       < Setter Property = "FontSize" Value = "14" />
                              
                                          < !--< Setter Property = "Cursor" Value = "Hand" /> -->
                                  
                                              < Setter Property = "Template" >
                                   
                                                   < Setter.Value >
                                   
                                                       < ControlTemplate TargetType = "DataGridColumnHeader" >
                                    
                                                            < Border x: Name = "BackgroundBorder" BorderThickness = "0,1,0,1"
                             BorderBrush = "#FFD2DCE6"
                              Width = "Auto" >
                            < Grid >
                                < Grid.ColumnDefinitions >
                                    < ColumnDefinition Width = "*" />
 
                                 </ Grid.ColumnDefinitions >
 
                                 < ContentPresenter  Margin = "0,0,0,0" VerticalAlignment = "Center" HorizontalAlignment = "Center" />
      
                                      < Path x: Name = "SortArrow" Visibility = "Collapsed" Data = "M0,0 L1,0 0.5,1 z" Stretch = "Fill"  Grid.Column = "2" Width = "8" Height = "6" Fill = "White" Margin = "0,0,50,0"
                            VerticalAlignment = "Center" RenderTransformOrigin = "1,1" />
  
                                  < Rectangle Width = "1" Fill = "#FFD2DCE6" HorizontalAlignment = "Right" Grid.ColumnSpan = "1" />
         
                                         < !--< TextBlock  Background = "Red" >
           
                                       < ContentPresenter ></ ContentPresenter ></ TextBlock > -->
           
                                           < Thumb x: Name = "PART_RightHeaderGripper"
                                               Cursor = "SizeWE"
                                                HorizontalAlignment = "Right"
                                                Width = "1"
                                                Height = "25"
                                                VerticalAlignment = "Center" >
                                </ Thumb >
                            </ Grid >
                        </ Border >
                    </ ControlTemplate >
                </ Setter.Value >
            </ Setter >
            < Setter Property = "Height" Value = "25" />
   
           </ Style >
   
           < !--行样式触发-- >
   
           < !--背景色改变必须先设置cellStyle 因为cellStyle会覆盖rowStyle样式-- >
    
            < Style  TargetType = "DataGridRow" >
     
                 < Setter Property = "Background" Value = "#F2F2F2" />
        
                    < Setter Property = "Height" Value = "25" />
           
                       < Setter Property = "Foreground" Value = "Black" />
              
                          < Style.Triggers >
              
                              < !--隔行换色-- >
              
                              < Trigger Property = "AlternationIndex" Value = "0" >
                 
                                     < Setter Property = "Background" Value = "#e7e7e7" />
                    
                                    </ Trigger >
                    
                                    < Trigger Property = "AlternationIndex" Value = "1" >
                       
                                           < Setter Property = "Background" Value = "#f2f2f2" />
                          
                                          </ Trigger >
                          

                                          < Trigger Property = "IsMouseOver" Value = "True" >
                             
                                                 < Setter Property = "Background" Value = "LightGray" />
                                
                                                    < !--< Setter Property = "Foreground" Value = "White" /> -->
                                    
                                                    </ Trigger >
                                    

                                                    < Trigger Property = "IsSelected" Value = "True" >
                                       
                                                           < !--< Setter Property = "Foreground" Value = "Yellow" /> -->
                                           
                                                               < Setter Property = "Background" Value = "LightBlue" />
                                              
                                                              </ Trigger >
                                              
                                                          </ Style.Triggers >
                                              
                                                      </ Style >
                                              

                                                      < !--单元格样式触发-- >
                                              
                                                      < Style TargetType = "DataGridCell" >
                                               
                                                           < Setter Property = "Template" >
                                                
                                                                < Setter.Value >
                                                
                                                                    < ControlTemplate TargetType = "DataGridCell" >
                                                 
                                                                         < TextBlock TextAlignment = "Center" VerticalAlignment = "Center" >
                                                    
                                                                               < ContentPresenter />
                                                    
                                                                            </ TextBlock >
                                                    
                                                                        </ ControlTemplate >
                                                    
                                                                    </ Setter.Value >
                                                    
                                                                </ Setter >
                                                    
                                                                < Style.Triggers >
                                                    
                                                                    < Trigger Property = "IsSelected" Value = "True" >
                                                       
                                                                           < !--< Setter Property = "Background" Value = "Red" /> -->
                                                           
                                                                               < !--< Setter Property = "BorderThickness" Value = "0" /> -->
                                                               
                                                                                   < Setter Property = "Foreground" Value = "#FF27AE82" />
                                                                  
                                                                                  </ Trigger >
                                                                  
                                                                              </ Style.Triggers >
                                                                  
                                                                          </ Style >
                                                                  
                                                                      </ Page.Resources >
                                                                  
                                                                      < Grid x: Name = "Page08MFC" Background = "White" Height = "945" Width = "1920" HorizontalAlignment = "Left" VerticalAlignment = "Top" >
                                                                              
                                                                                      < DataGrid Name = "grid_saffer"
                                  IsReadOnly = "True"
                                  AlternationCount = "2"
                                  AutoGenerateColumns = "true" Margin = "453,171,507,260" Background = "{x:Null}" BorderBrush = "{x:Null}" >
      
                  < DataGrid.Columns >
      
                      < DataGridTextColumn Header = "开始时间"
                                                    Width = "1*"
                                                    Binding = "{Binding start}" />
                < DataGridTextColumn Header = "结束时间"
                                                    Width = "1*"
                                                    Binding = "{Binding end}" />
                < DataGridTextColumn Header = "地点"
                                                    Width = "1*"
                                                    Binding = "{Binding location}" />
            </ DataGrid.Columns >
        </ DataGrid >
    </ Grid >
    #endregion
