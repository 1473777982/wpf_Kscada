public class
{
    //实例化AutoResetEvent对象，参数bool表示are初始的状态，true表示通过
    private static AutoResetEvent are = new AutoResetEvent(true);
Thread th = new Thread(new ThreadStart(ThTest));
private static ThTest()
{
    are.WaitOne();
    Console.WriteLine(1);
    are.WaitOne();
    Console.WriteLine(2);
    are.WaitOne();
    Console.WriteLine(3);
}

主函数()
    {
    th.Start();
    Console.ReadLine();
    are.Set();
}

//输出
1
    2
    
    //解析
    //遇到第一个are.WaitOne()时，由于阻塞事件对象are的初始状态为可通过（true），所以th直接通过，同时are的状态改为不可通过，接着输出1
    //遇到第二个are.WaitOne()时，由于are的状态为不可通过，所以th被are阻塞，等待其他线程发送信号。
    //主线程上are.Set()发送了are处可通过的信号，th通过第二个are.WaitOne()，同时are的状态改为不可通过，输出2
    //遇到第三个are.WaitOne()，th又被阻塞，它只能等待，直到主线程结束也没有通过，th被强行终止
    //AutoResetResult中的Reset()常用于线程内部自阻塞
}

    public class
{
    //实例化ManualResetEvent对象，参数bool表示mre初始的状态，false表示不通过
    private static ManualResetEvent mre = new ManualResetEvent(false);
Thread th = new Thread(new ThreadStart(ThTest));
private static ThTest()
{
    mre.WaitOne();
    Console.WriteLine(1);
    mre.WaitOne();
    Console.WriteLine(2);
    mre.Reset();
    mre.WaitOne();
    Console.WriteLine(3);
    mre.WaitOne();
    Console.WriteLine(4);
}

主函数()
    {
    th.Start();
    Console.ReadLine();
    mre.Set();
}

//输出
1
    2
    
    //解析
    //遇到第一个mre.WaitOne()时，由于阻塞事件对象mre的初始状态为不可通过（false），所以th阻塞
    //这时主线程发送mre.Set()使mre变为通过状态，th通过第一个mre.WaitOne()，输出1
    //紧接着遇到第二个mre.WaitOne()，mre状态还是通过，th通过第二个mre.WaitOne()，输出2
    //这时线程内部将mre状态置为不可通过
    //遇到第三个mre.WaitOne()时，th被阻塞，mre状态为不可通过，th一直等待
    //直到主线程结束也没有通过，th被强行终止
}
    理解了AutoResetEvent后再理解ManualResetEvent也就不难了，AutoResetEvent与ManualResetEvent的区别是，
    AutoResetEvent.WaitOne()会自动改变事件对象的状态，
    即AutoResetEvent.WaitOne()每执行一次，事件的状态就改变一次，也就是从有信号变为无信号。
    而ManualResetEvent则是调用Set（）方法后其信号量不会自动改变，除非再设置Reset（）方法。