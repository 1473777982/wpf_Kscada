//纯C#代码实现自定义dialog窗口，
public class MyDialog : Window
{
    private Button btn1;
    private Button btn2;

    public MyDialog()
    {
        init();  //初始化Mainwindow
    }

    private void init()
    {
        //设置窗体
        this.Width = 200;
        this.Height = 200;
        this.Left = this.Top = 400;
        this.Title = "哈哈哈";

        //创建停靠面板;
        StackPanel panel = new StackPanel();

        //创建按钮对象;
        btn1 = new Button();
        btn1.Height = 20;
        btn1.Width = 100;
        btn1.Content = "clickme";
        btn1.Margin = new Thickness(30);
        btn1.Click += btn1_click;
        btn2 = new Button();
        btn2.Height = 20;
        btn2.Width = 100;
        btn2.Content = "clickme";
        btn2.Margin = new Thickness(5);
        btn2.Click += btn2_click;
        //创建容器panel 里面放上 btn1
        IAddChild container = panel;
        container.AddChild(btn1);
        container.AddChild(btn2);

        //创建容器Mainwindow 放上 panel 
        container = this;
        container.AddChild(panel);
    }

    private void btn1_click(object sender, RoutedEventArgs e)
    {
        btn1.Content = "谢谢1";
    }

    private void btn2_click(object sender, RoutedEventArgs e)
    {
        btn2.Content = "谢谢2";
    }
}